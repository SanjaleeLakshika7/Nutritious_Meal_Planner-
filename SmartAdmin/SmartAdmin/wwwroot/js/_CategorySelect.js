var CategorySelectClick = function () {
    CategorySelect();
}

var CategorySelect = function () {
    $("#modal_Select").modal('show');
    $('#spnLoadSelect').hide();
    $("#txtSearchSelect").val('');
    LoadCategoryData();
}

var LoadCategoryData = function () {
    $('#spnLoad').hide();

    var AppendDIV = "divSelectRecord";
    $('#' + AppendDIV).css('visibility', 'hidden');
    $('#divLoadSelect').show();
    $('#spnLoadSelect').show();

    setTimeout(function () {
        $.ajax({

            type: "GET",
            url: '/Catelog/CategorySub/_CategorySelect',
            data: { SystemItem: "N" },
            success: function (result) {
                $('#' + AppendDIV).html(result);
                $('#divLoadSelect').hide();
                $('#spnLoadSelect').hide();
                $('#' + AppendDIV).css('visibility', 'visible');

            },
            error: function (result) {
                alert(result.responseText);
            }
        })
    }, 500);
}

var CategorySelected = function (selectedID, selectedMainID, selectedName) {

    $('#txtCategorySubID').val(selectedID);
    $('#txtCategoryMainID').val(selectedMainID);
    $('#txtCategoryName').html(selectedName);
    $('#txtCategorySubName').val(selectedName);

    $("#modal_Select").modal('hide');
}

function SearchDatatStart() {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("txtSearchSelect");
    filter = input.value.toUpperCase();
    table = document.getElementById("tblSelectBody");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        td1 = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = td.textContent || td.innerText;
            txtValue = txtValue + ' ' + td1.textContent || td1.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }


}