<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendEmailWeekly.aspx.cs" Inherits="Pages_SendEmailWeekly" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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

        /* Create an array with the values of all the checkboxes in a column */
        $.fn.dataTableExt.afnSortData['dom-checkbox'] = function (oSettings, iColumn) {
            var aData = [];
            $('td:eq(' + iColumn + ') input', oSettings.oApi._fnGetTrNodes(oSettings)).each(function () {
                aData.push(this.checked == true ? "1" : "0");
            });
            return aData;
        }

        function OpenCompanyDetails(Id, CompanyName) {


            


            if (Id==-1)
                CompanyName = "New Email Task";
            else
                CompanyName = "Email Task Weekly :" + Id;

            var x = parent.winPopup("pages/PopUp_editSendEmailWeekly.aspx?Id=" + Id, '1000px', '550px', CompanyName);
            //alert(x);

        }

        function ReturnValueFromModal(val) {
         
            if (val) {
               $("#btnSearch").click();
              
                parent.retVal = false;
            }
        }





        function UpdateSelectedOnPostBack() {

            $('.myGrid tr').bind('click', function (e) {

                $('td:not(td.neg):not(td.sorting_1)').css('background-color', 'transparent');
                $('td.neg').css('background-color', '#EECCCC');
                $('tr.odd td.sorting_1').css('background-color', '#D3D6FF');
                $('tr.even td.sorting_1').css('background-color', '#EAEBFF');
                $('td.neg.sorting_1').css('background-color', '#BB99AA');

                $(e.currentTarget).children('td:not(td.neg):not(td.sorting_1)').css('background-color', '#B0C2D4');
                $(e.currentTarget).children('td.neg').css('background-color', '#C2A0A4');
                $(e.currentTarget).children('td.sorting_1').css('background-color', '#B3B6CF');
            })
        }

        function EnableTheme() {

            $("#btnSearch").button();
            $("#btnNew").button();
            //$("select").dropkick();
            $("select").css("margin-top", 5);
            $("select").css("margin-bottom", 5);

            $("#dvTheGridProgress").show();

            setTimeout(function () {

                try {
                    var listHeight = getListHeight();
                    $("#<%=gvTheGrid.ClientID%>").dataTable({
                        "bJQueryUI": true,
                        "bPaginate": false,
                        "bFilter": false,
                        "bInfo": false,
                        "aaSorting": [],
                        "aoColumns": [
							null,
							null,
							null,
							
							
							null,
							null,
                            null
                        ],
                        "sScrollY": "" + (listHeight - 35) + "px"
                    });
                }
                catch (err) { }
                $("#dvTheGridProgress").hide();

            }, 100);

            $("#dvDocsProgress").show();

            setTimeout(function () {

                try {
                    var sortingString = $("#<%=hdnSortInfo.ClientID%>").val();
                    var sorting = [];
                    if (sortingString != "") {

                        var ss = sortingString.split(",");
                        for (var i = 0; i < ss.length; i++) {
                            if (i == 0 || i == 2) {
                                sorting[i] = parseInt(ss[i]);
                            } else {
                                sorting[i] = ss[i];
                            }
                        }

                        sorting = [sorting];
                    }

                    var listHeight = getListHeight();




                }
                catch (err) { }

                var headers = $(".DataTables_sort_wrapper");
                for (var i = 0; i < headers.length; i++) {

                    if ($(headers[i]).html().indexOf("Obligo") >= 0) {
                        $(headers[i]).append("<div style='background: orange; height: 3px; font-size: 1px;'></div>");
                    }
                }

            

            }, 100);
        }

        $(document).ready(function () {

            $("#tabs").tabs({
                select: function (event, ui) {
                    switch (ui.index) {
                        case 0:

                            $("#dvTheGridProgress").show();

                            setTimeout(function () {
                                $("#<%=gvTheGrid.ClientID%>").dataTable().fnAdjustColumnSizing();
                                $("#<%=gvTheGrid.ClientID%>").dataTable().fnAdjustColumnSizing();

                                $("#dvTheGridProgress").hide();

                            }, 100);

                            break;

                     
                    }
                }
            });

            setTimeout(function () {

                var listHeight = getListHeight();

                $("#dvGridContainer").css("height", (listHeight + 10) + "px");
               

            }, 50);


            $(".ui-tabs-panel").css("padding", "5px");
           

            $("body").css("display", "");

           
        });

        function pageLoad() {

            EnableTheme();
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

        <asp:HiddenField ID="hdnScrollPos" runat="server" Value="0" />
        <asp:HiddenField ID="hdnSortInfo" runat="server" Value="" />

        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div id="topBar" class="dvNoRoundAndBg">
            </div>
            <div class="tab-container" style="margin: 10px;">

                <div id="tabs" runat="server">
                    <ul id="tabHeader">
                        <li><a href="#tabs-1">Send Email Weekly</a></li>

                    </ul>
                    <div id="tabs-1">
                         
                        <div class="dvTabContent">

                             <button type="button" id="btnSearch" runat="server" style="visibility:hidden" onserverclick="btnSearch_Click">
                                    Search
                                </button>
                            <div id="dvGridContainer" style="overflow: auto; overflow-x: hidden">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvTheGrid" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="false"
                                                BackColor="WhiteSmoke" AllowPaging="false" AllowSorting="false" OnSorting="gvTheGrid_Sorting"
                                                Width="100%" CssClass="myGrid myGridInTabs" OnPageIndexChanging="gvTheGrid_PageIndexChanging"
                                                EmptyDataText="No Send Mail Task Found!" EmptyDataRowStyle-Font-Bold="true" OnRowDeleting="gvTheGrid_RowDeleting"
                                                EmptyDataRowStyle-Font-Size="Medium" OnRowDataBound="gvTheGrid_RowDataBound"
                                                DataKeyNames="Id" >
                                                <Columns>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <img id="imgEdit" style="padding: 2px;" title="Edit" onclick="OpenCompanyDetails('<%# Eval("Id") %>', '<%# Eval("Name") %>')"
                                                                src="../App_Themes/Theme1/images/edit3.png" width="20px" height="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Title" SortExpression="Title" ItemStyle-Font-Size="Larger" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Eval("Title").ToString()%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                   <%--   <asp:TemplateField HeaderText="Subject" SortExpression="Subject" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Eval("Subject").ToString()%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Email" SortExpression="Email" ItemStyle-Font-Size="Larger" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Eval("Email").ToString()%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <%-- <asp:TemplateField HeaderText="Email CC" SortExpression="EmailCopy" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Eval("EmailCopy").ToString()%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  --%>
                                                    <asp:TemplateField HeaderText="Day" SortExpression="Day" ItemStyle-Font-Size="Larger">
                                                        <ItemTemplate>
                                                            <%#Eval("Name").ToString()%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 
                                                

                                                    <asp:TemplateField HeaderText="Active" SortExpression="Active" ItemStyle-Font-Size="Larger">
                                                         <ItemTemplate>
                                             <asp:CheckBox ID="chActive" runat="server" Enabled="false" Checked='<%#bool.Parse(Eval("Active").ToString())%>'/> 
                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Delete" CommandArgument=''
                                                                CommandName="Delete" ImageUrl="~/App_Themes/Theme1/Images/delete.png" Style="padding: 4px"
                                                                ToolTip="Delete" CausesValidation="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="dvTheGridProgress" style="position: absolute; background: white; left: 0px; top: 0px; right: 0px; bottom: 0px">
                                    <div style="position: absolute; top: 50%; left: 50%; margin-top: -16px; margin-left: -32px;">
                                        <img src="../App_Themes/Theme1/Images/ajax-loader.gif" width="32px" height="32px" />
                                    </div>
                                </div>
                            </div>
                            <div style="margin-top: 4px; text-align: left;" class="blueButton">
                                <button id="btnNew" type="button" runat="server" onclick="OpenCompanyDetails(-1);">
                                    New Weekly Task
                                </button>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
