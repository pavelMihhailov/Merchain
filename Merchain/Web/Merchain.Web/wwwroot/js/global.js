$(window).on("load", function () {
    refreshCartItems();
});

function refreshCartItems() {
    $.ajax({
        type: "GET",
        url: "/ShoppingCart/GetCartItemsCount",
        success: function (res) {
            let $itemsCount = $($(".shopping-card span")[0]);
            $itemsCount.html("");
            $itemsCount.html(res);
        }
    });
}
