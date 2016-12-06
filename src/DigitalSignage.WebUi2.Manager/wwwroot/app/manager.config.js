/*! DigitalSignage.WebUi2.Manager - v2.2.0-1647 - 06.12.2016 */
(function () {
  var app = angular.module('app');

  angular
    .module('app')
    .constant('appConfig', {
      apiUrl: 'http://localhost:52208',
      showPoweronButton: false,
      status: ['', 'Läuft', 'Abgeschlossen', 'Verschoben', 'Unterbrochen', 'Aufgehoben']
    });
})();