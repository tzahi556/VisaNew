$(function () {
    let inputFile = $('.myInput');
    let button = $('.myButton');
    let buttonSubmit = $('#mySubmitButton');
    let filesContainer = $('#myFiles');
    let files = [];

    inputFile.change(function () {
        
        //alert(this.id);
        var dvContainer = "dv" + this.id;
        inputFile = $('#' + this.id);
        let newFiles = [];
        for (let index = 0; index < inputFile[0].files.length; index++) {
            let file = inputFile[0].files[index];
            newFiles.push(file);
            files.push(file);

           
        }

        newFiles.forEach(file => {
            let fileElement = $("<span class='fileUploadName'>" + file.name + "</span>");//$(`<p style='border:solid 1px red'> ${file.name}</p>`);
            fileElement.data('fileData', file);

           
            $('#' + dvContainer).append(fileElement);
            validateUploadFiles();
            // filesContainer.append(fileElement);

            fileElement.click(function (event) {
                let fileElement = $(event.target);
                let indexToRemove = files.indexOf(fileElement.data('fileData'));
                fileElement.remove();
                files.splice(indexToRemove, 1);
                validateUploadFiles();
            });



        });
    });

    button.click(function () {
      
        var inputId = this.id + "Input";
        $('#' + inputId).click();
        //inputFile.click();

    });

    buttonSubmit.click(function () {

        debugger

        let formData = new FormData();

        files.forEach(file => {
            formData.append('file', file);
        });

        console.log('Sending...');

        $.ajax({
            url: 'https://this_is_the_url_to_upload_to',
            data: formData,
            type: 'POST',
            success: function (data) { console.log('SUCCESS !!!'); },
            error: function (data) { console.log('ERROR !!!'); },
            cache: false,
            processData: false,
            contentType: false
        });
    });
});