/// <reference path="oidc-client.js" />

function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerText += msg + '\r\n';
    });
}

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("logout").addEventListener("click", logout, false);

//var config = {
//    authority: "https://localhost:7004",
//    client_id: "js",
//    redirect_uri: "https://localhost:7005/callback.html",
//    response_type: "id_token token",
//    scope: "openid profile EshopApi",
//    post_logout_redirect_uri: "https://localhost:7005/index.html",
//};

var config = {
    authority: "https://localhost:7004",
    client_id: "eshop_client",
    redirect_uri: "https://localhost:7005/callback.html",
    response_type: "code",
    scope: "openid profile eshopapi", // scope is not case sensitive. use lowercase always
    post_logout_redirect_uri: "https://localhost:7005/index.html",
};

var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in", user.profile);
    }
    else {
        log("User not logged in");
    }
});

function login() {
    mgr.signinRedirect();
}

function api() {
    mgr.getUser().then(function (user) {
        var url = "https://localhost:7010/api/Users";
        debugger
        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function logout() {
    mgr.signoutRedirect();
}