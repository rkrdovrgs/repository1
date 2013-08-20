﻿var ProfileController = new Controller(WebPortfolio, "ProfileController",
    function () {


        var _Index = function () {
        };

        var _Details = function ($scope, $wprepository) {
            //$rootScope.isReady = false;

            $scope.userProfile = {};
            $scope.userAddress = {};
            $scope.userPhone = {};           
            $scope.countries = [];
                       
           // $('.datepicker').datepicker();

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
            //var app = angular.module('form-example', []);

            
            $scope.addPhone = function () {                
                $scope.userProfile.UserPhones.push({ Number: null, UserId: $scope.userProfile.Id });
                
               // $scope.$apply();
            };

           

            $scope.updateProfile = function () {
                $wprepository
                    .UserProfile
                    .Update($scope.userProfile)
                    .then(function (data) {
                        if ($scope.userProfile.UserAddress != null)                        
                            $scope.userProfile.UserAddress.Id = data.userAddressId;
                        $scope.userProfile.UserAddress.Country.Name = data.country;
                       
                        

                        //$location.path('/Profile/' + $scope.userProfile.UserId);
                    });
            };

            $scope.isNullOrEmpty = function (val) {
                return val == null || val == undefined || val.length == 0;
            };

            $wprepository.Country.GetList($scope.countries);

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



