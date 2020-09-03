
function activar(idProveedor, nombreProveedor) { activarProveedor(idProveedor, nombreProveedor); }

function desactivar(idProveedor, nombreProveedor) { desactivarProveedor(idProveedor, nombreProveedor); }


function activarProveedor(proveedor, nombreProveedor) {
    $.ajax({
        url: "/Proveedor/ActivarProveedor",
        type: 'GET',
        data: { "IdProveedor": proveedor },
        success: function (response) {
            console.log(response);
            if (response) {
                alert('El proveedor:' + nombreProveedor + ' ha sido activado');
                location.reload();
            } else
                alert('No se a podido activar el proveedor:' + nombreProveedor);
        },
        error: function (error) {
        }
    });
}
function desactivarProveedor(proveedor, nombreProveedor) {

    $.ajax({
       // url: "@Url.Action("DesactivarProveedor", "Proveedor")",
        url: "/Proveedor/DesactivarProveedor",
        type: 'GET',
        data: { "IdProveedor": proveedor },
        success: function (response) {
            console.log(response);

            if (response) {
                alert('El proveedor:' + nombreProveedor + ' ha sido desactivado');
                location.reload();
            } else
                alert('No se a podido desactivar el proveedor:' + nombreProveedor);



        },
        error: function (error) {
        }
    });
}