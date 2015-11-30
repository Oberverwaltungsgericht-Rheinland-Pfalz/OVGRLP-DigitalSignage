(function () {
  'use strict';

  angular
    .module('ds-core')
    .directive('dsScrollContent', dsScrollContent);

  dsScrollContent.$inject = ['$window', '$interval'];

  function dsScrollContent($window, $interval) {
    var directive = {
      restrict: 'E',
      transclude: true,
      template: '<div style="margin:0px;padding:0px;width:100%;height:100%"><div class="scroll-inner" ng-transclude></div></div>',
      scope: {
        checkDuration: '@',
        moveStep: '@',
        moveDuration: '@',
        horizontal: '@'
      },
      link: link
    };
    return directive;

    function link(scope, element, attrs) {
      var checkDuration = scope.checkDuration || 1000 * 5;
      var moveStep = scope.moveStep || 20;
      var moveDuration = scope.moveDuration || 600;
      var horizontal = scope.horizontal || false;

      $interval(function () {
        doProcess();
      }, checkDuration);

      function doProcess() {
        var child = element.find('.scroll-inner');

        if (!horizontal) {
          if (element[0].offsetHeight < child[0].scrollHeight) {
            var mTop = parseFloat(child.css('margin-top'));
            var newTop = mTop - moveStep;

            if ((newTop + 2 * moveStep) < (element[0].offsetHeight - child[0].scrollHeight))
              newTop = 0;


            child.animate({ marginTop: newTop }, moveDuration);
          } else {
            child.animate({ marginTop: 0 }, moveDuration);
          }
        } else {
          if (element[0].offsetWidth < child[0].scrollWidth) {
            var mLeft = parseFloat(child.css('margin-left'));
            var newLeft = mLeft - moveStep;

            if ((newLeft + child[0].scrollWidth) <= 0)
              newLeft = 0;

            child.animate({ marginLeft: newLeft }, moveDuration);
          } else {
            child.animate({ marginLeft: 0 }, moveDuration);
          }
        }
      }
    }
  }

})();