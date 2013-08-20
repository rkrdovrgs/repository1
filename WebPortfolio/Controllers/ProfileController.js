var ProfileController = new Controller(WebPortfolio, "ProfileController",
    function () {


        var _Index = function () {
        };

        var _Details = function ($scope, $wprepository) {
            //$rootScope.isReady = false;

            $scope.userProfile = {};
            $scope.userAddress = {};
            $scope.userPhone = {};
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
            /* <div class="controls" data-ng-repeat="ph in userProfile.UserPhones" >
                    <input type="text" class="validation" data-ng-model="ph.Number" />
                </div>*/
            $scope.soloLetras = function () {
                angular.isString($scope.first)
                
            };
           
            $scope.addPhone = function () {                
                $scope.userProfile.UserPhones.push({ Number: null, UserId: $scope.userProfile.UserId });
                console.log($scope.userProfile.UserPhones);
               // $scope.$apply();
            };
            //$(function () {
            //    $("#DOB").datepicker();
            //});

            $scope.updateProfile = function () {
                $wprepository
                    .UserProfile
                    .Update($scope.userProfile)
                    .then(function (data) {
                        if ($scope.userProfile.UserAddress != null)
                            $scope.userProfile.UserAddress.Id = data.userAddressId;

                        $scope.userProfile.UserPhones = data.userPhones;

                        //$location.path('/Profile/' + $scope.userProfile.UserId);
                    });
            };

            $scope.isNullOrEmpty = function (val) {
                return val == null || val == undefined || val.length == 0;
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



