var BPDropdown = $("#BPTime");
$.each(HalfHoursXAxis, function () {
    BPDropdown.append($("<option />").val(this).text(this));
});
var PulseTime = $("#PulseTime");
$.each(HalfHoursXAxis, function () {
    PulseTime.append($("<option />").val(this).text(this));
});



var positionIntervalsHalfHour = HalfHoursXAxis.map(x => moment.utc(getDate(x)).valueOf());

var input1 = "09:30";
var input2 = "11:00";

var BP = [];
var Pulse = [];
var bpPost = [];


createBPChart();

function createBPChart() {
    var chart = Highcharts.chart('PulseBP', {

        chart: {
            type: 'scatter',
            //zoomType: 'xy',
            scrollablePlotArea: {
                minWidth: 880,
                scrollPositionX: 0
            }
        },


        title: {
            text: '',
            x: -20 //center
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
            title: {
                text: 'Pulse and BP'
            },
            min: 60,
            max: 180,
            gridLineColor: '#000',
            tickInterval: 10,

            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
        tooltip: {
            formatter: function () {

                if (this.series.name === 'Pulse') {
                    return '<b>' + this.series.name + '</b><br/>' +
                        this.y + ', ' + Highcharts.dateFormat('%H:%M', this.x);
                }
                return '<b>' + this.point.name === undefined ? this.series.name : this.point.name + '</b><br/>' +
                    this.y + ', ' + Highcharts.dateFormat('%H:%M', this.x);
            }
        },
        legend: { enabled: false },
        plotOptions: {
            series: {
                color: 'black'
            },
            scatter: {
                lineWidth: 1
            }
        },
        series: [
            {

                name: 'BP',
                data: BP,
                color: 'blue',
                marker: {
                    symbol: 'url(../../images/dash.png)',
                    lineColor: 'blue',
                    lineWidth: 2
                }
            }, {
                name: 'Pulse',
                data: Pulse,
                color: 'black'
            }
        ]

    });
}



function UpdatePulseBP(partograpId) {
    if ($('#Systolic').val() == '' || $('#Diastolic').val() == '') {
        ShowToast('Value Cannot be null');
        return;
    }
    var Systolic = parseInt($("#Systolic").val());
    var Diastolic = parseInt($("#Diastolic").val());

    let date = convertstringtodate(initiateDate, $('#BPTime').val());
    var Time = moment.utc(date).valueOf();
    if (Time < startPoint) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }

    

    const bpPostData = [Time, Systolic, Diastolic];

    const existingIndex = findIndexOfExistngBPPost(Time);
    if (existingIndex > -1) {
        bpPost[existingIndex] = bpPostData;
    } else {
        bpPost.push(bpPostData);
    }
    bpPost.sort((a, b) => a[0] - b[0]);

    createBloodPressureData();
    createBPChart();
    PostBP(partograpId);

}



function UpdatePulse(partograpId) {
    if ($('#Pulse').val() == '') {
        ShowToast('Value Cannot be null');
        return;
    }


    let date = convertstringtodate(initiateDate, $('#PulseTime').val());
    const xValue = moment.utc(date).valueOf();
    if (xValue < startPoint) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }
    var PulseValue = $('#Pulse').val();
    if (PulseValue < 60) {
        ShowToast('Pulse Should not be below 60.');
        return
    }
    Pulse.push([xValue, parseInt(PulseValue)]);
    createBPChart();
    this.PostPulse();
}

function PostPulse(partographId) {

    const postData = {
        partographID: partographId,
        data: Pulse
    };

    baseApi.postRequest(
        "Pulse/AddPulse",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );

}

function PostBP(partographId) {

    const postData = {
        partographID: partographId,
        data: bpPost
    };

    baseApi.postRequest(
        "BloodPressure/AddBloodPressure",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );

}


function findIndexOfExistngBPPost(time) {
    let index = -1;
    for (var i = 0; i < bpPost.length; i++) {
        if (time === bpPost[i][0]) {
            index = i;
            break;
        }
    }
    return index;
}
