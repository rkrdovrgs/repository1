var ProfileController = new Controller(WebPortfolio, "ProfileController",
    function () {

        var userprofilerepository = IRepository('UserProfile');
        

        var _Index = function ($rootScope) {


            $rootScope.isReady = true;

        };




        var _Details = function ($routeParams, $scope, $repository, $rootScope) {
            $rootScope.isReady = false;

            $scope.userProfile = {};
           
            userprofilerepository()
                .Get($routeParams.id, $scope.userProfile)
                .then(function () {
                    $rootScope.isReady = true;
                });
            
        };


        var _Edit = function ($routeParams, $scope, $repository, $rootScope) {
            $rootScope.isReady = false;
            

            $scope.userProfile = {};

            userprofilerepository().Get($routeParams.id, $scope.userProfile);

            $scope.updateProfile = function () {
                userprofilerepository().Update($routeParams.id, $scope.userProfile);
            };
        };



        return {
            Index: _Index,
            Details: _Details,
            Edit: _Edit
        };

    }()
);



