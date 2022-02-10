
var form;
(function ($) {
    //https://stackoverflow.com/questions/11082759/jquery-on-and-updatepanels
    form = $("#wizard");

    form.validate({
        errorPlacement: function errorPlacement(error, element) {
             element.before(error); 
        },
        rules: {
            txtPassport : {
                required: true,
            },
            txtSurname : {
                required: true,
            },

            txtName: {
                required: true,
            },
            txtCountry: {
                required: true,
            },
            
            txtPassportIssueDate: {
                required: true,
                date: true
            },
            txtPassportExpDate: {
                required: true,
                date: true
            },
            txtDateofBirth: {
                required: true,
                date: true
            },

            txtEmail : {
                required: true,
                email: true
            },

            //******************** Soup ****************
            txtSoupFamilyname: {

                required: false
            },
            txtSoupGivenname: {

                required: false
            },
            txtSoupMaidenname: {

                required: false
            },
            txtSoupFathersname: {

                required: false
            },
            txtSoupPlaceofBirth: {

                required: false
            },
            txtSoupDateofBirth: {

                required: false,
                date: true
            },

            txtSoupPassport: {
                required: false
            },
            

            txtSoupPassportIsueDate: {

                required: false,
                date: true
            },
            txtSoupPassportExpDate: {

                required: false,
                date: true
            },

            
            
            
            
            
            


        },
        messages: {
            txtPassport : {
                required: "Please enter your Passport"
            },
            txtSurname : {
                required : "Please enter your last name"
            },
            txtName : {
                required : "Please enter your first name"
               
            },
            txtCountry: {
                required: "Please enter your Country"
            },
            
            txtEmail: {
                required: "Please enter your first Email",
                email: "Please enter a valid email address!"
            },

            txtPassportIssueDate: {
                required: "Please enter your Passport Issue Date"
            },
            txtPassportExpDate: {
                required: "Please enter your Passport Expire Date"
            },
            txtDateofBirth: {
                required: "Please enter your Date of Birth"
               
            },

            //************************************

            txtSoupFamilyname: {
                required: "Please enter your Soup Family name"
            },
            txtSoupGivenname: {

                required: "Please enter your Soup Given name"
            },
            txtSoupMaidenname: {

                required: "Please enter your Soup Maiden name"
            },
            txtSoupFathersname: {
                required: "Please enter your Soup Father name"
            },
            txtSoupPlaceofBirth: {

                required: "Please enter your Soup Place of Birth"
            },
            txtSoupDateofBirth: {

                required: "Please enter your Soup Date of Birth"
            },

            txtSoupPassport: {
                required: "Please enter your Soup Passport"
            },


            txtSoupPassportIsueDate: {

                required: "Please enter your Soup Passport Isue Date"
            },
            txtSoupPassportExpDate: {

                required: "Please enter your Soup Passport Expire Date"
            },





        },
        onfocusout: function(element) {
            $(element).valid();
        },
        highlight : function(element, errorClass, validClass) {
            $(element).parent().parent().find('.form-group').addClass('form-error');
            $(element).removeClass('valid');
            $(element).addClass('error');
        },
        unhighlight: function(element, errorClass, validClass) {
            $(element).parent().parent().find('.form-group').removeClass('form-error');
            $(element).removeClass('error');
            $(element).addClass('valid');
        }
    });

    form.steps({
        headerTag: "h4",
        bodyTag: "section",
        transitionEffect: "fade",
        enableAllSteps: true,
        transitionEffectSpeed: 300,
        labels: {
            next: "Next",
            previous: "Back"
        },
        onStepChanging: function (event, currentIndex, newIndex) {

           // if (currentIndex > newIndex) return true;

            form.validate().settings.ignore = ":disabled,:hidden";
            //alert(form.valid());
            if (form.valid()) {

                if (newIndex === 1) {
                    $('.steps ul').addClass('step-2');
                } else {

                    $('.steps ul').removeClass('step-2');
                }
                if (newIndex === 2) {
                    $('.steps ul').addClass('step-3');
                    $('.actions ul').addClass('mt-7');
                } else {
                    $('.steps ul').removeClass('step-3');
                    $('.actions ul').removeClass('mt-7');
                }

                if (newIndex === 3) {
                    $('.steps ul').addClass('step-4');
                    $('.actions ul').addClass('mt-7');
                } else {
                    $('.steps ul').removeClass('step-4');
                    $('.actions ul').removeClass('mt-7');
                }

                if (newIndex === 4) {
                    $('.steps ul').addClass('step-5');
                    $('.actions ul').addClass('mt-7');
                } else {
                    $('.steps ul').removeClass('step-5');
                    $('.actions ul').removeClass('mt-7');
                }

                return true;

            }


            return false;
           
        },
        titleTemplate : '<h3 class="title">#title#</h3>',
        onInit: function (event, currentIndex) { 
           
            // Suppress (skip) "Warning" step if the user is old enough.
            if(currentIndex === 0) {
                form.find('.actions').addClass('test');
            }
        },
        //onStepChanging: function (event, currentIndex, newIndex)
        //{
        //    form.validate().settings.ignore = ":disabled,:hidden";
        //    return form.valid();
        //},
        onFinishing: function (event, currentIndex)
        {
            
            form.validate().settings.ignore = ":disabled";
            return form.valid();
        },
        onFinished: function (event, currentIndex)
        {
           
            SendForm();
          
            //alert('Sumited');
        },
        onStepChanged: function (event, currentIndex, priorIndex)
        {
            if (currentIndex == 4) CallServer();
            if (currentIndex == 3) CallServerStep2();
         
        }
    });

    form.IsFamaly = false;

    jQuery.extend(jQuery.validator.messages, {
        required: "",
        remote: "",
        email: "",
        url: "",
        date: "",
        dateISO: "",
        number: "",
        digits: "",
        creditcard: "",
        equalTo: ""
    });
    
})(jQuery);
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('.your_picture_image')
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}
