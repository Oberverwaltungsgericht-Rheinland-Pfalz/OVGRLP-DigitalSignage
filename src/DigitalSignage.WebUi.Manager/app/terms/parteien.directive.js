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
        items: '=',
        title: '@',
        entityName: '@'
      },
      controller: ParteienController,
      controllerAs: 'vm',
      bindToController: true
    };
    
    return directive;

    ParteienController.$inject = ['dataService'];

    function ParteienController(dataService) {
      var vm = this;

      vm.addNewItem = addNewItem;

      function addNewItem() {
        vm.items.push(dataService.createNewEntity(vm.entityName));
      }
    }
  }
})();