

var ErrorAlEliminar = "Error al eliminar";
var EliminadoCorrectamente = "Se eliminó correctamente";
var InformacionEnviada = "Información Enviada";
var InformacionNoEnviada = "Información No Enviada";
var InformaciónAlmacenada = "Información Almacenada";
var NoSeEncontroInformacion = "No Se Encontró Información";
var NoSePudoObtenerLaInformacion = "No se pudo obtener la Información";
var Error = "Ocurrió un Error";
var idTable = "";



var sinInfo = "<button type='button' value='NUEVO' class='btn btn-success ml-5' onclick='AbrirFormulario(1);'> <i class='fa fa-user-plus'> NUEVO</i></button>"
            + "<h5 class='ml-2 mt-2' id='Title'>" + $("#Titulo").text() + "</h5>"
            + "<center clas>"
            + "<h1>No hay información para mostrar!!</h1>"
            + "</center>";

var InfoError = "<center>"
            + "<h1>No se pudo obtener la información!!</h1>"
            + "</center>";

var spinner =  "<center>"
              +"<h1>Cargando...</h1>"
              +"<div class='preloader-wrapper big active'> " 
              +"<div class='spinner-layer spinner-blue-only' >"
              +"<div class='circle-clipper left'>"
              +"<div class='circle'></div>"
              +"</div><div class='gap-patch'>"
              +"<div class='circle'></div>"
              +"</div><div class='circle-clipper right'>"
              +"<div class='circle'></div>"
              +"</div>"
              +"</div >"
              + "</div >"
              +"</center>";


var formSearch = "<form method='POST' id='formSearched' class='row container-fluid'>"
                +"<div class='ml-auto form-inline'>"
                +"<label for='searched' class='col-form-label mr-3'>Buscar</label>"
                +"<div class='form-inlinemb-3'>"
                + "<input type='text' class='form-control' id='searched' name='searched' />"
                + "</div>"
                + "</div >"
                + "</form >";

function TableDesign(idTable) {

  
    document.getElementById("footer").classList.remove("fixed-bottom");

    $("#" + idTable + "_length").addClass("text-primary form-inline");
    $("#" + idTable + "_filter").addClass("text-primary");
    $("#" + idTable + "_info").addClass("text-primary");
    $("#" + idTable + "_paginate").addClass("text-primary");
    $(".form-control-sm").addClass("text-primary");

    var groupColumn = 1;

    // Order by the grouping
    $('#' + idTable + ' tbody').on('click', 'tr.group', function () {
        var currentOrder = table.order()[0];
        if (currentOrder[0] === groupColumn && currentOrder[1] === 'asc') {
            table.order([groupColumn, 'desc']).draw();
        }
        else {
            table.order([groupColumn, 'asc']).draw();
        }
    });


}



function centrar(id) {
    var nav = document.getElementById("nav");
    var footer = document.getElementById("footer");
    footer.classList.add("fixed-bottom");
    var height = nav.clientHeight + footer.clientHeight;
    var objeto = document.getElementById(id);
    var centrar = window.innerHeight - height;
    objeto.style.paddingTop = 0;
    objeto.style.cssText = "padding-top:" + (centrar - (centrar -1)) + "px !important";
}


