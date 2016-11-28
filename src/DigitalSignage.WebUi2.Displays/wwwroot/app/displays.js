/*! DigitalSignage.WebUi2.Displays - v2.2.0-1647 - 28.11.2016 */
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
(function () {
  'use strict';

  angular
    .module('app.core', [
      'ngAnimate',
      'ngMaterial',
      'ui.router',
      'restangular'
    ]);
})();
(function () {
  'use strict';

  angular
    .module('app.core')
    .filter('capitalize', capitalize);

  function capitalize() {
    return function (input, all) {
      return (!!input) ? input.replace(/([^\W_]+[^\s-]*) */g, function (txt) {
        return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
      }) : '';
    }
  };
})();
(function () {
  'use strict';

  angular
    .module('app')
    .controller('DisplayController', DisplayController);

  DisplayController.$inject = ['$scope', '$sce', '$stateParams', '$interval', 'Restangular', 'appConfig'];

  function DisplayController($scope, $sce, $stateParams, $interval, Restangular, appConfig) {
    var vm = this;
    var DisplaysSrv = Restangular.service('settings/displays');

    vm.display = [];
    vm.dateTime = new Date();
    vm.notes = null;

    activate();

    function activate() {
      $interval(function () {
        vm.dateTime = new Date();
      }, 1000 * 60);

      loadDisplay();

      $interval(function () {
        loadDisplay();
      }, 1000 * 60);
    }
    
    function loadDisplay() {
      DisplaysSrv.one($stateParams.id).get().then(function (display) {
        vm.display = display;
        vm.layout = appConfig.templatesUrl + '/' + display.template + '/main.html';
        vm.notes = $sce.trustAsHtml(vm.display.notes);

        $scope.$parent.loadStyles(vm.display.styles);
      });
    }
  };
})();
(function () {
  'use strict';

  angular
    .module('app')
    .controller('MainController', MainController);

  MainController.$inject = ['$scope', 'appConfig'];

  function MainController($scope, appConfig) {
    var vm = this;

    vm.styles = [];

    activate();

    function activate() {
    }

    $scope.loadStyles = function (styles) {
      vm.styles = [];
      vm.styles.push('css/default.css');
      styles.split(',').forEach(function (style) {
        vm.styles.push(appConfig.stylesUrl + '/' + style.trim() + '.css');
      });
    };
  };
})();
(function () {
  'use strict';

  angular
    .module('app')
    .controller('TermineController', TermineController);

  TermineController.$inject = ['$scope', '$stateParams', '$timeout', '$interval', '$filter', 'Restangular'];

  function TermineController($scope, $stateParams, $timeout, $interval, $filter, Restangular) {
    var vm = this;
    var TermineSrv = Restangular.service('termine', Restangular.one('settings/displays', $stateParams.id));

    vm.updateInterval = 15 * 1000;
    vm.termine = [];
    vm.detailTermin = null;

    activate();

    function activate() {
      updateData();

      $interval(function () {
        updateData();
      }, vm.updateInterval);
    };

    function updateData() {
      TermineSrv.getList().then(function (data) {
        var now = moment();

        vm.termine = $filter('orderBy')(data, '+uhrzeitAktuell');

        vm.termine.forEach(function (term) {
          var termDat = moment(term.datum + ' ' + term.uhrzeitAktuell, 'DD.MM.YYYY HH:mm');
          if (termDat.isValid()) {
            term.beginnt = termDat.diff(now, 'minutes');
          } else {
            term.beginnt = -1;
          };
        });

        updateDetailTermin(vm.termine);
      });
    };

    function updateDetailTermin(termine) {
      var tmpData = _.filter(termine, function (termin) {
        if (termin.status == 'Läuft')
          return true;
      });

      if (tmpData.length < 1) {
        tmpData = _.filter(termine, function (termin) {
          if (termin.status != 'Abgeschlossen' && termin.status != 'Aufgehoben')
            return true;
        });
      }

      if (tmpData.length >= 1) {
        vm.detailTermin = tmpData[0];
      } else {
        vm.detailTermin = null;
      }
    };

    function arrayToString(data) {
      var outStr = '';
      data.forEach(function (item) {
        outStr = outStr + item.uhrzeitAktuell + ' ';
      });
      return outStr;
    };
  };
})();