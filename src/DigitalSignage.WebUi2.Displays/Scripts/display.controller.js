﻿(function () {
  'use strict';

  angular
    .module('app')
    .controller('DisplayController', DisplayController);

  DisplayController.$inject = ['$scope', '$sce', '$stateParams', '$interval', 'Restangular', 'appConfig'];

  function DisplayController($scope, $sce, $stateParams, $interval, Restangular, appConfig) {
    var vm = this;
    var DisplaysSrv = Restangular.service('settings/displays');

    vm.display = [];
    vm.dateTime = new Date();
    vm.notes = null;

    activate();

    function activate() {
      $interval(function () {
        vm.dateTime = new Date();
      }, 1000 * 60);

      loadDisplay();

      $interval(function () {
        loadDisplay();
      }, 1000 * 60);
    }
    
    function loadDisplay() {
      DisplaysSrv.one($stateParams.name).get().then(function (display) {
        vm.display = display;
        vm.layout = appConfig.templatesUrl + '/' + display.template + '/main.html';
        vm.notes = $sce.trustAsHtml(vm.display.notes);

        $scope.$parent.loadStyles(vm.display.styles);
      });
    }
  };
})();