var globalModule = angular.module("global", ['ng']),
    //App main namespace
    WebPortfolio = {},
    IRepository,
    Repository = function (modelName) {
        return function () {
            return IRepository(modelName);
        };
    };



globalModule.config(function ($routeProvider) {

    RouteConfig.RegisterRoutes($routeProvider, WebPortfolio);

});

globalModule.factory('$repository', function ($http, $q) {
    //Custom IoC
    IRepository = $repository($http, $q);
});







