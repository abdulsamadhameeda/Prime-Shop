var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
var chart = echarts.init(document.getElementById('ordersChart'));


var chart;
var pieChart;

// ضفنا هذه الدالة لتستقبل البيانات وتبدأ الرسم فوراً:
function initDashboardCharts(initialBarData, initialPieData) {
    // تهيئة شارت الأعمدة
    var barDom = document.getElementById('ordersChart');
    if (barDom) {
        chart = echarts.init(barDom);
        renderChart(initialBarData); // استدعاء دالة الرسم القديمة بالبيانات الممررة
    }

    // تهيئة شارت الدائرة
    var pieDom = document.getElementById('orderStatusChart');
    if (pieDom) {
        pieChart = echarts.init(pieDom);
        renderPieChart(initialPieData); // استدعاء دالة رسم الدائرة بالبيانات الممررة
    }
}



function renderChart(data) {
    var labels = data.map(x => months[x.month - 1]);
    var values = data.map(x => x.totalOrders);

    chart.setOption({
        tooltip: {},
        xAxis: {
            type: 'category',
            data: labels
        },
        yAxis: {
            type: 'value',
            minInterval: 1

        },
        series: [{
            type: 'bar',
            data: values,
            itemStyle: { color: '#FFA500' }
        }]
    });
}

function filterByYear(year) {
    $.ajax({
        url: '/Admin/GetOrdersPerMonth?year=' + year,
        type: 'GET',
        success: function (data) {
            renderChart(data);
        }
    });
}

// var initialData = @Json.Serialize(Model.OrdersPerMonth);
// renderChart(initialData);
var pieChart = echarts.init(document.getElementById('orderStatusChart'));

var statusNames = ['Pending', 'Processing', 'Shipped', 'Delivered', 'Cancelled'];
var statusColors = ['#eda100', '#2a78d6', '#4a3aa7', '#1baf7a', '#e34948'];
// var data = @Json.Serialize(Model.OrderStatus);

function renderPieChart(data) {
    if (!pieChart) return;
    pieChart.setOption({
        tooltip: {
            trigger: 'item'
        },
        series: [{
            type: 'pie',
            radius: ['40%', '70%'],
            data: data.map(x => ({
                name: statusNames[x.status - 1] || 'Unknown',
                value: x.total,
                itemStyle: { color: statusColors[x.status - 1] || '#ccc' }
            })),
            label: {
                formatter: '{b}: {c}'
            }
        }]
    });
}

window.addEventListener('resize', function () {
    chart.resize();
    pieChart.resize();

});

