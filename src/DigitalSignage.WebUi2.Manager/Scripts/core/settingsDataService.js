(function () {
  'use strict';

  angular
    .module('app.core')
    .factory('settingsDataService', settingsDataService);

  settingsDataService.$inject = ['$q', '$http', 'breeze'];

  function settingsDataService($q, $http, breeze) {
    var serviceName = 'http://localhost:52208/breeze/EurekaDaten';
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