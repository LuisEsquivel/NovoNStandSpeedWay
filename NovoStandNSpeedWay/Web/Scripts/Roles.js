
//navegadores modernos
document.addEventListener("DOMContentLoaded", function (event) {
    window.list("/Roles/List", ["Id", "Nombre", "Activo", "Fecha Alta"], 0, null);
});


function GetInfoById(id) {
    window.GetById("/Roles/GetById", id);
}

function ReturnData(url) {
    return $.ajax({
        url: url
    });
}


function GetTraining(id) {
    window.list("/Roles/List", ["Id", "Nombre", "Activo", "Fecha Alta"], 0, null);
}



function Add() {
    var form = document.getElementById("form");
    window.add("/Roles/Add", form, ["Id", "Nombre", "Activo", "Fecha Alta"])
}


function ChangeInfoStatus(id) {
    window.ChangeStatus("/TipoDeSeguimiento/ChangeStatus", id, ["Id", "Nombre", "Activo", "Fecha Alta"]);
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