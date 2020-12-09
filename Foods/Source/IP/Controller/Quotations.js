/*-- General Starts --*/

    $(".ProductName").focus();

    $("#lblErrorProductName").hide();
    $("#lblErrorProductQuantity").hide();
    $("#lblErrorProductUnits").hide();
    $("#lblErrorQuotedBy").hide();
    $("#lblErrorInt").hide();
    $("#lblErrorQuotedTo").hide();
    $("#lblErrorDate").hide();

/*-- General End --*/



/*--Validation Starts--*/

    var ProEmptyName = function () {

        if ($(".ProductName").val() == '') {
            $("#alerts").show();
            $('#lblErrorProductName').show();
            $("#lblErrorProductQuantity").hide();
            $("#lblErrorProductUnits").hide();
            $("#lblErrorQuotedBy").hide();
            $("#lblErrorInt").hide();
            $("#lblErrorQuotedTo").hide();
            $("#lblErrorDate").hide();

            return false;

        } else {

            $("#alerts").hide();
            $('#lblErrorProductName').hide();

            return true;
        }

    }


    var ProEmptyQty = function () {

        if ($(".ProductQuantity").val() == '') {
            $("#alerts").show();
            $('#lblErrorProductName').hide();
            $("#lblErrorProductQuantity").show();
            $("#lblErrorProductUnits").hide();
            $("#lblErrorQuotedBy").hide();
            $("#lblErrorInt").hide();
            $("#lblErrorQuotedTo").hide();
            $("#lblErrorDate").hide();

            return false;

        } else {

            $("#alerts").hide();
            $('#lblErrorProductQuantity').hide();

            return true;
        }

    }

    var ProIntQty = function () {
        var ProQuantity = $(".ProductQuantity").val();

        if ($.isNumeric(ProQuantity)) {
        
            console.log("it is an Integer!");

            return false;

        } else {

            $("#alerts").show();
            $('#lblErrorProductName').hide();
            $("#lblErrorProductQuantity").hide();
            $("#lblErrorProductUnits").hide();
            $("#lblErrorQuotedBy").hide();
            $("#lblErrorInt").show();
            $("#lblErrorQuotedTo").hide();
            $("#lblErrorDate").hide();

            return true;
        }

    }
    var ProEmptyUnits = function () {

        if ($(".Units").val() == '') {
            $("#alerts").show();
            $('#lblErrorProductName').hide();
            $("#lblErrorProductQuantity").hide();
            $("#lblErrorProductUnits").show();
            $("#lblErrorQuotedBy").hide();
            $("#lblErrorInt").hide();
            $("#lblErrorQuotedTo").hide();
            $("#lblErrorDate").hide();

            return false;

        } else {

            $("#alerts").hide();
            $('#lblErrorProductUnits').hide();

            return true;
        }

    }


    var ProEmptyQuotBy = function () {

        if ($(".Units").val() == '') {
            $("#alerts").show();
            $('#lblErrorProductName').hide();
            $("#lblErrorProductQuantity").hide();
            $("#lblErrorProductUnits").hide();
            $("#lblErrorQuotedBy").show();
            $("#lblErrorInt").hide();
            $("#lblErrorQuotedTo").hide();
            $("#lblErrorDate").hide();

            return false;

        } else {

            $("#alerts").hide();
            $('#lblErrorQuotedBy').hide();

            return true;
        }

    }


    var ProEmptyQuotTo = function () {

        if ($(".Units").val() == '') {
            $("#alerts").show();
            $('#lblErrorProductName').hide();
            $("#lblErrorProductQuantity").hide();
            $("#lblErrorProductUnits").hide();
            $("#lblErrorQuotedBy").hide();
            $("#lblErrorInt").hide();
            $("#lblErrorQuotedTo").show();
            $("#lblErrorDate").hide();

            return false;

        } else {

            $("#alerts").hide();
            $('#lblErrorQuotedTo').hide();

            return true;
        }

    }


    var check = function () {

        var regexemail = /^([0-9a-zA-Z]([-_\\.]*[0-9a-zA-Z]+)*)@([0-9a-zA-Z]([-_\\.]*[0-9a-zA-Z]+)*)[\\.]([a-zA-Z]{2,9})$/;
        var regexphone = /^(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$/i;
        var numericExpression = /^[0-9]+$/;

        if ($(".ProductName").val() == '') {

            $("#alerts").show();
            $('#lblErrorProductName').hide();
            $("#lblErrorProductQuantity").hide();
            $("#lblErrorProductUnits").hide();
            $("#lblErrorQuotedBy").hide();
            $("#lblErrorInt").hide();
            $("#lblErrorQuotedTo").show();
            $("#lblErrorDate").hide();

            return false;

        } else if ($(".ProductQuantity").val() == '') {

            $("#alerts").hide();
            $('#lblErrorProductName').show();
            $("#lblErrorProductQuantity").hide();
            $("#lblErrorProductUnits").hide();
            $("#lblErrorQuotedBy").hide();
            $("#lblErrorInt").hide();
            $("#lblErrorQuotedTo").show();
            $("#lblErrorDate").hide();

            return false;

        } else if ($(".Units").val() == '') {

            $("#alerts").show();
            $('#lblErrorProductName').hide();
            $("#lblErrorProductQuantity").hide();
            $("#lblErrorProductUnits").show();
            $("#lblErrorQuotedBy").hide();
            $("#lblErrorInt").hide();
            $("#lblErrorQuotedTo").hide();
            $("#lblErrorDate").hide();

            return false;

        } else if ($(".QuotedBy").val() == '') {

            $("#alerts").show();
            $('#lblErrorProductName').hide();
            $("#lblErrorProductQuantity").hide();
            $("#lblErrorProductUnits").hide();
            $("#lblErrorQuotedBy").show();
            $("#lblErrorInt").hide();
            $("#lblErrorQuotedTo").hide();
            $("#lblErrorDate").hide();

            return false;

        } else if ($(".QuotedTo").val() == '') {

            $("#alerts").show();
            $('#lblErrorProductName').hide();
            $("#lblErrorProductQuantity").hide();
            $("#lblErrorProductUnits").hide();
            $("#lblErrorQuotedBy").hide();
            $("#lblErrorInt").hide();
            $("#lblErrorQuotedTo").show();
            $("#lblErrorDate").hide();

            return false;

        } else if ($.isNumeric($(".ProductQuantity").val())) {
            
            console.log("it is Not Integer!");
            $("#alerts").show();
            $("#lblInt").show();

            return false;
        } else {

            return true;
        }
    }



/*--Validations Ends--*/
