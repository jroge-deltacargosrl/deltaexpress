//import Swal from "sweetalert2/src/sweetalert2";

//import Swal from 'sweetalert2/dist/sweetalert2.js';
jQuery(document).ready(function ($) {

    // desahbilitar los cuadros de seleccion al ingresar
    /*$("select[name=loadingSource]").prop("disabled", true);
    $("select[name=loadingDestination]").prop("disabled", true);*/

    initFormCustomer();
    initFormCarrier();

    // modificar estas funciones Jquery
    $('select[name=serviceType]').on('change', function () {
        var service = $('select[name=serviceType] option:selected').val();
        // ampliar las posibilidades (funciones modulare)
        if (service == 1) { // Transporte Nacional
            changeStateSelectorByName("loadingSource");
            changeStateSelectorByName("loadingDestination");

            changeStateLabelForSelectorById("#lblOrigen");
            changeStateLabelForSelectorById("#lblDestino");

            /*$("select[name=loadingSource]")
                .css("background-color", "#ffff")
                .css("color", "#000000")
                .removeAttr("disabled");
            $("select[name=loadingDestination]")
                .css("background-color", "#ffff")
                .css("color", "#000000")
                .removeAttr("disabled");

            $("#lblOrigen").css("color", "#000000")
            $("#lblDestino").css("color", "#000000")*/

        } else { // Almacenamiento de Carga
            changeStateSelectorByName("loadingSource",false);
            changeStateSelectorByName("loadingDestination",false);

            changeStateLabelForSelectorById("#lblOrigen",false);
            changeStateLabelForSelectorById("#lblDestino",false);

            /*$("select[name=loadingSource]")
                .css("background-color", "#e6e6e5")
                .css("color", "#e6e6e5")
                .attr("disabled", true);
            $("select[name=loadingDestination]")
                .css("background-color", "#e6e6e5")
                .css("color","#e6e6e5")
                .attr("disabled", true);

            $("#lblOrigen").css("color", "#e6e6e5")
            $("#lblDestino").css("color", "#e6e6e5")*/
        }
    });

});


function initFormCarrier() {

    changeStateSelectorByName("loadingSource", false);
    changeStateSelectorByName("loadingDestination", false);

}

function initFormCustomer() {

}


function changeStateLabelForSelectorById(id, enable = true) {
    var colorBack = enable ? "#000000" : "#e6e6e5";
    $(id).css("color", colorBack);    
}
   

function changeStateSelectorByName(name, enable = true) {
    if (enable) { // habilitar 
        $("select[name=" + name + "]")
            .css("background-color", "#FFFF")
            .css("color", "#000000")
            .removeAttr("disabled");
    } else { // deshabilitar
        $("select[name=" + name + "]")
            .css("background-color", "#e6e6e5")
            .css("color", "#e6e6e5")
            .attr("disabled",true);
    }
    
}

function responseCotization() {

    var $formContact = $('form')[0];
    if ($formContact.checkValidity()) {
        var contactRequest = {
            company: $('input[name=company]').val(),
            fullname: $('input[name=fullname]').val(),
            phone: $('input[name=phone]').val(),
            email: $('input[name=email]').val(),
            serviceType: $('select[name=serviceType]').val(),
            transportRoute: {
                loadingSource: $('select[name=loadingSource]').val(),
                loadingDestination: $('select[name=loadingDestination]').val()
            }
        };
        $.post("/Contact/ContactForm/",
            contactRequest,
            function (data, status, jqXHR) {
                Swal.fire({
                    title: 'Solicitud de Cotizacion',
                    text: data.message,
                    type: data.code == 200 ? 'success' : 'error',
                    showConfirmButton: false,
                    timer: 2000
                });
                clearFields();
            }, "json")
            .always(function (data) {
                Swal.fire({
                    title: 'Solicitud de Cotizacion',
                    text: data.message,
                    type: data.code == 200 ? 'success' : 'error',
                    showConfirmButton: false,
                    timer: 2000
                });
                clearFields();
            })
            .fail(function (error) { });
        return false;    
    } 
    return true;
    
    /*if (contactRequest.fullname != '' && contactRequest.phone != '' && contactRequest.serviceType != 0) {
        
    }*/
}

function clearFields() {
    $('input[name=company]').val('');
    $('input[name=fullname]').val('');
    $('input[name=phone]').val('');
    $('input[name=email]').val('');
}


function onlyAlert() {
    setTimeout(function () {
        Swal.fire({
            title: 'Solicitud de cotizacion enviada!!!',
            text: 'Do you want to continue',
            type: 'success',
            confirmButtonText: 'OK'
        });
    }, 1000);
    return false;
}




