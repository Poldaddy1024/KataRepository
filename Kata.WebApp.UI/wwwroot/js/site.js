/* ============================================================
 * Index View 
 * ============================================================ */
(function ($) {

    'use strict';

    $(document).ready(function () {              

        // Set multi-data form
        var formToBeSent = new FormData();

        // Set the DropZone object
        var dzTrips = new Dropzone("#formTrips", {
            acceptedFiles: 'text/plain',
            addRemoveLinks: true,
            autoProcessQueue: false,
            timeout: 0,
            url: "#",
            maxFiles: 1,
            maxFilesize: 5,
            dictDefaultMessage: "Drop a file or click here!",
            dictRemoveFile: "Remove",
            maxfilesexceeded: function (file) {
                this.removeAllFiles();
                this.addFile(file);
            },
            autoQueue: false,
            init: function () {
                this.on("addedfile", function (file) {

                    // Ask to restrict just file size of 5 MB
                    if (file.size > this.options.maxFilesize * 1024 * 1024 * 1024 * 1024 * 1024) {

                        // Remove the file
                        this.removeAllFiles();

                        // File removed
                        $('.container').pgNotification({
                            style: 'bar',
                            title: 'Client',
                            message: 'Please, select a file under 5 MB.',
                            position: 'top-right',
                            timeout: 5000,
                            type: 'danger'
                        }).show();

                    } else {

                        // Add the file to the form that is going to be sent in the ajax call
                        formToBeSent.append("TripFile", file);

                        // With this line we inforce to display the check mark
                        return file.previewElement.classList.add("dz-success");
                    }
                });
                this.on("removedfile", function (file) {

                    // Remove the file into the form that is going to be sent in the ajax call
                    formToBeSent.delete(file);
                });
            }
        });


        // ==============================================================
        //                      Upload Trips
        // ==============================================================
        $(document).on("submit", "#formTrips", function (e) {
            
            // Prevent rendering
            e.preventDefault();                      

            // Validate DropZone not empty
            var fileQty = 0;
            fileQty = dzTrips.files.length;

            if (fileQty === 0) {

                // Display message file missing
                $('.container').pgNotification({
                    style: 'bar',
                    title: 'Warning',
                    message: 'Please, select a file. ',
                    position: 'top-right',
                    timeout: 3000,
                    type: 'danger'
                }).show();

                return false;
            }

            // Execute ajax
            $.ajax({
                url: "/Home/UploadTrips",
                type: "POST",
                data: formToBeSent,
                contentType: false,
                processData: false,
                async: true,                
                success: function (result) {

                    // File uploaded successfully
                    $('.container').pgNotification({
                        style: 'bar',
                        title: 'Status',
                        message: 'The file was uploaded successfully.',
                        position: 'top-right',
                        timeout: 3000,
                        type: 'success'
                    }).show();                                        

                    // Get data in place
                    $('#PlaceHolderResult').empty();
                    $('#PlaceHolderResult').html(result);                    
                   
                }
            });                  
        });

    });

})(window.jQuery);