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