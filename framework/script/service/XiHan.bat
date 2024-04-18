cd ..
sc create XiHan binPath= %~dp0XiHan.exe start= auto 
sc description XiHan "XiHan"
Net Start XiHan
pause
