document.addEventListener('DOMContentLoaded', function () {

    // Grab all forms with needs-validation
    let forms = document.querySelectorAll('.needs-validation');

    forms.forEach(form => {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');
        });
    });

    // Custom validation for name in registration form
    let nameInput = document.getElementById('yourName'); 
    if (nameInput) {
        nameInput.addEventListener('input', function () {
            let namePattern = /^[a-zA-Z\s]*$/;
            let valid = namePattern.test(nameInput.value);
            if (valid) {
                nameInput.setCustomValidity("");
            } else {
                nameInput.setCustomValidity("Name should only contain letters and spaces.");
            }
        });
    }

    // Custom validation for phone number in registration form
    let phoneInput = document.getElementById('yourPhone');
    if (phoneInput) {
        phoneInput.addEventListener('input', function () {
            let phoneNumberPattern = /^[0-9]{10,15}$/;
            let valid = phoneNumberPattern.test(phoneInput.value);
            if (valid) {
                phoneInput.setCustomValidity("");
            } else {
                phoneInput.setCustomValidity("Invalid Phone Number. Please enter 10 to 15 numeric characters.");
            }
        });
    }

    // Custom validation for email
    let emailInput = document.getElementById('yourEmail');
    if (emailInput) {
        emailInput.addEventListener('input', function () {
            let emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
            let valid = emailPattern.test(emailInput.value);
            if (valid) {
                emailInput.setCustomValidity("");
            } else {
                emailInput.setCustomValidity("Invalid Email Address.");
            }
        });
    }

    // Custom validation for password length in registration form
    let passwordInput = document.getElementById('yourPassword');
    if (passwordInput) {
        passwordInput.addEventListener('input', function () {
            if (passwordInput.value.length < 8) {
                passwordInput.setCustomValidity("Password should be at least 8 characters long.");
            } else {
                passwordInput.setCustomValidity("");
            }
        });
    }

    // Hide the success message after 3 seconds
    let successAlert = document.querySelector('.alert-success');
    if (successAlert) {
        setTimeout(function () {
            successAlert.style.display = 'none';
        }, 3000);
    }

});

document.addEventListener('DOMContentLoaded', function () {
    setTimeout(function () {
        let alertBox = document.querySelector('.alert-danger');
        if (alertBox) {
            alertBox.style.display = 'none';
        }
    }, 4000); // 4000 milliseconds = 4 seconds
});


