// Tabs
function openCategory(evt, CategoryName) {
    var i;
    // Hide all elements with class "category"
    var x = document.getElementsByClassName("category");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }

    // Remove "w3-theme-dark" class from all elements with class "testbtn"
    var activebtn = document.getElementsByClassName("testbtn");
    for (i = 0; i < x.length; i++) {
        activebtn[i].className = activebtn[i].className.replace(" w3-theme-dark", "");
    } 

    // Display the element with ID = CategoryName
    document.getElementById(CategoryName).style.display = "block";

    // Add "w3-theme-dark" class to the clicked element
    evt.currentTarget.className += " w3-theme-dark";
}

// Simulate click on the first element with class "testbtn"
var mybtn = document.getElementsByClassName("testbtn")[0];
mybtn.click();

// The following code handles an accordion functionality
if ($(".accordion__item__header").length > 0) {
    var active = "active";

    // Toggle the 'active' class and slide toggle the next div on header click
    $(".accordion__item__header").click(function () {
        $(this).toggleClass(active);
        $(this).next("div").slideToggle(200);
    });
}