var Controller = function (namespace, controllerName, controller) {
    
    if (namespace.Controllers == undefined)
        namespace.Controllers = {};

    namespace.Controllers[controllerName] = controller;
    
    return controller;
};

//Controller.prototype = function () {
        
//};