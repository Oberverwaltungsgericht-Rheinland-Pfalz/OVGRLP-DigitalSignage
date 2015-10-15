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