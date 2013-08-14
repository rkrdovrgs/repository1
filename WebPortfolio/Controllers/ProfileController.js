var ProfileController = new Controller(WebPortfolio, "ProfileController",
    function () {        


        var _Index = function () {
        };

        var _Details = function ($scope, $wprepository) {
            //$rootScope.isReady = false;

            $scope.userProfile = {};
            $scope.userAddress = {};
            //$scope.userAddress = {};
            
           
            //$('.validation').mouseenter(function () {
            //    if ($(this).val() != "")
            //        $(this).css("background", "#E4ECED");
            //});
            //$('.validation').mouseleave(function () {
            //    if ($(this).val() != "")
            //        $(this).css("background", "none");
            //});

            //$('.validation').keyup(function () {
            //    if ($(this).val() != "")
            //        $(this).css("background", "none");
            //});

            //$(".addme").click(function () {
            //    var txt = document.createElement('input');
            //    txt.setAttribute('class', 'validation');
            //    document.getElementById("divvemail").appendChild(txt);
            //});
            //$(function () {
            //    $("#DOB").datepicker();
            //});
            
            $scope.updateProfile = function () {
                $wprepository
                    .UserProfile
                    .Update($scope.userProfile)
                    .then(function (data) {
                        //$scope.userProfile.UserAddresses[0].UserAddressId = data.userAddressId;
                        
                        //$location.path('/Profile/' + $scope.userProfile.UserId);
                    });  
            };
           
            return $wprepository
                .UserProfile
                .Details($scope.userProfile);           

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



