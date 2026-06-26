# WinTriage - 应急响应信息采集工具 v2.0

Windows 平台应急响应信息采集工具，面向安全事件发生后的**初步取证**和**现场勘查**。基于 .NET Framework 4.5 + WinForms 开发，通过调用系统命令行工具批量导出安全相关数据。

```
原始程序: GetInfo v1.2.1 By ra66it
重构版本: v2.0
目标框架: .NET Framework 4.5 (WinForms)
许可协议: Copyright © 2021
```

## 功能特性

- **25 类采集项**，覆盖进程、网络、持久化、痕迹四大维度
- **TabControl 分类布局**：系统状态 / 网络信息 / 痕迹取证
- **异步采集**：BackgroundWorker 驱动，界面不阻塞，支持随时停止
- **实时进度**：进度条 + 彩色时间戳日志
- **审计追溯**：自动生成 `_collection_log.txt` 记录每条命令执行时间
- **哈希校验**：采集完成后自动计算所有输出文件 SHA256，生成 `hash_list.txt`
- **IP 隔离**：输出目录默认以本机 IP 命名，便于多机并行采集

## 采集项矩阵

### 系统状态 (10 项)

| 采集项 | 执行命令 |
|---|---|
| 进程列表 | `tasklist /v` + `tasklist /svc` |
| 已登录用户 | `query user` |
| 系统服务 | `net start` + `sc query state= all` |
| 驱动列表 | `driverquery /v` |
| 环境变量 | `set` |
| 计划任务 | `schtasks.exe /query` + `ROBOCOPY Tasks` |
| 已装软件 | `reg query HKLM\...\Uninstall /s` |
| 系统补丁 | `wmic qfe get` |
| 硬件信息 | `wmic cpu / memorychip / diskdrive / nic` |
| 启动项 | `wmic startup` + 注册表 Run/RunOnce 导出 |

### 网络信息 (7 项)

| 采集项 | 执行命令 |
|---|---|
| 网络连接 | `ipconfig /all` + `netstat -ano` + `netstat -anob` |
| ARP | `arp -a` |
| 路由表 | `route print` |
| 网络代理 | `netsh winhttp show proxy` |
| 本机共享 | `net share` |
| Hosts | `ROBOCOPY drivers\etc` |
| 防火墙 | `netsh advfirewall show` + 规则详细导出 |

### 痕迹取证 (8 项)

| 采集项 | 执行命令 |
|---|---|
| 系统日志 | `ROBOCOPY winevt\Logs` |
| PowerShell 历史 | 拷贝 `ConsoleHost_history.txt` |
| mstsc | `reg export` RDP 连接历史 |
| Recent | `dir Recent` 目录列表 |
| Prefetch 文件 | `ROBOCOPY C:\Windows\Prefetch` |
| USB 使用信息 | `reg export` USB + USBSTOR + Portable Devices |
| 浏览器数据 | 拷贝 Chrome/Edge History / Login Data / Cookies |
| 其他文件 | `net use` / `net user` / DNS 缓存 / 3 天内修改文件 |

## 使用方式

```bash
# 编译
msbuild WinTriage.csproj /t:Build /p:Configuration=Release

# 或直接在 Visual Studio 中打开 WinTriage.sln 编译运行
```

1. 启动后**勾选**要采集的项（可按标签页"全选"）
2. 可选修改**输出目录**（默认 `.\Output\<本机IP>\`）
3. 点击 **▶ 开始采集**
4. 等待完成，可随时点击 **■ 停止**

## 输出结构

```
.\Output\192.168.1.100\
├── hash_list.txt              # SHA256 哈希清单
├── _collection_log.txt        # 审计日志
├── process_detail.txt         # 进程详情
├── process_svc.txt            # 进程-服务映射
├── logged_users.txt           # 已登录用户
├── services_all.txt           # 全部服务状态
├── drivers.txt                # 驱动列表
├── netstat.txt                # 网络连接
├── dns_cache.txt              # DNS 缓存
├── arp.txt                    # ARP 表
├── route_tables.txt           # 路由表
├── firewall.txt               # 防火墙策略
├── scheduled_tasks.txt        # 计划任务列表
├── software.txt               # 已装软件（注册表）
├── auto_start_wmic.txt        # 启动项
├── run_hklm.reg / run_hkcu.reg  # 注册表 Run 键
├── usb.reg / usbstor.reg      # USB 使用记录
├── mstsc.reg                  # RDP 连接历史
├── EventLog\                  # Windows 事件日志
├── Prefetch\                  # 应用程序预加载
├── TasksXML\                  # 计划任务 XML
├── etc\                       # Hosts 等网络配置
├── PSHistory\                 # PowerShell 命令历史
├── BrowserData\               # Chrome/Edge 浏览器数据
└── ...
```

## 设计参考

本工具参考以下标准和框架进行采集项设计：

- **NIST SP 800-61** — 计算机安全事件处理指南
- **SANS IR 框架** — 应急响应取证最佳实践
- **IOCE 挥发性优先级** — 数据采集顺序原则

## 局限性

本工具定位为**快速 triage（初步筛查）**工具，存在以下已知局限：

- 不含内存 Dump 能力（需 ProcDump / WinPmem 等配合）
- 不含 AmCache / ShimCache / Shellbags 等深度注册表取证
- 不含 $MFT / USN Journal 等文件系统级取证
- 不含 VSS 写保护 / 只读挂载机制
- 采集项硬编码，不支持 JSON/XML 配置文件扩展

如需完整的应急响应取证方案，建议配合 **KAPE**、**Velociraptor**、**FTK Imager** 等专业工具使用。

## 版本历史

| 版本 | 日期 | 变更 |
|---|---|---|
| v2.0 | 2026-06 | UI 全面重设计，25 类采集项，异步引擎，哈希校验，审计日志 |
