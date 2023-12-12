document.addEventListener('DOMContentLoaded', function () {
    const profileForm = document.querySelector('#profile-edit form');
    const nameInput = profileForm.querySelector('input[name="Name"]');
    const emailInput = profileForm.querySelector('input[name="Email"]');
    const phoneInput = profileForm.querySelector('input[name="Phone"]');

    profileForm.addEventListener('submit', function (event) {
        clearErrors(profileForm);

        let isValid = validateForm();

        if (!isValid) {
            event.preventDefault();
        }
    });

    // Real-time validation
    nameInput.addEventListener('input', function () {
        validateName(nameInput);
    });

    emailInput.addEventListener('input', function () {
        validateEmail(emailInput);
    });

    phoneInput.addEventListener('input', function () {
        validatePhone(phoneInput);
    });

    function validateForm() {
        let isNameValid = validateName(nameInput);
        let isEmailValid = validateEmail(emailInput);
        let isPhoneValid = validatePhone(phoneInput);

        return isNameValid && isEmailValid && isPhoneValid;
    }

    function validateName(inputElement) {
        if (/\d/.test(inputElement.value)) {
            displayError(inputElement, "Name should not contain numbers.");
            return false;
        } else {
            displayValid(inputElement);
            return true;
        }
    }

    function validateEmail(inputElement) {
        if (!/@/.test(inputElement.value)) {
            displayError(inputElement, "Email should contain '@'.");
            return false;
        } else {
            displayValid(inputElement);
            return true;
        }
    }

    function validatePhone(inputElement) {
        if (!/^\d{10,15}$/.test(inputElement.value)) {
            displayError(inputElement, "Phone should be a number and contain between 10 to 15 digits.");
            return false;
        } else {
            displayValid(inputElement);
            return true;
        }
    }

    function displayError(inputElement, message) {
        clearError(inputElement); // Clear any prior error or valid state
        inputElement.classList.add('invalid');
        const errorDiv = document.createElement('div');
        errorDiv.className = 'error-message';
        errorDiv.innerHTML = '<span>!</span> ' + message;
        inputElement.parentElement.appendChild(errorDiv);
    }

    function displayValid(inputElement) {
        clearError(inputElement); // Clear any prior error or valid state
        inputElement.classList.add('valid');
    }

    function clearError(inputElement) {
        inputElement.classList.remove('invalid', 'valid');
        const errorDiv = inputElement.parentElement.querySelector('.error-message');
        if (errorDiv) {
            errorDiv.remove();
        }
    }

    function clearErrors(form) {
        const inputs = form.querySelectorAll('input, select');
        inputs.forEach(clearError);
    }
});

