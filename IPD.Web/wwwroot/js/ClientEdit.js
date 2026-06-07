function onddlInkhundlachange() {
    let baseUrl = $('#baseurlstring').val();
    $("#ddlChiefdomID").empty();
    $.ajax({
        url: `${baseUrl}/Chiefdoms/LoadChiefdom`,
        dataType: "json",
        type: "GET",
        data: { InkhundlaID: $("#ddlInkhundla").val() },
        success: function (data) {
            //  console.log(data);
            var items = "";
            items = "<option value=\"\">Select</option>";
            $.each(data, function (i, item) {
                items += `<option selected value="${item.chiefdomID}">${item.name}</option>`;
            });

            $("#ddlChiefdomID").html(items);
        }
    });
}

$(document).ready(function () {
    //MAPPING RELAVANT CHIEFDOMS TO INKHUNDLA
    onddlInkhundlachange();
    var pin = $("#txtIdentifire").val();
    // console.log(pin);

    //SELECTS THE APPROPIATE RADIO BUTTON ON LOAD
    if (pin == "9999999999999") {
        $("#optCode9").attr("checked", true);
        $('#txtIdentifire').val(pin);
        removeReadonlyAttr();
        //  BuildDatePicker();
    }
    else if (pin == "1111111111111") {
        $("#optCode1").attr("checked", true);
        $('#txtIdentifire').val(pin);
        removeReadonlyAttr();
        //  BuildDatePicker();
    }
    else {
        $("#optPIN").prop("checked", true);
        addReadonlyAttr();
        //BuildDatePicker();
        DisableDemographics();
    }

    $("#ddlChiefdomID").html("<option value=\"\">Select</option>");

    $('[data-toggle="tooltip"]').tooltip();
    $("#btnSave").click(function () {
        if (!$("#frmClientProfile").valid()) {
            bootbox.alert("One or more required fields are empty! Please review your entry before saving.");
            return false;
        }

        if ($("#ddlMaritalStatus").val() == 0) {
            bootbox.alert("Marital status is required!");
            return false;
        }

        if ($("#ddlNationality").val() == 0) {
            bootbox.alert("Nationality is required!");
            return false;
        }

        if ($("#ddlTinkhundlaID").val() == 0) {
            bootbox.alert("Inkhundla is required!");
            return false;
        }

        if ($("#ddlChiefdomID").val() == null || $("#ddlChiefdomID").val() == 0) {
            bootbox.alert("Chiefdom is required!");
            return false;
        }
    });

    //DISABLING NON-NUMERIC CHARACTER INPUT FOR COUNTRY CODE AND CELL/LANDPHONE
    $("#txtIdentifire, #txtCellphoneCountryCode, #txtCellphone, #txtLandPhoneCountryCode, #txtLandPhone").bind("keypress", function (e) {
        var characterCode = (e.which) ? e.which : e.keyCode;

        if (characterCode == 8 || characterCode == 9 || characterCode == 37 || characterCode == 39)
            return true;

        if (characterCode < 48 || characterCode > 57)
            return false;

        return true;
    });

    //DISABLING INPUT FOR DEMOGRAPHICS IF PIN IS SUPPLIED
    $("#optPIN").click(function () {
        $('#txtIdentifire').val('');
        ClearDemographics();
        DisableDemographics();
    });
});

function DisableDemographics() {
    $("#txtFirstName").bind("keypress, keydown", false)
    $("#txtMiddleName").bind("keypress, keydown", false);
    $("#txtLastName").bind("keypress, keydown", false);
    $("#txtDOB").bind("keypress, keydown", false);
}

function EnableDemographics() {
    $("#txtFirstName").val("").unbind("keypress, keydown")
    $("#txtMiddleName").val("").unbind("keypress, keydown");
    $("#txtLastName").val("").unbind("keypress, keydown");
    $("#txtDOB").val("").unbind("keypress, keydown");
}

function ClearDemographics() {
    $("#txtFirstName").val("")
    $("#txtMiddleName").val("")
    $("#txtLastName").val("")
    $("#txtDOB").val("")
    $("#ddlSex").val("")
}
function removeReadonlyAttr() {
    console.log('remove');
    $('#txtFirstName').attr('readonly', false);
    $('#txtMiddleName').attr('readonly', false);
    $('#txtLastName').attr('readonly', false);
    $('#datepicker').attr('disabled', false);
    $('#sex').attr('disabled', false);
    $('#dob').attr("disabled", true);
    $('#gender').attr('disabled', true);
}
function addReadonlyAttr() {
    console.log('add');
    $('#txtFirstName').attr('readonly', true);
    $('#txtMiddleName').attr('readonly', true);
    $('#txtLastName').attr('readonly', true);
    $('#datepicker').attr('disabled', true);
    $('#sex').attr('disabled', true);
    $('#dob').attr("disabled", false);
    $('#gender').attr('disabled', false);
}

$('#optCode9').click(function () {
    $('#txtIdentifire').val();
    $('#txtIdentifire').val(9999999999999);
    ClearDemographics();
    // BuildDatePicker();
    EnableDemographics();
});

$("#optCode1").click(function () {
    $('#txtIdentifire').val();
    $('#txtIdentifire').val(1111111111111);
    //ClearDemographics();
    // BuildDatePicker();
    EnableDemographics();
});

function onlyAlphabets(e, t) {
    if (window.event) {
        var charCode = window.event.keyCode;
    }
    else if (e) {
        var charCode = e.which;
    }
    else { return true; }

    if (charCode == 8 || charCode == 9 || charCode == 32 || charCode == 37 || charCode == 39)
        return true;

    if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
        return true;
    else
        return false;
}


$(function () {
    $("#datepicker").datepicker({
        dateFormat: 'yy-mm-dd',
        showButtonPanel: true,
        changeMonth: true,
        changeYear: true,
        yearRange: '1900:2022',
        inline: true,
        maxDate: 0
    });

});
$("#btnPicker").click(function () {

    $("#datepicker").focus();
});

// For date validation
function checkFilled() {
    var inputVal = document.getElementById("datepicker");
    if (inputVal.value == "") {
        inputVal.style.backgroundColor = "white";
    }
    else {
        inputVal.style.backgroundColor = "white";
    }
}
checkFilled();

//Phone number validation
function onlyNumberKey(evt) {

    // Only ASCII character in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
};
//Disable int value for string field
function alphaOnly(event) {
    var key = event.keyCode;
    return ((key >= 65 && key <= 90) || key == 8);
};

function validateForm() {
    let a = document.forms["ClientsEditForm"]["ContactAddress"].value;

    if (a == "") {
        document.getElementById("ContactAddressError").innerHTML = "The Residential Address is required!";
        return false;
    } else {
        document.getElementById("ContactAddressError").innerHTML = " ";
    }
}