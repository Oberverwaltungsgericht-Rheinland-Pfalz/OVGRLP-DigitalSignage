(function () {
  'use strict';

  angular
    .module('app', [
      'ngMaterial',
      'ui.router',
      'restangular'
    ])
    .config(config);

  config.$inject = ['$mdThemingProvider', '$mdIconProvider', 'RestangularProvider', 'appConfig']

  function config($mdThemingProvider, $mdIconProvider, RestangularProvider, appConfig) {
    RestangularProvider.setBaseUrl(appConfig.apiUrl);

    $mdThemingProvider.theme('default')
      .primaryPalette('indigo')
      .accentPalette('red');

    $mdIconProvider
      .icon('menu', 'icons/ic_menu_black_48px.svg')
      .icon('home', 'icons/ic_home_black_48px.svg')
      .icon('save', 'icons/ic_save_black_48px.svg')
      .icon('terms', 'icons/ic_event_note_black_48px.svg')
      .icon('display', 'icons/ic_dvr_black_48px.svg')
      .icon('power', 'icons/ic_power_settings_new_black_48px.svg')
      .icon('restart', 'icons/ic_replay_black_48px.svg')
      .icon('refresh', 'icons/ic_refresh_black_48px.svg');
  }
})();