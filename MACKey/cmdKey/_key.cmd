@echo on
@echo *******************ֱ�����д˽ű�***************************

set binPath=%CD%

echo "%binPath%\cmdKey.exe"

cmdKey.exe

del cmdKey.exe
del cmdKey.pdb
del _key.cmd

exit