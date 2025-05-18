var tablaCategoria;

$(document).ready(function () {
    cargarDataTable();
});

function cargarDataTable() {
    tablaCategoria = $("#tblCategorias").DataTable({
        "ajax": {
            "url": "/Admin/Categoria/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "descripcion" },
            {
                "data": "estado",
                "render": function (data) {
                    return data
                        ? '<span class="badge bg-success">Activo</span>'
                        : '<span class="badge bg-danger">Inactivo</span>';
                }
            },
            {
                "data": "idCategoria",
                "render": function (data) {
                    return `
                <div class="text-center">
                    <a href="/Admin/Categoria/Edit/${data}" class="btn btn-sm btn-outline-primary" style="width: 80px">
                        <i class="fas fa-edit"></i> Editar
                    </a>
                    &nbsp;
                    <a onclick="Eliminar('/Admin/Categoria/Delete/${data}')" class="btn btn-sm btn-outline-danger" style="width: 80px">
                        <i class="fas fa-trash-alt"></i> Borrar
                    </a>
                </div>
    `;
                },
                "width": "20%"
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json"
        },
        responsive: true
    });
}

function Eliminar(url) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: "¡No podrás recuperar la categoría!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'POST',
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        tablaCategoria.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
