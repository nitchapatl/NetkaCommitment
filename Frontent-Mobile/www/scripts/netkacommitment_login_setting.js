$(document).ready(function(){   


const WEBAPI_ACCESS_TOKEN = localStorage.getItem("token");
//alert(WEBAPI_ACCESS_TOKEN)


if(WEBAPI_ACCESS_TOKEN == null)
{
    window.location.href = window.location.protocol + "//" + window.location.host + "/index-login.html";
}


$("#logout").click(function () {
    localStorage.removeItem("token");
});


});