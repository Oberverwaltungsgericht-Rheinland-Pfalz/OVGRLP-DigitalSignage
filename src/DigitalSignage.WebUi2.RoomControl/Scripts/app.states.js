(function () {
  'use strict';

  angular
    .module('app')
    .config(config);

  config.$inject = ['$stateProvider', '$urlRouterProvider'];

  function config($stateProvider, $urlRouterProvider) {
    $stateProvider
      .state('rooms', {
        url: '/',
        templateUrl: 'app/rooms/rooms.html',
        controller: 'RoomsController',
        controllerAs: 'vm'
      }).state('room', {
        abstract: true,
        url: '/:id',
        templateUrl: 'app/rooms/room.html',
        controller: 'RoomController',
        controllerAs: 'vm'
      }).state('room.terms', {
        url: '/terms',
        templateUrl: 'app/terms/terms.html',
        controller: 'TermsController',
        controllerAs: 'vm'
      }).state('room.display', {
        url: '/display',
        templateUrl: 'app/display/display.html',
        controller: 'DisplayController',
        controllerAs: 'vm'
      });

    $urlRouterProvider.otherwise('/');
  }
})();