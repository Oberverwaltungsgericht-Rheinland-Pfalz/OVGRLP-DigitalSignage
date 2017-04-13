/*! DigitalSignage.WebUi2.RoomControl - v2.2.1-1712 - 13.04.2017 */
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