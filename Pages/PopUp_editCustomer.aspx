<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUp_editCustomer.aspx.cs"
    Inherits="Pages_PopUp_editCustomer" %>

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

        function RedirectToCompany() {


           // parent.ChooseCompany();
            var CompanyId = $('#<%= ddlCompany.ClientID %>').val();
            //parent.dialog.ChooseCompany(CompanyId);
            parent.retValCompanyId = CompanyId;
            closeDialog();

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

            $("#btnSave").button();
            $("#btnCancel").button();
            $("#btnGoTo").button();

            $("select").dropkick();

            $("#dk_container_ddlCompany .dk_toggle").css("width", "200px");
        }
        function OpenFamilyExpert(ExpertId, CompanyId, ExpertName) {

            // alert(window.top.location.href);
            window.location.href = "../pages/PopUp_editCustomer.aspx?CompanyId=" + CompanyId + "&ExpertId=" + ExpertId;
            var parentBody = window.parent.document.body;

            $(".ui-dialog-title", parentBody).html(ExpertName);
            // var x = parent.parent.parent.winPopup("pages/PopUp_editCustomer.aspx?CompanyId=" + CompanyId + "&ExpertId=" + ExpertId + "&StepId=" + StepId, '1100', '750', ExpertName);
        }

        function ReturnToParent() {

            var ExpertId = $('#<%= ddlParents.ClientID %>').val();

            if (ExpertId == "0") return;

            var CompanyId = $('#<%= ddlCompany.ClientID %>').val();
            var ExpertName = $("#ddlParents option:selected").text();

            OpenFamilyExpert(ExpertId, CompanyId, ExpertName);

        }


    </script>
</head>
<body class="popupBody" style="padding: 0px;">
    <form id="form1" runat="server">
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
                            <table width="100%" border="0" cellpadding="0" cellspacing="3">
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
                                    <td align="left">Phone:
                                    </td>
                                    <td align="left" colspan="4">
                                        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
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
                                    <td align="left" colspan="4">
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
                                    <td align="left" colspan="4">
                                        <asp:TextBox ID="txtCountry" runat="server" Style=""></asp:TextBox>
                                    </td>


                                </tr>

                                <tr>
                                    <td align="left">Email:
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtEmail" runat="server" ></asp:TextBox>
                                    </td>
                                   
                                  
                                    <td align="left">Title:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtJob" runat="server"></asp:TextBox>
                                    </td>

                                    <td align="left">Title Hebrew:
                                    </td>
                                    <td align="left" colspan="4">
                                        <asp:TextBox ID="txtJobHeb" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="left">Nationality:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNationality" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">D.O.B:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDateofBirth" runat="server" CssClass="txtCalander"></asp:TextBox>
                                    </td>

                                    <td align="left">Send Email:
                                        <asp:CheckBox ID="chIsEmail" runat="server" />
                                    </td>



                                    <td align="left">Active:&nbsp;<asp:CheckBox ID="chActive" runat="server" />
                                    </td>

                                    <td>Days to Exp:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDiff" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                        &nbsp;
                                    </td>



                                </tr>


                                <tr>
                                      <td align="left">Company:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">----  All Companies ----</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCompany"
                                            InitialValue="0" ForeColor="red" ErrorMessage="*" ValidationGroup="up" />
                                    </td>
                                    <td align="left" style="cursor: hand" onclick="ReturnToParent()">Parent Expert:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlParents" runat="server" AppendDataBoundItems="true" Width="120">
                                            <asp:ListItem Value="0">----  No Parent ----</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>


                                     <td align="left" >45 Days:&nbsp;<asp:CheckBox ID="chIsMonthly" runat="server" />
                                    </td>
                                     <td align="left" >90 Days:&nbsp;<asp:CheckBox ID="chIs90" runat="server" />
                                    </td>

                                     <td align="right">Level:
                                    </td>

                                    <td  align="left" colspan="2">

                                        <asp:DropDownList ID="ddlLevel" runat="server" AppendDataBoundItems="true" Width="60%">
                                            <asp:ListItem Value="">-No-</asp:ListItem>
                                            <asp:ListItem Value="HL">HL</asp:ListItem>
                                            <asp:ListItem Value="LL">LL</asp:ListItem>
                                            <asp:ListItem Value="BV">BV</asp:ListItem>
                                            <asp:ListItem Value="EXV">EXV</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                </tr>




                            </table>
                            <table width="100%" border="1" cellpadding="0" cellspacing="3" style="display:none">
                                <tr>
                                    
                                   <%-- <td align="left">Nationality Heb:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNationalityHeb" runat="server" Style="width: 80px"></asp:TextBox>
                                    </td>
                                    <td>Title Abroad:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtJobAbroad" runat="server" Style=""></asp:TextBox>
                                    </td>--%>
                                    <%--<td>Send Email:
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chIsEmail" runat="server" />
                                    </td>--%>
                                    <%--<td align="left">Active:
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chActive" runat="server" />
                                    </td>
                                    <td>Days to Exp:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDiff" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                        &nbsp;
                                    </td>--%>
                                </tr>
                               <%--    <tr>
                                 <td align="left">Email:
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:TextBox ID="txtEmail" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td style="cursor: hand" onclick="ReturnToParent()">Parent Expert:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlParents" runat="server" AppendDataBoundItems="true" Width="120">
                                            <asp:ListItem Value="0">----  No Parent ----</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td>45 Days:
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chIsMonthly" runat="server" />
                                    </td>
                                    <td>Level:
                                    </td>

                                    <td colspan="4">

                                        <asp:DropDownList ID="ddlLevel" runat="server" AppendDataBoundItems="true" Width="50">
                                            <asp:ListItem Value="">-No-</asp:ListItem>
                                            <asp:ListItem Value="HL">HL</asp:ListItem>
                                            <asp:ListItem Value="LL">LL</asp:ListItem>
                                            <asp:ListItem Value="BV">BV</asp:ListItem>
                                            <asp:ListItem Value="EXV">EXV</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>





                                </tr>--%>
                            </table>
                        </div>
                        <div class="dvRoundAndBgPopup" style="font-size: medium; margin-top: 8px; padding-top: 10px; padding-bottom: 5px">
                            <div class="section-head">
                                Step 1
                            </div>
                            <table width="100%" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                     <td align="left">Application ref:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtApplicationref" runat="server" Style="width: 80px" MaxLength="9"></asp:TextBox>

                                        <asp:TextBox ID="txtClientDate" runat="server" CssClass="txtCalander" Visible="false" Style="width: 80px"></asp:TextBox>
                                    </td>
