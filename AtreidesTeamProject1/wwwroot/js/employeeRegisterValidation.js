document.addEventListener("DOMContentLoaded", function () {
    var form = document.querySelector('form.needs-validation');

    // Bind the validation functions to the input event of each field
    document.getElementById("name").addEventListener('input', validateName);
    document.getElementById("email").addEventListener('input', validateEmail);
    document.getElementById("phone").addEventListener('input', validatePhone);
    document.getElementById("username").addEventListener('input', validateUsername);
    document.getElementById("password").addEventListener('input', validatePassword);
    document.querySelector("[name='DepartmentID']").addEventListener('change', validateDepartment); // For dropdowns, use 'change' event
    document.getElementById("date").addEventListener('input', validateDob);

    form.addEventListener('submit', function (event) {
        var isValid = true; // Start with assuming the form is valid
        clearAllErrorMessages(); // First clear all error messages

        if (!validateName()) isValid = false;
        if (!validateEmail()) isValid = false;
        if (!validatePhone()) isValid = false;
        if (!validateUsername()) isValid = false;
        if (!validatePassword()) isValid = false;
        if (!validateDepartment()) isValid = false;
        if (!validateDob()) isValid = false;

        if (!isValid) {
            event.preventDefault(); // Prevent the form from submitting if there's any error
        }
    });

    function validateName() {
        var nameInput = document.getElementById("name");
        var nameRegex = /^[a-zA-Z\s]+$/;
        if (!nameInput.value) {
            showError(nameInput, "Name is required.");
            return false;
        } else if (!nameRegex.test(nameInput.value)) {
            showError(nameInput, "Name can only contain letters.");
            return false;
        } else {
            nameInput.classList.add('valid');
            removeError(nameInput);
            return true;
        }
    }

    function validateEmail() {
        var emailInput = document.getElementById("email");
        if (!emailInput.value || !emailInput.value.includes('@')) {
            showError(emailInput, "Please enter a valid Email address.");
            return false;
        } else {
            emailInput.classList.add('valid');
            removeError(emailInput);
            return true;
        }
    }

    function validatePhone() {
        var phoneInput = document.getElementById("phone");
        var phoneRegex = /^[0-9]+$/;

        if (!phoneInput.value) {
            showError(phoneInput, "Phone Number is required.");
            return false;
        }
        else if (!phoneRegex.test(phoneInput.value)) {
            showError(phoneInput, "Phone Number can only contain numeric characters.");
            return false;
        }
        else if (phoneInput.value.length < 10 || phoneInput.value.length > 15) {
            showError(phoneInput, "Invalid Phone Number. Please enter 10 to 15 numeric characters.");
            return false;
        }
        else {
            phoneInput.classList.add('valid');
            removeError(phoneInput);
            return true;
        }
    }


    function validateUsername() {
        var usernameInput = document.getElementById("username");
        if (!usernameInput.value) {
            showError(usernameInput, "Username is required.");
            return false;
        } else {
            usernameInput.classList.add('valid');
            removeError(usernameInput);
            return true;
        }
    }

    function validatePassword() {
        var passwordInput = document.getElementById("password");
        if (!passwordInput.value || passwordInput.value.length < 8) {
            showError(passwordInput, "Password should be at least 8 characters long.");
            return false;
        } else {
            passwordInput.classList.add('valid');
            removeError(passwordInput);
            return true;
        }
    }

    function validateDepartment() {
        var departmentSelect = document.querySelector("[name='DepartmentID']");
        if (!departmentSelect.value) {
            showError(departmentSelect, "Please, select a department.");
            return false;
        } else {
            departmentSelect.classList.add('valid');
            removeError(departmentSelect);
            return true;
        }
    }

    function validateDob() {
        var dobInput = document.getElementById("date");
        if (!dobInput.value) {
            showError(dobInput, "Please, select a date of birth.");
            return false;
        } else {
            var dob = new Date(dobInput.value);
            var today = new Date();
            var age = today.getFullYear() - dob.getFullYear();

            if (today < new Date(dob.setFullYear(today.getFullYear()))) {
                age = age - 1;
            }

            if (age < 16) {
                showError(dobInput, "Employee must be 16 years or older.");
                return false;
            } else {
                dobInput.classList.add('valid');
                removeError(dobInput);
                return true;
            }
        }
    }


    // Utility function to show the error message below the input
    function showError(inputElem, message) {
        inputElem.classList.remove('valid');
        inputElem.classList.add('invalid');

        // Check if there's already an error message
        var existingErrorElem = inputElem.parentElement.querySelector('.error-message');
        if (existingErrorElem) {
            existingErrorElem.innerHTML = '<span>!</span> ' + message;  // Update the message
        } else {
            // Create a new error element if one doesn't exist
            var errorElem = document.createElement('div');
            errorElem.className = 'error-message';
            errorElem.innerHTML = '<span>!</span> ' + message;
            inputElem.parentElement.appendChild(errorElem);
        }
    }


    // Utility function to remove the error message from a specific input
    function removeError(inputElem) {
        var errorElem = inputElem.parentElement.querySelector('.error-message');
        if (errorElem) {
            errorElem.remove();
        }
    }


    // Utility function to clear all the error messages
    function clearAllErrorMessages() {
        var errorMessages = document.querySelectorAll('.error-message');
        errorMessages.forEach(function (elem) {
            elem.remove();
        });
    }
});
