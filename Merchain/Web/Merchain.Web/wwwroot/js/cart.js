var proQty = $('.cart-quantity');
proQty.prepend('<span class="dec qtybtn qtybtn-cart">-</span>');
proQty.append('<span class="inc qtybtn qtybtn-cart">+</span>');
proQty.on('click', '.qtybtn-cart', function () {
	var $button = $(this);
	var oldValue = $button.parent().find('input').val();

	var productId = $($button.closest("article")).attr("id");

	if ($button.hasClass('inc')) {
		var newVal = parseFloat(oldValue) + 1;

		if ($button.parents('.cart-section').length) {
			let size = $button.closest("article").find(".size-col").text();
			let colorId = null;
			let color = $button.closest("article").find(".box")[0];
			if (color !== undefined) {
				colorId = color.id;
			}
			$.ajax({
				type: "GET",
				url: "/ShoppingCart/AddProduct",
				data: { 'id': productId, 'quantity': 1, 'size': size, 'colorId': colorId },
				success: function () {
				}
			});

			refreshCartPrices(productId, false, true);
		}

		$button.parent().find('input').val(newVal);
	} else {
		// Don't allow decrementing below zero
		if (oldValue > 1) {
			var newVal = parseFloat(oldValue) - 1;
			$button.parent().find('input').val(newVal);

			if ($button.parents('.cart-section').length) {
				let size = $button.closest("article").find(".size-col").text();
				let colorId = null;
				let color = $button.closest("article").find(".box")[0];
				if (color !== undefined) {
					colorId = color.id;
				}

				$.ajax({
					type: "GET",
					url: "/ShoppingCart/DecreaseQuantity",
					data: { 'id': productId, 'size': size, 'colorId': colorId },
					success: function () {
					}
				});

				refreshCartPrices(productId, false, false);
			}
		}
	}
});

$(document).ready(function () {
	$(".remove-product").on("click", function () {
		$el = $(this);

		let productId = $($el.closest("article")).attr("id");

		//refreshCartPrices(productId, true, false);

		let size = $el.closest("article").find(".size-col").text();
		let colorId = null;
		let color = $el.closest("article").find(".box")[0];
		if (color !== undefined) {
			colorId = color.id;
		}

		$.ajax({
			type: "GET",
			url: "/ShoppingCart/RemoveProduct",
			data: { 'id': productId, 'size': size, 'colorId': colorId },
			success: function () {
				location.reload();
			}
		});
	});

	$(".applyPromoCode").on("click", function (e) {
		let promoCodeValue = $($("input[name='PromoCode']")[0]).val();

		if (promoCodeValue.length) {
			document.location.href = "/Order?promoCode=" + promoCodeValue;
		}
		else {
			e.preventDefault();
		}
	});
});

function refreshCartPrices(id, isRemoved, increment) {
	let productRow = $("article[id='" + id + "']")[0];

	let productPriceTag = parseFloat($(productRow).find(".total-col")[0].innerText);

	let totalPriceTag = $(".total-cost").find("span")[0];
	let totalPriceHtml = totalPriceTag.innerText;

	let totalPrice = parseFloat(totalPriceHtml.substring(0, totalPriceHtml.length - 3));

	if (increment) {
		totalPrice += productPriceTag;
	}
	else {
		totalPrice -= productPriceTag;
	}

	$(totalPriceTag).html(totalPrice.toFixed(2) + " лв.");
}