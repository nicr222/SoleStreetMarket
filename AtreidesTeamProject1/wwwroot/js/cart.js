window.onload = Calculate();
function Calculate() {
    // Get all the rows in the table body
    var rows = document.querySelectorAll("tbody tr");
    var total = 0;
    var tax = .05;

    // Iterate through each row
    rows.forEach(function (row) {
        // Get the price and quantity elements in the current row
        var priceElement = row.querySelector("#price");
        var quantityElement = row.querySelector("#quantity");
        var subtotalElement = row.querySelector("#subtotal");

        if (priceElement == null || quantityElement == null) {
            return;
        }
        // Parse price and quantity as numbers
        var price = parseFloat(priceElement.textContent);
        var quantity = parseInt(quantityElement.textContent);

        // Calculate subtotal
        var subtotal = price * quantity;

        // Display the calculated subtotal in the subtotal element
        subtotalElement.textContent = /*"$" +*/ subtotal.toFixed(2); // Assuming you want two decimal places
        total += subtotal;
    });
    tax = total * tax;
    total += tax;

    // Display the total in the total element
    var totalElement = document.getElementById("total");
    var taxElement = document.getElementById("tax");
    taxElement.textContent = /*'$' + */tax.toFixed(2);
    totalElement.textContent = /*"$" +*/ total.toFixed(2); // Assuming you want two decimal places
}

function Discount() {
    var codeValid = false;
    //var total = document.getElementById("total");
    //var subTotalPrice = parseFloat(total.textContent).replace('$', "");
    let total = document.getElementById('total').textContent;
    var totalElement = document.getElementById("total");
    coupon = document.getElementById("discountCode").value;

    // Arrays with coupon codes based on percentages.
    var percent10 = ['10off', '10off2', '10off3'];
    var percent20 = ['20off', '20off2', '20off3'];
    var percent50 = ['50off', '50off2', '50off3'];

    // This area checks to see what category the coupon code used is in.
    if (percent10.includes(coupon)) {
        savings = total * .10;
        codeValid = true;
    }
    else if (percent20.includes(coupon)) {
        savings = total * .20;
        codeValid = true;
    }
    else if (percent50.includes(coupon)) {
        savings = total * .50;
        codeValid = true;
    }
    else {
        alert("Sorry, that code is invalid.")
    }

    // Ensures non-negative total
    if (total < 0) { total = 0 };

    total -= savings;
    totalElement.textContent = /*"$" +*/ total.toFixed(2);

    // Hides the discount code area so a user can't input the coupon more than once.
    /*if (codeValid == true) {
        document.getElementById('codeArea').hidden = true
    }*/
}

//This checks if there is anything in the cart and then either displays message or populates values into the payment form.
function PopulateInfo() {
    let total = document.getElementById('total').textContent;
    // Get the total element
    // Get the checkout link
    const checkoutLink = document.getElementById("checkoutLink");

    // Check if the elements exist
    if (totalElement && checkoutLink) {
        // Get the total value (strip the "$" and convert it to a number)
        const totalValue = parseFloat(totalElement.textContent.replace("$", ""));

        // Update the link's href attribute with the total value
        checkoutLink.href = checkoutLink.getAttribute("href") + "?total=" + totalValue;
    }
/*
    if (total === "0.00") {
        //displaying a message for the user to know that they cant checkout with no items in the cart.
        alert("You can't checkout without having anyting in your cart.");
        window.location.href = window.location.href;
    } else {
        document.getElementById('currency-field').textContent = "$" + total;



        //add more payment info fillers so that if the user is logged in it will take the name and address from the user and display it here.
    }*/
}


