(function () {
  var app = angular.module('app.terms');

  app.controller('TermsController', function ($scope, $stateParams, $filter, Restangular) {
    var baseTerms = Restangular.all('daten/verfahren');
    var defaultSort = [
        { field: 'uhrzeitAktuell', sort: 'asc' }
    ];

    var columnDefs = [
        { headerName: 'Plan', headerGroup: 'Uhrzeit', width: 80, suppressSizeToFit: true, field: 'uhrzeitPlan' },
        { headerName: 'Aktuell', headerGroup: 'Uhrzeit', width: 80, suppressSizeToFit: true, field: 'uhrzeitAktuell' },
        { headerName: 'Aktenzeichen', width: 150, suppressSizeToFit: true, field: 'az' },
        { headerName: 'Status', width: 150, suppressSizeToFit: true, field: 'status' },
        { headerName: 'Aktiv', headerGroup: 'Parteien', suppressSorting: true, suppressMenu: true, template: '<span ng-repeat="partei in data.parteienAktiv">{{partei}}</span>' },
        { headerName: 'Passiv', headerGroup: 'Parteien', suppressSorting: true, suppressMenu: true, template: '<span ng-repeat="partei in data.parteienPassiv">{{partei}}</span>' },
        { headerName: 'Datum', width: 100, suppressSizeToFit: true, field: 'datum' },
        { headerName: '', width: 100, suppressSizeToFit: true, suppressSorting: true, suppressMenu: true, template: '<a ui-sref="term({id:data.id})">Bearbeiten</a>' }
    ];

    $scope.gridOptions = {
      angularCompileRows: true,
      enableSorting: true,
      enableFilter: true,
      columnDefs: columnDefs,
      rowData: null,
      groupHeaders: true,
      groupKeys: ['sitzungssaal'],
      groupUseEntireRow: true,
      ready: function (api) {
        api.sizeColumnsToFit();
        api.setSortModel(defaultSort);
      }
    };

    baseTerms.getList().then(function (data) {
      $scope.gridOptions.rowData = data;
      $scope.gridOptions.api.onNewRows();
    });
  });

  app.controller('TermDetailsController', function ($scope, $state, $stateParams, $mdToast, $mdDialog, Restangular) {
    var baseTerm = Restangular.one('daten/verfahren', $stateParams.id);

    loadTerm = function () {
      baseTerm.get().then(function (term) {
        $scope.term = term;
      });
    };

    $scope.newRichter = function () {
      $scope.term.besetzung.push('');
    };

    $scope.newAktivPartei = function () {
      $scope.term.parteienAktiv.push('');
    };

    $scope.newProzBevAktiv = function () {
      $scope.term.prozBevAktiv.push('');
    };

    $scope.newPassivPartei = function () {
      $scope.term.parteienPassiv.push('');
    };

    $scope.newProzBevPassiv = function () {
      $scope.term.prozBevPassiv.push('');
    };

    $scope.newBeigeladenPartei = function () {
      $scope.term.parteienBeigeladen.push('');
    };

    $scope.newProzBevBeigeladen = function () {
      $scope.term.prozBevBeigeladen.push('');
    };

    $scope.newSvPartei = function () {
      $scope.term.parteienSv.push('');
    };

    $scope.newZeugenPartei = function () {
      $scope.term.parteienZeugen.push('');
    };

    $scope.reset = function () {
      loadTerm();
    };

    $scope.save = function () {
      $scope.term.put().then(function () {
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

    $scope.delete = function () {
      var confirm = $mdDialog.confirm()
          .parent(angular.element(document.body))
          .title('Wirklich löschen?')
          .content('Soll dieses Verfahren wirklich gelöscht werden?')
          .ariaLabel('Löschen Dialog')
          .ok('Ja')
          .cancel('Nein');
      $mdDialog.show(confirm).then(function () {
        $scope.term.remove().then(function () {
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

    loadTerm();
  });

})();