(function () {
    'use strict';

    var app = angular.module('infoscreen.app', [
        'ngMaterial',
        'restangular'
    ]);

    app.config(function (RestangularProvider) {
        RestangularProvider.setBaseUrl('http://localhost:52208');
    });

    app.controller('InfoscreenMainController', function ($scope, Termine) {
        $scope.title = 'Digital Signage - Infoscreen';

        $scope.terms = [];

        $scope.filters = [];

        $scope.filters.gericht = [
            {
                active: false,
                title: 'Oberverwaltungsgericht Rheinland-Pfalz',
                expression: { gericht: 'Oberverwaltungsgericht Rheinland-Pfalz' }
            },
            {
                active: false,
                title: 'Verwaltungsgericht Koblenz',
                expression: { gericht: 'Verwaltungsgericht Koblenz' }
            },
            {
                active: false,
                title: 'Sozialgericht Koblenz',
                expression: { gericht: 'Sozialgericht Koblenz' }
            },
            {
                active: false,
                title: 'Arbeitsgericht Koblenz',
                expression: { gericht: 'Arbeitsgericht Koblenz' }
            }
        ];

        $scope.filters.status = [
            {
                active: false,
                title: 'Läuft',
                expression: { status: 'Läuft' }
            },
            {
                active: false,
                title: 'Abgeschlossen',
                expression: { status: 'Abgeschlossen' }
            },
            {
                active: false,
                title: 'Verschoben',
                expression: { status: 'Verschoben' }
            },
            {
                active: false,
                title: 'Aufgeboben',
                expression: { status: 'Aufgehoben' }
            },
        ];

        $scope.updateData = function () {
            Termine.getList().then(function (data) {
                $scope.terms = data;
            });
        };

        $scope.updateData();
    });

    app.factory('Termine', function (Restangular) {
        return Restangular.service('daten/verfahren');
    });

    app.filter('multifilter', function () {
        return function (items, options) {
            var activeFilter = _.where(options, { active: true });

            var resultItems = [];

            if (activeFilter.length > 0) {
                _.each(activeFilter, function (filter) {
                    resultItems = resultItems.concat(_.where(items, filter.expression));
                });
            } else {
                resultItems = items;
            }

            return _.uniq(resultItems);
        }
    });
})();