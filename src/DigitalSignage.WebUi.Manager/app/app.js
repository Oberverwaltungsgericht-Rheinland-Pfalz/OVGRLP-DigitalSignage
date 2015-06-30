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

    app.config(function ($stateProvider, $urlRouterProvider, $mdThemingProvider, RestangularProvider) {
        $stateProvider
            .state('displays', {
                url: '/displays',
                templateUrl: 'app/displays/index.html',
                controller: 'DisplaysController',
                data: {
                    pageTitle: 'Anzeigen'
                }
            })
            .state('displays.details', {
                url: '/:id',
                templateUrl: 'app/displays/details.html',
                controller: 'DisplayController'
            }).state('terms', {
                url: '/terms',
                templateUrl: 'app/terms/index.html',
                controller: 'TermsController',
                data: {
                    pageTitle: 'Sitzungen'
                }
            });
            /*
            .state('displays', {
                url: '/displays',
                templateUrl: 'app/displays/index.html',
                controller: 'DisplaysController',
                data: {
                    displayName: 'Anzeigen'
                }
            }).state('displays.details', {
                url: '/:id',
                templateUrl: 'app/displays/details.html',
                controller: 'DisplayController',
                data: {
                    displayName: '{{display.name}}'
                },
                resolve: {
                    display: function ($stateParams, Displays) {
                        return Displays.one($stateParams.id).get();
                    }
                }
            }).state('terms', {
                abstract: true,
                data: {
                    proxy: 'terms.list'
                }
            }).state('terms.list', {
                url: '/terms',
                templateUrl: 'app/terms/index.html',
                controller: 'TermsController',
                data: {
                    displayName: 'Termine'
                }
            }).state('terms.details', {
                url: '/:id',
                templateUrl: 'app/terms/details.html',
                controller: 'TermController',
                data: {
                    displayName: '{{term.az}}'
                },
                resolve: {
                    term: function ($stateParams, Terms) {
                        return Terms.one($stateParams.id).get();
                    }
                }
            });
            */

        $urlRouterProvider.otherwise('/displays');

        RestangularProvider.setBaseUrl('http://localhost:52208');

        $mdThemingProvider.theme('default')
            .primaryPalette('brown')
            .accentPalette('red');
    });
})();