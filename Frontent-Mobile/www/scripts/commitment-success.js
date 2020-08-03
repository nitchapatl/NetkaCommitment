getDDLDepartmentWIG();
getCommitmentSummary(); //Chart Commitment

var host = "https://localhost:32770";

function getDatatable(){
    var table = $('#tbCommitment').DataTable( {
		destroy: true,
        responsive: true,
		lengthMenu: [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]]
    } );
}
function getDDLDepartmentWIG(){
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: host + '/api/commitment/getDepartmentWig',
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
		url: host + '/api/commitment/getDepartmentWig',
		data: '',
		dataType: "json",
		success: function (data,status){
			$('#ddlDepartmentLM').empty();
			$('#ddlDepartmentLM').append($('<option value="default" disabled selected>Select LM</option>'));
			for(var i in data){
				if(data[i].WigID==wigID){
					data[i].LmList.forEach(function(item,index){
						var id = index+1; 
						$('#ddlDepartmentLM').append($('<option></option>').attr('value',item.LmID).text("LM" + id + ":" + item.LmName));
					});				
				}
			}
			$('#ddlDepartmentLM').prop('selectedIndex', 0);
		},
		error: function (data,status){
			console.log(data);
		}
	});
}

function addNewCommitment(commitmentLm,commimentName,commitmentStartDate){
	var data = {};
	data.DepartmentLmId = commitmentLm;
	data.CommitmentName = commimentName;
	data.CommitmentStartDate = commitmentStartDate;

	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: host + '/api/commitment/addCommitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
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
			getTableCommitment(1);
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

	$('#ddlDepartmentWIG').prop('selectedIndex', 0);
	$('.add-wig').find('i').remove();
	$('.add-wig').find('em').append('<i class="fa fa-angle-down"></i>');

	$('#ddlDepartmentLM').prop('selectedIndex', 0);
	$('.add-lm').find('i').remove();
	$('.add-lm').find('em').append('<i class="fa fa-angle-down"></i>');

	$('.add-commitment').find('i').remove();
	$('.add-commitment').find('em').append('<i class="fa fa-angle-down"></i>');
	//$('.add-commitment-commitment').find('textarea').css('line-height','25px');
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
		addNewCommitment(ddlDepartmentLm,txtCommitment,txtStartDate);
	}
	
	if(!ddlDepartmentWig || !ddlDepartmentLm || txtCommitment==="" || txtStartDate===""){
		$('.offline-message').text(message)
		$('.offline-message').addClass('offline-message-active');
		setTimeout(function(){
			$('.offline-message').removeClass('offline-message-active');
		},3000);
	}
}

function getTableCommitment(createBy){
	data = {};
	data.CreatedBy = createBy;
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: host + '/api/commitment/GetTCommitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
			$("#tbCommitment").empty();
			var html = "<thead>"
				html += "<tr>"
				html += "<th>No.</th>"
				html += "<th>Commitment</th>"
				html += "<th>Start Date</th>"
				html += "<th>Status</th>"
				html += "<th>Update</th>"
				html += "</tr>";
				html += "</thead>"
				html += "<tbody>"
			for(var i in data){
				html += "<tr>"
				html += "<td>" + data[i].CommitmentId + "</td>"
				html += "<td style='text-align:left'>" + data[i].CommitmentName + "</td>"
				html += "<td>" + data[i].CommitmentStartDate + "</td>"
				html += "<td>" + data[i].CommitmentStatus + "</td>"
				html += '<td>'
				html += '<input onclick="getConfirmMenu($(this));" value="Update" type="button" class="button button-m shadow-small button-round-small bg-blue2-dark"/>'
				html += '<input onclick="Delete();" value="Delete" type="button" style="margin-left:5px;" class="button button-m shadow-small button-round-small bg-blue2-dark"/>'
				html += '</td>'
				html += "</tr>"
			}
			html += "</tbody>"
			$("#tbCommitment").append(html);
			getDatatable();
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
	if(status==="Postpone"){
		$('.update-remark').show();
		$('.update-postpone').show();
		$('#menu-confirm').css("height","520px");
	}else if(status==="Fail"){
		$('.update-remark').show();
		$('.update-postpone').hide();	
		$('#menu-confirm').css("height","450px");
	}else if(status==="Delete"){
		$('.update-remark').hide();
		$('.update-postpone').hide();	
		$('#menu-confirm').css("height","450px");
	}else if(status==="Done"){	
		$('.update-remark').hide();
		$('.update-postpone').hide();	
		$('#menu-confirm').css("height","350px");
	}else{		
		$('.update-remark').hide();
		$('.update-postpone').hide();		
		$('#menu-confirm').css("height","350px");
	}
}

