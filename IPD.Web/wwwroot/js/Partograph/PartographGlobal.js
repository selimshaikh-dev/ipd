
const XAxis = [ '0','1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19'
    , '20', '21', '22', '23','24'
];

const YAxis = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '10'];

const timezone  = moment.tz.guess();
Highcharts.setOptions({
    time: {
        timezone: timezone
    }
});
const halfHourInterval = 1800 * 1000;
const hourInterval = 3600 * 1000;
var initiateDate = '';
var initiateTime = '';
var partographId = '';
var hourXAxis = [];

const HalfHoursXAxis = [
    '00:00',
    '00:30',
    '01:00',
    '01:30',
    '02:00',
    '02:30',
    '03:00',
    '03:30',
    '04:00',
    '04:30',
    '05:00',
    '05:30',
    '06:00',
    '06:30',
    '07:00',
    '07:30',
    '08:00',
    '08:30',
    '09:00',
    '09:30',
    '10:00',
    '10:30',
    '11:00',
    '11:30',
    '12:00',
    '12:30',
    '13:00',
    '13:30',
    '14:00',
    '14:30',
    '15:00',
    '15:30',
    '16:00',
    '16:30',
    '17:00',
    '17:30',
    '18:00',
    '18:30',
    '19:00',
    '19:30',
    '20:00',
    '20:30',
    '21:00',
    '21:30',
    '22:00',
    '22:30',
    '23:00',
    '23:30'
]


var startPoint = 0;
var endPoint = startPoint + (12 * 3600 * 1000);

 
$("#HidePartoInputs").hide();

$("#ShowPartoInputs").click(function () {
    $(".showInput").show();
    $("#ShowPartoInputs").hide();
    $("#HidePartoInputs").show();
});
$("#HidePartoInputs").click(function () {
    $(".showInput").hide();
    $("#ShowPartoInputs").show();
    $("#HidePartoInputs").hide();
});


function convertstringtodate(date, time) {
    const concatedDate = date + ' ' + time; 
    return moment(concatedDate, 'YYYY-MM-DD HH:mm').toDate();
}


function initiateAllCharts(date = '2000-01-01', time = '00:00') {
    startPoint = moment.utc(convertstringtodate(date, time)).valueOf();
    endPoint = startPoint + (12 * 3600 * 1000);
    let HalfHoursXAxisTds = '';
    let startIndex = HalfHoursXAxis.findIndex(i => i == time);
    for (let i = 0; i < (HalfHoursXAxis.length / 2); i++) {
        let data = HalfHoursXAxis[startIndex];
        HalfHoursXAxisTds += `<td class="rotate" style="border-style: none;">${data}</td>`;
        startIndex++;
        if (startIndex > 47) {
            startIndex = 0;
        }
    }
    let hoursXAxisTds = '';
    startIndex = HalfHoursXAxis.findIndex(i => i == time);
    for (let i = 0; i < (HalfHoursXAxis.length / 2); i +=2) {
        let data = HalfHoursXAxis[startIndex];
        hoursXAxisTds += `<td class="rotate" style="text-align:center; border-style: none;">${data}</td>`;
        startIndex += 2;
        if (startIndex > 47) {
            if (startIndex % 2 === 0) {
                startIndex = 0;
            } else {
                startIndex = 1;
            }
            
        }
    }

    $('#LMTimeRow').html(HalfHoursXAxisTds);
    $('#ContractionTimeCRow').html(HalfHoursXAxisTds);
    $('#OxytocinTimeRow').html(HalfHoursXAxisTds);
    $('#UrineTableRow').html(hoursXAxisTds);
    $('#DrugsTimeRow').html(hoursXAxisTds);
    $('#TempTimeRow').html(hoursXAxisTds);
    this.initiateHourXAxis();
    InitDrugs();
    InitUrine();
}

function initiateHourXAxis() {
    let startIndex = HalfHoursXAxis.findIndex(i => i == initiateTime);
    for (let i = 0; i < (HalfHoursXAxis.length / 2); i += 2) {
        hourXAxis.push(HalfHoursXAxis[startIndex]);
        startIndex += 2;
        if (startIndex > 47) {
            if (startIndex % 2 === 0) {
                startIndex = 0;
            } else {
                startIndex = 1;
            }

        }
        
    }
}

function createAllChart() {
    createFetalHeartRateChart();
    createCervixChart();
    createBPChart();

    plotLiquor();
    plotMolding();
    plotDrugs();
    plotTemperatures();

    plotProtein();
    plotAcetone();
    plotVolume();


    plotOxytocin();
    plotDrops();

    plotContractions();
}

function findIndexOfExistngGlobal(data, xValue) {
    let index = -1;
    for (var i = 0; i < data.length; i++) {
        if (xValue === data[i][0]) {
            index = i;
            break;
        }
    }
    return index;
}

function createBloodPressureData() {
    BP = [];

    for (let v of bpPost) {
        BP.push({ name: "Systolic", x: v[0], y: v[1] });
        BP.push({ name: "Diastolic", x: v[0], y: v[2] });
        BP.push({ name: "Diastolic", x: v[0], y: null });
    }
    console.log(BP);
    console.log(bpPost);
}