var LayoutController = function ($scope, dataservice, $q) {

    $scope.userProfile = {};

    $("#sbmtfilePicture").change(function () {
        //alert("p");
        //$("#formpicture").submit();

        //$scope.UploadProfilePiture = function (data) {
        //    //console.log(data);

        //    if (data == "Please wait..." || data == "")
        //        return false;


        //    return dataservice
        //        .UserProfile
        //        .UpdatePicture(data)
        //        .then(function (data) {
        //            $scope.userProfile.PictureUrl = data.pictureUrl;
        //        });
        //};
        //document.getElementById('#sbmtpicture').trigger('click');
        $("#formpicture").children('#sbmtpicture').click(); 
    });

    $scope.UploadProfilePiture = function (data) {
        //console.log(data);
        if (data == "Please wait..." || data == "")
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
