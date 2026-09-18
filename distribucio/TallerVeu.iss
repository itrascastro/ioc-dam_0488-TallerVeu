[Setup]
AppName=TallerVeu
AppVersion=1.0
DefaultDirName={autopf}\TallerVeu
DefaultGroupName=TallerVeu
OutputBaseFilename=Instal·lador_TallerVeu
Compression=lzma2
SolidCompression=yes

[Files]
Source: "..\TallerVeu\bin\Release\net8.0-windows\win-x64\publish\*"; DestDir: "{app}"; Flags: recursesubdirs

[Icons]
Name: "{group}\TallerVeu"; Filename: "{app}\TallerVeu.exe"
Name: "{group}\Desinstal·la TallerVeu"; Filename: "{uninstallexe}"
Name: "{autodesktop}\TallerVeu"; Filename: "{app}\TallerVeu.exe"; Tasks: icona_escriptori

[Tasks]
Name: "icona_escriptori"; Description: "Crea una icona a l'escriptori"; GroupDescription: "Icones addicionals:"
