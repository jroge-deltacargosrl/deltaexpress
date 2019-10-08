//import Swal from "sweetalert2/src/sweetalert2";

//import Swal from 'sweetalert2/dist/sweetalert2.js';
jQuery(document).ready(function ($) {

    // desahbilitar los cuadros de seleccion al ingresar
    /*$("select[name=loadingSource]").prop("disabled", true);
    $("select[name=loadingDestination]").prop("disabled", true);*/

    initFormCustomer();
    initFormCarrier();

    // Selector de los tipos de servicio del formulario de clien
    $('select[name=serviceType]').on('change', function () {
        var service = $('select[name=serviceType] option:selected').val();
        // ampliar las posibilidades (funciones modulare)
        let idService = parseInt(service); 
        changePlaceholderComment(idService);
        switch (idService) {
            case 1:
                showContainerTransporteNacional();
                break;
            case 2:
                showContainerAlmacenamientoCarga();
                break;
            case 3:
                showContainerTransporteUrbanoSCZ();
                break;
            default:
                break;
        }
    });


    function showContainerTransporteNacional() {
        $("#divPesoVolumen").hide();
        $("#divAlmacenamientoCarga").hide();
        $("#divTransporteNacional").show();
    }


    function showContainerTransporteUrbanoSCZ() {
        $("#divTransporteNacional").hide();
        $("#divAlmacenamientoCarga").hide();
        $("#divPesoVolumen").show();
    }

    function showContainerAlmacenamientoCarga() {
        $("#divTransporteNacional").hide();
        $("#divPesoVolumen").hide();
        $("#divAlmacenamientoCarga").show();
    }

    function initFormCarrier() {

        /*changeStateSelectorByName("loadingSource", false);
        changeStateSelectorByName("loadingDestination", false);*/

    }

    function initFormCustomer() {

    }


    // funcion para escribir dinamicamente los cuadros del placeholder
    function changePlaceholderComment(typeService) {
        let commentTxt = "";
        switch (typeService) {
            case 1:
                commentTxt = "Hola! 1uiero transportar 10 pallets, peso total 20 Tn, en la ruta indicada esta semana...";
                break;
            case 2:
                commentTxt = "Hola! Tengo 10 Pallets de baldes de pintura, con un peso total de 20 Tn..."
                break;
            case 3:
                commentTxt = "Hola! quiero transportar 10 Pallets, desde mis almacenes en el Parque Industrial hasta la Av. Banzer 9no Anillo, almacenes XYZ...";
                break;
            default:
                break;
        }
        $("textarea[name=comentanos]").attr("placeholder",commentTxt).blur();
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
                .attr("disabled", true);
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
});




