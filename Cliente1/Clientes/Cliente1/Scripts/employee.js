//this function loads data in the employees' table 
$(document).ready(function () {
    //loadData();
    $('#tablee').DataTable();
});

function loadData() {
    $.ajax({
        url: "/Home/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.EmployeeId + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Age + '</td>';
                html += '<td>' + item.Country + '</td>';
                html += '<td>' + item.State + '</td>';
                html += '<td><a href="#" onclick="return GetById(' + item.EmployeeId + ')">Edit</a> | <a href="#" onclick="Delete(' + item.EmployeeId + ')">Delete</a></td>';
            });
            $('.tbody').html(html);

        },
        error: function (errorMessage) {
            alert(errorMessage.responseText);
        }
    });
}


function Add() {

    var x = $("#Name").val();
    var xx = $("#Age").val();
    var xxx = $("#Country").val();
    var xxxx = $("#State").val();

    if (x == "" || xx == "" || xxx == "" || xxxx == "") {
        alert("Campos vacios");
    } else {
        var employee = {
            //EmployeeId: $('#EmployeeId').val(),
            Name: $('#Name').val(),
            Age: $('#Age').val(),
            Country: $('#Country').val(),
            State: $('#State').val()
        };

        $.ajax({
            url: "/Home/Add",
            data: JSON.stringify(employee),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                loadData();
                $('#myModal').modal('hide');
            },
            error: function (errorMessage) {
                alert(errorMessage.responseText);
            }
        });
    }
}

function Update() {
    var employee = {
        EmployeeId: $('#EmployeeId').val(),
        Name: $('#Name').val(),
        Age: $('#Age').val(),
        State: $('#State').val(),
        Country: $('#Country').val()
    };

    $.ajax({
        url: "/Home/Update",
        data: JSON.stringify(employee),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errorMessage) {
            alert(errorMessage.responseText);
        }
    });
}


function GetById(employeeId) {

    $.ajax({
        url: "/Home/GetById/" + employeeId,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            $('#EmployeeId').val(result.EmployeeId);
            $('#Name').val(result.Name);
            $('#Age').val(result.Age);
            $('#State').val(result.State);
            $('#Country').val(result.Country);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();


            $('#tAdd').hide();
            $('#tUpdate').show();
        },
        error: function (errorMessage) {
            alert(errorMessage.responseText);
        }
    });
}


function Delete(employeeId) {
    var alert = confirm("Are you sure you want to delete this record?");
    if (alert) {
        $.ajax({
            url: "/Home/Delete/" + employeeId,
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                loadData();

            },
            error: function (errorMessage) {
                alert(errorMessage.responseText);
            }
        });
    }
}


//Function for clearing the textboxes
function clearTextBox() {
    // $('#EmployeeId').val("");
    $('#Name').val("");
    $('#Age').val("");
    $('#State').val("");
    $('#Country').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Age').css('border-color', 'lightgrey');
    $('#State').css('border-color', 'lightgrey');
    $('#Country').css('border-color', 'lightgrey');
}


function cargarSelect2(valor) {
    var arrayValores = new Array(
        new Array("CR", "San Jose", "San Jose"),
        new Array("CR", "Limon", "Limon"),
        new Array("CR", "Cartago", "Cartago"),
        new Array("USA", "New York", "New York"),
        new Array("USA", "Florida", "Florida")
    );
    if (valor == 0) {
        // desactivamos el segundo select
        document.getElementById("State").disabled = true;
    } else {
        // eliminamos todos los posibles valores que contenga el select2
        document.getElementById("State").options.length = 0;

        // añadimos los nuevos valores al select2
        document.getElementById("State").options[0] = new Option("Selecciona una opcion", "0");
        for (i = 0; i < arrayValores.length; i++) {
            // unicamente añadimos las opciones que pertenecen al id seleccionado
            // del primer select
            if (arrayValores[i][0] == valor) {
                document.getElementById("State").options[document.getElementById("State").options.length] = new Option(arrayValores[i][2], arrayValores[i][1]);
            }
        }

        // habilitamos el segundo select
        document.getElementById("State").disabled = false;
    }
}

/**
 * Una vez selecciona una valor del segundo selecte, obtenemos la información
 * de los dos selects y la mostramos
 */
function seleccinado_select2(value) {
    var v1 = document.getElementById("Country");
    var valor1 = v1.options[v1.selectedIndex].value;
    var text1 = v1.options[v1.selectedIndex].text;
    var v2 = document.getElementById("State");
    var valor2 = v2.options[v2.selectedIndex].value;
    var text2 = v2.options[v2.selectedIndex].text;

}


