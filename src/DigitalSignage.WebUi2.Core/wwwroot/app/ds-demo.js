/*! DigitalSignage.WebUi2.Core - v1.0.0 - 30.11.2015 */
(function () {
  'use strict';

  angular
    .module('ds-demo', [
      'ds-core'
  ]);
})();
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
