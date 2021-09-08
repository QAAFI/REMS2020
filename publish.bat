@echo off

if %1.==. GOTO WARNING1

set VERSION=%1

rem Define variables
set "REMS=%USERPROFILE%\Documents\Releases\REMS2020"
set "CERT=user.pfx"

rem Clear the release directory
del %REMS% /s /q /f 1>nul

rem Build the solution.
dotnet publish -v:q -c Release -f net5.0-windows -r win-x64 --no-self-contained -o %REMS% "%~dp0Presentation\WindowsClient\WindowsClient.csproj"
if errorlevel 1 exit /b 1

set TIMESTAMP="http://timestamp.comodoca.com/?td=sha256"
rem Sign the .dll's
SignTool sign -q -as -fd sha256 -tr %TIMESTAMP% -td sha256 -f %CERT% %REMS%\Rems.Domain.dll
SignTool sign -q -as -fd sha256 -tr %TIMESTAMP% -td sha256 -f %CERT% %REMS%\Rems.Application.dll
SignTool sign -q -as -fd sha256 -tr %TIMESTAMP% -td sha256 -f %CERT% %REMS%\Rems.Infrastructure.dll

rem Create the installer.
set "INSTALLER=REMS2020Setup"
iscc -Q -O%REMS% -F%INSTALLER% -DVERSION=%VERSION% -DOUTPUT=%REMS% rems.iss
if errorlevel 1 exit /b 1

rem Sign the installer
SignTool sign -q -as -fd sha256 -tr %TIMESTAMP% -td sha256 -f %CERT% %REMS%\%INSTALLER%.exe
if errorlevel 1 exit /b 1
SignTool verify -pa -v -d %REMS%\%INSTALLER%.exe
if errorlevel 1 exit /b 1

:SUCCESS
    echo Publish completed
GOTO END

:WARNING1
    echo Missing version argument
GOTO END

:END
    exit