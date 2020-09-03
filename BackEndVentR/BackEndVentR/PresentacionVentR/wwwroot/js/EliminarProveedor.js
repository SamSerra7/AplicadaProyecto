


function eliminarProveedor(proveedor, nombreProveedor) {

    $.ajax({
        // url: "@Url.Action("DesactivarProveedor", "Proveedor")",
        url: "/Proveedor/EliminarProveedor",
        type: 'GET',
        data: { "IdProveedor": proveedor },
        success: function (response) {
            console.log(response);

            if (response) {
               
                alert('El proveedor:' + nombreProveedor + ' ha sido eliminado');
                location.reload();
              
            } else
                alert('No se a podido eliminar el proveedor:' + nombreProveedor+", verifica si está asociado a otra tabla.");



        },
        error: function (error) {
        }
    });
}