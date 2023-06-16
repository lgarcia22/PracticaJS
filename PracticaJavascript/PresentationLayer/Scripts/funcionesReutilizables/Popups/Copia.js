function CerrarPopup(idPopup) {
    try {
        $("#" + idPopup).dxPopup("instance").hide();
    }
    catch(e) {
        console.log(e);
    }
}

function AbrirPopup(idPopup, titulo, contenedor, contenido) {
    try {
        if (titulo)
            $(idPopup).dxPopup("instance").option("title", titulo);

        $(idPopup).dxPopup("instance").show();

        if (contenedor && contenido)
            $(contenedor).html(contenido);
    }
    catch (err) {
        console.log(err);
    }
}