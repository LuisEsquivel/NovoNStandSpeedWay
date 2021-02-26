
document.addEventListener("DOMContentLoaded", function (event) {
    window.list("/Contacto/List", ["Id", "Nombre", "Solucion", "Teléfono", "Whatsapp", "Porcentaje", "Es Integrador", "Activo", "Fecha Alta"], 0, null);

    $.get("/Home/listarComoSeEntero", function (data) {
        window.llenarCombo(data, document.getElementById("ComoSeEnteroIdInt"), true);
    });

});


function GetInfoById(id) {
    window.GetById("/Contacto/GetById", id);
}

function ReturnData(url) {
    return $.ajax({
        url: url
    });
}


function GetTraining(id){
    window.list("/Contacto/List", ["Id", "Nombre", "Solucion", "Teléfono", "Whatsapp", "Porcentaje", "Es Integrador", "Activo" ,"Fecha Alta"], 0, null);
}



 function Add() {
   var form = document.getElementById("form");
    window.add("/Contacto/Add", form, ["Id", "Nombre", "Solucion", "Telefono", "Whatsapp", "Porcentaje Cierre", "Es Integrador", "Activo", "Fecha Alta"])
}

function ChangeInfoStatus(id) {
    window.ChangeStatus("/Contacto/ChangeStatus", id, ["Id", "Nombre", "Solucion", "Teléfono", "Whatsapp", "Porcentaje", "Es Integrador", "Activo", "Fecha Alta" ]);
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


