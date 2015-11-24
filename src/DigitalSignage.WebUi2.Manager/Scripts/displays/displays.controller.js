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
      { headerName: 'Name', template: '<a ui-sref="display({id:data.Id})">{{data.Name}}</a>' },
      { headerName: 'Titel', field: 'Title' },
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
      onReady: function (params) {
        params.api.sizeColumnsToFit();
      }
    };

    activate();

    function activate() {
      settingsDataService.getDisplayList().then(function (data) {
        vm.gridOptions.api.setRowData(data.results);
        vm.gridOptions.rowData.forEach(function (display) {
          display.update();
        });
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
        return "img/display-undefined-icon.png";
      } else if (id == 0) {
        return "img/display-offline-icon.png";
      } else if (id == 1) {
        return "img/display-online-icon.png";
      } else {
        return "img/display-unknown-icon.png";
      }
    };
  }
})();