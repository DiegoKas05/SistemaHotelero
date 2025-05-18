

$(document).ready(function () {
    $('#tblEmpleados').DataTable({
        ajax: {
            url: '/Admin/Usuarios/GetAllEmpleados',
            type: 'GET',
            datatype: 'json'
        },
        columns: [
            { data: 'nombre' },
            { data: 'apellido' },
            { data: 'email' },
            { data: 'direccion' },
            { data: 'tipoDocumento' },
            { data: 'numeroDocumento' },
            {
                data: 'id',
                render: function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Usuarios/EditEmpleados/${data}" class="btn btn-sm btn-outline-primary" style="width: 80px">
                                <i class="fas fa-edit"></i> Editar
                            </a>
                            &nbsp;
                            <a onclick="Eliminar('/Admin/Usuarios/Delete/${data}')" class="btn btn-sm btn-outline-danger" style="width: 80px">
                                <i class="fas fa-trash-alt"></i> Borrar
                            </a>
                        </div>`;
                },
                width: "15%"
            }
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json'
        }
    });
});

function Eliminar(url) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: "¡No podrás recuperar el empleado!",
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
                        window.location.reload(); // Recarga toda la vista IndexEmpleados
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
