$repository = function($http, $q) {
    var _modelName;
    var _Get = function (id, model) {

        var d = $q.defer();

        $http.get("/api/" + _modelName + "/" + id)
            .success(function (data) {
                angular.copy(data, model);

                d.resolve(data);
            })
            .error(function () {
                //error code here

                deferred.reject();
            });

        return d.promise;
    };


    var _Put = function (id, model) {
        var d = $q.defer();

        
        $http.put("/api/" + _modelName + "/" + id, model)
            .success(function (data) {
                //success code here
                d.resolve(data);
            })
            .error(function () {
                //error code here
                deferred.reject();
            });

        return d.promise;
    }


    return function (modelName) {
        _modelName = modelName;
        return {
            Get: _Get,
            //GetList: _GetValues,
            //Add: _Post,
            Update: _Put,
            //Delete: _Delete
        };
    };

};