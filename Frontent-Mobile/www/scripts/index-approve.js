function checkAll(ctrl){
	var table = $('#tbCommitmentApprove').DataTable();
	var rows = table.rows({'search':'applied'}).nodes();
	var isCheck = ctrl.prop("checked");
	$('input[type="checkbox"]', rows).prop('checked', isCheck);

	if(isCheck){
		$(rows).addClass('selected');
	}else{
		$(rows).removeClass('selected');
	}

}

function checkCommitment(ctrl){
	var isSelect = $(ctrl).parents().eq(1).hasClass("selected");
	//if(isSelect){
	//	$(ctrl).parents().eq(1).removeClass('selected');
	//}

	var isCheck = ctrl.prop("checked");
	if(isCheck){
		if(!isSelect){
			$(ctrl).parents().eq(1).addClass('selected');
		}
	}else{
		if(isSelect){
			$(ctrl).parents().eq(1).removeClass('selected');	
		}
	}
}

function validateInput(){
	var commitment = [];
	var table = $('#tbCommitmentApprove').DataTable();
	commitment.push(table.rows('.selected').data());

	var status = $('input[name=status]:checked', '#chbStatus').val();
	var level = $('#ddlLevel option:selected').val();
	var message="";
	if (status===undefined)
	{
		message = "Please select status";
	}
	else if(level==""){
		message = "Please select level";
	}
	else if (commitment.length<=0)
	{
		message = "Please select commitment";
	}

	notification(message);
	return
}

function validateConfrim(ctrl){
	validateInput();
	$('#menu-confirm-approve').addClass('menu-active');
	$('.menu-hider').addClass('menu-active');
	$('.confirm-approve').text('Do you want to approve this commitment?');
}

function clearData(){
	var table = $('#tbCommitmentApprove').DataTable();
	var rows = table.rows({'search':'applied'}).nodes();
	$('input[type="checkbox"]', rows).prop('checked', false);
	$(rows).removeClass('selected');

	$('.menu-hider').removeClass('menu-active');
	$('.online-message').addClass('online-message-active');
}

function confirmApprove(){	
	var table = $('#tbCommitmentApprove').DataTable();
	dataSelected = table.rows('.selected').data();
	var data = {};
	data.listCommitmentId = [];
	for(var i=0;i<dataSelected.length;i++)
	{
		data.listCommitmentId.push(dataSelected[i][0]);
	}
	data.ApproveStatus = $('input[name=status]:checked', '#chbStatus').val();
	data.ApproveType = $('#ddlLevel option:selected').val();
	data.ApproveRemark = $('#txtRemarkApprove').val();
	data.ApproveUserId = 13;
	data.CreatedBy = 13;

	console.log(data);
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: URL + '/api/commitment/AddCommitmentApprove',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
			console.log('AddCommitmentApprove >>> ',data);
			//clearData();
		},
		error: function (data,status){
			$("#lblError").css('display','block')
			console.log(data);		
		}
	});
}

function notification(message){
	$('.offline-message').text(message)
	$('.offline-message').addClass('offline-message-active');
	setTimeout(function(){
		$('.offline-message').removeClass('offline-message-active');
	},3000);
}

