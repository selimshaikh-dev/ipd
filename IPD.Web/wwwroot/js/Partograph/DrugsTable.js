function InitDrugs() {
    var DrugsTimeDropdown = $("#DrugsTimeDropdown");
    $.each(HalfHoursXAxis, function (i, v) {
        let startIndex = HalfHoursXAxis.findIndex(j => j == initiateTime);
        let index = startIndex % 2;
        if (index === -1) {
            index = 0;
        }
        if (i % 2 === index) {
            DrugsTimeDropdown.append($("<option />").val(v).text(v));
        }
    });

    var TempTimeDropdown = $("#TempTimeDropdown");
    $.each(HalfHoursXAxis, function (i, v) {
        let startIndex = HalfHoursXAxis.findIndex(j => j == initiateTime);
    
        let index = startIndex % 2;
        if (index === -1) {
            index = 0;
        }
        if (i % 2 === index) {
            TempTimeDropdown.append($("<option />").val(v).text(v));
        }

    });
}

var DrugTds = $('#DrugsTable  > tbody  > tr').html();
for (let i = 1; i < 25; i += 2) {
    DrugTds += '<td class="MedicineDataCell"></td>';
}

$('#DrugsTable  > tbody  > tr').html(DrugTds);


var TempTds = $('#TempTable  > tbody  > tr').html();
for (let i = 1; i < 25; i += 2) {
    TempTds += '<td class="TempDataCell" rowspan="2"></td>';
}

$('#TempTable  > tbody  > tr').html(TempTds);

var drugsData = [];
var temparatureData = [];



function UpdateDrugs(partographId){
   var Drug =  $('#Medicine').val();
    var DrugTime = $('#DrugsTimeDropdown').val();
    let DrugIndex = hourXAxis.findIndex(a =>a == DrugTime);
    if (DrugIndex < 0) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }
    var AllDrugTds = $('#DrugsTable  > tbody  > tr').children('td');
    AllDrugTds[DrugIndex].innerHTML = Drug;


    let date = convertstringtodate(initiateDate, DrugTime);
    let NewTime = moment(date);
    let xValue = moment.utc(NewTime).valueOf();
    let cellData = [xValue.toString(), Drug];
    const dataIndex = findIndexOfExistngGlobal(drugsData, xValue.toString());
    if (dataIndex > -1) {
        drugsData[dataIndex] = cellData;
    } else {
        drugsData.push(cellData);
    }
    PostDrugs(partographId);

}

function UpdateTemp(partographId) {
    var Temp = $('#Temperature').val();
    var TempTime = $('#TempTimeDropdown').val();
    let TempIndex = hourXAxis.findIndex(a => a == TempTime);
    if ((temparatureData.length === 0 && TempIndex > 0) || TempIndex < 0) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }
    var AllTempTds = $('#TempTable  > tbody  > tr').children('td');
    AllTempTds[TempIndex].innerHTML = Temp;

    let date = convertstringtodate(initiateDate, TempTime);
    let NewTime = moment(date);
    const xValue = moment.utc(NewTime).valueOf();
    let cellData = [xValue, Temp];

    const dataIndex = findIndexOfExistngGlobal(temparatureData, xValue);
    if (dataIndex > -1) {
        temparatureData[dataIndex] = cellData;
    } else {
        temparatureData.push(cellData);
    }

    PostTemperatures(partographId);
}

function PostDrugs(partographId) {

    const postData = {
        partographID: partographId,
        data: drugsData
    };

    baseApi.postRequest(
        "Medicines/AddMedicines",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );

}

function PostTemperatures(partographId) {

    const postData = {
        partographID: partographId,
        data: temparatureData
    };

    baseApi.postRequest(
        "Temperatures/AddTemperatures",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );

}


function plotDrugs() {
    if (drugsData.length > 0) {
        var AllDrugTds = $('#DrugsTable  > tbody  > tr').children('td');
        for (let v of drugsData) {
            let time = moment(Number(v[0])).format('HH:mm');
            let drugIndex = hourXAxis.findIndex(a => a == time);
            if (drugIndex > -1) {
                AllDrugTds[drugIndex].innerHTML = v[1];
            }
        }
    }
}



function plotTemperatures() {
    if (temparatureData.length > 0) {
        var AllTempTds = $('#TempTable  > tbody  > tr').children('td');
        for (let v of temparatureData) {
            let time = moment(Number(v[0])).format('HH:mm');
            let temparatureIndex = hourXAxis.findIndex(a => a == time);
            if (temparatureIndex > -1) {
                AllTempTds[temparatureIndex].innerHTML = v[1];
            }
        }
    }
}

