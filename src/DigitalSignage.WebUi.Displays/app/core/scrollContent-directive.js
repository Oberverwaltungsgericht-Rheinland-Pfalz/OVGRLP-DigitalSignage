(function () {
  'use strict';

  angular
    .module('app.core')
    .directive('dsScrollContent', dsScrollContent);

  dsScrollContent.$inject = ['$interval'];

  function dsScrollContent($interval) {
    var directive = {
      restrict: 'E',
      scope: {
        step: '@',
        speed: '@',
        stepDuration: '@',
        watchData: '='
      },
      link: link
    };
    return directive;

    function link(scope, element, attrs) {
      var timeoutId;

      scope.$watch('watchData', function (newValue, oldValue) {
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
          child.animate({ marginTop: 0 }, scope.stepDuration);
        }
      });
    }
  };
})();