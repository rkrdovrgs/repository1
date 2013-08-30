var Controller = function (namespace, controllerName, controller) {

    if (namespace.Controllers == undefined)
        namespace.Controllers = {};



    $.each(controller, function (actionName, action) {
        
        
        var parmNames = action.GetParamNames();
        var actionParms = new Array();

        controller[actionName] = function ($scope, $routeParams, dataservice, $rootScope, $location, $q) {
            $context = {
                $scope: $scope,
                $routeParams: $routeParams,
                dataservice: dataservice,
                $rootScope: $rootScope,
                $location: $location,
                $q: $q
            };

            $rootScope.isReady = false;

            //console.log(arguments);
            
            
            $.each(parmNames, function (idx, pName) {
                actionParms[idx] = $context[pName];
            });

            var result = action.apply(action, actionParms);

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


