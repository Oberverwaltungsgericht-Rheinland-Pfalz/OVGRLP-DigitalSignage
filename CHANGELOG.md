<a name="2.2.1-1712"></a>
### 2.2.1-1712 (23.03.2017)

#### Breaking Changes

* __WebApi / Ui: Name statt Id__ - Die WebApi und die Ui-Projekte Displays und RoomControl wurden so
umgebaut, dass nun der *Name* des Displays für die Navigation genutzt wird und nicht mehr die *Id*.
Hierdurch ändern sich die Urls zu den Displays. Der Manager sollte von den Änderungen nicht 
betroffen sein. (#97)

<a name="2.2.0-1649"></a>
### 2.2.0-1649 (07.12.2016)

#### Erweiterungen

* __API/Daten: SitzungssaalNr__ - In der Datenstruktur wurde nun die Eigenschaft *SitzunggssaalNr*
hinzugef&uuml;gt. (#63)

#### Fehlerkorrekturen

* __InfoScreen: Filter Status__ - Es kann nun wieder nach dem Status gefiltert werden. (#71)

* __RoomControl: Men&uuml;leiste im IE__ - Die Men&uuml;leiste wird nun auch im Internet 
Explorer korrekt angezeigt. (#72)

* __Manager: Verfahren löschen__ - Verfahren können nun wieder gelöscht werden. (#74)

<a name="2.1.0"></a>
### 2.1.0 (25.11.2015)

#### &Auml;nderungen

* __API/Daten Dummy-S&auml;le:__ In der Tabelle _Displays_ in der Datenbank kann nun in der Spalte 
_Dummy_ definiert werden, ob ein Display in den Auswahllisten angezeigt wird oder nicht.
(a222a2c1, closes #60)

* __Manager Anzeigen&uuml;bersicht:__ Die &Uuml;bersicht mit den Anzeigen geht nun &uuml;ber die gesamte Seite.
Die Details werden nun ebenfalls auf der ganzen Seite angezeigt und nicht mehr nur im unteren Bereich.
(0b6d084d, closes #52)

* __Manager Anschalten optional:__ Die Schaltfl&auml;che zum Anschalten von Displays kann nun &uuml;ber die
Eigenschaft _showPoweronButton_ in der _roomcontrol.config_ eingeblendet und ausgeblendet werden. 
(7b685fc5, closes #63)

* __Manager Gruppe aufklappen:__ In der &Uuml;bersicht mit den Terminen wird nur noch die erste 
Ebene (die Gerichte) in der Tabelle automatisch aufgeklappt. (a74s42fc, closes #64)

* __Manager Aufruf &uuml;ber Az:__ Die Sitzungsdetails werden nun &uuml;ber einen Klick auf das Aktenzeichen
aufgerufen. Die Schaltfl&auml;che _Bearbeiten_ entf&auml;llt. (a74d42fc, closes #66)

* __Manager Status DropDown:__ Im Manager kann nun auch der Status in einem DropDown-Feld ausgew&auml;hlt 
werden. Die Liste mit m&ouml;glichen Eintr&auml;gen wird in der _manager.config_ definiert.
(92a0ffaa, closes #67)

* __Manager Auswahl &ouml;ffentlich:__ Im Manager k&ouml;nnen nun in einer Auswahlliste die Werte ja, nein f&uuml;r 
&ouml;ffentlich ausgew&auml;hlt werden. Ein Schalter wie in RoomControl konnte nicht in das bestehende 
Formular aufgenommen werden. (257b27c4, closes #69)

* __RoomControl Home optional:__ Der Home Button kann nun &uuml;ber die Eigenschaft _showHome_ in der 
_roomcontrol.config_ eingeblendet und ausgeblendet werden. (d8566aec, closes #59)

* __RoomControl Manager-Link:__ Es ist nun m&ouml;glich von den Sitzungen im RoomControl zu einer Sitzung
im Manager zu navigieren. Hierzu muss nur auf das Aktenzeichen der Sitzung geklicken werden. Der Manager 
geht in einem neuen Fenster bzw. Tab auf. In der _roomcontrol.config_ muss hierzu die Eigenschaft 
_termDetailsUrl_ auf die gew&uuml;nschte Url zum Manager gesetz werden. &Uuml;ber die Eigenschaft _showTermDetails_ 
kann eingestellt werden, ob die Verlinkung zum Manager aktiviert wird oder nicht. (6a159894, closes #61)

* __RoomControl Status konfigurierbar:__ Die Liste mit m&ouml;glichen Eintr&auml;gen f&uuml;r den Status wird nun in der 
_roomcontrol.config_ definiert. (a63a209f, closes #72)

#### Fehlerkorrekturen

* __Manager Pfad zu Bildern falsch:__ Pfad zu den Bildern und Icons wurde korrigiert.
(a74d42fc, closes #62)

* __Manager Sitzungen bearbeiten:__ Die Direktiven f&uuml;r die Bearbeitung von Listeneintr&auml;gen wurden 
korrigiert. Die Bearbeitung ist nun wieder m&ouml;glich. (ff07fe98, closes #65)