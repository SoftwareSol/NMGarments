


/*-- General Starts --*/
function showsupplier() {
    $("#showsupplier").show();
    $(".SupplierName").focus();

}

$("#lblerrorSupplierName").hide();
$("#lblPhone").hide();
$("#lblCellNo").hide();
$("#lblDesignation").hide();
$("#lblAddressOne").hide();
$("#lblNIC").hide();
$("#lblBusinessNature").hide();
$("#lblInt").hide();




/*-- General End --*/

/*--Validation Starts--*/

var SuppEmptyName = function () {

    if ($(".SupplierName").val() == '') {
        $("#alerts").show();
        $("#lblerrorSupplierName").show();
        $("#lblPhone").hide();
        $("#lblCellNo").hide();
        $("#lblDesignation").hide();
        $("#lblAddressOne").hide();
        $("#lblNIC").hide();
        $("#lblBusinessNature").hide();
        $("#lblInt").hide();



        return false;

    } else {

        $("#alerts").hide();
        $('#lblerrorSupplierName').hide();

        return true;
    }

}

function CheckEmptyPhone() {

    if ($(".Phone").val() == '') {
        $("#alerts").show();
        $("#lblerrorSupplierName").hide();
        $("#lblPhone").show();
        $("#lblCellNo").hide();
        $("#lblDesignation").hide();
        $("#lblAddressOne").hide();
        $("#lblNIC").hide();
        $("#lblBusinessNature").hide();
        $("#lblInt").hide();


        return false;

    } else {

        $("#alerts").hide();
        $('#lblPhone').hide();

        return true;
    }

}

function CheckEmptyEmail() {

    if ($(".TBEmail").val() == '') {
        $("#alerts").show();
        $("#lblerrorSupplierName").hide();
        $("#lblPhone").hide();
        $("#lblCellNo").hide();
        $("#lblDesignation").hide();
        $("#lblAddressOne").hide();
        $("#lblNIC").hide();
        $("#lblBusinessNature").hide();
        $("#lblInt").hide();

        return false;

    } else {

        $("#alerts").hide();
        $("#lblEmail").hide();

        return true;
    }

}


function CheckEmptyCellNo() {

    if ($(".CellNo").val() == '') {
        $("#alerts").show();
        $("#lblCellNo").show();
        $("#lblerrorSupplierName").hide();
        $("#lblPhone").hide();        
        $("#lblDesignation").hide();
        $("#lblAddressOne").hide();
        $("#lblNIC").hide();
        $("#lblBusinessNature").hide();
        $("#lblInt").hide();

        return false;

    } else {

        $("#alerts").hide();
        $("#lblCellNo").hide();

        return true;
    }

}

function CheckEmptyDesignation() {

    if ($(".Designation").val() == '') {
        $("#alerts").show();
        $("#lblDesignation").show();
        $("#lblerrorSupplierName").hide();
        $("#lblPhone").hide();
        $("#lblCellNo").hide();
        $("#lblAddressOne").hide();
        $("#lblNIC").hide();
        $("#lblBusinessNature").hide();
        $("#lblInt").hide();

        return false;

    } else {

        $("#alerts").hide();
        $("#lblDesignation").hide();

        return true;
    }

}

function CheckEmptyNIC() {

    if ($(".NIC").val() == '') {
        $("#alerts").show();
        $("#lblNIC").show();
        $("#lblerrorSupplierName").hide();
        $("#lblPhone").hide();
        $("#lblCellNo").hide();
        $("#lblDesignation").hide();
        $("#lblAddressOne").hide();
        $("#lblBusinessNature").hide();

        return false;

    } else {

        $("#alerts").hide();
        $("#lblNIC").hide();

        return true;
    }

}

function CheckEmptyBusinessNature() {

    if ($(".Business").val() == '') {
        $("#alerts").show();
        $("#lblBusinessNature").show();
        $("#lblNIC").hide();
        $("#lblerrorSupplierName").hide();
        $("#lblPhone").hide();
        $("#lblCellNo").hide();
        $("#lblDesignation").hide();
        $("#lblAddressOne").hide();

        return false;

    } else {

        $("#alerts").hide();
        $("#lblBusinessNature").hide();

        return true;
    }

}


