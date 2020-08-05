var commitment = {};
commitment.host = "https://localhost:32770";
commitment.userId = 15;
getDDLDepartmentWIG();
getGraphCommitmentSummary(); 
getTableCommitment(commitment.userId);
getTableCommitmentPending(commitment.userId);
getTableCommitmentClosed(commitment.userId);

function getDatatable(id){
    var table = $('#' + id).DataTable( {
		destroy: true,
        responsive: true,
		lengthMenu: [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]]
    } );
}
function getDDLDepartmentWIG(){
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: commitment.host + '/api/commitment/getDepartmentWig',
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
		url: commitment.host + '/api/commitment/getDepartmentWig',
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
	data.CreatedBy = commitment.userId;
	
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: commitment.host + '/api/commitment/addCommitment',
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
			getTableCommitment(commitment.userId);
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
		url: commitment.host + '/api/commitment/GetCommitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
			$("#tbCommitment").empty();
			var html = "<thead>"
				html += "<tr>"
				html += "<th>No.</th>"
				html += "<th>Complete</th>"
				html += "<th>Delete</th>"
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
				html += '<td>'
				html += '<input onclick="commitmentSuccess();" value="Success" type="button" class="button button-xs shadow-small button-round-small bg-mint-dark btnSuccess"/>'
				html += '</td>'
				html += '<td>'
				html += '<input onclick="commitmentDelete($(this));" value="Delete" type="button" class="button button-xs shadow-small button-round-small bg-red1-dark btnDelete"/>'
				html += '</td>'
				html += "<td style='text-align:left'>" + data[i].CommitmentName + "</td>"
				html += "<td>" + moment(data[i].CommitmentStartDate).format('DD/MM/YYYY') + "</td>"
				html += "<td>" + data[i].CommitmentStatus + "</td>"
				html += '<td>'
				html += '<input onclick="commitmentPostpone();" value="postpone" type="button" class="button button-xs shadow-small button-round-small bg-blue2-dark btnPostpone"/>'
				html += '<input onclick="commitmentFail();" value="Fail" type="button" style="margin-left:5px;" class="button button-xs shadow-small button-round-small bg-brown2-dark btnFail"/>'
				html += '</td>'
				html += "</tr>"
			}
			html += "</tbody>"
			$("#tbCommitment").append(html);
			getDatatable('tbCommitment');
		},
		error: function (data,status){
			$("#lblError").css('display','block')
			console.log(data);		
		}
	});
}

function getTableCommitmentPending(createBy){
	data = {};
	data.CreatedBy = createBy;
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: commitment.host + '/api/commitment/GetCommitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
			$("#tbCommitmentPending").empty();
			var html = "<thead>"
				html += "<tr>"
				html += "<th>No.</th>"
				html += "<th>Commitment</th>"
				html += "<th>Start Date</th>"
				html += "<th>Status</th>"
				html += "<th>Delete</th>"
				html += "</tr>";
				html += "</thead>"
				html += "<tbody>"
			for(var i in data){
				html += "<tr>"
				html += "<td>" + data[i].CommitmentId + "</td>"
				html += "<td style='text-align:left'>" + data[i].CommitmentName + "</td>"
				html += "<td>" + moment(data[i].CommitmentStartDate).format('DD/MM/YYYY') + "</td>"
				html += "<td>" + data[i].CommitmentStatus + "</td>"
				html += '<td>'
				html += '<input onclick="commitmentDelete($(this));" value="Delete" type="button" class="button button-xs shadow-small button-round-small bg-red1-dark btnDelete"/>'
				html += '</td>'
				html += "</tr>"
			}
			html += "</tbody>"
			$("#tbCommitmentPending").append(html);
			getDatatable('tbCommitmentPending');
		},
		error: function (data,status){
			$("#lblError").css('display','block')
			console.log(data);		
		}
	});
}

function getTableCommitmentClosed(createBy){
	data = {};
	data.CreatedBy = createBy;
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: commitment.host + '/api/commitment/GetCommitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
			$("#tbCommitmentClosed").empty();
			var html = "<thead>"
				html += "<tr>"
				html += "<th>No.</th>"
				html += "<th>Commitment</th>"
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
				html += "<td>" + data[i].CommitmentStatus + "</td>"
				html += "<td>" + StartDate + "</td>"
				html += "<td>" + closeDate + "</td>" 
				html += "</tr>"
			}
			html += "</tbody>"
			$("#tbCommitmentClosed").append(html);
			getDatatable('tbCommitmentClosed');
		},
		error: function (data,status){
			$("#lblError").css('display','block')
			console.log(data);		
		}
	});
}

function statusOnChange(){
	var status = $("#ddlStatus option:selected").val();
	$('#txtRemark').val('');
	if(status==="Postpone"){
		$('.update-remark').show();
		$('.update-postpone').show();
		$('#menu-confirm').css("height","500px");
	}else if(status==="Fail"){
		$('.update-remark').show();
		$('.update-postpone').hide();
		$('#menu-confirm').css("height","450px");
	}	
}

