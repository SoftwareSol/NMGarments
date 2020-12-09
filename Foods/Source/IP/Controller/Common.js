



$('#btnalertOk').click(function () {
   // $("#ModalAlert").hide();
});


/* Check for Empty*/



var checkforempty = function (Field) {
    
    if ($(Field).val() == '') {

        $("#alerts").show();

        // Stop the PostBack...
        return false;

    } else {

        $("#alerts").hide();

        // Execute the PostBack...
        return true;
    }
}


/* Check for Integer */

var checkint = function (data, field) {

    var numericExpression = /^[0-9]+$/;
    if (data != '') {
        if (data.match(numericExpression)) {
            console.log("Yes it is Integer!");
            return true;

        } else {
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

        }
    }
    else {
        console.log('empty');
    }
}

/* Check for Phone Num */

function ValidatePhone(phone, field) {
    
    var regex = /^(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$/i;

    if (regex.test(phone)) {
        return true;
        $("#alerts").hide();

    } else {

        $("#alerts").show();
        $("#lblphonenum").show();
        $("#lblerrorCustomerName").hide();
        $("#lblphonenum1").hide();
        $("#lblEmail").hide();
        $("#lblInt").hide();
        $("#lblNIC").hide();
        $("#lblAddess").hide();
        $("#lblCellNo").hide();

        $(field).focus();
        return false;

    }
}


/* Check for Email */



function isEmailAddress(str) {

    var pattern = "^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";

    //str = "azamsharp@gmail.com";
    
    if (str.match(pattern)) {
        alert(str);
    } else {
        alert('do not match');
    }
    //return str.match(pattern);

}

function validateEmail(email) {
     
    var regex = /^([0-9a-zA-Z]([-_\\.]*[0-9a-zA-Z]+)*)@([0-9a-zA-Z]([-_\\.]*[0-9a-zA-Z]+)*)[\\.]([a-zA-Z]{2,9})$/;

    if (!regex.test(email)) {
        $("#alerts").show();
        $("#lblEmail").show();
        $("#lblerrorCustomerName").hide();
        $("#lblphonenum").hide();
        $("#lblphonenum1").hide();
        $("#lblInt").hide();
        $("#lblNIC").hide();
        $("#lblAddess").hide();
        $("#lblCellNo").hide()
        return false;
    }
}


/* Alert */


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


function DisplayLoadingImage() {

    if ($(".Search").val() == '') {

        document.getElementById("HiddenLoadingImage").style.display = "none";

    } else {

        document.getElementById("HiddenLoadingImage").style.display = "block";
        $("body").css({ opacity: 0.9 });
    }
};


