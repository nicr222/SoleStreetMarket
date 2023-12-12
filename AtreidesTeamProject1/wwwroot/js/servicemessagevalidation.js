
document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('.php-email-form');

    form.addEventListener('submit', function (e) {
        e.preventDefault();

        let isValid = true;

        // Name validation
        const name = document.getElementById('name');
        const namePattern = /^[a-zA-Z\s]+$/; // This regex allows only alphabets and spaces
        if (name.value.trim().length < 2 || !namePattern.test(name.value.trim())) {
            isValid = false;
            alert('Name must be at least 2 characters long and should not contain numbers or special characters.');
        }

        // Email validation
        const email = document.getElementById('email');
        const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
        if (!emailPattern.test(email.value.trim())) {
            isValid = false;
            alert('Please enter a valid email address.');
        }


        if (isValid) {
            form.submit();
        }
    });
});