$(document).ready(function () {

    //userLogeado();

    //cargar imágen
    $('#file').change(function (e) {
        addImage(e);
    });

    $("#ActivoBit").change(() => {
        $("#IsActive").val($("#ActivoBit").prop("checked"));
    }); 
    $("#EsIntegradorBit").change(() => {
        $("#EsIntegrador").val($("#EsIntegradorBit").prop("checked"));
    }); 
    

    if ($("#Modal").length > 0) {
        var modal = document.getElementById("Modal");
        modal.style.cssText = "background-color:transparent !important; width:100% !important; overflow-y:hidden; max-height:100% !important;";
    }


    if ($("#frmLogin").length > 0) {
        centrar("frmLogin");
        document.getElementById("footer").classList.remove("fixed-bottom");
    }

    if ($("#frmRegister").length > 0) {
        centrar("frmRegister");
        document.getElementById("footer").classList.remove("fixed-bottom");
    }

    if ($("#frmOlvideContraseña").length > 0) {
        centrar("frmOlvideContraseña");
        document.getElementById("footer").classList.remove("fixed-bottom");
    }



    if ($("#frmVerificarCuenta").length > 0) {
        centrar("frmVerificarCuenta");
        document.getElementById("footer").classList.remove("fixed-bottom");
    }

    

    if ($("#ChkFiltros").length > 0) {

        HabilitarFiltros();

        $("#ChkFiltros").change(() => {
            HabilitarFiltros();
       }); 
    
    }


    if ($("#BtnClearDateInicial").length > 0) {
        $("#BtnClearDateInicial").click(() => {
            if ($("#FechaInicialDate").length > 0) {
                $("#FechaInicialDate").val("");
                $("#FechaFinalDate").val("");
                $("#FechaInicialDate").keypress();
            }
        });
    }


    if ($("#BtnClearDateFinal").length > 0) {
        $("#BtnClearDateFinal").click(() => {
            if ($("#FechaFinalDate").length > 0) {
                $("#FechaFinalDate").val("");
                $("#FechaFinalDate").keypress();
            }
        });
    }

    if ($("#FechaInicialDate").length > 0) {
        $("#FechaInicialDate").on("keyup change", () => {
            $("#FechaFinalDate").val(  $("#FechaInicialDate").val()  );
        });
    }


    //$.get("/Home/UsuarioLogeado", function (data) {
    //    if (data.length > 0) {
    //        document.getElementById("userLogeado").innerHTML =data;
    //    }

    //})

 
});


function userLogeado()
{

    $.ajax({
        type: 'GET',
        url: '/Home/UsuarioLogeado',
        data: $(this).serialize(),
        success: function (data) {
            if (data.length > 0) {
                document.getElementById("userLogeado").innerHTML = data;
            }
        }
    });

    //$.ajax({
    //    type: "POST",
    //    url: "/Home/UsuarioLogeado",
    //    contentType: false,
    //    processData: false,
    //    success: function (data) {
    //        if (data.length > 0) {
    //            document.getElementById("userLogeado").innerHTML = data;
    //        }
    //    }
    //})
}


function HabilitarFiltros() {
    $('#formSearch').find('input, textarea, button, select').val("");
    $('#formSearched').find('input').val("");

    if ($("#ChkFiltros").prop("checked")) {
        $('#formSearch').find('input, textarea, button, select').attr('disabled', false);
        $('#formSearch').show();

        $('#formSearched').hide();
    } else {
        $('#formSearch').find('input, textarea, button, select').attr('disabled', true);
        $('#formSearch').hide();

        $('#formSearched').show();
    }
}



language_data =
    {

        "sProcessing": "Procesando...",
        "sLengthMenu": "Mostrar _MENU_ registros",
        "sZeroRecords": "No se encontraron resultados",
        "sEmptyTable": "Ningún dato disponible en esta tabla =(",
        "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
        "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
        "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
        "sInfoPostFix": "",
        "sSearch": "Buscar:",
        "sUrl": "",
        "sInfoThousands": ",",
        "sLoadingRecords": "Cargando...",
        "oPaginate": {
            "sFirst": "Primero",
            "sLast": "Último",
            "sNext": "Siguiente",
            "sPrevious": "Anterior"
        },
        "oAria": {
            "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
            "sSortDescending": ": Activar para ordenar la columna de manera descendente"
        },
        "buttons": {
            "copy": "Copiar",
            "colvis": "Visibilidad"
        }

    }


jQuery(document).ready(function () {
    jQuery('.soloNumeros').keypress(function (tecla) {
        if (tecla.charCode < 48 || tecla.charCode > 57) return false;

    });
});



