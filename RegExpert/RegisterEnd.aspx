<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterEnd.aspx.cs" Inherits="Register_End" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Register DG Law</title>



    <!-- MATERIAL DESIGN ICONIC FONT -->
    <link rel="stylesheet" href="fonts/material-design-iconic-font/css/material-design-iconic-font.css">

    <!-- DATE-PICKER -->
    <link rel="stylesheet" href="vendor/date-picker/css/datepicker.min.css">


    <link rel="stylesheet" href="css/style.css">
    <script src="js/jquery-3.3.1.min.js"></script>
    <script src="js/script.js"></script>
     <link href="jAlert/jAlerts.css" rel="stylesheet" />

    <script>



        $(document).ready(function () {
            MessageSubmit();
            $("a,ul").hide();

          

        });

        function BeforeSave() {


        
        }


        function MessageSubmit() {
            jAlert({
                headingText: 'System Message',
                contentText: 'Thank you for filling out your information.   Confirmation mail has been sent to you.     get back to you if additional information is needed.'

            }, "top"); // <---- Notice how this is changed to "left"

        } // end left() function

        function SwitchFamily(data) {

            if ($(window).width() >= 767) {
                $(".formobile").hide();
            }

            if (data == "secSoup") {
                $("#secSoup").hide();
                //$("#secChilds").prop("visibility","visible");
                $("#secChilds").show();

            } else {
                $("#secChilds").hide();
                $("#secSoup").show();

            }





        }

        function changeYesNo() {



            var res = $('input[name=yesno]:checked').val();

            if (res == "rdButtYes") {

                $("#secSoup").show();
                $("#aSaveStart").show();
                // $("#secChilds").hide();


            } else {


                $("#secChilds").hide();
                $("#secSoup").hide();
                $("#aSaveStart").hide();



            }

        }

        function SendForm() {

            if (!validateUploadFiles()) {

               

            } else {

                alert("Must upload red Document!");

            }
         

           

        }


        function validateUploadFiles() {

            $("#dvmyButtonInputContainer,#dvmyButton1InputContainer,#dvmyButton2InputContainer").removeClass("dvInputUploadContainer");


            var res = true;
            var firstFileUpload = $("#dvmyButtonInput").html();
            if (!$.trim(firstFileUpload)) {
                $("#dvmyButtonInputContainer").addClass("dvInputUploadContainer");
                res = false;
            }

            var firstFileUpload1 = $("#dvmyButton1Input").html();
            if (!$.trim(firstFileUpload1)) {
                $("#dvmyButton1InputContainer").addClass("dvInputUploadContainer");
                res = false;
            }

            var firstFileUpload2 = $("#dvmyButton2Input").html();
            if (!$.trim(firstFileUpload2)) {
                $("#dvmyButton2InputContainer").addClass("dvInputUploadContainer");
                res = false;
            }

            return res;
        }

        function SearchExistExpert() {

            var Passport = $("#txtPassport").val();

            // alert(Passport);

            $.ajax({
                type: "POST",
                url: "Register.aspx/GetIfExistPassport",
                data: "{'Passport':" + Passport + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) { alert(data.d); }
            });

        }

        function Test() {

            //   alert();
        }


    </script>




</head>
<body>
    <div class="wrapper">



        <form method="POST" id="wizard" class="" runat="server" enctype="multipart/form-data">





            <asp:ScriptManager ID="scr" runat="server">
            </asp:ScriptManager>

            <h4></h4>
            <section style="text-align: center">
                <h3> Thank you
                    <br />
                </h3>
                <br />

                <img src="../App_Themes/Theme1/Images/login.png" alt="" width="365px" height="300px" />

                <div>
                  

                </div>
                <br />

             <%--   <div class="disabled">
                    We Call you

                </div>
--%>




            </section>
            




        </form>

        <!-- JQUERY STEP -->
        <script src="js/jquery.steps.js"></script>

        <!-- DATE-PICKER -->
        <script src="vendor/date-picker/js/datepicker.js"></script>
        <script src="vendor/date-picker/js/datepicker.en.js"></script>
        <script src="vendor/jquery-validation/dist/jquery.validate.min.js"></script>
        <script src="vendor/jquery-validation/dist/additional-methods.min.js"></script>

        <script src="js/main.js"></script>

          <script src="jAlert/jAlerts.js"></script>
</body>
</html>
