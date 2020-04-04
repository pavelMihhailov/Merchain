$(document).ready(function () {
    $('.address-inputs').hide();

    $("input[name='UseRegularAddress']").on("click", function () {
        $el = $(this);

        if ($el.val() === 'true') {
            $('.address-inputs').hide();
        }
        else {
            $('.address-inputs').show();
        }
    });

    //$("form[name='orderForm']").submit(function (event) {
    //    let isRegular = $('#regularTrue').is(':checked');

    //    if (!isRegular) {
    //        $("form[name='orderForm']").validate({
    //            rules: {
    //                Address: "required",
    //                Country: "required",
    //                ZipCode: "required",
    //                Phone: "required"
    //            },
    //            messages: {
    //                Address: "Please enter this required field",
    //                Country: "Please enter this required field",
    //                ZipCode: "Please enter this required field",
    //                Phone: "Please enter this required field"
    //            },
    //            submitHandler: function (form) {
    //                form.submit();
    //            }
    //        });
    //    }

    //    return true;
    //});
});