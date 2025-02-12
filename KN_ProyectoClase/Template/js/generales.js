function ConsultarNombre () {
    let identificacion = $("#Identificacion").val();
    $("#Nombre").val("");

    // NO existe cedula con menos de 9 digitos
    if (identificacion.length >= 9) {
        $.ajax({
            url: `https://apis.gometa.org/cedulas/${identificacion}`,
            method: "GET",
            datatype: "json",
            success: (data) => {
                $("#Nombre").val(data.nombre);
            }
        });
    }
}