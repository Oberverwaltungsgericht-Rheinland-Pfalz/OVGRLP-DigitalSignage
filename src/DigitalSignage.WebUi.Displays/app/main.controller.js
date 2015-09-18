(function () {
  'use strict';

  angular
    .module('app')
    .controller('MainController', MainController);

  MainController.$inject = ['$scope'];

  function MainController($scope) {
    var vm = this;

    vm.styles = [];

    activate();

    function activate() {
    }

    $scope.loadStyles = function (styles) {
      vm.styles = [];
      vm.styles.push('default');
      styles.split(',').forEach(function (style) {
        vm.styles.push(style.trim());
      });
    };
  };
})();