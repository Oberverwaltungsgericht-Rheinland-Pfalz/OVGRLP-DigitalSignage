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

        RestangularProvider.setBaseUrl('http://localhost:52208');

        $mdThemingProvider.theme('default')
            .primaryPalette('indigo')
            .accentPalette('red');

        $mdIconProvider
            .icon('menu', 'assets/icons/ic_menu_black_48px.svg')
            .icon('save', 'assets/icons/ic_save_black_48px.svg')
            .icon('terms', 'assets/icons/ic_event_note_black_48px.svg')
            .icon('display', 'assets/icons/ic_dvr_black_48px.svg');
    });


    app.controller('RoomsController', function ($scope, Restangular) {
        $scope.displays = [];

        var baseDisplays = Restangular.all('settings/displays');

        baseDisplays.getList().then(function (data) {
            $scope.displays = data;
        });
    });

    app.controller('RoomController', function ($scope, $stateParams, Restangular) {
        $scope.baseDisplay = Restangular.one('settings/displays', $stateParams.id);
        $scope.display = $scope.baseDisplay.get().$object;
    });

    app.controller('RoomTermsController', function ($scope, $filter, $stateParams, $mdToast, Restangular) {
        $scope.termine = [];

        $scope.baseDisplay.getList('termine').then(function (data) {
            $scope.termine = data;
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

        /*
        var TermineSrv = Restangular.service('termine', Restangular.one('settings/displays', $stateParams.id));
        TermineSrv = Restangular.service('termine', $scope.display);

        TermineSrv.getList().then(function (data) {
            $scope.termine = $filter('orderBy')(data, '+uhrzeitAktuell');
        });
        */
    });

    app.controller('RoomDisplayController', function ($scope, $stateParams) {
        $scope.id = $stateParams.id;
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