(function () {
  'use strict';

  angular
    .module('app.displays')
    .controller('DisplayController', DisplayController);

  DisplayController.$inject = ['$stateParams', '$http', 'settingsDataService', 'appConfig'];

  function DisplayController($stateParams, $http, settingsDataService, appConfig) {
    var vm = this;

    vm.display = [];
    vm.loading = false;

    vm.poweron = poweron;
    vm.restart = restart;
    vm.shutdown = shutdown;
    vm.refresh = refresh;
    vm.appConfig = appConfig;

    activate();

    function activate() {
      settingsDataService.getDisplay($stateParams.id)
        .then(function (data) {
          vm.display = data.results[0];
          vm.display.update();
        });
    };

    function poweron() {
      if (vm.display) {
        vm.display.poweron()
          .then(function (data) {
            console.log(data);
          }, function (err) {
            console.log(err);
          });
      }
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