(function () {
  'use strict';

  angular
    .module('app.core')
    .factory('dataService', dataService);

  dataService.$inject = ['$q', 'breeze'];

  function dataService($q, breeze) {
    //breeze.NamingConvention.camelCase.setAsDefault();

    var serviceName = 'http://localhost:52208/breeze/EurekaDaten';
    var manager = new breeze.EntityManager(serviceName);

    var service = {
      getVerfahren : getVerfahren,
      saveChanges : saveChanges,
      rejectChanges : rejectChanges,
      hasChanges: hasChanges,
      addNewRichter: addNewRichter,
      metaDataFetched : false
    };

    return service;

    function getVerfahren(id) {
      var query = breeze.EntityQuery
        .from('Verfahren')
        .where('VerfahrensId', '==', id)
        .expand('Besetzung');

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

    function addNewRichter(verfahren) {
      var richter = manager.createEntity('Besetzung');
      verfahren.Besetzung.push(richter);
    }
  }
})();