var _CodEquipamento = '';

$(document).ready(function () {
    ObtieneEquipamentos();
});

function ObtieneEquipamentos() {
    var Token = sessionStorage.getItem("Token");
    var _Descripcion = $("#idSearchText").val();
    var _Take = 20;
    var _Skip = 0;


    $.ajax({
        url: UriCors + UriApi + 'Equipamentos?Skip=' + _Skip + '&Take=' + _Take + '&SearchText=' + _Descripcion,
        method: 'get',
        async: true,
        headers: {
            'Authorization': 'Bearer ' + Token
        },
        success: function (json) {
            var sOptions = '';
            for (var i = 0; i < json.length; i++) {
                sOptions += '<tr>';
                sOptions += '<td style="text-align:center; width:2%">';
                sOptions += '<div class="btn-group">';
                sOptions += '<a class="dropdown-toggle" data-toggle="dropdown" href="#">';
                sOptions += '<span class="glyphicon glyphicon-option-vertical" title="Opciones" data-toggle="tooltip" data-placement="top"></span></a>';
                sOptions += '<ul class="dropdown-menu" role="menu"><li role="presentation" class="dropdown-header">Acciones</li><li>';
                sOptions += '<a id=btnDetalles href="#" onclick="javascript: IndexDetails(\'' + json[i].CodEquipamento + '\')"><span class="glyphicon glyphicon-zoom-in"></span>&nbsp;&nbsp;Detallar</a>'
                sOptions += '<a id=btnEditar href="#" onclick="javascript: IndexEditar(\'' + json[i].CodEquipamento + '\')"><span class="glyphicon glyphicon-pencil"></span>&nbsp;&nbsp;Editar</a>'
                sOptions += '<a id=btnEliminar href="#" onclick="javascript: IndexEliminar(\'' + json[i].CodEquipamento + '\');"><span class="glyphicon glyphicon-trash"></span>&nbsp;&nbsp;Eliminar</a>'
                sOptions += '</li></ul></div></td>';
                sOptions += '<td style="width: 15%">' + json[i].CodEquipamento + '</td>';
                sOptions += '<td>' + json[i].NomEquipamento + '</td>';
                sOptions += '</tr>';
            }
            $("#tlbIndiceEquipamento").html();
            $("#tlbIndiceEquipamento").html('<tbody>' + sOptions + '</tbody>');
            $("#tlbIndiceEquipamento").append('<thead><tr><th style="width: 2%"></th><th style="width: 15%">Código Equipamento</th><th>Nombre Equipamento</th></tr></thead>');
        },
        error: function () {
            fjsMensajeGrowl('No se pudo establecer la conexión', 'Error', 'danger');
        },
        beforeSend: function () {
            fjsMensajeEspera('show', false);
        },
        complete: function () {
            fjsMensajeEspera('hide', true);
        }
    });
}

function NuevoEquipamento() {
    window.location.href = window.location.origin + RoutePub + '/Equipamentos/Create';
}

function IndexDetails(CodEquipamento) {
    var Token = sessionStorage.getItem("Token");

    $.ajax({
        url: UriCors + UriApi + 'Equipamentos/' + CodEquipamento,
        method: 'get',
        async: true,
        headers: {
            'Authorization': 'Bearer ' + Token
        },
        success: function (json) {
            if (json != '') {
                $("#IdCodigo").val(json.CodEquipamento);
                $("#IdDescripcion").val(json.NomEquipamento);

                $("#divDetailsEquipamento").modal('show');
            }
        },
        error: function () {
            fjsMensajeGrowl('No se pudo establecer la conexión', 'Error', 'danger');
        }
    });
}

function IndexEditar(CodEquipamento) {
    _CodEquipamento = CodEquipamento;

    var Token = sessionStorage.getItem("Token");

    $.ajax({
        url: UriCors + UriApi + 'Equipamentos/' + CodEquipamento,
        method: 'get',
        async: true,
        headers: {
            'Authorization': 'Bearer ' + Token
        },
        success: function (json) {
            if (json != '') {
                $("#IdCodigo_Edt").val(json.CodEquipamento);
                $("#IdDescripcion_Edt").val(json.NomEquipamento);

                $("#divEditEquipamento").modal('show');
            }
        },
        error: function () {
            fjsMensajeGrowl('No se pudo establecer la conexión', 'Error', 'danger');
        }
    });
}

function ActualizaEquipamento() {
    if ($("#IdDescripcion_Edt").val() == '') {
        fjsMensajeGrowl('Se debe ingresar un nombre de equipamento', 'Alerta', 'warning');
    } else {
        var Token = sessionStorage.getItem("Token");

        $.ajax({
            url: UriCors + UriApi + 'Equipamentos/' + _CodEquipamento,
            method: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify({
                'CodEquipamento': $("#IdCodigo_Edt").val(),
                'NomEquipamento': $("#IdDescripcion_Edt").val()
            }),
            headers: {
                'Authorization': 'Bearer ' + Token
            },
            async: false,
            success: function (json) {
                $("#divEditEquipamento").modal('hide');
                fjsMensajeGrowl('Se actualizo correctamente el Equipamento con el código: ' + _CodEquipamento, 'Alerta', 'success');
                ObtieneEquipamentos();
            },
            error: function () {
                fjsMensajeGrowl('No se pudo establecer la conexión', 'Error', 'danger');
            }
        });
    }
}

function IndexEliminar(CodEquipamento) {
    _CodEquipamento = CodEquipamento;
    $("#divEliminaEquipamento").modal('show');
}

function ConfirmaEliminaEquipamento() {
    var Token = sessionStorage.getItem("Token");

    $.ajax({
        url: UriCors + UriApi + 'Equipamentos/' + _CodEquipamento,
        method: 'DELETE',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer ' + Token
        },
        success: function (json) {
            if (json != '') {
                $("#divEliminaEquipamento").modal('hide');
                fjsMensajeGrowl('Se elimino correctamente el Equipamento con el código: ' + _CodEquipamento, 'Alerta', 'success');
                ObtieneEquipamentos();
            }
        },
        error: function () {
            fjsMensajeGrowl('No se pudo establecer la conexión', 'Error', 'danger');
        }
    });
}