let tablaCategorias;

$(document).ready(function () {
    tablaCategorias = $("#tblCategorias").DataTable({
        "ajax": {
            "url": "/Admin/Mantenimiento_Categoria/GetAll",
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
                },
                "width": "15%"
            },
            {
                "data": "idCategoria",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Mantenimiento_Categoria/Editar/${data}" class="btn btn-outline-primary btn-sm me-2">
                                <i class="fas fa-edit"></i> Editar
                            </a>
                            <a onclick="Eliminar('/Admin/Mantenimiento_Categoria/Delete/${data}')" class="btn btn-outline-danger btn-sm">
                                <i class="fas fa-trash-alt"></i> Borrar
                            </a>
                        </div>
                    `;
                },
                "width": "25%"
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json"
        },
        responsive: true
    });
});

function Eliminar(url) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: "Esta acción no se puede deshacer.",
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
                        tablaCategorias.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
