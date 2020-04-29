$(document).ready(function () {
    $('.address-inputs').hide();

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
});