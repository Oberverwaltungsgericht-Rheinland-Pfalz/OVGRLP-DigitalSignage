(function () {
    var app = angular.module('app');

    app.config(function (RestangularProvider) {
        RestangularProvider.setBaseUrl('http://localhost:52208');
    });
})();