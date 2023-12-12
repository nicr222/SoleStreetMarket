document.addEventListener("DOMContentLoaded", function () {
    // Success Message
    var successAlertElement = document.getElementById('SuccessMessage');
    if (successAlertElement) {
        setTimeout(function () {
            successAlertElement.style.display = 'none';
        }, 4000);  // 4000 milliseconds = 4 seconds
    }

    // Error Message
    var errorAlertElement = document.getElementById('ErrorMessage');
    if (errorAlertElement) {
        setTimeout(function () {
            errorAlertElement.style.display = 'none';
        }, 4000);  // 4000 milliseconds = 4 seconds
    }
});
