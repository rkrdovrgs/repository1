﻿function BindRegisterLink() {
    $('#register-link').click(function () {
        $.ajax({
            url: '/Account/Register',
            success: function (data) {
                $('#blockbody').html(data);

            }
        });

        return false;
    });
}

$('#blockbody').on("click", '#login-link', function () {
    $.ajax({
        url: '/Account/Login',
        success: function (data) {
            $('#blockbody').html(data);
            BindRegisterLink();
        }
    });

    return false;
});

BindRegisterLink();