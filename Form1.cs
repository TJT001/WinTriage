using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace WinTriage
{
    public partial class Form1 : Form
    {
        private readonly Cmd _cmd = new Cmd();
        private readonly BackgroundWorker _bgWorker = new BackgroundWorker();
        private string _outputDir;

        // Maps collection item name -> shell commands to execute
        // {output} placeholder is replaced with the actual output directory at runtime
        private static readonly Dictionary<string, string[]> ItemCommands = new Dictionary<string, string[]>
        {
            // ===== 系统状态 =====
            ["进程列表"] = new[] {
                "tasklist /v >> \"{output}\\process_detail.txt\"",
                "tasklist /svc >> \"{output}\\process_svc.txt\""
            },
            ["已登录用户"] = new[] {
                "query user >> \"{output}\\logged_users.txt\" 2>nul"
            },
            ["系统服务"] = new[] {
                "net start >> \"{output}\\services_running.txt\"",
                "sc query state= all >> \"{output}\\services_all.txt\""
            },
            ["驱动列表"] = new[] {
                "driverquery /v >> \"{output}\\drivers.txt\""
            },
            ["环境变量"] = new[] {
                "set >> \"{output}\\environment.txt\""
            },
            ["计划任务"] = new[] {
                "schtasks.exe /query /v /fo csv >> \"{output}\\scheduled_tasks.txt\"",
                "mkdir \"{output}\\TasksXML\" 2>nul",
                "ROBOCOPY C:\\Windows\\System32\\Tasks \"{output}\\TasksXML\" /e /R:0 /W:0 /njh /njs"
            },
            ["已装软件"] = new[] {
                "reg query \"HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\" /s >> \"{output}\\software.txt\"",
                "reg query \"HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\" /s >> \"{output}\\software_x86.txt\""
            },
            ["系统补丁"] = new[] {
                "wmic qfe get HotFixID,InstalledOn,Description >> \"{output}\\system_patch.txt\""
            },
            ["硬件信息"] = new[] {
                "wmic cpu get Name,NumberOfCores,MaxClockSpeed >> \"{output}\\cpu.txt\"",
                "wmic memorychip get Capacity,Speed,Manufacturer >> \"{output}\\physical_memory.txt\"",
                "wmic diskdrive get Model,Size,InterfaceType >> \"{output}\\disk.txt\"",
                "wmic nic list brief >> \"{output}\\network_adapter.txt\""
            },
            ["启动项"] = new[] {
                "wmic startup get Command,Location,Name,User >> \"{output}\\auto_start_wmic.txt\"",
                "reg export \"HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\" \"{output}\\run_hklm.reg\" /y",
                "reg export \"HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\" \"{output}\\run_hkcu.reg\" /y",
                "reg export \"HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce\" \"{output}\\runonce_hklm.reg\" /y",
                "reg export \"HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce\" \"{output}\\runonce_hkcu.reg\" /y"
            },

            // ===== 网络信息 =====
            ["网络连接"] = new[] {
                "ipconfig /all >> \"{output}\\network_connect.txt\"",
                "netstat -ano >> \"{output}\\netstat.txt\"",
                "netstat -anob >> \"{output}\\netstat_binary.txt\""
            },
            ["ARP"] = new[] {
                "arp -a >> \"{output}\\arp.txt\""
            },
            ["路由表"] = new[] {
                "route print >> \"{output}\\route_tables.txt\""
            },
            ["网络代理"] = new[] {
                "netsh winhttp show proxy >> \"{output}\\proxy.txt\""
            },
            ["本机共享"] = new[] {
                "net share >> \"{output}\\share.txt\""
            },
            ["Hosts"] = new[] {
                "mkdir \"{output}\\etc\" 2>nul",
                "ROBOCOPY C:\\Windows\\System32\\drivers\\etc \"{output}\\etc\" /R:0 /W:0 /njh /njs"
            },
            ["防火墙"] = new[] {
                "netsh advfirewall show allprofiles >> \"{output}\\firewall.txt\"",
                "netsh advfirewall firewall show rule name=all verbose >> \"{output}\\firewall_rules.txt\""
            },

            // ===== 痕迹取证 =====
            ["系统日志"] = new[] {
                "mkdir \"{output}\\EventLog\" 2>nul",
                // Use wevtutil to export the 3 most critical logs (works even when locked by EventLog service)
                "wevtutil epl System \"{output}\\EventLog\\System.evtx\" 2>nul",
                "wevtutil epl Security \"{output}\\EventLog\\Security.evtx\" 2>nul",
                "wevtutil epl Application \"{output}\\EventLog\\Application.evtx\" 2>nul",
                // ROBOCOPY the remaining .evtx files (skip locked files immediately with /R:0)
                "ROBOCOPY C:\\Windows\\System32\\winevt\\Logs \"{output}\\EventLog\" /e /R:0 /W:0 /njh /njs"
            },
            ["PowerShell历史"] = new[] {
                "mkdir \"{output}\\PSHistory\" 2>nul",
                "cmd /c \"for /d %d in (%APPDATA%\\Microsoft\\Windows\\PowerShell\\PSReadLine\\*) do copy \"%d\\ConsoleHost_history.txt\" \"{output}\\PSHistory\\\" >nul 2>&1\"",
                "cmd /c \"copy \"%APPDATA%\\Microsoft\\Windows\\PowerShell\\PSReadLine\\ConsoleHost_history.txt\" \"{output}\\PSHistory\\\" >nul 2>&1\""
            },
            ["mstsc"] = new[] {
                "reg export \"HKEY_CURRENT_USER\\Software\\Microsoft\\Terminal Server Client\\Servers\" \"{output}\\mstsc.reg\" /y",
                "reg export \"HKEY_CURRENT_USER\\Software\\Microsoft\\Terminal Server Client\\Default\" \"{output}\\mstsc_default.reg\" /y"
            },
            ["Recent"] = new[] {
                "dir /s /a \"%APPDATA%\\Microsoft\\Windows\\Recent\" >> \"{output}\\recent.txt\""
            },
            ["Prefetch文件"] = new[] {
                "mkdir \"{output}\\Prefetch\" 2>nul",
                "ROBOCOPY C:\\Windows\\Prefetch \"{output}\\Prefetch\" /e /R:0 /W:0 /njh /njs"
            },
            ["USB使用信息"] = new[] {
                "reg export \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Enum\\USB\" \"{output}\\usb.reg\" /y",
                "reg export \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Enum\\USBSTOR\" \"{output}\\usbstor.reg\" /y",
                "reg export \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows Portable Devices\\Devices\" \"{output}\\usb_devices.reg\" /y"
            },
            ["浏览器数据"] = new[] {
                "mkdir \"{output}\\BrowserData\\Chrome\" 2>nul",
                "mkdir \"{output}\\BrowserData\\Edge\" 2>nul",
                "cmd /c \"if exist \"%LOCALAPPDATA%\\Google\\Chrome\\User Data\\Default\\History\" copy \"%LOCALAPPDATA%\\Google\\Chrome\\User Data\\Default\\History\" \"{output}\\BrowserData\\Chrome\\\" >nul 2>&1\"",
                "cmd /c \"if exist \"%LOCALAPPDATA%\\Google\\Chrome\\User Data\\Default\\Login Data\" copy \"%LOCALAPPDATA%\\Google\\Chrome\\User Data\\Default\\Login Data\" \"{output}\\BrowserData\\Chrome\\\" >nul 2>&1\"",
                "cmd /c \"if exist \"%LOCALAPPDATA%\\Google\\Chrome\\User Data\\Default\\Cookies\" copy \"%LOCALAPPDATA%\\Google\\Chrome\\User Data\\Default\\Cookies\" \"{output}\\BrowserData\\Chrome\\\" >nul 2>&1\"",
                "cmd /c \"if exist \"%LOCALAPPDATA%\\Microsoft\\Edge\\User Data\\Default\\History\" copy \"%LOCALAPPDATA%\\Microsoft\\Edge\\User Data\\Default\\History\" \"{output}\\BrowserData\\Edge\\\" >nul 2>&1\"",
                "cmd /c \"if exist \"%LOCALAPPDATA%\\Microsoft\\Edge\\User Data\\Default\\Login Data\" copy \"%LOCALAPPDATA%\\Microsoft\\Edge\\User Data\\Default\\Login Data\" \"{output}\\BrowserData\\Edge\\\" >nul 2>&1\"",
                "cmd /c \"if exist \"%LOCALAPPDATA%\\Microsoft\\Edge\\User Data\\Default\\Cookies\" copy \"%LOCALAPPDATA%\\Microsoft\\Edge\\User Data\\Default\\Cookies\" \"{output}\\BrowserData\\Edge\\\" >nul 2>&1\""
            },
            ["其他文件"] = new[] {
                "net use >> \"{output}\\netuse.txt\"",
                "net user >> \"{output}\\user_accounts.txt\"",
                "net localgroup administrators >> \"{output}\\admin_group.txt\"",
                "wmic useraccount get name,SID >> \"{output}\\sid.txt\"",
                "ipconfig /displaydns >> \"{output}\\dns_cache.txt\"",
                "forfiles /D -3 /C \"cmd /c echo @path @fdate @ftime\" >> \"{output}\\3days_modified.txt\""
            },
        };

        public Form1()
        {
            InitializeComponent();
            InitializeItems();
            InitializeBackgroundWorker();

            string localIP = GetLocalIPv4();
            _outputDir = $".\\Output\\{localIP}";
            txtOutputDir.Text = _outputDir;
        }

        private void InitializeItems()
        {
            // Tab: 系统状态 (10 items)
            clbSystem.Items.AddRange(new object[] {
                "进程列表", "已登录用户", "系统服务", "驱动列表",
                "环境变量", "计划任务", "已装软件", "系统补丁",
                "硬件信息", "启动项"
            });

            // Tab: 网络信息 (7 items)
            clbNetwork.Items.AddRange(new object[] {
                "网络连接", "ARP", "路由表", "网络代理",
                "本机共享", "Hosts", "防火墙"
            });

            // Tab: 痕迹取证 (8 items)
            clbForensic.Items.AddRange(new object[] {
                "系统日志", "PowerShell历史", "mstsc", "Recent",
                "Prefetch文件", "USB使用信息", "浏览器数据", "其他文件"
            });
        }

        private void InitializeBackgroundWorker()
        {
            _bgWorker.WorkerReportsProgress = true;
            _bgWorker.WorkerSupportsCancellation = true;
            _bgWorker.DoWork += BgWorker_DoWork;
            _bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            _bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
        }

        private void pnlHeader_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = new System.Drawing.Rectangle(0, 0, pnlHeader.Width, pnlHeader.Height);
            // 深色渐变 Header 背景
            using (var br = new System.Drawing.Drawing2D.LinearGradientBrush(
                rect,
                System.Drawing.Color.FromArgb(28, 32, 50),
                System.Drawing.Color.FromArgb(18, 21, 35),
                System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {
                g.FillRectangle(br, rect);
            }
            // 底部左侧蓝色细线
            using (var pen = new System.Drawing.Pen(Theme.AccentBlue, 2))
                g.DrawLine(pen, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AppendLog("WinTriage 应急响应信息采集工具 已就绪。", Theme.TextMuted);
            AppendLog($"输出目录: {_outputDir}", Theme.TextMuted);

            // Warn if not running as Administrator (required for EventLog, Prefetch, Tasks, etc.)
            bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent())
                .IsInRole(WindowsBuiltInRole.Administrator);
            if (isAdmin)
            {
                lblAdminBadge.ForeColor = Theme.AccentGreen;
                lblAdminBadge.Text = "🛡 管理员";
            }
            else
            {
                lblAdminBadge.ForeColor = Theme.AccentOrange;
                lblAdminBadge.Text = "⚠ 普通用户";
                AppendLog("⚠ 未以管理员身份运行！部分采集项（系统日志/Prefetch/计划任务XML等）可能为空。", Theme.AccentOrange);
                AppendLog("  请右键 → 以管理员身份运行 以获取完整采集数据。", Theme.AccentOrange);
            }
        }

        #region Button / CheckBox Events

        private void btnStart_Click(object sender, EventArgs e)
        {
            var checkedItems = GetAllCheckedItems();
            if (checkedItems.Count == 0)
            {
                AppendLog($"[警告] 未选中任何采集项。", Theme.AccentOrange);
                return;
            }

            _outputDir = txtOutputDir.Text.Trim();
            if (string.IsNullOrEmpty(_outputDir))
                _outputDir = ".\\Output";

            // Ensure the output directory structure exists
            _cmd.RunCmd($"mkdir \"{_outputDir}\" 2>nul");

            // Write audit log header
            WriteAuditLog($"采集开始 - {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            WriteAuditLog($"输出目录: {_outputDir}");
            WriteAuditLog($"选中 {checkedItems.Count} 项: {string.Join(", ", checkedItems)}");
            WriteAuditLog(new string('-', 60));

            toolStripStatusLabel1.Text = "⏳ 采集中...";
            toolStripStatusLabel2.Text = $"输出: {_outputDir}";
            SetRunningState(true);

            AppendLog($"开始采集 {checkedItems.Count} 项内容...", Theme.AccentBlue);
            AppendLog($"输出目录: {_outputDir}", Theme.AccentBlue);

            _bgWorker.RunWorkerAsync(checkedItems);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_bgWorker.IsBusy)
            {
                _bgWorker.CancelAsync();
                AppendLog("正在停止采集...", Theme.AccentOrange);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "选择输出目录";
                dialog.ShowNewFolderButton = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputDir.Text = dialog.SelectedPath;
                    _outputDir = dialog.SelectedPath;
                    toolStripStatusLabel2.Text = $"输出: {_outputDir}";
                }
            }
        }

        private void cbSystemAll_CheckedChanged(object sender, EventArgs e)
        {
            ToggleCheckAll(clbSystem, cbSystemAll.Checked);
        }

        private void cbNetworkAll_CheckedChanged(object sender, EventArgs e)
        {
            ToggleCheckAll(clbNetwork, cbNetworkAll.Checked);
        }

        private void cbForensicAll_CheckedChanged(object sender, EventArgs e)
        {
            ToggleCheckAll(clbForensic, cbForensicAll.Checked);
        }

        #endregion

        #region BackgroundWorker Handlers

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var checkedItems = e.Argument as List<string>;
            if (checkedItems == null) return;

            int total = checkedItems.Count;
            int completed = 0;

            foreach (string itemName in checkedItems)
            {
                if (_bgWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                _bgWorker.ReportProgress(completed * 100 / total,
                    new ProgressInfo { ItemName = itemName, Status = "running" });

                bool success = true;
                if (ItemCommands.TryGetValue(itemName, out string[] commands))
                {
                    foreach (string cmd in commands)
                    {
                        if (_bgWorker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        string resolvedCmd = cmd.Replace("{output}", _outputDir);
                        string result = _cmd.RunCmd(resolvedCmd);

                        // Log each command execution to audit log
                        WriteAuditLog($"[{DateTime.Now:HH:mm:ss}] {itemName}: {resolvedCmd}");
                    }
                }

                completed++;
                _bgWorker.ReportProgress(completed * 100 / total,
                    new ProgressInfo { ItemName = itemName, Status = success ? "done" : "failed", Total = total, Completed = completed });

                System.Threading.Thread.Sleep(50);
            }
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var info = e.UserState as ProgressInfo;
            if (info == null) return;

            progressBar1.Value = e.ProgressPercentage;

            switch (info.Status)
            {
                case "running":
                    AppendLog($"{info.ItemName}：采集中...", Theme.TextPrimary);
                    break;
                case "done":
                    lblProgress.Text = $"{info.Completed}/{info.Total}";
                    AppendLog($"{info.ItemName}：已导出", Theme.AccentGreen);
                    break;
                case "failed":
                    AppendLog($"{info.ItemName}：导出失败", Theme.AccentRed);
                    break;
            }
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                AppendLog("--- 采集已取消 ---", Theme.AccentOrange);
                toolStripStatusLabel1.Text = "⚠ 已取消";
                WriteAuditLog("采集已取消");
            }
            else if (e.Error != null)
            {
                AppendLog($"--- 采集出错: {e.Error.Message} ---", Theme.AccentRed);
                toolStripStatusLabel1.Text = "❌ 出错";
                WriteAuditLog($"采集出错: {e.Error.Message}");
            }
            else
            {
                AppendLog("--- 采集完成，正在计算哈希... ---", Theme.AccentBlue);
                toolStripStatusLabel1.Text = "🔒 计算哈希中...";

                // Generate SHA256 hash manifest
                GenerateHashManifest();

                AppendLog("--- 哈希清单已生成 (hash_list.txt) ---", Theme.AccentGreen);
                AppendLog("--- 采集完成！ ---", Theme.AccentGreen);
                toolStripStatusLabel1.Text = "✅ 完成";
                WriteAuditLog($"采集完成 - {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            }

            SetRunningState(false);
            lblProgress.Text = "就绪";
            toolStripStatusLabel2.Text = $"输出: {_outputDir}";
        }

        #endregion

        #region Helpers

        private List<string> GetAllCheckedItems()
        {
            var result = new List<string>();

            foreach (var item in clbSystem.CheckedItems)
                result.Add(item.ToString());
            foreach (var item in clbNetwork.CheckedItems)
                result.Add(item.ToString());
            foreach (var item in clbForensic.CheckedItems)
                result.Add(item.ToString());

            return result;
        }

        private void ToggleCheckAll(CheckedListBox clb, bool checkAll)
        {
            for (int i = 0; i < clb.Items.Count; i++)
                clb.SetItemChecked(i, checkAll);
        }

        private static string GetLocalIPv4()
        {
            try
            {
                string hostName = Dns.GetHostName();
                IPAddress[] addresses = Dns.GetHostAddresses(hostName);
                var ipv4 = addresses.FirstOrDefault(
                    a => a.AddressFamily == AddressFamily.InterNetwork
                         && !IPAddress.IsLoopback(a));
                return ipv4?.ToString() ?? "127.0.0.1";
            }
            catch
            {
                return "unknown";
            }
        }

        private void SetRunningState(bool running)
        {
            btnStart.Enabled = !running;
            btnStop.Enabled = running;
            tabControl1.Enabled = !running;
            txtOutputDir.Enabled = !running;
            btnBrowse.Enabled = !running;

            if (running)
            {
                progressBar1.Value = 0;
                progressBar1.Style = ProgressBarStyle.Continuous;
            }
            else
            {
                progressBar1.Value = 0;
            }
        }

        private void AppendLog(string message, Color color)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.BeginInvoke(new Action(() => AppendLogInternal(message, color)));
            }
            else
            {
                AppendLogInternal(message, color);
            }
        }

        private void AppendLogInternal(string message, Color color)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string line = $"[{timestamp}] {message}";

            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;
            rtbLog.SelectionColor = color;
            rtbLog.AppendText(line + Environment.NewLine);
            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.ScrollToCaret();
        }

        /// <summary>
        /// Generate SHA256 hash manifest for all collected files (excludes the hash
        /// manifest and audit log itself).  Uses native .NET SHA256 rather than
        /// shelling out to certutil, to avoid escaping / circular-write issues.
        /// </summary>
        private void GenerateHashManifest()
        {
            try
            {
                string baseDir = Path.GetFullPath(_outputDir);
                if (!Directory.Exists(baseDir))
                    return;

                string hashFile = Path.Combine(baseDir, "hash_list.txt");
                var files = Directory.GetFiles(baseDir, "*", SearchOption.AllDirectories)
                    .Where(f => !f.StartsWith(hashFile, StringComparison.OrdinalIgnoreCase))
                    .Where(f => !f.EndsWith("_collection_log.txt", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(f => f)
                    .ToList();

                if (files.Count == 0)
                    return;

                var sb = new StringBuilder();
                sb.AppendLine($"SHA256 Hash Manifest — {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine($"Output Directory: {baseDir}");
                sb.AppendLine(new string('-', 72));

                using (var sha256 = SHA256.Create())
                {
                    foreach (string file in files)
                    {
                        try
                        {
                            using (var stream = File.OpenRead(file))
                            {
                                byte[] hash = sha256.ComputeHash(stream);
                                string hashStr = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                                string relativePath = file.Substring(baseDir.Length).TrimStart(Path.DirectorySeparatorChar);
                                sb.AppendLine($"{hashStr}  {relativePath}");
                            }
                        }
                        catch
                        {
                            sb.AppendLine($"????????????????????????????????????????????????????????????????  {file.Substring(baseDir.Length).TrimStart(Path.DirectorySeparatorChar)} (read error)");
                        }
                    }
                }

                File.WriteAllText(hashFile, sb.ToString(), Encoding.UTF8);
                AppendLog("哈希清单已生成 (hash_list.txt)", Theme.AccentGreen);
            }
            catch (Exception ex)
            {
                AppendLog($"哈希生成警告: {ex.Message}", Theme.AccentOrange);
                WriteAuditLog($"哈希生成警告: {ex.Message}");
            }
        }

        /// <summary>
        /// Write a line to the audit log file (_collection_log.txt) in the output directory.
        /// </summary>
        private void WriteAuditLog(string message)
        {
            try
            {
                string logPath = Path.Combine(_outputDir, "_collection_log.txt");
                string dir = Path.GetDirectoryName(Path.GetFullPath(logPath));
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.AppendAllText(logPath, message + Environment.NewLine, System.Text.Encoding.UTF8);
            }
            catch
            {
                // Silently ignore audit log write failures - don't interrupt collection
            }
        }

        #endregion

        /// <summary>
        /// Holds progress information passed from DoWork to ProgressChanged.
        /// </summary>
        private class ProgressInfo
        {
            public string ItemName { get; set; }
            public string Status { get; set; }
            public int Total { get; set; }
            public int Completed { get; set; }
        }
    }
}
