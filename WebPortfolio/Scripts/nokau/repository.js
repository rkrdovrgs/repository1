$repository = function ($http, $q, dataContext) {

    

    var _modelName;
    var _Get = function (id, model) {

        var d = $q.defer();

        var url = "/api/" + _modelName + "/" + id;
        if (dataContext[_modelName][url] != undefined)
        {
            angular.copy(dataContext[_modelName]
        }


        $http.get(url)
            .success(function (data) {
                angular.copy(data, model);
                dataContext[_modelName][url] = data;
                d.resolve(data);
            })
            .error(function () {
                //error code here

                deferred.reject();
            });

        return d.promise;
    };

    var _FindOne = function (filters, model) {

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

        if (dataContext[modelName] == undefined)
            dataContext[modelName] = {};

        return {
            Get: _Get,
            //GetList: _GetValues,
            //Add: _Post,
            Update: _Put,
            //Delete: _Delete
        };
    };

};