function CheckEmptyAddess() {

    if ($(".addressone").val() == '') {
        $("#alerts").show();        
        $("#lblAddressOne").show();
        $("#lblerrorSupplierName").hide();
        $("#lblPhone").hide();
        $("#lblCellNo").hide();
        $("#lblDesignation").hide();
        $("#lblNIC").hide();
        $("#lblBusinessNature").hide();

        return false;

    } else {

        $("#alerts").hide();
        $("#lblAddressOne").hide();

        return true;
    }

}

var delete_ = function () {
    Alert();
    return false;
}
var progressbar = function () {

    if (trim($().val) == "Uploading:") {
        $("#uploadbar").show();

    } else {
        $("#uploadbar").hide();

    }

}

var checkcity = function () {

    if ($(".city").val() == '') {

        $("#alertcity").show();
        $("#lblcity").show();

        //return false;
    }
}

var check = function () {

    var regexemail = /^([0-9a-zA-Z]([-_\\.]*[0-9a-zA-Z]+)*)@([0-9a-zA-Z]([-_\\.]*[0-9a-zA-Z]+)*)[\\.]([a-zA-Z]{2,9})$/;
    var regexphone = /^(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$/i;
    var numericExpression = /^[0-9]+$/;
    var email = $(".email").val();
    var data = $(".Phone").val() || $(".CellNo").val() || $(".NIC").val();
    var phone = $(".Phone").val();



    if ($(".SupplierName").val() == '') {
        
        $("#alerts").show();
        $("#lblerrorSupplierName").show();
        $("#lblPhone").hide();
        $("#lblCellNo").hide();
        $("#lblDesignation").hide();
        $("#lblAddressOne").hide();
        $("#lblNIC").hide();
        $("#lblBusinessNature").hide();
        $("#lblInt").hide();

        return false;

    } else if ($(".Phone").val() == '') {
        
        $("#alerts").show();
        $("#lblerrorSupplierName").hide();
        $("#lblPhone").show();
        $("#lblCellNo").hide();
        $("#lblDesignation").hide();
        $("#lblAddressOne").hide();
        $("#lblNIC").hide();
        $("#lblBusinessNature").hide();
        $("#lblInt").hide();

        return false;

    } else if (!regexphone.test(phone)) {
        
        $("#alerts").show();
        $("#lblPhone").show();
        $("#lblerrorSupplierName").hide();
        $("#lblCellNo").hide();
        $("#lblDesignation").hide();
        $("#lblAddressOne").hide();
        $("#lblNIC").hide();
        $("#lblBusinessNature").hide();
        $("#lblInt").hide();

        return false;

    } else if ($(".CellNo").val() == '') {

        $("#alerts").show();
        $("#lblCellNo").show();
        $("#lblerrorSupplierName").hide();
        $("#lblPhone").hide();
        $("#lblDesignation").hide();
        $("#lblAddressOne").hide();
        $("#lblNIC").hide();
        $("#lblBusinessNature").hide();
        $("#lblInt").hide();

        return false;

    } else if ($(".NIC").val() == '') {

        

        $("#alerts").show();
        $("#lblNIC").show();
        $("#lblerrorSupplierName").hide();
        $("#lblPhone").hide();
        $("#lblCellNo").hide();
        $("#lblDesignation").hide();
        $("#lblAddressOne").hide();
        $("#lblBusinessNature").hide();

        return false;

    } else if ($(".addressone").val() == '') {

        $("#alerts").show();
        $("#lblAddressOne").show();
        $("#lblerrorSupplierName").hide();
        $("#lblPhone").hide();
        $("#lblCellNo").hide();
        $("#lblDesignation").hide();
        $("#lblNIC").hide();
        $("#lblBusinessNature").hide();

        return false;

    } else if (!data.match(numericExpression)) {
        
        console.log("it is Not Integer!");
        $("#alerts").show();
        $("#lblInt").show();
        $("#lblerrorCustomerName").hide();
        $("#lblphonenum").hide();
        $("#lblphonenum1").hide();
        $("#lblEmail").hide();
        $("#lblNIC").hide();
        $("#lblAddess").hide();
        $("#lblCellNo").hide();

        return false;
    } else {

        return true;
    }
}


/*--Validation Ends --*/



/*-- Modal PopUp Starts --*/

var Alert = function () {

    $("#ModalAlert").on("show", function () {    // wire up the OK button to dismiss the modal when shown

        $("#ModalAlert a.btn").on("click", function (e) {
            console.log("button pressed");   // just as an example...
            $("#ModalAlert").modal('hide');     // dismiss the dialog
        });

    });

    $("#ModalAlert").on("hide", function () {    // remove the event listeners when the dialog is dismissed
        $("#ModalAlert a.btn").off("click");
    });

    $("#ModalAlert").on("hidden", function () {  // remove the actual elements from the DOM when fully hidden
        $("#ModalAlert").remove();
    });

    $("#ModalAlert").modal({                    // wire up the actual modal functionality and show the dialog
        "backdrop": "static",
        "keyboard": true,
        "show": true                     // ensure the modal is shown immediately
    });
}


var AlertDelete = function () {

    $("#MyModalDelete").on("show", function () {    // wire up the OK button to dismiss the modal when shown

        $("#MyModalDelete a.btn").on("click", function (e) {
            console.log("button pressed");   // just as an example...
            $("#MyModalDelete").modal('hide');     // dismiss the dialog
        });

    });

    $("#MyModalDelete").on("hide", function () {    // remove the event listeners when the dialog is dismissed
        $("#MyModalDelete a.btn").off("click");
    });

    $("#MyModalDelete").on("hidden", function () {  // remove the actual elements from the DOM when fully hidden
        $("#MyModalDelete").remove();
    });

    $("#MyModalDelete").modal({                    // wire up the actual modal functionality and show the dialog
        "backdrop": "static",
        "keyboard": true,
        "show": true                     // ensure the modal is shown immediately
    });
}



var myModalCustCategory = function () {

    $("#myModalCustCategory").on("show", function () {    // wire up the OK button to dismiss the modal when shown

        $("#myModalCustCategory a.btn").on("click", function (e) {
            console.log("button pressed");   // just as an example...
            $("#myModalCustCategory").modal('hide');     // dismiss the dialog
        });

    });

    $("#myModalCustCategory").on("hide", function () {    // remove the event listeners when the dialog is dismissed
        $("#myModalCustCategory a.btn").off("click");
    });

    $("#myModalCustCategory").on("hidden", function () {  // remove the actual elements from the DOM when fully hidden
        $("#myModalCustCategory").remove();

    });

    $("#myModalCustCategory").modal({                    // wire up the actual modal functionality and show the dialog
        "backdrop": "static",
        "keyboard": true,
        "show": true                     // ensure the modal is shown immediately
    });

}

var myModalCity = function () {

    $("#ModalCity").on("show", function () {    // wire up the OK button to dismiss the modal when shown

        $("#ModalCity a.btn").on("click", function (e) {
            console.log("button pressed");   // just as an example...
            $("#ModalCity").modal('hide');     // dismiss the dialog
        });

    });

    $("#ModalCity").on("hide", function () {    // remove the event listeners when the dialog is dismissed
        $("#ModalCity a.btn").off("click");
    });

    $("#ModalCity").on("hidden", function () {  // remove the actual elements from the DOM when fully hidden
        $("#ModalCity").remove();

    });

    $("#ModalCity").modal({                    // wire up the actual modal functionality and show the dialog
        "backdrop": "static",
        "keyboard": true,
        "show": true                     // ensure the modal is shown immediately
    });

}

var MyModalCustomerType = function () {

    $("#ModalCustomerType").on("show", function () {    // wire up the OK button to dismiss the modal when shown

        $("#ModalCustomerType a.btn").on("click", function (e) {
            console.log("button pressed");   // just as an example...
            $("#ModalCustomerType").modal('hide');     // dismiss the dialog
        });

    });

    $("#ModalCustomerType").on("hide", function () {    // remove the event listeners when the dialog is dismissed
        $("#ModalCustomerType a.btn").off("click");
    });

    $("#ModalCustomerType").on("hidden", function () {  // remove the actual elements from the DOM when fully hidden
        $("#ModalCustomerType").remove();

    });

    $("#ModalCustomerType").modal({                    // wire up the actual modal functionality and show the dialog
        "backdrop": "static",
        "keyboard": true,
        "show": true                     // ensure the modal is shown immediately
    });

}

/*-- Modal PopUp Starts --*/