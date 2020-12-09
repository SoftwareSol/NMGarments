
/*-Load Initial Parameters in Client Side Start-*/

$("#alerts").hide();

$(".AccGenID").attr("disabled", true);
$(".SubAccGenID").attr("disabled", true);
$(".AccCatNameID").attr("disabled", true);
$(".AccCatfourGenID").attr("disabled", true);
$(".SubAccCatFiveGenID").attr("disabled", true);




/*-Load Initial Parameters in Client Side End-*/


/*--Check Empty Validation Starts--*/


var InsertAcc = function () {

    if ($(".InsertAccount").val() == '') {
        $("#alerts").show();

        $('#lblHead').show();
        $('#lblSubHead').hide();
        $('#lblHeadCategories').hide();
        $('#lblHeadCategoryFour').hide();
        $('#lblHeadCategoryFive').hide();

        return false;

    } else {

        $("#alerts").hide();
        $('#lblHead').hide();

        return true;
    }

}

var InsertSubAcc = function () {

    if ($(".SubAccount").val() == '' || $(".Head").val() == '0') {

        $("#alerts").show();

        $('#lblSubHead').show();        
        $('#lblHead').hide();
        $('#lblHeadCategories').hide();
        $('#lblHeadCategoryFour').hide();
        $('#lblHeadCategoryFive').hide();

        return false;

    } else {

        $("#alerts").hide();
        $('#lblSubHead').hide();
     
        return true;
    }

}

var InsertAccCat = function () {

    if ($(".AccountCategories").val() == '' || $(".AccName").val() == '0' || $(".SubAccName").val() == '0') {
        $("#alerts").show();

        $('#lblHeadCategories').show();
        $('#lblSubHead').hide();
        $('#lblHead').hide();        
        $('#lblHeadCategoryFour').hide();
        $('#lblHeadCategoryFive').hide();

        return false;

    } else {

        $("#alerts").hide();
        $('#lblHeadCategories').hide();

        return true;
    }

}

var IntAccCatFour = function () {

    if ($(".AccCatFour").val() == '' || $(".CatefourAccName").val() == '0' || $(".CatfourSubAccName").val() == '0' || $(".CatfourSubAccCatName").val() == '0') {

        $("#alerts").show();
        $('#lblHeadCategoryFour').show();
        $('#lblHeadCategories').hide();
        $('#lblSubHead').hide();
        $('#lblHead').hide();
        $('#lblHeadCategoryFive').hide();

        return false;

    } else {

        $("#alerts").hide();
        $('#lblHeadCategoryFour').hide();

        return true;
    }

}

var IntAccCatFive = function () {

    if ($(".AccCatFive").val() == '' || $(".CatefiveAccName").val() == '0' || $(".CatfiveSubAccName").val() == '0' || $(".CatfiveSubAccCatName").val() == '0' || $(".CatfiveSubAccCatfourName").val() == '0') {
        $("#alerts").show();

        $('#lblHeadCategoryFive').show();
        $('#lblHeadCategoryFour').hide();
        $('#lblHeadCategories').hide();
        $('#lblSubHead').hide();
        $('#lblHead').hide();

        return false;

    } else {

        $("#alerts").hide();
        $('#lblHeadCategoryFive').hide();

        return true;
    }

}

var check = function () {

    if ($(".SubAccount").val() == '') {

        $("#alerts").show();

        $('#lblSubHead').show();
        $('#lblHead').hide();
        $('#lblHeadCategories').hide();
        $('#lblHeadCategoryFour').hide();
        $('#lblHeadCategoryFive').hide();

        return false;

    } else if ($(".SubAccount").val() == '') {

        $("#alerts").show();

        $('#lblSubHead').show();
        $('#lblHead').hide();
        $('#lblHeadCategories').hide();
        $('#lblHeadCategoryFour').hide();
        $('#lblHeadCategoryFive').hide();

        return false;

    } else if ($(".AccountCategories").val() == '') {

        $("#alerts").show();

        $('#lblHeadCategories').show();
        $('#lblSubHead').hide();
        $('#lblHead').hide();
        $('#lblHeadCategoryFour').hide();
        $('#lblHeadCategoryFive').hide();

        return false;

    } else if ($(".AccCatFour").val() == '') {

        $("#alerts").show();

        $('#lblHeadCategoryFour').show();
        $('#lblHeadCategories').hide();
        $('#lblSubHead').hide();
        $('#lblHead').hide();
        $('#lblHeadCategoryFive').hide();

        return false;

    } else if ($(".AccCatFive").val() == '') {

        $("#alerts").show();

        $('#lblHeadCategoryFive').show();
        $('#lblHeadCategoryFour').hide();
        $('#lblHeadCategories').hide();
        $('#lblSubHead').hide();
        $('#lblHead').hide();

        return false;

    } else {

        return true;
    }
}
/*--Check Empty Validation Ends--*/



