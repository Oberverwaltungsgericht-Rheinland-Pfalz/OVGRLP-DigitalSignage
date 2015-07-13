(function () {
    var app = angular.module('app.terms');

    app.controller('TermsController', function ($scope, $stateParams, $filter, Restangular) {
        var baseTerms = Restangular.all('daten/verfahren');
        var defaultSort =[
            {field: 'uhrzeitAktuell', sort: 'asc'}
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

        $scope.gridOptions = {
            angularCompileRows: true,            enableSorting: true,            enableFilter: true,            columnDefs: columnDefs,
            rowData: null,
            groupHeaders: true,            groupKeys: ['sitzungssaal'],            groupUseEntireRow: true,            ready: function (api) {
                api.sizeColumnsToFit();
                api.setSortModel(defaultSort);
            }
        };

        baseTerms.getList().then(function (data) {
            $scope.gridOptions.rowData = data;
            $scope.gridOptions.api.onNewRows();
        });
    });

    app.controller('TermDetailsController', ['$scope', 'term', function ($scope, $stateParams, Restangular) {
        console.log('load' + $stateParams.id);
    }]);

    app.factory('Terms', ['Restangular', function (Restangular) {
        Restangular.extendModel('daten/verfahren', function (model) {
            return model;
        });

        return Restangular.service('daten/verfahren');
    }]);

})();