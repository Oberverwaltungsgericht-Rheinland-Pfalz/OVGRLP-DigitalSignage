/*! DigitalSignage.WebUi2.RoomControl - v2.1.0 - 25.11.2015 */
(function () {
  'use strict';

  angular
    .module('app')
    .constant('appConfig', {
      apiUrl: 'http://localhost:52208',
      termDetailsUrl: 'http://localhost:51445/#/terms/',
      showTermDetails: true,
      showHome: false,
      status: ['', 'Läuft', 'Abgeschlossen', 'Verschoben', 'Unterbrochen', 'Aufgehoben']
    });

})();