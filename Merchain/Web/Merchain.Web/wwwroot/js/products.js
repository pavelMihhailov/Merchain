$(document).ready(function () {
    $(".load-more").on("click", function () {
        $el = $(this);

        let skip = 0;
        let categoryId = -1;
        let minPrice = $(".price-range").data("min");
        let maxPrice = $(".price-range").data("max");

        $.ajax({
            type: "GET",
            url: "/Products/RefreshProducts",
            data: { 'skip': skip, 'categoryId': categoryId, 'minPrice': minPrice, 'maxPrice': maxPrice },
            success: function (res) {
                let productsList = $("#listed-products");
                let loadMoreBtn = $("#loadMoreBtn");

                $("#loadMoreBtn").remove();

                $(res).appendTo(productsList);
                loadMoreBtn.appendTo(productsList);
            }
        });
    });
});