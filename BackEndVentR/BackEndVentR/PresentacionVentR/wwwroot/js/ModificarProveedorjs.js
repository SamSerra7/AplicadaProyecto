$(document).ready(function () {
    $("#btnEnviar").click(function () {
        var idP = $('#IdProveedor').val();
        var mensaje_error = [];
        var error = document.getElementById("mensaje");
        error.style.color = 'red';
        var proveedor = {
            IdProveedor: Number(idP),
            'Nombre': $('#Nombre').val(),
        };
        var nombre = $('#Nombre').val();
        if (nombre == "" || nombre == null) {
            mensaje_error.push('**Ingrese un nombre para el proveedor');
        } else {
            modificarProveedor(proveedor);
        }
        error.innerHTML = mensaje_error.join(', ') + '\n';

    });
});
function modificarProveedor(proveedor) {
    $.ajax({
        url:'/Proveedor/Modificar', //"@Url.Action("Modificar", 'Proveedor')",
        type: 'put',
        contentType: 'application/json',
        data: JSON.stringify(proveedor),
        success: function (response) {
            console.log(response);
            if (response) {
                alert('El proveedor fue modificado exitosamente.');
            } else {
                alert('No ha sido modificado');
            }
        },
        error: function (error) {
        }
    });
}