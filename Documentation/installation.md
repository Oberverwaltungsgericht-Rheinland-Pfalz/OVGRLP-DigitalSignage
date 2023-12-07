# Allgemeines

Diese Anleitung bezieht sich auf die Installation auf einem Windows-Server System in Verbindung mit einem Hosting auf einem IIS.

# Installation WebApi

Bei dieser Anwendung handelt es sich um eine REST WebApi, das Backend bzw. ein Endpunkt für die verschiedenen DigitalSignage Web-Projekte.

Die WebApi erlaubt Abfragen zu den Stammdaten, persistieren von Stammdaten und noch mehr.

## Allgemeine Vorbereitungen

Dem Windows-Server sollten einige "Rollen und Features" hinzugefügt werden
 *	Unter Serverrollen
    *	Webserver (IIS) -> Webserver -> Anwendungsentwicklung 
        *	.NET-Erweiterbarkeit 4.x
        * ASP.NET 4.x


## Vorbereitung der Datenbank

Für die Datenbank ist eine SQL Server Express Instanz ausreichend. 
Im ersten Schritt müssen wir eine neue Datenbank vorbereiten. Dazu 
  *	legen wir im SQL Server Management Studio eine Neue Datenbank "DigitalSignage" an
  *	In einer neuen Abfrage führen wir das letzte "full" SQL-Anlageskript aus dem Repository im Unterordner "src\DigitalSignage.Data\Migrations" (bspw. 2-9-0-1904_Full.sql) aus.
  * Anschließend ist die Datenbank wie folgt angelegt: <br>
![Datenbank](inst_db.png)
  * Später (nach Anlage des Anwendungspools - siehe Kapitel "IIS -> Hosting") sollte abschließend der entsprechende Anwendungspool für die Datenbank berechtigt werden.
![Berechtigung Apppool\Digitalsignage](inst_BerApppool.png)

## IIS

### Vorbereitungen
  * bevor wir die web.config konfigurieren, müssen wir IIS dahingehend konfigurieren, dass die Authentifizierungs-Einstellungen über die web.config vorgenommen werden dürfen. <br> ![Windows Authentifizierung](inst_WindowsAuth.png)
  * weiterhin sollten die Handlerzuordnungen auch auf Lesen/Schreiben stehen <br> ![Handlerzuordnungen](inst_handlerzuord.png)
  * 
### Hosting

Um die WebApi im IIS zu hosten sind exemplarisch folgende Schritte Not
wendig
  * Anlage eines neuen Anwendungspool´s <br> ![Anwendungspool](inst_AnlAnwendungsp.png)
  * Anschließend können wir eine neue Website "Digitalsignage" hinzufügen, dabei kann auch ein Hostname vergeben werden <br> ![Website](inst_WebsiteHinz.png)
  * Alternativ zum Hostnamen können wir diese auch über einen bestimmten Port freigeben, dieser muss aber dann in der Firewall freigegeben werden. Wir nehmen hier einmal exemplarisch den Port 8082.
  * Direkt in dem Pfad c:\wwwroot\Digitalsignage erzeugen wir einen Unterordner "WebApi", wo wir die Programmdateien inkl. web.config einfügen. Im IIS müssen wir diesen Unterordner als Anwendung konvertieren. <br> ![Anwendung konvertieren](inst_AnwKonv.png)


## Konfiguration der Web.config

### Windows Authentificaton
In der web.config muss für die Windows-Authentication im Knoten <system.webServer> folgende Zeilen eingefügt werden

```xml 
<security>
  <authentication>
    <anonymousAuthentication enabled="false" userName="" />
    <windowsAuthentication enabled="true"></windowsAuthentication>
  </authentication>
</security>
```

### DB Connection hinterlegen
In der web.config der WebApi muss im Knoten <connectionStrings> die Datenbank-Instanz unter " data source=" und die Datenbank selbst unter "initial catalog=" eintragen.

```xml
<connectionStrings>
  <add name="DigitalSignageDbConnectionString" connectionString="Server=[server]\[instance]; Database=[dbname]; Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
```
### Verhalten der Anwendung
Die Anwendung selbst kann auch über die web.config konfiguriert werden, im Knoten <DigitalSignage.WebApi.Properties.Settings> können folgende Werte dazu angepasst werden.

| Setting| Beschreibung|
|---|---|
| setting 1 | descr <br> descr <br> ... |

TODO

## Test

Um die Anwendung zu Testen können einfach mal die Displays abgefragt werden:

TODO
http://[Hostname]:[port]/webapi/settings/displays

# Installation Webanwendung Displays 
TODO
# Installation Webanwendung RoomControl 
TODO
# Installation Webanwendung DS-Manager 
TODO