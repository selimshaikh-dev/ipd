var ContractionTimeDropdown = $("#ContractionTimeDropdown");
$.each(HalfHoursXAxis, function () {
    ContractionTimeDropdown.append($("<option />").val(this).text(this));
});

var ContractionsTds = '';
for (let i = 0; i < HalfHoursXAxis.length; i += 2) {
    ContractionsTds += '<td class="TempDataCell"></td>';
}



$('#FirstCRow').html(ContractionsTds);
$('#SecondCRow').html(ContractionsTds);
$('#ThirdCRow').html(ContractionsTds);
$('#FourthCRow').html(ContractionsTds);
$('#FifthCRow').html(ContractionsTds);

var contractionData = [];

var contractionValue = [
    { 'Value': '1', 'Text': 'Contractions Less than 20 seconds'},
    { 'Value': '2', 'Text': 'Contractions lasting between 20-40 seconds'},
    { 'Value': '3', 'Text': 'Contractions more than 40 seconds'}
]


function UpdateContraction(partographId) {
    let Value = $('#Contraction').val();
    let Time = $('#ContractionTimeDropdown').val();

    let Index = HalfHoursXAxis.findIndex(a => a == Time) - HalfHoursXAxis.findIndex(a => a == initiateTime);
    if (Index < 0) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }
    let contractionDuration = $('#CDuration').val();

    updateColumn(Index, contractionDuration, Value);

    let date = convertstringtodate(initiateDate, Time);
    let timePoint = moment.utc(date).valueOf();
    let data = [timePoint.toString(), Value.toString(), getContractionsDuration(contractionDuration)];

    const dataIndex = findIndexOfExistngGlobal(contractionData, timePoint.toString());
    if (dataIndex > -1) {
        contractionData[dataIndex] = data;
    } else {
        contractionData.push(data);
    }
    PostContraction(partographId);

}


function PostContraction(partographId) {

    const postData = {
        partographID: partographId,
        data: contractionData
    };

    baseApi.postRequest(
        "Contractions/AddContractions",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );

}


function getContractionsDuration(value) {
    let data = contractionValue.filter((v, i) => v.Value == value);
    let output = '';
    if (data.length > 0) {
        output = data[0].Text;
    }
    return output;
}


function getContractionsDurationValue(text) {
    let data = contractionValue.filter((v, i) => v.Text == text);
    let output = '';
    if (data.length > 0) {
        output = data[0].Value;
    }
    return output;
}


function plotContractions() {
    if (contractionData.length > 0) {
        for (let v of contractionData) {
            let time = moment(Number(v[0])).format('HH:mm');
            let index = HalfHoursXAxis.findIndex(a => a == time) - HalfHoursXAxis.findIndex(a => a == initiateTime);
            let contractionDuration = getContractionsDurationValue(v[2]);

            updateColumn(index, contractionDuration, v[1]);

        }
    }
}

function updateColumn(Index, contractionDuration,value) {
    var FirstRowTds = $('#FirstCRow').children('td');
    var SecondRowTds = $('#SecondCRow').children('td');
    var ThirdRowTds = $('#ThirdCRow').children('td');
    var FourthRowTds = $('#FourthCRow').children('td');
    var FifthRowTds = $('#FifthCRow').children('td');


    FirstRowTds[Index].style.backgroundImage = '';
    FirstRowTds[Index].style.backgroundSize = '';
    SecondRowTds[Index].style.backgroundImage = '';
    SecondRowTds[Index].style.backgroundSize = '';
    ThirdRowTds[Index].style.backgroundImage = '';
    ThirdRowTds[Index].style.backgroundSize = '';
    FourthRowTds[Index].style.backgroundImage = '';
    FourthRowTds[Index].style.backgroundSize = '';
    FifthRowTds[Index].style.backgroundImage = '';
    FifthRowTds[Index].style.backgroundSize = '';



    var background = '';
    var backgroundSize = '';
    switch (contractionDuration) {
        case '1':
            background = 'url("../../images/CDot.png")'
            backgroundSize = "22px 33px";
            break;
        case '2':
            background = 'url("../../images/CDiag.jpg")'
            backgroundSize = "22px 33px";
            break;
        case '3':
            background = 'url("../../images/CBlack.png")'
            break;
        default:
            ImageUrl = '';
    }

    switch (value) {
        case '1':
            FirstRowTds[Index].style.backgroundImage = background;
            FirstRowTds[Index].style.backgroundSize = backgroundSize;
            break;
        case '2':
            FirstRowTds[Index].style.backgroundImage = background;
            FirstRowTds[Index].style.backgroundSize = backgroundSize;
            SecondRowTds[Index].style.backgroundImage = background;
            SecondRowTds[Index].style.backgroundSize = backgroundSize;
            break;
        case '3':
            FirstRowTds[Index].style.backgroundImage = background;
            FirstRowTds[Index].style.backgroundSize = backgroundSize;
            SecondRowTds[Index].style.backgroundImage = background;
            SecondRowTds[Index].style.backgroundSize = backgroundSize;
            ThirdRowTds[Index].style.backgroundImage = background;
            ThirdRowTds[Index].style.backgroundSize = backgroundSize;

            break;
        case '4':
            FirstRowTds[Index].style.backgroundImage = background;
            FirstRowTds[Index].style.backgroundSize = backgroundSize;
            SecondRowTds[Index].style.backgroundImage = background;
            SecondRowTds[Index].style.backgroundSize = backgroundSize;
            ThirdRowTds[Index].style.backgroundImage = background;
            ThirdRowTds[Index].style.backgroundSize = backgroundSize;
            FourthRowTds[Index].style.backgroundImage = background;
            FourthRowTds[Index].style.backgroundSize = backgroundSize;
            break;
        case '5':
            FirstRowTds[Index].style.backgroundImage = background;
            FirstRowTds[Index].style.backgroundSize = backgroundSize;
            SecondRowTds[Index].style.backgroundImage = background;
            SecondRowTds[Index].style.backgroundSize = backgroundSize;
            ThirdRowTds[Index].style.backgroundImage = background;
            ThirdRowTds[Index].style.backgroundSize = backgroundSize;
            FourthRowTds[Index].style.backgroundImage = background;
            FourthRowTds[Index].style.backgroundSize = backgroundSize;
            FifthRowTds[Index].style.backgroundImage = background;
            FifthRowTds[Index].style.backgroundSize = backgroundSize;
            break;
        default:
    }
}