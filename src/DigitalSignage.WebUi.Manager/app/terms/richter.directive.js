(function () {
  'use strict';

  angular
    .module('app.terms')
    .directive('dsRichter', dsRichter);

  function dsRichter() {
    var directive = {
      restrict: 'E',
      templateUrl: 'app/terms/richter.directive.html',
      scope: {
        items: '=',
        title: '@',
        entityName: '@'
      },
      controller: RichterController,
      controllerAs: 'vm',
      bindToController: true
    };
    
    return directive;

    RichterController.$inject = ['termsDataService'];

    function RichterController(termsDataService) {
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