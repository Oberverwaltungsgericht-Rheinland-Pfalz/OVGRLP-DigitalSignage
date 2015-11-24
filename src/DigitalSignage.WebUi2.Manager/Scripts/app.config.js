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