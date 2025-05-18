var tablaRecepcion;

$(document).ready(function () {
    cargarTablaRecepcion();
});

function cargarTablaRecepcion() {
    tablaRecepcion = $("#tblRecepcion").DataTable({
        "ajax": {
            "url": "/Admin/Recepcion/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "numero" },
            { "data": "detalle" },
            {
                "data": "precio",
                "render": function (data) {
                    return "$ " + parseFloat(data).toFixed(2);
                }
            },
            {
                "data": "categoria.descripcion",
                "defaultContent": "Sin categoría"
            },
            {
                "data": "piso.descripcion",
                "defaultContent": "Sin piso"
            },
            {
                "data": "estadoHabitacion.descripcion",
                "defaultContent": "Sin estado"
            },
            {
                "data": "estado",
                "render": function (data) {
                    return data
                        ? '<span class="badge bg-success">Activo</span>'
                        : '<span class="badge bg-danger">Inactivo</span>';
                },
                "width": "10%"
            },
            {
                "data": "idHabitacion",
                "render": function (data) {
                    return `
                    <div class="text-center">
                        <a href="/Admin/Recepcion/Registrar/${data}" class="btn btn-sm btn-outline-success" style="width: 120px">
                            <i class="fas fa-bed me-1"></i> Reservar
                        </a>
                    </div>`;
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
