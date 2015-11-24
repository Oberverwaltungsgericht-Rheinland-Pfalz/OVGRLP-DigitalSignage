(function () {
  'use strict';

  angular
    .module('app.terms')
    .directive('dsProzbev', dsProzbev);

  function dsProzbev() {
    var directive = {
      restrict: 'E',
      templateUrl: 'app/terms/prozbev.directive.html',
      scope: {
        items: '=',
        title: '@',
        entityName: '@'
      },
      controller: ['termsDataService', ProzbevController],
      controllerAs: 'vm',
      bindToController: true
    };
    
    return directive;

    function ProzbevController(termsDataService) {
      var vm = this;

      vm.addNewItem = addNewItem;
      vm.deleteItem = deleteItem;

      function addNewItem() {
        vm.items.push(termsDataService.createNewEntity(vm.entityName));
      }

      function deleteItem(item) {
        var i = vm.items.indexOf(item);
        item.entityAspect.setDeleted();
        vm.items.splice(i, 1);
      }
    }
  }
})();