document.addEventListener("DOMContentLoaded", function () {
    var tabs = document.querySelectorAll('.custom-nav-link');

    tabs.forEach(function (tab) {
        tab.addEventListener('click', function (event) {
            event.preventDefault();

            tabs.forEach(function (item) {
                item.classList.remove('active', 'show');
            });

            this.classList.add('active', 'show');

            var activeTab = this.getAttribute('href');
            var contentPanes = document.querySelectorAll('.tab-pane');

            contentPanes.forEach(function (pane) {
                pane.classList.remove('active', 'show');
            });

            document.querySelector(activeTab).classList.add('active', 'show');
        });
    });
});
