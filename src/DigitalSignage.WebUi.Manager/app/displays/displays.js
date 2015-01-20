(function () {
    var app = angular.module('app.displays');

    app.controller('DisplayController', ['$scope', 'display', function ($scope, display) {
        console.log(display);
        $scope.display = display;
    }]);

    app.controller('DisplaysController', ['$scope', '$filter', 'Displays', 'ngTableParams', function ($scope, $filter, Displays, ngTableParams) {
        $scope.title = 'Saalanzeigen';

        $scope.table = new ngTableParams({
            page: 1,
            count: 50,
            sorting: {
                name: 'asc'
            }
        }, {
            counts: [],
            groupBy: 'group',
            total: 0,
            getData: function ($defer, params) {
                Displays.getList().then(function (data) {
                    var filteredData = params.filter() ? $filter('filter')(data, params.filter()) : data;
                    var orderedData = params.sorting() ? $filter('orderBy')(filteredData, params.orderBy()) : filteredData;

                    orderedData.forEach(function (display) {
                        display.updateStatus();
                    });

                    params.total(orderedData.length);
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                });
            }
        });
    }]);

    app.factory('Displays', ['Restangular', function (Restangular) {
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
    }]);
})();