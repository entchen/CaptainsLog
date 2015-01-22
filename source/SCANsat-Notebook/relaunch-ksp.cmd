setlocal

set KSP="d:\SteamLibrary\SteamApps\common\Kerbal Space Program.0.90.min"

if not exist %KSP%\GameData\SCANsat-Notebook mkdir %KSP%\GameData\SCANsat-Notebook

copy bin\debug\SCANsat-Notebook.dll %KSP%\GameData\SCANsat-Notebook\Plugins

endlocal
