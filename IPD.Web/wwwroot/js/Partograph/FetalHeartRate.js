
var arr = [];

var input1 = "09:30";
var input2 = "10:00";
var input3 = "10:30";
var input4 = ["13:30",120];
var input5 = ["14:00",140];

function getDate(e) {
    var cdt = moment(e, 'HH:mm');
    return cdt.toDate();
}


function getCurentTimeSpan(ah = 0) {
    const cd = new Date();
    const h = cd.getHours() + ah;
    const m = cd.getMinutes();
    return (h>10?h:'0'+h) + ':' + (m>29?'30':'00');
}



var FetalDropdown = $("#FetalTimeOption");
$.each(HalfHoursXAxis, function () {
    FetalDropdown.append($("<option />").val(this).text(this));
});
var positionIntervalsHalfHour = HalfHoursXAxis.map(x => moment.utc(getDate(x)).valueOf());

var DynamicDataSeries = [];

function createNewFetalHeartRateChart() {
    DynamicDataSeries = [];
    createFetalHeartRateChart()
}

function createFetalHeartRateChart() {
   Highcharts.chart('FetalHeartRate', {

        events: {
            redraw: function () {
                const label = this.renderer.label('The chart was just redrawn', 100, 120)
                    .attr({
                        fill: Highcharts.getOptions().colors[0],
                        padding: 10,
                        r: 0,
                        zIndex: 0
                    })
                    .css({
                        color: '#FFFFFF'
                    })
                    .add();

                setTimeout(() => {
                    label.fadeOut();
                }, 1000);
            }
        },
        chart: {
            height : 300,
            scrollablePlotArea: {
                minWidth: 880,
                scrollPositionX: 0
            }
        },
        legend: { enabled: false },
        redraw: true,

        title: {
            text: '',
            x: -20 
        },

        xAxis: {
            type: 'datetime',
            dateTimeLabelFormats: {

                hour: '%H:%M',
            },
            gridLineColor: '#000',
            tickInterval: halfHourInterval,
            min: startPoint,
            max: endPoint,
        },
        yAxis: {
            gridLineColor:'#000',
            title: {
                text: 'Fetal Heart Rate'
            },
            min: 80,
            max: 200,
            tickInterval: 10,
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
        tooltip: {
            formatter: function () {
                return '<b>FHR</b><br/>' +
                    
                    this.y +', ' +Highcharts.dateFormat('%H:%M', this.x);
            }
        },
      
        plotOptions: {
            series: {
                color: 'black'
            }
        },
        series: [
            {
                name : "FHR",
                data: DynamicDataSeries
            }
        ]
   });
}
function UpdateFetalRate(partographId) {
    let date = convertstringtodate(initiateDate, $("#FetalTimeOption").val());
    var input = [date, parseInt($("#FetalOption").val()), partographId]

    this.PlotFetalRate(input);
}

function PlotFetalRate(e) {
    let NewTime = moment(e[0]);
    const xValue = moment.utc(NewTime).valueOf();
    if ((DynamicDataSeries.length === 0 && xValue > startPoint) || (xValue < startPoint)) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }
    const point = [xValue, e[1]];
    const existingIndex = findIndexOfExistng(xValue);
    if (existingIndex > -1) {
        DynamicDataSeries[existingIndex][1] = e[1]; 
    } else {
        DynamicDataSeries.push(point);
    }
    DynamicDataSeries.sort((a, b) => a[0] - b[0]);
    this.PostFHR(e[2]);
    createFetalHeartRateChart();

}

function findIndexOfExistng(xValue) {
    let index = -1;
    for (var i = 0; i < DynamicDataSeries.length; i++) {
        if (xValue === DynamicDataSeries[i][0]) {
            index = i;
            break;
        }
    }
    return index;
}


function PostFHR(partographId) {

    const postData = {
        partographID: partographId,
        data: DynamicDataSeries
    };

    baseApi.postRequest(
        "FetalHeartRates/AddFetalHeartRates",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );

}