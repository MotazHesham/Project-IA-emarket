$(document).ready(function(){
	"use strict";

	//function change img preview when edit item ... before upload page 
	$('#imgFile').change(function () {
		var input = this;
		var url = $(this).val();
		var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
		if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
			var reader = new FileReader();

			reader.onload = function (e) {
				$('#img').attr('src', e.target.result);
			}
			reader.readAsDataURL(input.files[0]);
		}
		else {
			$('#img').attr('src', 'Uploads/empty.jpg');
		}
	});

});	