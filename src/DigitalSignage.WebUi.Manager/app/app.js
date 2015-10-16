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

  app.config(function ($stateProvider, $urlRouterProvider) {
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
  });

})();