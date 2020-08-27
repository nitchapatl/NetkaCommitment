$(document).ready(function() {
	getGraphCommitment(); 
	getTableCommitment(15,null,null);

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
				console.log('data :::: ',data);
				$("#tbCommitment").empty();
				var html = "<thead>"
					html += "<tr>"
					html += "<th>No.</th>"
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
					html += "<td style='text-align:left'>" + data[i].CommitmentName + "</td>"
					html += "<td style='text-align:left'>" + data[i].DepartmentWigName + "</td>"
					html += "<td style='text-align:left'>" + data[i].DepartmentLmName + "</td>"
					html += "<td>" + data[i].CommitmentStatus + "</td>"
					html += "<td>" + StartDate + "</td>"
					html += "<td>" + closeDate + "</td>" 
					html += "</tr>"
				}
				html += "</tbody>"
				$("#tbCommitment").append(html);

				var table = $('#tbCommitment').DataTable( {
					destroy: true,
					responsive: true,
					lengthMenu: [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]],
					columnDefs: [
						{ responsivePriority: 1, targets: 0 },
						{ responsivePriority: 2, targets: 4 },
						{ responsivePriority: 3, targets: 6 },
						{targets:[2], class:"wrapok"}
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
				console.log(data);

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
