


/*-- General Starts --*/
function showempyee() {
    $("#showemp").show();
    $("#TBEmpName").focus();

}

$("#lblerrorCustomerName").hide();
$("#lblphonenum").hide();
$("#lblphonenum1").hide();
$("#lblEmail").hide();
$("#lblInt").hide();
$("#lblNIC").hide();
$("#lblAddess").hide();
$("#lblCellNo").hide();
$("#lblcity").hide();

/*-- General End --*/

/*--Validation Starts--*/

var CustEmptyName = function () {

    if ($(".CustomersName").val() == '') {
        $("#alerts").show();
        $('#lblerrorCustomerName').show();
        $("#lblphonenum").hide();
        $("#lblphonenum1").hide();
        $("#lblEmail").hide();
        $("#lblInt").hide();
        $("#lblNIC").hide();
        $("#lblAddess").hide();
        $("#lblCellNo").hide();

        return false;

    }else {

        $("#alerts").hide();
        $('#lblerrorCustomerName').hide();

        return true;
    }

}

function CheckEmptyPhone() {

    //if ($(".TBPhone").val() == '') {
    //    $("#alerts").show();
    //    $('#lblphonenum1').show();
    //    $("#lblerrorCustomerName").hide();
    //    $("#lblphonenum").hide();
    //    $("#lblEmail").hide();
    //    $("#lblInt").hide();
    //    $("#lblNIC").hide();
    //    $("#lblAddess").hide();
    //    $("#lblCellNo").hide();
    //    return false;

    //} else {

    //    $("#alerts").hide();
    //    $('#lblphonenum1').hide();

    //    return true;
    //}

}

function CheckEmptyEmail() {

    //if ($(".TBEmail").val() == '') {
    //    $("#alerts").show();
    //    $("#lblEmail").show();
    //    $("#lblerrorCustomerName").hide();
    //    $("#lblphonenum").hide();
    //    $("#lblphonenum1").hide();
    //    $("#lblInt").hide();
    //    $("#lblNIC").hide();
    //    $("#lblAddess").hide();
    //    $("#lblCellNo").hide();

    //    return false;

    //} else {

    //    $("#alerts").hide();
    //    $("#lblEmail").hide();

    //    return true;
    //}
}


function CheckEmptyCellNo() {
   
    if ($(".CellNo").val() == '') {
        $("#alerts").show();
        $("#lblCellNo").show();
        $("#lblerrorCustomerName").hide();
        $("#lblphonenum").hide();
        $("#lblphonenum1").hide();
        $("#lblEmail").hide();
        $("#lblInt").hide();
        $("#lblNIC").hide();
        $("#lblAddess").hide();

        return false;

    } else {

        $("#alerts").hide();
        $("#lblCellNo").hide();

        return true;
    }

}

function CheckEmptyNIC() {

    //if ($(".NIC").val() == '') {
    //    $("#alerts").show();
    //    $("#lblNIC").show();
    //    $("#lblerrorCustomerName").hide();
    //    $("#lblphonenum").hide();
    //    $("#lblphonenum1").hide();
    //    $("#lblEmail").hide();
    //    $("#lblInt").hide();
    //    $("#lblAddess").hide();
    //    $("#lblCellNo").hide();

    //    return false;

    //} else {

    //    $("#alerts").hide();
    //    $("#lblNIC").hide();

    //    return true;
    //}
}


function CheckEmptyAddess() {

    if ($(".Addess").val() == '') {
        $("#alerts").show();
        $("#lblAddess").show();
        $("#lblerrorCustomerName").hide();
        $("#lblphonenum").hide();
        $("#lblphonenum1").hide();
        $("#lblEmail").hide();
        $("#lblInt").hide();
        $("#lblNIC").hide();
        $("#lblCellNo").hide();

        return false;

    } else {

        $("#alerts").hide();
        $("#lblAddess").hide();

        return true;
    }

}

var delete_ = function () {    
    
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
    var data = $(".TBPhone").val() || $(".CellNo").val() || $(".NIC").val();
    var phone = $(".TBPhone").val();
    
   

    if ($(".CustomersName").val() == '') {
       
        $("#alerts").show();
        $("#lblerrorCustomerName").show();
        $("#lblphonenum").hide();
        $("#lblphonenum1").hide();
        $("#lblEmail").hide();
        $("#lblInt").hide();
        $("#lblNIC").hide();
        $("#lblAddess").hide();
        $("#lblCellNo").hide();

        return false;

    }

    //else if ($(".TBPhone").val() == '') {
      
    //    $("#alerts").show();
    //    $("#lblphonenum1").show();
    //    $("#lblerrorCustomerName").hide();
    //    $("#lblphonenum").hide();
    //    $("#lblEmail").hide();
    //    $("#lblInt").hide();
    //    $("#lblNIC").hide();
    //    $("#lblAddess").hide();
    //    $("#lblCellNo").hide();

    //    return false; 
//}
//else if (!regexphone.test(phone)) {
       
//        $("#alerts").show();
//        $("#lblphonenum").show();
//        $("#lblerrorCustomerName").hide();
//        $("#lblphonenum1").hide();
//        $("#lblEmail").hide();
//        $("#lblInt").hide();
//        $("#lblNIC").hide();
//        $("#lblAddess").hide();
//        $("#lblCellNo").hide();

//        return false;
//}
//else if ($(".email").val() == '') {
       
//        $("#alerts").show();
//        $("#lblEmail").show();
//        $("#lblerrorCustomerName").hide();
//        $("#lblphonenum").hide();
//        $("#lblphonenum1").hide();
//        $("#lblInt").hide();
//        $("#lblNIC").hide();
//        $("#lblAddess").hide();
//        $("#lblCellNo").hide();

//        return false;
//    } else if (!regexemail.test(email)) {
       
//        $("#alerts").show();
//        $("#lblEmail").show();
//        $("#lblerrorCustomerName").hide();
//        $("#lblphonenum").hide();
//        $("#lblphonenum1").hide();
//        $("#lblInt").hide();
//        $("#lblNIC").hide();
//        $("#lblAddess").hide();
//        $("#lblCellNo").hide();

//        return false;
//    }
    else if ($(".CellNo").val() == '') {

        $("#alerts").show();
        $("#lblCellNo").show();
        $("#lblerrorCustomerName").hide();
        $("#lblphonenum").hide();
        $("#lblphonenum1").hide();
        $("#lblEmail").hide();
        $("#lblInt").hide();
        $("#lblNIC").hide();
        $("#lblAddess").hide();
        
        return false;
    }
    //else if ($(".NIC").val() == '') {

    //    $("#alerts").show();
    //    $("#lblNIC").show();
    //    $("#lblerrorCustomerName").hide();
    //    $("#lblphonenum").hide();
    //    $("#lblphonenum1").hide();
    //    $("#lblEmail").hide();
    //    $("#lblInt").hide();
    //    $("#lblAddess").hide();
    //    $("#lblCellNo").hide();

    //    return false;
    //}
    else if ($(".Addess").val() == '') {

        $("#alerts").show();
        $("#lblAddess").show();
        $("#lblerrorCustomerName").hide();
        $("#lblphonenum").hide();
        $("#lblphonenum1").hide();
        $("#lblEmail").hide();
        $("#lblInt").hide();
        $("#lblNIC").hide();
        $("#lblCellNo").hide();

        return false;
    }  else if (!data.match(numericExpression)) {
        alert(data);
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