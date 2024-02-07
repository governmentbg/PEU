window.addEventListener("load", function () {
    var a = document.querySelector("a.PostLogoutRedirectUri");
    if (a) {
        setTimeout(function () {
            window.location.href = a.href;
        }, 100);
    }
});