(function () {
  'use strict';

  angular
    .module('app')
    .controller('MainController', MainController);

  MainController.$inject = ['$stateParams', 'appConfig'];

  function MainController($stateParams, appConfig) {
    var vm = this;

    vm.showMenu = false;

    activate();

    function activate() {
      vm.showMenu = ($stateParams.mode === 'support');
    };
  }
})();