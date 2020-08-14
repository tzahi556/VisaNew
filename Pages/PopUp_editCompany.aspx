<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUp_editCompany.aspx.cs"
    Inherits="Pages_PopUp_editCustomer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" class="popupHtml">
<head id="Head1" runat="server">
    <title>Customer</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-ui-1.8.23.custom.min.js"
        type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dropkick-1.0.0.js" type="text/javascript"></script>
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/dropkick.css" rel="stylesheet"
        type="text/css" />
    <link href="../App_Themes/jquery-ui-1.8.23.custom/css/smoothness/jquery-ui-1.8.23.custom.css"
   
        rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .dk_fouc select {
            position: relative;
            top: 0em;
            visibility: visible;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {


            $(".txtCalander").datepicker({
                showOn: "button",
                buttonImage: "../App_Themes/Theme1/Images/calendar.gif",
                buttonImageOnly: true,
                dateFormat: "d MM yy"
            });

            $(".ui-datepicker").css("font-size", "11px");

            $(".ui-datepicker-trigger").css("position", "relative").css("left", "3px").css("top", "2px");

            $("#btnSave").button();
            $("#btnCancel").button();
            $("#btnGenerate").button();

            $("select").dropkick();

        });


        function closeDialog() {

            parent.dialog.dialog('close');


        }


        function ClientScriptOnSave() {
            parent.retVal = true;
        }

        function OpenLink() {

            $("#txtLink").val("http://dgtracking.co.il/RegExpert/Register.aspx?CompanyId=" + <%=this.CompanyId%>);

        }


    </script>
</head>
<body class="popupBody">
    <form id="form1" runat="server">
        <div align="center">
            <asp:Label ID="lblTitle" Text="New Company" Font-Bold="true" Font-Size="Large" runat="server"
                Style="display: none"></asp:Label>
            <div class="dvRoundAndBgPopup" style="font-size: medium;">
                <table width="100%" border="0" cellpadding="5" cellspacing="3">
                    <tr>
                        <td align="left">Name:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                ForeColor="red" ErrorMessage="*" ValidationGroup="up" />
                        </td>
                        <td align="left">Number:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
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
                        <td align="left">Address:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAddress" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td align="left">Country:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Interior Reg:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtInteriorReg" runat="server"></asp:TextBox>
                        </td>
                        <td align="left">Parent Company:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true" Width="200px">
                                <asp:ListItem Value="0">---- Choose Parent Company ----</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <br />
                            Active:
                        </td>
                        <td align="left">
                            <br />
                            <asp:CheckBox ID="chActive" runat="server" />
                        </td>
                        <td align="left">
                            <br />
                            Is Expert Allowed:
                        </td>
                        <td align="left">
                            <br />
                            <asp:CheckBox ID="chIsExp" runat="server" />
                            &nbsp;&nbsp;&nbsp;
                          Is Send Email: 
                            <asp:CheckBox ID="chIsEmail" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div class="dvRoundAndBgPopup" style="font-size: medium;">
                <table width="100%" border="0" cellpadding="5" cellspacing="3">
                    <tr>
                        <td align="left">Contact Person:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtContactMan" runat="server"></asp:TextBox>
                        </td>
                        <td align="left">Contact Person Job:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtContactManJob" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Contact Person Email:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtContactManEmail" runat="server"></asp:TextBox>
                        </td>
                        <td align="left">Contact Person Phone:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtContactManPhone" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div class="dvRoundAndBgPopup" style="font-size: medium;">
                <table width="100%" border="0" cellpadding="5" cellspacing="3">
                    <tr>
                        <td align="left">Authorized Signatory:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAuthorizedSigner" runat="server"></asp:TextBox>
                        </td>


                        <td align="left">Passport Country:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPassportCountry" runat="server"></asp:TextBox>
                        </td>




                        <td align="left">Authorized Signatory Job:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAuthorizedSignerJob" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Authorized Signatory Passport:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAuthorizedSignerPassport" runat="server"></asp:TextBox>
                        </td>

                        <td align="right">Israeli ID:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtIsraelID" runat="server"></asp:TextBox>
                        </td>





                        <td align="right">Company Work Field:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtWork" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 9px; margin-right: 5px">
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
               
            </div>
              <div style="padding-top: 9px; margin-right: 5px">
                    <span class="blueButton">
                    <button id="btnGenerate" type="button" runat="server" onclick="OpenLink();"
                        validationgroup="up" style="width: 150px">
                        <img src="../App_Themes/Theme1/Images/save1.jpg" width="16" height="16" style="display: none" />
                        Generate Link
                    </button>
                </span>
                  <input type="text" id="txtLink" style="width:70%" readonly />
              </div>


        </div>
    </form>
</body>
</html>
