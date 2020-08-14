<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUp_editExpertRegister.aspx.cs"
    Inherits="Pages_PopUp_editExpertRegister" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" class="popupHtml">
<head id="Head1" runat="server">
    <title>Customer</title>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../ScrollableGridPlugin.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-ui-1.8.23.custom.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dropkick-1.0.0.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/dropkick.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/datatable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/jquery-ui-1.8.23.custom/css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />



    <script type="text/javascript">

        $(document).ready(function () {

            prepareUI();

            $("#tabs").tabs({});

            $("#txtSurname").keyup(function () {
                this.value = this.value.toUpperCase();

            });


            $("#txtName").keyup(function () {
                if (this.value.length == 1) {
                    this.value = this.value.toUpperCase();
                }
            });


        });



        function closeDialog() {
            parent.dialog.dialog('close');
        }

        function ClientScriptOnSave() {
            parent.retVal = true;
        }

        function prepareUI() {

            enableTheme();
        }

        function enableTheme() {

            var RoleId = "<%=RoleId%>";
            //alert(RoleId);

            // למי שהוא לא אדמין הסתרת כל האייקונים של שינוי
            if (RoleId == "1") {

                $(".txtCalander").datepicker({
                    showOn: "button",
                    changeYear: true,
                    changeMonth: true,
                    buttonImage: "../App_Themes/Theme1/Images/calendar.gif",
                    buttonImageOnly: true,
                    dateFormat: "d M yy"
                });

                $(".ui-datepicker").css("font-size", "10px");

                $(".ui-datepicker-trigger").css("position", "relative").css("left", "3px").css("top", "2px");


            }
            else {
                $('INPUT').attr('readonly', 'readonly');
                $('#btnSave').css('display', 'none');
                $('select').attr('disabled', true);
            }

            $("#btnSave,#btnMove").button();
            $("#btnCancel,#btnCancelFam,#btnSaveFam").button();
            $("select").dropkick();

            $("#dk_container_ddlCompany .dk_toggle").css("width", "450px");
        }
        function OpenFamilyExpert(ExpertId, CompanyId, ExpertName) {

            // alert(window.top.location.href);
            window.location.href = "../pages/PopUp_editCustomer.aspx?CompanyId=" + CompanyId + "&ExpertId=" + ExpertId;
            var parentBody = window.parent.document.body;

            $(".ui-dialog-title", parentBody).html(ExpertName);
            // var x = parent.parent.parent.winPopup("pages/PopUp_editCustomer.aspx?CompanyId=" + CompanyId + "&ExpertId=" + ExpertId + "&StepId=" + StepId, '1100', '750', ExpertName);
        }




    </script>
