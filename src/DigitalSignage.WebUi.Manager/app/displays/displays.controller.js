(function () {
  'use strict';

  angular
    .module('app.displays')
    .controller('DisplaysController', DisplaysController);

  DisplaysController.$inject = ['Restangular'];

  function DisplaysController(Restangular) {
    var vm = this;

    var columnDefs = [
      { headerName: '', width: 30, suppressSizeToFit: true, template: '<img ng-src="{{getStateImg(data.status)}}" alt="{{getStateText(data.status)}}"></img>' },
      { headerName: 'Name', field: 'name' },
      { headerName: 'Titel', field: 'title' },
      { headerName: '', template: '<a ui-sref="displays.details({id:data.id})">Details</a>' }
    ];

    var baseDisplays = Restangular.all('settings/displays');

    vm.getStateText = getStateText;
    vm.getStateImg = getStateImg;
    vm.gridOptions = {
      angularCompileRows: true,
      columnDefs: columnDefs,
      rowData: null,
      groupKeys: ['group'],
      groupUseEntireRow: true,
      ready: function (api) {
        api.sizeColumnsToFit();
      }
    };

    activate();

    function activate() {
      baseDisplays.getList().then(function (data) {
        vm.gridOptions.rowData = data;
        vm.gridOptions.rowData.forEach(function (display) {
          display.status = -1;
          display.customGET('status').then(function (data) {
            display.status = data.result;
          });
        });
        vm.gridOptions.api.onNewRows();
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