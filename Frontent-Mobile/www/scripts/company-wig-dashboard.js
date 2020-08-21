
getWIGDashboardInfo();
getDashboardCommitmentInfo();

function getWIGDashboardInfo() {
	$.ajax({
		type:'GET',
		contentType: 'application/json',
		url: 'https://localhost:32772/api/dashboard/company/wig',
		data: '',
		dataType: "json",
		success: function (data,status){
            
            //console.log(data);
            var wig_series = [];
            var lm_series = [];
            
            $.each(data, function(i,item){
                
                var wig_info = { name: "WIG" + item.WigID + " " + item.WigName,
                                 y: item.WigValue,
                                 wigid: item.WigID,
                                 lmid: "",
                                 drilldown: item.WigID};
                
                //console.log(wig_info);
                wig_series.push(wig_info);
                
                var lm_data = [];
                $.each(item.LmList, function(j,item_lm){
                    
                    lm_info = ["LM" + item_lm.LmID + " " + item_lm.LmName, item_lm.LmValue, item.WigID, item_lm.LmID];
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

    Highcharts.setOptions({
        colors: ['#24CBE5', '#64E572', '#FF9655', '#CB2326', '#6AF9C4']
    });
    Highcharts.chart('container', {
        credits: {
            enabled: false
        },
        chart: {
            type: 'pie',
            backgroundColor: 'transparent',
            events: {    
                drilldown: function(e) {    
                       
                },    
                drillup: function(e) {    
                    getDashboardCommitmentInfo();
                }    
            }    
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
                },
                events: {
                        click: function(e) {
                            
                            //alert(e.point.name)
                            //alert(e.point.wigid)
                            //alert(e.point.lmid)
                            //console.log(e.point.x)
                            //console.log(e.point.series.userOptions.data)
                            //console.log(this.chart.series.options.data[e.point.x])

                            var WigID = e.point.wigid; //console.log(WigID)
                            var LmID = e.point.lmid; //console.log(LmID)

                            var XAxis_value = e.point.x; 
                            //console.log(XAxis_value)
                            //console.log(e.point.series.userOptions.data[XAxis_value])
                            var wiglm_data = e.point.series.userOptions.data[XAxis_value];

                            // Get commitment by WigID or LmID //
                            if (WigID != "" && LmID == "") 
                            {
                                console.log("display commitment by wig")
                                console.log("WIG ID: " + WigID)

                                getDashboardWigCommitmentInfo(WigID);
                            }
                            else if (WigID != "" && LmID != "") {
                                console.log("display commitment by lm")
                                var WigID_value = wiglm_data[2];
                                var LmID_value = wiglm_data[3];
                                console.log("WIG ID: " + WigID_value);
                                console.log("LM ID: " + LmID_value);
                                
                                getDashboardLmCommitmentInfo(LmID_value);
                            }

                        }
                }
                
            }
        },

        tooltip: {
            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
            pointFormat: '<span style="color:{point.color}"> {point.name}</span>: <b>{point.y}</b> <br/>'
        },

        series: [{
            name: "Company WIG",
            colorByPoint: true,
            data: wig_series
        }],
        drilldown: {
            series: lm_series
        }
    });
}

function getDashboardCommitmentInfo() {

    clearWigDashboardTable();
    
    $.ajax({
		type:'GET',
		contentType: 'application/json',
		url: 'https://localhost:32772/api/dashboard/company/commitment',
		data: '',
		dataType: "json",
		success: function (data,status){
            
            console.log(data);

            $.each(data, function(i, item){
                
                var startdate = new Date(item.CommitmentStartDate);
                var finishdate = new Date(item.CommitmentFinishDate);

                $("#wig-dashboard-table").find('tbody').append(
                        "<tr><td>" + item.CommitmentNo + "</td>" 
                      + "<td class='color-green1-dark'>" + item.CommitmentName + "</td>"
                      + "<td>" + item.CommitmentRemark + "</td>"
                      + "<td>" + item.DepartmentLmName + "</td>"
                      + "<td class='color-yellow1-dark'>" + moment(startdate).format("YYYY-MM-DD HH:mm:ss") + "</td>"
                      + "<td class='color-yellow1-dark'>" + moment(finishdate).format("YYYY-MM-DD HH:mm:ss") + "</td>"
                      + "<td class='color-blue1-dark'>" + item.CommitmentStatus + "</td>"
                      + "</tr>");
                                
            });

		},
		error: function (data,status){
			console.log(data);
		}
	});
}

function getDashboardWigCommitmentInfo(WigID) {

    clearWigDashboardTable();

    $.ajax({
		type:'GET',
		contentType: 'application/json',
		url: 'https://localhost:32772/api/dashboard/company/wig/commitment/' + WigID,
		data: '',
		dataType: "json",
		success: function (data,status){
            
            console.log(data);

            $.each(data, function(i, item){
                
                var startdate = new Date(item.CommitmentStartDate);
                var finishdate = new Date(item.CommitmentFinishDate);

                $("#wig-dashboard-table").find('tbody').append(
                        "<tr><td>" + item.CommitmentNo + "</td>" 
                      + "<td class='color-green1-dark'>" + item.CommitmentName + "</td>"
                      + "<td>" + item.CommitmentRemark + "</td>"
                      + "<td>" + item.DepartmentLmName + "</td>"
                      + "<td class='color-yellow1-dark'>" + moment(startdate).format("YYYY-MM-DD HH:mm:ss") + "</td>"
                      + "<td class='color-yellow1-dark'>" + moment(finishdate).format("YYYY-MM-DD HH:mm:ss") + "</td>"
                      + "<td class='color-blue1-dark'>" + item.CommitmentStatus + "</td>"
                      + "</tr>");
                                
            });

		},
		error: function (data,status){
			console.log(data);
		}
	});
}

function getDashboardLmCommitmentInfo(LmID) {

    clearWigDashboardTable();

    $.ajax({
		type:'GET',
		contentType: 'application/json',
		url: 'https://localhost:32772/api/dashboard/company/lm/commitment/' + LmID,
		data: '',
		dataType: "json",
		success: function (data,status){
            
            console.log(data);

            $.each(data, function(i, item){
                
                var startdate = new Date(item.CommitmentStartDate);
                var finishdate = new Date(item.CommitmentFinishDate);

                $("#wig-dashboard-table").find('tbody').append(
                        "<tr><td>" + item.CommitmentNo + "</td>" 
                      + "<td class='color-green1-dark'>" + item.CommitmentName + "</td>"
                      + "<td>" + item.CommitmentRemark + "</td>"
                      + "<td>" + item.DepartmentLmName + "</td>"
                      + "<td class='color-yellow1-dark'>" + moment(startdate).format("YYYY-MM-DD HH:mm:ss") + "</td>"
                      + "<td class='color-yellow1-dark'>" + moment(finishdate).format("YYYY-MM-DD HH:mm:ss") + "</td>"
                      + "<td class='color-blue1-dark'>" + item.CommitmentStatus + "</td>"
                      + "</tr>");
                                
            });

		},
		error: function (data,status){
			console.log(data);
		}
	});
}

function clearWigDashboardTable() {
    $('#wig-dashboard-table tbody').html("");
}

