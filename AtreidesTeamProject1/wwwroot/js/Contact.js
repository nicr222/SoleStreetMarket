(function () {
    "use strict";
    /**
      * Swpier function for testimonial section
      */
    new Swiper('.testimonials-slider', {
        speed: 600,
        loop: true,
        autoplay: {
            delay: 5000,
            disableOnInteraction: false
        },
        slidesPerView: 'auto',
        pagination: {
            el: '.swiper-pagination',
            type: 'bullets',
            clickable: true
        },
        breakpoints: {
            320: {
                slidesPerView: 1,
                spaceBetween: 20
            },

            1200: {
                slidesPerView: 3,
                spaceBetween: 20
            }
        }
    });
})()

// Disappear alert message after 4 sec
document.addEventListener("DOMContentLoaded", function () {
    var alertElement = document.getElementById('success-alert');
    if (alertElement) {
        setTimeout(function () {
            alertElement.style.display = 'none';
        }, 4000);  // 4000 milliseconds = 4 seconds
    }
});

// Disappear feedback alert message after 4 sec
document.addEventListener("DOMContentLoaded", function () {
    var feedbackAlertElement = document.getElementById('feedback-success-alert');
    if (feedbackAlertElement) {
        setTimeout(function () {
            feedbackAlertElement.style.display = 'none';
        }, 4000);  // 4000 milliseconds = 4 seconds
    }
});
