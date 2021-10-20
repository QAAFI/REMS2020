@echo off

rem Define variables
set "REMS=%USERPROFILE%\Documents\Releases\REMS2020"
set "CERT=user.pfx"

rem Clear the release directory
del %REMS% /s /q /f 1>nul

rem Build the solution.
dotnet publish -v:q -c Release -f net5.0-windows -r win-x64 --no-self-contained -o %REMS% "%~dp0Presentation\WindowsClient\WindowsClient.csproj"
if errorlevel 1 exit /b 1

rem Sign the .dll's
set TIMESTAMP="http://timestamp.comodoca.com/?td=sha256"
SignTool sign -q -as -fd sha256 -tr %TIMESTAMP% -td sha256 -f %CERT% %REMS%\Rems.Domain.dll
SignTool sign -q -as -fd sha256 -tr %TIMESTAMP% -td sha256 -f %CERT% %REMS%\Rems.Application.dll
SignTool sign -q -as -fd sha256 -tr %TIMESTAMP% -td sha256 -f %CERT% %REMS%\Rems.Infrastructure.dll

rem Find the assembly version
FOR /F "USEBACKQ" %%F IN (`powershell -NoLogo -NoProfile -Command ^(Get-Item "%REMS%\WindowsClient.exe"^).VersionInfo.FileVersion`) DO (SET VERSION=%%F)

rem Create the installer.
set "INSTALLER=REMS2020Setup"
iscc -Q -O%REMS% -F%INSTALLER% -DVERSION=%VERSION% -DOUTPUT=%REMS% rems.iss
if errorlevel 1 exit /b 1

rem Sign the installer
SignTool sign -q -as -fd sha256 -tr %TIMESTAMP% -td sha256 -f %CERT% %REMS%\%INSTALLER%.exe
if errorlevel 1 exit /b 1
SignTool verify -pa -v -d %REMS%\%INSTALLER%.exe
if errorlevel 1 exit /b 1

rem Create the xml version file
(
    echo ^<?xml version="1.0" encoding="UTF-8"?^>
    echo ^<item^>
    echo     ^<title^>Update available!^</title^>
    echo     ^<version^>%VERSION%^</version^>
    echo     ^<url^>https://github.com/MikeStower/REMS2020/releases/download/v%VERSION%/REMS2020Setup.exe^</url^>
    echo     ^<changelog^>https://mikestower.github.io/REMS2020/^</changelog^>
    echo     ^<mandatory^>false^</mandatory^>
    echo     ^<args^>/SP /SILENT /DIR=%%path%% /SUPPRESSMSGBOXES /CURRENTUSER /NOCANCEL /CLOSEAPPLICATION ^</args^>
    echo ^</item^>
) > version.xml

git commit -a -m "Published version %VERSION%"
git push

:SUCCESS
    echo Publish completed
GOTO END

:END
    exit