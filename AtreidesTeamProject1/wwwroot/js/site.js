// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

<script>
    $(document).ready(function () {
        $('#contactDropdown').on('click', function () {
            $('#contactDropdownMenu').slideToggle('slow'); // You can use .fadeIn() or .fadeOut() for a fade effect
        });
    });
</script>
