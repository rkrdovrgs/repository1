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
            var count = 1;
            $("#addphone").click(function () {                
                //var strcount = count;
                //var div = document.createElement('div');
                //div.setAttribute('class', 'controls');
                //div.setAttribute('id', 'phone'+count.toString());              
                //document.getElementById("divphones").appendChild(div);

                //var txt = document.createElement('input');
                //txt.setAttribute('class', 'validation');              
                //document.getElementById("phone" + count.toString()).appendChild(txt);
                //count++;
                $scope.userProfile.UserPhones.push({Number: null, UserId: $scope.userProfile.UserId});
                console.log($scope.userProfile.UserPhones);
                $scope.$apply();
            });
            //$(function () {
            //    $("#DOB").datepicker();
            //});
            
            $scope.updateProfile = function () {
                $wprepository
                    .UserProfile
                    .Update($scope.userProfile)
                    .then(function (data) {
                        $scope.userProfile.UserAddress.UserId = data.userAddressId;
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



