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
      {
        headerName: 'Urhzeit',
        children: [
          {
            headerName: 'Plan',
            width: 80,
            suppressSizeToFit: true,
            field: 'UhrzeitPlan'
          },
          {
            headerName: 'Aktuell',
            width: 80,
            suppressSizeToFit: true,
            field: 'UhrzeitAktuell'
          }
        ]
      },
      {
        headerName: 'Aktenzeichen',
        width: 150,
        suppressSizeToFit: true,
        template: '<a ui-sref="main.term({id:data.VerfahrensId})">{{data.Az}}</a>'
      },
      {
        headerName: 'Gericht',
        hide: true,
        rowGroupIndex: 0,
        field: 'Gericht'
      },
      {
        headerName: 'Sitzungssaal',
        hide: true,
        rowGroupIndex: 1,
        field: 'Sitzungssaal'
      },
      {
        headerName: 'Status',
        width: 150,
        suppressSizeToFit: true,
        field: 'Status'
      },
      {
        headerName: 'Parteien',
        children: [
          {
            headerName: 'Aktiv',
            suppressSorting: true,
            suppressMenu: true,
            template: '<span ng-repeat="item in data.ParteienAktiv">{{item.Partei}}<span ng-hide="$last">; </span></span>'
          },
          {
            headerName: 'Passiv',
            suppressSorting: true,
            suppressMenu: true,
            template: '<span ng-repeat="item in data.ParteienPassiv">{{item.Partei}}<span ng-hide="$last">; </span></span>'
          }
        ]
      },
      {
        headerName: 'Datum',
        width: 100,
        field: 'Datum'
      }
    ];

    vm.gridOptions = {
      angularCompileRows: true,
      enableSorting: true,
      enableFilter: true,
      columnDefs: columnDefs,
      rowData: null,
      groupUseEntireRow: true,
      groupDefaultExpanded: 1,
      onGridReady: function (params) {
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