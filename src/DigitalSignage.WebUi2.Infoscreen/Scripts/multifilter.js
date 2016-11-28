(function () {
  'use strict';

  angular
    .module('ds-infoscreen')
    .filter('multifilter', multifilter);

  function multifilter() {
    return function (items, options) {
      var activeFilter = _.filter(options, { active: true });

      var resultItems = [];

      if (activeFilter.length > 0) {
        _.each(activeFilter, function (filter) {
          resultItems = resultItems.concat(_.filter(items, filter.expression))
        });
      } else {
        resultItems = items;
      }

      return _.uniq(resultItems);
    }
  }
})();
