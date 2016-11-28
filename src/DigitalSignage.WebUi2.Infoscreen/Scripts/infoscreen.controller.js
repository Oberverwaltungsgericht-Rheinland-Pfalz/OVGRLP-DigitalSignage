(function () {
  'use strict';

  angular
    .module('ds-infoscreen')
    .controller('InfoscreenController', InfoscreenController);

  InfoscreenController.$inject = ['termsDataService'];

  function InfoscreenController(termsDataService) {
    var vm = this;

    vm.title = 'Digital Signage - Infoscreen';
    vm.terms = [];
    vm.filters = [];
    vm.filters.gericht = [];

    vm.filters.status = [
      {
        active: false,
        title: 'Läuft',
        expression: { Status: 'Läuft' }
      }, {
        active: false,
        title: 'Abgeschlossen',
        expression: { Status: 'Abgeschlossen' }
      }, {
        active: false,
        title: 'Verschoben',
        expression: { Status: 'Verschoben' }
      }, {
        active: false,
        title: 'Aufgeboben',
        expression: { Status: 'Aufgehoben' }
      }
    ];

    vm.updateData = updateData;

    activate();

    function activate() {
      updateData();
    };

    function updateData() {
      termsDataService.getVerfahrenList().then(function (data) {
        vm.terms = data.results;
        loadGerichteFilters();
      });
    };

    function loadGerichteFilters() {
      var gerichte = [];

      vm.terms.forEach(function (term) {
        gerichte.push(term.Gericht);
      });

      _.uniq(gerichte).forEach(function (data) {
        vm.filters.gericht.push({
          active: false,
          title: data,
          expression: { Gericht: data }
        });
      });
    };
  }
})();