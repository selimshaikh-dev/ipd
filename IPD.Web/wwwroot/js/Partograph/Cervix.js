// Populate Dropdowns

Highcharts.SVGRenderer.prototype.symbols.cross = function (x, y, w, h) {
    return ['M', x, y, 'L', x + w, y + h, 'M', x + w, y, 'L', x, y + h, 'z'];
};
if (Highcharts.VMLRenderer) {
    Highcharts.VMLRenderer.prototype.symbols.cross = Highcharts.SVGRenderer.prototype.symbols.cross;
}


// A point click event that uses the Renderer to draw a label next to the point
//On subsequent clicks, move the existing label instead of creating a new one.
Highcharts.addEvent(Highcharts.Point, 'click', function () {
    if (this.series.options.className.indexOf('popup-on-click') !== -1) {
        const chart = this.series.chart;
        const date = Highcharts.dateFormat('%A, %b %e, %Y', this.x);
        const text = `<b>${date}</b><br/>${this.y} ${this.series.name}`;

        const anchorX = this.plotX + this.series.xAxis.pos;
        const anchorY = this.plotY + this.series.yAxis.pos;
        const align = anchorX < chart.chartWidth - 200 ? 'left' : 'right';
        const x = align === 'left' ? anchorX + 10 : anchorX - 10;
        const y = anchorY - 30;
        if (!chart.sticky) {
            chart.sticky = chart.renderer
                .label(text, x, y, 'callout', anchorX, anchorY)
                .attr({
                    align,
                    fill: 'rgba(0, 0, 0, 0.75)',
                    padding: 0,
                    zIndex: 0 // Above series, below tooltip
                })
                .css({
                    color: 'white'
                })
                .on('click', function () {
                    chart.sticky = chart.sticky.destroy();
                })
                .add();
        } else {
            chart.sticky
                .attr({ align, text })
                .animate({ anchorX, anchorY, x, y }, { duration: 250 });
        }
    }
});


// Hard Coded Cervix Chart Lines


const AlertLine = [[1, 4], [2, 5], [3, 6], [4, 7], [5, 8], [6, 9], [6, 10], [15, 11]];

const ActionLine = [[8, 4], [9, 5], [10, 6], [11, 7], [12, 8], [13, 9], [14, 10], [15, 11]];


// end


var $CervixDropDown = $("#CervixSelectOption");
$.each(YAxis, function () {
    $CervixDropDown.append($("<option />").val(this).text(this));
});

var $CervixTimeDropDown = $("#CervixTimeOption");
$.each(HalfHoursXAxis, function (v,i) {
    $CervixTimeDropDown.append($("<option />").val(i).text(i));
});


var DescentHeadDropDown = $("#DescentHeadTimeOption");
$.each(HalfHoursXAxis, function () {
    DescentHeadDropDown.append($("<option />").val(this).text(this));
});


var cervixAlertData = [[startPoint, 4], [(startPoint + 12 * halfHourInterval), 10]];
var actionAlertData = [[(startPoint + 8 * halfHourInterval), 4], [(startPoint + 20 * halfHourInterval), 10]];



function createCervixAlertData() {
    cervixAlertData = [];
    cervixAlertData.push([startPoint, 4]);
    cervixAlertData.push([(startPoint + 12 * halfHourInterval), 10]);
    actionAlertData = [];
    actionAlertData.push([(startPoint + 8 * halfHourInterval), 4]);
    actionAlertData.push([(startPoint + 20 * halfHourInterval), 10]);
}


var cervix = [];
var DescentHead = [];

var PreviousTime = 0;
function UpdateCervix(partographId) {
    let date = convertstringtodate(initiateDate, $("#CervixTimeOption").val());

    var input = [date, parseInt($("#CervixSelectOption").val()), partographId]

    this.PlotCervixValues(input);
}

function UpdateDescentHead(partographId) {
    let date = convertstringtodate(initiateDate, $('#DescentHeadTimeOption').val());

    var input = [date, parseInt($("#DescentHeadSelectOption").val()), partographId]

    this.PlotUpdateDescentHeadValues(input);

}



