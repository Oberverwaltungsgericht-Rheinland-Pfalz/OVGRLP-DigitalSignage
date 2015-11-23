(function () {
  'use strict';

  angular
    .module('app')
    .controller('TermsController', TermsController);

  TermsController.$inject = ['$stateParams', '$mdDialog', '$mdToast', 'Restangular'];

  function TermsController($stateParams, $mdDialog, $mdToast, Restangular) {
    var vm = this;

    vm.termine = [];
    vm.loading = false;
    vm.save = save;

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
  }
})();