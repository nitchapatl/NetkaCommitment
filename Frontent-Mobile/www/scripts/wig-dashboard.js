$(document).ready(function() {
    

var DepartmentId = 5; 
    
displayUserDetail();
getDepartmentWIGDashboardInfo(DepartmentId);
getDashboardDepartmentCommitmentInfo(DepartmentId);

function displayUserDetail() {
    var firstname = localStorage.getItem("userFirstNameEn"); //alert(firstname)
    var lastname = localStorage.getItem("userLastNameEn"); //alert(lastname)

    var department = localStorage.getItem("departmentName"); //alert(department)

    $("#display_user").text(firstname + " " + lastname);
    $("#display_team").text(department);
}

function getDepartmentWIGDashboardInfo(DepartmentId) {
    $.ajax({
		type:'GET',
		contentType: 'application/json',
		url: URL + '/api/dashboard/department/wig/' + DepartmentId,
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

            console.log(wig_series);
            console.log(lm_series);

            drawPiechart(wig_series, lm_series);

		},
		error: function (data,status){
			console.log(data);
		}
	});
}

function drawPiechart(wig_series, lm_series) {

    Highcharts.setOptions({
        colors: ['#7fdbda', '#ade498', '#ede682', '#febf63', '#df5e88', '#1dd3bd', '#436f8a', '#e5e5e5', '#fcf876', '#fe8a71']
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
                    getDashboardDepartmentCommitmentInfo(DepartmentId);
                }    
            }    
        },
        title: {
            text: 'WIG Dashboard'
        },
        subtitle: {
            text: 'Department :' + DepartmentId + ' Employee: ' + 'mf'
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
                    format: '{point.name}: {point.y}',
                    padding: 0
                },
                events: {
                        click: function(e) {
                            
                            
                            var WigID = e.point.wigid; //console.log(WigID)
                            var LmID = e.point.lmid; //console.log(LmID)

                            var XAxis_value = e.point.x; //console.log(XAxis_value)
                            //console.log(e.point.series.userOptions.data[XAxis_value])

                            var wiglm_data = e.point.series.userOptions.data[XAxis_value];

                            // Get department commitment by WigID or LmID //
                            if (WigID != "" && LmID == "") 
                            {
                                console.log("display commitment by wig")
                                console.log("WIG ID: " + WigID)

                                getDashboardDepartmentWigCommitmentInfo(WigID);
                            }
                            else if (WigID != "" && LmID != "") {
                                console.log("display commitment by lm")
                                var WigID_value = wiglm_data[2];
                                var LmID_value = wiglm_data[3];
                                console.log("WIG ID: " + WigID_value);
                                console.log("LM ID: " + LmID_value);
                                
                                getDashboardDepartmentLmCommitmentInfo(LmID_value);
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
            name: "Department WIG",
            colorByPoint: true,
            data: wig_series
        }],
        drilldown: {
            series: lm_series
        }
    });
}

function getDashboardDepartmentCommitmentInfo(DepartmentId) {

    data = {};
    data.DepartmentId = DepartmentId;
    
    //clearWigDashboardTable();

    $.ajax({
        type:'POST',
        contentType: 'application/json; charset=utf-8',
        url: URL + '/api/dashboard/department/commitment',
        data: JSON.stringify(data),
        dataType: "json",
        success: function (data,status){
            
            $("#wig-dashboard-table").empty();

            console.log(data);

            $("#wig-dashboard-table").append(
                "<thead><tr><td>Commitment NO</td>" 
              + "<td>Name</td>"
              + "<td>Remark</td>"
              + "<td>Start Date</td>"
              + "<td>Finish Date</td>"
              + "<td>Status</td>"
              + "</tr></thead><tbody>");

            $.each(data, function(i, item){
                
                var startdate = new Date(item.CommitmentStartDate);
                var finishdate = new Date(item.CommitmentFinishDate);

                $("#wig-dashboard-table").append(
                        "<tr><td>" + item.CommitmentNo + "</td>" 
                      + "<td class='color-green1-dark'>" + item.CommitmentName + "</td>"
                      + "<td>" + item.CommitmentRemark + "</td>"
                      + "<td class='color-yellow1-dark'>" + moment(startdate).format("YYYY-MM-DD HH:mm:ss") + "</td>"
                      + "<td class='color-yellow1-dark'>" + moment(finishdate).format("YYYY-MM-DD HH:mm:ss") + "</td>"
                      + "<td class='color-blue1-dark'>" + item.CommitmentStatus + "</td>"
                      + "</tr>");
                                
            });
            $("#wig-dashboard-table").append("</tbody>");

            getDatatable('wig-dashboard-table');

        },
        error: function (data,status){
            console.log(data);
        }
    });
    
}

