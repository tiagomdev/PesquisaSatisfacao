// Write your JavaScript code.
function errorMessage(m) {
    $("#alertDanger").html(m);
    $("#alertDanger").show();
    setTimeout(function () {
        $("#alertDanger").html("");
        $("#alertDanger").hide();
    }, 2500)
}

function successMessage(m) {
    $("#alertSuccess").html(m);
    $("#alertSuccess").show();
    setTimeout(function () {
        $("#alertSuccess").html("");
        $("#alertSuccess").hide();
    }, 2500)
}
