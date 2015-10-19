(function () {
  'use strict';

  angular
    .module('app', [
      'app.core',
      'app.displays',
      'app.terms',
      'ngMaterial',
      'ui.router',
      'restangular',
      'angularUtils.directives.uiBreadcrumbs',
      'angularGrid'
    ]);

})();