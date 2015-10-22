(function () {
  'use strict';

  angular
    .module('app.displays')
    .controller('DisplaysController', DisplaysController);

  DisplaysController.$inject = ['settingsDataService'];

  function DisplaysController(settingsDataService) {
    var vm = this;

    var columnDefs = [
      { headerName: '', width: 30, suppressSizeToFit: true, template: '<img ng-src="{{vm.getStateImg(data.Status)}}" alt="{{vm.getStateText(data.Status)}}"></img>' },
      { headerName: 'Name', field: 'Name' },
      { headerName: 'Titel', field: 'Title' },
      { headerName: '', width: 70, suppressSizeToFit: true, template: '<a ui-sref="display({id:data.Id})">Details</a>' }, 
      { headerName: '', width: 110, suppressSizeToFit: true, template: '<a href="" ng-click="data.update()">Aktualisieren</a>'}
    ];

    vm.getStateText = getStateText;
    vm.getStateImg = getStateImg;
    vm.gridOptions = {
      angularCompileRows: true,
      columnDefs: columnDefs,
      rowData: null,
      groupKeys: ['Group'],
      groupUseEntireRow: true,
      ready: function (api) {
        api.sizeColumnsToFit();
      }
    };

    activate();

    function activate() {
      settingsDataService.getDisplayList().then(function(data) {
        vm.gridOptions.rowData = data.results;
        vm.gridOptions.rowData.forEach(function (display) {
          display.update();
        });
        vm.gridOptions.api.onNewRows();
        vm.gridOptions.api.expandAll();
      });
    }

    function getStateText(id) {
      if (id == -1) {
        return "undefiniert";
      } else if (id == 0) {
        return "ausgeschaltet";
      } else if (id == 1) {
        return "angeschaltet";
      } else {
        return "unbekannt";
      }
    };

    function getStateImg(id) {
      if (id == -1) {
        return "assets/img/display-undefined-icon.png";
      } else if (id == 0) {
        return "assets/img/display-offline-icon.png";
      } else if (id == 1) {
        return "assets/img/display-online-icon.png";
      } else {
        return "assets/img/display-unknown-icon.png";
      }
    };
  }
})();