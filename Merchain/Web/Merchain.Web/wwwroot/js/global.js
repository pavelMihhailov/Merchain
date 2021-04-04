$(window).on("load", function () {
    refreshCartItems();
});

$(document).ready(function () {
    let messagePopup = $("#msgPopup");

    if (messagePopup.length !== 0) {
        setTimeout(function () {
            messagePopup.fadeOut("slow");
        }, 8000);
    }

    $(document).on('click', '.add-wishlist', function (e) {
        e.preventDefault();

        $el = $(this);

        let productId = $el.attr("id");
        let quantityTag = $("input[name='quantity']");
        let quantity = null;

        if (quantityTag.length) {
            quantity = quantityTag.val();
        }

        $.ajax({
            type: "GET",
            url: "/Products/AddToWishList",
            data: { 'id': productId, 'quantity': quantity },
            success: function (res) {
                $("#addedToWishList").addClass("show-modal");
                setTimeout(function () {
                    $("#addedToWishList").removeClass("show-modal");
                }, 2400);
            }
        });
    });


    $(".remove-wishlist").on("click", function (e) {
        e.preventDefault();

        $el = $(this);

        let productId = $el.attr("id");

        $.ajax({
            type: "GET",
            url: "/Products/RemoveFromWishList",
            data: { 'id': productId },
            success: function () {
                $el.parents(".col-lg-3")[0].remove();
                if ($(".col-lg-3").length === 0) {
                    $($("section .row")[0]).html("<div>Нямате харесани продукти към момента.</div>");
                }
            }
        });
    });

    $(document).on('click', '.add-card', function (e) {
        e.preventDefault();

        $el = $(this);

        let productId = $el.attr("id");
        let quantity = $("input[name='quantity']").val();

        if (quantity === undefined) {
            quantity = 1;
        }

        let colorId = null;
        if ($("#colors").length) {
            let selectedColor = $("#colors .box.selected");
            if (!selectedColor.length) {
                $(".color-error").text("Моля изберете цвят");
                return;
            }

            colorId = $("#colors .box.selected")[0].id;
        }

        let size = "";
        if ($(".fw-size-choose").length) {
            let selectedSize = $("input[name='Size']:checked");
            if (!selectedSize.length) {
                $(".size-error").text("Моля изберете размер");
                return;
            }

            size = selectedSize[0].id;
        }

        $.ajax({
            type: "GET",
            url: "/ShoppingCart/AddProduct",
            data: { 'id': productId, 'quantity': quantity, 'size': size, 'colorId': colorId },
            success: function (res) {
                if ($(".cart-table").length) {
                    location.reload();
                }
                else {
                    $("#addedToCart").addClass("show-modal");
                    refreshCartItems();
                    setTimeout(function () {
                        $("#addedToCart").removeClass("show-modal");
                    }, 2000);
                }
            }
        });
    });

    $(document).on('click', '.box', function (e) {
        $el = $(this);

        $(".box").removeClass("selected");
        $el.addClass("selected");
        $(".color-error").text("");
    });

    $(document).on('click', "input[name='Size']", function (e) {
        $(".size-error").text("");
    });

    $("#removeImage").on("click", function (e) {
        e.preventDefault();

        $el = $(this);

        var imageUrl = $($el.siblings())[0].src;

        var $inputImages = $("input[name='Product.ImagesUrls']");
        var inputImagesVal = $inputImages.val();

        var splitterIndex = inputImagesVal.indexOf(";");

        var splitter = "";
        if (splitterIndex !== -1) {
            splitter = ";";
        }

        var resultUrls = inputImagesVal.replace(imageUrl + splitter, "");

        $inputImages.val(resultUrls);
        $el.parents("div")[0].remove();
    });
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

function hideCookieBanner() {
    var cookieConsent = $("#cookieConsent");
    document.cookie = cookieConsent.data("cookie-string");
    cookieConsent.addClass("d-none");
}

window.onscroll = function () { scrollFunction(); };

var topBtn = document.getElementsByClassName("topBtn")[0];

function scrollFunction() {
    if (document.body.scrollTop > 30 || document.documentElement.scrollTop > 30) {
        topBtn.style.display = "block";
    } else {
        topBtn.style.display = "none";
    }
}

function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}