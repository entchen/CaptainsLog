setlocal

set KSP="d:\SteamLibrary\SteamApps\common\Kerbal Space Program.0.90.min"

if not exist %KSP%\GameData\CaptainsLog mkdir %KSP%\GameData\CaptainsLog

copy bin\debug\CaptainsLog.dll %KSP%\GameData\CaptainsLog\Plugins

endlocal
