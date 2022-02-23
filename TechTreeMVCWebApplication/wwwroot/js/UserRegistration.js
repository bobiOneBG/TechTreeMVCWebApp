$(function () {

    $("#userRegistrationModal button[name = 'register']").prop("disabled", true);

    $("#userRegistrationModal input[name = 'AcceptUserAgreement']").click(onAcceptUserAgreementClick);

    function onAcceptUserAgreementClick() {

        if ($(this).is(":checked")) {
            $("#userRegistrationModal button[name = 'register']").prop("disabled", false);
        }

        else {
            $("#userRegistrationModal button[name = 'register']").prop("disabled", true);
        }
    }

    $("#userRegistrationModal input[name = 'Email']").blur(function () {

        var email = $("#userRegistrationModal input[name = 'Email']").val();

        var url = "UserAuth/UserNameExist?userName=" + email;

        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                if (data == true) {

                    /* create empty div element to the _UserRegistrationPartial with id="alert_placeholder_register" */
                    PresentClosableBootstrapAlert("#alert_placeholder_register", "warning", "Invalid Email", "This email address has already been registered");                  
                }

                else {
                    CloseAlert("#alert_placeholder_register");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {

                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                PresentClosableBootstrapAlert("#alert_placeholder_register", "danger", "Error!", errorText);

                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);
            }
        });
    });

    var registerUserButton = $("#userRegistrationModal button[name = 'register']").click(onUserRegisterClick);

    function onUserRegisterClick() {
        var url = "/UserAuth/RegisterUser";

        var antiForgeryToken = $("#userRegistrationModal input[name='__RequestVerificationToken']").val();
        var email = $("#userRegistrationModal input[name='Email']").val();
        var password = $("#userRegistrationModal input[name='Password']").val();
        var confirmPassword = $("#userRegistrationModal input[name='ConfirmPassword']").val();
        var firstName = $("#userRegistrationModal input[name='FirstName']").val();
        var lastName = $("#userRegistrationModal input[name='LastName']").val();
        var address = $("#userRegistrationModal input[name='Address']").val();

        var user = {
            __RequestAntiForgeryToken: antiForgeryToken,
            Email: email,
            Password: password,
            ConfirmPassword: confirmPassword,
            FirstName: firstName,
            LastName: lastName,
            Address: address,
            AcceptUserAgreement: true
        };

        $.ajax({
            type: "POST",
            url: url,
            data: user,
            success: function (data) {

                var parsed = $.parseHTML(data);

                var hasErrors = $(parsed).find("input[name='RegistrationInValid']").val() == 'true';

                if (hasErrors) {

                    $("#userRegistrationModal").html(data);
                    var registerUserButton = $("#userRegistrationModal button[name = 'register']").click(onUserRegisterClick);

                    $("#userRegistrationForm").removeData("validator");
                    $("#userRegistrationForm").removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse("#UserRegistrationForm");
                }
                else {
                    location.href = '/Home/Index';
                }

            },
            error: function (xhr, ajaxOptions, thrownError) {

                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                PresentClosableBootstrapAlert("#alert_placeholder_register", "danger", "Error!", errorText);

                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);
            }

        });

    }

});