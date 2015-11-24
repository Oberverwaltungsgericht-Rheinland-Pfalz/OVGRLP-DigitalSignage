﻿(function () {
  'use strict';

  angular.module('app', [
    'app.core',
    'app.displays',
    'app.terms',
    'ngMaterial',
    'ui.router',
    'agGrid'
  ])
  .run(run)
  .config(config);

  run.$inject = ['$rootScope', '$state', '$stateParams'];
  config.$inject = ['$mdThemingProvider', '$mdIconProvider', '$stateProvider', '$urlRouterProvider'];

  function run($rootScope, $state, $stateParams) {
    $rootScope.$state = $state;
    $rootScope.$stateParams = $stateParams;
  }

  function config($mdThemingProvider, $mdIconProvider, $stateProvider, $urlRouterProvider) {
    $mdThemingProvider.theme('default')
      .primaryPalette('brown')
      .accentPalette('red');

    $mdIconProvider
      .icon('menu', 'icons/ic_menu_black_48px.svg')
      .icon('save', 'icons/ic_save_black_48px.svg')
      .icon('terms', 'icons/ic_event_note_black_48px.svg')
      .icon('display', 'icons/ic_dvr_black_48px.svg')
      .icon('power', 'icons/ic_power_settings_new_black_48px.svg')
      .icon('restart', 'icons/ic_replay_black_48px.svg')
      .icon('refresh', 'icons/ic_refresh_black_48px.svg')
      .icon('delete', 'icons/ic_delete_black_24px.svg')
      .icon('person', 'icons/ic_person_black_24px.svg')
      .icon('personadd', 'icons/ic_person_add_black_24px.svg');

    $stateProvider
      .state('displays', {
        url: '/displays',
        templateUrl: 'app/displays/displays.html',
        controller: 'DisplaysController',
        controllerAs: 'vm'
      })
      .state('display', {
        url: '/displays/:id',
        templateUrl: 'app/displays/display.html',
        controller: 'DisplayController',
        controllerAs: 'vm'
      })
      .state('terms', {
        url: '/terms',
        templateUrl: 'app/terms/terms.html',
        controller: 'TermsController',
        controllerAs: 'vm'
      })
      .state('term', {
        url: '/terms/:id',
        templateUrl: 'app/terms/term.html',
        controller: 'TermController',
        controllerAs: 'vm'
      });

    $urlRouterProvider.otherwise('/displays');
  }

})();