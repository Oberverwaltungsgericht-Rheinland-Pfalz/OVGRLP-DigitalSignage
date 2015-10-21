(function () {
  'use strict';

  angular
    .module('app.displays')
    .controller('DisplayController', DisplayController);

  DisplayController.$inject = ['$stateParams', '$http', 'settingsDataService'];

  function DisplayController($stateParams, $http, settingsDataService) {
    var vm = this;
    //var baseDisplay = Restangular.one('settings/displays', $stateParams.id);

    vm.display = [];
    vm.loading = false;

    vm.poweron = poweron;
    vm.restart = restart;
    vm.shutdown = shutdown;
    vm.refresh = refresh;

    activate();

    function activate() {
      settingsDataService.getDisplay($stateParams.id)
        .then(function (data) {
          vm.display = data.results[0];
          vm.display.update();
        });
    };

    function poweron() {
      /*
      if (vm.display && vm.display.controlUrl) {
        vm.display.customGET('start').then(function (data) {
        });
      };
      */
    };

    function restart() {
      if (vm.display) {
        vm.display.restart();
      }
    };

    function shutdown() {
      if (vm.display) {
        vm.display.shutdown();
      }
    };

    function refresh() {
      if (vm.display) {
        vm.display.update();
      };
    };
  }

})();