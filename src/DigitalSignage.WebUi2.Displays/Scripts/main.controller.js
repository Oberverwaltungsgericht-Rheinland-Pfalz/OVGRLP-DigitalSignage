(function () {
  'use strict';

  angular
    .module('app')
    .controller('MainController', MainController);

  MainController.$inject = ['$scope', 'appConfig'];

  function MainController($scope, appConfig) {
    var vm = this;

    vm.styles = [];

    activate();

    function activate() {
    }

    $scope.loadStyles = function (styles) {
      vm.styles = [];
      vm.styles.push('css/default.css');
      styles.split(',').forEach(function (style) {
        vm.styles.push(appConfig.stylesUrl + '/' + style.trim() + '.css');
      });
    };
  };
})();