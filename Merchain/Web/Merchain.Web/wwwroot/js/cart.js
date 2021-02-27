$(document).ready(function () {
    $(".remove-icon").on("click", function () {
        $el = $(this);

        let productId = $($el.closest("tr")).attr("id");

        refreshCartPrices(productId, true, false);

        let size = $el.closest("tr").find(".size-col").text();
        let colorId = null;
        let color = $el.closest("tr").find(".box")[0];
        if (color !== undefined) {
            colorId = color.id;
        }

        $.ajax({
            type: "GET",
            url: "/ShoppingCart/RemoveProduct",
            data: { 'id': productId, 'size': size, 'colorId': colorId },
            success: function () {
                $($el.closest("tr")).remove();
                refreshCartItems();
            }
        });
    });

    $(".applyPromoCode").on("click", function (e) {
        let promoCodeValue = $($("input[name='PromoCode']")[0]).val();

        document.location.href = "/Order?promoCode=" + promoCodeValue;
    });
});

function refreshCartPrices(id, isRemoved, increment) {
    let productRow = $("tr[id='" + id + "']")[0];

    let productPriceTag = $($(productRow).find(".total-col")[0]).find("h4")[0];
    let totalPriceTag = $(".total-cost").find("span")[0];

    let productPriceHtml = $(productPriceTag)[0].innerText;
    let totalPriceHtml = totalPriceTag.innerText;

    let productTotalPrice = parseFloat(productPriceHtml.substring(0, productPriceHtml.length - 3));
    let totalPrice = parseFloat(totalPriceHtml.substring(0, totalPriceHtml.length - 3));

    let quantity = $(productRow).find('input').val();

    let productPrice = productTotalPrice / quantity;

    if (increment) {
        productTotalPrice += productPrice;
        totalPrice += productPrice;
    }
    else {
        productTotalPrice -= productPrice;
        if (isRemoved) {
            totalPrice -= productPrice * quantity;
        }
        else {
            totalPrice -= productPrice;
        }
    }

    $(productPriceTag).html(productTotalPrice.toFixed(2) + " лв.");
    $(totalPriceTag).html(totalPrice.toFixed(2) + " лв.");
}