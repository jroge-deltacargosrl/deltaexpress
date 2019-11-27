//import Swal from "sweetalert2";
//import { Alert } from "../lib/bootstrap/dist/js/bootstrap.bundle";
//import Swal from "sweetalert2/src/sweetalert2";
//import Swal from 'sweetalert2/dist/sweetalert2.js';
const URL_AZURE_SERVER = "https://deltacargoapi.azurewebsites.net/api/v1";
const URL_IIS_SERVER = "https://deltacargoapi.azurewebsites.net/api/v1";
const URL_IIS_EXPRESS_SERVER = "https://localhost:44333/api/v1/";



jQuery(document).ready(function ($) {

    // accion inicial tabs de tecnologia
    $('.tabContents article').hide();
    $('.img').hide();
    $('#tab1').show();
    $('#tab1img').show();
    $('#li1 a').addClass('active');

    // llamando a funciones necesarias para interactuar con los formularios de la pagina
    changeTabsSectionTechnology();
    changeStateSelectorsRouteNational();

    // metodo para los tabs de la seccion de tecnologia
    function changeTabsSectionTechnology() {
        $('.divOptions .tabs li a').click(function () {
            $('.divOptions .tabs li a').removeClass('active');
            $(this).addClass('active');
            var activeTab = $(this).attr('href');
            $('.tabContents article').hide();
            $('.img').hide();
            $(activeTab).show();
            var activeImg = activeTab + "img";
            $(activeImg).show();
            return false;
        });


    }

    initFormCustomer();
    initFormCarrier();

    // Selector de los tipos de servicio del formulario de clien
    $('select[name=serviceTypeClient]').on('change', function () {
        var nameService = $('select[name=serviceTypeClient] option:selected').text().trim();
        // rediseñar el algoritmo para obtener los datos de manera dinámica
        changePlaceholderComment(nameService);
        switch (nameService) {
            case "Transporte Nacional":
                showContainerTransporteNacional();
                break;
            case "Almacenamiento de Carga en SCZ":
                showContainerAlmacenamientoCarga();
                break;
            case "Transporte Urbano en SCZ":
                showContainerTransporteUrbanoSCZ();
                break;
            default:
                break;
        }
    });

    // cambio de estados en los selectores de origen y destino
    function changeStateSelectorsRouteNational() {
        // Rutas Nacionales (Origen)
        $('select[name = routeNationalOrigin]').change(function (e) {
            let valueRouteDestinationDisabled = $(this).data('pre');
            $(this).data('pre', $(this).val());//update the pre data
            // habilitar en el selector destino nuevamente la opcion excluida
            // producto de la anterior seleccion de una ruta origen
            $('select[name = routeNationalDestination] option[value =' + valueRouteDestinationDisabled + ']')
                .prop('disabled', false)
                .css('color', '#9c9c9c');
            // inhabilitar en el destino la opcion seleccionada en el origen
            let valueRouteOriginCurrent = $('select[name = routeNationalOrigin] option:selected').val();
            $('select[name = routeNationalDestination] option[value =' + valueRouteOriginCurrent + ']')
                .prop('disabled', true)
                .css('color', '#C9C9C9');;
        });
        // Rutas Nacionales (Destino)
        $('select[name = routeNationalDestination]').change(function (e) {
            let valueRouteOriginDisabled = $(this).data('pre');
            $(this).data('pre', $(this).val());//update the pre data
            // habilitar en el selector origen nuevamente la opcion excluida
            // producto de la anterior seleccion de una ruta destino
            $('select[name = routeNationalOrigin] option[value =' + valueRouteOriginDisabled + ']')
                .prop('disabled', false)
                .css('color', '#9c9c9c');
            // inhabilitar en el destino la opcion seleccionada en el origen
            let valueRouteOriginCurrent = $('select[name = routeNationalDestination] option:selected').val();
            $('select[name = routeNationalOrigin] option[value =' + valueRouteOriginCurrent + ']')
                .prop('disabled', true)
                .css('color', '#C9C9C9');;
        });
    }

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
            case "Transporte Nacional":
                commentTxt = "Hola! Quiero transportar 10 pallets, peso total 20 Tn, en la ruta indicada esta semana...";
                break;
            case "Almacenamiento de Carga en SCZ":
                commentTxt = "Hola! Tengo 10 Pallets de baldes de pintura, con un peso total de 20 Tn..."
                break;
            case "Transporte Urbano en SCZ":
                commentTxt = "Hola! Quiero transportar 10 Pallets, desde mis almacenes en el Parque Industrial hasta la Av. Banzer 9no Anillo, almacenes XYZ...";
                break;
            default:
                break;
        }
        $("textarea[name=commentQuotation]").attr("placeholder", commentTxt).blur();
    }


    //funciones no utilizadas
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


});

