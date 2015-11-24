﻿(function () {
  'use strict';

  angular
    .module('ds-infoscreen')
    .filter('multifilter', multifilter);

  function multifilter() {
    return function (items, options) {
      var activeFilter = _.where(options, { active: true });

      var resultItems = [];

      if (activeFilter.length > 0) {
        _.each(activeFilter, function (filter) {
          resultItems = resultItems.concat(_.where(items, filter.expression))
        });
      } else {
        resultItems = items;
      }

      return _.uniq(resultItems);
    }
  }
})();