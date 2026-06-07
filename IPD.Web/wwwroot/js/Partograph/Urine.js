function InitUrine() {
    var ProteinTimeDropdown = $("#ProteinTimeDropdown");
    $.each(HalfHoursXAxis, function (i, v) {
        let startIndex = HalfHoursXAxis.findIndex(j => j == initiateTime);
        let index = startIndex % 2;
        if (index === -1) {
            index = 0;
        }
        if (i % 2 === index) {
            ProteinTimeDropdown.append($("<option />").val(v).text(v));
        }

    });


    var AcetoneTimeDropdown = $("#AcetoneTimeDropdown");
    $.each(HalfHoursXAxis, function (i, v) {
        let startIndex = HalfHoursXAxis.findIndex(j => j == initiateTime);
        let index = startIndex % 2;
        if (index === -1) {
            index = 0;
        }
        if (i % 2 === index) {
            AcetoneTimeDropdown.append($("<option />").val(v).text(v));
        }

    });

    var VolumeTimeDropdown = $("#VolumeTimeDropdown");
    $.each(HalfHoursXAxis, function (i, v) {
        let startIndex = HalfHoursXAxis.findIndex(j => j == initiateTime);
        let index = startIndex % 2;
        if (index === -1) {
            index = 0;
        }
        if (i % 2 === index) {
            VolumeTimeDropdown.append($("<option />").val(v).text(v));
        }

    });

}
var UrineTds = '';
for (let i = 1; i < XAxis.length; i += 2) {
    UrineTds += '<td class="TempDataCell"></td>';
}

var proteinData = [];
var acetoneData = [];
var volumeData = [];


$('#ProteinRow').html(UrineTds);
$('#AcetoneRow').html(UrineTds);
$('#VolumeRow').html(UrineTds);


function UpdateProtein(partographId) {
    let Value = $('#Protein').val();
    let Time = $('#ProteinTimeDropdown').val();

    let Index = hourXAxis.findIndex(a => a == Time);
    if (Index < 0) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }
    var AllTds = $('#ProteinRow').children('td');

    AllTds[Index].innerHTML = Value;

    let date = convertstringtodate(initiateDate, Time);
    let NewTime = moment(date);
    const xValue = moment.utc(NewTime).valueOf().toString();
    let cellData = [xValue, Value];

    const dataIndex = findIndexOfExistngGlobal(proteinData, xValue);
    if (dataIndex > -1) {
        proteinData[dataIndex] = cellData;
    } else {
        proteinData.push(cellData);
    }
    PostProtein(partographId);
}

function UpdateAcetone(partographId) {
    let Value = $('#Acetone').val();
    let Time = $('#AcetoneTimeDropdown').val();

    let Index = hourXAxis.findIndex(a => a == Time);
    if (Index < 0) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }
    var AllTds = $('#AcetoneRow').children('td');

    AllTds[Index].innerHTML = Value;


    let date = convertstringtodate(initiateDate, Time);
    let NewTime = moment(date);
    const xValue = moment.utc(NewTime).valueOf().toString();
    let cellData = [xValue, Value];

    const dataIndex = findIndexOfExistngGlobal(acetoneData, xValue);
    if (dataIndex > -1) {
        acetoneData[dataIndex] = cellData;
    } else {
        acetoneData.push(cellData);
    }
    PostAcetone(partographId);
}

function UpdateVolume(partographId) {
    let Value = $('#Volume').val();
    let Time = $('#VolumeTimeDropdown').val();

    let Index = hourXAxis.findIndex(a => a == Time);
    if (Index < 0) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }
    var AllTds = $('#VolumeRow').children('td');

    AllTds[Index].innerHTML = Value;


    let date = convertstringtodate(initiateDate, Time);
    let NewTime = moment(date);
    const xValue = moment.utc(NewTime).valueOf().toString();
    let cellData = [xValue, Value];

    const dataIndex = findIndexOfExistngGlobal(volumeData, xValue);
    if (dataIndex > -1) {
        volumeData[dataIndex] = cellData;
    } else {
        volumeData.push(cellData);
    }
    PostVolume(partographId);
}


function plotProtein() {
    if (proteinData.length > 0) {
        var AllTds = $('#ProteinRow').children('td');
        for (let v of proteinData) {
            let time = moment(Number(v[0])).format('HH:mm');
            let index = hourXAxis.findIndex(a => a == time);
            if (index > -1) {
                AllTds[index].innerHTML = v[1];
            }
        }
    }
}


function plotAcetone() {
    if (acetoneData.length > 0) {
        var AllTds = $('#AcetoneRow').children('td');
        for (let v of acetoneData) {
            let time = moment(Number(v[0])).format('HH:mm');
            let index = hourXAxis.findIndex(a => a == time);
            if (index > -1) {
                AllTds[index].innerHTML = v[1];
            }
        }
    }
}



function plotVolume() {
    if (volumeData.length > 0) {
        var AllTds = $('#VolumeRow').children('td');
        for (let v of volumeData) {
            let time = moment(Number(v[0])).format('HH:mm');
            let index = hourXAxis.findIndex(a => a == time);
            if (index > -1) {
                AllTds[index].innerHTML = v[1];
            }
        }
    }
}



function PostProtein(partographId) {

    const postData = {
        partographID: partographId,
        data: proteinData
    };

    baseApi.postRequest(
        "Proteins/AddProteins",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );

}

function PostAcetone(partographId) {

    const postData = {
        partographID: partographId,
        data: acetoneData
    };

    baseApi.postRequest(
        "Acetones/AddAcetones",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );

}

function PostVolume(partographId) {

    const postData = {
        partographID: partographId,
        data: volumeData
    };

    baseApi.postRequest(
        "Volumes/AddVolumes",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );

}
