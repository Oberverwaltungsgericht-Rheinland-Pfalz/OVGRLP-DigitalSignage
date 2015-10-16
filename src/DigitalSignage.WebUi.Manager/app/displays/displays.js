(function () {
  var app = angular.module('app.displays');

  app.controller('DisplaysController', function ($scope, Restangular) {
    var columnDefs = [
        { headerName: '', width: 30, suppressSizeToFit: true, template: '<img ng-src="{{getStateImg(data.status)}}" alt="{{getStateText(data.status)}}"></img>' },
        { headerName: 'Gruppe', field: 'group' },
        { headerName: 'Name', field: 'name' },
        { headerName: 'Titel', field: 'title' },
        { headerName: '', template: '<a ui-sref="displays.details({id:data.id})">Details</a>' }
    ];

    var baseDisplays = Restangular.all('settings/displays');

    $scope.getStateText = function (id) {
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

    $scope.getStateImg = function (id) {
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

    $scope.gridOptions = {
      angularCompileRows: true,
      columnDefs: columnDefs,
      rowData: null,
      ready: function (api) {
        api.sizeColumnsToFit();
      }
    };

    baseDisplays.getList().then(function (data) {
      $scope.gridOptions.rowData = data;
      $scope.gridOptions.rowData.forEach(function (display) {
        display.status = -1;
        display.customGET('status').then(function (data) {
          display.status = data.result;
        });
      });
      $scope.gridOptions.api.onNewRows();
    });
  });

  app.controller('DisplayDetailsController', function ($scope, $stateParams, $http, Restangular) {
    var baseDisplay = Restangular.one('settings/displays', $stateParams.id);

    $scope.poweron = function () {
      if ($scope.display && $scope.display.controlUrl) {
        $scope.display.customGET('start').then(function (data) {
        });
      };
    };

    $scope.restart = function () {
      if ($scope.display && $scope.display.controlUrl) {
        $http.get($scope.display.controlUrl + '/api/restart');
      };
    };

    $scope.shutdown = function () {
      if ($scope.display && $scope.display.controlUrl) {
        $http.get($scope.display.controlUrl + '/api/shutdown');
      };
    };

    $scope.refresh = function () {
      if ($scope.display && $scope.display.controlUrl) {
        update();
      };
    };

    update = function () {
      if ($scope.display && $scope.display.controlUrl) {
        $scope.loading = true;

        $scope.display.status = -1;
        $scope.display.customGET('status').then(function (data) {
          $scope.display.status = data.result;

          if ($scope.display.status < 1) {
            $scope.screenshot = 'assets/img/offline-display.png';
          } else {
            $scope.screenshot = $scope.display.controlUrl + '/api/screenshot?dt=' + new Date().getTime();
          };

          $scope.loading = false;
        }, function (error) {
          $scope.loading = false;
        });
      };
    };

    baseDisplay.get().then(function (display) {
      $scope.display = display;
      update();
    });
  });

  app.factory('Displays', function (Restangular) {
    Restangular.extendModel('settings/displays', function (model) {
      model.status = 0;

      model.updateStatus = function () {
        model.status = -1;
        model.customGET('status').then(function (data) {
          model.status = data.result;
        });
      };

      return model;
    });

    return Restangular.service('settings/displays');
  });
})();