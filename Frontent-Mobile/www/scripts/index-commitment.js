getDDLDepartmentWIG();

function getDDLDepartmentWIG(){
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: 'https://localhost:32768/api/commitment/getDepartmentWig',
		data: '',
		dataType: "json",
		success: function (data,status){
			data.forEach(function(item){
				$('#ddlDepartmentWIG').append($('<option></option>').attr('value',item.WigID).text("W" + item.WigID + ":" + item.WigName));
			});
		},
		error: function (data,status){
			console.log(data);
		}
	});
}

function getDDLDepartmentLM(wigID){
	//check null or empty
	if(!wigID){
		return;
	}

	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: 'https://localhost:32768/api/commitment/getDepartmentWig',
		data: '',
		dataType: "json",
		success: function (data,status){
			$('#ddlDepartmentLM').empty();
			for(var i in data){
				if(data[i].WigID==wigID){
					data[i].LmList.forEach(function(item,index){
						var id = index+1; 
						$('#ddlDepartmentLM').append($('<option></option>').attr('value',item.LmID).text("LM" + id + ":" + item.LmName));
					});				
				}
			}
		},
		error: function (data,status){
			console.log(data);
		}
	});
}

function addNewCommitment(commitmentLm,commimentName){
	var data = {};
	data.DepartmentLmId = commitmentLm;
	data.CommitmentName = commimentName;

	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: 'https://localhost:32768/api/commitment/addCommitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
			debugger
			if(!data){
				$('.offline-message').text("Duplicate Data")
				$('.offline-message').addClass('offline-message-active');
				setTimeout(function(){
					$('.offline-message').removeClass('offline-message-active');
				},3000);
				return
			}

			$('.online-message').text("Success")
			$('.online-message').addClass('online-message-active');
			setTimeout(function(){
				$('.online-message').removeClass('online-message-active');
			},3000);
			getCommitment(1);
			clearData();
		},
		error: function (data,status){
			$("#lblError").css('display','block')
			console.log(data);
		}
	});
}

function clearData(){
	$('#txtCommitment').val("");
	$('#ddlDepartmentWIG').val("");
	$('#ddlDepartmentLM').val("");
}

function addCommitment(){
	var txtStartDate = $('#txtStartDate').val();
	var txtCommitment = $('#txtCommitment').val();
	var ddlDepartmentWig = $('#ddlDepartmentWIG').val();
	var ddlDepartmentLm = $('#ddlDepartmentLM').val();
	var message = "Warning!"
	if(!ddlDepartmentWig){
		message = "Please select WIG";
	}else if(!ddlDepartmentLm){
		message = "Please select LM";
	}else if(txtCommitment===""){
		message = "Please input commitment";
	}else if(txtStartDate===""){
		message = "Please input startdate";
	}else{
		addNewCommitment(ddlDepartmentLm,txtCommitment);
	}
	
	if(!ddlDepartmentWig || !ddlDepartmentLm || txtCommitment==="" || txtStartDate===""){
		$('.offline-message').text(message)
		$('.offline-message').addClass('offline-message-active');
		setTimeout(function(){
			$('.offline-message').removeClass('offline-message-active');
		},3000);
	}
}

function getCommitment(createBy){
	data = {};
	data.CreatedBy = createBy;
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: 'https://localhost:32768/api/commitment/GetCommitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
			$("#tbCommitment").empty();
			var html = "<tr>"
				html += "<th>No.</th>"
				html += "<th>Commitment</th>"
				html += "<th>Start Date</th>"
				html += "<th>Update Date</th>"
				html += "<th>Status</th>"
				html += "<th>Update</th>"
				html += "</tr>";
			for(var i in data){
				console.log(data[i]);
				html += "<tr>"
				html += "<td>" + data[i].CommitmentId + "</td>"
				html += "<td style='text-align:left'>" + data[i].CommitmentName + "</td>"
				html += "<td>" + data[i].CommitmentStartDate + "</td>"
				html += "<td>" + data[i].UpdatedDate + "</td>"
				html += "<td>" + data[i].CommitmentStatus + "</td>"
				html += '<td><input onclick="getConfirmMenu($(this));" value="Update" type="button" class="button button-m shadow-small button-round-small bg-blue2-dark"/></td>'
				html += "</tr>"
			}
			$("#tbCommitment").append(html);
		},
		error: function (data,status){
			$("#lblError").css('display','block')
			console.log(data);
			
		}
	});
}

function statusOnChange(){
	var status = $( "#ddlStatus option:selected" ).val();
	$('#txtRemark').val('');
	if(status==="postpone"){
		$('.remark').show();
		$('.postpone-date').show();
		$('#menu-confirm').css("height","420px");
	}else if(status==="fail"){
		$('.remark').show();
		$('.postpone-date').hide();	
		$('#menu-confirm').css("height","350px");
	}else if(status==="delete"){
		$('.remark').show();
		$('.postpone-date').hide();	
		$('#menu-confirm').css("height","350px");
	}else if(status==="done"){	
		$('.remark').hide();
		$('.postpone-date').hide();	
		$('#menu-confirm').css("height","250px");
	}else{		
		$('.remark').hide();
		$('.postpone-date').hide();		
		$('#menu-confirm').css("height","250px");
	}
}

