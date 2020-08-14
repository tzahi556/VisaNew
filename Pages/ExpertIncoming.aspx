<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpertIncoming.aspx.cs" EnableEventValidation="false"
    Inherits="Pages_ExpertIncoming" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-ui-1.8.23.custom.min.js"
        type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.inputhints.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dropkick-1.0.0.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/normalize.css">
    <%-- <link rel="stylesheet" href="css/stylesheet.css">--%>
    <script src="dist/js/standalone/selectize.js"></script>
    <script src="js/index.js"></script>
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/dropkick.css" rel="stylesheet"
        type="text/css" />
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/datatable.css" rel="stylesheet"
        type="text/css" />
    <link href="../App_Themes/jquery-ui-1.8.23.custom/css/smoothness/jquery-ui-1.8.23.custom.css"
        rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />
    <!--[if IE 8]><script src="js/es5.js"></script><![endif]-->
    <%-- <script src="js/jquery.min.js"></script>--%>


    <script type="text/javascript">



        /* Create an array with the values of all the checkboxes in a column */
        $.fn.dataTableExt.afnSortData['dom-checkbox'] = function (oSettings, iColumn) {
            var aData = [];
            $('td:eq(' + iColumn + ') input', oSettings.oApi._fnGetTrNodes(oSettings)).each(function () {
                aData.push(this.checked == true ? "1" : "0");
            });
            return aData;
        }

        /* Create an array with the values of all the checkboxes in a column */
        $.fn.dataTableExt.afnSortData['dom-indicator'] = function (oSettings, iColumn) {
            var aData = [];
            $('td:eq(' + iColumn + ') img', oSettings.oApi._fnGetTrNodes(oSettings)).each(function () {
                aData.push($(this).attr("title") ? $(this).attr("title") : '');
            });

            return aData;
        }

        $.fn.dataTableExt.afnSortData['dom-date'] = function (oSettings, iColumn) {
            var aData = [];
            $('td:eq(' + iColumn + ')', oSettings.oApi._fnGetTrNodes(oSettings)).each(function () {

                //var d = $(this).html().split(" ");
                var d = $(this).text().trim();
                if (d != "") {

                    var ds = d.split(" ");
                    var year = ds[2];
                    var smonth = ds[1];
                    var day = ds[0];
                    var month = 0;

                    switch (smonth.toLowerCase()) {

                        case "jan": month = "01"; break;
                        case "feb": month = "02"; break;
                        case "mar": month = "03"; break;
                        case "apr": month = "04"; break;
                        case "may": month = "05"; break;
                        case "jun": month = "06"; break;
                        case "jul": month = "07"; break;
                        case "aug": month = "08"; break;
                        case "sep": month = "09"; break;
                        case "oct": month = "10"; break;
                        case "nov": month = "11"; break;
                        case "dec": month = "12"; break;
                    }

                    var s = year + " " + month + " " + day;
                    aData.push(s);

                } else {

                    aData.push("");
                }

            });
            return aData;
        }


        var Selected = "0";

        function OpenCustomerDetails(ExpertId, ExpertName, CompanyId,IsArchive) {

            if (ExpertId) {
                var x = parent.winPopup("pages/PopUp_editExpertRegister.aspx?IsArchive=" + IsArchive + "&CompanyId=" + CompanyId + "&ExpertId=" + ExpertId + "&ts=" + new Date().getTime(), '1100', '630', ExpertName);

            }

            //alert(x);

        }



        function UpdateSelectedOnPostBack() {

            Selected = "0";

            $('.myGrid tr').bind('click', function (e) {

                $('td:not(td.neg):not(td.sorting_1)').css('background-color', 'transparent');
                $('td.neg').css('background-color', '#EECCCC');
                $('tr.odd td.sorting_1').css('background-color', '#D3D6FF');
                $('tr.even td.sorting_1').css('background-color', '#EAEBFF');
                $('td.neg.sorting_1').css('background-color', '#BB99AA');

                $(e.currentTarget).children('td:not(td.neg):not(td.sorting_1)').css('background-color', '#B0C2D4');
                $(e.currentTarget).children('td.neg').css('background-color', '#C2A0A');
                $(e.currentTarget).children('td.sorting_1').css('background-color', '#B3B6CF');
            })

        }

        function ReturnValueFromModal(val) {

            if (val) {
                $("#btnSearch").click();

                parent.retVal = false;
            }
        }

        function UpdateAllChecbox(Obj) {
            if (Obj.checked) {
                $("[id$=chCustomer]:not(:checked)").trigger('click');
            } else {
                $("[id$=chCustomer]:checked").trigger('click');
            }

        }

        function ChecBoxChange(Obj, ExpertId) {

            if (Obj.checked) {
                Selected = Selected + "," + ExpertId;
            }
            else {
                Selected = Selected.replace("," + ExpertId, "");
            }


        }

        function EnableTheme() {
            $("#btnSearch").button();
            //******************************************************************************8
            $("#dvIncomingProgress").show();

            setTimeout(function () {

                try {

                    if ($("#<%=grdIncoming.ClientID%> td").text().indexOf("No Expert Register Found!") < 0) {
                        var listHeight = getListHeight();
                        $("#<%=grdIncoming.ClientID%>").dataTable({
                        "bJQueryUI": true,
                        "bPaginate": false,
                        "bFilter": false,
                        "bInfo": false,
                        "aaSorting": [],
                        "aoColumns": [
							null,
							//{ "sSortDataType": "dom-indicator", "sType": "numeric" },
							null,
							null,
						    null,
							null,
                            { "sSortDataType": "dom-date" },
							//{ "sSortDataType": "dom-date" },
							//{ "sSortDataType": "dom-date" },
							//{ "sSortDataType": "dom-date" },


                            <% if (RoleId == "1")
                            { %>

								null

							<% } %>
                        ],
                        "sScrollY": "" + (listHeight + 5) + "px",
                    });
                }
            }
                catch (err) { }

                $("#dvIncomingProgress").hide();

            }, 100);

            //**********************************************************************************
            //******************************************************************************8
        $("#dvIncomingArchiveProgress").show();

        setTimeout(function () {

            try {

                 if ($("#<%=grdIncomingArchive.ClientID%> td").text().indexOf("No Expert Register Found!") < 0) {
                var listHeight = getListHeight();
                $("#<%=grdIncomingArchive.ClientID%>").dataTable({
                        "bJQueryUI": true,
                        "bPaginate": false,
                        "bFilter": false,
                        "bInfo": false,
                        "aaSorting": [],
                        "aoColumns": [
						null,
							//{ "sSortDataType": "dom-indicator", "sType": "numeric" },
							null,
							null,
						    null,
							null,
                            { "sSortDataType": "dom-date" },
							//{ "sSortDataType": "dom-date" },
							//{ "sSortDataType": "dom-date" },
							//{ "sSortDataType": "dom-date" },


                            <% if (RoleId == "1")
                            { %>

								null

							<% } %>
                        ],
                        "sScrollY": "" + (listHeight + 5) + "px",
                    });
                }
               }
                catch (err) { }

                $("#dvIncomingArchiveProgress").hide();

            }, 100);

            //**********************************************************************************






        }


        function buildSelectizePlugin() {



            $('#ddlCompany').selectize({

                render: {
                    option: function (item, escape) {

                        if (item.value.indexOf("_") != "-1") {
                            return '<div class="optgroup-header" style="font-size:15px">' + item.text + '</div>';
                        }

                        if (item.value.indexOf("Single") != "-1") {
                            return '<div class="optgroup-header" style="font-size:15px">' + item.text + '</div>';
                        }


                        else
                            return '<div style="margin-left:10px">' + item.text + '</div>';

                    }

                }
            });



            $(".selectize-control").on("click", function () {

                // initialize the selectize control
                var $select = $('#ddlCompany').selectize();

                $select[0].selectize.setValue("");

            });



        }


        $(document).ready(function () {



            $("#tabs").tabs({
                select: function (event, ui) {
                    switch (ui.index) {


                        case 0:

                            $("#dvIncomingProgress").show();

                            setTimeout(function () {

                                try {
                                    $("#<%=grdIncoming.ClientID%>").dataTable().fnAdjustColumnSizing();
                                    $("#<%=grdIncoming.ClientID%>").dataTable().fnAdjustColumnSizing();
                                } catch (e) { }

                                $("#dvIncomingProgress").hide();

                            }, 100);

                            break;

                        case 1:

                            $("#dvIncomingArchiveProgress").show();

                            setTimeout(function () {

                                try {
                                    $("#<%=grdIncomingArchive.ClientID%>").dataTable().fnAdjustColumnSizing();
                                    $("#<%=grdIncomingArchive.ClientID%>").dataTable().fnAdjustColumnSizing();
                                } catch (e) { }

                                $("#dvIncomingArchiveProgress").hide();

                            }, 100);

                            break;
                    }
                }
            });
            setTimeout(function () {

                var listHeight = getListHeight();
                //alert(listHeight);

                $("#dvIncoming").css("height", (listHeight + 40) + "px");
                $("#dvIncomingArchive").css("height", (listHeight + 40) + "px");


            }, 100);
            $(".ui-tabs-panel").css("padding", "5px");
            $("body").css("display", "");

        });

        function pageLoad() {

            EnableTheme();
            buildSelectizePlugin();


        }

        function DoPostBack() {

            if (!$("#ddlCompany").val()) return;

            $("#btnSearch").click();
        }

        function getListHeight() {

            var topHeight = $(topBar).height() + 20;
            var tabHeadHeight = $(tabHeader).height();
            var listHeight = (eval(parent.MainHeight) - topHeight - tabHeadHeight - 100);

            return listHeight;
        }

    </script>
    <style type="text/css">
        .dataTables_scrollBody {
            border-bottom: 1px solid #808080;
        }
    </style>