$(document).ready(function() {
	getGraphCommitment(); 
	getTableCommitment(15,null,null);

	$('.menu').css('overflow','hidden');
	function getTableCommitment(userId,wigName,lmName){
		data = {};
		data.CreatedBy = userId;
		data.DepartmentWigName = wigName;
		data.DepartmentLmName = lmName;
		$.ajax({
			type:'POST',
			contentType: 'application/json; charset=utf-8',
			url: URL + '/api/commitment/getCommitmentClosed',
			data: JSON.stringify(data),
			dataType: "json",
			success: function (data,status){
				$("#tbCommitmentApprove").empty();
				var html = "<thead>"
					html += "<tr>"
					html += "<th>No.</th>"
					html += '<th style="text-align:center"><input onclick="checkAll($(this))" value="All" id="chbAll" type="checkbox"></th>'
					html += "<th>Commitment</th>"
					html += "<th>WIG</th>"
					html += "<th>LM</th>"
					html += "<th>Status</th>"
					html += "<th>Start Date</th>"
					html += "<th>Close Date</th>"
					html += "</tr>";
					html += "</thead>"
					html += "<tbody>"
				for(var i in data){
					var closeDate = (moment(data[i].UpdatedDate).isValid()) ? moment(data[i].UpdatedDate).format('DD/MM/YYYY') : '';
					var StartDate = (moment(data[i].CommitmentStartDate).isValid()) ? moment(data[i].CommitmentStartDate).format('DD/MM/YYYY') : '';
					html += "<tr>"
					html += "<td>" + data[i].CommitmentId + "</td>"
					html += '<td><input onclick="checkCommitment($(this));" name="select_all" value=' + data[i].CommitmentId + ' id="example-select-all" type="checkbox"></td>';
					html += "<td style='text-align:left'>" + data[i].CommitmentName + "</td>"
					html += "<td style='text-align:left'>" + "<b>WIG" + data[i].DepartmentWigSequence + ":</b>:" + data[i].DepartmentWigName + "</td>"
					html += "<td style='text-align:left'>" + data[i].DepartmentLmName + "</td>"
					html += "<td>" + data[i].CommitmentStatus + "</td>"
					html += "<td nowrap>" + StartDate + "</td>"
					html += "<td nowrap>" + closeDate + "</td>" 
					html += "</tr>"
				}
				html += "</tbody>"
				$("#tbCommitmentApprove").append(html);

				var table = $('#tbCommitmentApprove').DataTable( {
					destroy: true,
					responsive: true,
					select: {
						style: 'multi'
					},
					scrollX: false,
					lengthMenu: [[5, 10, 25, 50, 100,-1], [5, 10, 25, 50, 100,'All']],
					columnDefs: [
						{ responsivePriority: 1, targets: 0 },
						{ responsivePriority: 2, targets: 1 },
						{ responsivePriority: 3, targets: 5 },
						{ responsivePriority: 4, targets: 6 },
						{ targets:[2], class:"wrapok"},
						{
							targets: 1,
							searchable: false,
							orderable: false,
							className: 'dt-body-center'
						}
					]
				});
 
			},
			error: function (data,status){
				$("#lblError").css('display','block')
				console.log(data);		
			}
		});
	}

	function getGraphCommitment(){
		data = {};
		data.CreatedBy = 1;
		$.ajax({
			type:'POST',
			contentType: 'application/json; charset=utf-8',
			url: 'https://localhost:32774/api/commitment/GetCommitmentSummary',
			data: JSON.stringify(data),
			dataType: "json",
			success: function (data,status){
				//Wig
				var arrCommitmentWigId = [];
				var arrCommitmentWig = [];
				var countCommitment = 0;

				//Lm
				var drilldown = [];			
				for (var j in data)
				{
					//Wig
					var departmentWigId = data[j].DepartmentWigId;
					countCommitment += data[j].CommitmentCount;

					//Lm
					var lmData = [];
					if(arrCommitmentWigId.indexOf(departmentWigId)===-1){
						//Wig
						var objCommitmentWig = {};
						objCommitmentWig.y = 0;
						objCommitmentWig.name = data[j].DepartmentWigName;
						objCommitmentWig.drilldown = data[j].DepartmentWigName;
						objCommitmentWig.y += data[j].CommitmentCount;
						arrCommitmentWig.push(objCommitmentWig);
						arrCommitmentWigId.push(departmentWigId);

						//Lm
						var lm = {};
						lm.data =[];
						lm.countLm = 0;
						lm.name = data[j].DepartmentWigName;
						lm.id = data[j].DepartmentWigName;
						lm.countLm = data[j].CommitmentCount;

						lmData[0] = data[j].DepartmentLmName;
						lmData[1] = data[j].CommitmentCount; 
						lm.data.push(lmData);

						drilldown.push(lm);
					}else{
						//Wig
						var index = arrCommitmentWigId.indexOf(departmentWigId);
						arrCommitmentWig[index].y += data[j].CommitmentCount;	
						
						//Lm
						drilldown[index].countLm += data[j].CommitmentCount;

						var index = arrCommitmentWigId.indexOf(departmentWigId);
						lmData[0] = data[j].DepartmentLmName;
						lmData[1] = data[j].CommitmentCount; 
						drilldown[index].data.push(lmData);
					}
				}

				var arrWigId = [];
				var wig = {};
				wig.name = "WIG",
				wig.colorByPoint = true,
				wig.data = arrCommitmentWig;
				var series = [];
				for (var i=0; i<data.length;i++ )
				{	
					//check duplicate
					var wigId = data[i].DepartmentWigId;

					if(arrWigId.indexOf(wigId)===-1){
						arrWigId.push(wigId);

						//Wig 
						var index = arrWigId.indexOf(wigId);
						wig.data[index].y = (countCommitment!==0) ? parseFloat(((wig.data[index].y*100)/countCommitment).toFixed(2)) : 0;

						//Lm
						var countLm = drilldown[index].countLm;
						var arrLmData = [];
						arrLmData = drilldown[index].data;
						for(var k in arrLmData){						
							drilldown[index].data[k][1] = (countLm!==0) ? parseFloat(((arrLmData[k][1]*100)/countLm).toFixed(2)) : 0;  
						}
					}
				}
				series.push(wig);

				createPieChart(series,drilldown);
				//createColumnChart(series,drilldown);
			},
			error: function (data,status){
				$("#lblError").css('display','block')
				console.log(data);
			}
		});
	}


	function createPieChart(wig,lm){
	   Highcharts.chart('container', {
			credits: {
				enabled: false
			},
			chart: {
				type: 'pie',
				backgroundColor: 'transparent',
				events: {
					drilldown: function (e) {
						var wigName = e.point.name;
						getTableCommitment(15,wigName,null);
					},
					drillup: function (e) {
						getTableCommitment(15,null,null);
					}
				}
			},
			colors: [
				'#003f5c',
				'#2f4b7c',
				'#665191',
				'#a05195',
				'#d45087',
				'#f95d6a',
				'#ff7c43',
				'#ffa600'
			],
			title: {
				text: ''
			},
			subtitle: {
				text: ''
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
						format: '{point.name}: {point.y:.2f}%'
					},
					point: {
						events: {
							/*click: function(e){

							},*/
							select: function(e){
								var wigName = this.series.name;
								var lmName = this.name; 
								getTableCommitment(15,wigName,lmName);
							},
							unselect: function(e){							
								var wigName = this.series.name;
								var lmName = this.name; 
								getTableCommitment(15,wigName,null);
							}
						}
					}
				},
				pie:{
					borderWidth: 0,
					allowPointSelect: true,
				}
			},
			tooltip: {
				headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
				pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
			},
			series: wig,
			drilldown: {
				series: lm
			}
		});
	}

	function createColumnChart(wig,lm){
		Highcharts.chart('container2', {
			credits: {
				enabled: false
			},
			chart: {
				type: 'column',
				backgroundColor: 'transparent'
			},
			colors: [
				'#003f5c',
				'#2f4b7c',
				'#665191',
				'#a05195',
				'#d45087',
				'#f95d6a',
				'#ff7c43',
				'#ffa600'
			],
			title: {
				text: ''
			},
			subtitle: {
				text: ''
			},
			accessibility: {
				announceNewData: {
					enabled: true
				}
			},
			xAxis: {
				type: 'category'
			},
			yAxis: {
				title: {
					text: 'commitment (%)'
				}

			},
			legend: {
				enabled: false
			},
			plotOptions: {
				series: {
					borderWidth: 0,
					dataLabels: {
						enabled: true,
						format: '{point.y:.2f}%'
					}
				}
			},

			tooltip: {
				headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
				pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
			},

			series: wig,
			drilldown: {
				series: lm
			}
		});
	}
});