function commitmentDelete(ctrl){
	var tableId = ctrl.closest('table').attr('id');
	$('#' + tableId + ' tbody').on('click', '.btnDelete', function (e) {
		e.preventDefault();
		var table = $('#' + tableId).DataTable(); 
		var index = table.row($(this).parents('tr')).index(); 
		var rowData = table.row(index).data();

		var id = rowData[0];
		var status = 'Delete';

		$("#menu-confirm-update").attr("data-id",id);
		$("#menu-confirm-update").attr("data-status",status);

		$('#menu-confirm-update').addClass('menu-active');
		$('.menu-hider').addClass('menu-active');

		var messsage = 'Do you want to remove this commitment?';
		$('.confirm-commitment').text(messsage);
	});
}

function commitmentSuccess(){
	$('#tbCommitment tbody tr').on('click', '.btnSuccess', function () {
		var table = $('#tbCommitment').DataTable(); 
		var index = table.row($(this).parents('tr')).index(); 
		var rowData = table.row(index).data();

		var id = rowData[0];
		var status = 'Success';

		$("#menu-confirm-update").attr("data-id",id);
		$("#menu-confirm-update").attr("data-status",status);

		$('#menu-confirm-update').addClass('menu-active');
		$('.menu-hider').addClass('menu-active');

		var messsage = 'Do you want to save thes changes?';
		$('.confirm-commitment').text(messsage);
	});
}

function commitmentFail(){
	$('#tbCommitment tbody tr').on('click', '.btnFail', function () {
		var table = $('#tbCommitment').DataTable(); 
		var index = table.row($(this).parents('tr')).index(); 
		var rowData = table.row(index).data();

		var id = rowData[0];
		var commitment = rowData[3];
		var updateDate = rowData[4];
		var status = rowData[5];

		$('#ddlStatus').prop('selectedIndex',2);
		$('.update-status').find('i').remove();
		$('.update-status').find('em').append('<i class="fa fa-angle-down"></i>');

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

		$('#ddlStatus').prop('disabled',true);
		statusOnChange();
	});
}

function commitmentPostpone(ctrl){
	$('#tbCommitment tbody tr').on('click', '.btnPostpone', function () {
		var table = $('#tbCommitment').DataTable(); 
		var index = table.row($(this).parents('tr')).index(); 
		var rowData = table.row(index).data();
		
		var id = rowData[0];
		var commitment = rowData[3];
		var updateDate = rowData[4];
		var status = rowData[5];

		$('#ddlStatus').prop('selectedIndex',1);
		$('.update-status').find('i').remove();
		$('.update-status').find('em').append('<i class="fa fa-angle-down"></i>');

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

		$('#ddlStatus').prop('disabled',true);
		statusOnChange();
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
	var id = $('#menu-confirm-update').attr("data-id");
	var status = $('#menu-confirm-update').attr("data-status");

	var data = {};
	data.CommitmentId = id;
	data.CommitmentStatus = status;
	data.UpdatedBy = 15;

	debugger
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: 'https://localhost:32770/api/commitment/updateCommitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){			
			$('#menu-confirm').removeClass('menu-active');
			$('.menu-hider').removeClass('menu-active');
			$('.online-message').addClass('online-message-active');

			getTableCommitment(commitment.userId);
			getTableCommitmentPending(commitment.userId);
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

function UpdateCommitment(){
	var id = $('#menu-confirm').attr("data-id");
	var status = $( "#ddlStatus option:selected" ).val(); 
	var commitmentName = $('#txtCommitmentName').val();
	var remark = $('#txtRemark').val();
	var postpone = $('#txtPostpone').val();

	if(status==""){
		message = "Please select status";
	}else if (commitmentName==""){
		message = "Please input commitment"
	}
	else if (remark==""){
		message = "Please input remark";
	}

	if((status==="Fail") && (commitmentName==="" || remark==="")){
		notification(message);
		return
	}else if(status==="Postpone" && (postpone==="" || commitmentName==="" || remark==="")){
		notification(message);
		return
	}
	
	var data = {};
	data.CommitmentId = id;
	data.CommitmentName = commitmentName;
	data.CommitmentStatus = status;
	data.CommitmentRemark = remark;
	data.UpdatedBy = 15;

	if(status==="postpone"){
		data.CommitmentStartDate = $('#txtPostpone').val();
	}

	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: 'https://localhost:32770/api/commitment/updateCommitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){			
			$('#menu-confirm').removeClass('menu-active');
			$('.menu-hider').removeClass('menu-active');
			$('.online-message').addClass('online-message-active');

			getTableCommitment(commitment.userId);
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

function getGraphCommitmentSummary(){
	data = {};
	data.CreatedBy = 1;
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: commitment.host + '/api/commitment/GetCommitmentSummary',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
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
		},
		error: function (data,status){
			$("#lblError").css('display','block')
			console.log(data);
		}
	});
}