var add = async function (urlAdd, form, arrayColumnas, BtnAddUser = false) {


    form.onsubmit = async (e) => {
        e.preventDefault();

        $.ajax({
            type: "POST",
            url: urlAdd,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: async function (result) {


                if (JSON.stringify(result) == '{}') {
                    swal(Error, "", "warning"); 
                    return
                }

                if (result.message == "Correo Enviado") {
                    swal(InformacionEnviada, "", "success");
                    LimpiarFormulario();
                    return;
                }
                if (result.message == "Problema al enviar correo") {
                    swal(Error, InformacionNoEnviada, "warning");
                    return;
                }

                if (JSON.stringify(result.message).includes("Ya Existe")) {
                    swal(result.message, "", "warning");
                    return;
                }


                swal(InformaciónAlmacenada, "", "success");
                LimpiarFormulario();
                CerrarFormulario();
                Table(arrayColumnas, result.message, true, BtnAddUser);
            }
        });



    }

}


var list = function (url, arrayColumnas, id = 0, ClasificacionID = 0, columnaAcciones = true, BtnAddUser = true) {

    var container = document.getElementById("container");
    container.innerHTML = spinner;
    centrar("container");

    var data = {
        "id": id,
        "ClasificacionID": ClasificacionID
    }

    $.ajax({
        method: "GET",
        url: url,
        data: data, /*parámetros enviados al controlador*/
        contentType: "application/json;charset=utf-8",
        dataType: "json",

        success: function (data) {

          
            if (!$.trim(data)) {
                swal(NoSeEncontroInformacion, "", "warning");
                $("#container").empty();
                container.innerHTML = sinInfo;
            } else {
                Table(arrayColumnas, data, columnaAcciones, BtnAddUser);    
            }


        },
        error: function () {
            console.log("No se ha podido obtener la información");
            $("#container").empty();
            container.innerHTML = InfoError;
        }
    });

}






function Table(arrayColumnas, data, columnaAcciones = true, BtnAddUser = true) {

    var container = document.getElementById("container");;
    container.classList.add("container");
    var contenido = "";

    var keys = Object.keys(data[0]);

    contenido += "<div class='justify-content-start mb-5  form-inline'>";

    if (BtnAddUser) {
     contenido += "<button type='button' value='NUEVO' class='btn btn-success' onclick='AbrirFormulario(1);'> <i class='fa fa-user-plus'> NUEVO</i></button>";
    }

    if (keys.includes("CapacitacionID")) {
        contenido += "<input type='button' value='REGRESAR' class='btn btn-secondary ml-2' onclick='ShowClassification();'/>";
    }

    contenido += " <h2 class='ml-2 mt-2 text-center font-weight-bold' id='Title'  style='color:#F08034;' >" + $("#Titulo").text() + "</h2>"
    contenido += "</div>";

    if (data == null) {
        container.innerHTML = contenido;
    }

    if (data.length == 0) {

        if ($(idTable).length > 0) {
            $(idTable).DataTable().clear().draw();
        }
        return;
    }


    idTable = "Table" + $("#Titulo").text().replace(/\s+/g, "");
    contenido += "<table class='table' id='" + idTable + "' >";
    contenido += "<thead class='bg-dark text-white vh100'>";
    contenido += "<tr>";

    for (i = 0; i < arrayColumnas.length; i++) {

        var theadColumn = arrayColumnas[i];

        if (theadColumn.includes("_")) {
            theadColumn = theadColumn.replace("_", " ");
        }

        contenido += "<th>";
        contenido += theadColumn;
        contenido += "</th>";
    }

    if (columnaAcciones) {
    contenido += "<th class='text-center'> Editar </th>";
    //contenido += "<th class='text-center'> Eliminar </th>";
    }

    contenido += "</tr>";
    contenido += "</thead>";


    var id;

    contenido += "<tbody>";
    for (row = 0; row < data.length; row++) {

        contenido += "<tr>";

        for (celda = 0; celda < keys.length; celda++) {

            var cell = keys[celda];
          
            //if cell is an Image

            if (cell == "Imagen") {
                contenido += "<td>";
                contenido += "<img src=" + data[row][cell] + " class='img-fluid' style='width: 100px; height: 100px;'>";
                contenido += "</td>";
            }


            if ( !cell.includes("Imagen") && !cell.includes("Pdf") && !cell.includes("Video") ) {
                contenido += "<td>";
                contenido += data[row][cell];
                contenido += "</td>";
            }
           

            
            /*Get Id For Filter or Delete(EstadoActivo==false)*/
            if (celda == 0) {
                id = data[row][cell];

                //verificamos si el id contiene letras y si es así lo ponemos entre comillas
                const regex = /^[0-9]*$/;
                if (regex.test(id) == false) { id = '"' + id + '"'; }

            }


            if (cell.includes("Pdf")) {
                var pdf = data[row][cell];
                contenido += "<td class='text-center'>";
                if (pdf != null) {
                    pdf = '"' + pdf + '"';
                    contenido += "<img class='img' onclick='Modal(" + pdf + ");' src='../img/PDF.png'/>"

                }
                contenido += "</td>";
            }

            if (cell.includes("Video")) {
                var video = data[row][cell];
                contenido += "<td class='text-center'>";

                if (video != null) {
                    video = '"' + video + '"';
                    contenido += "<img class='img p-0' onclick='Modal(" + video + ");' src='../img/iconyoutube.png' />"
                }
                contenido += "</td>";
            }

        }


        if (columnaAcciones) {

        contenido += "<td class='text-center'>";
        contenido += "<button id='BtnEditar' class='editar btn btn-secondary btn-sm  redondo' onclick='GetInfoById(" + id + ");'>  <i class='fa fa-pencil-square-o fa-lg' aria-hidden='true'></i></button> ";
        contenido += "</td>";

        }


        contenido += "</tr>";

    }//end data

    contenido += "</tbody>";
    contenido += "</table>";

 
    $("#container").css("height", "");
    container.innerHTML = contenido;

    $("#" + idTable).DataTable({
        //dom: 'Bfrtip',
        scrollY: true,

        lengthMenu: [
            [7, 14, 21, -1],
            ['7', '14', '21', 'Todo']
        ],
        buttons: [
            'pageLength',
        ],

        order: [[0, "desc"]],

        language: language_data
    });


    TableDesign(idTable);
}





