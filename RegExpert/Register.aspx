<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>


    <script type="text/javascript" src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery-ui-1.10.3.custom.min.js"></script>
    <script type="text/javascript" src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.inputhints.min.js"></script>
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/custom-theme/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />
    <script src="script.js"></script>

    <script src="../akquinet-jquery-toastmessage-plugin-6f5d7bf/src/main/javascript/jquery.toastmessage.js"
        type="text/javascript"></script>
    <link href="../akquinet-jquery-toastmessage-plugin-6f5d7bf/src/main/resources/css/jquery.toastmessage.css"
        rel="stylesheet" type="text/css" />


    <script language="javascript">

        var CurrentStep = 1;
        // 1 פרטים
        // 2 משפחה
        // 3 מסמכים ושמירה
        $(document).ready(function () {

            //$("#fileUpload1").change(function (e) {

            //    alert(e.target.files.length);
            //    $("#txtUploadFile1").val($(this).val());

            //    //alert();
            //    //submit the form here
            //});
            $(".buttBlue").button();
            DisableButtons(0, 1, 0);

            //$("#dvStep1").hide();
            //$("#dvStep3").show();

            //rdButtNo

            $("#rdButtNo").change(function () {

                $("#dvFamaly").hide();
            });

            $("#rdButtYes").change(function () {

                $("#dvFamaly").show();
            });




            //$('input[type=radio][name=yesno]').change(function () {
            //    if (this.value == '1')
            //        $("#dvFamaly").show();
            //    else
            //        $("#dvFamaly").hide();

            //});


            $(".txtCalander").datepicker({
                showOn: "button",
                changeYear: true,
                changeMonth: true,
                buttonImage: "../App_Themes/Theme1/Images/calendar.gif",
                buttonImageOnly: true,
                dateFormat: "d M y"
            });





            $(".ui-datepicker").css("font-size", "15px");

            $(".ui-datepicker-trigger").css("position", "relative").css("left", "3px").css("top", "2px");

        });
        function DisableButtons(button0, button1, button2) {

            if (button0 == 1)
                $("#btnAction0").show();
            else
                $("#btnAction0").hide();

            if (button1 == 1)
                $("#btnAction1").show();
            else
                $("#btnAction1").hide();

            if (button2 == 1)
                $("#btnAction2").show();
            else
                $("#btnAction2").hide();

        }


        function btnClick(action) {


            if (action == 2) {

                if (!ValidateForm3()) {

                    ShowMessage('Must Fill All Filed With * ', '4');
                    return false;
                }

                return true;


            }


            if (action == 1 && CurrentStep == 1) {

                if (!ValidateForm1()) {

                    ShowMessage('Must Fill All Filed With * ', '4');
                    return;
                }

                $("#dvStep1").hide();
                $("#dvStep2").show();
                DisableButtons(1, 1, 0);
                CurrentStep = 2;

                return;
            }


            if (action == 1 && CurrentStep == 2) {

                // hhh
                if ($("#<%= rdButtYes.ClientID %>").is(":checked")) {

                    if (!ValidateForm2()) {

                        ShowMessage('Must Fill All Filed With * ', '4');
                        return;
                    }


                }
                $("#dvStep1").hide();
                $("#dvStep2").hide();
                $("#dvStep3").show();
                DisableButtons(1, 0, 1);
                CurrentStep = 3;

                return;

            }

            if (action == 0 && CurrentStep == 2) {
                $("#dvStep2").hide();
                $("#dvStep1").show();
                $("#dvStep3").hide();
                DisableButtons(0, 1, 0);
                CurrentStep = 1;

                return;
            }

            if (action == 0 && CurrentStep == 3) {
                $("#dvStep1").hide();
                $("#dvStep2").show();
                $("#dvStep3").hide();
                DisableButtons(1, 1, 0);
                CurrentStep = 2;

                return;
            }




        }



        function ValidateForm1() {



            if ($('#<%=txtPassport.ClientID%>').val() == "" ||
                $('#<%=txtName.ClientID%>').val() == "" ||
                $('#<%=txtPassportExpDate.ClientID%>').val() == "" ||
                $('#<%=txtPassportIssueDate.ClientID%>').val() == "" ||
                $('#<%=txtSurname.ClientID%>').val() == ""

                ) {

                return false;

            }


            return true;
        }

        function ValidateForm2() {


            if (
                   $('#<%=txtSoupFamilyname.ClientID%>').val() == "" ||
                    $('#<%=txtSoupGivenname.ClientID%>').val() == "" ||
                    $('#<%=txtSoupPassport.ClientID%>').val() == "" ||
                    $('#<%=txtSoupPassportIsueDate.ClientID%>').val() == "" ||
                    $('#<%=txtSoupPassportExpDate.ClientID%>').val() == "" ||
                    $('#<%=txtSoupPlaceofBirth.ClientID%>').val() == "" ||
                    $('#<%=txtSoupMaidenname.ClientID%>').val() == "" ||
                    $('#<%=txtSoupFathersname.ClientID%>').val() == "" ||
                    $('#<%=txtSoupDateofBirth.ClientID%>').val() == ""


                ) {

                return false;

            }


            return true;
        }






        function ValidateForm3() {


            if (
                $.trim($("#dvmyButtonInput").html()) == "" || 

                 $.trim($("#dvmyButton1Input").html()) == "" || 

                 $.trim($("#dvmyButton2Input").html()) == ""

                )
              

            return false;

            return true;

        }


        function ShowMessage(text, type) {
            $().toastmessage({
                text: text,
                sticky: false,
                position: 'middle-center',
                type: 'success'
                // close: function () { console.log("toast is closed ..."); }
            });
            // saving the newly created toast into a variable
            if (type == "1" || !type) {
                $().toastmessage('showNoticeToast');
            }

            if (type == "2") {
                $().toastmessage('showSuccessToast');
            }

            if (type == "3") {
                $().toastmessage('showWarningToast');
            }

            if (type == "4") {
                $().toastmessage('showErrorToast');
            }

        }



    </script>