// Metodos para llamar al formulario por eventos personalizados
function postCarrier() {
    var formCarrier = document.forms[0];
    if (formCarrier.checkValidity()) {
        let model = {
            nroLicense: formCarrier.querySelector('input[name = nroLicenseCarrier]')
                .value,
            fullname: formCarrier.querySelector('input[name = fullnameCarrier]')
                .value,
            lastname: formCarrier.querySelector('input[name = lastnameCarrier]')
                .value,
            company: formCarrier.querySelector('input[name = companyCarrier]')
                .value,
            phone: formCarrier.querySelector('input[name = phoneCarrier]')
                .value,
            email: formCarrier.querySelector('input[name = emailCarrier]')
                .value,
            id_truckType: formCarrier.querySelector('select[name = truckTypeCarrier] option:checked')
                .value,
            id_membership: formCarrier.querySelector('input[name = idMembershipCarrier]')
                .value
        };
        //let url = 'https://jsonplaceholder.typicode.com/todos/1';
        //let url = "https://deltacargoapi.azurewebsites.net/api/v1/carrier/";
        let url = URL_AZURE_SERVER+"/carrier/";
        var headers = {
            //'Accept': 'application/json', 
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, OPTIONS'
            //'Access-Control-Allow-Credentials': 'true'
        };
        sendRequestHTTP(url, 'POST', headers, model, true)
            .then(responseData => {
                //console.log(responseData);
                Swal.fire({
                    title: 'DELTA CARGO SRL',
                    text: 'Tu registro fue exitoso!! Ya eres parte del Club de Choferes Delta Cargo SRL',
                    type: 'success',
                    showConfirmButton: false,
                    timer: 2000
                });
                formCarrier.reset();
            });
        /*const options = {
            method: 'POST',
            headers: {
                // ya utilizados en la parte de arriba
            },
            body: JSON.stringify(model)
        };*/

        /*fetch(url, options)
            .then(content => content.json())
            .then(data => {
                // data es el objeto obtenido de la peticion al servidor
                Swal.fire({
                    title: 'DELTA CARGO SRL',
                    text: 'Tu registro fue exitoso!! Ya eres parte del Club de Choferes Delta Cargo SRL',
                    type: 'success',
                    showConfirmButton: false,
                    timer: 2000
                });
                formCarrier.reset();
            });*/
    }
    return false;
}




function sendRequestHTTP(url, methodRequest, headers, data, enableCors) {
    let requestHTTP = new Request(url, {
        method: methodRequest,
        body: methodRequest == 'POST' ? JSON.stringify(data) : {},
        headers: headers,
        mode: enableCors ? 'cors' : 'no-cors'
    });
    return fetch(requestHTTP)
        .then(response => {
            return response.json();
        });
}


