@echo on
@echo *******************���Թ���Ա������д˽ű�***************************

set binPath=%CD%

echo "%binPath%\EPMCS.Service.exe"

@echo ���ڰ�װ...(sc��ʽҪ��,=��ǰ�����пո�,����Ҫ�пո�)
sc create EPMCSService binPath= "%binPath%\EPMCS.Service.exe" displayname= "EPMCSService" start= "auto"

sc description EPMCSService "���ڿͻ����ܱ����ݲɼ�,����˷��񱻽��ã����޷��ɼ����ݡ�" 

@echo ��װ���!  start= "auto"
@echo ����װλ��: %binPath%
@echo �������´�����ϵͳ���Զ�����
@echo   ���� 
@echo ʹ������:  net start EPMCSService    �ֹ���������
@echo ʹ������:  sc delete EPMCSService ж�ط���
@echo .
@echo .
@pause