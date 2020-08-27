(function($) {
    
    
    $("#submit_btn").click(function(){

        var username = $.trim($("#username").val()); //alert(username)
        var email = $.trim($("#email").val()); //alert(email)

        data = {};
        data.UserName = username;
        data.Email = email;

        $('#forgot_password_status').html('<p style="color:#ffffff;">Sending...</p>');

        $.ajax({
            type: "POST",
            contentType: 'application/json',
            url: URL + '/api/login/forgotpassword',
            data: JSON.stringify(data),
		    //dataType: "json",
            success: function(data){

                console.log(data);
                window.location.href = window.location.protocol + "//" + window.location.host + "/index-reset-password.html";
            },
            error: function (req, status, error) {
                alert(req.responseText);
            }
        });

    });

}(jQuery));