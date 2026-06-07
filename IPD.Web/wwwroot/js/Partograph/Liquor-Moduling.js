var LiquorTimeDropdown = $("#LiquorTimeDropdown");
$.each(HalfHoursXAxis, function () {
    LiquorTimeDropdown.append($("<option />").val(this).text(this ));
});

var MoldingTimeDropdown = $("#MoldingTimeDropdown");
$.each(HalfHoursXAxis, function () {
    MoldingTimeDropdown.append($("<option />").val(this).text(this ));
});

// LiquorMoldingTable

var LiquorMoldingTds = '';
for (let i = 0; i < HalfHoursXAxis.length; i += 2) {
    LiquorMoldingTds += '<td class="TempDataCell"></td>';
}

var moldingsData = [];
var liquorsData = [];



$('#LiquorRow').html(LiquorMoldingTds);
$('#MoldingRow').html(LiquorMoldingTds);

//UpdateMolding
function UpdateLiquor(partographId) {
    let Liquor = $('#Liquor').val();
    let LiquorTime = $('#LiquorTimeDropdown').val();

    var LiquorIndex = HalfHoursXAxis.findIndex(a => a == LiquorTime) - HalfHoursXAxis.findIndex(a => a == initiateTime);
    if (LiquorIndex < 0) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }
    var AllLiquorTds = $('#LiquorRow').children('td');

    AllLiquorTds[LiquorIndex].innerHTML = Liquor;

    let date = convertstringtodate(initiateDate, LiquorTime);
    let NewTime = moment(date);
    const xValue = moment.utc(NewTime).valueOf();
    let cellData = [xValue.toString(), Liquor]
    liquorsData.push(cellData);

    PostLiquor(partographId);
}

function UpdateMolding(partographId) {
    let Molding = $('#Molding').val();
    let MoldingTime = $('#MoldingTimeDropdown').val();

    let MoldingIndex = HalfHoursXAxis.findIndex(a => a == MoldingTime) - HalfHoursXAxis.findIndex(a => a == initiateTime);
    if (MoldingIndex < 0) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }
    var AllMoldingTds = $('#MoldingRow').children('td');

    AllMoldingTds[MoldingIndex].innerHTML = Molding;



    let date = convertstringtodate(initiateDate, MoldingTime);
    let NewTime = moment(date);
    const xValue = moment.utc(NewTime).valueOf();
    let cellData = [xValue.toString(), Molding]
    moldingsData.push(cellData);

    PostMolding(partographId);
}

function PostMolding(partographId) {
    const postData = {
        partographID: partographId,
        data: moldingsData
    };

    baseApi.postRequest(
        "Mouldings/AddMouldings",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );

}


function PostLiquor(partographId) {
    const postData = {
        partographID: partographId,
        data: liquorsData
    };

    baseApi.postRequest(
        "Liquors/AddLiquors",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );

}

function plotLiquor() {
    if (liquorsData.length > 0) {
        var AllLiquorTds = $('#LiquorRow').children('td');
        for (let v of liquorsData) {
            let LiquorTime = moment(Number(v[0])).format('HH:mm');
            let LiquorIndex = HalfHoursXAxis.findIndex(a => a == LiquorTime) - HalfHoursXAxis.findIndex(a => a == initiateTime);
            if (LiquorIndex > -1) {
                AllLiquorTds[LiquorIndex].innerHTML = v[1];
            }
        }
    }
}

function plotMolding() {
    if (liquorsData.length > 0) {
        var AllMoldingTds = $('#MoldingRow').children('td');
        for (let v of moldingsData) {
            let time = moment(Number(v[0])).format('HH:mm');
            let index = HalfHoursXAxis.findIndex(a => a == time) - HalfHoursXAxis.findIndex(a => a == initiateTime);
            if (index > -1) {
                AllMoldingTds[index].innerHTML = v[1];
            }
        }
    }
}
