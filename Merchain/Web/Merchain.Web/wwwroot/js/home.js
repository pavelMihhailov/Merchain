$(document).ready(function () {
	/*------------------
		ScrollBar
	--------------------*/
    $(".cart-table-warp, .product-thumbs").niceScroll({
        cursorborder: "",
        cursorcolor: "#afafaf",
        boxzoom: false
    });

	/*------------------
		Hero Slider
	--------------------*/
	var hero_s = $(".hero-slider");
	hero_s.owlCarousel({
		loop: true,
		margin: 0,
		nav: true,
		items: 1,
		dots: true,
		animateOut: 'fadeOut',
		animateIn: 'fadeIn',
		navText: ['<i class="flaticon-left-arrow"></i>', '<i class="flaticon-right-arrow"></i>'],
		smartSpeed: 1200,
		autoHeight: false,
		autoplay: true,
		onInitialized: function () {
			var a = this.items().length;
			$("#snh-1").html("<span>1</span><span>" + a + "</span>");
		}
	}).on("changed.owl.carousel", function (a) {
		var b = --a.item.index, a = a.item.count;
		$("#snh-1").html("<span> " + (1 > b ? b + a : b > a ? b - a : b) + "</span><span>" + a + "</span>");
	});

	hero_s.append('<div class="slider-nav-warp"><div class="slider-nav"></div></div>');
	$(".hero-slider .owl-nav, .hero-slider .owl-dots").appendTo('.slider-nav');

	/*------------------
		Brands Slider
	--------------------*/
	$('.product-slider').owlCarousel({
		loop: true,
		nav: true,
		dots: false,
		margin: 30,
		autoplay: true,
		navText: ['<i class="flaticon-left-arrow-1"></i>', '<i class="flaticon-right-arrow-1"></i>'],
		responsive: {
			0: {
				items: 1
			},
			480: {
				items: 2
			},
			768: {
				items: 3
			},
			1200: {
				items: 4
			}
		},
		autoplayHoverPause: true
	});

	/*------------------
		Popular Services
	--------------------*/
	$('.popular-services-slider').owlCarousel({
		loop: true,
		dots: false,
		margin: 40,
		autoplay: true,
		nav: true,
		navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
		responsive: {
			0: {
				items: 1
			},
			768: {
				items: 2
			},
			991: {
				items: 3
			}
		}
	});
});