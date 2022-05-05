function GetHtml(url, obj) {
    return $.ajax({
        url: url,
        cache: false,
        method: "GET",
        data: obj,
        datatype: "html",
        contentType: "application/html; charset=utf-8"
    });
}

function OpenModalOnClick() {
    $('body').on('click', '.modal-link', function (event) {
        event.preventDefault();

        $('#modal-container').children('.modal-dialog').children('.modal-content').remove();

        GetHtml($(this).data("targeturl")).done(function (data) {
            $('#modal-container').children('.modal-dialog').html(data);
            $('#modal-container').modal("show");
        });
    });
}

// Contact creation or edit

function PhoneNumberMask() {
    $('#PhoneNumber').change(function (event) {
        if ($(this).val() == undefined || $(this).val() == "") {
            PhoneNumberMaskCleanRule();
        }
    });

    $('#PhoneNumber').on('paste', function (event) {
        if ($(this).val() == undefined || $(this).val() == "") {
            PhoneNumberMaskCleanRule();
        }
        else {
            PhoneNumberMaskRule($(this).val());
        }
    });

    $('#PhoneNumber').blur(function (event) {
        if ($(this).val() == undefined || $(this).val() == "") {
            PhoneNumberMaskCleanRule();
        }
        else {
            PhoneNumberMaskRule($(this).val());
        }
    });
}

function PhoneNumberMaskRule(phoneNumber) {

    let isCellphone = /^([0-9]{2}9)/.test(phoneNumber);
    let isCellphoneMask = /^(\([0-9]{2}\)9)/.test(phoneNumber);

    if (isCellphone || isCellphoneMask) {
        return $('#PhoneNumber').mask('(00)00000-0000');
    }

    return $('#PhoneNumber').mask('(00)0000-0000');
}

function PhoneNumberMaskCleanRule() {
    $('#PhoneNumber').unmask();
}