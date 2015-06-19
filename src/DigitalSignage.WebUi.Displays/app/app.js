(function () {
    var app = angular.module('app', [
        //Angular
        'ngAnimate',
        // 3rd Party Modules
        'ui.router',
        'restangular'
    ]);

    angular.module('app.data', []);

    app.filter('capitalize', function () {
        return function (input, all) {
            return (!!input) ? input.replace(/([^\W_]+[^\s-]*) */g, function (txt) {
                return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
            }) : '';
      }
  });

    app.config(function ($stateProvider, $urlRouterProvider, RestangularProvider) {
        $stateProvider
            .state('index', {
                url: '/',
                templateUrl: 'app/index.html'
            }).state('content', {
                url: '/{id}',
                templateUrl: 'app/display.html',
                controller: 'DisplayController'
            });

        $urlRouterProvider.otherwise('/');

        RestangularProvider.setBaseUrl('http://localhost:52208');
    });

    app.controller(
        'DisplayController',
        ['$scope', '$stateParams', '$interval', 'Restangular',
            function ($scope, $stateParams, $interval, Restangular) {
                var DisplaysSrv = Restangular.service('settings/displays');

                $scope.display;
                $scope.dateTime = new Date();

                loadDisplay = function () {
                    DisplaysSrv.one($stateParams.id).get().then(function (display) {
                        $scope.display = display;
                        $scope.layout = 'app/templates/' + display.template + '/main.html';

                        $scope.$parent.loadStyles($scope.display.styles);
                    });
                };

                $interval(function () {
                    $scope.dateTime = new Date();
                }, 1000 * 60);

                loadDisplay();

                $interval(function () {
                    loadDisplay();
                }, 1000 * 60 * 10);
            }]);

    app.controller(
        'TermineController',
        ['$scope', '$stateParams', '$timeout', '$interval', '$filter', 'Restangular',
            function ($scope, $stateParams, $timeout, $interval, $filter, Restangular) {
                var TermineSrv = Restangular.service('termine', Restangular.one('settings/displays', $stateParams.id));

                var stock = [];
                var scrollMode = false;
                var scrollOffset = 0;
                
                $scope.termine = [];
                $scope.detailTermine = [];

                $scope.maxItems = 9;
                $scope.updateInterval = 15 * 1000;
                $scope.stepDelay = 3 * 1000;
                $scope.stepDetailTermine = 10 * 1000;

                $scope.isActiveTermin = function (termin) {
                    return ($scope.detailTermine[$scope.currentTermin] == termin);
                };

                initialize = function () {
                    updateData();

                    $interval(function () {
                        updateData();
                    }, $scope.updateInterval);

                    $interval(function () {
                        if ($scope.detailTermine.length > 1) {
                            var term = angular.copy($scope.detailTermine.shift());
                            $scope.detailTermine.push(term);
                        }
                    }, $scope.stepDetailTermine);

                    $interval(function () {
                        if (scrollMode) {
                            doStep();
                        }
                    }, $scope.stepDelay);
                };

                updateData = function () {
                    TermineSrv.getList().then(function (data) {
                        var tmpData = $filter('orderBy')(data, '+uhrzeitAktuell');
                        if (tmpData.length > $scope.maxItems) {
                            scrollMode = true;

                            var dupData = tmpData.concat(tmpData);
                            offset = 0;

                            if ($scope.termine.length > 0) {
                                var offset = dupData.indexOf(_.find(dupData, function (item) {
                                    return item.id == $scope.termine[0].id;
                                }));
                            }

                            $scope.termine = dupData.slice(offset, offset + $scope.maxItems);
                            stock = dupData.slice(offset + $scope.maxItems).concat(dupData.slice(0));
                        } else {
                            $scope.termine = tmpData;

                            scrollMode = false;
                            scrollOffset = 0;
                            stock = [];
                        }

                        updateDetailTermine(tmpData);
                    });
                };

                updateDetailTermine = function (termine) {
                    var tmpData = _.filter(termine, function(termin) {
                        return termin.status == 'Läuft';
                    });

                    if (tmpData.length <= 1) {
                        tmpData.push(_.find(termine, function (termin) {
                            return (termin.status != 'Abgeschlossen' && termin.status != 'Aufgehoben');
                        }));
                    }

                    if (tmpData.length > 1) {
                        if ($scope.detailTermine[0]) {
                            var currentIndex = tmpData.indexOf(_.find(tmpData, function (item) {
                                return item.id == $scope.detailTermine[0].id;
                            }));

                            if (currentIndex > -1) {
                                tmpData = tmpData.slice(currentIndex).concat(tmpData.slice(0, currentIndex));
                            }
                        }
                    }

                    $scope.detailTermine = tmpData;
                    console.log($scope.detailTermine);
                };

                arrayToString = function (data) {
                    var outStr = '';
                    data.forEach(function (item) {
                        outStr = outStr + item.uhrzeitAktuell + ' ';
                    });
                    return outStr;
                };

                doStep = function () {
                    $scope.termine.shift();
                    $scope.termine.push(stock.shift());
                };

                initialize();

                $scope.init = function (maxItems, stepDelay, updateInterval, stepDetailTermine) {
                    $scope.maxItems = maxItems;
                    $scope.stepDelay = stepDelay;
                    $scope.updateInterval = updateInterval;
                    $scope.stepDetailTermine = stepDetailTermine;
                }
            }]);

    app.controller(
        'MainController',
        ['$scope',
            function ($scope) {
                $scope.styles = [];

                $scope.loadStyles = function (styles) {
                    $scope.styles = [];
                    $scope.styles.push('default');
                    styles.split(',').forEach(function (style) {
                        $scope.styles.push(style.trim());
                    });
                };
            }]);
})();