// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('input[type=text]').forEach(node => node.addEventListener('keypress', e => {
        if (e.keyCode == 13) {
            e.preventDefault();
        }
    }))
});
// Write your Javascript code.
$(document).ready(function () {
    jQuery(document).ready(function ($) {
        $(document).ready(function () {
             

            
        });
    });
    

});
function revisarNumero(evento, id) {
 
    const campoNumerico = document.getElementById(id);
    console.log(campoNumerico.value);
    const teclaPresionada = evento.key;
    const teclaPresionadaEsUnNumero =
        Number.isInteger(parseInt(teclaPresionada));

    const sePresionoUnaTeclaNoAdmitida =
        teclaPresionada != 'ArrowDown' &&
        teclaPresionada != 'ArrowUp' &&
        teclaPresionada != 'ArrowLeft' &&
        teclaPresionada != 'ArrowRight' &&
        teclaPresionada != 'Backspace' &&
        teclaPresionada != 'Delete' &&
        teclaPresionada != 'Enter' &&
        !teclaPresionadaEsUnNumero;
    const comienzaPorCero =
        campoNumerico.value.length === 0 &&
        teclaPresionada == 0;

    

    if (sePresionoUnaTeclaNoAdmitida || comienzaPorCero) {
        evento.preventDefault();
    }

    if (campoNumerico.value == 0) {
        campoNumerico.value = 1;
    }
    if (campoNumerico.value < 0) {
        campoNumerico.value = 1;
    }
};

function revisarNumero2(evento, id) {
    console.log(evento);
    const campoNumerico = document.getElementById(id);
    const teclaPresionada = evento.key;
    const teclaPresionadaEsUnNumero =
        Number.isInteger(parseInt(teclaPresionada));

    const sePresionoUnaTeclaNoAdmitida =
        teclaPresionada != 'ArrowDown' &&
        teclaPresionada != 'ArrowUp' &&
        teclaPresionada != 'ArrowLeft' &&
        teclaPresionada != 'ArrowRight' &&
        teclaPresionada != 'Backspace' &&
        teclaPresionada != 'Delete' &&
        teclaPresionada != 'Enter' &&
        !teclaPresionadaEsUnNumero;
    const comienzaPorCero =
        campoNumerico.value.length === 0 &&
        teclaPresionada == 0;



    if (sePresionoUnaTeclaNoAdmitida || comienzaPorCero) {
        evento.preventDefault();
    }

    

};



function validaNegativosyMayores100(evento, id) {
    console.log(evento);
    const campoNumerico = document.getElementById(id);
    const teclaPresionada = evento.key;
    const teclaPresionadaEsUnNumero =
        Number.isInteger(parseInt(teclaPresionada));

    const sePresionoUnaTeclaNoAdmitida =
        teclaPresionada != 'ArrowDown' &&
        teclaPresionada != 'ArrowUp' &&
        teclaPresionada != 'ArrowLeft' &&
        teclaPresionada != 'ArrowRight' &&
        teclaPresionada != 'Backspace' &&
        teclaPresionada != 'Delete' &&
        teclaPresionada != 'Enter' &&
        !teclaPresionadaEsUnNumero;
    const comienzaPorCero =
        campoNumerico.value.length === 0 &&
        teclaPresionada == 0;



    if (sePresionoUnaTeclaNoAdmitida ) {
        evento.preventDefault();
    }

    if (campoNumerico.value < 0 ) {
        campoNumerico.value = 0;
    }
    if (campoNumerico.value > 100) {
        campoNumerico.value = 100;
    }

};


function validaNegativos(evento, id) {
    console.log(evento);
    const campoNumerico = document.getElementById(id);
    const teclaPresionada = evento.key;
    const teclaPresionadaEsUnNumero =
        Number.isInteger(parseInt(teclaPresionada));

    const sePresionoUnaTeclaNoAdmitida =
        teclaPresionada != 'ArrowDown' &&
        teclaPresionada != 'ArrowUp' &&
        teclaPresionada != 'ArrowLeft' &&
        teclaPresionada != 'ArrowRight' &&
        teclaPresionada != 'Backspace' &&
        teclaPresionada != 'Delete' &&
        teclaPresionada != 'Enter' &&
        !teclaPresionadaEsUnNumero;
    const comienzaPorCero =
        campoNumerico.value.length === 0 &&
        teclaPresionada == 0;



    if (sePresionoUnaTeclaNoAdmitida) {
        evento.preventDefault();
    }

    if (campoNumerico.value < 0) {
        campoNumerico.value = 0;
    }
  

};


function formatoDecimal(numero) {
    var number = numero;
    var decimal = "."; 
    if (numero.split(".")[1] != undefined) {
        decimal += numero.split(".")[1];
    } else {
        decimal += "00";
    }
    var opciones = {
        maximumFractionDigits: 2, minimumFractionDigits: 2,
        useGrouping: false
    };
    // En el alemán la coma se utiliza como separador decimal y el punto para los millares
    //if (new Intl.NumberFormat("en-US").format(number).split(".")[1] != undefined) {
    //    return new Intl.NumberFormat("en-US",opciones).format(number);
    //}
    return new Intl.NumberFormat("en-US").format(number); //+ decimal;
}

