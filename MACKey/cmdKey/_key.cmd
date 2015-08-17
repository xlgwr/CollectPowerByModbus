@echo on
@echo *******************直接运行此脚本***************************

set binPath=%CD%

echo "%binPath%\cmdKey.exe"

cmdKey.exe

del cmdKey.exe
del cmdKey.pdb
del _key.cmd

exit