function getConfirmMenu(ctrl){
	var index = ctrl.closest('tr').index()
	var element = $('#tbCommitment tr:eq(' + index + ')').find("td:eq(0)");
	var id = (element.length > 0) ? element[0].innerHTML : "";
	$("#menu-confirm").attr("data-id",id);
	$('#ddlStatus').val('');
	$('#txtRemark').val('');

	$('#menu-confirm').addClass("menu-active");
	$('.menu-hider').addClass("menu-active");
}

function UpdateStatus(){
	var id = $('#menu-confirm').attr("data-id");
	var status = $( "#ddlStatus option:selected" ).val(); 
	var remark = $('#txtRemark').val();

	if(status==""){
		message = "Please select status";
	}else if (remark=="")
	{
		message = "Please input remark";
	}
	if(status!=="done" && (status=="" || remark=="")){
		$('.offline-message').text(message)
		$('.offline-message').addClass('offline-message-active');
		setTimeout(function(){
			$('.offline-message').removeClass('offline-message-active');
		},3000);

		return
	}
	
	var data = {};
	data.CommitmentStatus = status;
	data.CommitmentId = id;
	data.CommitmentRemark = remark;
	data.UpdatedBy = 15;

	if(status==="postpone"){
		data.CommitmentStartDate = $('#txtPostponeDate').val();
	}
	console.log(data);

	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: 'https://localhost:32768/api/commitment/updateCommitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){			
			console.log(data);
			$('#menu-confirm').removeClass('menu-active');
			$('.menu-hider').removeClass('menu-active');
			$('.online-message').addClass('online-message-active');

			getCommitment(1);
			clearData();

			setTimeout(function(){
				$('.online-message').removeClass('online-message-active');
			},3000);
		},
		error: function (data,status){
			$("#lblError").css('display','block')
			console.log(data);
		}
	});
}

(function($) {
    Highcharts.chart('container', {
        credits: {
            enabled: false
        },
        chart: {
            type: 'pie',
            backgroundColor: 'transparent'
        },
        title: {
            text: 'Commitment'
        },
        subtitle: {
            text: 'Overall'
        },

        accessibility: {
            announceNewData: {
                enabled: true
            },
            point: {
                valueSuffix: '%'
            }
        },

        plotOptions: {
            series: {
                dataLabels: {
                    enabled: true,
                    format: '{point.name}: {point.y:.1f}%'
                }
            }
        },

        tooltip: {
            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
        },

        series: [{
            name: "Browsers",
            colorByPoint: true,
            data: [{
                    name: "Chrome",
                    y: 62.74,
                    drilldown: "Chrome"
                },
                {
                    name: "Firefox",
                    y: 10.57,
                    drilldown: "Firefox"
                },
                {
                    name: "Internet Explorer",
                    y: 7.23,
                    drilldown: "Internet Explorer"
                },
                {
                    name: "Safari",
                    y: 5.58,
                    drilldown: "Safari"
                },
                {
                    name: "Edge",
                    y: 4.02,
                    drilldown: "Edge"
                },
                {
                    name: "Opera",
                    y: 1.92,
                    drilldown: "Opera"
                },
                {
                    name: "Other",
                    y: 7.62,
                    drilldown: null
                }
            ]
        }],
        drilldown: {
            series: [{
                    name: "Chrome",
                    id: "Chrome",
                    data: [
                        ["v65.0",0.1],
                        ["v64.0",1.3],
                        ["v63.0",53.02],
                        ["v62.0",1.4],
                        ["v61.0",0.88],
                        ["v60.0",0.56],
                        ["v59.0",0.45],
                        ["v58.0",0.49],
                        ["v57.0",0.32],
                        ["v56.0",0.29],
                        ["v55.0",0.79],
                        ["v54.0",0.18],
                        ["v51.0",0.13],
                        ["v49.0",2.16],
                        ["v48.0",0.13],
                        ["v47.0",0.11],
                        ["v43.0",0.17],
                        ["v29.0",0.26]
                    ]
                },
                {
                    name: "Firefox",
                    id: "Firefox",
                    data: [
                        ["v58.0",1.02],
                        ["v57.0",7.36],
                        ["v56.0",0.35],
                        ["v55.0",0.11],
                        ["v54.0",0.1],
                        ["v52.0",0.95],
                        ["v51.0",0.15],
                        ["v50.0",0.1],
                        ["v48.0",0.31],
                        ["v47.0",0.12]
                    ]
                },
                {
                    name: "Internet Explorer",
                    id: "Internet Explorer",
                    data: [
                        ["v11.0",6.2],
                        ["v10.0",0.29],
                        ["v9.0",0.27],
                        ["v8.0",0.47]
                    ]
                },
                {
                    name: "Safari",
                    id: "Safari",
                    data: [
                        ["v11.0",3.39],
                        ["v10.1",0.96],
                        ["v10.0",0.36],
                        ["v9.1",0.54],
                        ["v9.0",0.13],
                        ["v5.1",0.2]
                    ]
                },
                {
                    name: "Edge",
                    id: "Edge",
                    data: [
                        ["v16",2.6],
                        ["v15",0.92],
                        ["v14",0.4],
                        ["v13",0.1]
                    ]
                },
                {
                    name: "Opera",
                    id: "Opera",
                    data: [
                        ["v50.0",0.96],
                        ["v49.0",0.82],
                        ["v12.1",0.14]
                    ]
                }
            ]
        }
    });

	//Create table 
	getCommitment(1);
}(jQuery));