(function () {
  'use strict';

  angular
    .module('app.terms')
    .controller('TermController', TermController);

  TermController.$inject = ['$state', '$stateParams', '$mdToast', '$mdDialog', 'Restangular'];

  function TermController($state, $stateParams, $mdToast, $mdDialog, Restangular) {
    var vm = this;

    var baseTerm = Restangular.one('daten/verfahren', $stateParams.id);

    vm.newRichter = newRichter;
    vm.newAktivPartei = newAktivPartei;
    vm.newProzBevAktiv = newProzBevAktiv;
    vm.newPassivPartei = newPassivPartei;
    vm.newProzBevPassiv = newProzBevPassiv;
    vm.newBeigeladenPartei = newBeigeladenPartei;
    vm.newProzBevBeigeladen = newProzBevBeigeladen;
    vm.newSvPartei = newSvPartei;
    vm.newZeugenPartei = newZeugenPartei;
    vm.reset = reset;
    vm.save = save;
    vm.delete = deleteTerm;

    activate();

    function activate() {
      loadTerm();
    };

    function loadTerm() {
      baseTerm.get().then(function (term) {
        vm.term = term;
      });
    };

    function newRichter() {
      vm.term.besetzung.push('');
    };

    function newAktivPartei() {
      vm.term.parteienAktiv.push('');
    };

    function newProzBevAktiv() {
      vm.term.prozBevAktiv.push('');
    };

    function newPassivPartei() {
      vm.term.parteienPassiv.push('');
    };

    function newProzBevPassiv() {
      vm.term.prozBevPassiv.push('');
    };

    function newBeigeladenPartei() {
      vm.term.parteienBeigeladen.push('');
    };

    function newProzBevBeigeladen() {
      vm.term.prozBevBeigeladen.push('');
    };

    function newSvPartei() {
      vm.term.parteienSv.push('');
    };

    function newZeugenPartei() {
      vm.term.parteienZeugen.push('');
    };

    function reset() {
      loadTerm();
    };

    function save() {
      vm.term.put().then(function () {
        $mdToast.show($mdToast.simple()
          .parent(angular.element(document.body))
          .content('Gespeichert!')
          .position('bottom right')
          .hideDelay(4000)
        );
      }, function (error) {
        $mdDialog.show(
          $mdDialog.alert()
            .parent(angular.element(document.body))
            .title('Fehler')
            .content('Beim Speichern der Daten ist ein Fehler aufgetreten.')
            .ariaLabel('Fehler Dialog')
            .ok('OK')
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
        vm.term.remove().then(function () {
          $state.go('terms');
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