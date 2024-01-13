cd ..
sc create XiHanCore binPath= %~dp0XiHan.exe start= auto 
sc description XiHanCore "XiHanCore"
Net Start XiHanCore
pause
