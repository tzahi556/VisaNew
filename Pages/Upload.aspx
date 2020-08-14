<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Pages_Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-ui-1.8.23.custom.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dropkick-1.0.0.js" type="text/javascript"></script>
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/dropkick.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/jquery-ui-1.8.23.custom/css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />


    <script src="../akquinet-jquery-toastmessage-plugin-6f5d7bf/src/main/javascript/jquery.toastmessage.js"
        type="text/javascript"></script>
    <link href="../akquinet-jquery-toastmessage-plugin-6f5d7bf/src/main/resources/css/jquery.toastmessage.css"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        td {
            font-size: 14px;
        }
    </style>
    <script type="text/javascript">


        $(document).ready(function () {

            $("#btnUpload").button();

            $("#btnSend").button();


            $("select").dropkick();

            $(".txtCalander").datepicker({
                showOn: "button",
                changeYear: true,
                changeMonth: true,
                buttonImage: "../App_Themes/Theme1/Images/calendar.gif",
                buttonImageOnly: true,
                dateFormat: "d M y"
            });

            $(".ui-datepicker").css("font-size", "10px");

            $(".ui-datepicker-trigger").css("position", "relative").css("left", "3px").css("top", "2px");
        });

        function CheckValidDate() {

            if ($('#<%=txtDate.ClientID%>').val() == "") {

                ShowMessage('Must Choose Date ', '4');
                return false;
            }

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
<body style="background: white;">
    <form id="form1" runat="server">
        <div align="center">
            <div>
                <br />
                <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Large" Text="Manual Send Emails " Style="color: #3a89c6"></asp:Label>
                <br />
                <br />
            </div>
            <div class="dvRoundAndBgPopup" style="width: 80%; margin: 0 auto 0 auto">
                <table border="0" width="50%">
                    <tr>


                        <td>Email Manual Date:
                        </td>
                        <td style="font-size: 14px;" align="left">
                            <asp:TextBox ID="txtDate" runat="server" CssClass="txtCalander"></asp:TextBox>
                        </td>

                        <td align="left">
                            <div class="blueButton">

                                <asp:Button ID="btnSend" runat="server" CssClass="buttBlue" OnClientClick="return CheckValidDate();" OnClick="SendClientEmails" Text=" Send Email" />



                            </div>
                        </td>
                    </tr>
                </table>


            </div>
            <br />


            <div>
                <br />
                <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Size="Large" Text="Upload Excel to DataBase" Style="color: #3a89c6">




                </asp:Label>
                <br />
                <br />
            </div>


            <div class="dvRoundAndBgPopup" style="width: 80%; margin: 0 auto 0 auto">
                <table border="0" style="width: 100%">
                    <tr>

                        <td style="text-align: left">
                            <br />
                            <asp:FileUpload ID="FileUpload1" runat="server" BorderStyle="None" />
                        </td>


                        <td style="text-align: left">Spread Sheet Name<br />
                            <asp:TextBox ID="txtSheet" runat="server" Text="Data"></asp:TextBox>
                        </td>

                        <td style="text-align: left">Company<br />
                            <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Value="0">----  All Companies ----</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCompany" InitialValue="0" ForeColor="red" ErrorMessage="*" />
                        </td>

                        <td style="text-align: left">
                            <br />

                            <div class="blueButton">
                                <button id="btnUpload" type="button" runat="server" onserverclick="Button1_Click">
                                    Upload To DataBase
                                </button>
                            </div>
                        </td>



                    </tr>
                </table>
            </div>
            <br />

            <br />
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>

            <br />
            <br />
            <img src="../App_Themes/Theme1/Images/exlogo.png" />
            <img src="../App_Themes/Theme1/Images/database.png" />
        </div>
    </form>
</body>
</html>
