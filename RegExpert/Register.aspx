<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

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
    <%-- <script src="../akquinet-jquery-toastmessage-plugin-6f5d7bf/src/main/javascript/jquery.toastmessage.js"
        type="text/javascript"></script>
    <link href="../akquinet-jquery-toastmessage-plugin-6f5d7bf/src/main/resources/css/jquery.toastmessage.css"
        rel="stylesheet" type="text/css" />--%>
    <link href="jAlert/jAlerts.css" rel="stylesheet" />
   
   
    <script>



        $(document).ready(function () {

            var Company = $("#lblCompany").html();

            if (!Company) {

                $("#lblCompany").html("<span class='nocompany'>***The Url Not Valid!***</span>");
                $("a,.disabled").hide();


                
            }
           
          
            var postbackControl = null;
            //  Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            //function BeginRequestHandler(sender, args) {
            //    postbackControl = args.get_postBackElement();
            //    postbackControl.disabled = true;
            //} 
           
            function EndRequestHandler(sender, args) {

                var txtPassportIssueDate = $("#txtPassportIssueDate").val();
                var txtPassportExpDate = $("#txtPassportExpDate").val();
                var txtDateofBirth = $("#txtDateofBirth").val();

                var txtSoupPassportIsueDate = $("#txtSoupPassportIsueDate").val();
                var txtSoupPassportExpDate = $("#txtSoupPassportExpDate").val();
                var txtSoupDateofBirth = $("#txtSoupDateofBirth").val();


                var txtChild1DateofBirth = $("#txtChild1DateofBirth").val();
                var txtChild1PassportIsueDate = $("#txtChild1PassportIsueDate").val();
                var txtChild1PassportExpDate = $("#txtChild1PassportExpDate").val();

                var txtChild2DateofBirth = $("#txtChild2DateofBirth").val();
                var txtChild2PassportIsueDate = $("#txtChild2PassportIsueDate").val();
                var txtChild2PassportExpDate = $("#txtChild2PassportExpDate").val();

                var txtChild3DateofBirth = $("#txtChild3DateofBirth").val();
                var txtChild3PassportIsueDate = $("#txtChild3PassportIsueDate").val();
                var txtChild3PassportExpDate = $("#txtChild3PassportExpDate").val();

                var txtChild4DateofBirth = $("#txtChild4DateofBirth").val();
                var txtChild4PassportIsueDate = $("#txtChild4PassportIsueDate").val();
                var txtChild4PassportExpDate = $("#txtChild4PassportExpDate").val();
               
                $('.datepicker-here').datepicker({ dateFormat: 'dd M yy' });

                $("#txtPassportIssueDate").val(txtPassportIssueDate);
                $("#txtPassportExpDate").val(txtPassportExpDate);
                $("#txtDateofBirth").val(txtDateofBirth);
              
                $("#txtSoupPassportIsueDate").val(txtSoupPassportIsueDate);
                $("#txtSoupPassportExpDate").val(txtSoupPassportExpDate);
                $("#txtSoupDateofBirth").val(txtSoupDateofBirth);

                $("#txtChild1DateofBirth").val(txtChild1DateofBirth);
                $("#txtChild1PassportIsueDate").val(txtChild1PassportIsueDate);
                $("#txtChild1PassportExpDate").val(txtChild1PassportExpDate);

                $("#txtChild2DateofBirth").val(txtChild2DateofBirth);
                $("#txtChild2PassportIsueDate").val(txtChild2PassportIsueDate);
                $("#txtChild2PassportExpDate").val(txtChild2PassportExpDate);


                $("#txtChild3DateofBirth").val(txtChild3DateofBirth);
                $("#txtChild3PassportIsueDate").val(txtChild3PassportIsueDate);
                $("#txtChild3PassportExpDate").val(txtChild3PassportExpDate);

                $("#txtChild4DateofBirth").val(txtChild4DateofBirth);
                $("#txtChild4PassportIsueDate").val(txtChild4PassportIsueDate);
                $("#txtChild4PassportExpDate").val(txtChild4PassportExpDate);

                InitUpload();


                //jAlert({
                //    headingText: 'jQuery jAlerts Demos',
                //    contentText: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi rhoncus et elit id rutrum. Nunc ac quam id erat hendrerit pretium vitae vel felis. Quisque fermentum elit non velit auctor'
                //}, "left"); // <---- Notice how this is changed to "left"

               
            }


            if ($(window).width() >= 767) {
                $(".formobile").hide();
            }


            $("#secChilds").hide();
            $("#secSoup").hide();
            $("#aSaveStart").hide();

            $("a").addClass('isDisabled');    


        });



       
        

        function BeforeSave() {


            form.validate().settings.ignore = ":disabled,:hidden";
            if (form.valid()) {

                __doPostBack('<%=LinkButtonSave.UniqueID%>', '');
                Message();
                //ShowMessage("The Data Save Successed!", 2);
                //return true;
            }


           

            // return false;
        }


        function Message() {
           
            jAlert({
                headingText: 'System Message',
                contentText: 'Data successfully saved'
            }, "top"); // <---- Notice how this is changed to "left"

        } // end left() function


        function CallServer() {

            __doPostBack('<%=btnGetFiles.UniqueID%>', '');
            

        }

        function CallServerStep2() {
            changeYesNo();
        }

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

            //alert(form.IsFamaly);
            

            var res = $('input[name=yesno]:checked').val();

            if (res == "rdButtYes") {

                $("#secSoup").show();
                $("#aSaveStart").show();

                $('input[name^="txtSoup"]').each(function () {
                    $(this).rules('add', {
                        required: true
                    });
                });
              


            } else {


                $("#secChilds").hide();
                $("#secSoup").hide();
                $("#aSaveStart").hide();

                $('input[name^="txtSoup"]').each(function () {
                    $(this).rules('remove');
                });

            }

        }

        function SendForm() {
            
            if (validateUploadFiles()) {

                 __doPostBack('<%=btnSendForm.UniqueID%>', '');

            } else {

                alert("Must upload red Document!");

            }
         

           

        }


        function validateUploadFiles() {

            //$("#dvmyButtonInputContainer,#dvmyButton1InputContainer,#dvmyButton2InputContainer").removeClass("dvInputUploadContainer");


            var res = true;
            //var firstFileUpload = $("#dvmyButtonInput").html();
            //if (!$.trim(firstFileUpload)) {
            //    $("#dvmyButtonInputContainer").addClass("dvInputUploadContainer");
            //    res = false;
            //}

            //var firstFileUpload1 = $("#dvmyButton1Input").html();
            //if (!$.trim(firstFileUpload1)) {
            //    $("#dvmyButton1InputContainer").addClass("dvInputUploadContainer");
            //    res = false;
            //}

            //var firstFileUpload2 = $("#dvmyButton2Input").html();
            //if (!$.trim(firstFileUpload2)) {
            //    $("#dvmyButton2InputContainer").addClass("dvInputUploadContainer");
            //    res = false;
            //}

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


        function setEnableNext() {

            if ($("#confirmCompany").is(":checked")) {
                $("a").removeClass('isDisabled');
            } else {
                $("a").addClass('isDisabled');    
            }
        }


        //function ShowMessage(text, type) {
        //    $().toastmessage({
        //        text: text,
        //        sticky: false,
        //        position: 'middle-center',
        //        type: 'success'
        //        // close: function () { console.log("toast is closed ..."); }
        //    });
        //    // saving the newly created toast into a variable
        //    if (type == "1" || !type) {
        //        $().toastmessage('showNoticeToast');
        //    }

        //    if (type == "2") {
        //        $().toastmessage('showSuccessToast');
        //    }

        //    if (type == "3") {
        //        $().toastmessage('showWarningToast');
        //    }

        //    if (type == "4") {
        //        $().toastmessage('showErrorToast');
        //    }

        //}


    </script>




</head>
<body>
    <div class="wrapper">



        <form method="POST" id="wizard" class="" runat="server" enctype="multipart/form-data">





            <asp:ScriptManager ID="scr" runat="server">
            </asp:ScriptManager>

            <h4></h4>
            <section style="text-align: center">
                <h3>Welcome To DG Law
                    <br />
                </h3>
                <br />

                <img src="../App_Themes/Theme1/Images/login.png" alt="" width="300px" height="200px" />

                <div class="mt-4">
                    This page is created for  
                            <div class="dvcompanyName" id="lblCompany" runat="server"></div>
                   
                     <br /> 
                  
                          
                          <input  type="checkbox"  id="confirmCompany" onchange="setEnableNext()" class="checkboxOf"   > 
                          <span class="bl-ar"> Confirm you work in this Company</span>
                 
                   
                 
                    <br />
                    <br />
                     experts for B1 work permit.


                </div>
                <br />

                <div class="disabled mt-4">
                    notice! <br />
                    you can Save data all the time before submit! <br />
                    please, click next to start fill form

                </div>





            </section>
            <!-- SECTION 1 -->
            <h4></h4>


            <section>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="HiddenFieldExpertRegId" runat="server" Value="0" />
                        <asp:HiddenField ID="HiddenFieldSoup" runat="server" Value="0" />
                        <asp:HiddenField ID="HiddenFieldChild1" runat="server" Value="0" />
                        <asp:HiddenField ID="HiddenFieldChild2" runat="server" Value="0" />
                        <asp:HiddenField ID="HiddenFieldChild3" runat="server" Value="0" />
                        <asp:HiddenField ID="HiddenFieldChild4" runat="server" Value="0" />
                        <div class="form-row" style="margin-bottom: 0px">
                            <div class="form-col">

                                <h3>Expert Details
                                </h3>

                            </div>

                            <div class="form-col" style="text-align: right">
                                <%--  <asp:Button ID="btnSaveData" Text="Save"  runat="server"  OnClick="btnSaveData_Click" CssClass="saveLink" OnClientClick="return BeforeSave();" />--%>

                                <asp:LinkButton ID="LinkButtonSave" OnClick="btnSaveData_Click" runat="server"></asp:LinkButton>
                                <button id="btnSaveData" class="saveLink" type="button" runat="server" onclick="BeforeSave();">Save</button>
                            </div>
                        </div>





                        <div>
                            ***** if you register once please insert your passport first
                        </div>
                        <br />

                        <div class="form-row">



                            <div class="form-col">

                                <label for="">
                                    Passport
                                </label>
                                <div class="form-holder">
                                    <div class="form-group">
                                        <i class="zmdi zmdi-account-o"></i><span class="redMust">*</span>

                                        <asp:TextBox ID="txtPassport" runat="server" CssClass="form-control" OnTextChanged="txtChanged" AutoPostBack="true"></asp:TextBox>



                                    </div>
                                </div>
                            </div>
                            <div class="form-col">

                                <label for="">
                                    Surname
                                </label>
                                <div class="form-holder">
                                    <div class="form-group">
                                        <i class="zmdi zmdi-account-o"></i><span class="redMust">*</span>
                                        <input runat="server" type="text" name="txtSurname" id="txtSurname" class="form-control">
                                    </div>
                                </div>
                            </div>
                            <div class="form-col">
                                <label for="">
                                    Name
                                </label>
                                <div class="form-holder">
                                    <i class="zmdi zmdi-account-o"></i><span class="redMust">*</span>
                                    <!--zmdi-edit-->
                                    <input runat="server" type="text" name="txtName" id="txtName" class="form-control">
                                </div>
                            </div>



                        </div>

                        <div class="form-row">
                            <div class="form-col">
                                <label for="">
                                    Passport Issue Date
                                </label>
                                <div class="form-holder">
                                    <i class="zmdi zmdi-calendar"></i><span class="redMust">*</span>
                                    <input runat="server" type="text" name="txtPassportIssueDate" id="txtPassportIssueDate" autocomplete="off" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                </div>
                            </div>
                            <div class="form-col">
                                <label for="">
                                    Passport Exp Date
                                </label>
                                <div class="form-holder">
                                    <i class="zmdi zmdi-calendar"></i><span class="redMust">*</span>
                                    <input runat="server" type="text" name="txtPassportExpDate" id="txtPassportExpDate" autocomplete="off" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                </div>
                            </div>
                            <div class="form-col">
                                <label for="">
                                    Date Birth
                                </label>
                                <div class="form-holder">
                                    <i class="zmdi zmdi-calendar"></i><span class="redMust">*</span>
                                    <input runat="server" type="text" name="txtDateofBirth" id="txtDateofBirth" class="form-control datepicker-here" autocomplete="off" data-language='en' data-date-format="dd M yy">
                                </div>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="form-col">
                                <label for="">
                                    Phone
                                </label>
                                <div class="form-holder">
                                    <i class="zmdi zmdi-smartphone-android"></i>
                                    <input runat="server" type="text" name="txtPhone" id="txtPhone" class="form-control">
                                </div>
                            </div>
                            <div class="form-col">
                                <label for="">
                                    Email
                                </label>
                                <div class="form-holder">
                                    <i class="zmdi zmdi-email"></i><span class="redMust">*</span>
                                    <input runat="server" type="text" name="txtEmail" id="txtEmail" class="form-control">
                                </div>
                            </div>

                            <div class="form-col">
                                <label for="">
                                    Job
                                </label>
                                <div class="form-holder">
                                    <i class="zmdi zmdi-edit"></i>
                                    <!--zmdi-edit-->
                                    <input runat="server" type="text" name="txtJob" id="txtJob" class="form-control">
                                </div>
                            </div>

                        </div>

                        <div class="form-row">
                            <div class="form-col">
                                <label for="">
                                    Street and house no.
                                </label>
                                <div class="form-holder">
                                    <i class="zmdi zmdi-edit"></i>
                                    <input runat="server" type="text" name="txtStreet" id="txtStreet" class="form-control">
                                </div>
                            </div>
                            <div class="form-col">
                                <label for="">
                                    Town
                                </label>
                                <div class="form-holder">
                                    <i class="zmdi zmdi-edit"></i>
                                    <input runat="server" type="text" name="txtTown" id="txtTown" class="form-control">
                                </div>
                            </div>

                            <div class="form-col">
                                <label for="">
                                    Country
                                </label>
                                <div class="form-holder">
                                    <i class="zmdi zmdi-edit"></i><span class="redMust">*</span>
                                    <!--zmdi-edit-->
                                    <input runat="server" type="text" name="txtCountry" id="txtCountry" class="form-control">
                                </div>
                            </div>

                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <%-- <asp:AsyncPostBackTrigger ControlID="txtPassport" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnSaveData"  EventName="Click" />--%>
                    </Triggers>
                </asp:UpdatePanel>

            </section>

            <!-- SECTION 2 -->
            <h4></h4>
            <section>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <div id="secStart">
                            <div class="form-row" style="margin-bottom: 0px">
                                <div class="form-col">
                                    <h3>Family
                                    </h3>
                                </div>

                                <div class="form-col" style="text-align: right" id="aSaveStart">

                                    <button id="Button1" class="saveLink" type="button" runat="server" onclick="BeforeSave();">Save</button>

                                </div>

                            </div>


                            <div class="form-row">
                                <div class="form-col" style="width: auto">
                                    Is your family will be traveling with you or plan to sty in Israel?


                                </div>


                                <div class="">


                                    <%--   <label class="label">
                                <input runat="server" type="radio" id="rdButtYes" name="yesno" onchange="changeYesNo()"  value="yes">Yes</label>
                            <label class="label">
                                <input runat="server"  type="radio" id="rdButtNo" name="yesno" onchange="changeYesNo()" checked  value="no">No</label>--%>


                                    <asp:RadioButton ID="rdButtNo" GroupName="yesno" runat="server" Checked Text="No" onclick="changeYesNo();" />
                                    <asp:RadioButton ID="rdButtYes" GroupName="yesno" runat="server" Text="Yes" onclick="changeYesNo();" />



                                </div>






                            </div>


                        </div>

                        <div id="secSoup" runat="server">



                            <h1>Soup</h1>


                            <div class="form-row">
                                <div class="form-col">

                                    <label for="">
                                        Family name
                                    </label>

                                    <div class="form-holder">
                                        <i class="zmdi zmdi-account-o"></i>
                                        <input runat="server" type="text" name="txtSoupFamilyname" id="txtSoupFamilyname" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="">
                                        Given name
                                    </label>
                                    <div class="form-holder">
                                        <i class="zmdi zmdi-account-o"></i>
                                        <input runat="server" type="text" name="txtSoupGivenname" id="txtSoupGivenname" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="">
                                        Maiden name
                                    </label>
                                    <div class="form-holder">
                                        <i class="zmdi zmdi-account-o"></i>
                                        <input runat="server" type="text" name="txtSoupMaidenname" id="txtSoupMaidenname" class="form-control">
                                    </div>
                                </div>


                            </div>

                            <div class="form-row">

                                <div class="form-col">
                                    <label for="">
                                        Father’s name
                                    </label>
                                    <div class="form-holder">
                                        <i class="zmdi zmdi-account-o"></i>
                                        <input runat="server" type="text" name="txtSoupFathersname" id="txtSoupFathersname" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="">
                                        Place of Birth
                                    </label>
                                    <div class="form-holder">
                                        <i class="zmdi zmdi-account-o"></i>
                                        <input runat="server" type="text" name="txtSoupPlaceofBirth" id="txtSoupPlaceofBirth" class="form-control">
                                    </div>
                                </div>

                                <div class="form-col">
                                    <label for="">
                                        Date of Birth
                                    </label>
                                    <div class="form-holder">
                                        <i class="zmdi zmdi-calendar"></i>
                                        <input runat="server" type="text" name="txtSoupDateofBirth" id="txtSoupDateofBirth" autocomplete="off" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>

                            </div>

                            <div class="form-row">

                                <div class="form-col">
                                    <label for="">
                                        Passport
                                    </label>
                                    <div class="form-holder">
                                        <i class="zmdi zmdi-account-o"></i>
                                        <input runat="server" type="text" name="txtSoupPassport" id="txtSoupPassport" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="">
                                        Passport Issue Date
                                    </label>
                                    <div class="form-holder">
                                        <i class="zmdi zmdi-calendar"></i>
                                        <input runat="server" type="text" name="txtSoupPassportIsueDate" id="txtSoupPassportIsueDate" autocomplete="off" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>

                                <div class="form-col">
                                    <label for="">
                                        Passport Exp Date
                                    </label>
                                    <div class="form-holder">
                                        <i class="zmdi zmdi-calendar"></i>
                                        <input runat="server" type="text" name="txtSoupPassportExpDate" id="txtSoupPassportExpDate" autocomplete="off" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>

                            </div>

                            <div class="form-row">
                                <div class="form-col">
                                </div>
                                <div class="form-col">
                                </div>

                                <div class="form-col" style="text-align: right">
                                    <a href="#" style="text-decoration: underline" onclick="SwitchFamily('secSoup')">Switch to add children</a>
                                </div>

                            </div>


                        </div>

                        <div id="secChilds" runat="server">
                            <!--<div class="form-row" style="margin-bottom:0px">
                    <div class="form-col">
                        <h3>
                            Family
                        </h3>
                    </div>



                    <div class="form-col" style="text-align:right">
                        <a onclick="alert()" class="saveLink">Save</a>
                    </div>
                </div>-->





                            <h1>Children under the age of 18</h1>
                            <br />

                            <div class="form-row">

                                <div class="form-col">
                                    <label for="">
                                        Given name
                                    </label>

                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild1Givenname" style="padding-left: 1px" placeholder="1." id="txtChild1Givenname" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="">
                                        Country of birth
                                    </label>

                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild1Countryofbirth" style="padding-left: 1px" placeholder="1." id="txtChild1Countryofbirth" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="">
                                        Date of Birth
                                    </label>

                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild1DateofBirth" style="padding-left: 1px" placeholder="1." id="txtChild1DateofBirth" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="">
                                        Passport
                                    </label>

                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild1Passport" style="padding-left: 1px" placeholder="1." id="txtChild1Passport" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="">
                                        Issue Date
                                    </label>

                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild1PassportIsueDate" style="padding-left: 1px" placeholder="1." id="txtChild1PassportIsueDate" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="">
                                        Exp Date
                                    </label>

                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild1PassportExpDate" style="padding-left: 1px" placeholder="1." id="txtChild1PassportExpDate" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>

                            </div>
                            <div class="form-row">

                                <div class="form-col">
                                    <label for="" class="formobile">
                                        2. Given name
                                    </label>

                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild2Givenname" style="padding-left: 1px" placeholder="2." id="txtChild2Givenname" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        2.Country of birth
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild2Countryofbirth" style="padding-left: 1px" placeholder="2." id="txtChild2Countryofbirth" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        2.Date of Birth
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild2DateofBirth" style="padding-left: 1px" placeholder="2." id="txtChild2DateofBirth" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        2. Passport
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild2Passport" style="padding-left: 1px" placeholder="2." id="txtChild2Passport" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        2. Passport Issue Date
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild2PassportIsueDate" style="padding-left: 1px" placeholder="2." id="txtChild2PassportIsueDate" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        2. Passport Exp Date
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild2PassportExpDate" style="padding-left: 1px" placeholder="2." id="txtChild2PassportExpDate" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>

                            </div>
                            <div class="form-row">

                                <div class="form-col">
                                    <label for="" class="formobile">
                                        3. Given name
                                    </label>

                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild3Givenname" style="padding-left: 1px" placeholder="3." id="txtChild3Givenname" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        3.Country of birth
                                    </label>

                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild3Countryofbirth" style="padding-left: 1px" placeholder="3." id="txtChild3Countryofbirth" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        3.Date of Birth
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild3DateofBirth" style="padding-left: 1px" placeholder="3." id="txtChild3DateofBirth" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        3. Passport
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild3Passport" style="padding-left: 1px" placeholder="3." id="txtChild3Passport" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        3. Passport Issue Date
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild3PassportIsueDate" style="padding-left: 1px" placeholder="3." id="txtChild3PassportIsueDate" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        3. Passport Exp Date
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild3PassportExpDate" style="padding-left: 1px" placeholder="3." id="txtChild3PassportExpDate" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>

                            </div>
                            <div class="form-row">

                                <div class="form-col">
                                    <label for="" class="formobile">
                                        4. Given name
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild4Givenname" style="padding-left: 1px" placeholder="4." id="txtChild4Givenname" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        4.Country of birth
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild4Countryofbirth" style="padding-left: 1px" placeholder="4." id="txtChild4Countryofbirth" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        4.Date of Birth
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild4DateofBirth" style="padding-left: 1px" placeholder="4." id="txtChild4DateofBirth" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        4. Passport
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild4Passport" style="padding-left: 1px" placeholder="4." id="txtChild4Passport" class="form-control">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        4. Passport Issue Date
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild4PassportIsueDate" style="padding-left: 1px" placeholder="4." id="txtChild4PassportIsueDate" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>
                                <div class="form-col">
                                    <label for="" class="formobile">
                                        4. Passport Exp Date
                                    </label>
                                    <div class="form-holder">
                                        <input runat="server" type="text" name="txtChild4PassportExpDate" style="padding-left: 1px" placeholder="4." id="txtChild4PassportExpDate" class="form-control datepicker-here" data-language='en' data-date-format="dd M yy">
                                    </div>
                                </div>

                            </div>
                            <div class="form-row">
                                <div class="form-col">
                                </div>
                                <div class="form-col">
                                </div>

                                <div class="form-col" style="text-align: right">
                                    <a href="#" style="text-decoration: underline" onclick="SwitchFamily('secChilds')">Switch to Soup data</a>
                                </div>

                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtPassport" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>

            </section>

            <!-- SECTION 3 -->
            <h4></h4>
            <section>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-row" style="margin-bottom: 0px">
                            <div class="form-col">

                                <h3>Upload Docs
                                </h3>

                            </div>

                            <div class="form-col" style="text-align: right">
                            <%--  <button id="Button2" class="saveLink" type="button" runat="server" onclick="BeforeSave();">Save</button>--%>
                            </div>
                        </div>

                        <%--<div>
                          <span class="redMust"></span>  please, Upload files <br /><br />
                        </div>--%>

                        
                        <div class="form-row">

                            <div class="form-col">
                                <div class="upload-img-box bg-gray-200" id="dvmyButtonInputContainer">
                                    <div class="f-14  rtl mt-4"><span class="redMust"></span> Copy of passport</div>
                                    <button id="myButton" class="uploadLink myButton" type="button">Upload file</button>
                                    <input runat="server" id="myButtonInput" type="file" class="myInput"  style="display: none" runat="server" />
                                    
                                    <button id="myButtonRemove" class="removeLink myButtonRemove" type="button">Remove file</button>
                            
                                    <div id="dvmyButtonInput" class="dvFilesUpload" runat="server" >
                                    
                                    </div>
                                </div>

                            </div>
                            <div class="form-col">
                                <div class="upload-img-box bg-gray-200 " id="dvmyButton1InputContainer">
                                    <div class="f-14  rtl mt-4"><span class="redMust"></span>CV English </div>
                                   <%-- <img src="images/camera.svg" id="myButton1" class="myButton" alt="">--%>
                                    <button id="myButton1" class="uploadLink myButton" type="button">Upload file</button>
                                    <input runat="server" id="myButton1Input" type="file" class="myInput"  style="display: none" runat="server" />
                                    <button id="myButton1Remove" class="removeLink myButtonRemove" type="button">Remove file</button>
                                    
                                    <div id="dvmyButton1Input" class="dvFilesUpload" runat="server">
                                    </div>
                                </div>

                            </div>
                            <div class="form-col">
                                <div class="upload-img-box bg-gray-200  " id="dvmyButton2InputContainer">
                                    <div class="f-14  rtl mt-4"><span class="redMust"></span>Diplomas/Qualification</div>
                                   <button id="myButton2" class="uploadLink myButton" type="button">Upload file</button>
                                    <input runat="server" id="myButton2Input" type="file" class="myInput"  style="display: none" runat="server" />
                                   
                                    <button id="myButton2Remove" class="removeLink myButtonRemove" type="button">Remove file</button>
                                    <div id="dvmyButton2Input" class="dvFilesUpload" runat="server" >
                                    </div>
                                </div>

                            </div>
                            <div class="form-col">
                                <div class="upload-img-box bg-gray-200  ">
                                    <div class="f-14  rtl mt-4">Vaccination/Recovery </div>
                                    <button id="myButton3" class="uploadLink myButton" type="button">Upload file</button>
                                    <input runat="server" id="myButton3Input" type="file" class="myInput"  style="display: none" runat="server" />
                                    <button id="myButton3Remove" class="removeLink myButtonRemove" type="button">Remove file</button>
                                    <div id="dvmyButton3Input" class="dvFilesUpload" runat="server">
                                    </div>
                                </div>

                            </div>

                        </div>

                        <div class="form-row">

                            <div class="form-col">
                                <div class="upload-img-box bg-gray-200  ">
                                    <div class="f-14  rtl mt-4">Medical Insurance</div>
                                   <button id="myButton4" class="uploadLink myButton" type="button">Upload file</button>
                                    <input runat="server" id="myButton4Input" type="file" class="myInput" multiple style="display: none" runat="server" />
                                    <button id="myButton4Remove" class="removeLink myButtonRemove" type="button">Remove file</button>
                                    <div id="dvmyButton4Input" class="dvFilesUpload" runat="server">
                                    </div>
                                </div>

                            </div>
                            <div class="form-col">
                                <div class="upload-img-box bg-gray-200  ">
                                    <div class="f-14  rtl mt-4">Israeli B-1 work visas</div>
                                     <button id="myButton5" class="uploadLink myButton" type="button">Upload file</button>
                                    <input runat="server" id="myButton5Input" type="file" class="myInput" multiple style="display: none" runat="server" />
                                   
                                     <button id="myButton5Remove" class="removeLink myButtonRemove" type="button">Remove file</button>
                                    <div id="dvmyButton5Input" class="dvFilesUpload" runat="server" >
                                    </div>
                                </div>

                            </div>
                            <div class="form-col">
                                <div class="upload-img-box bg-gray-200  ">
                                    <div class="f-14  rtl mt-4">Application Forms </div>
                                     <button id="myButton6" class="uploadLink myButton" type="button">Upload file</button>
                                    <input runat="server" id="myButton6Input" type="file" class="myInput" multiple style="display: none" runat="server" />
                                   
                                      <button id="myButton6Remove" class="removeLink myButtonRemove" type="button">Remove file</button>
                                    <div id="dvmyButton6Input" class="dvFilesUpload" runat="server">
                                    </div>
                                </div>

                            </div>
                            <div class="form-col">
                                <div class="upload-img-box bg-gray-200  ">
                                    <div class="f-14  rtl mt-4">Doc 8 </div>
                                     <button id="myButton7" class="uploadLink myButton" type="button">Upload file</button>
                                    <input runat="server" id="myButton7Input" type="file" class="myInput" multiple style="display: none" runat="server" />
                                  
                                    <button id="myButton7Remove" class="removeLink myButtonRemove" type="button">Remove file</button>
                                    <div id="dvmyButton7Input" class="dvFilesUpload" runat="server">
                                    </div>
                                </div>

                            </div>

                        </div>

                        <div class="form-row">

                            <div class="form-col">
                                <div class="upload-img-box bg-gray-200  ">
                                    <div class="f-14  rtl mt-4">Doc 9</div>
                                    <button id="myButton8" class="uploadLink myButton" type="button">Upload file</button>
                                    <input runat="server" id="myButton8Input" type="file" class="myInput" multiple style="display: none" runat="server" />
                                     <button id="myButton8Remove" class="removeLink myButtonRemove" type="button">Remove file</button>
                                    <div id="dvmyButton8Input" class="dvFilesUpload" runat="server">
                                    </div>
                                </div>

                            </div>
                            <div class="form-col">
                                <div class="upload-img-box bg-gray-200  ">
                                    <div class="f-14  rtl mt-4">Doc 10 </div>
                                     <button id="myButton9" class="uploadLink myButton" type="button">Upload file</button>
                                    <input runat="server" id="myButton9Input" type="file" class="myInput" multiple style="display: none" runat="server" />
                                     <button id="myButton9Remove" class="removeLink myButtonRemove" type="button">Remove file</button>
                                    <div id="dvmyButton9Input" class="dvFilesUpload" runat="server">
                                    </div>
                                </div>

                            </div>
                            <div class="form-col">
                                <div class="upload-img-box bg-gray-200  ">
                                    <div class="f-14  rtl mt-4">Doc 11 </div>
                                    <button id="myButton10" class="uploadLink myButton" type="button">Upload file</button>
                                    <input runat="server" id="myButton10Input" type="file" class="myInput" multiple style="display: none" runat="server" />
                                      <button id="myButton10Remove" class="removeLink myButtonRemove" type="button">Remove file</button>
                                    <div id="dvmyButton10Input" class="dvFilesUpload" runat="server">
                                    </div>
                                </div>

                            </div>
                            <div class="form-col">
                                <div class="upload-img-box bg-gray-200  ">
                                    <div class="f-14  rtl mt-4">Doc 12 </div>
                                    <button id="myButton11" class="uploadLink myButton" type="button">Upload file</button>
                                    <input runat="server" id="myButton11Input" type="file" class="myInput" multiple style="display: none" runat="server" />
                                    <button id="myButton11Remove" class="removeLink myButtonRemove" type="button">Remove file</button>
                                    <div id="dvmyButton11Input" class="dvFilesUpload" runat="server">
                                    </div>
                                </div>

                            </div>

                        </div>
                         <asp:LinkButton ID="btnGetFiles" OnClick="btnGetFiles_Click" runat="server"></asp:LinkButton>
                        <asp:LinkButton ID="btnSendForm" OnClick="SendClientForm" runat="server"></asp:LinkButton>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSendForm" />
                        <asp:AsyncPostBackTrigger ControlID="btnGetFiles" />
                    </Triggers>
                </asp:UpdatePanel>


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
