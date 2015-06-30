(function () {
    var app = angular.module('app.displays');

    app.controller('DisplaysController', function ($scope, Displays) {
        var columnDefs = [
            { headerName: 'Gruppe', field: 'group'},
            { headerName: 'Name', field: 'name'},
            { headerName: 'Titel', field: 'title'},
            { headerName: 'Status', field: 'status' },
            { headerName: '', template: '<a ui-sref="displays.details({id:data.id})">Details</a>' }

        ];

        $scope.gridOptions = {
            angularCompileRows: true,            columnDefs: columnDefs,
            rowData: null
        };

        Displays.getList().then(function (data) {
            data.forEach(function (display) {
                display.updateStatus();
            });

            $scope.gridOptions.rowData = data;
            $scope.gridOptions.api.onNewRows();
        });
    });

    app.controller('DisplayController', function ($scope, $stateParams, Displays) {
        $scope.display = Displays.one($stateParams.id).get().$object;
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