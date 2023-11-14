// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
(function () {
    var app = angular.module('app', [
        // Custom modules

        // 3rd Party Modules
        'ngMaterial'
    ]);

    /*
    app.run(['$rootScope', '$state', '$stateParams', function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
        
    }]);
    */

    app.config(function ($mdThemingProvider, $mdIconProvider) {
        $mdThemingProvider.theme('default')
           .primaryPalette('brown')
           .accentPalette('red');

        $mdIconProvider
            .icon('shutdown', 'assets/icons/ic_power_settings_new_black_48px.svg')
            .icon('restart', 'assets/icons/ic_replay_black_48px.svg')
            .icon('refresh', 'assets/icons/ic_refresh_black_48px.svg');
    });

    app.controller('DisplayController', function ($scope, $http) {
        $scope.screenshot = '../../api/screenshot?dt=' + new Date().getTime();

        $scope.restart = function () {
            $http.get('../../api/restart');
        };

        $scope.shutdown = function () {
            $http.get('../../api/shutdown');
        };

        $scope.refresh = function () {
            $scope.screenshot = '../../api/screenshot?dt=' + new Date().getTime();
        };
    });

})();