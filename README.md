# Dev

Die SPA kann fürs debugging gestart werden durch 
1. Start der Digital.Signage.WebApi über Visual Studio
2. C:\dev\repos\digitalsignage\src\ds-suite> npm run serve:displays
3. C:\dev\repos\digitalsignage\src\ds-suite> npm run serve:dsmanager

sowie eintragen der richtigen ports in den configs
1. apps > displays > src > assets > config.json > webApiUrl : "webApiUrl": "http://localhost:52208/",
2. apps > dsmanager > src > assets > config.json > webApiUrl : "webApiUrl": "http://localhost:52208/",


NuGet-Quelle Hinzufügen
Unter Optionen > NuGet-Paketmanager > Paketquellen folgende Quelle Hinzufügen
\\Ovgvg\it\OVGRLP\Dev\NugetFeed\
