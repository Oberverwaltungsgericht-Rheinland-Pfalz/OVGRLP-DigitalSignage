(function () {
    var app = angular.module('app', [
        // 3rd Party Modules
        'ngMaterial',
        'ui.router',
        'restangular'
    ]);

    app.run(['$rootScope', '$state', '$stateParams', function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    }]);

    app.config(function ($stateProvider, $urlRouterProvider, $mdThemingProvider, $mdIconProvider, RestangularProvider) {
        $stateProvider
            .state('rooms', {
                url: '/',
                templateUrl: 'app/rooms.html',
                controller: 'RoomsController'
            }).state('room', {
                abstract: true,
                url: '/:id',
                templateUrl: 'app/room.html',
                controller: 'RoomController'
            }).state('room.terms', {
                url: '/terms',
                templateUrl: 'app/terms.html',
                controller: 'RoomTermsController'
            }).state('room.display', {
                url: '/display',
                templateUrl: 'app/display.html',
                controller: 'RoomDisplayController'
            });

        $urlRouterProvider.otherwise('/');
    });

    app.controller('RoomsController', function ($scope, $mdDialog, Restangular) {
        $scope.displays = [];

        var baseDisplays = Restangular.all('settings/displays');

        $scope.loading = true;

        baseDisplays.getList().then(function (data) {
            $scope.displays = data;
            $scope.loading = false;
        }, function (error) {
            $scope.loading = false;
            $mdDialog.show(
              $mdDialog.alert()
                .parent(angular.element(document.body))
                .title('Fehler')
                .content('Beim abrufen der Daten ist ein Fehler aufgetreten.')
                .ariaLabel('Fehler Dialog')
                .ok('OK')
            );
        });
    });

    app.controller('RoomController', function ($scope, $stateParams, Restangular) {
        $scope.baseDisplay = Restangular.one('settings/displays', $stateParams.id);
        $scope.baseDisplay.get().then(function (data) {
            $scope.display = data;
        });
    });

    app.controller('RoomTermsController', function ($scope, $filter, $mdToast, Restangular) {
        $scope.termine = [];
        $scope.loading = true;
        
        $scope.baseDisplay.getList('termine').then(function (data) {
            $scope.termine = data;
            $scope.loading = false;
        }, function (error) {
            $mdDialog.show(
              $mdDialog.alert()
                .parent(angular.element(document.body))
                .title('Fehler')
                .content('Beim abrufen der Daten ist ein Fehler aufgetreten.')
                .ariaLabel('Fehler Dialog')
                .ok('OK')
            );
            $scope.loading = false;
        });

        $scope.save = function () {

            $scope.termine.forEach(function (termin) {
                Restangular.one('daten/verfahren', termin.id).customPUT(termin).then(function () {

                }, function () {
                    //TODO: Fehler melden
                });
            });

            $mdToast.show($mdToast.simple()
                .content('Gespeichert!')
                .position('top left')
                .hideDelay(4000)
            );
        };
    });

    app.controller('RoomDisplayController', function ($scope, $http) {
        $scope.$watch('display', function (newData, oldData) {
            update();
        });

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
                        $scope.screenshot = '../assets/img/offline-display.png';
                    } else {
                        $scope.screenshot = $scope.display.controlUrl + '/api/screenshot?dt=' + new Date().getTime();
                    };

                    $scope.loading = false;
                }, function (error) {
                    $scope.loading = false;
                });
            };
        };
    });

    /*
    app.controller('RoomsController', function ($scope, Displays) {
        $scope.rooms = null;

        Displays.getList().then(function (data) {
            data.forEach(function (display) {
                display.updateStatus();
            });

            $scope.rooms = data;
        });
    });
    */

    /*
    app.controller('RoomController', function ($scope, $filter, $stateParams, Restangular) {
        var TermineSrv = Restangular.service('termine', Restangular.one('settings/displays', $stateParams.id));

        $scope.termine = [];

        updateData = function () {
            TermineSrv.getList().then(function (data) {
                $scope.termine = $filter('orderBy')(data, '+uhrzeitAktuell');
            });
        };

        updateData();
    });*/

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