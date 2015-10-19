(function () {
  'use strict';

  angular
    .module('app.terms')
    .directive('dsParteien', dsParteien);

  function dsParteien() {
    var directive = {
      restrict: 'E',
      templateUrl: 'app/terms/parteien.directive.html',
      scope: {
        title: '@',
        parteien: '='
      },
      controller: ParteienController,
      controllerAs: 'vm',
      bindToController: true
    };
    
    return directive;

    function ParteienController($scope) {
      var vm = this;

      console.log(vm.parteien);
      console.log($scope.parteien);

      //vm.title = $scope.title;
      //vm.parteien = $scope.parteien;
    }
  }
})();