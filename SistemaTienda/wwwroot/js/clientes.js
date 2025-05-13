var dataTable;

$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#tblCliente").DataTable({
        "ajax": {
            "url": "/Admin/Usuarios/GetAllUsuarios",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "tipoDocumento", "width": "15%" },
            { "data": "documento", "width": "15%" },
            {
                "data": null,
                "render": function (data) {
                    return `${data.nombre} ${data.apellido}`;
                },
                "width": "25%"
            },
            { "data": "correo", "width": "25%" },
            {
                "data": "idPersona",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Usuarios/Edit/${data}" class="btn btn-sm btn-outline-primary" style="width: 80px">
                                    <i class="fas fa-edit"></i> Editar
                                </a>
                                &nbsp;
                                <a onclick="Delete('/Admin/Usuarios/Delete/${data}')" class="btn btn-sm btn-outline-danger" style="width: 80px">
                                    <i class="fas fa-trash-alt"></i> Borrar
                                </a>
                            </div>`;
                },
                "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "No hay usuarios registrados",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ entradas",
            "infoEmpty": "Mostrando 0 a 0 de 0 entradas",
            "infoFiltered": "(filtrado de _MAX_ entradas totales)",
            "lengthMenu": "Mostrar _MENU_ entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "No se encontraron usuarios",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}

function Delete(url) {
    Swal.fire({
        title: "¿Estás seguro?",
        text: "¡No podrás recuperar este usuario!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "POST",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload(null, false);
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
