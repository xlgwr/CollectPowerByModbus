@ECHO OFF

ECHO 安装前执行脚本
ECHO.
ECHO 即将开始更新版本 %CurrentVersion%，按任意键继续
ECHO 如果在10秒钟内您没有按任意键继续，应该会看到一个执行超时的警告框。。。

net stop EPMCSService

PAUSE 1>nul 2>nul

