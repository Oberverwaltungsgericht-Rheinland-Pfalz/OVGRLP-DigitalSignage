(function () {
  'use strict';

  angular
    .module('app')
    .constant('appConfig', {
      apiUrl: 'http://localhost:52208',
      templatesUrl: '../templates',
      stylesUrl: '../styles'
    });
})();