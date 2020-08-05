(function($) {
    
    function getUserDevice(){

        /*debug = (window.cordova === undefined);
        alert(debug)

        if(!debug) {
        
            document.addEventListener("deviceready", onDeviceReady, false);
            onDeviceReady();
        }*/
        if (navigator.userAgent.match(/(iPhone|iPod|iPad|Android|BlackBerry|IEMobile)/)) {
            document.addEventListener("deviceready", onDeviceReady, false); //phone
            onDeviceReady();

        } else {
            onDesktop(); //this is the browser
        }
    }

    function onDesktop() {
        alert("desktop browser");
    }

    function onDeviceReady() {
        alert("mobile")
        alert(device.cordova)
        /*alert("Cordova version: " + device.cordova + "\n" +
      "Device model: " + device.model + "\n" +
      "Device platform: " + device.platform + "\n" +
      "Device UUID: " + device.uuid + "\n" +
      "Device version: " + device.version);*/
    }

    $("#login-btn").click(function(){
        
        getUserDevice();
        var username = $.trim($("#username").val()); //alert(username)
        var password = $.trim($("#password").val()); 

        data = {};
        data.UserName = username;
        data.UserPassword = password;
        data.UserDevice = "";

        $("#login_message").text("Authenticating...");
        
        /*$.ajax({
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            url: URL + '/api/login/autorize',
            data: JSON.stringify(data),
		    dataType: "json",
            success: function(data){

                console.log(data);
                if(data == "success") {
                    $("#status").text("Login Success..!");
                    localStorage.loginstatus = "true";
                    window.location.href = "welcome.html";
                }
                else if(data == "error")
                {
                    $("#status").text("Login Failed..!");
                }
            }
        });*/
    });
}(jQuery));