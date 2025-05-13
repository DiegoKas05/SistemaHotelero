let tablaHabitaciones;

$(document).ready(function () {
    // Inicializa DataTable
    tablaHabitaciones = $("#tblHabitaciones").DataTable({
        "ajax": {
            "url": "/Admin/Mantenimiento_Hab/GetAll", // Endpoint que devuelve las habitaciones
            "type": "GET", // Método HTTP
            "datatype": "json" // Tipo de dato esperado
        },
        "columns": [
            { "data": "numero" }, // Columna para el número de habitación
            { "data": "detalle" }, // Columna para el detalle de la habitación
            {
                "data": "precio", // Columna para el precio
                "render": function (data) {
                    // Si el precio está presente, lo formatea a 2 decimales, si no, muestra $0.00
                    return data ? "$ " + parseFloat(data).toFixed(2) : "$ 0.00";
                }
            },
            {
                "data": "categoria.descripcion", // Columna para la categoría de la habitación
                "defaultContent": "Sin categoría" // Valor por defecto si no hay categoría
            },
            {
                "data": "piso.descripcion", // Columna para el piso de la habitación
                "defaultContent": "Sin piso" // Valor por defecto si no hay piso
            },
            {
                "data": "estadoHabitacion.descripcion", // Columna para el estado de la habitación
                "defaultContent": "Sin estado" // Valor por defecto si no hay estado
            },
            {
                "data": "estado", // Columna para el estado de la habitación (activo o inactivo)
                "render": function (data) {
                    // Muestra un badge verde para activo y rojo para inactivo
                    return data
                        ? '<span class="badge bg-success">Activo</span>'
                        : '<span class="badge bg-danger">Inactivo</span>';
                }
            },
            {
                "data": "idHabitacion", // Columna con el id de la habitación para las acciones
                "render": function (data) {
                    // Botones para editar y eliminar
                    return `
                        <div class="text-center">
                            <a href="/Admin/Mantenimiento_Hab/Editar/${data}" class="btn btn-outline-primary btn-sm me-2">
                                <i class="fas fa-edit"></i> Editar
                            </a>
                            <a onclick="Eliminar('/Admin/Mantenimiento_Hab/Delete/${data}')" class="btn btn-outline-danger btn-sm">
                                <i class="fas fa-trash-alt"></i> Borrar
                            </a>
                        </div>
                    `;
                },
                "width": "20%" // Ancho de la columna de acciones
            }
        ],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.13.4/i18n/es-ES.json" // Idioma en español
        },
        responsive: true // Habilitar diseño responsivo
    });
});

// Función para eliminar una habitación
function Eliminar(url) {
    // Muestra una alerta de confirmación antes de eliminar
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
            // Realiza la solicitud AJAX para eliminar la habitación
            $.ajax({
                url: url,
                type: 'POST',
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message); // Muestra mensaje de éxito
                        tablaHabitaciones.ajax.reload(); // Recarga la tabla
                    } else {
                        toastr.error(data.message || 'Ocurrió un error al eliminar la habitación'); // Muestra mensaje de error
                    }
                },
                error: function () {
                    toastr.error('Error al realizar la solicitud de eliminación'); // Muestra error si falla la solicitud AJAX
                }
            });
        }
    });
}
