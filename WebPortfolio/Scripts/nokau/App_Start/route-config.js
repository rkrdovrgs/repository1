var RouteConfig = function () {

    _RegisterRoutes = function ($routeProvider, $nameSpace) {
        var suffix = ConfigurationManager.AppSettings.templateSuffix;

        console.log($routeProvider);

        //#region Default route

        /// <summary>Neither the controller, nor the action, nor the id</summary>
        $routeProvider.when("/", {
            controller: "ProfileController.Index",
            templateUrl: "/Views/Profile/_Index" + suffix
        });

        $.each($nameSpace.Controllers, function (className, actions) {


            //console.log(className, actions);
            var controllerName = className.replace('Controller', '');

           
            $.each(actions, function (actionName, value) {
                //console.log(actionName, value);

                /// <summary>Controller, action, and id</summary>
                $routeProvider.when("/" + controllerName + "/" + actionName + "/:id",
                    {
                        controller: className + "." + actionName,
                        templateUrl: "/Views/" + controllerName + "/_" + actionName + suffix
                    });

                /// <summary>Controller and action</summary>
                $routeProvider.when("/" + controllerName + "/" + actionName,
                    {
                        controller: className + "." + actionName,
                        templateUrl: "/Views/" + controllerName + "/_" + actionName + suffix
                    });

                

            });


            /// <summary>Only controller</summary>
            var defaultAction = 'Index';
            if (actions[defaultAction] != undefined)
                $routeProvider.when("/" + controllerName,
                        {
                            controller: className + "." + defaultAction,
                            templateUrl: "/Views/" + controllerName + "/_" + defaultAction + suffix
                        });

            /// <summary>Controller and id</summary>
            var defaultActionWithId = 'Details';
            if (actions[defaultActionWithId] != undefined)
                $routeProvider.when("/" + controllerName + "/:id",
                        {
                            controller: className + "." + defaultActionWithId,
                            templateUrl: "/Views/" + controllerName + "/_" + defaultActionWithId + suffix
                        });

        });
        //#endregion 



        $routeProvider.otherwise({ redirectTo: "/" });
    };

    return {
        RegisterRoutes: _RegisterRoutes
    };

}();