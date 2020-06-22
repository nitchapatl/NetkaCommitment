getWIGDashboardInfo();
getDashboardCommitmentInfo();

function getWIGDashboardInfo() {
	$.ajax({
		type:'GET',
		contentType: 'application/json',
		url: 'https://localhost:32768/api/dashboard/company/wig',
		data: '',
		dataType: "json",
		success: function (data,status){
            
            //console.log(data);
            var wig_series = [];
            var lm_series = [];
            
            $.each(data, function(i,item){
                
                var wig_info = { name: "WIG" + item.WigID + " " + item.WigName,
                                 y: item.WigValue,
                                 drilldown: item.WigID};
                
                //console.log(wig_info);
                wig_series.push(wig_info);
                
                
                var lm_data = [];
                $.each(item.LmList, function(j,item_lm){
                    
                    lm_info = ["LM" + item_lm.LmID + " " + item_lm.LmName, item_lm.LmValue];
                    //console.log(lm_info);
                    lm_data.push(lm_info);
                    
                });

                var lm_list = { name: "WIG" + item.WigID,
                                id: item.WigID,
                                data: lm_data}
                
                lm_series.push(lm_list);
            });

            //console.log(wig_series);
            //console.log(lm_series);

            drawPiechart(wig_series, lm_series);

		},
		error: function (data,status){
			console.log(data);
		}
	});
}

function drawPiechart(wig_series, lm_series) {
    
    Highcharts.chart('container', {
        credits: {
            enabled: false
        },
        chart: {
            type: 'pie',
            backgroundColor: 'transparent'
        },
        title: {
            text: 'WIG Dashboard'
        },
        subtitle: {
            text: 'Overall'
        },

        accessibility: {
            announceNewData: {
                enabled: true
            },
            point: {
                valueSuffix: ''
            }
        },

        plotOptions: {
            series: {
                dataLabels: {
                    enabled: true,
                    format: '{point.name}: {point.y}'
                }
            }
        },

        tooltip: {
            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
            pointFormat: '<span style="color:{point.color}"> {point.name}</span>: <b>{point.y}</b> <br/>'
        },

        series: [{
            name: "WIG Company",
            colorByPoint: true,
            data: wig_series
        }],
        drilldown: {
            series: lm_series
        }
    });
}

function getDashboardCommitmentInfo() {

    $.ajax({
		type:'GET',
		contentType: 'application/json',
		url: 'https://localhost:32768/api/dashboard/commitment',
		data: '',
		dataType: "json",
		success: function (data,status){
            
            console.log(data);

            $.each(data, function(i, item){
                $("#wig-dashboard-table").append(
                        "<tr><td>" + item.CommitmentNo + "</td>" 
                      + "<td>" + item.CommitmentName + "</td>"
                      + "<td>" + item.CommitmentDescription + "</td>"
                      + "<td>" + item.CommitmentRemark + "</td>"
                      + "<td>" + item.CommitmentStartDate + "</td>"
                      + "<td>" + item.CommitmentFinishDate + "</td>"
                      + "<td>" + item.CommitmentStatus + "</td>"
                      + "</tr>");
                                
            });

		},
		error: function (data,status){
			console.log(data);
		}
	});
}