function getDashboardDepartmentWigCommitmentInfo(WigID) {

    data = {};
    data.DepartmentWigId = WigID;
    
    //clearWigDashboardTable();

    $.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: URL + '/api/dashboard/department/wig/commitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){

            $("#wig-dashboard-table").empty();
            
            console.log(data);

            $("#wig-dashboard-table").append(
                "<thead><tr><td>Commitment NO</td>" 
              + "<td>Name</td>"
              + "<td>Remark</td>"
              + "<td>Start Date</td>"
              + "<td>Finish Date</td>"
              + "<td>Status</td>"
              + "</tr></thead><tbody>");

            $.each(data, function(i, item){
                
                var startdate = new Date(item.CommitmentStartDate);
                var finishdate = new Date(item.CommitmentFinishDate);

                $("#wig-dashboard-table").append(
                        "<tr><td>" + item.CommitmentNo + "</td>" 
                      + "<td class='color-green1-dark'>" + item.CommitmentName + "</td>"
                      + "<td>" + item.CommitmentRemark + "</td>"
                      + "<td class='color-yellow1-dark'>" + moment(startdate).format("YYYY-MM-DD HH:mm:ss") + "</td>"
                      + "<td class='color-yellow1-dark'>" + moment(finishdate).format("YYYY-MM-DD HH:mm:ss") + "</td>"
                      + "<td class='color-blue1-dark'>" + item.CommitmentStatus + "</td>"
                      + "</tr>");
                                
            });

            $("#wig-dashboard-table").append("</tbody>");

            getDatatable('wig-dashboard-table');

		},
		error: function (data,status){
			console.log(data);
		}
	});
}

function getDashboardDepartmentLmCommitmentInfo(LmID) {

    data = {};
    data.DepartmentLmId = LmID;

    //clearWigDashboardTable();

    $.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: URL + '/api/dashboard/department/lm/commitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
            
            $("#wig-dashboard-table").empty();

            console.log(data);

            $("#wig-dashboard-table").append(
                "<thead><tr><td>Commitment NO</td>" 
              + "<td>Name</td>"
              + "<td>Remark</td>"
              + "<td>Start Date</td>"
              + "<td>Finish Date</td>"
              + "<td>Status</td>"
              + "</tr></thead><tbody>");

            $.each(data, function(i, item){
                
                var startdate = new Date(item.CommitmentStartDate);
                var finishdate = new Date(item.CommitmentFinishDate);

                $("#wig-dashboard-table").append(
                        "<tr><td>" + item.CommitmentNo + "</td>" 
                      + "<td class='color-green1-dark'>" + item.CommitmentName + "</td>"
                      + "<td>" + item.CommitmentRemark + "</td>"
                      + "<td class='color-yellow1-dark'>" + moment(startdate).format("YYYY-MM-DD HH:mm:ss") + "</td>"
                      + "<td class='color-yellow1-dark'>" + moment(finishdate).format("YYYY-MM-DD HH:mm:ss") + "</td>"
                      + "<td class='color-blue1-dark'>" + item.CommitmentStatus + "</td>"
                      + "</tr>");
                                
            });

            $("#wig-dashboard-table").append("</tbody>");

            getDatatable('wig-dashboard-table');

		},
		error: function (data,status){
			console.log(data);
		}
	});
}

function getDatatable(id){
    
    var table = $('#' + id).DataTable( {
        destroy: true,
        responsive: true,
		lengthMenu: [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]]
    } );

}

function clearDatatable(id) {

    $('#' + id).empty();
  
}

function clearWigDashboardTable() {
   
    $('#wig-dashboard-table thead').html("");
    $('#wig-dashboard-table tbody').html("");
}

} );