(function () {
  var app = angular.module('app');

  app.config(function ($stateProvider, $urlRouterProvider, RestangularProvider) {
    RestangularProvider.setBaseUrl('http://localhost:52208');

    $stateProvider
        .state('index', {
          url: '/',
          templateUrl: 'app/index.html'
        }).state('display', {
          url: '/{id}',
          templateUrl: 'app/display.html',
          controller: 'DisplayController',
          controllerAs: 'dc'
        });

    $urlRouterProvider.otherwise('/');
  });
})();