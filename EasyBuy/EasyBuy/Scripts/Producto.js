var editando = false;
var $dialog;
var ultimoScroll = 0;

$(document).ready(function () {

    //Oculta el boton btnAgregardetalle hasta que se cree un plan
    $('#btnAgregarDetalle').hide();

    $('#contenido').hide();

    //Ocultar y mostrar Formulario Crear Plan 
    $('#forMostrar').hide();
    $('#forEditar').hide();
    $('#contenido').hide();

    $('#forOcultar').on("click", function () {
        $('#forOcultar').hide();
        $('#Divformulario').hide("slow");
        $('#imgCrearPlan').hide("slow");
        $('#forMostrar').show();
    });

    $('#forMostrar').on("click", function () {
        $('#forMostrar').hide();
        $('#forOcultar').show();
        $('#Divformulario').show("slow");
        $('#imgCrearPlan').show("slow");

    });

    //Abre la ventana modal
    $('body').on("click", ".popup", function (e) {
        e.preventDefault();
        var page = $(this).attr('href') + '?id=' + $(this).attr('id');
        abrirVentana(page);
    });

    //Guarda el producto
    $("body").on('submit', '#FormProducto', function (e) {
        e.preventDefault();
        GuardarProducto();
    });
    //Evento Guardar detalle
    $("body").on('submit', '#FormGuardar', function (e) {
        e.preventDefault();
        $('#btnAgrede').attr('disabled', true);
        GuardarDetalle();
        $('#btnAgrede').attr('disabled', false);
    });


});

//Carga la ventana modal
function abrirVentana(Page) {
    var $pageContent = $('<div/>');
    $pageContent.load(Page);
    $dialog = $('<div class="popupWindow" style="overflow:auto"></div>')
            .html($pageContent)
            .dialog({
                draggable: true,
                autoOpen: false,
                resizable: true,
                model: true,
                height: 600,
                width: 1100,
                scrollable: true,
                close: function () {
                    $dialog.dialog('destroy').remove();
                }
            })
    $dialog.dialog('open');
}

//Valida los datos del formulario crear plan 
function validarProducto() {

    if ($('#description').val().trim() == '') {
        swal("Error!", "Debe Agregar una descripcion", "error");
        return false;
    }
    return true;
}

//Guardar Plan de Capacitaciones por AJAX
function GuardarProducto() {

   // Validación
    if (!validarProducto()) {
        return false;
    }

    var producto = {
        description: $('#description').val()
    };
    //Agrega validation token
    producto.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Producto/RegistrarProducto',
        type: 'POST',
        data: producto,
        success: function (data) {
            if (data.description!='') {

                $('#description').attr("readonly", "readonly");
                $('#btnCrearProducto').hide();
                $('#btnAgregarDetalle').show();
                $('#contenido').show();
                $('#divDetalle').show();
                $('#id_producto').attr("value", data.id_producto);
                swal({ title: "Bien!", text: data.mensaje, timer: 2000, type: "success", showConfirmButton: false });
            }
            else {
                swal("Error!", data.mensaje, "error")
            }
        },
        error: function () {
            swal("Error!", "Error al crear el Producto, Intentelo nuevamente...", "error");
        }
    });
}

//Guardar Capacitación por AJAX
function GuardarDetalle() {

    //Validar campos
    if (!validarDetalle()) {
        return false;
    }
    var detalle_producto = {
        id_producto: $('#id_producto').val(),
        talla: $('#talla').val(),
        precio: $('#precio').val(),
        cantidad: $('#cantidad').val(),
        color: $('#color').val(),
        imagen: $('#imagen').val(),
        promocion: $('#promocion').prop('checked')
     };

    //Agregar validation token
    detalle_producto.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Producto/AgregarDetalle',
        type: 'POST',
        data: detalle_producto,
        success: function (data) {
            if (data.estado) {
                //cargarCapacitaciones();
                //ajustesValores(capacitacion);

                $('#id_producto').val('');
                $('#talla').val('');
                $('#precio').val('');
                $('#cantidad').val('');
                $('#color').val('');
                $('#imagen').val('');
                $dialog.dialog('close');

                swal({
                    title: "Bien!",
                    text: data.mensaje,
                    timer: 2000,
                    type: "success",
                    showConfirmButton: false
                });
            }
            else {
                swal("Error!", data.mensaje, "error");
            }

        },
        error: function () {
            swal("Error!", "Error al crear el detalle...", "error");
        }
    });
}

//Valida los datos del formulario Crear
function validarDetalle() {

    if ($('#talla').val().trim() == '' ||
        $('#precio').val().trim() == '' || $('#cantidad').val().trim() == '' ||
        $('#color').val().trim() == '' || $('#imagen').val().trim() == '') {
        swal("Error!", "Todos los campos son requeridos", "error");
        return false;
    }

    if (isNaN($('#precio').val().trim())) {
        swal("Error!", "El Costo  debe ser un numero", "error");
        return false;
    }
    return true;
}


//Carga la tabla de capacitaciones que pertenecen al plan
function cargarCapacitaciones() {
    $('#contenido').html("Cargando...");

    $.ajax({
        url: '/Producto/ObtenerDetalleCP',
        type: 'GET',
        data: {
            'id_producto': $('#id_producto').val()
        },
        success: function (data) {
            if (data.length > 0) {
                $('#contenido').html(data);
            }
            else {
                $('#contenido').html('');
            }
        },
        error: function () {
            swal("Error!", "Error al cargar detalles...", "error");
            $('#contenido').html("No se ha podido cargar los detalles...");
        }

    });
}