<%--                                <td align="left">Client Authorization Date:
                                    </td>
                                    <td align="left">
                                        
                                    </td>--%>


                                    <td align="left">Approval Submission Date:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAppSub" runat="server" CssClass="txtCalander" Style="width: 80px"></asp:TextBox>
                                    </td>
                                    <td align="left">Approval from Date:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAppStart" runat="server" CssClass="txtCalander" Style="width: 80px"></asp:TextBox>
                                    </td>
                                    <td align="left">Approval Exp Date:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAppExp" runat="server" CssClass="txtCalander" Style="width: 80px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="dvRoundAndBgPopup" style="font-size: medium; margin-top: 8px; padding-top: 10px; padding-bottom: 5px;">
                            <div class="section-head">
                                Step 2
                            </div>
                            <table width="100%" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td align="left">Visa/Invitation Issue Date:
                                    <asp:TextBox ID="txtInviteVisa" runat="server" CssClass="txtCalander"></asp:TextBox>
                                    </td>
                                    <td align="left">Visa Exp Date:
                                    <asp:TextBox ID="txtExpVisa" runat="server" CssClass="txtCalander"></asp:TextBox>
                                    </td>
                                    <td align="left">First Entry:
                                    <asp:TextBox ID="txtFirstEntry" runat="server" CssClass="txtCalander"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="dvRoundAndBgPopup" style="font-size: medium; margin-top: 8px; padding-top: 10px; padding-bottom: 5px;">
                            <div class="section-head">
                                Step 3
                            </div>
                            <table width="100%" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td align="left">Multiple Visa Issue Date:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSubmitMulti" runat="server" CssClass="txtCalander"></asp:TextBox>
                                    </td>
                                    <td align="left">Multiple Visa Exp Date:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtExpMulti" runat="server" CssClass="txtCalander"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
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
                              <button id="btnGoTo" type="button" onclick="RedirectToCompany();" >
                                <img src="../App_Themes/Theme1/Images/save1.jpg"  height="16" style="display: none" />
                                Go To Company Experts
                            </button>
                                 </span>


                        </div>
                    </div>
                </div>
                <div id="tabs-2" style="border: solid 1px gray">
                    <div>
                        <asp:GridView ID="gvTheGrid" runat="server" GridLines="Both" CellPadding="5" AutoGenerateColumns="false"
                            HeaderStyle-BackColor="WhiteSmoke" AllowPaging="false" AllowSorting="false" Width="100%"
                            CssClass="myGrid myGridInTabs" EmptyDataText="No Family Found!" EmptyDataRowStyle-Font-Bold="true"
                            EmptyDataRowStyle-HorizontalAlign="center" DataKeyNames=""
                            HeaderStyle-Height="35" RowStyle-Height="30">
                            <Columns>

                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <img id="imgEdit" style="padding: 3px;" title="Edit" onclick="OpenFamilyExpert( <%#Eval("ExpertId").ToString()%>, <%#Eval("CompanyId").ToString()%>, '<%# Eval("Surname") %>&nbsp;<%# Eval("Name") %>')"
                                            src="../App_Themes/Theme1/images/edit3.png" width="20px" height="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Surname" SortExpression="Surname">
                                    <ItemTemplate>
                                        &nbsp;  <%#Eval("Surname").ToString()%>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                    <ItemTemplate>
                                        &nbsp; <%#Eval("Name").ToString()%>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Passport" SortExpression="ContactMan">
                                    <ItemTemplate>
                                        &nbsp; <%#Eval("Passport").ToString()%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--   <asp:TemplateField HeaderText="Parent Company" SortExpression="ParentCompany" ItemStyle-Width="210">
                                <ItemTemplate>
                                    <%#Eval("ParentCompany").ToString()%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <%--   <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Delete" CommandArgument=''
                                        CommandName="Delete" ImageUrl="~/App_Themes/Theme1/Images/delete.png" Style="padding: 4px"
                                        ToolTip="Delete" CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
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
