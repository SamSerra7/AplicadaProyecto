$("#btnEnviar").click(function () {
    var mensaje_error = [];
    var error = document.getElementById("mensaje");
    error.style.color = 'red';
    var proveedor = {
        'Nombre': $('#Nombre').val()
    };
    var nombre = $('#Nombre').val();
    if (nombre == "" || nombre == null) {
        mensaje_error.push('**Ingrese un nombre para el proveedor');
    } else {
        registrarProveedor(proveedor);
    }
    error.innerHTML = mensaje_error.join(', ') + '\n';
});

function registrarProveedor(proveedor) {
    $.ajax({
        url: '/Proveedor/Registrar', 
     //   url: "@Url.Action("Registrar", "Proveedor")",
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(proveedor),
        success: function (response) {
            console.log(response);
            if (response) {
                alert('El proveedor fue registrado exitosamente.');
            } else {
                alert('No ha sido registrado');
            }
        },
        error: function (error) {
        }
    });
}