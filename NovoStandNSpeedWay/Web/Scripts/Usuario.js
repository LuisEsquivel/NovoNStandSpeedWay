
//navegadores modernos
document.addEventListener("DOMContentLoaded", function (event) {
    window.list("/Usuarios/Listar", ["Id", "Nombre", "Rol", "Activo", "Fecha Alta"], 0, null);
    $.get("/Usuarios/listarRoles", function (data) {
        llenarCombo(data, document.getElementById("RolIdInt"), true)
    });
});


function GetInfoById(id) {
    window.GetById("/Usuarios/GetById", id);
}

function ReturnData(url) {
    return $.ajax({
        url: url
    });
}


function GetTraining(id) {
    window.list("/Usuarios/List", ["Id", "Nombre", "Rol", "Activo", "Fecha Alta"], 0, null);
}



function Add() {
    var form = document.getElementById("form");
    window.add("/Usuarios/Add", form, ["Id", "Nombre", "Rol", "Activo", "Fecha Alta"])
}


function ChangeInfoStatus(id) {
    window.ChangeStatus("/TipoDeSeguimiento/ChangeStatus", id, ["Id", "Nombre", "Rol", "Activo", "Fecha Alta"]);
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

//METODO GENERICO PARA LLENAR LOS COMBOBOX
function llenarCombo(data, control, primerElemento) {
    var contenido = "";

    if (primerElemento == true) {
        contenido += "<option value=''>---SELECCIONE---</option>"
    }

    for (var i = 0; i < data.length; i++) {
        contenido += "<option value='" + data[i].IID + "'>";
        contenido += data[i].NOMBRE;
        contenido += "</option>";
    }

    control.innerHTML = contenido;
}
