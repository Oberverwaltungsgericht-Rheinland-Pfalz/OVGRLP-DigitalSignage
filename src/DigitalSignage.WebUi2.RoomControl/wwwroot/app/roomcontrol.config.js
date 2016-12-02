/*! DigitalSignage.WebUi2.RoomControl - v2.2.0-1647 - 29.11.2016 */
(function () {
  'use strict';

  angular
    .module('app')
    .constant('appConfig', {
      apiUrl: 'http://localhost:52208',
      termDetailsUrl: 'http://localhost:51445/#/user/terms/',
      showTermDetails: true,
      showHome: false,
      status: ['', 'Läuft', 'Abgeschlossen', 'Verschoben', 'Unterbrochen', 'Aufgehoben']
    });

})();