////$(function () {

////    var sss = $("#userRegistrationModal button[name='register']").click(onUserRegisterClick);

////    function onUserRegisterClick() {
////        alert("Uraaaaaa")
////    };
////});

$(function () {
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

                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);
            }

        });

    }

});