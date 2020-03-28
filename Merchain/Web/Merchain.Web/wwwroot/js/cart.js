$(document).ready(function () {
    $(".remove-icon").on("click", function () {
        $el = $(this);

        let productId = $($el.closest("tr")).attr("id");

        refreshCartPrices(productId, true, false);

        $.ajax({
            type: "GET",
            url: "/ShoppingCart/RemoveProduct",
            data: { 'id': productId },
            success: function () {
                $($el.closest("tr")).remove();
                refreshCartItems();
            }
        });
    });
});

function refreshCartPrices(id, isRemoved, increment) {
    let productRow = $("tr[id='" + id + "']")[0];

    let productPriceTag = $($(productRow).find(".total-col")[0]).find("h4")[0];
    let totalPriceTag = $(".total-cost").find("span")[0];

    let productPriceHtml = $(productPriceTag)[0].innerText;
    let totalPriceHtml = totalPriceTag.innerText;

    let productTotalPrice = parseFloat(productPriceHtml.substring(1, productPriceHtml.length));
    let totalPrice = parseFloat(totalPriceHtml.substring(1, totalPriceHtml.length));

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

    $(productPriceTag).html("$" + productTotalPrice.toFixed(2));
    $(totalPriceTag).html("$" + totalPrice.toFixed(2));
}