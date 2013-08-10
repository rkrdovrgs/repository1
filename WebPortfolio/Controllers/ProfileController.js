var ProfileController = new Controller(WebPortfolio, "ProfileController",
    function () {

        var userprofilerepository = IRepository('UserProfile');


        var _Index = function ($context) {


        };




        var _Details = function ($context) {
            //$rootScope.isReady = false;

            $context.$scope.userProfile = {};

            return userprofilerepository()
                .Get($context.$routeParams.id, $context.$scope.userProfile);

        };


        var _Edit = function ($context) {


            $context.$scope.userProfile = {};



            $context.$scope.updateProfile = function () {
                userprofilerepository()
                    .Update($context.$routeParams.id, $context.$scope.userProfile)
                    .then(function () {
                        $context.$location.path('/Profile/' + $context.$scope.userProfile.UserId);
                    });
            };


            return userprofilerepository()
                .Get($context.$routeParams.id, $context.$scope.userProfile);
        };



        return {
            Index: _Index,
            Details: _Details,
            Edit: _Edit
        };

    }()
);



