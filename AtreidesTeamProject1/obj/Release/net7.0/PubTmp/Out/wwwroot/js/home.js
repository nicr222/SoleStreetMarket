document.addEventListener("DOMContentLoaded", function () {
    // Select the carousel element by its ID
    var homeCarousel = document.getElementById("homeCarousel");

    if (homeCarousel) {
        // Initialize the Bootstrap carousel
        var carousel = new bootstrap.Carousel(homeCarousel, {
            interval: 5000,  // Time for each slide, 5 seconds
            pause: "hover",  // Pauses the carousel when hovered
            wrap: true,      // Carousel continues sliding when reaching the end
            keyboard: true,  // Use keyboard keys to navigate the carousel
            touch: true      // Enables swipe gestures for touch devices
        });
    }
});


/**
     * Brief summary of this code
     */
// The script filter items in the container using Isotope Library(allows items to be filtered, sorted, and rearranged smoothly with animations).
// When the filter button is clicked, it rearranges the items and update its animations.

(function () {
    "use strict";
    /**
      * Trend isotope and filter
      */
    // Listens for the 'load' event on the window.
    window.addEventListener('load', () => {
        let portfolioContainer = document.querySelector('.portfolio-container');
        if (portfolioContainer) {

            //// Initializes the Isotope layout library on the container, targeting 'item' as the items to layout.
            let portfolioIsotope = new Isotope(portfolioContainer, {
                itemSelector: '.portfolio-item',
            });

            let portfolioFilters = document.querySelectorAll('#portfolio-flters li');

            portfolioFilters.forEach(filter => {
                filter.addEventListener('click', function (e) {
                    e.preventDefault();

                    // Remove 'filter-active' class from all filters
                    portfolioFilters.forEach(el => {
                        el.classList.remove('filter-active');
                    });

                    // Add 'filter-active' class to the clicked filter
                    this.classList.add('filter-active');

                    // Arrange the isotope grid
                    portfolioIsotope.arrange({
                        filter: this.getAttribute('data-filter')
                    });

                    // Listens for the 'arrangeComplete' event
                    portfolioIsotope.on('arrangeComplete', function () {
                        // Refreshes the AOS (Animate On Scroll) library. This updates any animations on newly visible items after filtering.
                        AOS.refresh();
                    });
                });
            });
        }
    });
})()