</head>
<body>
    <form id="Form1" runat="server">
         <asp:HiddenField ID="HiddenFieldExpertRegId" runat="server" Value="0"/>
         <asp:HiddenField ID="HiddenFieldSoup" runat="server" Value="0"/>
         <asp:HiddenField ID="HiddenFieldChild1" runat="server" Value="0"/>
         <asp:HiddenField ID="HiddenFieldChild2" runat="server" Value="0"/>
         <asp:HiddenField ID="HiddenFieldChild3" runat="server" Value="0"/>
         <asp:HiddenField ID="HiddenFieldChild4" runat="server" Value="0"/>

        <asp:PlaceHolder runat="server" ID="TablePlaceHolder"></asp:PlaceHolder>
        <div align="center">
            <div id="dvMainReg" class="dvRoundAndBgForReg" runat="server">
                <div class="">

                    <div class="">
                        <div style="float: left">
                            <h2 style="color: #025482; font-size: 30px;">Welcome To DG Law</h2>
                            <br />
                            <span style="color: #025482; font-size: 23px;">This page is created for "<asp:Label ID="lblCompany" runat="server"></asp:Label>" <br />
                                 experts for B1 work permit.<br />
                                <%-- if you are not employed by company name please insert passport first.  --%>
                            </span>
                        </div>

                       <%-- <div style="float: right">
                            <img src="../App_Themes/Theme1/Images/login.png" alt="" width="165px" height="140px" />
                        </div>--%>

                    </div>

                    <div style="clear: both"></div>

                    <div id="dvStep1" class="dvSteps">
                        <hr />

                        <span style="color: #025482; font-size: 19px;"><u>Details</u></span>
                        <span style="color: red; font-size: 15px;">(*) the fields with * are must to use</span>
                        <br />
                        <br />
                        ***** if you register once please insert your passport first
                        <br />
                        <br />
                        <table border="0" class="tblDetalis">
                            <tr>
                                <td>
                                    <div>
                                        <span class="spTitle">Passport</span><br />
                                        <asp:TextBox ID="txtPassport" CssClass="txtRegister" OnTextChanged="txtChanged" AutoPostBack="true" runat="server" Width="240" Height="25" />
                                        <span class="spRed">*</span>
                                    </div>
                                </td>



                                <td>
                                    <div>
                                        <span class="spTitle">Surname</span><br />
                                        <asp:TextBox ID="txtSurname" CssClass="txtRegister" runat="server" Width="240" Height="25" />
                                        <span class="spRed">*</span>
                                    </div>
                                </td>


                                <td>
                                    <div>
                                        <span class="spTitle">Name</span><br />
                                        <asp:TextBox ID="txtName" CssClass="txtRegister" runat="server" Width="240" Height="25" />
                                        <span class="spRed">*</span>
                                    </div>
                                </td>



                            </tr>

                            <tr>
                                <td>
                                    <div>
                                        <span class="spTitle">Passport Issue Date</span><br />
                                        <asp:TextBox ID="txtPassportIssueDate" CssClass="txtRegister txtCalander" runat="server" Width="240" Height="25" />
                                        <span class="spRed">&nbsp;*</span>
                                    </div>
                                </td>



                                <td>
                                    <div>
                                        <span class="spTitle">Passport Exp Date</span><br />
                                        <asp:TextBox ID="txtPassportExpDate" CssClass="txtRegister txtCalander" runat="server" Width="240" Height="25" />
                                        <span class="spRed">&nbsp;*</span>
                                    </div>
                                </td>


                                <td>
                                    <div>
                                        <span class="spTitle">Date Birth</span><br />
                                        <asp:TextBox ID="txtDateofBirth" CssClass="txtRegister txtCalander" runat="server" Width="240" Height="25" />
                                        <%-- <span class="spRed">*</span>--%>
                                    </div>
                                </td>



                            </tr>

                            <tr>
                                <td colspan="5">
                                    <hr />

                                </td>

                            </tr>



                            <tr>
                                <td>
                                    <div>
                                        <span class="spTitle">Phone</span><br />
                                        <asp:TextBox ID="txtPhone" CssClass="txtRegister" runat="server" Width="240" Height="25" />
                                    </div>
                                </td>



                                <td>
                                    <div>
                                        <span class="spTitle">Email</span><br />
                                        <asp:TextBox ID="txtEmail" CssClass="txtRegister" runat="server" Width="240" Height="25" />

                                    </div>
                                </td>


                                <td>
                                    <div>
                                        <span class="spTitle">Job</span><br />
                                        <asp:TextBox ID="txtJob" CssClass="txtRegister" runat="server" Width="240" Height="25" />

                                    </div>
                                </td>



                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <span class="spTitle">Street and house no. </span>
                                        <br />
                                        <asp:TextBox ID="txtStreet" CssClass="txtRegister" runat="server" Width="240" Height="25" />
                                    </div>
                                </td>

                                <td>
                                    <div>
                                        <span class="spTitle">Town </span>
                                        <br />
                                        <asp:TextBox ID="txtTown" CssClass="txtRegister" runat="server" Width="240" Height="25" />
                                    </div>
                                </td>

                                <td>
                                    <div>
                                        <span class="spTitle">country</span><br />
                                        <asp:TextBox ID="txtCountry" CssClass="txtRegister" runat="server" Width="240" Height="25" />
                                    </div>
                                </td>




                            </tr>




                        </table>


                    </div>

                    <div id="dvStep2" class="dvSteps" style="display: none">
                        <hr />
                        <span style="color: #025482; font-size: 19px;"><u>Famely</u></span>
                        <span style="color: red; font-size: 15px;">(*) the fields with * are must to use</span>
                        <br />

                        <table>
                            <tr>
                                <td valign="top">
                                    <h2 style="color: #025482; font-size: 20px;">Is your family will be traveling with you or plan to sty in Israel?
                                  
                                  
                                    </h2>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <div style="color: #025482; font-size: 20px;">

                                        <asp:RadioButton ID="rdButtNo" GroupName="yesno" runat="server" Checked Text="No" />
                                        <asp:RadioButton ID="rdButtYes" GroupName="yesno" runat="server" Text="Yes" />


                                        <%--  <input type="radio" runat="server" id="rdNo" name="yesno" value="0" checked>
                                        No
                                       
                                        <input type="radio" runat="server" id="rdYes" name="yesno" value="1">
                                        Yes--%>
                                    </div>

                                </td>

                            </tr>

                        </table>

                        <div id="dvFamaly" style="display: none">
                            <div>

                                <h2 style="color: #025482; font-size: 20px; text-decoration: underline">Soup</h2>
                            </div>
                            <div>
                                <table width="100%" class="tblFamaly">
                                    <tr>
                                        <td class="tdFamHeader"><span class="spRedFam">*</span>Family name 
                                        </td>
                                        <td class="tdFamHeader"><span class="spRedFam">*</span>Given name
                                        </td>
                                        <td class="tdFamHeader"><span class="spRedFam">*</span>Maiden name
                                        </td>
                                        <td class="tdFamHeader"><span class="spRedFam">*</span>Father’s name

                                        </td>
                                        <td class="tdFamHeader"><span class="spRedFam">*</span>Place of Birth

                                        </td>
                                        <td class="tdFamHeader"><span class="spRedFam">*</span>Date of Birth
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtSoupFamilyname" CssClass="txtFamely" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtSoupGivenname" CssClass="txtFamely" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtSoupMaidenname" CssClass="txtFamely" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtSoupFathersname" CssClass="txtFamely" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtSoupPlaceofBirth" CssClass="txtFamely" runat="server" />

                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtSoupDateofBirth" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>
                                    </tr>






                                </table>
                                <table width="80%" class="tblFamaly">
                                    <tr>

                                        <td class="tdFamHeader"><span class="spRedFam">*</span>Passport

                                        </td>
                                        <td class="tdFamHeader"><span class="spRedFam">*</span>Passport Issue Date

                                        </td>
                                        <td class="tdFamHeader"><span class="spRedFam">*</span>Passport Exp Date

                                        </td>
                                    </tr>
                                    <tr>

                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtSoupPassport" CssClass="txtFamely" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtSoupPassportIsueDate" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtSoupPassportExpDate" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>


                                    </tr>



                                </table>





                            </div>

                            <div>
                                <br />
                                <h2 style="color: #025482; font-size: 20px; text-decoration: underline">Children under the age of 18</h2>
                            </div>
                            <div>
                                <table width="100%" class="tblFamaly">
                                    <tr>
                                        <td class="tdFamHeader">Given name

                                        </td>
                                        <td class="tdFamHeader">Country of birth

                                        </td>
                                        <td class="tdFamHeader">Date of Birth

                                        </td>
                                        <td class="tdFamHeader">Passport

                                        </td>
                                        <td class="tdFamHeader">Passport Issue Date

                                        </td>
                                        <td class="tdFamHeader">Passport Exp Date

                                        </td>





                                    </tr>
                                    <tr>
                                        <td class="tdFamData">
                                            <b>1.</b>
                                            <asp:TextBox ID="txtChild1Givenname" CssClass="txtFamely txtFamelyNumber" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild1Countryofbirth" CssClass="txtFamely" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild1DateofBirth" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>

                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild1Passport" CssClass="txtFamely" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild1PassportIsueDate" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild1PassportExpDate" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>



                                    </tr>
                                    <tr>
                                        <td class="tdFamData">
                                            <b>2.</b>
                                            <asp:TextBox ID="txtChild2Givenname" CssClass="txtFamely txtFamelyNumber" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild2Countryofbirth" CssClass="txtFamely" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild2DateofBirth" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>





                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild2Passport" CssClass="txtFamely" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild2PassportIsueDate" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild2PassportExpDate" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>


                                    </tr>

                                    <tr>
                                        <td class="tdFamData">
                                            <b>3.</b>
                                            <asp:TextBox ID="txtChild3Givenname" CssClass="txtFamely txtFamelyNumber" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild3Countryofbirth" CssClass="txtFamely" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild3DateofBirth" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>

                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild3Passport" CssClass="txtFamely" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild3PassportIsueDate" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild3PassportExpDate" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>

                                    </tr>

                                    <tr>
                                        <td class="tdFamData">
                                            <b>4.</b>
                                            <asp:TextBox ID="txtChild4Givenname" CssClass="txtFamely txtFamelyNumber" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild4Countryofbirth" CssClass="txtFamely" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild4DateofBirth" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>

                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild4Passport" CssClass="txtFamely" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild4PassportIsueDate" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>
                                        <td class="tdFamData">
                                            <asp:TextBox ID="txtChild4PassportExpDate" CssClass="txtFamely txtCalander txtFamelyDate" runat="server" />
                                        </td>

                                    </tr>






                                </table>

                            </div>

                        </div>

                    </div>

                    <div id="dvStep3" class="dvSteps" style="display: none">
                        <hr />
                        <span style="color: #025482; font-size: 19px;"><u>Upload Document</u></span>
                        <span style="color: red; font-size: 15px;">(*) the fields with * are must to use</span>
                        <br />
                        <br />

                        <table border="0" width="100%">
                            <tr>

                                <td class="tdUpload">
                                    <span style="color: red">*</span>
                                    <input type="button" id="myButton" class="buttBlue myButton" runat="server" value="Copy of current passport" />
                                    <input id="myButtonInput" type="file" class="myInput" multiple style="display: none" runat="server" />
                                </td>
                                <td class="tdUpload">
                                    <span style="color: red">*</span><input type="button" id="myButton1" class="buttBlue myButton" runat="server" value="Cv" />
                                    <input id="myButton1Input" type="file" class="myInput" multiple style="display: none" runat="server" />
                                </td>
                                <td class="tdUpload">
                                    <span style="color: red">*</span><input type="button" id="myButton2" class="buttBlue myButton" runat="server" value="Certificates" />
                                    <input id="myButton2Input" type="file" class="myInput" multiple style="display: none" runat="server" />

                                </td>

                            </tr>
                            <tr>
                                <td align="center">
                                    <br />
                                </td>

                            </tr>

                            <tr>

                                <td align="center">

                                    <div id="dvmyButtonInput" class="dvFilesUpload">
                                    </div>

                                </td>

                                <td align="center">

                                    <div id="dvmyButton1Input" class="dvFilesUpload">
                                    </div>

                                </td>

                                <td align="center">

                                    <div id="dvmyButton2Input" class="dvFilesUpload">
                                    </div>

                                </td>


                            </tr>


                        </table>



                    </div>

                    <div style="clear: both"></div>

                    <div id="dvfooter" class="dvNavigate">
                        <hr />
                        <br />
                        <input type="button" id="btnAction0" class="buttBlue" runat="server" value="Prev Step" onclick="btnClick(0)" />
                        <input type="button" id="btnAction1" class="buttBlue" runat="server" value="Next Step" onclick="btnClick(1)" />
                        <%-- <input type="button" id="btnAction2" class="buttBlue" runat="server" value="Send Form" onclick="return btnClick(2);" onserverclick="SendClientForm" />
                        --%>
                        <asp:Button ID="btnAction2" runat="server" CssClass="buttBlue" OnClientClick="return btnClick(2);" OnClick="SendClientForm" Text="Send Form" />
                        <%-- <asp:Button ID="Button4" runat="server" Text="Button" OnClick="SendClientForm" />--%>
                    </div>





                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        
    </script>

