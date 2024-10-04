
// Não permite inserir dados não numéricos no campo
function toNumber(string) {
    var numsStr = string.replace(/[^0-9]/g, '');
    return numsStr;
}

// Alertas personalizados de DIV
function callAlertSuccess(div, msg, callback) {
    $(div).removeClass();
    $(div).html(msg).addClass("alert alert-success").show("fast");
    setTimeout(function () {
        callback();
    }, 2000);
}

function callAlertDanger(div, msg, callback) {
    $(div).removeClass();
    $(div).html(msg).addClass("alert alert-danger").show("fast");
    setTimeout(function () {
        callback();
    }, 2000);
}

function callAlertWarning(div, msg, callback) {
    $(div).removeClass();
    $(div).html(msg).addClass("alert alert-warning").show("fast");
    setTimeout(function () {
        callback();
    }, 2000);
}

function callAlertNeutral(div, msg, callback) {
    $(div).removeClass();
    $(div).html(msg).addClass("alert alert-secondary").show("fast");
    setTimeout(function () {
        callback();
    }, 2000);
}