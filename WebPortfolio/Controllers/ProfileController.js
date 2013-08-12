var ProfileController = new Controller(WebPortfolio, "ProfileController",
    function () {

        //var userprofilerepository = Repository('UserProfile');


        var _Index = function ($context) {


        };


        var _Details = function ($context) {
            //$rootScope.isReady = false;

            $scope.userProfile = {};

            return $wprepository
                .UserProfileRepository
                .Details($scope.userProfile);
                //.FindOne($context.$scope.userProfile, null, 'details');

        };


        var _Edit = function ($context) {


            $context.$scope.userProfile = {};



            $context.$scope.updateProfile = function () {
                userprofilerepository()
                    .Update($context.$scope.userProfile, $context.$routeParams.id)
                    .then(function () {
                        $context.$location.path('/Profile/' + $context.$scope.userProfile.UserId);
                    });
            };


            return userprofilerepository()
                .FindOne($context.$scope.userProfile, null, 'details');
        };



        return {
            Index: _Index,
            Details: _Details,
            Edit: _Edit
        };

    }()
);



