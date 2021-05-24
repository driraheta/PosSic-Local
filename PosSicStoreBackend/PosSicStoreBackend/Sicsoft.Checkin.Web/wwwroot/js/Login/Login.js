$(document).ready(function () {
    document.getElementById('CodComp').addEventListener('keydown', CambiarCampo1);
    document.getElementById('CodCajero').addEventListener('keydown', CambiarCampo);
   
 
 
});

function CambiarCampo(event) {
    if (event.keyCode == 13) {
        document.getElementById('CajPass').focus();
    }
}
function CambiarCampo1(event) {
    if (event.keyCode == 13) {
        document.getElementById('CodCajero').focus();
    }
}

function BuscarCompañia(url) {

    $("#btnSave").prop('disabled', true);
    $("#CodCajero").attr("readonly", "readonly");
    $("#CajPass").attr("readonly", "readonly");
    console.log(url);
    $.ajax({
        url: url, //@Url.Page("Editar", "AddNewItemToView")
        dataType: 'json',
        method: 'get',
        async: false,
        data: { codigo: $("#CodComp").val()},
        success: function (json) {
            console.log(json);
            if (json.token != "" || json.token != null) {
                $("#CodCajero").removeAttr("readonly");
                $("#CajPass").removeAttr("readonly");
                $("#Compañia").text( json.compañia.nomCompania);
                $("#Token").val(json.token);
                localStorage.setItem("compañia", json.compañia.nomCompania);
                localStorage.setItem("ubicacion", json.compañia.ubicacion);
            }

        },
        beforeSend: function (xhr) {  },
        error: function (ex) {
            console.log(ex);
        }
    });
}

function BuscarCaja(url) {
    console.log(url);

    var token = $("#Token").val();

    $.ajax({
        url: Url + "/api/Caja/Conectar?codigo=" + $("#CodCaj").val(), //@Url.Page("Editar", "AddNewItemToView")
        dataType: 'json',
        method: 'get',
        async: false,
        //data: { codigo: $("#CodCaj").val() },
        success: function (json) {
            console.log(json);
            if (json.CodCaja != "" || json.CodCaja != null) {
                $("#Cajero").text($("#Cajero").text() + " " + json.NomCaja);
             
            }

        },
        beforeSend: function (xhr) { xhr.setRequestHeader('Authorization', $("#Token").val()); console.log("Pasa antes de entrar"); },
        error: function (ex) {
            console.log(ex);
        }
    });
}

function IniciarSesion() {
  

     

    $.ajax({
        url: Url + "/api/Login/iniciarSesion?codigo=" + $("#CodCajero").val() + "&clavePaso=" + $("#CajPass").val() + "&codCaja=" + $("#CodCaj").val() , //@Url.Page("Editar", "AddNewItemToView")
        dataType: 'json',
        method: 'get',
        async: false,
        //data: { codigo: $("#CodCaj").val() },
        success: function (json) {
            console.log(json);
            $("#ErrorClave").text("");
            $("#NombreCajero").val(json.login.NomUsuario);

            var modulos = "";

            for (let i = 0; i < json.seguridad.length; i++) {
                modulos += json.seguridad[i].CodModulo + "|";
            }
            console.log(modulos);
            $("#Seguridad").val(modulos);
            
            $("#btnSave").removeAttr('disabled');
            //if (json.PermiteAperturaCaja) {
            //    $("#btnSave").removeAttr('disabled');

            //    if (!json.PrimeraApertura) {
            //        $("#ErrorEntrar").text("Reapertura de caja ");
            //    }

            //} else {
            //    if (json.CajaAbierta) {

            //        if (json.CajaActivaPor != "") {
            //            $("#ErrorEntrar").text("Caja fue abierta por: " + json.CajaActivaPor);

            //        }

            //        if (json.EstaCajaInactiva != "") {
            //            $("#ErrorEntrar").text("Caja fue abierta pero está inactiva " );

            //        }


                    


            //    } else {
            //        if (json.CajeroTieneAbiertaOtraCaja != "") {
            //            $("#ErrorEntrar").text("Cajero tiene la caja: " + json.NumCajaAbiertaPorEsteCajero + " abierta");

            //        }

            //        $("#ErrorEntrar").text("Caja se encuentra cerrada");
            //    }
            //}

            
        },
        beforeSend: function (xhr) { xhr.setRequestHeader('Authorization', $("#Token").val()); console.log("Pasa antes de entrar"); },
        error: function (ex) {
            console.log(ex);

            $("#ErrorClave").text(ex.responseText.replace('"',''));
        }
    });
}

