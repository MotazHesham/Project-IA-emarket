$(document).ready(function(){
	"use strict";

	//navbar scrolling animation
	$(window).on("scroll", function () {
		if ($(document).scrollTop() > 50) {
			$('.navbar-edit').addClass('nav-scrolling');
			$('.navbar-edit').removeClass('nav-scrolling00');
		} else {
			$('.navbar-edit').removeClass('nav-scrolling');
		}
	});

	$(".wave-container img").animate({
		right: "8%"
	}, 1000).animate({
		right: "5%"
	}, 700);

	$(".wave-container .welcome-text").animate({
		top: "38%"
	}, 1000).animate({
		top: "34%"
	}, 700);



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

	/*ajax call*/
	$('#categories_ajax').on('change', function () {
		var category_id = $(this).val();
		$.ajax({
			method: "Post",
			url: "/Products/category_select/",
			data: { "category_id": category_id },
			success: function (data) {
				$('#category-select').html(data);
			}
		})
	})

	/*ajax call*/
	$('#select_all_products').on('click', function () {
		$.ajax({
			method: "Post",
			url: "/Products/select_all_products/",
			success: function (data) {
				$('#category-select').html(data);
			}
		})
	})

});	