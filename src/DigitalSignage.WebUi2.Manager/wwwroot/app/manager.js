/*! DigitalSignage.WebUi2.Manager - v2.1.0 - 25.11.2015 */
(function () {
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
(function () {
  'use strict';

  angular
    .module('app.core', [
      'breeze.angular'
    ]);
})();
(function () {
  'use strict';

  angular
    .module('app.displays', []);
})();
(function () {
  'use strict';

  angular
    .module('app.terms', []);
})();
(function () {
  'use strict';

  angular
    .module('app.core')
    .factory('settingsDataService', settingsDataService);

  settingsDataService.$inject = ['$q', '$http', 'breeze', 'appConfig'];

  function settingsDataService($q, $http, breeze, appConfig) {
    var serviceName = appConfig.apiUrl + '/breeze/EurekaDaten';
    var manager = new breeze.EntityManager(serviceName);
    var store = manager.metadataStore;

    store.registerEntityTypeCtor('Display', Display);

    var service = {
      getDisplayList: getDisplayList,
      getDisplay: getDisplay,
      metaDataFetched: false
    };

    return service;

    function getDisplayList() {
      var query = breeze.EntityQuery
        .from('Displays');

      var promise = manager.executeQuery(query)
        .catch(function (err) {
          console.log(err);
        }).finally(function () {
          service.metaDataFetched = true;
        });

      return promise;
    }

    function getDisplay(id) {
      var query = breeze.EntityQuery
        .from('Displays')
        .where('Id', '==', id);

      var promise = manager.executeQuery(query)
        .catch(function (err) {
          console.log(err);
        }).finally(function () {
          service.metaDataFetched = true;
        });

      return promise;
    }

    function Display() {
      var display = this;

      display.Status = -1;
      display.Screenshot = "";
      display.poweron = poweron;
      display.update = update;
      display.restart = restart;
      display.shutdown = shutdown;

      function update() {
        $http.get(serviceName + '/Display/' + display.Id + '/status')
          .then(function (data) {
            display.Status = data.data;
          }, function (err) {
            display.Status = -1;
          });
        display.Screenshot = display.ControlUrl + '/api/screenshot?dt=' + new Date().getTime();
      };

      function shutdown() {
        if (display.ControlUrl) {
          $http.get(display.ControlUrl + '/api/shutdown');
        }
      };

      function restart() {
        if (display.ControlUrl) {
          $http.get(display.ControlUrl + '/api/restart');
        }
      };

      function poweron() {
        return $http.get(serviceName + '/Display/' + display.Id + '/poweron');
      }
    }

  }
})();
(function () {
  'use strict';

  angular
    .module('app.core')
    .factory('termsDataService', termsDataService);

  termsDataService.$inject = ['$q', 'breeze', 'appConfig'];

  function termsDataService($q, breeze, appConfig) {
    //breeze.NamingConvention.camelCase.setAsDefault();

    var serviceName = appConfig.apiUrl + '/breeze/EurekaDaten';
    var manager = new breeze.EntityManager(serviceName);

    var service = {
      getVerfahrenList : getVerfahrenList,
      getVerfahren : getVerfahren,
      saveChanges : saveChanges,
      rejectChanges : rejectChanges,
      hasChanges: hasChanges,
      createNewEntity: createNewEntity,
      metaDataFetched : false
    };

    return service;

    function getVerfahrenList() {
      var query = breeze.EntityQuery
        .from('VerfahrenList');

      var promise = manager.executeQuery(query)
        .catch(function (err) {
          console.log(err);
        }).finally(function () {
          service.metaDataFetched = true;
        });

      return promise;
    }

    function getVerfahren(id) {
      var query = breeze.EntityQuery
        .from('Verfahren')
        .where('VerfahrensId', '==', id)
        .expand('Stammdaten, Besetzung, ParteienAktiv, ProzBevAktiv, ParteienPassiv, ProzBevPassiv, ParteienBeigeladen, ProzBevBeigeladen, ParteienZeugen, ParteienSV');

      var promise = manager.executeQuery(query)
        .catch(function (err) {
          console.log(err);
        }).finally(function () {
          service.metaDataFetched = true;
        });

      return promise;
    }

    function saveChanges() {
      return manager.saveChanges().finally(function () {
          service.metaDataFetched = true;
        });
    }

    function rejectChanges() {
      return manager.rejectChanges();
    }

    function hasChanges() {
      return manager.hasChanges();
    }

    function createNewEntity(entityName) {
      return manager.createEntity(entityName);
    }
  }
})();
(function () {
  'use strict';

  angular
    .module('app.displays')
    .controller('DisplayController', DisplayController);

  DisplayController.$inject = ['$stateParams', '$http', 'settingsDataService', 'appConfig'];

  function DisplayController($stateParams, $http, settingsDataService, appConfig) {
    var vm = this;

    vm.display = [];
    vm.loading = false;

    vm.poweron = poweron;
    vm.restart = restart;
    vm.shutdown = shutdown;
    vm.refresh = refresh;
    vm.appConfig = appConfig;

    activate();

    function activate() {
      settingsDataService.getDisplay($stateParams.id)
        .then(function (data) {
          vm.display = data.results[0];
          vm.display.update();
        });
    };

    function poweron() {
      if (vm.display) {
        vm.display.poweron()
          .then(function (data) {
            console.log(data);
          }, function (err) {
            console.log(err);
          });
      }
    };

    function restart() {
      if (vm.display) {
        vm.display.restart();
      }
    };

    function shutdown() {
      if (vm.display) {
        vm.display.shutdown();
      }
    };

    function refresh() {
      if (vm.display) {
        vm.display.update();
      };
    };
  }

})();
(function () {
  'use strict';

  angular
    .module('app.displays')
    .controller('DisplaysController', DisplaysController);

  DisplaysController.$inject = ['settingsDataService'];

  function DisplaysController(settingsDataService) {
    var vm = this;

    var columnDefs = [
      { headerName: '', width: 30, suppressSizeToFit: true, template: '<img ng-src="{{vm.getStateImg(data.Status)}}" alt="{{vm.getStateText(data.Status)}}"></img>' },
      { headerName: 'Name', template: '<a ui-sref="display({id:data.Id})">{{data.Name}}</a>' },
      { headerName: 'Titel', field: 'Title' },
      { headerName: '', width: 110, suppressSizeToFit: true, template: '<a href="" ng-click="data.update()">Aktualisieren</a>'}
    ];

    vm.getStateText = getStateText;
    vm.getStateImg = getStateImg;
    vm.gridOptions = {
      angularCompileRows: true,
      columnDefs: columnDefs,
      rowData: null,
      groupKeys: ['Group'],
      groupUseEntireRow: true,
      onReady: function (params) {
        params.api.sizeColumnsToFit();
      }
    };

    activate();

    function activate() {
      settingsDataService.getDisplayList().then(function (data) {
        vm.gridOptions.api.setRowData(data.results);
        vm.gridOptions.rowData.forEach(function (display) {
          display.update();
        });
      });
    }

    function getStateText(id) {
      if (id == -1) {
        return "undefiniert";
      } else if (id == 0) {
        return "ausgeschaltet";
      } else if (id == 1) {
        return "angeschaltet";
      } else {
        return "unbekannt";
      }
    };

    function getStateImg(id) {
      if (id == -1) {
        return "img/display-undefined-icon.png";
      } else if (id == 0) {
        return "img/display-offline-icon.png";
      } else if (id == 1) {
        return "img/display-online-icon.png";
      } else {
        return "img/display-unknown-icon.png";
      }
    };
  }
})();
(function () {
  'use strict';

  angular
    .module('app.terms')
    .directive('dsParteien', dsParteien);

  function dsParteien() {
    var directive = {
      restrict: 'E',
      templateUrl: 'app/terms/parteien.directive.html',
      scope: {
        items: '=',
        title: '@',
        entityName: '@'
      },
      controller: ['termsDataService', ParteienController],
      controllerAs: 'vm',
      bindToController: true
    };
    
    return directive;

    function ParteienController(termsDataService) {
      var vm = this;

      vm.addNewItem = addNewItem;
      vm.deleteItem = deleteItem;

      function addNewItem() {
        vm.items.push(termsDataService.createNewEntity(vm.entityName));
      }

      function deleteItem(item) {
        var i = vm.items.indexOf(item);
        item.entityAspect.setDeleted();
        vm.items.splice(i, 1);
      }
    }
  }
})();
(function () {
  'use strict';

  angular
    .module('app.terms')
    .directive('dsProzbev', dsProzbev);

  function dsProzbev() {
    var directive = {
      restrict: 'E',
      templateUrl: 'app/terms/prozbev.directive.html',
      scope: {
        items: '=',
        title: '@',
        entityName: '@'
      },
      controller: ['termsDataService', ProzbevController],
      controllerAs: 'vm',
      bindToController: true
    };
    
    return directive;

    function ProzbevController(termsDataService) {
      var vm = this;

      vm.addNewItem = addNewItem;
      vm.deleteItem = deleteItem;

      function addNewItem() {
        vm.items.push(termsDataService.createNewEntity(vm.entityName));
      }

      function deleteItem(item) {
        var i = vm.items.indexOf(item);
        item.entityAspect.setDeleted();
        vm.items.splice(i, 1);
      }
    }
  }
})();
(function () {
  'use strict';

  angular
    .module('app.terms')
    .directive('dsRichter', dsRichter);

  function dsRichter() {
    var directive = {
      restrict: 'E',
      templateUrl: 'app/terms/richter.directive.html',
      scope: {
        items: '=',
        title: '@',
        entityName: '@'
      },
      controller: ['termsDataService', RichterController],
      controllerAs: 'vm',
      bindToController: true
    };
    
    return directive;

    function RichterController(termsDataService) {
      var vm = this;

      vm.addNewItem = addNewItem;
      vm.deleteItem = deleteItem;

      function addNewItem() {
        vm.items.push(termsDataService.createNewEntity(vm.entityName));
      }

      function deleteItem(item) {
        var i = vm.items.indexOf(item);
        item.entityAspect.setDeleted();
        vm.items.splice(i, 1);
      }
    }
  }
})();
(function () {
  'use strict';

  angular
    .module('app.terms')
    .controller('TermController', TermController);

  TermController.$inject = ['$state', '$stateParams', '$mdToast', '$mdDialog', 'termsDataService', 'appConfig'];

  function TermController($state, $stateParams, $mdToast, $mdDialog, termsDataService, appConfig) {
    var vm = this;

    vm.reset = reset;
    vm.save = save;
    vm.delete = deleteTerm;
    vm.appConfig = appConfig;

    activate();

    function activate() {
      loadTerm();
    };

    function loadTerm() {
      termsDataService.getVerfahren($stateParams.id).then(function (term) {
        vm.term = term.results[0];
      });
    };

    function reset() {
      termsDataService.rejectChanges();
    };

    function save() {
      termsDataService.saveChanges()
        .catch(function (err) {
          $mdDialog.show(
            $mdDialog.alert()
              .parent(angular.element(document.body))
              .title('Fehler')
              .content('Beim Speichern der Daten ist ein Fehler aufgetreten.')
              .ariaLabel('Fehler Dialog')
              .ok('OK')
          );
        }).then(function () {
          $mdToast.show($mdToast.simple()
            .parent(angular.element(document.body))
            .content('Gespeichert!')
            .position('bottom right')
            .hideDelay(4000)
          );
        });
    };

    function deleteTerm() {
      var confirm = $mdDialog.confirm()
        .parent(angular.element(document.body))
        .title('Wirklich löschen?')
        .content('Soll dieses Verfahren wirklich gelöscht werden?')
        .ariaLabel('Löschen Dialog')
        .ok('Ja')
        .cancel('Nein');

      $mdDialog.show(confirm).then(function () {
        vm.term.remove().then(function () {
          $state.go('terms');
        }, function (error) {
          $mdDialog.show(
            $mdDialog.alert()
              .parent(angular.element(document.body))
              .title('Fehler')
              .content('Beim Löschen des Verfahrens ist ein Fehler aufgetreten.')
              .ariaLabel('Fehler Dialog')
              .ok('OK')
           );
        });
      }, function () {
      });
    };
  };
})();
(function () {
  'use strict';

  angular
    .module('app.terms')
    .controller('TermsController', TermsController);

  TermsController.$inject = ['$stateParams', '$filter', 'termsDataService'];

  function TermsController($stateParams, $filter, termsDataService) {
    var vm = this;

    var defaultSort = [
      { field: 'uhrzeitAktuell', sort: 'asc' }
    ];

    var columnDefs = [
      { headerName: 'Plan', headerGroup: 'Uhrzeit', width: 80, suppressSizeToFit: true, field: 'UhrzeitPlan' },
      { headerName: 'Aktuell', headerGroup: 'Uhrzeit', width: 80, suppressSizeToFit: true, field: 'UhrzeitAktuell' },
      { headerName: 'Aktenzeichen', width: 150, suppressSizeToFit: true, template: '<a ui-sref="term({id:data.VerfahrensId})">{{data.Az}}</a>' },
      { headerName: 'Status', width: 150, suppressSizeToFit: true, field: 'Status' },
      { headerName: 'Aktiv', headerGroup: 'Parteien', suppressSorting: true, suppressMenu: true, template: '<span ng-repeat="item in data.ParteienAktiv">{{item.Partei}}<span ng-hide="$last">; </span></span>' },
      { headerName: 'Passiv', headerGroup: 'Parteien', suppressSorting: true, suppressMenu: true, template: '<span ng-repeat="item in data.ParteienPassiv">{{item.Partei}}<span ng-hide="$last">; </span></span>' },
      { headerName: 'Datum', width: 100, field: 'Datum' }
    ];

    vm.gridOptions = {
      angularCompileRows: true,
      enableSorting: true,
      enableFilter: true,
      columnDefs: columnDefs,
      rowData: null,
      groupHeaders: true,
      groupKeys: ['Gericht', 'Sitzungssaal'],
      groupUseEntireRow: true,
      groupDefaultExpanded: 1,
      onReady: function (params) {
        params.api.sizeColumnsToFit();
        params.api.setSortModel(defaultSort);
      }
    };

    activate();

    function activate() {
      termsDataService.getVerfahrenList().then(function (data) {
        vm.gridOptions.api.setRowData(data.results);
      })
    };

  };

})();