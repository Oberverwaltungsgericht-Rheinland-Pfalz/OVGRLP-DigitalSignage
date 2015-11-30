(function () {
  'use strict';

  angular
    .module('app', [
      'app.core'
    ])
    .config(config);

  config.$inject = ['$stateProvider', '$urlRouterProvider', 'RestangularProvider', 'appConfig'];

  function config($stateProvider, $urlRouterProvider, RestangularProvider, appConfig) {
    RestangularProvider.setBaseUrl(appConfig.apiUrl);

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
  }

})();