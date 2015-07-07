(function () {
    var app = angular.module('app', [
        //Angular
        'ngAnimate',
        'ngMaterial',
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
            }).state('display', {
                url: '/{id}',
                templateUrl: 'app/display.html',
                controller: 'DisplayController'
            });

        $urlRouterProvider.otherwise('/');
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

                $scope.updateInterval = 15 * 1000;
                $scope.termine = [];

                $scope.detailTermin = null;

                initialize = function () {
                    updateData();

                    $interval(function () {
                        updateData();
                    }, $scope.updateInterval);
                };

                updateData = function () {
                    TermineSrv.getList().then(function (data) {
                        var now = moment();
                        
                        $scope.termine = $filter('orderBy')(data, '+uhrzeitAktuell');

                        $scope.termine.forEach(function (term) {
                            var termDat = moment(term.datum + ' ' + term.uhrzeitAktuell, 'DD.MM.YYYY HH:mm');
                            if (termDat.isValid()) {
                                term.beginnt = termDat.diff(now, 'minutes');
                            } else {
                                term.beginnt = -1;
                            };
                        });

                        updateDetailTermin($scope.termine);
                    });
                };

                updateDetailTermin = function (termine) {
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
                        $scope.detailTermin = tmpData[0];
                    } else {
                        $scope.detailTermin = null;
                    }
                };

                arrayToString = function (data) {
                    var outStr = '';
                    data.forEach(function (item) {
                        outStr = outStr + item.uhrzeitAktuell + ' ';
                    });
                    return outStr;
                };

                initialize();
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

    app.directive('dsScrollContent', ['$interval', function ($interval) {
        return {
            restrict: 'E',
            scope: {
                step: '@',
                speed: '@',
                stepDuration: '@',
                watchData: '='
            },
            link: function (scope, element, attrs) {
                var timeoutId;

                scope.$watch('watchData', function (newValue, newValue) {
                    var parent = element.parent();
                    var child = element.children();

                    if (parent[0].offsetHeight < child[0].offsetHeight) {
                        if (angular.isDefined(timeoutId)) {
                            return;
                        };

                        timeoutId = $interval(function () {
                            var mTop = parseFloat(child.css('margin-top'));
                            var newValue = mTop - scope.step;

                            if ((newValue + 2 * scope.step) < (parent[0].offsetHeight - child[0].offsetHeight))
                                newValue = 0;

                            child.animate({ marginTop: newValue }, scope.stepDuration);
                        }, scope.speed);
                    } else {
                        if (angular.isDefined(timeoutId)) {
                            $interval.cancel(timeoutId);
                        };
                        child.animate({ marginTop: 0}, scope.stepDuration);
                    }
                });
            }
        }
    }]);

})();