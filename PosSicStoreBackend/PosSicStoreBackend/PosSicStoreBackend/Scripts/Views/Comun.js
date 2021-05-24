const Publicacion = true;
var _divModal;
const UriApi = 'http://fc05.facturaclick.cr/CheckInAPI/api/';
const UriCors = Publicacion ? '' : 'https://cors-anywhere.herokuapp.com/';
const RoutePub = Publicacion ? '/Chekin' : '';

$(document).ready(function () {
    $("#lblUsuario").text(sessionStorage.getItem("CorreoE"));

    _divModal = $("#divProcesando");
    _divModal.modal('hide');

});

function fjsMensajeEspera(sAccion, bRetardo) {
    if (bRetardo) {
        setTimeout(function () {
            _divModal.modal(sAccion);
        }, 1000);
    }
    else { _divModal.modal(sAccion); }
}

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