// test function Request DATA
function testRequest() {
    let headersRequest = new Headers();

    // headersRequest.append('Accept', 'application/json');
    headersRequest.append('Content-Type', 'application/json');
    headersRequest.append('Access-Control-Allow-Origin', '*');
    headersRequest.append('Access - Control - Allow - Methods', 'GET, POST, OPTIONS');
    headersRequest.append('Access - Control - Allow - Credentials', 'true');

    sendRequestHTTP('https://reqres.in/api/users?page=2', 'GET', headersRequest, null, true)
        .then(responseData => {
            console.log(responseData);
        });


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


function postQuotationWithRegisterClient() {
    var formQuotation = $('form')[1];
    if (formQuotation.checkValidity()) {
        // solicitud HTTP para registrar el cliente
        var model = {
            fullname: $('input[name=fullnameClient]').val(),
            lastname: $('input[name=fullnameClient]').val(),
            company: $('input[name=companyClient]').val(),
            phone: $('input[name=phoneClient]').val(),
            id_membership: 2 // delta x
        };
        //let urlApiRegisterClient = "https://localhost:44333/api/v1/client/";
        let urlApiRegisterClient = URL_AZURE_SERVER + "/client/";
        var headers = {
            //'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, OPTIONS'
            //'Access-Control-Allow-Credentials': 'true'
        };
        sendRequestHTTP(urlApiRegisterClient, 'POST', headers, model, true)
            .then(responseClient => {
                postQuotation(responseClient.id);
            });
    }
    return false;
}

function postQuotation(idContact) {
    var quotation = {
        /*company: $('input[name=companyClient]').val(),
        fullname: $('input[name=fullnameClient]').val(),
        phone: $('input[name=phoneClient]').val(),
        email: $('input[name=emailClient]').val(),*/
        idContact: idContact,
        idServiceType: $('select[name=serviceTypeClient] option:selected').val()
    };
    // logica de de campos a tomar de acuerdo al servicio seleccionado
    switch (parseInt(quotation.idServiceType)) {
        case 1: // Transporte Urbano en SCZ
            quotation.loadWeight = $('input[name=weightQuotation]').val();
            quotation.loadVolume = $('input[name=volumeQuotation]').val();
            break;
        case 2: // Almacenamiento de Carga en SCZ
            quotation.storageCapacity = $('input[name=storageCapacityQuotation]').val();
            quotation.idUmStorageCapacity = $('select[name=unitMeasurementCapacityQuotation] option:selected').val();
            quotation.storageTime = $('input[name=storageTimeQuotation]').val();
            quotation.idUmStorageTime = $('select[name=unitMeasurementTimeQuotation] option:selected').val();
            break;
        case 3: // Transporte Nacional
            quotation.routeCityOrigin = $('select[name=routeNationalOrigin] option:selected').val();
            quotation.routeCityDestination = $('select[name=routeNationalDestination] option:selected').val();
            break;
    }
    quotation.comment = $('textarea[name=commentQuotation]').val();
    quotation.id_membership = 2; // afiliacion : DeltaX

    // solicitud a la API TMS
    //let url = "https://deltacargoapi.azurewebsites.net/api/v1/quotation/";
    //let urlRequestQuotation = "https://localhost:44333/api/v1/quotation/";
    let urlRequestQuotation = URL_AZURE_SERVER + "/quotation/";

    var headers = {
        //'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET, POST, OPTIONS'
        //'Access-Control-Allow-Credentials': 'true'
    };
    sendRequestHTTP(urlRequestQuotation, 'POST', headers, quotation, true)
        .then(responseQuotationDetails => {

            sendMailWithQuotation(responseQuotationDetails);

            /*Swal.fire({
                title: 'DELTAX',
                text: 'Solicitud de Cotizacion realizada con éxito, pronto nos pondremos en contacto contigo.',
                type: 'success',
                showConfirmButton: false,
                timer: 3000
            });**/
            var formQuotation = $('form')[1];
            formQuotation.reset();
        });
}




// FUNCIONES YA NO UTILIZADAS
function sendMailWithQuotation(quotationDetails) {

    var headers = {
        'Accept': 'application/json', 
        'Content-Type': 'application/json'
    };
    let urlMailServices = "/Quotation/SendMail/";
    sendRequestHTTP(urlMailServices, 'POST', headers, quotationDetails, false)
        .then(responseQuotationMail => {
            Swal.fire({
                title: 'Solicitud de Cotizacion',
                text: responseQuotationMail.message,
                type: responseQuotationMail.code == 200 ? 'success' : 'error',
                showConfirmButton: false,
                timer: 3000
            });
        });
    

    /*var $formContact = $('form')[0];
    if ($formContact.checkValidity()) {
        var contactRequest = {
            company: $('input[name=company]').val(),
            fullname: $('input[name=fullname]').val(),
            phone: $('input[name=phone]').val(),
            email: $('input[name=email]').val(),
            serviceType: $('select[name=serviceType] option:selected').val(),


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
                clearFieldsFormQuotation();
            }, "json")
            .always(function (data) {
                Swal.fire({
                    title: 'Solicitud de Cotizacion',
                    text: data.message,
                    type: data.code == 200 ? 'success' : 'error',
                    showConfirmButton: false,
                    timer: 2000
                });
                clearFieldsFormQuotation();
            })
            .fail(function (error) { });
        return false;
    }
    return true;*/

    /*if (contactRequest.fullname != '' && contactRequest.phone != '' && contactRequest.serviceType != 0) {
        
    }*/
}



