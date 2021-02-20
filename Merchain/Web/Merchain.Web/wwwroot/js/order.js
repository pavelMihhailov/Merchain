$(document).ready(function () {
    $('.shipping-info').hide();
    $('.shipping-info input').each(function (i, e) {
        $(e).attr("type", "hidden");
    });

    $("input[name='ShipToOffice']").on("click", function () {
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
});