(function () {
  'use strict';

  angular
    .module('app.core')
    .factory('settingsDataService', settingsDataService);

  settingsDataService.$inject = ['$q', '$http','breeze'];

  function settingsDataService($q, $http, breeze) {
    //breeze.NamingConvention.camelCase.setAsDefault();

    var serviceName = 'http://localhost:52208/breeze/EurekaDaten';
    var manager = new breeze.EntityManager(serviceName);
    var store = manager.metadataStore;

    store.registerEntityTypeCtor('Display', Display);

    var service = {
      getDisplayList: getDisplayList,
      metaDataFetched : false
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

    function Display() {
      var display = this;

      display.Status = -1;
      display.update = update;

      function update() {
        $http.get(serviceName + '/Display/' + display.Id + '/Status')
          .then(function (data) {
            display.Status = data.data;
          }, function (err) {
            display.Status = -1;
          })
      };
    }

  }
})();