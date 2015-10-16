(function () {
  var app = angular.module('app.displays');

  app.factory('Displays', function (Restangular) {
    Restangular.extendModel('settings/displays', function (model) {
      model.status = 0;

      model.updateStatus = function () {
        model.status = -1;
        model.customGET('status').then(function (data) {
          model.status = data.result;
        });
      };

      return model;
    });

    return Restangular.service('settings/displays');
  });
})();