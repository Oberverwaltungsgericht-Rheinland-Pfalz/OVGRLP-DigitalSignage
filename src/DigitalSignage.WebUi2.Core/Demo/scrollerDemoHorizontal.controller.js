(function () {
  'use strict';

  angular
    .module('ds-demo')
    .controller('scrollerDemoHorizontal', scrollerDemoHorizontal);

  scrollerDemoHorizontal.$inject = ['$location'];

  function scrollerDemoHorizontal($location) {
    /* jshint validthis:true */
    var vm = this;
    vm.title = 'scrollerDemo (Horizontal)';

    vm.termine = [
      'Termin 1',
      'Termin 2',
      'Termin 3'
    ];

    vm.changeOne = function () {
      vm.termine[1] = 'Termin XYZ';
    };

    vm.addOne = function () {
      vm.termine.push('Termin ' + Math.random());
    };

    vm.defineTwo = function () {
      vm.termine = [
        'Termin 1',
        'Termine 2',
      ];
    };

    vm.defineOne = function () {
      vm.termine = [
        'Termin 1'
      ];
    };

    activate();

    function activate() { }
  }
})();
