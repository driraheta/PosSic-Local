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

    $("#frmNuevoEquipamento").validate({
        rules: {
            idNombreEquipamento: {
                required: true,
                minLength: 5
            }
        }
    });

    ObtenerCodigoConsecutivo();

});

function ObtenerCodigoConsecutivo() {
    $.ajax({
        url: UriCors + UriApi + 'Equipamentos?Skip=0&Take=200&SearchText=',
        method: 'get',
        async: false,
        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem("Token")
        },
        success: function (json) {
            var JsonLength = json.length + 1;
            var _JsonNumero = (JsonLength <= 9 ? ('00' + JsonLength) : (JsonLength >= 10 && JsonLength <= 99 ? ('0' + JsonLength) : JsonLength));

            $("#idCodigoEquipamento").val(_JsonNumero);
        },
        error: function () {
            fjsMensajeGrowl('No se pudo establecer la conexión', 'Error', 'danger');
        }
    });
}

function ValidateGuardaEquipamento() {
    $("#idNombreEquipamento").valid();

    if ($("#idCodigoEquipamento").valid() && $("#idNombreEquipamento").valid()) {
        GuardarEquipamento();
    } else {
        if ($("#idCodigoEquipamento").val() == '') {
            fjsMensajeGrowl('Se debe ingresar un código de equipamento válido', 'Alerta', 'warning');
            $("#idCodigoEquipamento").focus();
        } else if ($("#idNombreEquipamento").val() == '') {
            fjsMensajeGrowl('Se debe ingresar un nombre de equipamento válido', 'Alerta', 'warning');
            $("#idNombreEquipamento").focus();
        }
    }
}

function GuardarEquipamento() {
    var Token = sessionStorage.getItem("Token");

    $.ajax({
        url: UriCors + UriApi + 'Equipamentos',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        async: false,
        headers: {
            'Authorization': 'Bearer ' + Token
        },
        data: JSON.stringify({
            'CodEquipamento': $("#idCodigoEquipamento").val(),
            'NomEquipamento': $("#idNombreEquipamento").val()
        }),
        success: function (json) {
            if (json != '') {
                fjsMensajeGrowl('El equipamento se agrego correctamente', 'Alerta', 'success');
                window.location.href = window.location.origin + RoutePub + '/Equipamentos/Index';
            }
        },
        beforeSend: function () {
            fjsMensajeEspera('show', false);
        },
        error: function (json) {
            if (json.status == 409) {
                fjsMensajeGrowl('El código de equipamento, ya se encuentra dado de alta', 'Alerta', 'warning');
            } else {
                fjsMensajeGrowl('No se pudo establecer conexión', 'Error', 'danger');
            }
        },
        complete: function () {
            fjsMensajeEspera('hide', true);
        }
    });
}