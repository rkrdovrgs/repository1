var ProfileController = new Controller(WebPortfolio, "ProfileController",
    function () {


        var _Index = function () {
        };

        var _Details = function ($scope, $wprepository, $q) {
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

            $('.validation').change(function () {
                if ($(this).val() == "") 
                    $(this).addClass('inputempty');                
                else
                    $(this).removeClass('inputempty');               
            });
            /* <div class="controls" data-ng-repeat="ph in userProfile.UserPhones" >
                    <input type="text" class="validation" data-ng-model="ph.Number" />
                </div>*/
           
            
            $(document).ready(function () {
                $(".DoB").inputmask("mask", { "mask": "9999-99-99" });
                $(".DoB").datepicker({ format: 'yyyy-mm-dd' });
               // $(".maskphone").mask("(999) 999-9999");
            });
            

            $scope.addPhone = function () {                
                $scope.userProfile.UserPhones.push({ Number: null, UserId: $scope.userProfile.Id });                
                // $scope.$apply();
                
                    $(".maskphone").inputmask({ "mask": "(999) 999-9999" });
                
            };

            
           

            $scope.updateProfile = function () {
                $wprepository
                    .UserProfile
                    .Update($scope.userProfile)
                    .then(function (data) {
                        if ($scope.userProfile.UserAddress != null)                        
                            $scope.userProfile.UserAddress.Id = data.userAddressId;

                        //$location.path('/Profile/' + $scope.userProfile.UserId);
                    });
            };

            $scope.isNullOrEmpty = function (val) {
                return val == null || val == undefined || val.length == 0;
            };

            //$wprepository.Country.GetList($scope.countries);

            return $q.all([
                $wprepository
                    .UserProfile
                    .Details($scope.userProfile),
                $wprepository.Country.GetList($scope.countries)
                ]);

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



