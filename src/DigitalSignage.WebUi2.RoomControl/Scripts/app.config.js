(function () {
  'use strict';

  angular
    .module('app')
    .constant('appConfig', {
      apiUrl: 'http://localhost:52208',
      termDetailsUrl: 'http://localhost:51445/#/terms/',
      showTermDetails: true,
      showHome: false
    });

})();