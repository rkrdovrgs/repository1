$.replaceUrl = function (e, t) {
    if (t != undefined && t != "")
        $("title").text(t);
    if (Modernizr.history)
        window.history.replaceState("object or string", "Title", e);
    else {
        window.location.hash = e
    }
};

if (!Modernizr.history && window.location.hash != "") {
    var loc = window.location.hash + "";
    loc = loc.replace("#", "");
    if (loc != "")
        window.location = loc
}