</head>
<body class="popupBody" style="padding: 0px;">
    <form id="form1" runat="server">


        <asp:HiddenField ID="HiddenFieldSoup" runat="server" Value="0"/>
         <asp:HiddenField ID="HiddenFieldChild1" runat="server" Value="0"/>
         <asp:HiddenField ID="HiddenFieldChild2" runat="server" Value="0"/>
         <asp:HiddenField ID="HiddenFieldChild3" runat="server" Value="0"/>
         <asp:HiddenField ID="HiddenFieldChild4" runat="server" Value="0"/>

        <div class="tab-container" style="">
            <div id="tabs" runat="server">
                <ul id="tabHeader">
                    <li><a href="#tabs-1">Expert Details</a></li>
                    <li><a href="#tabs-2">Expert Family</a></li>
                </ul>
                <div id="tabs-1" style="border: solid 1px gray">
                    <div align="center">
                        <asp:Label ID="lblTitle" Text="New Expert" Font-Bold="true" Font-Size="Large" runat="server"
                            Style="display: none"></asp:Label>
                        <div class="dvRoundAndBgPopup" style="font-size: medium; padding-top: 8px; padding-bottom: 5px;">
                            <div class="section-head">
                                Details
                            </div>
                            <table width="100%" border="0" cellpadding="0" cellspacing="10">
                                <tr>
                                    <td align="left" style="width: 60px">Surname:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSurname"
                                            ForeColor="red" ErrorMessage="*" ValidationGroup="up" />
                                    </td>
                                    <td align="left">Name:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rf2" runat="server" ControlToValidate="txtName" ForeColor="red"
                                            ErrorMessage="*" ValidationGroup="up" />
                                    </td>
                                    <td align="left">Date of Birth:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDateBirth" runat="server" CssClass="txtCalander"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Passport:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPassport" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rf3" runat="server" ControlToValidate="txtPassport"
                                            ForeColor="red" ErrorMessage="*" ValidationGroup="up" />
                                    </td>

                                    <td align="left">Passport Issue Date:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPassportIssueDate" runat="server" CssClass="txtCalander"></asp:TextBox>
                                    </td>
                                    <td align="left">Passport Exp Date:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPassportExpDate" runat="server" CssClass="txtCalander"></asp:TextBox>
                                    </td>

                                </tr>

                                <tr>
                                    <td align="left">Address:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">Town:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtTown" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">Country:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                                    </td>

                                </tr>

                                <tr>
                                    <td align="left">Job:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtJob" runat="server"></asp:TextBox>
                                    </td>

                                    <td align="left">Phone:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                                    </td>

                                    <td align="left">Email:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>

                                    <td align="left">Company:
                                    </td>
                                    <td align="left" colspan="5">
                                        <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">----  All Companies ----</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCompany"
                                            InitialValue="0" ForeColor="red" ErrorMessage="*" ValidationGroup="up" />
                                    </td>

                                </tr>


                            </table>

                        </div>

                        <div class="dvRoundAndBgPopup" style="font-size: medium; margin-top: 8px; padding-top: 10px; padding-bottom: 5px; height: 150px;overflow:auto">
                            <div class="section-head">
                                Upload Docs
                            </div>
                            <div id="dvUploadFile" runat="server">
                            </div>
                        </div>
                        <div class="dvRoundAndBgPopup" style="font-size: medium; margin-top: 8px; padding-top: 20px;">
                            <div class="section-head">
                                Comments
                            </div>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left">
                                        <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" Rows="3" Columns="79"
                                            Style="height: 30px; width: 100%;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-top: 8px">



                            <span class="blueButton">
                                <button id="btnSave" type="button" runat="server" onserverclick="btnSave_Click" onclick="ClientScriptOnSave();"
                                    validationgroup="up" style="width: 100px">
                                    <img src="../App_Themes/Theme1/Images/save1.jpg" width="16" height="16" style="display: none" />
                                    Save
                                </button>
                            </span>
                            <button id="btnCancel" type="button" onclick="closeDialog();" style="width: 100px">
                                <img src="../App_Themes/Theme1/Images/close.png" width="16" height="16" style="display: none" />
                                Close
                            </button>

                             <span class="blueButton" style="float:right">
                              <button id="btnMove" type="button"   runat="server" onserverclick="btnMove_Click" onclick="ClientScriptOnSave();" style="width: 200px">
                                <img src="../App_Themes/Theme1/Images/close.png" width="16" height="16" style="display: none" />
                                Move To Live Expert ->
                            </button>

                                 </span>

                        </div>
                    </div>
                </div>
                <div id="tabs-2" style="border: solid 1px gray">

                    <div class="dvRoundAndBgPopup" style="font-size: medium; padding-top: 8px; padding-bottom: 5px;">
                        <div class="section-head">
                            Soup
                        </div>
                        <div>
                            <table width="100%" cellspacing="10">

                                <tr>

                                    <td align="left">Family name: 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSoupFamilyname" runat="server" />
                                    </td>

                                    <td align="left">Given name:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSoupGivenname" runat="server" />
                                    </td>
                                    <td align="left">Maiden name:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSoupMaidenname" runat="server" />
                                    </td>

                                </tr>
                                <tr>



                                    <td align="left">Father’s name:

                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSoupFathersname" runat="server" />
                                    </td>
                                    <td align="left">Place of Birth:

                                    </td>

                                    <td align="left">
                                        <asp:TextBox ID="txtSoupPlaceofBirth" runat="server" />

                                    </td>
                                    <td align="left">Date of Birth:
                                    </td>

                                    <td align="left">
                                        <asp:TextBox ID="txtSoupDateofBirth" CssClass="txtCalander" runat="server" />
                                    </td>


                                </tr>

                                <tr>
                                    <td align="left">Passport:

                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSoupPassport" CssClass="" runat="server" />
                                    </td>
                                    <td align="left">Passport Issue Date:

                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSoupPassportIsueDate" CssClass="txtCalander" runat="server" />
                                    </td>
                                    <td align="left">Passport Exp Date:

                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSoupPassportExpDate" CssClass="txtCalander" runat="server" />
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>


                    <div class="dvRoundAndBgPopup" style="font-size: medium; margin-top: 8px; padding-top: 10px; padding-bottom: 5px;">
                        <div class="section-head">
                            Children under the age of 18
                        </div>
                        <div>
                            <table width="100%" cellspacing="10">
                                <tr>
                                    <td>
                                        <br />

                                    </td>

                                </tr>

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
                                    <td>
                                        <b>1.</b>
                                        <asp:TextBox ID="txtChild1Givenname" CssClass="txtFamelyNumber" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild1Countryofbirth" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild1DateofBirth" CssClass="txtCalander txtFamelyDate" runat="server" />
                                    </td>

                                    <td>
                                        <asp:TextBox ID="txtChild1Passport" CssClass="" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild1PassportIsueDate" CssClass="txtCalander txtFamelyDate" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild1PassportExpDate" CssClass="txtCalander txtFamelyDate" runat="server" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <b>2.</b>
                                        <asp:TextBox ID="txtChild2Givenname" CssClass="txtFamelyNumber" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild2Countryofbirth" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild2DateofBirth" CssClass="txtCalander txtFamelyDate" runat="server" />
                                    </td>

                                    <td>
                                        <asp:TextBox ID="txtChild2Passport" CssClass="" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild2PassportIsueDate" CssClass="txtCalander txtFamelyDate" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild2PassportExpDate" CssClass="txtCalander txtFamelyDate" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>3.</b>
                                        <asp:TextBox ID="txtChild3Givenname" CssClass="txtFamelyNumber" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild3Countryofbirth" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild3DateofBirth" CssClass="txtCalander txtFamelyDate" runat="server" />
                                    </td>

                                    <td>
                                        <asp:TextBox ID="txtChild3Passport" CssClass="" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild3PassportIsueDate" CssClass="txtCalander txtFamelyDate" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild3PassportExpDate" CssClass="txtCalander txtFamelyDate" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>4.</b>
                                        <asp:TextBox ID="txtChild4Givenname" CssClass="txtFamelyNumber" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild4Countryofbirth" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild4DateofBirth" CssClass="txtCalander txtFamelyDate" runat="server" />
                                    </td>

                                    <td>
                                        <asp:TextBox ID="txtChild4Passport" CssClass="" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild4PassportIsueDate" CssClass="txtCalander txtFamelyDate" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChild4PassportExpDate" CssClass="txtCalander txtFamelyDate" runat="server" />
                                    </td>
                                </tr>









                            </table>


                        </div>



                    </div>

                      <div style="margin-top: 8px;text-align:center" >
                            <span class="blueButton">
                                <button id="btnSaveFam" type="button" runat="server" onserverclick="btnSave_Click" onclick="ClientScriptOnSave();"
                                    validationgroup="up" style="width: 100px">
                                    <img src="../App_Themes/Theme1/Images/save1.jpg" width="16" height="16" style="display: none" />
                                    Save
                                </button>
                            </span>
                            <button id="btnCancelFam" type="button" onclick="closeDialog();" style="width: 100px">
                                <img src="../App_Themes/Theme1/Images/close.png" width="16" height="16" style="display: none" />
                                Close
                            </button>
                        </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        enableTheme();
    </script>
</body>
</html>
