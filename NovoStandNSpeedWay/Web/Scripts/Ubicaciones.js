
document.addEventListener("DOMContentLoaded", function (event) {
    window.list("/Ubicaciones/List", ["Id", "Descripción", "Activo", "Fecha Alta"], 0, null);
});

document.addEventListener(("click") , () => {

        if ($("#Accion").val() == "update") {
            $("#UbicacionIdVar").prop("readonly", true);
        } else {
            $("#UbicacionIdVar").prop("readonly", false);
        }

});


function GetInfoById(id) {
    window.GetById("/Ubicaciones/GetById", id);
    $("#Accion").val("update");
}

function ReturnData(url) {
    return $.ajax({
        url: url
    });
}


function GetTraining(id) {
    window.list("/Ubicaciones/List", ["Id", "Descripción", "Activo", "Fecha Alta"], 0, null);
}



async function Add() {
    var form = document.getElementById("form");
    await  window.add("/Ubicaciones/Add", form, ["Id", "Descripción", "Activo", "Fecha Alta"])
}



function Modal(url) {
    window.AbrirModal(url);
}


function Cerrar() {

    $('#Modal iframe').attr('src', '');
    $("#Modal").modal("hide");
    window.modal_open = false;

}



$(document).keyup(function (e) {
    if (e.which == 27) {

        if (window.maximized == false) {
            window.Cerrar();
        }

        if (window.maximized == true) {
            window.MinimizeModal();
            window.AbrirModal();
        }

    }
});