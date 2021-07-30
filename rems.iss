;REMS2020 setup script

#include  "ISPPBuiltins.iss"

[Setup]
AppName=REMS2020
AppVerName=REMS2020 v{#VERSION}
ArchitecturesInstallIn64BitMode=x64
OutputBaseFilename=REMS2020Setup
VersionInfoVersion={#VERSION}
PrivilegesRequired=admin
PrivilegesRequiredOverridesAllowed=dialog
AppVersion={#VERSION}
AppID=REMS2020{#VERSION}
DefaultDirName={autopf}\REMS2020
DefaultGroupName=REMS2020
UninstallDisplayIcon={app}\bin\WindowsClient.exe
Compression=lzma/Max
ChangesAssociations=true
VersionInfoCompany=QAAFI
VersionInfoDescription=REMS Installer
VersionInfoProductName=REMS2020
VersionInfoProductVersion={#VERSION}
UsedUserAreasWarning=no

[Code]
var
    DownloadPage: TDownloadWizardPage;

function OnDownloadProgress(const Url, FileName: String; const Progress, ProgressMax: Int64): Boolean;
begin
    if Progress = ProgressMax then
        Log(Format('Successfully downloaded file to {tmp}: %s', [FileName]));
    Result:= True;
end;

procedure InitializeWizard;
begin
    DownloadPage := CreateDownloadPage(SetupMessage(msgWizardPreparing), SetupMessage(msgPreparingDesc), @OnDownloadProgress);
end;
   
// https://stackoverflow.com/questions/37825650/compare-version-strings-in-inno-setup
// 
// Returns 0, if the versions are equal.
// Returns -1, if the V1 is older than the V2.
// Returns 1, if the V1 is newer than the V2.
// 1.12 is considered newer than 1.1.
// 1.1 is considered the same as 1.1.0.
// Throws an exception, when a version is syntactically invalid (only digits and dots are allowed).
function CompareVersion(V1, V2: string): Integer;
var
  P, N1, N2: Integer;
begin
  Result := 0;
  while (Result = 0) and ((V1 <> '') or (V2 <> '')) do
  begin
    P := Pos('.', V1);
    if P > 0 then
    begin
      N1 := StrToInt(Copy(V1, 1, P - 1));
      Delete(V1, 1, P);
    end
      else
    if V1 <> '' then
    begin
      N1 := StrToInt(V1);
      V1 := '';
    end
      else
    begin
      N1 := 0;
    end;
    P := Pos('.', V2);
    if P > 0 then
    begin
      N2 := StrToInt(Copy(V2, 1, P - 1));
      Delete(V2, 1, P);
    end
      else
    if V2 <> '' then
    begin
      N2 := StrToInt(V2);
      V2 := '';
    end
      else
    begin
      N2 := 0;
    end;
    if N1 < N2 then Result := -1
      else
    if N1 > N2 then Result := 1;
  end;
end;

function HasDotNetCore(version: string) : boolean;
var
	runtimes: TArrayOfString;
	I: Integer;
	versionCompare: Integer;
	registryKey: string;
begin
	registryKey := 'SOFTWARE\WOW6432Node\dotnet\Setup\InstalledVersions\x64\sharedfx\Microsoft.NETCore.App';
	if(not IsWin64) then
	   registryKey :=  'SOFTWARE\dotnet\Setup\InstalledVersions\x86\sharedfx\Microsoft.NETCore.App';
	   
	Log('[.NET] Look for version ' + version);
	   
	if not RegGetValueNames(HKLM, registryKey, runtimes) then
	begin
	  Log('[.NET] Issue getting runtimes from registry');
	  Result := False;
	  Exit;
	end;
	
    for I := 0 to GetArrayLength(runtimes)-1 do
	begin
	  versionCompare := CompareVersion(runtimes[I], version);
	  Log(Format('[.NET] Compare: %s/%s = %d', [runtimes[I], version, versionCompare]));
	  if(not (versionCompare = -1)) then
	  begin
	    Log(Format('[.NET] Selecting %s', [runtimes[I]]));
	    Result := True;
	  	Exit;
	  end;
    end;
	Log('[.NET] No compatible found');
	Result := False;
end;

{ Read from the registry the path to the uninstaller }
{ @param version: the version to upgrade from }
function GetUninstallString(version: String): String;
var
  regKey: String;
  uninstaller: String;
begin
  regKey := 'Software\Microsoft\Windows\CurrentVersion\Uninstall\REMS2020' + version + '_is1';
  uninstaller := '';
  if not RegQueryStringValue(HKLM, regKey, 'UninstallString', uninstaller) then
    RegQueryStringValue(HKCU, regKey, 'UninstallString', uninstaller);
  Result := uninstaller;
end;
function UnInstallOldVersion(oldVersion : String): Integer;
var
  uninstaller: String;
  uninstallResult: Integer;
begin
{ Return Values: }
{ 1 - uninstall string is empty }
{ 2 - error executing the UnInstallString }
{ 3 - successfully executed the UnInstallString }
  { default return value }
  Result := 0;
  { get the uninstall string of the old app }
  uninstaller := GetUninstallString(oldVersion);
  if uninstaller <> '' then begin
    uninstaller := RemoveQuotes(uninstaller);
    if Exec(uninstaller, '/SILENT /NORESTART /SUPPRESSMSGBOXES','', SW_HIDE, ewWaitUntilTerminated, uninstallResult) then
      Result := 3
    else
      Result := 2;
  end else
    Result := 1;
end;

{ This function is called during the setup's initialisation. We check if the 'upgradefrom'
  command-line argument was provided, and if so, attempt to uninstall the previous version
  before installing this version. If the uninstallation fails, we ask the user if they wish
  to continue. The return value of this function is true iff installation should continue. }
function UpgradeIfNecessary(): Boolean;
var oldVersion : String;
var uninstallResult, continueInstall : Integer;
begin
  oldVersion := ExpandConstant('{param:upgradefrom|}')
  if (oldVersion = '') then
    Result := true
  else
  begin
    uninstallResult := UnInstallOldVersion(oldVersion);
    if (uninstallResult <> 3) then
    begin
      continueInstall := MsgBox('Uninstallation of previous version of REMS2020 was unsuccessful. Do you wish to continue installing the new version?', mbConfirmation, MB_YESNO);
      Result := continueInstall = IDYES;
    end
    else
      Result := True;
  end;
end;

// this is the main function that detects the required version
function IsRequiredDotNetDetected(): Boolean;  
begin
    result := HasDotNetCore('5.0.0');
end;

function InitializeSetup(): Boolean;
var
  answer: integer;
  ErrorCode: Integer;
begin
  result := true
  //check for the .net runtime. If it is not found then show a message.
  if not IsRequiredDotNetDetected() then 
  begin
      result := false;
      answer := MsgBox('The Microsoft .NET5.0 Runtime or above is required.' + #13#10 + #13#10 +
      'Click OK to go to the web site or Cancel to quit', mbInformation, MB_OKCANCEL);
      if (answer = MROK) then
      begin
        ShellExecAsOriginalUser('open', 'https://download.visualstudio.microsoft.com/download/pr/36a9dc4e-1745-4f17-8a9c-f547a12e3764/ae25e38f20a4854d5e015a88659a22f9/dotnet-runtime-5.0.0-win-x64.exe', '', '', SW_SHOWNORMAL, ewNoWait, ErrorCode);
        result := true
      end;
  end;
  if result = true then
    result := UpgradeIfNecessary();
end;

[InstallDelete]
Name: {localappdata}\VirtualStore\REMS2020\*.*; Type: filesandordirs
Name: {localappdata}\VirtualStore\REMS2020; Type: dirifempty

[Files]
Source: {#OUTPUT}\ApplicationFiles\*; DestDir: {app}\bin\ApplicationFiles; Flags: recursesubdirs
Source: {#OUTPUT}\*; DestDir: {app}\bin; Flags: ignoreversion;

[Tasks]
Name: desktopicon; Description: Create a &desktop icon; GroupDescription: Additional icons:; Flags: unchecked

[Run]
Filename: {app}\bin\WindowsClient.exe; Description: Launch REMS2020; Flags: postinstall nowait skipifsilent