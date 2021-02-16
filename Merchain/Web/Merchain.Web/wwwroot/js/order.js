$(document).ready(function () {
    $("input[name='UseRegularAddress']").on("click", function () {
        $el = $(this);

        if ($el.val() === 'true') {
            $('.address-inputs').hide();
            $('.address-inputs input').each(function (i, e) {
                $(e).attr("type", "hidden");
            });

        }
        else {
            $('.address-inputs').show();
            $('.address-inputs input').each(function (i, e) {
                $(e).attr("type", "text");
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