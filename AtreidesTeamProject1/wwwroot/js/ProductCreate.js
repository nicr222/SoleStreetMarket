
/*$(document).ready(function () {
    // Attach a submit event handler to the form
    $("form").submit(function (event) {
        // Prevent the form from submitting initially
        event.preventDefault();

        // Validate the form fields
        var isValid = true;

        // Validate TypeID (Dropdown)
        var typeID = $("#TypeID").val();
        if (!typeID) {
            displayValidationMessage("TypeID", "You must select a value.");
            isValid = false;
        } else {
            hideValidationMessage("TypeID");
        }

        // Validate Name
        var name = $("#Name").val();
        if (!name) {
            displayValidationMessage("Name", "You must enter a name.");
            isValid = false;
        } else {
            hideValidationMessage("Name");
        }

        // Validate Description
        var description = $("#Description").val();
        if (!description) {
            displayValidationMessage("Description", "You must enter a description.");
            isValid = false;
        } else {
            hideValidationMessage("Description");
        }

        // Validate DepartmentID (Dropdown)
        var departmentID = $("#DepartmentID").val();
        if (!departmentID) {
            displayValidationMessage("DepartmentID", "You must select a value.");
            isValid = false;
        } else {
            hideValidationMessage("DepartmentID");
        }

        // Validate Price
        var price = $("#Price").val();
        if (!price) {
            displayValidationMessage("Price", "You must enter a price.");
            isValid = false;
        } else {
            hideValidationMessage("Price");
        }

        // Validate URL
        var url = $("#Url").val();
        if (!url) {
            displayValidationMessage("Url", "You must enter an image URL.");
            isValid = false;
        } else {
            hideValidationMessage("Url");
        }

        // If all fields are valid, submit the form
        if (isValid) {
            this.submit();
        }
    });

    // Function to display validation message
    function displayValidationMessage(fieldName, message) {
        $("#" + fieldName).addClass("is-invalid");
        $("#" + fieldName + "-validation").text(message);
    }

    // Function to hide validation message
    function hideValidationMessage(fieldName) {
        $("#" + fieldName).removeClass("is-invalid");
        $("#" + fieldName + "-validation").text("");
    }
});*/