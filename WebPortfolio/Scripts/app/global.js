var globalModule = angular.module("global", ['ng']),
    //App main namespace
    WebPortfolio = {},
    Repository,
    IRepository = function (modelName) {
        return function () {
            return Repository(modelName);
        };
    };



globalModule.config(function ($routeProvider) {

    RouteConfig.RegisterRoutes($routeProvider, WebPortfolio);

});

globalModule.factory('$repository', function ($http, $q) {
    //Custom IoC
    Repository = $repository($http, $q);
});







