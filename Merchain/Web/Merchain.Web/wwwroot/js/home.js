$(document).ready(function () {
    $(".list-products").on("click", function () {
        $el = $(this);

        let categoryId = $el.attr("id");

        $.ajax({
            type: "GET",
            url: "/Home/ListProductsBy",
            data: { 'categoryId': categoryId },
            success: function (res) {
                let productsList = $("#listed-products");
                productsList.html("");
                productsList.html(res);
            }
        });
    });

    $($(".list-products")[0]).click();
});