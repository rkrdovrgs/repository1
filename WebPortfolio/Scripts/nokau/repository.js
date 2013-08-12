$repository = function ($http, $q) {

    var prefix = "/api/";

    var _modelName;

    var _get = function (model, url, filters) {
        var d = $q.defer();

        $http.get(url, filters)
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

    var _Get = function (model, id) {



        var url = prefix + _modelName + "/";
        if (id != undefined)
            url += id;


        return _get(model, url);

    };

    var _FindOne = function (model, filters, actionName) {


        var url = prefix + _modelName + "/" + actionName;

        return _get(model, url, filters);
    };

    var _GetList = function (model, filters, actionName) {

        url = prefix + _modelName;
        if (actionName != undefined)
            url += "/" + actionName;

        return _get(model, url, filters);
    };


    var _Post = function (model, actionName) {
        var d = $q.defer();

        var url = prefix + _modelName;
        if (actionName != undefined)
            url += "/" + actionName;

        $http.post(url, model)
            .success(function (data) {
                //success code here
                d.resolve(data);
            })
            .error(function () {
                //error code here
                deferred.reject();
            });

        return d.promise;
    };

    var _Put = function (model, id, actionName) {
        var d = $q.defer();

        var url = prefix + _modelName + "/";
        if (actionName != undefined)
            url += actionName;
        else
            url += id

        $http.put(url, model)
            .success(function (data) {
                //success code here
                d.resolve(data);
            })
            .error(function () {
                //error code here
                deferred.reject();
            });

        return d.promise;
    };


    var _Delete = function (id, actionName) {
        var d = $q.defer();

        var url = prefix + _modelName + "/";
        if (actionName != undefined)
            url += actionName;
        else
            url += id

        $http.delete(url)
            .success(function (data) {
                //success code here
                d.resolve(data);
            })
            .error(function () {
                //error code here
                deferred.reject();
            });

        return d.promise;
    };


    return function (modelName) {
        _modelName = modelName;



        return {
            Get: _Get,
            FindOne: _FindOne,
            GetList: _GetList,
            Add: _Post,
            Update: _Put,
            Delete: _Delete
        };
    };

};