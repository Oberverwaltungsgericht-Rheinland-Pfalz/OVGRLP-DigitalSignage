(function () {
  'use strict';

  angular
    .module('app')
    .controller('RoomController', RoomController);

  RoomController.$inject = ['$stateParams', '$mdSidenav', 'Restangular', 'appConfig'];

  function RoomController($stateParams, $mdSidenav, Restangular, appConfig) {
    var vm = this;
    var DisplaySrv = Restangular.service('settings/displays');

    vm.display = [];
    vm.openNavbar = openNavbar;
    vm.appConfig = appConfig;

    activate();

    function activate() {
      loadDisplay();
    }

    function loadDisplay() {
      DisplaySrv.one($stateParams.id).get().then(function (display) {
        vm.display = display;
      })
    }

    function openNavbar() {
      $mdSidenav('navbar').toggle();
    }

    //$scope.baseDisplay = Restangular.one('settings/displays', $stateParams.id);
    //$scope.baseDisplay.get().then(function (data) {
    //  $scope.display = data;
    //});

    //$scope.openNavbar = function () {
    //  $mdSidenav('navbar').toggle();
    //};
  }
})();