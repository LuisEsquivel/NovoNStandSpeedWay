
document.addEventListener("DOMContentLoaded", function (event) {
    window.list("/Activos/List", ["Id", "Descripción", "Activo", "Fecha Alta"], 0, null);
});



function GetInfoById(id) {
    window.GetById("/Activos/GetById", id);
}

function ReturnData(url) {
    return $.ajax({
        url: url
    });
}


function GetTraining(id) {
    window.list("/Activos/List", ["Id", "Descripción", "Activo", "Fecha Alta"], 0, null);
}



async function Add() {
    var form = document.getElementById("form");
    await window.add("/Activos/Add", form, ["Id", "Descripción", "Activo", "Fecha Alta"])
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