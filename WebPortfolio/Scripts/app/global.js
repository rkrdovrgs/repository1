var globalModule = angular.module("global", ['ngUpload']),
    //App main namespace
    CoreModule = {};



globalModule.config(function ($routeProvider) {

    RouteConfig.RegisterRoutes($routeProvider, CoreModule);

});

globalModule.factory('dataservice', function ($http, $q) {
    //Custom IoC
    var Repository = new $repository($http, $q);

    return new CoreModule.dataservice(Repository);

});







