var keyValueStoreApp = angular.module('keyValueStoreApp', ["ngRoute"]);

configFunction.$inject = ["$routeProvider", "$httpProvider"];

function configFunction($routeProvider, $httpProvider) {
    $routeProvider.
    when("/Dashboard", {
        templateUrl: "KeyValueStore/Dashboard",
        controller: "KeyValueStoreController"
    })
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=UTF-8';
}

keyValueStoreApp.config(configFunction);