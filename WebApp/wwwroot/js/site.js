
// Contact creation or edit

// Reference: https://pt.stackoverflow.com/questions/42238/m%C3%A1scara-de-telefones-usando-jquery-mask-plugin
function PhoneNumberMask() {
    $('#PhoneNumber').change(function (event) {
        PhoneNumberMaskRule($(this).val());
    });
}

function PhoneNumberMaskRule(phoneNumber) {
    if (phoneNumber.length == 15) {
        return $('#PhoneNumber').mask('(00)00000-0000');
    }
    else {
        return $('#PhoneNumber').mask('(00)0000-0000');
    }
}