/*! DigitalSignage.WebUi2.RoomControl - v2.1.0 - 24.11.2015 */
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
(function () {
  'use strict';

  angular
    .module('app')
    .controller('DisplayController', DisplayController);

  DisplayController.$inject = ['$http', '$stateParams', 'Restangular']

  function DisplayController($http, $stateParams, Restangular) {
    var vm = this;
    var DisplaySrv = Restangular.service('settings/displays');

    vm.display = [];
    vm.loading = false;

    vm.poweron = poweron;
    vm.restart = restart;
    vm.shutdown = shutdown;
    vm.refresh = refresh;

    activate();

    function activate() {
      loadDisplay();
    }

    function loadDisplay() {
      vm.loading = true;

      DisplaySrv.one($stateParams.id).get().then(function (display) {
        vm.display = display;
        vm.display.status = -1;

        vm.display.customGET('status').then(function (data) {
          vm.display.status = data.result;

          if (vm.display.status < 1) {
            vm.screenshot = '../assets/img/offline-display.png';
          } else {
            vm.screenshot = vm.display.controlUrl + '/api/screenshot?dt=' + new Date().getTime();
          };
          vm.loading = false;
        }, function (error) {
          vm.loading = false;
        });
      }, function (error) {
        vm.loading = false;
      });
    }

    function poweron() {
      if (vm.display && vm.display.controlUrl) {
        vm.display.customGET('start').then(function (data) {
        });
      }
    }

    function restart() {
      if (vm.display && vm.display.controlUrl) {
        $http.get(vm.display.controlUrl + '/api/restart');
      };
    }

    function shutdown() {
      if (vm.display && vm.display.controlUrl) {
        $http.get(vm.display.controlUrl + '/api/shutdown');
      };
    }

    function refresh() {
      loadDisplay();
    }
  }
})();
(function () {
  'use strict';

  angular
    .module('app')
    .controller('RoomController', RoomController);

  RoomController.$inject = ['$stateParams', '$mdSidenav', 'Restangular', 'appConfig'];

  function RoomController($stateParams, $mdSidenav, Restangular, appConfig) {
    var vm = this;
    var DisplaySrv = Restangular.service('settings/displays');

    vm.display = [];
    vm.openNavbar = openNavbar;
    vm.appConfig = appConfig;

    activate();

    function activate() {
      loadDisplay();
    }

    function loadDisplay() {
      DisplaySrv.one($stateParams.id).get().then(function (display) {
        vm.display = display;
      })
    }

    function openNavbar() {
      $mdSidenav('navbar').toggle();
    }

    //$scope.baseDisplay = Restangular.one('settings/displays', $stateParams.id);
    //$scope.baseDisplay.get().then(function (data) {
    //  $scope.display = data;
    //});

    //$scope.openNavbar = function () {
    //  $mdSidenav('navbar').toggle();
    //};
  }
})();
(function () {
  'use strict';

  angular
    .module('app')
    .controller('RoomsController', RoomsController)
    .filter('multifilter', multifilter);

  RoomsController.$inject = ['$mdDialog', 'Restangular'];

  function RoomsController($mdDialog, Restangular) {
    var vm = this;
    var DisplaysSrv = Restangular.service('settings/displays');

    vm.displays = [];
    vm.loading = false;
    vm.filters = [];


    activate();

    function activate() {
      loadDisplays();
    }

    function loadDisplays() {
      vm.loading = true;

      DisplaysSrv.getList().then(function (displays) {
        vm.displays = displays;
        vm.loading = false;

        loadFilters();
      }, function (error) {
        vm.loading = false;
        $mdDialog.show(
        $mdDialog.alert()
          .parent(angular.element(document.body))
          .title('Fehler')
          .content('Beim abrufen der Daten ist ein Fehler aufgetreten.')
          .ariaLabel('Fehler Dialog')
          .ok('OK')
        );
      });
    }

    function loadFilters() {
      var groups = [];

      vm.displays.forEach(function (display) {
        groups.push(display.group);
      })

      _.uniq(groups).forEach(function (data) {
        vm.filters.push({
          active: false,
          title: data,
          expression: { group: data }
        });
      });
    }
  }

  function multifilter() {
    return function (items, options) {
      var activeFilter = _.where(options, { active: true });

      var resultItems = [];

      if (activeFilter.length > 0) {
        _.each(activeFilter, function (filter) {
          resultItems = resultItems.concat(_.where(items, filter.expression))
        });
      } else {
        resultItems = items;
      }

      return _.uniq(resultItems);
    }
  }
})();
(function () {
  'use strict';

  angular
    .module('app')
    .controller('TermsController', TermsController);

  TermsController.$inject = ['$stateParams', '$mdDialog', '$mdToast', 'Restangular', 'appConfig'];

  function TermsController($stateParams, $mdDialog, $mdToast, Restangular, appConfig) {
    var vm = this;

    vm.termine = [];
    vm.loading = false;
    vm.save = save;
    vm.managerLink = managerLink;
    vm.appConfig = appConfig;

    activate();

    function activate() {
      loadTerms()
    }

    function loadTerms() {
      vm.loading = true;

      Restangular.one('settings/displays', $stateParams.id).getList('termine').then(function (termine) {
        vm.termine = termine;
        vm.loading = false;
      }, function (error) {
        vm.loading = false;
        $mdDialog.show(
          $mdDialog.alert()
            .parent(angular.element(document.body))
            .title('Fehler')
            .content('Beim abrufen der Daten ist ein Fehler aufgetreten.')
            .ariaLabel('Fehler Dialog')
            .ok('OK')
        );
      });
    }

    function save() {
      vm.termine.forEach(function (termin) {
        Restangular.one('daten/verfahren', termin.id).customPUT(termin).then(function () {
          //TODO: erfolgreich
        }, function(error) {
          //TODO: Fehler melden
        })
      });

      $mdToast.show($mdToast.simple()
        .content('Gespeichert!')
        .position('top left')
        .hideDelay(4000)
      );
    }

    function managerLink(termid) {
      return appConfig.termDetailsUrl + termid;
    }
  }
})();