function Delete(){

}

function getConfirmMenu(ctrl){
	$('#tbCommitment tbody').on('click', 'tr', function () {
		var table = $('#tbCommitment').DataTable(); 
		var index = table.row(this).index(); 
		var rowData = table.row(index).data();
		
		var id = rowData[0];
		var commitment = rowData[1];
		var updateDate = rowData[2];
		var status = rowData[3];

		$('#ddlStatus').prop('selectedIndex',0);
		$('.update-status').find('i').remove();
		$('.update-status').find('em').append('<i class="fa fa-angle-down"></i>');
		statusOnChange();

		$('#ddlStatus').val(status);
		$('.update-status').find('span').addClass('input-style-1-active');
		$('.update-status').find('i').remove();
		$('.update-status').find('em').append('<i class="fa fa-check color-green1-dark"></i>');

		$("#menu-confirm").attr("data-id",id);
		$('#txtCommitmentName').val(commitment);
		//$('.update-commitment').find('textarea').css('line-height','25px'); 
		//$('.update-commitment').find('textarea').css('padding-top','10px!important;'); 

		$('#menu-confirm').addClass("menu-active");
		$('.menu-hider').addClass("menu-active");

		//commitment 
		$('.update-commitment').find('span').removeClass('input-style-1').addClass('input-style-1-active');
		$('.update-commitment').find('i').remove();
	});
}

function notification(message){
	$('.offline-message').text(message)
	$('.offline-message').addClass('offline-message-active');
	setTimeout(function(){
		$('.offline-message').removeClass('offline-message-active');
	},3000);
}
function UpdateStatus(){
	var id = $('#menu-confirm').attr("data-id");
	var status = $( "#ddlStatus option:selected" ).val(); 
	var commitment = $('#txtCommitmentName').val();
	var remark = $('#txtRemark').val();
	var postpone = $('#txtPostpone').val();

	if(status==""){
		message = "Please select status";
	}else if (commitment==""){
		message = "Please input commitment"
	}
	else if (remark==""){
		message = "Please input remark";
	}

	if((status==="Done" || status==="Delete") && commitment===""){
		notification(message);
		return
	}else if((status==="Fail") && (commitment==="" || remark==="")){
		notification(message);
		return
	}else if(status==="In-Progress" && (commitment==="")){
		notification(message);
		return
	}else if(status==="Postpone" && (postpone==="" || commitment==="" || remark==="")){
		notification(message);
		return
	}
	
	var data = {};
	data.CommitmentName = commitment;
	data.CommitmentStatus = status;
	data.CommitmentId = id;
	data.CommitmentRemark = remark;
	data.UpdatedBy = 15;

	if(status==="postpone"){
		data.CommitmentStartDate = $('#txtPostpone').val();
	}
	console.log(data);

	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: host + '/api/commitment/updateCommitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){			
			console.log(data);
			$('#menu-confirm').removeClass('menu-active');
			$('.menu-hider').removeClass('menu-active');
			$('.online-message').addClass('online-message-active');

			getTableCommitment(1);
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

function getCommitmentSummary(){
	data = {};
	data.CreatedBy = 1;
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: host + '/api/commitment/GetCommitmentSummary',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
			console.log(data);
			var allCommitment = 0;
			for (var j in data)
			{
				allCommitment += data[j].CommitmentCount;
			}

			var wig = {};
			wig.name = "Commitment",
			wig.colorByPoint = true,
			wig.data = [];

			var series = [];
			for (var i=0; i<data.length;i++ )
			{	
				var wigData = {};
				wigData.name = data[i].DepartmentWigName;
				wigData.y = 0;
				wigData.drilldown = data[i].DepartmentWigName;
				wig.data.push(wigData);

				debugger;	
				var lmData = [];
				lmData[0] = data[i].DepartmentLmName;
				lmData[1] = (allCommitment!==0) ? (data[i].CommitmentCount/allCommitment).toFixed(2) : 0;

				var lm = {};
				lm.data =[];
				lm.name = data[i].DepartmentLmName;
				lm.id = data[i].DepartmentWigName;
				lm.data.push(lmData);
				series.push(lm);
			}
			console.log('wig >>>> ',wig);
			console.log('lm >>>> ',lm);
		},
		error: function (data,status){
			$("#lblError").css('display','block')
			console.log(data);
		}
	});
}


(function($) {
    Highcharts.chart('container', {
		colors: ['#003f5c','#2f4b7c','#665191','#a05195','#d45087','#f95d6a','#ff7c43','#ffa600'],
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
                    //enabled: true,
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
	getTableCommitment(1);
}(jQuery));


