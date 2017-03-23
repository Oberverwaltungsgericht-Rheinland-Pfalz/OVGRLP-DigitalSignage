(function () {
  'use strict';

  angular
    .module('app')
    .controller('DisplayController', DisplayController);

  DisplayController.$inject = ['$http', '$stateParams', 'Restangular']

  function DisplayController($http, $stateParams, Restangular) {
    var vm = this;
    var DisplaySrv = Restangular.service('settings/displays');

    vm.display = [];
    vm.loading = false;

    vm.poweron = poweron;
    vm.restart = restart;
    vm.shutdown = shutdown;
    vm.refresh = refresh;

    activate();

    function activate() {
      loadDisplay();
    }

    function loadDisplay() {
      vm.loading = true;

      DisplaySrv.one($stateParams.name).get().then(function (display) {
        vm.display = display;
        vm.display.status = -1;

        vm.display.customGET('status').then(function (data) {
          vm.display.status = data.result;

          if (vm.display.status < 1) {
            vm.screenshot = '../assets/img/offline-display.png';
          } else {
            vm.screenshot = vm.display.controlUrl + '/api/screenshot?dt=' + new Date().getTime();
          };
          vm.loading = false;
        }, function (error) {
          vm.loading = false;
        });
      }, function (error) {
        vm.loading = false;
      });
    }

    function poweron() {
      if (vm.display && vm.display.controlUrl) {
        vm.display.customGET('start').then(function (data) {
        });
      }
    }

    function restart() {
      if (vm.display && vm.display.controlUrl) {
        $http.get(vm.display.controlUrl + '/api/restart');
      };
    }

    function shutdown() {
      if (vm.display && vm.display.controlUrl) {
        $http.get(vm.display.controlUrl + '/api/shutdown');
      };
    }

    function refresh() {
      loadDisplay();
    }
  }
})();