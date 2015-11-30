(function () {
  'use strict';

  angular
    .module('ds-demo')
    .controller('scrollerDemo', scrollerDemo);

  scrollerDemo.$inject = ['$location'];

  function scrollerDemo($location) {
    /* jshint validthis:true */
    var vm = this;
    vm.title = 'scrollerDemo';

    vm.zeilen = [
      'Zeile 1',
      'Zeile 2',
      'Zeile 3',
      'Zeile 4',
      'Zeile 5',
      'Zeile 6',
      'Zeile 7',
      'Zeile 8',
      'Zeile 9',
      'Zeile 10'
    ];

    vm.changeLine = function () {
      vm.zeilen[4] = 'Zeile XY';
    };

    vm.addLine = function () {
      vm.zeilen.push('Zeile ' + Math.random());
    };

    vm.redefineLines = function () {
      vm.zeilen = [
        'Zeile 1',
        'Zeile 2',
        'Zeile 3',
        'Zeile 4',
        'Zeile 5',
        'Zeile 6'
      ];
    };

    activate();

    function activate() { }
  }
})();
