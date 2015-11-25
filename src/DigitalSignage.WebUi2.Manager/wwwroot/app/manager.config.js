/*! DigitalSignage.WebUi2.Manager - v2.1.0 - 25.11.2015 */
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