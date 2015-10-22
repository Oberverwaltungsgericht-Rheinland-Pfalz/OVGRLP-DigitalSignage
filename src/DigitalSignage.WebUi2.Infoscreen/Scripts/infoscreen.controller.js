(function () {
  'use strict';

  angular
    .module('ds-infoscreen')
    .controller('InfoscreenController', InfoscreenController);

  InfoscreenController.$inject = ['Restangular'];

  function InfoscreenController(Restangular) {
    var vm = this;

    var verfahrenService = Restangular.service('daten/verfahren');

    vm.title = 'Digital Signage - Infoscreen';
    vm.terms = [];
    vm.filters = [];
    vm.filters.gericht = [
      {
        active: false,
        title: 'Oberverwaltungsgericht Rheinland-Pfalz',
        expression: { gericht: 'Oberverwaltungsgericht Rheinland-Pfalz' }
      }, {
        active: false,
        title: 'Verwaltungsgericht Koblenz',
        expression: { gericht: 'Verwaltungsgericht Koblenz' }
      }, {
        active: false,
        title: 'Sozialgericht Koblenz',
        expression: { gericht: 'Sozialgericht Koblenz' }
      }, {
        active: false,
        title: 'Arbeitsgericht Koblenz',
        expression: { gericht: 'Arbeitsgericht Koblenz' }
      }
    ];
    vm.filters.status = [
      {
        active: false,
        title: 'Läuft',
        expression: { status: 'Läuft' }
      }, {
        active: false,
        title: 'Abgeschlossen',
        expression: { status: 'Abgeschlossen' }
      }, {
        active: false,
        title: 'Verschoben',
        expression: { status: 'Verschoben' }
      }, {
        active: false,
        title: 'Aufgeboben',
        expression: { status: 'Aufgehoben' }
      }
    ];

    vm.updateData = updateData;

    activate();

    function activate() {
      updateData();
    }

    function updateData() {
      verfahrenService.getList().then(function (data) {
        vm.terms = data;
      });
    };
  }
})();