// Images

var ImageSelectClick = function () {
   
    $('#upload').trigger('click');

}

var LoadImageList = function (imageId, imgUrl) {

    var CopyHTML = $("#CopyHTML").html();
   
    var NewHTML = CopyHTML;
    NewHTML = NewHTML.replace("_imageId", imageId);
    NewHTML = NewHTML.replace("_imgUrl", imgUrl);
    NewHTML = NewHTML.replace("_ImageId", imageId);
    NewHTML = NewHTML.replace("'_ImageId'", "'" + imageId + "'");
   
    NewHTML = NewHTML.replace("src_ImageId", "src_" + imageId);
 
   /* NewHTML = NewHTML.replace("imageIdbtn", imageId);*/

   
    $("#divImageList").append(NewHTML);

   

   
   
}

var w = $("#lblImageSizeWidth").html();
var h = $("#lblImageSizeHeight").html();

var vwidth = ImageSizeWidth;
var vheight = ImageSizeHeight;

var bwidth = ImageSizeWidth;
var bheight = ImageSizeHeight;

var c;

var CreateCroppie = function (resize) {
    c = new Croppie(document.getElementById('upload-img'), {
        viewport: { width: vwidth, height: vheight, type: 'square' },
        boundary: { width: bwidth, height: bheight },
        enableExif: true,
        enforceBoundary: true,
        enableResize: resize,
        enableOrientation: true
    })
};

$('#upload').on('change', function () {
   
    var reader = new FileReader();

    reader.onload = function (e) {
        
        $("#modal_ImageCrop").modal('show');

        $("#divProgress").show();
        $("#btnThumbnail").hide();
        $("#divControl").hide();
        $("#bSave").show();
        setTimeout(function () {

            if (c != null) {
                c.destroy();
            }

            if ($("#divImageList > div").length == 0) {
                CreateCroppie(false);
                $("#lblThumb").show();

            }
            else {
                CreateCroppie(true);
                $("#lblThumb").hide();
            }


            c.bind({
                url: e.target.result
            }).then(function () {
                console.log('jQuery bind complete');
                $("#divProgress").hide();
                $("#divControl").show();
            });

        }, 500);



    }
    reader.readAsDataURL(this.files[0]);

});

$('.upload-result').on('click', function (ev) {

    var UploadWidth = bwidth * 2;
    var UploadHeight = bheight * 2;

    c.result({
        type: 'base64',
        size: { width: UploadWidth, UploadHeight: UploadHeight }
    }).then(function (resp) {

        if ($('#txtRefID').length) {
            var url = UserImgAddUrl;
            var RefID = $("#txtRefID").val();
           

            $("#divProgress").show();
            $("#divControl").hide();
           
            setTimeout(function () {
                $.ajax({
                    url: url,
                    type: "POST",
                    data: { "base64image": resp, "RefID": RefID },
                    //data: resp,
                    success: function (data) {

                        if (data.errorMessage != "") {
                            alert("Upload error : " + data.errorMessage);
                        }

                        LoadImageList(data.imageID, data.imgUrl);

                        if ($("#divImageList > div").length >= 6) {
                            $("#divUpload").fadeOut(500, "swing");
                        }

                        $("#modal_ImageCrop").modal("hide");
                    }
                });
            }, 500);
        }
        else {
            var url = UserImgSaveUrl;

            $("#divProgress").show();
            $("#divControl").hide();

            setTimeout(function () {
                $.ajax({
                    url: url,
                    type: "POST",
                    data: { "base64image": resp },
                    //data: resp,
                    success: function (data) {
                        
                        if (data.errorMessage != "") {
                            alert("Upload error : " + data.errorMessage);
                        }

                        LoadImageList(data.imageID, data.imgUrl);

                        if ($("#divImageList > div").length >= 6) {
                            $("#divUpload").fadeOut(500, "swing");
                        }

                        $("#modal_ImageCrop").modal("hide");
                    }
                });
            }, 500);

        }
    });
});

$("#bRotateLeft").click(function () {
    c.rotate(parseInt($(this).data('rotate')));
});

$("#bRotateRight").click(function () {
    c.rotate(parseInt($(this).data('rotate')));
});

var DeleteImage = function (ImageId) {

    var url = UserImgDeleteUrl

    $.ajax({
        url: url,
        type: "POST",
        data: { "ImageID": ImageId },
        //data: resp,
        success: function (data) {

            if (data.ErrorMessage != undefined) {
                alert("Delete error : " + data.ErrorMessage);
            }

            $("#img" + ImageId).remove();

            $("#divUpload").fadeIn(500, "swing");

        }
    });
}


//Thumbnail Image

var ThumbnailImageSelectClick = function () {
    

    if ($("#divImageList > div").length == 0) {
        $('#upload').trigger('click');
    }
    else {
        
        $('#uploadThumbnail').trigger('click');
    }

}

$('#uploadThumbnail').on('change', function (ev) {
    var reader = new FileReader();

    reader.onload = function (e) {

        $("#modal_ImageCrop").modal('show');

        $("#divProgress").show();
        $("#btnThumbnail").show();
        $("#divControl").hide();
        $("#bSave").hide();
        setTimeout(function () {

            if (c != null) {
                c.destroy();
            }

            CreateCroppie(false);
            $("#lblThumb").show();

           


            c.bind({
                url: e.target.result
            }).then(function () {
                console.log('jQuery bind complete');
                $("#divProgress").hide();
                $("#divControl").show();
            });

        }, 500);



    }
    reader.readAsDataURL(this.files[0]);
  

});


$('.uploadThumbnail-result').on('click', function (ev) {

    var UploadWidth = bwidth * 2;
    var UploadHeight = bheight * 2;
    var ImageID = $('#divImageList > div:first-child').attr('data-id');
    c.result({
        type: 'base64',
        size: { width: UploadWidth, UploadHeight: UploadHeight }
    }).then(function (resp) {

        if ($('#txtRefID').length) {
            var url = UserImgAddUrlUpdate;
            var RefID = $("#txtRefID").val();


            $("#divProgress").show();
            $("#divControl").hide();

            setTimeout(function () {
                $.ajax({
                    url: url,
                    type: "POST",
                    data: { "base64image": resp, "RefID": RefID, "ImageID": ImageID },
                    //data: resp,
                    success: function (data) {

                        if (data.errorMessage != "") {
                            alert("Upload error : " + data.errorMessage);
                        }
                        $("#src_" + ImageID).removeAttr("src").attr("src", "/Uploads/" + ImageID + ".jpg" + `?v=${new Date().getTime()}`);
                       

                        if ($("#divImageList > div").length >= 6) {
                            $("#divUpload").fadeOut(500, "swing");
                        }

                        $("#modal_ImageCrop").modal("hide");
                    }
                });
            }, 500);
        }
        else {
            var url = UserImgSaveUrlUpdate;

            $("#divProgress").show();
            $("#divControl").hide();

            setTimeout(function () {
                $.ajax({
                    url: url,
                    type: "POST",
                    data: { "base64image": resp, "ImageID": ImageID },
                    //data: resp,
                    success: function (data) {

                        if (data.errorMessage != "") {
                            alert("Upload error : " + data.errorMessage);
                        }
                        $("#src_" + ImageID).removeAttr("src").attr("src", "/Uploads/" + ImageID + ".jpg" + `?v=${new Date().getTime()}`);
                       
                        if ($("#divImageList > div").length >= 6) {
                            $("#divUpload").fadeOut(500, "swing");
                        }

                        $("#modal_ImageCrop").modal("hide");
                    }
                });
            }, 500);

        }
    });
});




