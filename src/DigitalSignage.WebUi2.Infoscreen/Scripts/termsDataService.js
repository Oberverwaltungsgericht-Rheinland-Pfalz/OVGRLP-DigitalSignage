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