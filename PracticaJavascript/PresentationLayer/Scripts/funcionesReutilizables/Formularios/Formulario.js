function EjecutarAccionForm(urlAction, idFomulario, idPoPup, idGrid) {
    //Obtenemos instancia del formulario
    var form = $("#" + idFomulario).dxForm("instance");
    //Validamos si el formulario es válido
    if (!form.validate().isValid) {
        Alert("Error, campos requeridos están vacíos", "error");
        return;
    }
    //Obtenemos la información en sí del formulario
    MostrarPanelLoad();
    form = form.option("formData");
    $.ajax({
        //Url de la acción en sí. Llamado al método dentro de un controlador.
        url: urlAction,
        //Protocolo HTTP
        type: 'POST',
        //Especificar si nuestra función es asíncrona
        async: true,
        //Objeto de parámetro del controlador
        data: { model: form },
        //Bloque de ejecución exitosa de un controlador.
        success: function (resp) {
           /* funcionExito && funcionExito();*/
         
            Alert(resp, "success");
            CerrarPopup(idPoPup);
            RefrescarGrid(idGrid);
            OcultarPanelLoad();

            $("#Editar").dxButton("instance").option("disabled", true);
            $("#Eliminar").dxButton("instance").option("disabled", true);

        },
        //Bloque de ejecución errónea de un controlador.
        error: function (error) {
            Alert(error.responseText, "error");
        }
    });
}


function EjecutarAccionForm2(urlAction, idFomulario) {
    //Obtenemos instancia del formulario
    var form = $("#" + idFomulario).dxForm("instance");
    //Validamos si el formulario es válido
    if (!form.validate().isValid) {
        Alert("Error, campos requeridos están vacíos", "error");
        return;
    }
    //Obtenemos la información en sí del 
    form = form.option("formData");
    $.ajax({
        //Url de la acción en sí. Llamado al método dentro de un controlador.
        url: urlAction,
        //Protocolo HTTP
        type: 'POST',
        //Especificar si nuestra función es asíncrona
        async: true,
        //Objeto de parámetro del controlador
        data: { model: form },
        //Bloque de ejecución exitosa de un controlador.
        success: function (resp) {
        },
        //Bloque de ejecución errónea de un controlador.
        error: function (error) {
            Alert(error.responseText, "error");
        }
    });
}

function ValidarFechaMinima(e, idCampoFecha) {
    $("#" + idCampoFecha).dxDateBox("instance").option("min", e.value)
}
