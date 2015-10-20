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

    RichterController.$inject = ['dataService'];

    function RichterController(dataService) {
      var vm = this;

      vm.addNewItem = addNewItem;

      function addNewItem() {
        vm.items.push(dataService.createNewEntity(vm.entityName));
      }
    }
  }
})();