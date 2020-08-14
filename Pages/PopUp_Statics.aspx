<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUp_Statics.aspx.cs" EnableEventValidation="false"
    Inherits="Pages_Expert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" class="popupHtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-ui-1.8.23.custom.min.js" type="text/javascript"></script>
    <link href="../App_Themes/jquery-ui-1.8.23.custom/css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dataTables.min.js" type="text/javascript" ></script>
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/dropkick.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/datatable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    
		th div {
			font-size: 12px;
		}

    </style>
    <script type="text/javascript">

        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            // prtContent.innerHTML = strOldOne;
        }

        $(document).ready(function () {


            //  $("#dvGridContainer").css("height", 0.51 * eval(parent.MainHeight) + "px");
        });

        function getListHeight() {

            var listHeight = 300;

            return listHeight;
        }

        function SearchStatic(type) {

            // window.location.href = "PopUp_Statics.aspx?Filter=" + type;
            document.getElementById("hdnFilter").value = type;
            return true;

        }

        function EnableThemes() {

            var listHeight = getListHeight();

            $("#dvProgress").show();
            setTimeout(function () {

                try {
                    $("#<%=gvTheGrid.ClientID%>").dataTable({
                        "bJQueryUI": true,
                        "bPaginate": false,
                        "bFilter": false,
                        "bInfo": false,
                        "aaSorting": [],
                        "sScrollY": "" + (listHeight - 25) + "px"
                    });
                }
                catch (err) { }

                $("#dvProgress").hide();

            }, 100);

            $("button").button();
        }

        function pageLoad() {

            EnableThemes();
        }

    </script>
