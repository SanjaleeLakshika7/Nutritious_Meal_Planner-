var BrandSelectClick = function () {
    BrandSelect();
}

var BrandSelect = function () {
    $("#modal_Select").modal('show');
    $('#spnLoadSelect').hide();
    $("#txtSearchSelect").val('');
    LoadBrandData();
}

var LoadBrandData = function () {
    $('#spnLoad').hide();

    var AppendDIV = "divSelectRecord";
    $('#' + AppendDIV).css('visibility', 'hidden');
    $('#divLoadSelect').show();
    $('#spnLoadSelect').show();

    setTimeout(function () {
        $.ajax({

            type: "GET",
            url: '/Catelog/Brand/_BrandSelect',
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

var BrandSelected = function (selectedID, selectedName) {

    $('#btnBrandName').html(selectedName);

    $('#txtBrandID').val(selectedID);
    $('#txtBrandName').val(selectedName);

    $("#modal_Select").modal('hide');
}

var BrandClear = function () {

    $('#btnBrandName').html("(No brand)");

    $('#txtBrandID').val('');
    $('#txtBrandName').val("(No brand)");
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