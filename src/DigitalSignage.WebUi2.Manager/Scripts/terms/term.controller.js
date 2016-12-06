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
        termsDataService.deleteVerfahren(vm.term).then(function () {
          $state.go('main.terms');
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