</body>
</html>


<%--     <br />
                    <hr />
                    <span style="color: #025482; font-size: 19px;"><u>Documents</u></span>
                    <span style="color: red; font-size: 15px;">(*) the Documents with *  must upload</span>
                    <br />
                    <br />

                    <table border="0" class="tblDetalis" width="100%">
                        <tr>

                            <td style="width: 5px;">
                                <span class="spRedDoc">*</span>
                            </td>
                            <td>
                                <span class="spTitle">Copy of current passport: </span>

                            </td>

                            <td>
                                <div>


                                    <span class="buttBlue">
                                        <span class="btn btn-primary btn-file">Add Document
                                                        <input id="fileUpload1" type="file" multiple/>
                                        </span></span>
                                    <input type="text" id="txtUploadFile1" class="form-control" readonly>
                                </div>
                            </td>



                            <td>
                                <span class="spTitle">Document 1:</span>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="TextBox9" CssClass="txtRegister" runat="server" Width="220" Height="25" />
                                    <input type="button" id="Button3" class="buttBlue" runat="server" value="Add Document" />
                                </div>
                            </td>






                        </tr>
                        <tr>
                            <td style="width: 5px;">
                                <span class="spRedDoc">*</span>
                            </td>
                            <td>
                                <span class="spTitle">Cv:</span>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="TextBox7" CssClass="txtRegister" runat="server" Width="220" Height="25" />
                                    <input type="button" id="Button1" class="buttBlue" runat="server" value="Add Document" />
                                </div>
                            </td>


                            <td>
                                <span class="spTitle">Document 2:</span>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="TextBox10" CssClass="txtRegister" runat="server" Width="220" Height="25" />
                                    <input type="button" id="Button4" class="buttBlue" runat="server" value="Add Document" />
                                </div>
                            </td>



                        </tr>
                        <tr>

                            <td style="width: 5px;">
                                <span class="spRedDoc">*</span>
                            </td>

                            <td>
                                <span class="spTitle">Certificates: </span>

                            </td>


                            <td>

                                <div>


                                    <asp:TextBox ID="TextBox8" CssClass="txtRegister" runat="server" Width="220" Height="25" />
                                    <input type="button" id="Button2" class="buttBlue" runat="server" value="Add Document" />

                                </div>
                            </td>

                            <td>
                                <span class="spTitle">Document 3:</span>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="TextBox11" CssClass="txtRegister" runat="server" Width="220" Height="25" />
                                    <input type="button" id="Button5" class="buttBlue" runat="server" value="Add Document" />
                                </div>
                            </td>

                        </tr>
                    </table>


                    <hr />
                    <div style="margin-top: 20px; text-align: left;">
                        <input type="button" id="sasasas" class="buttBlue" onclick="SendClientForm()" runat="server" value="Send Form" />
                    </div>
                    <br />--%>