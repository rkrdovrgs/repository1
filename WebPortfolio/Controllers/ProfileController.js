var ProfileController = new Controller(WebPortfolio, "ProfileController",
    function () {

        //var userprofilerepository = Repository('UserProfile');


        var _Index = function () {


        };


        var _Details = function ($scope, $wprepository) {
            //$rootScope.isReady = false;

            $scope.userProfile = {};

            return $wprepository
                .UserProfile
                .Details($scope.userProfile);
                //.FindOne($context.$scope.userProfile, null, 'details');

        };


        var _Edit = function ($scope, $wprepository, $routeParams, $location) {


            $scope.userProfile = {};



            $scope.updateProfile = function () {
                $wprepository
                    .UserProfile
                    .Update($scope.userProfile, $routeParams.id)
                    .then(function () {
                        $location.path('/Profile/' + $scope.userProfile.UserId);
                    });
            };


            return $wprepository
                .UserProfile
                .Details($scope.userProfile);
        };



        return {
            Index: _Index,
            Details: _Details,
            Edit: _Edit
        };

    }()
);



