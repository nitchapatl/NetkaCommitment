(function($) {


    $("#reset_password_btn").click(function(){

        var username = $.trim($("#username").val()); //alert(username)
        var otp = $.trim($("#otp").val()); //alert(otp)
        var new_password = $.trim($("#new_password").val()); //alert(new_password)
        var confirm_password = $.trim($("#confirm_password").val()); //alert(confirm_password)

        if (new_password != confirm_password) {
            //can not reset password
            alert("Confirm Password does not match.");
        }
        else {
            //reset password
            
            data = {};
            data.UserName = username;
            data.OTP = otp;
            data.NewPassword = new_password;
            data.ConfirmPassword = confirm_password;

            $.ajax({
                type: "POST",
                contentType: 'application/json',
                url: URL + '/api/login/resetpassword',
                data: JSON.stringify(data),
		        //dataType: "json",
                success: function(data){

                    //alert(data);
                    var result = "<div class='shadow-large alert alert-small alert-round-medium bg-green1-dark'>";
                        result +="<i class='fa fa-check'></i>";
                        result +="<span>Reset password success.</span>";
                        result +="</div>";
                        $("#reset_password_status").html(result);
                        setTimeout(function(){ 
                            $("#reset_password_status").html("");
                        }
                        , 5000);

                    window.location.href = window.location.protocol + "//" + window.location.host + "/index-login.html";
                },
                error: function (req, status, error) {
                    //alert(req.responseText);

                    if(req.responseText == "Invalid OTP") {
    
                        var result = "<div class='shadow-large alert alert-small alert-round-medium bg-yellow1-dark'>";
                        result +="<i class='fa fa-exclamation-triangle'></i>";
                        result +="<span>Invalid OTP.</span>";
                        result +="</div>";
                        $("#reset_password_status").html(result);
                        setTimeout(function(){ 
                            $("#reset_password_status").html("");
                        }
                        , 5000);
                    }
                    else if(req.responseText == "Username not found") {
    
                        var result = "<div class='shadow-large alert alert-small alert-round-medium bg-yellow1-dark'>";
                        result +="<i class='fa fa-exclamation-triangle'></i>";
                        result +="<span>Username not found.</span>";
                        result +="</div>";
                        $("#reset_password_status").html(result);
                        setTimeout(function(){ 
                            $("#reset_password_status").html("");
                        }
                        , 5000);
                    }

                }
            });

        }
        
    });

}(jQuery));