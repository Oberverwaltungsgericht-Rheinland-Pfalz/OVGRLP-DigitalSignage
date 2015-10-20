(function () {
  'use strict';

  angular
    .module('app.terms')
    .controller('TermsController', TermsController);

  TermsController.$inject = ['$stateParams', '$filter', 'dataService'];

  function TermsController($stateParams, $filter, dataService) {
    var vm = this;

    var defaultSort = [
      { field: 'uhrzeitAktuell', sort: 'asc' }
    ];

    var columnDefs = [
      { headerName: 'Plan', headerGroup: 'Uhrzeit', width: 80, suppressSizeToFit: true, field: 'UhrzeitPlan' },
      { headerName: 'Aktuell', headerGroup: 'Uhrzeit', width: 80, suppressSizeToFit: true, field: 'UhrzeitAktuell' },
      { headerName: 'Aktenzeichen', width: 150, suppressSizeToFit: true, field: 'Az' },
      { headerName: 'Status', width: 150, suppressSizeToFit: true, field: 'Status' },
      { headerName: 'Aktiv', headerGroup: 'Parteien', suppressSorting: true, suppressMenu: true, template: '<span ng-repeat="item in data.ParteienAktiv">{{item.Partei}}<span ng-hide="$last">; </span></span>' },
      { headerName: 'Passiv', headerGroup: 'Parteien', suppressSorting: true, suppressMenu: true, template: '<span ng-repeat="item in data.ParteienPassiv">{{item.Partei}}<span ng-hide="$last">; </span></span>' },
      { headerName: 'Datum', width: 100, suppressSizeToFit: true, field: 'Datum' },
      { headerName: '', width: 100, suppressSizeToFit: true, suppressSorting: true, suppressMenu: true, template: '<a ui-sref="term({id:data.VerfahrensId})">Bearbeiten</a>' }
    ];

    vm.gridOptions = {
      angularCompileRows: true,
      enableSorting: true,
      enableFilter: true,
      columnDefs: columnDefs,
      rowData: null,
      groupHeaders: true,
      groupKeys: ['Gericht', 'Sitzungssaal'],
      groupUseEntireRow: true,
      ready: function (api) {
        api.sizeColumnsToFit();
        api.setSortModel(defaultSort);
      }
    };

    activate();

    function activate() {
      dataService.getVerfahrenList().then(function (data) {
        vm.gridOptions.rowData = data.results;
        vm.gridOptions.api.onNewRows();
        vm.gridOptions.api.expandAll();
      })
    };

  };

})();