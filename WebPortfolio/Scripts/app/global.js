var globalModule = angular.module("global", ['ng']),
    //App main namespace
    WebPortfolio = {};



globalModule.config(function ($routeProvider) {

    RouteConfig.RegisterRoutes($routeProvider, WebPortfolio);

});

globalModule.factory('$wprepository', function ($http, $q) {
    //Custom IoC
    var Repository = new $repository($http, $q);

    return new WebPortfolioRepository(Repository);

});







