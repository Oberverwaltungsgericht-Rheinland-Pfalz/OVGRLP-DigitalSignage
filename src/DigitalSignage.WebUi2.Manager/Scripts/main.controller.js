(function () {
  'use strict';

  angular
    .module('app')
    .controller('MainController', MainController);

  MainController.$inject = ['$stateParams', 'appConfig'];

  function MainController($stateParams, appConfig) {
    var vm = this;

    activate();

    function activate() {
    };
  }
})();