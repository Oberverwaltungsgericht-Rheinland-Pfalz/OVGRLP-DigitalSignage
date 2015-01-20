(function () {
    var app = angular.module('app.terms');

    app.controller('TermsController', ['$scope', '$stateParams', '$filter', 'Terms', 'ngTableParams', function($scope, $stateParams, $filter, Terms, ngTableParams) {
        $scope.table = new ngTableParams({
            page: 1,
            count: 15,
            sorting: {
                uhrzeitAktuell: 'asc'
            }
        }, {
            counts: [10, 15, 20, 30, 40],
            total: 0,
            getData: function ($defer, params) {
                Terms.getList().then(function (terms) {
                    var filteredTerms = params.filter() ? $filter('filter')(terms, params.filter()) : terms
                    var orderedTerms = params.sorting() ? $filter('orderBy')(filteredTerms, params.orderBy()) : filteredTerms;

                    params.total(orderedTerms.length);
                    $defer.resolve(orderedTerms.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                });
            }
        });

        $scope.reload = function () {
            $scope.table.reload();
        };
    }]);

    app.controller('TermController', ['$scope', 'term', function ($scope, term) {
        $scope.term = term;
    }]);

    app.factory('Terms', ['Restangular', function (Restangular) {
        Restangular.extendModel('daten/verfahren', function (model) {
            return model;
        });

        return Restangular.service('daten/verfahren');
    }]);

})();