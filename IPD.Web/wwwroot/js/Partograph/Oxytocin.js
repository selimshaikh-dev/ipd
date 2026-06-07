var oxytocinData = [];
var dropsData = [];
var OxytocinTimeDropdown = $("#OxytocinTimeDropdown");
$.each(HalfHoursXAxis, function () {
    OxytocinTimeDropdown.append($("<option />").val(this).text(this));

});

var DropsTimeDropdown = $("#DropsTimeDropdown");
$.each(HalfHoursXAxis, function () {
    DropsTimeDropdown.append($("<option />").val(this).text(this));

});


var OxytocinDropsTds = '';
for (let i = 0; i < HalfHoursXAxis.length; i += 2) {
    OxytocinDropsTds += '<td class="TempDataCell"></td>';
}


$('#OxytocinRow').html(OxytocinDropsTds);
$('#DropsRow').html(OxytocinDropsTds);



function UpdateOxytocin(partographId) {
    let Value = $('#Oxytocin').val();
    let Time = $('#OxytocinTimeDropdown').val();

    let Index = HalfHoursXAxis.findIndex(a => a == Time) - HalfHoursXAxis.findIndex(a => a == initiateTime);
    if (Index < 0) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }

    var AllTds = $('#OxytocinRow').children('td');

    AllTds[Index].innerHTML = Value;

    this.UpdateDrops(partographId);



    let date = convertstringtodate(initiateDate, Time);
    let NewTime = moment(date);
    const xValue = moment.utc(NewTime).valueOf();
    let cellData = [xValue, Value];

    const dataIndex = findIndexOfExistngGlobal(oxytocinData, xValue);
    if (dataIndex > -1) {
        oxytocinData[dataIndex] = cellData;
    } else {
        oxytocinData.push(cellData);
    }
    PostOxytocin(partographId);

}

function UpdateDrops(partographId) {
    let Value = $('#Drops').val();
    let Time = $('#OxytocinTimeDropdown').val();

    let Index = HalfHoursXAxis.findIndex(a => a == Time) - HalfHoursXAxis.findIndex(a => a == initiateTime);
    if (Index < 0) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }

    var AllTds = $('#DropsRow').children('td');

    AllTds[Index].innerHTML = Value;



    let date = convertstringtodate(initiateDate, Time);
    let NewTime = moment(date);
    const xValue = moment.utc(NewTime).valueOf();
    let cellData = [xValue, Value];

    const dataIndex = findIndexOfExistngGlobal(dropsData, xValue);
    if (dataIndex > -1) {
        dropsData[dataIndex] = cellData;
    } else {
        dropsData.push(cellData);
    }
    PostDrops(partographId);

}


function plotOxytocin() {
    if (oxytocinData.length > 0) {
        var AllTds = $('#OxytocinRow').children('td');
        for (let v of oxytocinData) {
            let time = moment(Number(v[0])).format('HH:mm');
            let index = hourXAxis.findIndex(a => a == time);
            if (index > -1) {
                AllTds[index].innerHTML = v[1];
            }
        }
    }
}


function plotDrops() {
    if (dropsData.length > 0) {
        var AllTds = $('#DropsRow').children('td');
        for (let v of dropsData) {
            let time = moment(Number(v[0])).format('HH:mm');
            let index = hourXAxis.findIndex(a => a == time);
            if (index > -1) {
                AllTds[index].innerHTML = v[1];
            }
        }
    }
}


function PostOxytocin(partographId) {
    const postData = {
        partographID: partographId,
        data: oxytocinData
    };

    baseApi.postRequest(
        "Oxytocin/AddOxytocin",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );
}


function PostDrops(partographId) {
    const postData = {
        partographID: partographId,
        data: dropsData
    };

    baseApi.postRequest(
        "Drops/AddDrops",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );
}