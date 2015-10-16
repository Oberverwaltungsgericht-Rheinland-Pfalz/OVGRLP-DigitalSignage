(function () {
  'use strict';

  angular
    .module('app.terms')
    .controller('TermsController', TermsController);

  TermsController.$inject = ['$stateParams', '$filter', 'Restangular'];

  function TermsController($stateParams, $filter, Restangular) {
    var vm = this;

    var baseTerms = Restangular.all('daten/verfahren');
    var defaultSort = [
      { field: 'uhrzeitAktuell', sort: 'asc' }
    ];

    var columnDefs = [
      { headerName: 'Plan', headerGroup: 'Uhrzeit', width: 80, suppressSizeToFit: true, field: 'uhrzeitPlan' },
      { headerName: 'Aktuell', headerGroup: 'Uhrzeit', width: 80, suppressSizeToFit: true, field: 'uhrzeitAktuell' },
      { headerName: 'Aktenzeichen', width: 150, suppressSizeToFit: true, field: 'az' },
      { headerName: 'Status', width: 150, suppressSizeToFit: true, field: 'status' },
      { headerName: 'Aktiv', headerGroup: 'Parteien', suppressSorting: true, suppressMenu: true, template: '<span ng-repeat="partei in data.parteienAktiv">{{partei}}</span>' },
      { headerName: 'Passiv', headerGroup: 'Parteien', suppressSorting: true, suppressMenu: true, template: '<span ng-repeat="partei in data.parteienPassiv">{{partei}}</span>' },
      { headerName: 'Datum', width: 100, suppressSizeToFit: true, field: 'datum' },
      { headerName: '', width: 100, suppressSizeToFit: true, suppressSorting: true, suppressMenu: true, template: '<a ui-sref="term({id:data.id})">Bearbeiten</a>' }
    ];

    vm.gridOptions = {
      angularCompileRows: true,
      enableSorting: true,
      enableFilter: true,
      columnDefs: columnDefs,
      rowData: null,
      groupHeaders: true,
      groupKeys: ['sitzungssaal'],
      groupUseEntireRow: true,
      ready: function (api) {
        api.sizeColumnsToFit();
        api.setSortModel(defaultSort);
      }
    };

    activate();

    function activate() {
      baseTerms.getList().then(function (data) {
        vm.gridOptions.rowData = data;
        vm.gridOptions.api.onNewRows();
      });
    };

  };

})();