var LayoutController = function ($scope, dataservice, $q) {

    $scope.userProfile = {};

    

    $scope.UploadProfilePiture = function (data) {
        if (data == "Please wait...")
            return false;

        
        return dataservice
            .UserProfile
            .UpdatePicture(data)
            .then(function (data) {

                $scope.userProfile.PictureUrl = data.pictureUrl;

            });
    };

    return dataservice
                .UserProfile
                .Details($scope.userProfile);

};
