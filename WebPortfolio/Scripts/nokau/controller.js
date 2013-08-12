var Controller = function (namespace, controllerName, controller) {

    if (namespace.Controllers == undefined)
        namespace.Controllers = {};



    $.each(controller, function (actionName, action) {
        //console.log(actionName);
        /// <summary>Auto Injection to all controller functions</summary>
        controller[actionName] = function ($scope, $routeParams, $repository, $rootScope, $location) {
            $context = {
                $scope: $scope,
                $routeParams: $routeParams,
                $repository: $repository,
                $rootScope: $rootScope,
                $location: $location
            };

            $rootScope.isReady = false;
            var result = action($context);

            if (result != undefined && result.then)
                return result
                    .then(function () {
                        $rootScope.isReady = true;
                    });


            $rootScope.isReady = true;
            if (result != undefined) 
                return result;


        };



    });

    namespace.Controllers[controllerName] = controller;


    return controller;
};

//Controller.prototype = function () {

//};