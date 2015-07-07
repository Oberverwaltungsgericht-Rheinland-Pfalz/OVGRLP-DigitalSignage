(function () {
    var app = angular.module('app');

    app.config(function (RestangularProvider) {
        RestangularProvider.setBaseUrl('http://10.10.79.121:52208');
    });
})();