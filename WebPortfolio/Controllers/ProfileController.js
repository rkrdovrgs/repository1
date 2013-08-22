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

            //estilos campos vacios
            $('.validation').change(function () {
                if ($(this).val() == "") 
                    $(this).addClass('inputempty');                
                else
                    $(this).removeClass('inputempty');               
            });//END

            /* <div class="controls" data-ng-repeat="ph in userProfile.UserPhones" >
                    <input type="text" class="validation" data-ng-model="ph.Number" />
                </div>*/

            //cargar imagen
            //$(function () {
            //    $('#picField').change(function (e) { //a un input de tipo 'file' se cargara la imagen(ruta de direccion), y llamamos al evento 'addImage'
            //        addImage(e);
            //    });

            //    function addImage(e) {
            //        var file = e.target.files[0],
            //        imageType = /image.*/; //se aplica un filtro para formato de imagen

            //        if (!file.type.match(imageType))// si no es de tipo imagen..
            //            return;

            //        var reader = new FileReader(); //se cargara el archivo en memoria y se asignara a un metodo('fileOnload') el procesamiento de los datos.
            //        reader.onload = fileOnload;
            //        reader.readAsDataURL(file);
            //        $('#prueba').attr("value", reader.readAsDataURL(file));
            //    }

            //    function fileOnload(e) { //se guarda los datos de la imagen en una variable y se agregara los datos al atributo src del img
            //        var result = e.target.result;
            //        console.log(result);
            //        $('#profile-picture').attr("src", result);
            //    } //Nota: La variable result contiene los datos de la imagen en codificación base64
            //});// END

            $('#enviar').click(function () {
                var formData = new FormData();
                formData.append("images", $('#pruebaa')[0].files[0]);
                $.ajax({
                    url: '/home/prueba',  //Server script to process data
                    type: 'POST',
                    //xhr: function () {  // Custom XMLHttpRequest
                    //    var myXhr = $.ajaxSettings.xhr();
                    //    if (myXhr.upload) { // Check if upload property exists
                    //        myXhr.upload.addEventListener('progress', progressHandlingFunction, false); // For handling the progress of the upload
                    //    }
                    //    return myXhr;
                    //},
                    //Ajax events
                    //beforeSend: beforeSendHandler,
                    //success: completeHandler,
                    //error: errorHandler,
                    // Form data
                   data: formData,
                    //Options to tell jQuery not to process data or worry about content-type.
                    //cache: false,
                    //contentType: false,
                    //processData: false
                });
            });

            //function archivo(evt) {
            //    var files = evt.target.files; // FileList object

            //    //Obtenemos la imagen del campo "file". 
            //    for (var i = 0, f; f = files[i]; i++) {
            //        //Solo admitimos imágenes.
            //        if (!f.type.match('image.*')) {
            //            continue;
            //        }

            //        var reader = new FileReader();

            //        reader.onload = (function (theFile) {
            //            return function (e) {
            //                // Creamos la imagen.
            //                document.getElementById("figure").innerHTML = ['<img class="thumb" src="', e.target.result, '" title="', escape(theFile.name), '"/>'].join('');
            //                console.log(result);
            //            };
            //        })(f);

            //        reader.readAsDataURL(f);
            //    }
            //}

            //document.getElementById('picField').addEventListener('change', archivo, false);

            //mascaras para input
            $(document).ready(function () {
                $(".DoB").inputmask("mask", { "mask": "9999-99-99" });
                $(".DoB").datepicker({ format: 'yyyy-mm-dd' });
               // $(".maskphone").mask("(999) 999-9999");
            });//END
            

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



