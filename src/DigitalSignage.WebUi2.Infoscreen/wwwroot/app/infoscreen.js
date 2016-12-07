/*! DigitalSignage.WebUi2.Infoscreen - v2.2.0-1649 - 07.12.2016 */
(function () {
  'use strict';

  angular
    .module('ds-infoscreen', [
      'ngMaterial',
      'breeze.angular'
  ]);

})();
(function () {
  'use strict';

  angular
    .module('ds-infoscreen')
    .controller('InfoscreenController', InfoscreenController);

  InfoscreenController.$inject = ['termsDataService'];

  function InfoscreenController(termsDataService) {
    var vm = this;

    vm.title = 'Digital Signage - Infoscreen';
    vm.terms = [];
    vm.filters = [];
    vm.filters.gericht = [];

    vm.filters.status = [
      {
        active: false,
        title: 'Läuft',
        expression: { Status: 'Läuft' }
      }, {
        active: false,
        title: 'Abgeschlossen',
        expression: { Status: 'Abgeschlossen' }
      }, {
        active: false,
        title: 'Verschoben',
        expression: { Status: 'Verschoben' }
      }, {
        active: false,
        title: 'Aufgeboben',
        expression: { Status: 'Aufgehoben' }
      }
    ];

    vm.updateData = updateData;

    activate();

    function activate() {
      updateData();
    };

    function updateData() {
      termsDataService.getVerfahrenList().then(function (data) {
        vm.terms = data.results;
        loadGerichteFilters();
      });
    };

    function loadGerichteFilters() {
      var gerichte = [];

      vm.terms.forEach(function (term) {
        gerichte.push(term.Gericht);
      });

      _.uniq(gerichte).forEach(function (data) {
        vm.filters.gericht.push({
          active: false,
          title: data,
          expression: { Gericht: data }
        });
      });
    };
  }
})();
(function () {
  'use strict';

  angular
    .module('ds-infoscreen')
    .filter('multifilter', multifilter);

  function multifilter() {
    return function (items, options) {
      var activeFilter = _.filter(options, { active: true });

      var resultItems = [];

      if (activeFilter.length > 0) {
        _.each(activeFilter, function (filter) {
          resultItems = resultItems.concat(_.filter(items, filter.expression))
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
    .module('ds-infoscreen')
    .factory('termsDataService', termsDataService);

  termsDataService.$inject = ['$q', 'breeze'];

  function termsDataService($q, breeze) {
    //breeze.NamingConvention.camelCase.setAsDefault();

    var serviceName = 'http://localhost:52208/breeze/EurekaDaten';
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
        .expand('Besetzung, ParteienAktiv, ProzBevAktiv, ParteienPassiv, ProzBevPassiv, ParteienBeigeladen, ProzBevBeigeladen, ParteienZeugen, ParteienSV');

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