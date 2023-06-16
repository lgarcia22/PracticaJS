
$(function () {
    CargarEstilos();
   
    $(document).ajaxComplete(function () {
        CargarEstilos();
    });

});

function mostrarError(mensaje) {
    Alert(mensaje, "error");
}

function mostrarToast(tipo, mensaje) {
    Alert(mensaje, tipo);
}

function confirmacionAccion(options)
{
    var title = options.titulo != undefined ? options.titulo : "Confirmaci�n";
    var descripcion = options.descripcion != undefined ? options.descripcion : "Est� seguro de realizar la siguiente acci�n";
    var accion = options.action != undefined ? options.action : accionNoDef;
    return $.confirm({
        title: title,
        content: descripcion,
        type: 'dark',
        animation: 'none',
        buttons: {
            cancel: {
                text: 'Cerrar'
            },
            confirm:
            {
                text: 'Aceptar',
                action: accion
            }
        }
    });
}

function zoom(e) {
    var zoomer = e.currentTarget;
    e.offsetX ? offsetX = e.offsetX : offsetX = e.touches[0].pageX
    e.offsetY ? offsetY = e.offsetY : offsetX = e.touches[0].pageX
    x = offsetX / zoomer.offsetWidth * 100
    y = offsetY / zoomer.offsetHeight * 100
    zoomer.style.backgroundPosition = x + '% ' + y + '%';
}

function colorEstadoTemplate(element, info)
{
    var estado = info.data.ESTADO;
  
    if (estado == "false")
    {
        element.append('<div style = "text-align:center; color:red; font-size: 14px;"><i class="fas fa-thumbs-down" title="Inactivo"></i></div>')
        //element.css("background-color", '#891f12');
    }
    else
    {
        element.append('<div style = "text-align:center; color:green; font-size: 14px;"><i class="fas fa-thumbs-up" title="Activo"></i></div>')
    }
}


//jcastro #Valoraciones
function ValoracionesTemplate(e, info) {

    var Cantidad = info.value;
    var texto;

    if (Cantidad > 2) {

        texto = '<div style = "color:#ffa400; text-align: center; font-size: 14px;">';

        for (var i = 1; i <= Cantidad; i++) {
            texto = texto + '<i class="fas fa-star"></i>';
        }

        texto = texto + ' </div>';
        e.append(texto);

    } else {

        texto = '<div style = "color:#5f5050; text-align: center; font-size: 14px;">';

        for (var i = 1; i <= Cantidad; i++) {
            texto = texto + '<i class="fas fa-star"></i>';
        }

        texto = texto + ' </div>';
        e.append(texto);
    }
    
}

function EstadoTemplate(e, info) {
    var ESTADO = info.value;

    if (ESTADO == true || ESTADO == 'Activo' || ESTADO == 1)
    {
        ESTADO = 'Activo';
        e.append('<div style = "color:#248c52; text-align: center; font-size: 14px;"><i class="fas fa-check-circle" title="' + ESTADO + '"></i> &nbsp' + ESTADO + '</div>');
    }
    if (ESTADO == false || ESTADO == 'Inactivo' || ESTADO == 2)
    {
        ESTADO = 'Inactivo';
        e.append('<div style = "color:#ff0000; text-align: center; font-size: 14px;"><i class="fas fa-exclamation-circle" title="' + ESTADO + '"></i> &nbsp' + ESTADO + '</div>');
    }
}


function CargarEstilos(accion) {

 

    $(".fa-solid .fa-xmark").addClass("fas-icon");
   
    $(".dx-icon-refresh").css("color", "white");
    $(".dx-icon-refresh").parent().parent().addClass("btn-primary");
    $(".dx-icon-export-to").css("color", "white");
    $(".dx-icon-export-to").parent().parent().addClass("btn-info");
    $(".dx-icon-export-excel-button").css("color", "white");
    $(".dx-icon-export-excel-button").parent().parent().addClass("btn-info");
    $(".dx-icon-column-chooser").css("color", "white");
    $(".dx-icon-column-chooser").parent().parent().addClass("btn-warning");

    $("[aria-label='Enviar a Revisar'").addClass("btn-succes");
    $("[aria-label='Enviar'").addClass("btn-succes");
    $("[aria-label='Crear'").addClass("btn-succes");
    $("[aria-label='Anular Informe'").addClass("btn-danger");
    $("[aria-label='Verificar'").addClass("btn-succes");
    $(".dx-button-mode-contained.dx-icon.dx-icon-plus").addClass("dx-icon2");
    $("[aria-label='Detallar'").addClass("btn-info");
    $("[aria-label='Enviar a Notificar'").addClass("btn-succes");
    $("[aria-label='Guardar'").addClass("btn-primary");
    $("[aria-label='Actualizar'").addClass("btn-primary");
    $("[aria-label='Aprobar'").addClass("btn-succes");
    $("[aria-label='Rechazar'").addClass("btn-danger");
    

    
   
}