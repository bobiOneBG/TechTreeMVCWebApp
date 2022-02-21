$(function () {

    var userLoginButton = $("#userLoginModal button[name='login']").click(onUserLoginClick)
    
    function onUserLoginClick() {
        var url = "UserAuth/Login";

        var antiForgeryToken = $("#userLoginModal input[name='__RequestVerificationToken']").val();

        var email = $("#userLoginModal input[name = 'Email']").val();
        var password = $("#userLoginModal input[name = 'Password']").val();
        var rememberMe = $("#userLoginModal input[name = 'RememberMe']").prop('checked');

        var userInput = {
            __RequestVerificationToken: antiForgeryToken,
            Email: email,
            Password: password,
            RememberMe: rememberMe
        };

        $.ajax({
            type: "POST",
            url: url,
            data: userInput,
            success: function (data) {

                var parsed = $.parseHTML(data);

                var hasErrors = $(parsed).find("input[name='LoginInValid']").val() == "true";

                if (hasErrors == true) {
                    $("#userLoginModal").html(data);
                    /*  code to wire Up event for the login button for the case when login dialog is rendered on the screen 
                     *  after failed login attempt */
                    userLoginButton = $("#userLoginModal button[name='login']").click(onUserLoginClick);

                    var form = $("#userLoginForm");
                    /* code for re-initialize unobtrusive validation functionality */
                    $(form).removeData("validator");
                    $(form).removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse(form);

                }
                else {
                    location.href = 'Home/Index';

                }
            },
            /* handle a case when the post request failed (404 not found, 500 internal server error etc) */
            error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                PresentClosableBootstrapAlert("#alert_placeholder_login", "danger", "Error!", errorText);

                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            }
        });
    };
});