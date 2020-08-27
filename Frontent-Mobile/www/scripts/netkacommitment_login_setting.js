$(document).ready(function(){   


const WEBAPI_ACCESS_TOKEN = localStorage.getItem("token");
//alert(WEBAPI_ACCESS_TOKEN)


if(WEBAPI_ACCESS_TOKEN == null)
{
    window.location.href = window.location.protocol + "//" + window.location.host + "/index-login.html";
}


$("#logout").click(function () {
    localStorage.removeItem("token");
    localStorage.removeItem("userName");
    localStorage.removeItem("departmentName");
    localStorage.removeItem("userLastNameEn");
    localStorage.removeItem("status");
    localStorage.removeItem("userLastName");
    localStorage.removeItem("userFirstNameEn");
    localStorage.removeItem("userId");
    localStorage.removeItem("departmentId");
    localStorage.removeItem("userFirstName");
    localStorage.removeItem("userCode");

    window.location.href = "index-login.html"; 
});

});