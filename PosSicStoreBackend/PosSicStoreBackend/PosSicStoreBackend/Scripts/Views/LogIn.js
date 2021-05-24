const Publicacion = true;
const UriCors = Publicacion ? '' : 'https://cors-anywhere.herokuapp.com/';
const UriApi = 'http://fc05.facturaclick.cr/CheckInAPI/';
const RoutePub = Publicacion ? '/Chekin' : '';

$(document).ready(function () {
    jQuery.extend(jQuery.validator.messages, {
        required: "El campo es requerido.",
        remote: "Please fix this field.",
        email: "Ingresa una dirección de correo valida.",
        url: "Please enter a valid URL.",
        date: "Ingresa una fecha valida.",
        dateISO: "Please enter a valid date (ISO).",
        number: "Por favor ingresa un número valido.",
        digits: "Please enter only digits.",
        creditcard: "Please enter a valid credit card number.",
        equalTo: "Please enter the same value again.",
        accept: "Please enter a value with a valid extension.",
        maxlength: jQuery.validator.format("Ingresa un máximo de {0} caracteres."),
        minlength: jQuery.validator.format("Ingresa por lo menos {0} caracteres."),
        rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
        range: jQuery.validator.format("Please enter a value between {0} and {1}."),
        max: jQuery.validator.format("Ingresa un valor menor o igual a: {0}."),
        min: jQuery.validator.format("Ingresa un valor mayor o igual: {0}.")
    });


    $("#frmLogin").validate({
        rules: {
            password: {
                required: true,
                minlength: 5
            }
        }
    });

});

function LogIn() {
    $("#txtUsuario").valid();
    $("#txtPassword").valid();

    if ($("#txtUsuario").valid() && $("#txtPassword").valid()) {
        $.ajax({
            url: UriCors + UriApi + 'Token',
            type: 'POST',
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            },
            data: {
                grant_type: 'password',
                userName: $("#txtUsuario").val(),
                password : $("#txtPassword").val()
            },
            async: true,
            success: function (json) {
                if (json === 0) {
                    fjsMensajeGrowl('El usuario y contraseña son incorrectas', 'Error', 'danger');
                } else {
                    var _Json = json;
                    sessionStorage.setItem("CorreoE", _Json.userName);
                    sessionStorage.setItem("Token", _Json.access_token);
                    window.location.href = window.location.origin + RoutePub + '/Home/Index';
                }
            },
            beforeSend: function () {
                $('#frmLogin').children('.ibox-content').toggleClass('sk-loading');
            },
            error: function (json) {
                fjsMensajeGrowl('No se pudo establecer conexión', 'Error', 'danger');
            },
            complete: function () {
                $('#frmLogin').children('.ibox-content').toggleClass('sk-loading');
            }
        });
    } else {
        if ($("#txtUsuario").val() == '') {
            fjsMensajeGrowl('Se debe ingresar un usuario valido', 'Alerta', 'warning');
            $("#txtUsuario").focus();
        } else if ($("#txtPassword").val() == '') {
            fjsMensajeGrowl('Se debe ingresar la contraseña', 'Alerta', 'warning');
            $("#txtPassword").focus();
        }
    }
}

//function LogIn() {
//    if ($("#txtUsuario").valid() && $("#txtPassword").valid()) {
//        $.ajax({
//            url: '../../LogIn/IniciarSesion',
//            type: 'POST',
//            contentType: 'application/json; charset=utf-8',
//            data: JSON.stringify({
//                Username: $("#txtUsuario").val(),
//                Password: $("#txtPassword").val()
//            }),
//            async: true,
//            success: function (json) {
//                if (json === 0) {
//                    fjsMensajeGrowl('El usuario y contraseña son incorrectas', 'Error', 'danger');
//                } else {
//                    var _Json = JSON.parse(json);
//                    sessionStorage.setItem("CorreoE", _Json.userName);
//                    sessionStorage.setItem("Token", _Json.access_token);
//                    window.location.href = window.location.origin + '/Home/Index';
//                }
//            },
//            beforeSend: function () {
//                $('#frmLogin').children('.ibox-content').toggleClass('sk-loading');
//            },
//            error: function (json) {
//                fjsMensajeGrowl('No se pudo establecer conexión', 'Error', 'danger');
//            },
//            complete: function () {
//                $('#frmLogin').children('.ibox-content').toggleClass('sk-loading');
//            }
//        });
//    } else {
//        if ($("#txtUsuario").val() == '') {
//            fjsMensajeGrowl('Se debe ingresar un usuario valido', 'Alerta', 'warning');
//            $("#txtUsuario").focus();
//        } else if ($("#txtPassword").val() == '') {
//            fjsMensajeGrowl('Se debe ingresar la contraseña', 'Alerta', 'warning');
//            $("#txtPassword").focus();
//        }
//    }
//}

function fjsMensajeGrowl(sMensaje, sTitulo, theme) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "progressBar": true,
        "preventDuplicates": false,
        "positionClass": "toast-top-right",
        "onclick": null,
        "showDuration": "400",
        "hideDuration": "1000",
        "timeOut": "7000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    switch (theme) {
        case 'danger':
            toastr.error(sMensaje, sTitulo);
            break;
        case 'warning':
            toastr.warning(sMensaje, sTitulo);
            break;
        case 'success':
            toastr.success(sMensaje, sTitulo);
            break;

    }
}