var GetById = function (url, id) {

    var parameters = {
        "id": id,
    };


    $.ajax({
        method: "POST",
        url: url,
        data: parameters, /*parámetros enviados al controlador*/
        dataType: "json",


        success: function (data) {
            AbrirFormulario(2);
            LlenarFormulario(data);
            $("#IsActive").val($("#ActivoBit").prop("checked"));
            $("#EsIntegrador").val($("#EsIntegradorBit").prop("checked"));
      
        },
        error: function () {
            console.log("No se ha podido obtener la información");
            swal(NoSePudoObtenerLaInformacion, "", "warning");

        }
    });


}



function LlenarFormulario(data) {

    //obtenemos los keys del JSON ejemplo: ID, NOMBRE...
    var keys = Object.keys(data[0]);

    for (row = 0; row < data.length; row++) {

        //recorremos las keys
        for (k = 0; k < keys.length; k++) {

            //celda actual
            var celda = keys[k];

            //obtenemos el valor de la celda
            var value = data[row][celda];
       

            //agregamos el valor obtenido al control mediante jquery el nombre del ID debe ser igual que la celda
            if (celda == "Imagen") {
                $("#" + celda).attr("src", value);
            } else {


                //se tuvo que hacer así ya que no pintaba el input aún borrando datos de navegación
                if (celda == "Titulo") {
                    var ajas = $("."+celda).val(value);
         
                } else {
                    $("#" + celda).val(value);
   
                    if ( ($("#" + celda).attr("type")) == 'checkbox') {
                        $("#" + celda).prop('checked', value);                           

                    }
                }
               
            }

        }//end for keys
    }

}




function addImage(e) {
    var file = e.target.files[0],
        imageType = /image.*/;

    if (!file.type.match(imageType))
        return;

    var reader = new FileReader();

    reader.onload = function (e) {
        var result = e.target.result;
        $('#Imagen').attr("src", result);
    }

    reader.readAsDataURL(file);
}





function LimpiarFormulario() {
    $("#form").trigger('reset');
}

