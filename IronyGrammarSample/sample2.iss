#include "filename.txt"

[Setup]
AppName=My Program

[Files]
Source: "MYPROG.EXE"; DestDir: "{app}"
Source: "Files\*"

; This is a comment. I could put reminders to myself here...
Source: "MYPROG.EXE"; DestDir: "{app}"; ExternalSize: 1048576; Flags: external
Source: "MYPROG.EXE"; DestDir: "{app}"; Attribs: hidden system; Permissions: users-modify
Source: "MYPROG2.EXE"; DestDir: "{app}"; StrongAssemblyName: "MyAssemblyName, Version=1.0.0.0, Culture=neutral, PublicKeyToken=abcdef123456, ProcessorArchitecture=MSIL"
Source: "OZHANDIN.TTF"; DestDir: "{fonts}"; FontInstall: "Oz Handicraft BT"; Flags: onlyifdoesntexist uninsneveruninstall
Source: "*"; Excludes: "*.~*"
Source: "*"; Excludes: "*.~*,\Temp\*"; Flags: recursesubdirs
Source: "OZHANDIN.TTF"; DestDir: "{fonts}"; FontInstall: "Oz Handicraft BT"; Flags: onlyifdoesntexist uninsneveruninstall
