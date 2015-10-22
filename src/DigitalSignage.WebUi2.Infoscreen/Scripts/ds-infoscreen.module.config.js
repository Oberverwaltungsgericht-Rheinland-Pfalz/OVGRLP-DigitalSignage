(function () {
  'use strict';

  angular
    .module('ds-infoscreen')
    .config(config);

  config.$inject = ['RestangularProvider'];

  function config(RestangularProvider) {
    RestangularProvider.setBaseUrl('http://localhost:52208');
  }
})();