function AbrirFormulario(operacion) {

    //document.getElementById("BtnAdd").classList.add("mt-3");
    //if ($("#BtnCancelar").length > 0) {
    //    document.getElementById("BtnCancelar").classList.add("mb-1");
    //}


    if (operacion == 1) {
        LimpiarFormulario();
        $("#BtnUpdate").hide();
        $("#BtnAdd").show();
        $("#ModalTitle").text("Agregar " + $("#Titulo").text());
        $("#DivIsActive").hide(); 
    }

    if (operacion == 2) {
        $("#BtnUpdate").show();
        $("#BtnAdd").hide();
        $("#ModalTitle").text("Actualizar " + $("#Titulo").text());
        $("#DivIsActive").show();
        document.getElementById("BtnAdd").classList.remove("mt-3");
        if ($("#BtnCancelar").length > 0) {
         document.getElementById("BtnCancelar").classList.remove("mb-1");
        }
      
    }

    operacion = null;
    $('#Modal').modal('show')
}



function CerrarFormulario() {
    LimpiarFormulario();
    $('#Modal').modal('hide')
}



function Cancelar() {
    LimpiarFormulario();
    CerrarFormulario();
}





function AbrirModal(url) {

    if (modal_open == false) {
        $("#FileUrl").attr("src", url);
        $("#FileUrl")[0].src += "?autoplay=1";
    }
    

    //para mantenerlo estático al presionar fuera del modal y si se presiona ESC que se cierre
    $('#Modal').modal({ backdrop: 'static', keyboard: true })

    var modal = "display: block; padding-rigth: 17px;";
    $(".modal").removeAttr("style");
    $(".modal").attr("style", modal);
 
    $("#Modal").modal("show");

    modal_open = true;
}


var style = "position: fixed;"
            "top: 0px;"
            "left: 0px;"
            "bottom: 0px;"
            "right: 0px;"
            "width: 100 %;"
            "height: 100%;"
            "border: none;"
            "margin: 0;"
            "padding: 0;"
            "overflow: hidden;"
            "z-index: 999999;";


var maximized = false;
var modal_open = false;


function MaximizeModal() {
    $("#FileUrl").attr("style", style);
    $("#MinimizeButtons").css("z-index", "1");
    $("#MinimizeButtons").fadeIn();
    maximized = true;
}

function MinimizeModal() {
    $("#FileUrl").removeAttr("style", style);
    $("#MinimizeButtons").hide();
    maximized = false;
}




jQuery(document).ready(function () {
    jQuery('.soloNumeros').keypress(function (tecla) {
        if (tecla.charCode < 48 || tecla.charCode > 57) return false;
    });
});




 var Filtrar = async function(frm, ControllerAction) {


        let response = await fetch(ControllerAction, {
            method: 'POST',
            processData: false,
            contentType: false,
            body: new FormData(frm)
        });

       let result = await response.json();

     if (   ControllerAction == "/Seguimiento/Filtrar"
         || ControllerAction == "/Seguimiento/Buscar"
         || ControllerAction == "/Seguimiento/Imprimir"
        ) {
            return result;
        }

        if (result != null) {
            Table(arrayColumnas, result);
        }
       

}




var Login = async function () {

    var container = document.getElementById("container");
    container.innerHTML = spinner;


    var frm = document.getElementById("frmLogin");

    frm.onsubmit = async (e) => {

        e.preventDefault();
      

        let response = await fetch("/Login/Access", {
            method: 'POST',
            processData: false,
            contentType: false,
            body: new FormData(frm)
        });

        let result = await response.json();

        if (result == 0) {
            swal("¡Usuario o Contraseña Incorrectos!", "Verifique", "warning");
        } else {

       
            if (result == "/Registrarse/VerificarCuenta") {
                window.location.href = result;
            } else {

                swal("Bienvenido " + result + " :)")
                    .then(() => {
                        window.location.href = "/Home/Index";
                        return;
                    });
            }

          
        }

    }

    $("#container").empty();
}





var Register = async function () {

    var container = document.getElementById("container");
    container.innerHTML = spinner;


    var frm = document.getElementById("frmRegister");

    frm.onsubmit = async (e) => {

        e.preventDefault();

        let response = await fetch("/Registrarse/Add", {
            method: 'POST',
            processData: false,
            contentType: false,
            body: new FormData(frm)
        });

        let result = await response.json();

        if (result == 1) {
            window.location.href = "/Registrarse/VerificarCuenta";   
        } else {

            if (JSON.stringify(result.message).includes("Ya Existe")) {
                swal(result.message, "", "warning");
                return;
            }
            swal("¡Algo salió mal al registrarse!", "Verifique", "warning");
        }

    }

    $("#container").empty();

}



