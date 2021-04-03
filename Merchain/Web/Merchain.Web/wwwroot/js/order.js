$(document).ready(function () {
    $('.shipping-info').hide();
    $('.shipping-info input').each(function (i, e) {
        $(e).attr("type", "hidden");
    });

    $("input[name='OrderAddress.ShipToOffice']").on("click", function () {
        $el = $(this);

        if ($el.val() === 'true') {
            $('.shipping-info').hide();
            $('.econt-info').show();
            $('.shipping-info input').each(function (i, e) {
                $(e).attr("type", "hidden");
            });
            $('.econt-info select').each(function (i, e) {
                $(e).attr("hidden", false);
                $(e).attr("required", true);
            });
        }
        else {
            $('.shipping-info').show();
            $('.econt-info').hide();
            $('.shipping-info input').each(function (i, e) {
                $(e).attr("type", "text");
            });
            $('.econt-info select').each(function (i, e) {
                $(e).attr("hidden", true);
                $(e).attr("required", false);
            });
        }
    });

    $("#citySelection").change(function () {
        var citySelected = $($('#citySelection')[0]).find(":selected");

        if (citySelected.text().length) {
            var cityId = $(citySelected[0]).val();

            $('#officeSelection').val("");
            $('#officeSelection option').removeClass("d-none");
            $('#officeSelection option').filter(":not([data-cityId='" + cityId + "'])").addClass("d-none");

            $("#officeSelection").attr("disabled", false);
        }
        else {
            $('#officeSelection').val("");
            $("#officeSelection").attr("disabled", true);
        }
    });

    $(".submit-order-btn").on("click", function (e) {
        if ($("#regularFalse")[0].checked === true) {
            var city = $("input[name='OrderAddress.Country']").val();
            var address = $("input[name='OrderAddress.Address']").val();
            var otherAddress = $("input[name='OrderAddress.Address2']").val();

            $.ajax({
                type: "GET",
                url: "/Econt/ValidateAddress",
                data: { 'city': city, 'address': address, 'otherAddress': otherAddress },
                success: function (res) {
                    var econtError = $("#econtAddressError");
                    if (res.addressValid) {
                        econtError.text("");
                        econtError.removeClass("m-1");
                        $("form[name='orderForm']").submit();
                    }
                    else {
                        econtError.text("Въведеният адрес е невалиден, моля въведете правилен адрес.");
                        econtError.addClass("m-1");
                        location.href = "#address_section";
                    }
                }
            });
        }
        $("form[name='orderForm']").submit();
    });
});