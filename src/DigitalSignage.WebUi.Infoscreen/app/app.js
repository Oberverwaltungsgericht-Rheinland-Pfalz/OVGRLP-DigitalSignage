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

        $scope.filterGroups = [
            {
                title: 'Gericht',
                filters: [
                    {
                        title: 'Oberverwaltungsgericht Rheinland-Pfalz'
                    },
                    {
                        title: 'Verwaltungsgericht Koblenz'
                    },
                    {
                        title: 'Sozialgerich Koblenz'
                    },
                    {
                        title: 'Arbeitsgericht Koblenz'
                    }
                ]
            },
            {
                title: 'Status',
                filters: [
                    {
                        title: 'Läuft'
                    },
                    {
                        title: 'Abgeschlossen'
                    },
                    {
                        title: 'Verschoben'
                    },
                    {
                        title: 'Aufgehoben'
                    }
                ]
            },
            {
                title: 'Uhrzeit',
                filters: [
                    {
                        title: 'vor 9:00 Uhr'
                    },
                    {
                        title: '9:00 Uhr bis 11:00 Uhr'
                    },
                    {
                        title: '11:00 Uhr bis 13:00 Uhr'
                    },
                    {
                        title: 'nach 13:00 Uhr'
                    }
                ]
            }
        ];

        Termine.getList().then(function (data) {
            $scope.terms = data;
        });
    });

    app.factory('Termine', function (Restangular) {
        return Restangular.service('daten/verfahren');
    });
})();