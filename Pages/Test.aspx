<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Pages_Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-ui-1.8.23.custom.min.js"
        type="text/javascript"></script>
    <link href="../App_Themes/jquery-ui-1.8.23.custom/css/smoothness/jquery-ui-1.8.23.custom.css"
        rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">



        function OpenCustomerDetails(ExpertId) {


            var x = parent.winPopup("pages/PopUp_editCustomer.aspx?ExpertId=" + ExpertId, '800px', '470px', 'Expert');
            //alert(x);

        }

        function ReturnValueFromModal(val) {
            if (val) {
                $("#btnSearch").click();
                //parent.setIframeContent('Pages/Expert.aspx');
            }
        }


        $(document).ready(function () {





        });


    </script>
</head>
<body style="">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="dvRoundAndBg" style="">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <table width="100%" border="0">
                        <%-- <tr>
                <td>
                  <div style="background:transparent url('../App_Themes/Theme1/Images/green_flag.jpg');background-repeat:no-repeat">
                  
                  dfdffd
                  </div>
                
                </td>
               
               </tr>--%>
                        <tr>
                            <td align="left">
                                <fieldset style="text-align: left;">
                                    <legend>Statstic</legend>&nbsp; &nbsp; In Progress - <span id="sp0" class="spStastic"
                                        runat="server">0</span>| Step 1 - <span id="sp1" class="spStastic" runat="server">0</span>|
                                    Step 2 - <span id="sp2" class="spStastic" runat="server">0</span>| Step 3 - <span
                                        id="sp3" class="spStastic" runat="server">0</span>
                                </fieldset>
                            </td>
                            <td>
                                Surname:
                                <asp:TextBox ID="txtSurname" runat="server" AutoPostBack="true" OnTextChanged="txt_TextChanged"></asp:TextBox>
                            </td>
                            <td align="left">
                                Name:
                                <asp:TextBox ID="txtName" runat="server" AutoPostBack="true" OnTextChanged="txt_TextChanged"></asp:TextBox>
                            </td>
                            <td align="left">
                                Company:
                                <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCompany_SelectedChange"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="0">-- All Companies --</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <button type="button" id="btnSearch" runat="server" onserverclick="btnSearch_Click">
                                    <img src="../App_Themes/Theme1/Images/search.png" width="16" height="16" />
                                    Search
                                </button>
                            </td>
                        </tr>
                        <%--   <tr>
                    <td>
                        Start:
                        <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        end:
                        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                    </td>
                   
                </tr>--%>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <div id="tabs" runat="server">
            <div id="tabs-1">
                <div class="dvRoundAndBg">
                    <div id="dvGridContainer" style="height:auto;overflow:auto; overflow-x: hidden">
                        <div style="padding-right: 10px">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvTheGrid" runat="server" AllowPaging="true" PageSize="10" GridLines="Both" CellPadding="3" AutoGenerateColumns="false"
                                        BackColor="WhiteSmoke"  AllowSorting="true" OnSorting="gvTheGrid_Sorting"
                                    OnPageIndexChanging="gridView_PageIndexChanging"    Width="100%" CssClass="myGrid myGridInTabs" 
                                        EmptyDataText="No Expert Found!" OnRowDataBound="gvTheGrid_RowDataBound" EmptyDataRowStyle-Font-Bold="true"
                                        EmptyDataRowStyle-Font-Size="Medium" DataKeyNames="diffday,ExpertId" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <img id="imgEdit" style="padding: 2px;" title="Edit" onclick="OpenCustomerDetails('<%# Eval("ExpertId") %>')"
                                                        src="../App_Themes/Theme1/images/edit.jpg" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chCustomer" runat="server" Width="20" />
                                                    <%-- <asp:Label ID="Label1" runat="server" Text='<%# Bind("ExpertId") %>'></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flag" SortExpression="diffday">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgStatus" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Theme1/Images/transparanet.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Surname" HeaderText="Surname" ItemStyle-Width="100px"
                                                SortExpression="Surname"></asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="100px" SortExpression="Name">
                                            </asp:BoundField>
                                            <%--   <asp:BoundField DataField="Client authorization Date" HeaderText="Client authorization Date"></asp:BoundField>--%>
                                            <asp:BoundField DataField="Approval Submition Date" HeaderText="Approval Submition "
                                                ItemStyle-Width="180px" SortExpression="ASDate"></asp:BoundField>
                                            <asp:BoundField DataField="Approval Exp Date" HeaderText="Approval Exp " SortExpression="AEDate">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Visa/ Invitation Issue date" HeaderText="Visa/Invitation Issue "
                                                SortExpression="VIIDate"></asp:BoundField>
                                            <asp:BoundField DataField="Visa Exp Date" HeaderText="Visa Exp " SortExpression="VEDate">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Multiple entry Visa" HeaderText="Multiple Entry Visa"
                                                SortExpression="MEVDate"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div id="tabs-2">
                <div class="dvRoundAndBg">
                    <div id="dvArchive" style="overflow: auto; overflow-x: hidden">
                        <div style="padding-right: 10px">
                            <asp:GridView ID="GridView1" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="false"
                                BackColor="WhiteSmoke" AllowPaging="false" AllowSorting="true" OnSorting="GridView1_Sorting"
                                Width="100%" CssClass="myGrid myGridInTabs" EmptyDataText="No Expert Archive Found!"
                                OnRowDataBound="GridView1_RowDataBound" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Medium"
                                DataKeyNames="diffday,ExpertId">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <img id="imgEdit" style="padding: 2px;" title="Edit" onclick="OpenCustomerDetails('<%# Eval("ExpertId") %>')"
                                                src="../App_Themes/Theme1/images/edit.jpg" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chCustomer" runat="server" Width="20" />
                                            <%-- <asp:Label ID="Label1" runat="server" Text='<%# Bind("ExpertId") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Flag" SortExpression="diffday">
                                        <ItemTemplate>
                                            <asp:Image ID="imgStatus" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Theme1/Images/transparanet.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Surname" ItemStyle-Width="100px" HeaderText="Surname"
                                        SortExpression="Surname"></asp:BoundField>
                                    <asp:BoundField DataField="Name" ItemStyle-Width="100px" HeaderText="Name" HeaderStyle-CssClass=""
                                        SortExpression="Name"></asp:BoundField>
                                    <%--   <asp:BoundField DataField="Client authorization Date" HeaderText="Client authorization Date"></asp:BoundField>--%>
                                    <asp:BoundField DataField="Approval Submition Date" HeaderText="Approval Submition "
                                        SortExpression="ASDate"></asp:BoundField>
                                    <asp:BoundField DataField="Approval Exp Date" HeaderText="Approval Exp " SortExpression="AEDate">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Visa/ Invitation Issue date" HeaderText="Visa/Invitation Issue "
                                        SortExpression="VIIDate"></asp:BoundField>
                                    <asp:BoundField DataField="Visa Exp Date" HeaderText="Visa Exp " SortExpression="VEDate">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Multiple entry Visa" HeaderText="Multiple Entry Visa"
                                        SortExpression="MEVDate"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div id="tabs-3">
                <div class="dvRoundAndBg">
                    <div id="dvDocs" style="height:300px; overflow: auto; overflow-x: hidden">
                        <div style="padding-right: 10px">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvDocs" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="false"
                                        BackColor="WhiteSmoke"  AllowPaging="true" PageSize="10" DataKeyNames="ExpertId,Cv,Diploma,MarriageCertificate"
                                        OnRowEditing="gvDocs_RowEditing" OnRowCancelingEdit="gvDocs_RowCancelingEdit"
                                        OnRowUpdating="gvDocs_RowUpdating" Width="100%" CssClass="myGrid myGridInTabs"
                                        EmptyDataRowStyle-Font-Size="Medium" EmptyDataRowStyle-Font-Bold="true" EmptyDataText="No Expert!!"
                                        AllowSorting="true" OnSorting="gvDocs_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="Surname" ItemStyle-Width="100px" ReadOnly="true" HeaderText="Surname"
                                                SortExpression="Surname"></asp:BoundField>
                                            <asp:BoundField DataField="Name" ItemStyle-Width="100px" ReadOnly="true" HeaderText="Name"
                                                HeaderStyle-CssClass="" SortExpression="Name"></asp:BoundField>
                                            <asp:TemplateField HeaderText="C.Passport" SortExpression="CopyofPassport">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chCopy" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("CopyofPassport").ToString())%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="chCopy" runat="server" Checked='<%#bool.Parse(Eval("CopyofPassport").ToString())%>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Diploma" HeaderStyle-Width="100" SortExpression="Diploma">
                                                <ItemTemplate>
                                                    <%# Eval("DiplomaT")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDiploma" runat="server" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">TBT</asp:ListItem>
                                                        <asp:ListItem Value="3">T Done</asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cv" HeaderStyle-Width="100" SortExpression="Cv">
                                                <ItemTemplate>
                                                    <%# Eval("CvT")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlCv" runat="server" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">TBT</asp:ListItem>
                                                        <asp:ListItem Value="3">T Done</asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Copy I.D." ItemStyle-Width="80px" SortExpression="CopyID">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chId" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("CopyID").ToString())%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="chId" runat="server" Checked='<%#bool.Parse(Eval("CopyID").ToString())%>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AL-33" ItemStyle-Width="60px" SortExpression="AL33">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ch33" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("AL33").ToString())%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="ch33" runat="server" Checked='<%#bool.Parse(Eval("AL33").ToString())%>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AL-15" ItemStyle-Width="60px" SortExpression="AL15">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ch15" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("AL15").ToString())%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="ch15" runat="server" Checked='<%#bool.Parse(Eval("AL15").ToString())%>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Obligo" SortExpression="EmployerObligo">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chEmployerObligo" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("EmployerObligo").ToString())%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="chEmployerObligo" runat="server" Checked='<%#bool.Parse(Eval("EmployerObligo").ToString())%>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Affidavit" SortExpression="Affidavit">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chAffidavit" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("Affidavit").ToString())%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="chAffidavit" runat="server" Checked='<%#bool.Parse(Eval("Affidavit").ToString())%>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Marriage" HeaderStyle-Width="100" SortExpression="MarriageCertificate">
                                                <ItemTemplate>
                                                    <%# Eval("MarriageCertificateT")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlMarriageCertificate" runat="server" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">TBT</asp:ListItem>
                                                        <asp:ListItem Value="3">T Done</asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Photo" SortExpression="Photo">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chPhoto" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("Photo").ToString())%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="chPhoto" runat="server" Checked='<%#bool.Parse(Eval("Photo").ToString())%>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attorney" SortExpression="PowerofAttorney">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chPowerofAttorney" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("PowerofAttorney").ToString())%>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="chPowerofAttorney" runat="server" Checked='<%#bool.Parse(Eval("PowerofAttorney").ToString())%>' />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Edit
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEdit" runat="server" AlternateText="Edit" CommandName="Edit"
                                                        ImageUrl="~/App_Themes/Theme1/Images/edit2.jpg" ToolTip="Edit" Style="padding: 4px" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="imgCancel" runat="server" AlternateText="Cancel" CommandName="CANCEL"
                                                        ImageUrl="~/App_Themes/Theme1/Images/cancel.jpg" ToolTip="Cancel" CausesValidation="false"
                                                        Style="" />
                                                    <asp:ImageButton ID="imgUpdate" runat="server" AlternateText="Save" CommandName="Update"
                                                        ImageUrl="~/App_Themes/Theme1/Images/save1.jpg" ValidationGroup="edit" ToolTip="Save"
                                                        Style="" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--    <button id="btnSave" type="button" runat="server" onserverclick="btnSave_Click">
                                        <img src="../App_Themes/Theme1/Images/save1.jpg" />
                                        Save
                                    </button>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="margin-top: 4px; float: left">
            <button id="btnNew" type="button" runat="server" onclick="OpenCustomerDetails(-1);">
                <img src="../App_Themes/Theme1/Images/insert.jpg" />
                New Expert
            </button>
        </div>
        <div style="margin-top: 0px; float: right" align="center">
            <fieldset>
                <legend>Flags</legend>
                <img src="../App_Themes/Theme1/Images/red_flag.png" />
                0 - 74 Days. &nbsp; &nbsp; &nbsp;
                <img src="../App_Themes/Theme1/Images/orange_flag.png" />
                75 - 160 Days. &nbsp; &nbsp; &nbsp;
                <img src="../App_Themes/Theme1/Images/green_flag.png" />
                160 + Days.
            </fieldset>
        </div>
    </div>
    </form>
</body>
</html>