function createCervixChart() {

    createCervixAlertData();
    createChart();
}


function createChart() {

    var chart = Highcharts.chart('cervix', {


        chart: {
            type: 'line',
            height: 400,
            scrollablePlotArea: {
                minWidth: 850,
                scrollPositionX: 0
            }

        },
        legend: { enabled: false },
        title: {
            text: 'Active Phase',
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
            max: endPoint
        },

        yAxis: {
            title: {
                text: 'Cervix (cm) (Pivot X)     |            Descent of head (Plot 0)'
            },
            min: 0,
            max: 10,
            tickInterval: 1
        },

        tooltip: {
            formatter: function () {
                return '<b>' + this.series.name + '</b><br/>' +

                    this.y + ', ' + Highcharts.dateFormat('%H:%M', this.x);
            },
            valueSuffix: 'cm'
        },

        plotOptions: {
            series: {
                color: 'black'
            }
        },
        series: [
            {
                name: 'Alert',
                data: cervixAlertData,
                color: 'black',
                enableMouseTracking: false
            },
            {
                name: 'Action',
                data: actionAlertData,
                color: 'black',
                enableMouseTracking: false
            },
            {
                name: 'Cervix',
                data: cervix,
                color: 'black',
                marker: {
                    symbol: 'cross',
                    lineColor: 'black',
                    lineWidth: 2
                }
            },
            {
                name: 'Descent of Head',
                data: DescentHead,
                color: 'blue',
                marker: {
                    symbol: 'circle'
                }
            }
        ]
    });
}

function PlotCervixValues(e) {
    let NewTime = moment(e[0]);
    const xValue = moment.utc(NewTime).valueOf();
    if ((cervix.length === 0 && xValue > startPoint) || (xValue < startPoint)) {
        ShowToast('Invalid time selected.' + inittimeString);
        return
    }

    if (e[1] < 4) {
        ShowToast('Cervix must be started from 4.');
        return
    }

    const point = [xValue, e[1]];
    const existingIndex = findIndexOfExistngCervix(xValue, cervix);
    if (existingIndex > -1) {
        cervix[existingIndex][1] = e[1];
    } else {
        cervix.push(point);
    }
    cervix.sort((a, b) => a[0] - b[0]);

    createChart();
    this.postCervix(e[2]);
}



function PlotUpdateDescentHeadValues(e) {
    let NewTime = moment(e[0]);
    const xValue = moment.utc(NewTime).valueOf();
    if (xValue < startPoint) {
        ShowToast('Invalid time selected. ' + inittimeString);
        return
    }
    if (e[1] <= 5) {
        const point = [xValue, e[1]];
        const existingIndex = findIndexOfExistngCervix(xValue, DescentHead);
        if (existingIndex > -1) {
            DescentHead[existingIndex][1] = e[1];
        } else {
            DescentHead.push(point);
        }
        DescentHead.sort((a, b) => a[0] - b[0]);
        this.postDecentOfHead(e[2]);
    }
    else {
        ShowToast("Value should not be greater than 5");
    }
    createChart();
    this.postDecentOfHead(e[2]);
}

function findIndexOfExistngCervix(xValue, arrayValue = []) {
    let index = -1;
    for (var i = 0; i < arrayValue.length; i++) {
        if (xValue === arrayValue[i][0]) {
            index = i;
            break;
        }
    }
    return index;
}

function postCervix(partographId) {

    const postData = {
        partographID: partographId,
        data: cervix
    };

    baseApi.postRequest(
        "Cervix/AddCervix",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );

}

function postDecentOfHead(partographId) {

    const postData = {
        partographID: partographId,
        data: DescentHead
    };

    baseApi.postRequest(
        "DescentOfHeads/AddDescentOfHeads",
        postData,
        (res) => {
            console.log(res);
        },
        (err) => {
            console.log(err);
        }
    );

}