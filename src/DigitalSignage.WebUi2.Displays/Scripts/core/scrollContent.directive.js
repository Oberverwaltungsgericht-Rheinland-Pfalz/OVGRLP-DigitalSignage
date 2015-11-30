(function () {
  'use strict';

  angular
    .module('app.core')
    .directive('dsScrollContent', dsScrollContent);

  dsScrollContent.$inject = ['$interval', '$document'];

  function dsScrollContent($interval, $document) {
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

    function link(scope, element) {

      console.log(element[0]);
      console.log(element[0].prop('offsetHeight'));

      /*
      var timeoutId;

      scope.$watch('watchData', function (newValue, oldValue) {
        var parent = element.parent();
        var child = element.children();

        console.log(parent.prop('offsetHeight'));

        console.log(child.prop('offsetHeight'));
       
        if (parent[0].offsetHeight < child[0].offsetHeight) {
          if (angular.isDefined(timeoutId)) {
            return;
          };

          timeoutId = $interval(function () {
            console.log('step');
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
      */
    }
  };
})();