$('#blockbody').on('click', '#register-link', function () {
    $.ajax({
        url: '/Account/Register',
        success: function (data) {
            $('#blockbody').html(data);
            $.validator.unobtrusive.parse($("#blockbody"));
            $.replaceUrl('/Register');
        }
    });

    return false;
});


$('#blockbody').on("click", '#login-link', function () {
    $.ajax({
        url: '/Account/Login',
        success: function (data) {
            $('#blockbody').html(data);
            $.validator.unobtrusive.parse($("#blockbody"));
            $.replaceUrl('/Login');
        }
    });

    return false;
});
