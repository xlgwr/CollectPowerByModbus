@ECHO OFF

CLS

ECHO 自动更新测试脚本
ECHO.
ECHO 已经完成更新
ECHO.
ECHO 当前更新的版本是：%CurrentVersion%
ECHO 更新的原始目录是：%ApplicationDirectory%
ECHO 更新的地址是：%UpdateDownloadUrl%

net start EPMCSService

PAUSE 1>nul 2>nul