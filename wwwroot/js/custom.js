// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function LoadTopCart(withShake) {

    var AppendDIV = "divTopCart";
    $('#' + AppendDIV).css('visibility', 'hidden');

    setTimeout(function () {
        $.ajax({

            type: "GET",
            url: '/Shop/_Cart',
            success: function (result) {
                $('#' + AppendDIV).html(result);
                $('#' + AppendDIV).css('visibility', 'visible');
                if (withShake == true) {
                    $('#' + AppendDIV).effect("shake", { times: 1 }, 200);
                }
            },
            error: function (result) {
                alert(result.responseText);
            }
        })
    }, 200);
}

$(".btn-cart").on("click", function () {

    var ItemID = $(this).data("itemid");
    var ItemPrice = $(this).attr("data-price");
    var ItemQty = $(this).data("qty");
    var ItemSizeName = "";
    ItemSizeName = $(this).attr("data-ItemSizeName");
   
   
    if ($(".quantity").length > 0) {
        var ItemQty = $(".quantity").val();
    }

    $.ajax({

        type: "POST",
        url: CartAddURL,
        data: { ItemID: ItemID, ItemPrice: ItemPrice, ItemQty: ItemQty, Remarks: ItemSizeName },
        success: function (result) {
            if (result != null && result.success) {

                LoadTopCart(true);

            } else {
                ReportError(result.responseText);
            }

        },
        error: function (result) {
            ReportError(result.responseText);
        }
    })
});

function UpdateCart() {
    var Error = "";

    $('.quantity').each(function () {
        var ItemID = $(this).data("itemid");
        var ItemQty = $(this).val();
        var ItemSizeName = "";
        ItemSizeName = $(this).attr("data-ItemSizeName");
       
        $.ajax({

            type: "POST",
            url: CartUpdateURL,
            async: false,
            data: { ItemID: ItemID, ItemQty: ItemQty, Remarks: ItemSizeName },
            success: function (result) {
                if (result != null && result.success) {

                    // Update Cart View

                } else {
                    Error = result.responseText;
                }

            },
            error: function (result) {
                Error = result.responseText;
            }
        })


    })

    if (Error != "") {
        alert(Error);
    }
    else {
        location.href = CartURL;
    }
}

function RemoveCart(ItemID, Remarks) {
    var Error = "";
    
    $.ajax({

        type: "POST",
        url: CartRemoveURL,
        async: false,
        data: { ItemID: ItemID, Remarks: Remarks},
        success: function (result) {
            if (result != null && result.success) {

                // Update Cart View

            } else {
                Error = result.responseText;
            }

        },
        error: function (result) {
            Error = result.responseText;
        }
    })

    if (Error != "") {
        alert(Error);
    }
    else {
        location.href = CartURL;
    }
}

function RemoveCartTop(ItemID, Remarks) {
   
    $.ajax({

        type: "POST",
        url: CartRemoveURL,
        async: false,
        data: { ItemID: ItemID,Remarks: Remarks },
        success: function (result) {
            if (result != null && result.success) {

                LoadTopCart();

            } else {
                alert(result.responseText);
            }

        },
        error: function (result) {
            alert(result.responseText);
        }
    })

}

