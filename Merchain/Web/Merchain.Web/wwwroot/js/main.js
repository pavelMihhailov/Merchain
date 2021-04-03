'use strict';

$(window).on('load', function () {
	/*------------------
		Preloder
	--------------------*/
	$(".loader").fadeOut();
	$("#preloder").fadeOut("slow");
});

(function ($) {
	/*------------------
		Navigation
	--------------------*/
	$('.main-menu').slicknav({
		prependTo: '.main-navbar .container',
		closedSymbol: '<i class="flaticon-down-arrow"></i>',
		openedSymbol: '<i class="flaticon-up-arrow"></i>'
	});

	$('<img/>').attr({
		src: '/img/coding-life-logo-white.png',
		alt: '',
		width: 147,
		height: 52,
		style: 'margin: auto'
	}).prependTo(".slicknav_menu");

	$($(".slicknav_nav").detach()).appendTo(".main-navbar .container");

	/*------------------
		Category menu
	--------------------*/
	$('.category-menu > li').hover(function (e) {
		$(this).addClass('active');
		e.preventDefault();
	});
	$('.category-menu').mouseleave(function (e) {
		$('.category-menu li').removeClass('active');
		e.preventDefault();
	});

	/*------------------
		Background Set
	--------------------*/
	$('.set-bg').each(function () {
		var bg = $(this).data('setbg');
		$(this).css('background-image', 'url(' + bg + ')');
	});

	/*------------------
		Accordions
	--------------------*/
	$('.panel-link').on('click', function (e) {
		$('.panel-link').removeClass('active');
		var $this = $(this);
		if (!$this.hasClass('active')) {
			$this.addClass('active');
		}
		e.preventDefault();
	});

	/*-------------------
		Quantity change
	--------------------- */

	$('.pro-qty').on('click', '.qtybtn', function () {
		if (!$('.cart-quantity').length) {
			var $button = $(this);
			var oldValue = $button.parent().find('input').val();

			if ($button.hasClass('inc')) {
				var newVal = parseFloat(oldValue) + 1;

				$button.parent().find('input').val(newVal);
			} else {
				// Don't allow decrementing below zero
				if (oldValue > 1) {
					var newVal = parseFloat(oldValue) - 1;

				} else {
					newVal = 0;
				}

				$button.parent().find('input').val(newVal);
			}
		}
	});

	/*------------------
		Single Product
	--------------------*/
	$('.product-thumbs-track > .pt').on('click', function () {
		$('.product-thumbs-track .pt').removeClass('active');
		$(this).addClass('active');
		var imgurl = $(this).data('imgbigurl');
		var bigImg = $('.product-big-img').attr('src');
		if (imgurl !== bigImg) {
			$('.product-big-img').attr({ src: imgurl });
			$('.zoomImg').attr({ src: imgurl });
		}
	});
})(jQuery);