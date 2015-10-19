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
        parteien: '=',
        title: '@',
        entityName: '@'
      },
      controller: ParteienController,
      controllerAs: 'vm',
      bindToController: true
    };
    
    return directive;

    ParteienController.$inject = ['$scope', 'dataService'];

    function ParteienController($scope, dataService) {
      var vm = this;

      vm.addNewItem = addNewItem;

      function addNewItem() {
        vm.parteien.push(dataService.createNewEntity(vm.entityName));
      }
    }
  }
})();