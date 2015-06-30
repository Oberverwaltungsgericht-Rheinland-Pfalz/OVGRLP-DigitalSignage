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

    app.config(function ($stateProvider, $urlRouterProvider, $mdThemingProvider, RestangularProvider) {
        $stateProvider
            .state('rooms', {
                url: '/',
                templateUrl: 'app/rooms.html',
                controller: 'RoomsController'
            })
            .state('room', {
                url: '/:id',
                templateUrl: 'app/room.html',
                controller: 'RoomController'
            });

        $urlRouterProvider.otherwise('/');

        RestangularProvider.setBaseUrl('http://localhost:52208');

        $mdThemingProvider.theme('default')
            .primaryPalette('grey')
            .accentPalette('green');
    });

    app.controller('RoomsController', function ($scope, Displays) {
        $scope.rooms = null;

        Displays.getList().then(function (data) {
            data.forEach(function (display) {
                display.updateStatus();
            });

            $scope.rooms = data;
        });
    });

    app.controller('RoomController', function ($scope, $filter, $stateParams, Restangular) {
        var TermineSrv = Restangular.service('termine', Restangular.one('settings/displays', $stateParams.id));

        $scope.termine = [];

        updateData = function () {
            TermineSrv.getList().then(function (data) {
                $scope.termine = $filter('orderBy')(data, '+uhrzeitAktuell');
            });
        };

        updateData();
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