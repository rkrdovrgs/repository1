Function.prototype.GetParamNames = function () {
    var fn = this;
    var fstr = fn.toString();
    return fstr.match(/\(.*?\)/)[0].replace(/[()]/gi, '').replace(/\s/gi, '').split(',');
};



