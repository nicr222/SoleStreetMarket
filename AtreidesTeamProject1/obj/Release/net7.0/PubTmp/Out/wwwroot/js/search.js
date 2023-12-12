// Department Search
document.getElementById('searchDept').addEventListener('input', function () {
    var input, filter, table, tbody, tr, td, i, txtValue;
    input = document.getElementById('searchDept');
    filter = input.value.toUpperCase();
    table = document.getElementById('deptTable');
    tbody = table.getElementsByTagName('tbody')[0];
    tr = tbody.getElementsByTagName('tr');

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName('td')[0]; // Adjust the index for the column you want to search (0 for the first column)
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = '';
            } else {
                tr[i].style.display = 'none';
            }
        }
    }
});

// Product Search
document.getElementById('searchProduct').addEventListener('input', function () {
    var input, filter, table, tbody, tr, td, i, txtValue;
    input = document.getElementById('searchProduct');
    filter = input.value.toUpperCase();
    table = document.getElementById('productTable');
    tbody = table.getElementsByTagName('tbody')[0];
    tr = tbody.getElementsByTagName('tr');

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName('td')[0]; // Adjust the index for the column you want to search (0 for the first column)
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = '';
            } else {
                tr[i].style.display = 'none';
            }
        }
    }
});

// Employee Searchf
document.getElementById('searchEmp').addEventListener('input', function () {
    var input, filter, table, tbody, tr, td, i, txtValue;
    input = document.getElementById('searchEmp');
    filter = input.value.toUpperCase();
    table = document.getElementById('empTable');
    tbody = table.getElementsByTagName('tbody')[0];
    tr = tbody.getElementsByTagName('tr');

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName('td')[0]; // Adjust the index for the column you want to search (0 for the first column)
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = '';
            } else {
                tr[i].style.display = 'none';
            }
        }
    }
});