</head>
<body style="display: none">
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hdnSelected" runat="server" Value="0" />
            <asp:HiddenField ID="hdnScrollPos" runat="server" Value="0" />
            <asp:HiddenField ID="hdnSortInfo" runat="server" Value="" />
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div id="topBar" class="dvNoRoundAndBg">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <table border="0">
                            <tr>
                                <%--<td align="left">
                                    <div class="blueButton">
                                        <button type="button" id="Button1" runat="server" onclick="OpenReport()">
                                            Reports
                                        </button>
                                        <%--   <button type="button" id="Button2" runat="server" onserverclick="SendEmailDemo">
                                        SendEmail
                                    </button>
                                    </div>
                                </td>--%>
                                <td>&nbsp;    &nbsp;    &nbsp;    &nbsp;  
                                </td>
                                <td>Surname:
                                </td>
                                <td style="font-size: 14px;" align="left">
                                    <asp:TextBox ID="txtSurname" runat="server" Width="150" AutoPostBack="true"
                                        Style="font-size: 14px;"></asp:TextBox>
                                </td>
                                <td>&nbsp;    &nbsp;    &nbsp;    &nbsp;  
                                </td>

                                <td>Name:
                                </td>
                                <td align="left" style="font-size: 14px;" align="left">
                                    <asp:TextBox ID="txtName" runat="server" AutoPostBack="true" Width="150"
                                        Style="font-size: 14px;"></asp:TextBox>
                                </td>

                                <td>&nbsp;    &nbsp;    &nbsp;    &nbsp;  
                                </td>

                                <td align="left" valign="center" style="width: 380px">

                                    <div class="control-group">

                                        <select id="ddlCompany" runat="server" name="mylist" class="demo-default" onchange="DoPostBack()">
                                        </select>
                                    </div>



                                </td>
                                <td align="right" style="width: 100px" class="blueButton">
                                    <button type="button" id="btnSearch" runat="server" onserverclick="btnSearch_Click">
                                        Search
                                    </button>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="tab-container" style="margin: 10px;">
                <div id="tabs" runat="server">
                    <ul id="tabHeader">

                        <li><a href="#tabs-1">Experts Incoming - New!</a>
                            <img src="../App_Themes/Theme1/Images/new.gif" height="25" width="60" style="margin-top: 3px" />
                        </li>
                        <li><a href="#tabs-2">Experts Incoming - Archive</a></li>
                    </ul>

                    <div id="tabs-1">
                        <div class="dvTabContent">
                            <div id="dvIncoming" style="overflow: auto; overflow-x: hidden">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grdIncoming" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="false"
                                                BackColor="WhiteSmoke" AllowPaging="false" AllowSorting="false" OnRowDeleting="GridView1_RowDeleting"
                                                OnRowDataBound="GridView_RowDataBound"
                                                 Width="100%" CssClass="myGrid myGridInTabs" EmptyDataText="No Expert Register Found!"
                                                EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Medium"
                                                DataKeyNames="ExpertRegId">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <img id="imgEdit" style="padding: 2px;" title="Edit" onclick="OpenCustomerDetails('<%# Eval("ExpertRegId") %>', '<%# Eval("Surname") %>&nbsp;<%# Eval("Name") %>','<%# Eval("CompanyId") %>',0)"
                                                                src="../App_Themes/Theme1/images/edit3.png" width="20px" height="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname">

                                                        <ItemStyle CssClass="gridCoumnName" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-CssClass=""
                                                        SortExpression="Name">

                                                        <ItemStyle CssClass="gridCoumnName" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Passport" HeaderText="Passport" SortExpression="Passport"></asp:BoundField>
                                                    <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName"></asp:BoundField>
                                                    <asp:BoundField DataField="DateReg" HeaderText="Date Register" SortExpression="DateReg"></asp:BoundField>
                                                    <%--  <asp:BoundField DataField="Approval From Date" HeaderText="Country" SortExpression="AFDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Visa/ Invitation Issue date" HeaderText="Town" SortExpression="VIIDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Visa Exp Date" HeaderText="Email" SortExpression="VEDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Multiple entry Visa" HeaderText="Re Entry Visa Exp" SortExpression="MEVDate"></asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Delete" CommandArgument='<%# Eval("ExpertRegId") %>'
                                                                CommandName="Delete" ImageUrl="~/App_Themes/Theme1/Images/delete.png" Style="padding: 4px"
                                                                ToolTip="Delete" CausesValidation="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div id="dvIncomingProgress" style="position: absolute; background: white; left: 0px; top: 0px; right: 0px; bottom: 0px">
                                <div style="position: absolute; top: 50%; left: 50%; margin-top: -16px; margin-left: -32px;">
                                    <img src="../App_Themes/Theme1/Images/ajax-loader.gif" width="32px" height="32px" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabs-2">
                        <div class="dvTabContent">
                            <div id="dvIncomingArchive" style="overflow: auto; overflow-x: hidden">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grdIncomingArchive" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="false"
                                                BackColor="WhiteSmoke" AllowPaging="false" AllowSorting="false" OnRowDeleting="GridView1_RowDeleting"
                                                Width="100%" CssClass="myGrid myGridInTabs" EmptyDataText="No Expert Register Found!"
                                                EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Medium"
                                                  OnRowDataBound="GridView_RowDataBound"
                                                 DataKeyNames="ExpertRegId">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <img id="imgEdit" style="padding: 2px;" title="Edit" onclick="OpenCustomerDetails('<%# Eval("ExpertRegId") %>', '<%# Eval("Surname") %>&nbsp;<%# Eval("Name") %>','<%# Eval("CompanyId") %>',1)"
                                                                src="../App_Themes/Theme1/images/edit3.png" width="20px" height="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname">

                                                        <ItemStyle CssClass="gridCoumnName" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-CssClass=""
                                                        SortExpression="Name">

                                                        <ItemStyle CssClass="gridCoumnName" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Passport" HeaderText="Passport" SortExpression="Passport"></asp:BoundField>
                                                    <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName"></asp:BoundField>
                                                    <asp:BoundField DataField="DateReg" HeaderText="Date Register" SortExpression="DateReg"></asp:BoundField>
                                                    <%--  <asp:BoundField DataField="Approval From Date" HeaderText="Country" SortExpression="AFDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Visa/ Invitation Issue date" HeaderText="Town" SortExpression="VIIDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Visa Exp Date" HeaderText="Email" SortExpression="VEDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Multiple entry Visa" HeaderText="Re Entry Visa Exp" SortExpression="MEVDate"></asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Delete" CommandArgument='<%# Eval("ExpertRegId") %>'
                                                                CommandName="Delete" ImageUrl="~/App_Themes/Theme1/Images/delete.png" Style="padding: 4px"
                                                                ToolTip="Delete" CausesValidation="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div id="dvIncomingArchiveProgress" style="position: absolute; background: white; left: 0px; top: 0px; right: 0px; bottom: 0px">
                                <div style="position: absolute; top: 50%; left: 50%; margin-top: -16px; margin-left: -32px;">
                                    <img src="../App_Themes/Theme1/Images/ajax-loader.gif" width="32px" height="32px" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
