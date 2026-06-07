$(document).ready(function () {
    partographId = $('#hdnPartographID').val();
    // partographId = '29235b36-a31d-4d6c-b546-da89b8b52d76';
    if (partographId !== '00000000-0000-0000-0000-000000000000') {
        $('#InitiateDate').prop('disabled', 'disabled');
        $('#InitiateTime').prop('disabled', 'disabled');
    } 

    $('input#InitiateDate').datetimepicker({
        timepicker: false,
        format: 'Y-m-d',
    });
    if ($("#Parity").val() == 0) {
        $("#Parity").val('');
    }
    if ($("#Abortion").val() == 0) {
        $("#Abortion").val('');
    }
    let RegularContractionsDropdown = $("#InitiateTime");
    $.each(HalfHoursXAxis, function () {
        RegularContractionsDropdown.append($("<option />").val(this).text(this));
    });

    $('input#birthDate').datetimepicker({
        timepicker: false,
        format: 'Y-m-d',
    });
    $('input#birthTime').datetimepicker({
        datepicker: false,
        format: 'H:i'
    });

    $('input#RegularContractions').datetimepicker({
        timepicker: false,
        format: 'Y-m-d',
    });
    $('input#MembranesRuptured').datetimepicker({
        timepicker: false,
        format: 'Y-m-d',
    });

    //EDD CALCULATION
    $('#lmpDate').datetimepicker({
        timepicker: false,
        format: 'Y-m-d',
    });

    function formatDate(date) {
        var monthNames = [
            "01", "02", "03",
            "04", "05", "06", "07",
            "08", "09", "10",
            "11", "12"
        ];
        date = new Date(date);
        var day = date.getDate();
        var monthIndex = date.getMonth();
        var year = date.getFullYear();
        return year + '-' + monthNames[monthIndex] + '-' + day;
    }

    function addDays(date, days) {
        var result = new Date(date);
        result.setDate(result.getDate() + days);
        return result;
    }

    $("#lmpDate").change(function () {
        var kmpDate = $('#lmpDate').val();
        var eddDate = formatDate(addDays(kmpDate, 280));
        $('#eddDate').val(eddDate)
    });

    $("#DeliveryInfo").change(function () {
        if ($("#DeliveryInfo").val() == 1) {
            $("#hidden-panel").show()
            $('#DeliveryInfo').val() = true;
        } else {
            $("#hidden-panel").hide()
            $('#DeliveryInfo').val() = false;
        }
    });

    baseApi.getRequest(
        'Partograph/FindPartographByKey/' + partographId,
        (res) => {
            initiateDate = res.initiateDate;
            initiateTime = res.initiateTime;
            $("#InitiateTime").val(initiateTime);
            initiateAllCharts(res.initiateDate, res.initiateTime);
            baseApi.getRequest(
                'PartographDetails/FindPartographDetailsByParographId/' + partographId,
                (res1) => {
                    DynamicDataSeries = res1.fetalHeartRateData;
                    cervix = res1.cervixData;
                    DescentHead = res1.descentData;
                    contractionData = res1.contractionsData;
                    drugsData = res1.medicineData;
                    temparatureData = res1.temparatureData;
                    moldingsData = res1.mouldingData;
                    liquorsData = res1.liquorData;
                    oxytocinData = res1.oxytocinData;
                    dropsData = res1.dropsData;
                    bpPost = res1.bloodPressureData;
                    Pulse = res1.pulseData;
                    proteinData = res1.proteinData;
                    acetoneData = res1.acetoneData;
                    volumeData = res1.volumeData;
                    createBloodPressureData();
                    createAllChart();
                },
                (err1) => {
                    console.log(err1);
                    createAllChart();
                }
            )
        },
        (err) => {
            console.log(err);
            initiateAllCharts();
            createAllChart();
        }
    )

    $('#btnInitiate').click(() => {
        const date = $('input#InitiateDate').datetimepicker('getDate');
        const time = $('#InitiateTime').val();
        initiateAllCharts(date.val(), time);
        createAllChart();
    });
    let cdot = $('#legend').children('td');
    let backgroundSize = '22px 33px';
    cdot[0].style.backgroundImage = 'url("../../images/CDot.png")';
    cdot[2].style.backgroundImage = 'url("../../images/CDiag.jpg")';
    cdot[4].style.backgroundImage = 'url("../../images/CBlack.png")';
    cdot[0].style.backgroundSize = backgroundSize;
    cdot[2].style.backgroundSize = backgroundSize;
    cdot[4].style.backgroundSize = backgroundSize;


});


const Toast = Swal.mixin({
    showCloseButton: true,
    position: 'top-right',
    iconColor: 'white',
    customClass: {
        popup: 'colored-toast'
    },
    showConfirmButton: false
});

const inittimeString = $('#InitiateTimeValue').val();

function ShowToast(message) {
    $('#toastParagraph').html(message);
    Toast.fire({
        icon: 'warning',
        text: message 
    })
}