var ValidarCuenta = async function () {

    var container = document.getElementById("container");
    container.innerHTML = spinner;

    var frm = document.getElementById("frmVerificarCuenta");

    frm.onsubmit = async (e) => {

        e.preventDefault();

        let response = await fetch("/Registrarse/ValidarCuenta", {
            method: 'POST',
            processData: false,
            contentType: false,
            body: new FormData(frm)
        });

        let result = await response.json();

        if (result == 1) {

            swal("Cuenta Verificada")
                .then(() => {
                    window.location.href = "/Home/Index";
                });
            s
        } else {
            swal("¡Revisa el código de verificación y vuelve a intentarlo!", "Verifique", "warning");
        }

    }

    $("#container").empty();

}





var EnviarCodigoDeVerificacion = async function () {

    var container = document.getElementById("container");
    container.innerHTML = spinner;

    var frm = document.getElementById("frmOlvideContraseña");

    frm.onsubmit = async (e) => {

        e.preventDefault();

        let response = await fetch("/Registrarse/EnviarCodigoDeVerificacion", {
            method: 'POST',
            processData: false,
            contentType: false,
            body: new FormData(frm)
        });

        let result = await response.json();

        if (result == 1) {
            window.location.href = "/Registrarse/ActualizarContraseña";
        } else {
            swal(result, "Verifique", "warning");
        }

    }

    $("#container").empty();

}




var UpdatePassword = async function () {

    var container = document.getElementById("container");
    container.innerHTML = spinner;

    var frm = document.getElementById("frmUpdatePassord");

    frm.onsubmit = async (e) => {

        e.preventDefault();

        let response = await fetch("/Registrarse/UpdatePassword", {
            method: 'POST',
            processData: false,
            contentType: false,
            body: new FormData(frm)
        });

        let result = await response.json();

        if (result == 1) {

            swal("Tu Contraseña Se Actualizó Con Exito")
                .then(() => {
                    window.location.href = "/Home/Index";
                });
          
        } else if (result == 0) {
            swal("Las Contraseñas No Coinciden", "Verifique", "warning");
        } else if (result == -1) {
            swal("Algo salió mal... Intentalo Más Tarde", "Verifique", "warning");
        } else if (result == 2)
        {
            swal("El Código de verificación no es correcto", "Verifique", "warning");
        }

    }

    $("#container").empty();

}



//METODO GENERICO PARA LLENAR LOS COMBOBOX
function llenarCombo(data, control, primerElemento) {
    var contenido = "";

    if (primerElemento == true) {
        contenido += "<option value='' selected>---SELECCIONE---</option>"
    }

    for (var i = 0; i < data.length; i++) {
        contenido += "<option value='" + data[i].IID + "'>";
        contenido += data[i].NOMBRE;
        contenido += "</option>";
    }

    control.innerHTML = contenido;
}



//Google Signing
async function GoogleSignIn(googleUser) {


    if (Registers == false) { return; }

    var profile = await googleUser.getBasicProfile();

    return SignInGoogle(profile);

    Registers = false;

}


async function SignInGoogle(profile) {


    var parameters = {
        "NombreVar": await profile.getName(),
        "UsuarioVar": await profile.getEmail(),
        "Password": "GoogleAccount",
        "GoogleAccount": true
    }


  
    $.ajax({
        method: "POST",
        url: "/Registrarse/Add",
        data: parameters, /*parámetros enviados al controlador*/
        dataType: "json",

        success: await function (result) {

            if (result == 1) {
                swal("Bienvenido " + profile.getName() + " :)")
                    .then(() => {
                        window.location.href = "/Home/Index";
                    });

            } else {
                swal("Algo salió mal al Iniciar Sesión", "", "warning");

            }

        },
        error: await function () {
            swal("Algo salió mal al Iniciar Sesión", "", "warning");
        }
    });


}



var Registers = false;
function Registrar() {
    Registers = true;
}