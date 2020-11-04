

window.chartColors = {
    red: '#658fc6',/*
  orange: 'rgb(255, 159, 64)',
  yellow: 'rgb(255, 205, 86)',
  green: 'rgb(75, 192, 192)',
  blue: 'rgb(54, 162, 235)',
  purple: 'rgb(153, 102, 255)',
  grey: 'rgb(231,233,237)'*/
};


var now = moment();
var dataTime1 = moment("2018-10-11T14:58:54.026Z");
var label1 = moment.duration(dataTime1.diff(now)).humanize(true);

var color = Chart.helpers.color;
var config = {
    type: 'radar',
    data: {
        labels: [
            "Health", "Managing at Home", "Finances", "Work", "Relationships", "Exercise", "      Volunteer", "Attitude"],
        datasets: [{
            backgroundColor: color(window.chartColors.red).alpha(0).rgbString(),
            borderColor: window.chartColors.red,
            pointBackgroundColor: window.chartColors.red,
            data: [8, 1, 5, 2, 3, 4, 7, 2],
            notes: ["Lorem Ipesum", "Lorem Ipesum", "Lorem Ipesum", "Lorem Ipesum", "Lorem Ipesum", "Lorem Ipesum", "Lorem Ipesum", "Lorem Ipesum"]
        },]
    },

    options: {
        legend: {
            display: false,
        },
        title: {
            display: false,
        },
        responsive: {
            display: true,
        },
        pane: {
            size: '100%'
        },
        scale: {
            ticks: {
                beginAtZero: true
            },
            pointLabels: {
                fontSize: 16,
                fontColor: '#094f82',

                FontFamily: "tahoma",
            }
        },
        tooltips: {
            enabled: false,
            callbacks: {
                label: function (tooltipItem, data) {
                    var datasetLabel = data.datasets[tooltipItem.datasetIndex].label || '';
                    //This will be the tooltip.body
                    return datasetLabel + ': ' + tooltipItem.yLabel + ': ' + data.datasets[tooltipItem.datasetIndex].notes[tooltipItem.index];
                }
            },
            custom: function (tooltip) {
                // Tooltip Element
                var tooltipEl = document.getElementById('chartjs-tooltip');
                if (!tooltipEl) {
                    tooltipEl = document.createElement('div');
                    tooltipEl.id = 'chartjs-tooltip';
                    tooltipEl.innerHTML = "<table></table>"
                    document.body.appendChild(tooltipEl);
                }
                // Hide if no tooltip
                if (tooltip.opacity === 0) {
                    tooltipEl.style.opacity = 0;
                    return;
                }
                // Set caret Position
                tooltipEl.classList.remove('above', 'below', 'no-transform');
                if (tooltip.yAlign) {
                    tooltipEl.classList.add(tooltip.yAlign);
                } else {
                    tooltipEl.classList.add('no-transform');
                }
                function getBody(bodyItem) {
                    return bodyItem.lines;
                }
                // Set Text
                if (tooltip.body) {
                    var titleLines = tooltip.title || [];
                    var bodyLines = tooltip.body.map(getBody);
                    var innerHtml = '<thead>';
                    titleLines.forEach(function (title) {
                        innerHtml += '<tr><th>' + title + '</th></tr>';
                    });
                    innerHtml += '</thead><tbody>';
                    bodyLines.forEach(function (body, i) {
                        var colors = tooltip.labelColors[i];
                        var style = 'background:' + colors.backgroundColor;
                        style += '; border-color:' + colors.borderColor;
                        style += '; border-width: 2px';
                        var span = '<span class="chartjs-tooltip-key" style="' + style + '"></span>';
                        innerHtml += '<tr><td>' + span + body + '</td></tr>';
                    });
                    innerHtml += '</tbody>';
                    var tableRoot = tooltipEl.querySelector('table');
                    tableRoot.innerHTML = innerHtml;
                }
                var position = this._chart.canvas.getBoundingClientRect();
                // Display, position, and set styles for font
                tooltipEl.style.opacity = 1;
                tooltipEl.style.left = position.left + tooltip.caretX + 'px';
                tooltipEl.style.top = position.top + tooltip.caretY + 'px';
                tooltipEl.style.fontFamily = tooltip._fontFamily;
                tooltipEl.style.fontSize = tooltip.fontSize;
                tooltipEl.style.fontStyle = tooltip._fontStyle;
                tooltipEl.style.padding = tooltip.yPadding + 'px ' + tooltip.xPadding + 'px';
            }
        }
    }
};
window.onload = function () {
    window.myRadar = new Chart(document.getElementById("canvas"), config);
};
var colorNames = Object.keys(window.chartColors);

  window.onload = pageLoad();

 function pageLoad() {
    debugger;
    var ctx = document.getElementById("myChart").getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'pie',
        data: {
            //labels: ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"],
            labels: ["בנים", "בנות"],
            datasets: [{
                data: [15, 12],
                backgroundColor: [
                    //'rgba(9,79,130)',
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        //options: {
        //    scales: {
        //        yAxes: [{
        //            ticks: {
        //                beginAtZero: true,
        //            }
        //        }]
        //    }
        //}
    });
}
