@ECHO OFF 

SET dbhost=127.0.0.1
SET dbuser=sa
SET dbpasswd=12345678
set dbName=master
SET sqlpath=%~dp0
set sqlfile=DeleteDatabase_SqlServer.sql

::执行SQL脚本
osql -S %dbhost% -U %dbuser% -P %dbpasswd% -d %dbName% -i %sqlpath%%sqlfile%

ECHO success!
PAUSE

@ECHO Done! 