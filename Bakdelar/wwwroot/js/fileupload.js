$(document).ready(function () {
    $('#btnCreateProduct').on('click', function () {
        var files = $('#fileUpload').prop("files");
        var fdata = new FormData();
        for (var i = 0; i < files.length; i++) {
            fdata.append("files", files[i]);
        }
        if (files.length > 0) {
            $.ajax({
                type: "POST",
                url: "/Index?handler=Upload",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: fdata,
                contentType: false,
                processData: false,
                success: function (response) {
                    alert('File Uploaded Successfully.')
                }
            });
        }
        else {
            alert('Please select a file.')
        }
    })
});