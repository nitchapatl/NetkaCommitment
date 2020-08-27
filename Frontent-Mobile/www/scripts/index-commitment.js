var commitment = {};
commitment.userId = 15;

function UpdateCommitment(){
	var id = $('#menu-confirm').attr("data-id");
	var status = $( "#ddlStatus option:selected" ).val(); 
	var commitmentName = $('#txtCommitmentName').val();
	var remark = $('#txtRemark').val();
	var postpone = $('#txtPostpone').val();

	var message="";
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
	data.UpdatedBy = commitment.userId;

	if(status==="postpone"){
		data.CommitmentStartDate = $('#txtPostpone').val();
	}

	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: 'https://localhost:32774/api/commitment/updateCommitment',
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

function getDDLDepartmentLM(wigID){
	//check null or empty
	if(!wigID){
		return;
	}

	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: URL + '/api/commitment/getDepartmentWig',
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

function addNewCommitment(commitmentLm,commimentName,commitmentStartDate){
	var data = {};
	data.DepartmentLmId = commitmentLm;
	data.CommitmentName = commimentName;
	data.CommitmentStartDate = commitmentStartDate;
	data.CreatedBy = commitment.userId;

	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: URL + '/api/commitment/addCommitment',
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

function getTableCommitment(userId){
	data = {};
	data.CreatedBy = userId;
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: URL + '/api/commitment/GetTCommitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
			console.log(data);
			$("#tbCommitment").empty();
			var html = "<thead>"
				html += "<tr>"
				html += "<th>No.</th>"
				html += "<th>Complete</th>"
				html += "<th>Delete</th>"
				html += "<th>Commitment</th>"
				html += "<th>WIG</th>"
				html += "<th>LM</th>"
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
				html += "<th style='text-align:left'>" + "<b>WIG" + data[i].DepartmentWigSequence + ":</b>" + data[i].DepartmentWigName + "</td>"
				html += "<th style='text-align:left'>" + data[i].DepartmentLmName + "</td>"
				html += "<td nowrap>" + moment(data[i].CommitmentStartDate).format('DD/MM/YYYY') + "</td>"
				html += "<td>" + data[i].CommitmentStatus + "</td>"
				html += '<td nowrap>'
				html += '<input onclick="commitmentPostpone();" value="postpone" type="button" class="button button-xs shadow-small button-round-small bg-blue2-dark btnPostpone"/>'
				html += '<input onclick="commitmentFail();" value="Fail" type="button" style="margin-left:5px;" class="button button-xs shadow-small button-round-small bg-brown2-dark btnFail"/>'
				html += '</td>'
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
					{ responsivePriority: 2, targets: 3 },
					{ responsivePriority: 3, targets: 1 },
					{ responsivePriority: 4, targets: 7 },
				]
			});

		},
		error: function (data,status){
			$("#lblError").css('display','block')
			console.log(data);		
		}
	});
}

function getTableCommitmentPending(userId){
	data = {};
	data.CreatedBy = userId;
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: URL + '/api/commitment/GetTCommitment',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
			$("#tbCommitmentPending").empty();
			var html = "<thead>"
				html += "<tr>"
				html += "<th>No.</th>"
				html += "<th>Commitment</th>"
				html += "<th>WIG</th>"
				html += "<th>LM</th>"
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
				html += "<th style='text-align:left'>" + "<b>WIG" + data[i].DepartmentWigSequence + ":</b>" + data[i].DepartmentWigName + "</td>"
				html += "<th style='text-align:left'>" + data[i].DepartmentLmName + "</td>"
				html += "<td nowrap>" + moment(data[i].CommitmentStartDate).format('DD/MM/YYYY') + "</td>"
				html += "<td>" + data[i].CommitmentStatus + "</td>"
				html += '<td>'
				html += '<input onclick="commitmentDelete($(this));" value="Delete" type="button" class="button button-xs shadow-small button-round-small bg-red1-dark btnDelete"/>'
				html += '</td>'
				html += "</tr>"
			}
			html += "</tbody>"
			$("#tbCommitmentPending").append(html);

			var table = $('#tbCommitmentPending').DataTable( {
				destroy: true,
				responsive: true,
				lengthMenu: [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]],
				columnDefs: [
					{ responsivePriority: 1, targets: 0 },
					{ responsivePriority: 2, targets: 1 },
					{ responsivePriority: 3, targets: 5 },
					{ responsivePriority: 4, targets: 6 },
				]
			});

		},
		error: function (data,status){
			$("#lblError").css('display','block')
			console.log(data);		
		}
	});
}

function getTableCommitmentClosed(userId,wigName){
	data = {};
	data.CreatedBy = userId;
	data.DepartmentWigName = wigName;
	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: URL + '/api/commitment/GetCommitmentClosed',
		data: JSON.stringify(data),
		dataType: "json",
		success: function (data,status){
			$("#tbCommitmentClosed").empty();
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
				html += "<th style='text-align:left'>" + "<b>WIG" + data[i].DepartmentWigSequence + ":</b>" + data[i].DepartmentWigName + "</td>"
				html += "<th style='text-align:left'>" + data[i].DepartmentLmName + "</td>"
				html += "<td>" + data[i].CommitmentStatus + "</td>"
				html += "<td nowrap>" + StartDate + "</td>"
				html += "<td nowrap>" + closeDate + "</td>" 
				html += "</tr>"
			}
			html += "</tbody>"
			$("#tbCommitmentClosed").append(html);

			var table = $('#tbCommitmentClosed').DataTable( {
				destroy: true,
				responsive: true,
				lengthMenu: [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]],
				columnDefs: [
					{ responsivePriority: 1, targets: 0 },
					{ responsivePriority: 2, targets: 1 },
					{ responsivePriority: 3, targets: 4 },
					{ responsivePriority: 4, targets: 6 },
				]
			});
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
		$('#menu-confirm').css("height","475px");
	}else if(status==="Fail"){
		$('.update-remark').show();
		$('.update-postpone').hide();
		$('#menu-confirm').css("height","410px");
	}	
}

function UpdateStatus(){
	var id = $('#menu-confirm-update').attr("data-id");
	var status = $('#menu-confirm-update').attr("data-status");

	var data = {};
	data.CommitmentId = id;
	data.CommitmentStatus = status;
	data.UpdatedBy = commitment.userId;

	$.ajax({
		type:'POST',
		contentType: 'application/json; charset=utf-8',
		url: 'https://localhost:32774/api/commitment/updateCommitment',
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

function notification(message){
	$('.offline-message').text(message)
	$('.offline-message').addClass('offline-message-active');
	setTimeout(function(){
		$('.offline-message').removeClass('offline-message-active');
	},3000);
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
		$('.menu-hider').css({'display':'block','transform':'translate(0px, 0px)'});

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
		$('.menu-hider').css({'display':'block','transform':'translate(0px, 0px)'});

		//commitment 
		$('.update-commitment').find('span').removeClass('input-style-1').addClass('input-style-1-active');
		$('.update-commitment').find('i').remove();

		$('#ddlStatus').prop('disabled',true);
		statusOnChange();
	});
}



$(document).ready(function(){
	getDDLDepartmentWIG();
	getTableCommitment(commitment.userId);
	getTableCommitmentPending(commitment.userId);
	getTableCommitmentClosed(commitment.userId,null);

	$('.menu').css('overflow','hidden');
	function getDDLDepartmentWIG(){
		$.ajax({
			type:'POST',
			contentType: 'application/json; charset=utf-8',
			url: URL + '/api/commitment/getDepartmentWig',
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
});
