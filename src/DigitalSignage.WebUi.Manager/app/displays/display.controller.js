(function () {
  'use strict';

  angular
    .module('app.displays')
    .controller('DisplayController', DisplayController);

  DisplayController.$inject = ['$stateParams', '$http', 'Restangular'];

  function DisplayController($stateParams, $http, Restangular) {
    var vm = this;
    var baseDisplay = Restangular.one('settings/displays', $stateParams.id);

    vm.display = [];
    vm.loading = false;

    vm.poweron = poweron;
    vm.restart = restart;
    vm.shutdown = shutdown;
    vm.refresh = refresh;

    activate();

    function activate() {
      baseDisplay.get().then(function (display) {
        vm.display = display;
        update();
      });
    }

    function poweron() {
      if (vm.display && vm.display.controlUrl) {
        vm.display.customGET('start').then(function (data) {
        });
      };
    };

    function restart() {
      if (vm.display && vm.display.controlUrl) {
        $http.get(vm.display.controlUrl + '/api/restart');
      };
    };

    function shutdown() {
      if (vm.display && vm.display.controlUrl) {
        $http.get(vm.display.controlUrl + '/api/shutdown');
      };
    };

    function refresh() {
      update();
    };

    function update() {
      if (vm.display && vm.display.controlUrl) {
        vm.loading = true;

        vm.display.status = -1;
        vm.display.customGET('status').then(function (data) {
          vm.display.status = data.result;

          if (vm.display.status < 1) {
            vm.screenshot = 'assets/img/offline-display.png';
          } else {
            vm.screenshot = vm.display.controlUrl + '/api/screenshot?dt=' + new Date().getTime();
          };

          vm.loading = false;
        }, function (error) {
          vm.loading = false;
        });
      };
    };
  }

})();