(function($) {
    
    function getUserDevice(){
        var device = "";
        if (navigator.userAgent.match(/(iPhone|iPod|iPad|Android|BlackBerry|IEMobile)/)) {
            //document.addEventListener("deviceready", onDeviceReady, false); //phone
            //onDeviceReady();
            return "mobile";
        } else {
            device = onDesktopBrowser(); //this is the browser
            return device;
        }
    }

    function onDesktopBrowser() {
        
        var browser = "";

        if((navigator.userAgent.indexOf("Opera") || navigator.userAgent.indexOf('OPR')) != -1 ){

            browser = "Opera";
        }
        else if(navigator.userAgent.indexOf("Chrome") != -1 ){
            
            browser = "Chrome";
        }
        else if(navigator.userAgent.indexOf("Safari") != -1){
            
            browser = "Safari";
        }
        else if(navigator.userAgent.indexOf("Firefox") != -1 ){
            
            browser = "Firefox";
        }
        else if((navigator.userAgent.indexOf("MSIE") != -1 ) || (!!document.documentMode == true )){
            
            browser = "IE";
        }  
        else {
           
           browser = "unknown";
        }

        return browser;
    }

    function onDeviceReady() {
        //alert("mobile")
        //alert(device.cordova)
        /*alert("Cordova version: " + device.cordova + "\n" +
      "Device model: " + device.model + "\n" +
      "Device platform: " + device.platform + "\n" +
      "Device UUID: " + device.uuid + "\n" +
      "Device version: " + device.version);*/
    }

    $("#login-btn").click(function(){
        
        var username = $.trim($("#username").val()); //alert(username)
        var password = $.trim($("#password").val()); //alert(password)
        var userdevice = getUserDevice(); //alert(userdevice)

        data = {};
        data.UserName = username;
        data.UserPassword = password;
        data.UserDevice = userdevice;

        //$("#login_status").text("Authenticating...");
        
        $.ajax({
            type: "POST",
            contentType: 'application/json',
            url: URL + '/api/login/authorize',
            data: JSON.stringify(data),
		    //dataType: "json",
            success: function(data){

                console.log(data);
                
                //$("#login_status").text("Login Success..!");
                //alert("Login Success..!")

                localStorage.token = data.Token;
                localStorage.userId = data.Id;
                localStorage.status = data.Status;
                    
                getUserDetail();
                   
            },
            error: function (req, status, error) {
                //alert(req.responseText);

                if(req.responseText == "Wrong password") {

                    //$("#login_status").text("Login Failed..! Wrong password.");
                    //alert("Login Failed..! Wrong password.")

                    var result = "<div class='shadow-large alert alert-small alert-round-medium bg-yellow1-dark'>";
                    result +="<i class='fa fa-times-circle'></i>";
                    result +="<span>Login Failed. Wrong password.</span>";
                    result +="</div>";
                    $("#login_status").html(result);
                    setTimeout(function(){ 
                        $("#login_status").html("");
                    }
                    , 5000);
                }
                else if(req.responseText == "Username not found")
                {
                    //$("#login_status").text("Login Failed..! Username not found.");
                    //alert("Login Failed..! Username not found.")

                    var result = "<div class='shadow-large alert alert-small alert-round-medium bg-yellow1-dark'>";
                    result +="<i class='fa fa-times-circle'></i>";
                    result +="<span>Login Failed. Username not found.</span>";
                    result +="</div>";
                    $("#login_status").html(result);
                    setTimeout(function(){ 
                        $("#login_status").html("");
                    }
                    , 5000);
                }

            }
        });
    });

    function getUserDetail() {
        var token = localStorage.token;
        var userId = localStorage.userId;
        //alert(userId)
        $.ajax({
            type: 'POST',
            url: URL + '/api/login/getuser/' + userId,
            headers: {
                Authorization: 'Bearer ' + token
            },
            dataType: 'json',
            success: function(data) {
              //console.log(data);
              localStorage.userCode = data.UserCode;
              localStorage.userName = data.UserName;
              localStorage.userFirstName = data.UserFirstName;
              localStorage.userLastName = data.UserLastName;
              localStorage.userFirstNameEn = data.UserFirstNameEn;
              localStorage.userLastNameEn = data.UserLastNameEn;
              localStorage.departmentId = data.DepartmentId;
              localStorage.departmentName = data.DepartmentName;

              window.location.href = "wig-dashboard.html"; 
            },
            error: function() {
              alert("Sorry, can not get user detail.");
            }
        });
    }
}(jQuery));