(function () {
    var app = angular.module('app', [
        // Custom modules
        'app.displays',
        'app.terms',

        // 3rd Party Modules
        'ngMaterial',
        'ui.router',
        'restangular',
        'angularUtils.directives.uiBreadcrumbs',
        'angularGrid'
    ]);

    angular.module('app.displays', []);
    angular.module('app.terms', [])

    app.run(['$rootScope', '$state', '$stateParams', function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    }]);

    app.config(function ($stateProvider, $urlRouterProvider, $mdThemingProvider, $mdIconProvider, RestangularProvider) {
        $stateProvider
            .state('displays', {
                url: '/displays',
                templateUrl: 'app/displays/index.html',
                controller: 'DisplaysController'
            })
            .state('displays.details', {
                url: '/:id',
                templateUrl: 'app/displays/details.html',
                controller: 'DisplayDetailsController'
            }).state('terms', {
                url: '/terms',
                templateUrl: 'app/terms/index.html',
                controller: 'TermsController'
            }).state('term', {
                url: '/terms/:id',
                templateUrl: 'app/terms/details.html',
                controller: 'TermDetailsController'
            });

        $urlRouterProvider.otherwise('/displays');

        RestangularProvider.setBaseUrl('http://localhost:52208');

        $mdThemingProvider.theme('default')
            .primaryPalette('brown')
            .accentPalette('red');

        $mdIconProvider
            .icon('menu', 'assets/icons/ic_menu_black_48px.svg')
            .icon('save', 'assets/icons/ic_save_black_48px.svg')
            .icon('terms', 'assets/icons/ic_event_note_black_48px.svg')
            .icon('display', 'assets/icons/ic_dvr_black_48px.svg')
            .icon('power', 'assets/icons/ic_power_settings_new_black_48px.svg')
            .icon('restart', 'assets/icons/ic_replay_black_48px.svg')
            .icon('refresh', 'assets/icons/ic_refresh_black_48px.svg')
            .icon('delete', 'assets/icons/ic_delete_black_24px.svg')
            .icon('person', 'assets/icons/ic_person_black_24px.svg')
            .icon('personadd', 'assets/icons/ic_person_add_black_24px.svg');
    });

})();