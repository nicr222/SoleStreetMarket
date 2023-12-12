
function PaymentInfo() {
    var message;
    //This does an on input validation for the current input box selected
    $('input, textarea, select').on('input', function () {
        validateField($(this));
    });

    let isValid = true;

    //This validates the entire form
    $('input, textarea, select').each(function () {
        if (!validateField($(this))) {
            isValid = false;
        }
    });
    if (!isValid) {
        event.preventDefault(); // Prevent form submission if there are errors
    }
}

//main portion for the validaion
function validateField(field) {
    let value = field.val().trim();
    let isValid = true;
    switch (field.attr('name')) {
        case 'first-name':
            if (TestNull(value) == false) {
                isValid = false;
                message = "Field must not be left blank";
            } else if (!LettersOnly(value)) {
                isValid = false;
                message = "Field must not contain numbers";
            } else if (length(value.length) == false) {
                isValid = false;
                message = "Field must not be greater than 70 characters";
            }
            toggleErrorMessage(field, !isValid, message);
            break;
        case 'last-name':
            if (TestNull(value) == false) {
                isValid = false;
                message = "Field must not be left blank";
            } else if (!LettersOnly(value)) {
                isValid = false;
                message = "Field must not contain numbers";
            } else if (length(value.length) == false) {
                isValid = false;
                message = "Field must not be greater than 70 characters";
            }
            toggleErrorMessage(field, !isValid, message);
            break;
        case 'number':
            if (TestNull(value) == false) {
                isValid = false;
                message = "Field must not be left blank";
            } else if (!NumbersOnly(value)) {
                isValid = false;
                message = "Field must only contain numbers";
            } else if (value.length != 16) {
                isValid = false;
                message = "Field must have 16 numbers";
            }
            toggleErrorMessage(field, !isValid, message);
            break;
        case 'expiry':
            if (TestNull(value) == false) {
                isValid = false;
                message = "Field must not be left blank";
            } else if (!validateCreditCardExpiration(value)) {
                isValid = false;
                message = "Field must only contain numbers";
            }
            toggleErrorMessage(field, !isValid, message);
            break;
        case 'cvc':
            if (!validateCvc(value) || TestNull(value) == false) {
                isValid = false;
                message = "Field must be valid CVC and must not be left blank";
            }
            toggleErrorMessage(field, !isValid, message);
            break;
        case 'streetaddress':
            if (TestNull(value) == false) {
                isValid = false;
                message = "Field must not be left blank";
            } else if (!validateStreetAddress(value)) {
                isValid = false;
                message = "Field must be a valid street address";
            }
            toggleErrorMessage(field, !isValid, message);
            break;
        case 'city':
            if (TestNull(value) == false) {
                isValid = false;
                message = "Field must not be left blank";
            } else if (!LettersOnly(value)) {
                isValid = false;
                message = "Field must only contain letters";
            }
            toggleErrorMessage(field, !isValid, message);
            break;
        case 'state':
            if (TestNull(value) == false) {
                isValid = false;
                message = "Field must not be left blank";
            }
            toggleErrorMessage(field, !isValid, message);
            break;
        case 'zipcode':
            if (TestNull(value) == false) {
                isValid = false;
                message = "Field must not be left blank";
            } else if (!validateZIPCode(value)) {
                isValid = false;
                message = "Field must be a valid zip code";
            }
            toggleErrorMessage(field, !isValid, message);
            break;
        case 'email':
            if (TestNull(value) == false) {
                isValid = false;
                message = "Field must not be left blank";
            } else if (!validateEmail(value)) {
                isValid = false;
                message = "Field must be a valid email";
            }
            toggleErrorMessage(field, !isValid, message);
            break;
        case 'phone':
            if (TestNull(value) == false) {
                isValid = false;
                message = "Field must not be left blank";
            } else if (!validatePhoneNumber(value)) {
                isValid = false;
                message = "Field must be a valid phone number";
            }
            toggleErrorMessage(field, !isValid, message);
            break;
    }
    //adds or removes a class that will display a red border if it is invalid
    if (isValid) {
        field.removeClass('is-invalid');
    } else {
        field.addClass('is-invalid');
    }
    return isValid;
}
//displaying a message to explain why your input is invalid if it is
function toggleErrorMessage(field, show, message) {
    const errorDiv = field.next('.invalid-feedback');
    if (show) {
        errorDiv.text(message);
    } else {
        errorDiv.text('');
    }
}

//functions to validate the form.
//Letters only
function LettersOnly(input) {
    return /^[A-Za-z]+$/.test(input);
}
//Numbers only
function NumbersOnly(input) {
    return /^[0-9]+$/.test(input);
}
//Date format
function DateFormat(input) {
    return /^\d{2}\/\d{2}\/\d{4}$/.test(input);
}
//Length
function length(input) {
    if (input > 70) {
        return false;
    } else {
        return true;
    }
}
//Tests nulls
function TestNull(input) {
    if (input == null || input == "") {
        return false;
    } else {
        return true;
    }
}
//Experation date format for credit card
function validateCreditCardExpiration(expirationDate) {
    return /^(0[1-9]|1[0-2])\/\d{2}$/.test(expirationDate) &&
        isFutureExpiration(expirationDate);
}
//Experation date future date for credit card
function isFutureExpiration(expirationDate) {
    const [inputMonth, inputYear] = expirationDate.split('/').map(part => parseInt(part, 10));
    const currentYear = new Date().getFullYear() % 100;
    const currentMonth = new Date().getMonth() + 1;

    if (inputYear < currentYear) {
        return false;
    }

    if (inputYear === currentYear && inputMonth < currentMonth) {
        return false;
    }

    return true;
}
//CVC
function validateCvc(cvc) {
    return /^\d{3,4}$/.test(cvc);
}
// Street Address (Example: 123 Main St.)function validateStreetAddress(address) 
function validateStreetAddress(address) {
    return /^[A-Za-z0-9\s\.\-\#]+$/.test(address);
}
// ZIP Code (Assuming U.S. ZIP Code format)
function validateZIPCode(zipCode) {
    return /^\d{5}(-\d{4})?$/.test(zipCode);
}
// Email
function validateEmail(email) {
    return /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(email);
}
// Phone Number (Assuming U.S. phone number format)
function validatePhoneNumber(phone) {
    return /^\d{10}$/.test(phone.replace(/\D/g, ''));
}