(function () {
  'use strict';

  angular
    .module('app.terms')
    .controller('TermsController', TermsController);

  TermsController.$inject = ['$stateParams', '$filter', 'termsDataService'];

  function TermsController($stateParams, $filter, termsDataService) {
    var vm = this;

    var defaultSort = [
      { field: 'uhrzeitAktuell', sort: 'asc' }
    ];

    var columnDefs = [
      { headerName: 'Plan', headerGroup: 'Uhrzeit', width: 80, suppressSizeToFit: true, field: 'UhrzeitPlan' },
      { headerName: 'Aktuell', headerGroup: 'Uhrzeit', width: 80, suppressSizeToFit: true, field: 'UhrzeitAktuell' },
      { headerName: 'Aktenzeichen', width: 150, suppressSizeToFit: true, template: '<a ui-sref="main.term({id:data.VerfahrensId})">{{data.Az}}</a>' },
      { headerName: 'Status', width: 150, suppressSizeToFit: true, field: 'Status' },
      { headerName: 'Aktiv', headerGroup: 'Parteien', suppressSorting: true, suppressMenu: true, template: '<span ng-repeat="item in data.ParteienAktiv">{{item.Partei}}<span ng-hide="$last">; </span></span>' },
      { headerName: 'Passiv', headerGroup: 'Parteien', suppressSorting: true, suppressMenu: true, template: '<span ng-repeat="item in data.ParteienPassiv">{{item.Partei}}<span ng-hide="$last">; </span></span>' },
      { headerName: 'Datum', width: 100, field: 'Datum' }
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
      groupDefaultExpanded: 1,
      onReady: function (params) {
        params.api.sizeColumnsToFit();
        params.api.setSortModel(defaultSort);
      }
    };

    activate();

    function activate() {
      termsDataService.getVerfahrenList().then(function (data) {
        vm.gridOptions.api.setRowData(data.results);
      })
    };

  };

})();