</head>
<body class="popupBody">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <fieldset style="text-align: center; padding-bottom:5px;">
                    <legend>Statistic</legend>
                    <table border="0" width="100%" cellpadding="5" cellspacing="5">
                        <tr>
                            <td>
                                <button id="Button1" type="button" runat="server" onserverclick="btnStatic_Click"
                                    onclick="SearchStatic(0);">
                                    <img src="../App_Themes/Theme1/Images/search.png" width="16" height="16" />
                                    Total
                                </button>
                                &nbsp;&nbsp;<span id="sp0" class="spStastic" runat="server">0</span>
                            </td>
                            <td>
                                <button id="Button2" type="button" runat="server" onserverclick="btnStatic_Click"
                                    onclick="SearchStatic(1);">
                                    <img src="../App_Themes/Theme1/Images/search.png" width="16" height="16" />
                                    In Progress
                                </button>
                                &nbsp;&nbsp;<span id="sp1" class="spStastic" runat="server">0</span>
                            </td>
                            <td>
                                <button id="Button3" type="button" runat="server" onserverclick="btnStatic_Click"
                                    onclick="SearchStatic(2);">
                                    <img src="../App_Themes/Theme1/Images/search.png" width="16" height="16" />
                                    Step 1
                                </button>
                                &nbsp;&nbsp;<span id="sp2" class="spStastic" runat="server">0</span>
                            </td>
                            <td>
                                <button id="Button4" type="button" runat="server" onserverclick="btnStatic_Click"
                                    onclick="SearchStatic(3);">
                                    <img src="../App_Themes/Theme1/Images/search.png" width="16" height="16" />
                                    Step 2
                                </button>
                                &nbsp;&nbsp;<span id="sp3" class="spStastic" runat="server">0</span>
                            </td>
                            <td>
                            </td>
                            <td>
                                <button id="Button5" type="button" runat="server" onserverclick="btnStatic_Click"
                                    onclick="SearchStatic(4);">
                                    <img src="../App_Themes/Theme1/Images/search.png" width="16" height="16" />
                                    Step 3
                                </button>
                                &nbsp;&nbsp;<span id="sp4" class="spStastic" runat="server">0</span>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hdnFilter" Value="0" runat="server" />
        <div style="padding: 10px">
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <button id="Button6" type="button" runat="server" onserverclick="btnStatic_Click"
                                    onclick="SearchStatic(5);">
                                    <img src="../App_Themes/Theme1/Images/red_flag.png" width="16" height="16" />
                                    0 - 90
                                </button>
                                &nbsp;&nbsp;<span id="sp5" class="spStastic" runat="server">0</span>
                                <button id="Button7" type="button" runat="server" onserverclick="btnStatic_Click"
                                    onclick="SearchStatic(6);">
                                    <img src="../App_Themes/Theme1/Images/orange_flag.png" width="16" height="16" />
                                    90 - 120
                                </button>
                                &nbsp;&nbsp;<span id="sp6" class="spStastic" runat="server">0</span>
                                <button id="Button8" type="button" runat="server" onserverclick="btnStatic_Click"
                                    onclick="SearchStatic(7);">
                                    <img src="../App_Themes/Theme1/Images/green_flag.png" width="16" height="16" />
                                    120 +
                                </button>
                                &nbsp;&nbsp;<span id="sp7" class="spStastic" runat="server">0</span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td align="right">
                        <button id="btnExcel" type="button" runat="server" onserverclick="btnExcel_Click"
                            style="width: 100px">
                            <img src="../App_Themes/Theme1/Images/excel.png" width="16" height="16" />
                            Excel
                        </button>
                        <button id="btnPrint" type="button" onclick="CallPrint('dvGridContainer');" style="width: 100px">
                            <img src="../App_Themes/Theme1/Images/printer.png" width="16" height="16" />
                            Print
                        </button>
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvGridContainer" style="height: 335px; overflow: auto; overflow-x: hidden">
            <div style="padding: 5px; position: relative;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvTheGrid" runat="server" GridLines="Both" AutoGenerateColumns="false"
                            BackColor="WhiteSmoke" AllowPaging="false" AllowSorting="false" OnSorting="gvTheGrid_Sorting"
                            Width="100%" CssClass="myGrid tdPadding" OnPageIndexChanging="gvTheGrid_PageIndexChanging"
                            EmptyDataText="No Expert Found!" OnRowDataBound="gvTheGrid_RowDataBound" EmptyDataRowStyle-Font-Bold="true"
                            EmptyDataRowStyle-Font-Size="Medium" DataKeyNames="diffday,ExpertId">
                            <Columns>
                                <asp:TemplateField HeaderText="diffday" SortExpression="diffday">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldiff" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:BoundField DataField="CompanyName" ItemStyle-Width="150" ItemStyle-Font-Size="Small"
                                    HeaderText="Company" SortExpression="CompanyName"></asp:BoundField>

                                <asp:BoundField DataField="Surname" ItemStyle-Width="150" ItemStyle-Font-Size="Small"
                                    HeaderText="Surname" SortExpression="Surname"></asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="90" SortExpression="Name">
                                </asp:BoundField>
                                <asp:BoundField DataField="Approval Submition Date" HeaderText="Approval Submission "
                                    SortExpression="ASDate"></asp:BoundField>
                                <asp:BoundField DataField="Approval Exp Date" HeaderText="Approval Expire" SortExpression="AEDate">
                                </asp:BoundField>
                                <asp:BoundField DataField="Visa/ Invitation Issue date" HeaderText="Visa/Invitation"
                                    SortExpression="VIIDate"></asp:BoundField>
                                <asp:BoundField DataField="Visa Exp Date" HeaderText=" Visa Expire" SortExpression="VEDate">
                                </asp:BoundField>
                                <asp:BoundField DataField="Multiple entry Visa" HeaderText="Multiple Visa" SortExpression="MEVDate">
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="dvProgress" style="position: absolute; background: white; left: 0px; top:0px; right: 0px; bottom: 0px">
    	<div style="position: absolute; top: 50%; left: 50%; margin-top: -16px; margin-left: -32px;">
    		<img src="../App_Themes/Theme1/Images/ajax-loader.gif" width="32px" height="32px"/>
    	</div>
    </div>
    </form>
</body>
</html>
