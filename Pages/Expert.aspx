<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expert.aspx.cs" EnableEventValidation="false"
    Inherits="Pages_Expert" %>

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

        function OpenCustomerDetails(ExpertId, ExpertName) {


            var CompanyId = $('#<%= ddlCompany.ClientID %>').val().replace("Single_", "").replace("Group_", "");
            if (!ExpertName)
                ExpertName = "New Expert";

            if (ExpertId) {
                var x = parent.winPopup("pages/PopUp_editCustomer.aspx?CompanyId=" + CompanyId + "&ExpertId=" + ExpertId + "&ts=" + new Date().getTime(), '1150', '630', ExpertName);

            }
            else {
                if (Selected == "0") {
                    parent.ShowMessage('Must Selected One Of Expert', '1');
                }
                else {
                    var StepId = $('#<%= ddlSteps.ClientID %>').val();
                    if (StepId == "0") {
                        parent.ShowMessage('Must Choose One Update Type', '1');
                    }
                    else {
                        var x = parent.winPopup("pages/PopUp_editCustomer.aspx?CompanyId=" + CompanyId + "&ExpertId=" + Selected + "&StepId=" + StepId, '1150', '630', ExpertName);
                    }
                }

            }

            //alert(x);

        }

        function OpenReport() {
            var CompanyId = $('#<%= ddlCompany.ClientID %>').val().replace("Single_", "").replace("Group_", "");
            // alert(CompanyId);
            var x = parent.winPopup("pages/PopUp_Statics.aspx?CompanyId=" + CompanyId, '900', '500', 'Reports');

        }

        function KeyupChange(type) {
            //  debugger
            var txtVal = $('#' + type).val();
            if (txtVal.length > 1 || txtVal.length == 0)
                __doPostBack(type, '');

        }


        function ChooseCompany(CompanyId) {
            // alert(CompanyId);

            $("#txtSurname").val("");
            $("#txtName").val("");

            var $select = $('#ddlCompany').selectize();

            $select[0].selectize.setValue(CompanyId);
            $select[0].selectize.setValue("Single_" + CompanyId);



        }

        function DeleteArchive(ExpertId) {

            ConfirmGeneric("Are u sure want delete from archive???", 2, ExpertId);
        }


        function SetFocusOnPostBack(type) {



            var fieldInput = (type == 1) ? $('#<%=txtSurname.ClientID %>') : $('#<%=txtName.ClientID %>');
            var fldLength = fieldInput.val().length;
            fieldInput.focus();
            fieldInput[0].setSelectionRange(fldLength, fldLength);
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

        function ReturnValueFromModal(val, retValCompanyId) {

            if (retValCompanyId) {

                ChooseCompany(retValCompanyId);
                parent.retValCompanyId = "";
            }


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


        function ConfirmDelete() {
            $("#<%=hdnSelected.ClientID%>").val(Selected);

            __doPostBack('<%=btnDel.UniqueID%>', '');

            $("#<%=hdnSelected.ClientID%>").val("0");


        }

        function ConfirmDeleteArchive(ExpertId) {

            $("#<%=hdnSelectedArchive.ClientID%>").val(ExpertId);

            // alert(ExpertId);

            __doPostBack('<%=btnDel.UniqueID%>', '');

            $("#<%=hdnSelectedArchive.ClientID%>").val("0");

        }

        function ConfirmGeneric(title, type, ExpertId) {
            dialog = $("<div id='dialog' title='Confirmation Required'>" + title + "</div>");
            dialog.dialog({

                modal: true,
                buttons: [
                    {
                        text: "Confirm",
                        class: 'okClass',
                        //open: function () {
                        //    $(this).addClass('okClass');
                        //},
                        click: function () {
                            $(this).dialog("close");
                            if (type == 1) ConfirmDelete();
                            if (type == 2) ConfirmDeleteArchive(ExpertId);
                            return true;
                        }
                    },
                    {
                        text: "Cancel",
                        class: 'cancelClass',
                        click: function () {
                            $(this).dialog("close");
                            return false;
                        }
                    }
                ]






                //buttons: {
                //    "Confirm": function () {
                //        $(this).dialog("close");
                //        ConfirmDelete();
                //        return true;

                //    },
                //    "Cancel": function () {
                //        $(this).dialog("close");
                //        return false;

                //    }
                //}
            });

            $(".ui-dialog").css("padding", "4px").css("font-size", "small");

        }

        function DeleteCustomer() {

            if (Selected == "0") {
                parent.ShowMessage('Must Selected One Of Expert', '1');
            }
            else {

                ConfirmGeneric("Are u sure want delete all selected???", 1);


              <%--  var x = confirm("Are u sure want delete all selected???");
                if (x) {

                    $("#<%=hdnSelected.ClientID%>").val(Selected);
                
                    __doPostBack('<%=btnDel.UniqueID%>', '');
                   
                }--%>
            }



        }

        function DuplicateCustomer() {

            if (Selected == "0") {
                parent.ShowMessage('Must Selected One Of Expert', '1');
            }
            else {
                var x = confirm("Are u sure want Duplicate all selected???");
                if (x) {

                    $("#<%=hdnSelected.ClientID%>").val(Selected);

                    __doPostBack('<%=btnDul.UniqueID%>', '');

                }
            }
        }

        function UpdateAllChecbox1() {
            $("[id$=chCustomer1]").attr('checked', $('#masterCheck1').is(':checked'));
        }

        function EnableTheme() {

            $("#Button1").button();
            $("#btnSearch").button();
            $("input[title]").inputHints();
            $("#ddlSteps").dropkick();
            $("#ddlFamily").dropkick(
                {
                    change: function (value, label) {

                        setTimeout(function () {

                            __doPostBack('ddlFamily', '');

                        }, 0);
                    }
                });

            //        	$("#ddlCompany").dropkick({
            //	        	change: function(value,label) {
            //		        	
            //		        	setTimeout(function() {
            //		        	
            //		        		__doPostBack('ddlCompany','');
            //			        	
            //		        	}, 0);
            //	        	}
            //        	});

            $("#btnNew").button();
            $("#btnDelete").button();
            $("#btnDuplicate").button();
            $("#btnSelected").button();
            $("#btnExcel").button();
            //$("select").dropkick();
            $("select").css("margin-top", 5);
            $("select").css("margin-bottom", 5);
            //$("#ddlCompany").hide();

            $("#dvTheGridProgress").show();

            setTimeout(function () {

                try {
                    if ($("#<%=gvTheGrid.ClientID%> td").text().indexOf("No Expert Found!") < 0) {

                        var listHeight = getListHeight();
                        $("#<%=gvTheGrid.ClientID%>").dataTable({
                            "bJQueryUI": true,
                            "bPaginate": false,
                            "bFilter": false,
                            "bInfo": false,
                            "aaSorting": [],
                            "aoColumns": [
                                null,
                                { "sSortDataType": "dom-checkbox" },
                                null,
                                { "sSortDataType": "dom-indicator", "sType": "numeric" },
                                null,
                                null,
                                //{ "sSortDataType": "dom-date" },
                                { "sSortDataType": "dom-date" },
                                { "sSortDataType": "dom-date" },
                                { "sSortDataType": "dom-date" },
                                { "sSortDataType": "dom-date" },
                                { "sSortDataType": "dom-date" },
                                { "sSortDataType": "dom-date" },

                            ],
                            "sScrollY": "" + (listHeight - 35) + "px",
                        });
                    }
                }
                catch (err) { }

                $("#dvTheGridProgress").hide();

            }, 100);

            $("#dvArchiveProgress").show();

            setTimeout(function () {

                try {

               // if($("#<%=GridView1.ClientID%> td").text().indexOf("No Expert Found!") < 0) {
                    var listHeight = getListHeight();
                    $("#<%=GridView1.ClientID%>").dataTable({
                        "bJQueryUI": true,
                        "bPaginate": false,
                        "bFilter": false,
                        "bInfo": false,
                        "aaSorting": [],
                        "aoColumns": [
                            null,
                            { "sSortDataType": "dom-indicator", "sType": "numeric" },
                            null,
                            null,
                            { "sSortDataType": "dom-date" },
                            { "sSortDataType": "dom-date" },
                            { "sSortDataType": "dom-date" },
                            { "sSortDataType": "dom-date" },
                            { "sSortDataType": "dom-date" },
                            { "sSortDataType": "dom-date" },


                            <% if (RoleId == "1")
        { %>

                            null

							<% } %>
                        ],
                        "sScrollY": "" + (listHeight + 5) + "px",
                    });
                }
                //}
                catch (err) { }

                $("#dvArchiveProgress").hide();

            }, 100);

            //******************************************************************************8
            <%--$("#dvIncomingProgress").show();
        
            setTimeout(function() {
            
	            try
	            {

               // if($("#<%=grdIncoming.ClientID%> td").text().indexOf("No Expert Found!") < 0) {
	            	var listHeight = getListHeight();
                    $("#<%=grdIncoming.ClientID%>").dataTable({
		            	"bJQueryUI": true,
		            	"bPaginate": false,
		            	"bFilter": false,
		            	"bInfo": false,
		            	"aaSorting":[],
						"aoColumns": [
							null,
							{ "sSortDataType": "dom-indicator", "sType": "numeric" },
							null,
							null,
							{ "sSortDataType": "dom-date" },
							{ "sSortDataType": "dom-date" },
                            { "sSortDataType": "dom-date" },
							{ "sSortDataType": "dom-date" },
							{ "sSortDataType": "dom-date" },
							{ "sSortDataType": "dom-date" },

							
                            <% if (RoleId == "1") { %>
		
								null
															
							<% } %>
						],
		            	"sScrollY": "" + (listHeight + 5) + "px",
		            });
		        }
                //}
		        catch(err) {}
	            
	            $("#dvIncomingProgress").hide();
	            
			}, 100);--%>

            //**********************************************************************************




            $("#dvDocsProgress").show();

            setTimeout(function () {

                try {
                    if ($("#<%=gvDocs.ClientID%> td").text().indexOf("No Expert") < 0) {

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
                        $("#<%=gvDocs.ClientID%>").dataTable({
                            "bJQueryUI": true,
                            "bPaginate": false,
                            "bFilter": false,
                            "bInfo": false,
                            "aaSorting": sorting,
                            "aoColumns": [

							<% if (RoleId == "1")
        { %>

                                null,

							<% } %>

                                null,
                                null,
                                { "sSortDataType": "dom-checkbox" },
                                { "sSortDataType": "dom-checkbox" },
                                { "sSortDataType": "dom-checkbox" },
                                { "sSortDataType": "dom-checkbox" },
                                { "sSortDataType": "dom-checkbox" },
                                null,
                                null,
                                { "sSortDataType": "dom-checkbox" },
                                { "sSortDataType": "dom-checkbox" },
                                { "sSortDataType": "dom-checkbox" },
                                { "sSortDataType": "dom-checkbox" },
                                null,
                                { "sSortDataType": "dom-checkbox" }
                            ],
                            "sScrollY": "" + (listHeight - 30) + "px",
                        });

                        $("#gvDocs_wrapper .dataTables_scrollBody").on("scroll", function () {
                            $("#<%=hdnScrollPos.ClientID%>").val(
                                $("#gvDocs_wrapper .dataTables_scrollBody").scrollTop());

                        });

                        $("#gvDocs_wrapper thead .DataTables_sort_wrapper").click(function () {

                            setTimeout(function () {

                                var sortInfo = $("#<%=gvDocs.ClientID%>").dataTable().fnSettings().aaSorting;
                                $("#<%=hdnSortInfo.ClientID%>").val(sortInfo);

                            }, 100);
                        });
                    }
                }
                catch (err) { }

                var headers = $(".DataTables_sort_wrapper");
                for (var i = 0; i < headers.length; i++) {

                    if ($(headers[i]).html().indexOf("Obligo") >= 0) {
                        $(headers[i]).append("<div style='background: orange; height: 3px; font-size: 1px;'></div>");
                    }
                }

                var st = parseInt($("#<%=hdnScrollPos.ClientID%>").val());
                $("#gvDocs_wrapper .dataTables_scrollBody").scrollTop(st);

                $("#dvDocsProgress").hide();

            }, 100);
        }


        function buildSelectizePlugin() {



            $('#ddlCompany').selectize({

                //                            sortField: [
                //                            {
                //                                 field: 'Order',
                //                                 direction: 'asc'
                //                            },
                //                            {
                //                                field: '$score'
                //                            }
                //                            ],


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


                //               var $select = $(this).selectize();
                //               var selectize = $select[0].selectize;
                //             //  selectize.setValue("");
                //               
                //               selectize.addItem("0", false);
                //               //selectize.clear(true);

                // alert();
            });



        }


        $(document).ready(function () {

            

            $("#tabs").tabs({
                select: function (event, ui) {
                    switch (ui.index) {
                        case 0:

                            $("#dvTheGridProgress").show();

                            setTimeout(function () {

                                try {

                                    if ($("#<%=gvTheGrid.ClientID%> td").text().indexOf("No Expert Found!") < 0) {

                                        $("#<%=gvTheGrid.ClientID%>").dataTable().fnAdjustColumnSizing();
                                        $("#<%=gvTheGrid.ClientID%>").dataTable().fnAdjustColumnSizing();
                                    }

                                } catch (e) { }

                                $("#dvTheGridProgress").hide();

                            }, 100);

                            break;
                        case 1:

                            $("#dvArchiveProgress").show();

                            setTimeout(function () {

                                try {
                                    $("#<%=GridView1.ClientID%>").dataTable().fnAdjustColumnSizing();
                                    $("#<%=GridView1.ClientID%>").dataTable().fnAdjustColumnSizing();
                                } catch (e) { }

                                $("#dvArchiveProgress").hide();

                            }, 100);

                            break;
                        case 2:

                            $("#dvDocsProgress").show();

                            setTimeout(function () {

                                try {

                                    if ($("#<%=gvDocs.ClientID%> td").text().indexOf("No Expert") < 0) {

                                        $("#<%=gvDocs.ClientID%>").dataTable().fnAdjustColumnSizing();
                                        $("#<%=gvDocs.ClientID%>").dataTable().fnAdjustColumnSizing();

                                        $("#gvDocs_wrapper .dataTables_scrollBody").on("scroll", function () {
                                            $("#<%=hdnScrollPos.ClientID%>").val(
                                                $("#gvDocs_wrapper .dataTables_scrollBody").scrollTop());

                                        });
                                    }

                                } catch (e) { }

                                $("#dvDocsProgress").hide();

                            }, 100);

                            break;



                    }
                }
            });
            setTimeout(function () {

                var listHeight = getListHeight();

                $("#dvGridContainer").css("height", (listHeight) + "px");
                $("#dvArchive").css("height", (listHeight + 40) + "px");
                $("#dvDocs").css("height", (listHeight + 10) + "px");

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
            <asp:HiddenField ID="hdnSelectedArchive" runat="server" Value="0" />
            <asp:HiddenField ID="hdnSelected" runat="server" Value="0" />
            <asp:HiddenField ID="hdnScrollPos" runat="server" Value="0" />
            <asp:HiddenField ID="hdnSortInfo" runat="server" Value="" />
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div id="topBar" class="dvNoRoundAndBg">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <table width="100%" border="0">
                            <tr>
                                <td align="left">
                                    <div class="blueButton">
                                        <button type="button" id="Button1" runat="server" onclick="OpenReport()">
                                            Reports
                                        </button>
                                        <%--   <button type="button" id="Button2" runat="server" onserverclick="SendEmailDemo">
                                        SendEmail
                                    </button>--%>
                                    </div>
                                </td>
                                <td>Filter:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFamily" Width="110px" runat="server" AppendDataBoundItems="true"
                                        OnSelectedIndexChanged="ddlFamily_SelectedChange" AutoPostBack="true">
                                        <asp:ListItem Value="0"> All </asp:ListItem>
                                        <asp:ListItem Value="1"> Head of Family </asp:ListItem>
                                        <asp:ListItem Value="2"> Family </asp:ListItem>
                                        <asp:ListItem Value="3"> 30 Days </asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>Surname:
                                </td>
                                <td style="font-size: 14px;" align="left">



                                  
                                            <asp:TextBox ID="txtSurname" runat="server" Width="150" onkeyup="KeyupChange('txtSurname');"
                                                Style="font-size: 14px;"></asp:TextBox>

                                 <%--   <asp:TextBox ID="txtSurname" runat="server" Width="150" OnTextChanged="txtChange"
                                                Style="font-size: 14px;"></asp:TextBox>--%>

                                    

                                </td>
                                <td>Name:
                                </td>
                                <td align="left" style="font-size: 14px;" align="left">
                                    <asp:TextBox ID="txtName" runat="server"  Width="150" onkeyup="KeyupChange('txtName');"
                                        Style="font-size: 14px;"></asp:TextBox>
                                </td>
                                <td align="left" valign="center" style="width: 380px">

                                    <div class="control-group">

                                        <select id="ddlCompany" runat="server" name="mylist" class="demo-default" onchange="DoPostBack()">
                                            <%--  <option value="">Select Company... </option>
                    	            <option value="a2_2" selected>Item A</option>
					                <option value="b2">Item B</option>--%>
                                        </select>
                                    </div>


                                    <%--  <asp:DropDownList Visible="true" ID="ddlCompany"  Width="200px" runat="server" AppendDataBoundItems="true"
                                    OnSelectedIndexChanged="ddlCompany_SelectedChange" AutoPostBack="true">
                                    <asp:ListItem Value="0">-- All Companies --</asp:ListItem>
                                </asp:DropDownList>--%>
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
                        <li><a href="#tabs-1">Experts</a></li>
                        <li><a href="#tabs-2">Experts Archive</a></li>
                        <li><a href="#tabs-3">Experts Docs</a></li>

                    </ul>
                    <div id="tabs-1">
                        <div class="dvTabContent">
                            <div id="dvGridContainer" style="overflow: auto; overflow-x: hidden;">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvTheGrid" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="false"
                                                BackColor="WhiteSmoke" AllowPaging="false" AllowSorting="false" OnSorting="gvTheGrid_Sorting"
                                                Width="100%" CssClass="myGrid myGridInTabs" EmptyDataText="No Expert Found!"
                                                OnRowDataBound="gvTheGrid_RowDataBound" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Medium"
                                                DataKeyNames="diffday,ExpertId,IsMonthly,ParentId,IsParentOfAny">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <img id="imgEdit" style="padding: 2px;" title="Edit" onclick="OpenCustomerDetails('<%# Eval("ExpertId") %>', '<%# Eval("Surname") %>&nbsp;<%# Eval("Name") %>')"
                                                                src="../App_Themes/Theme1/images/edit3.png" width="20px" height="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="MainCheckbox">
                                                        <HeaderTemplate>
                                                            <input id="masterCheck" type="checkbox" onclick="UpdateAllChecbox(this)" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <input id="chCustomer" onchange="ChecBoxChange(this,'<%# Eval("ExpertId") %>')" type="checkbox" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="LevelId" HeaderText="Level" SortExpression="LevelId">
                                                        <ItemStyle Font-Bold="True" BorderWidth="1" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="Flag" SortExpression="diffday">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgStatus" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Theme1/Images/transparanet.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname">
                                                        <ItemStyle CssClass="gridCoumnName" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                                                        <ItemStyle CssClass="gridCoumnName" />
                                                    </asp:BoundField>
                                                    <%--   <asp:BoundField DataField="Client authorization Date" HeaderText="Client authorization Date"></asp:BoundField>--%>
                                                    <asp:BoundField DataField="Approval Submition Date" HeaderText="Approval Submission"
                                                        SortExpression="ASDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Approval From Date" HeaderText="Approval From Date" SortExpression="AFDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Approval Exp Date" HeaderText="Approval Exp" SortExpression="AEDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Visa/ Invitation Issue date" HeaderText="Visa/Invitation Issue "
                                                        SortExpression="VIIDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Visa Exp Date" HeaderText=" Visa Exp Date" SortExpression="VEDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Multiple entry Visa" HeaderText="Re Entry Visa Exp" SortExpression="MEVDate"></asp:BoundField>
                                               
                                                    
                                               <%--  <asp:TemplateField HeaderText="">
                                                          <ItemTemplate>
                                                              <img src="../App_Themes/Theme1/Images/Edit2.png" onclick="ChooseCompany('<%# Eval("CompanyId") %>')" style="padding:5px" />
                                                          
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                --%>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div style="height: 30px;">
                                <div style="float: left;">
                                    <table cellpadding="0">
                                        <tr>
                                            <td class="blueButton">
                                                <button id="btnNew" type="button" runat="server" onclick="OpenCustomerDetails(-1);"
                                                    style="font-size: 14px; height: 30px">
                                                    New Expert
                                                </button>
                                            </td>
                                            <td class="redButton">
                                                <button id="btnDelete" type="button" runat="server" onclick="DeleteCustomer();" style="font-size: 14px; height: 30px">
                                                    Delete Selected
                                                </button>
                                            </td>
                                            <td>
                                                <button id="btnDuplicate" type="button" runat="server" onclick="DuplicateCustomer();"
                                                    style="font-size: 14px; height: 30px">
                                                    Duplicate Selected
                                                </button>
                                            </td>
                                            <td>
                                                <button id="btnSelected" type="button" runat="server" onclick="OpenCustomerDetails();"
                                                    style="font-size: 14px; height: 30px">
                                                    Open Selected
                                                </button>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSteps" Width="130" runat="server" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">-- Choose Update --</asp:ListItem>
                                                    <asp:ListItem Value="1">Step 1</asp:ListItem>
                                                    <asp:ListItem Value="2">Step 2</asp:ListItem>
                                                    <asp:ListItem Value="3">Step 3</asp:ListItem>
                                                    <asp:ListItem Value="4">Company</asp:ListItem>
                                                    <asp:ListItem Value="5">Active</asp:ListItem>
                                                    <asp:ListItem Value="6">Comments</asp:ListItem>
                                                    <asp:ListItem Value="7">Email</asp:ListItem>
                                                    <asp:ListItem Value="8">Title</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <button id="btnExcel" type="button" runat="server" onserverclick="btnExcel_Click"
                                                    style="font-size: 14px; height: 30px">
                                                    Docs To Excel
                                                </button>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="btnDel" OnClick="btnDelete_Click" runat="server"></asp:LinkButton>
                                            <asp:LinkButton ID="btnDul" OnClick="btnDuplicate_Click" runat="server"></asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div style="float: right; font-size: 12px; margin-top: 10px;" align="center">
                                    <img src="../App_Themes/Theme1/Images/red_flag.png" width="16px" height="16px" align="middle"
                                        style="margin-bottom: 6px" />
                                    0 - 90 Days. &nbsp;
                                <img src="../App_Themes/Theme1/Images/orange_flag.png" width="16px" height="16px"
                                    align="middle" style="margin-bottom: 6px" />
                                    90 - 120 Days. &nbsp;
                                <img src="../App_Themes/Theme1/Images/green_flag.png" width="16px" height="16px"
                                    align="middle" style="margin-bottom: 6px" />
                                    120 + Days.
                                </div>
                            </div>
                            <div id="dvTheGridProgress" style="position: absolute; background: white; left: 0px; top: 0px; right: 0px; bottom: 0px">
                                <div style="position: absolute; top: 50%; left: 50%; margin-top: -16px; margin-left: -32px;">
                                    <img src="../App_Themes/Theme1/Images/ajax-loader.gif" width="32px" height="32px" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabs-2">
                        <div class="dvTabContent">
                            <div id="dvArchive" style="overflow: auto; overflow-x: hidden">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridView1" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="false"
                                                BackColor="WhiteSmoke" AllowPaging="false" AllowSorting="false" OnSorting="GridView1_Sorting"
                                                Width="100%" CssClass="myGrid myGridInTabs" EmptyDataText="No Expert Archive Found!"
                                                OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound"
                                                EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="Medium"
                                                DataKeyNames="diffday,ExpertId,IsMonthly">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <img id="imgEdit" style="padding: 2px;" title="Edit" onclick="OpenCustomerDetails('<%# Eval("ExpertId") %>', '<%# Eval("Surname") %>&nbsp;<%# Eval("Name") %>')"
                                                                src="../App_Themes/Theme1/images/edit3.png" width="20px" height="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--   <asp:TemplateField HeaderText="" HeaderStyle-CssClass="MainCheckbox">
	                                                <HeaderTemplate>
	                                                    <input id="masterCheck1" type="checkbox" onclick="UpdateAllChecbox1()" />
	                                                </HeaderTemplate>
	                                                <ItemTemplate>
	                                                    <asp:CheckBox ID="chCustomer1" runat="server" Width="20" ClientIDMode="Static" />
	                                                </ItemTemplate>
	                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Flag" SortExpression="diffday">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgStatus" runat="server" CommandName="Edit" ImageUrl="~/App_Themes/Theme1/Images/transparanet.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname">

                                                        <ItemStyle CssClass="gridCoumnName" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-CssClass=""
                                                        SortExpression="Name">

                                                        <ItemStyle CssClass="gridCoumnName" />
                                                    </asp:BoundField>
                                                    <%--   <asp:BoundField DataField="Client authorization Date" HeaderText="Client authorization Date"></asp:BoundField>--%>
                                                    <asp:BoundField DataField="Approval Submition Date" HeaderText="Approval Submission"
                                                        SortExpression="ASDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Approval From Date" HeaderText="Approval From Date" SortExpression="AFDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Approval Exp Date" HeaderText="Approval Exp" SortExpression="AEDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Visa/ Invitation Issue date" HeaderText="Visa/Invitation Issue "
                                                        SortExpression="VIIDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Visa Exp Date" HeaderText="Visa Exp Date" SortExpression="VEDate"></asp:BoundField>
                                                    <asp:BoundField DataField="Multiple entry Visa" HeaderText="Re Entry Visa Exp" SortExpression="MEVDate"></asp:BoundField>
                                                  <%--  <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Delete" CommandArgument='<%# Eval("ExpertId") %>'
                                                                CommandName="Delete" ImageUrl="~/App_Themes/Theme1/Images/delete.png" Style="padding: 4px"
                                                                ToolTip="Delete" CausesValidation="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>


                                                       <asp:TemplateField HeaderText="">
                                                          <ItemTemplate>
                                                              <img src="../App_Themes/Theme1/Images/delete.png" onclick="DeleteArchive('<%# Eval("ExpertId") %>')" style="padding:5px" />
                                                          
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div id="dvArchiveProgress" style="position: absolute; background: white; left: 0px; top: 0px; right: 0px; bottom: 0px">
                                <div style="position: absolute; top: 50%; left: 50%; margin-top: -16px; margin-left: -32px;">
                                    <img src="../App_Themes/Theme1/Images/ajax-loader.gif" width="32px" height="32px" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabs-3">
                        <div class="dvTabContent">
                            <div id="dvDocs" style="overflow: auto; overflow-x: hidden">
                                <div>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvDocs" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="false"
                                                BackColor="WhiteSmoke" AllowPaging="false" DataKeyNames="ExpertId,
	                                                            Cv,
	                                                            Diploma,
	                                                            MarriageCertificate
	                                                            "
                                                OnRowEditing="gvDocs_RowEditing" OnRowCancelingEdit="gvDocs_RowCancelingEdit"
                                                OnRowUpdating="gvDocs_RowUpdating" Width="100%" CssClass="myGrid myGridInTabs"
                                                OnRowDataBound="gvDocs_RowDataBound" EmptyDataRowStyle-Font-Size="Medium" EmptyDataRowStyle-Font-Bold="true"
                                                EmptyDataText="No Expert!!" AllowSorting="false" OnSorting="gvDocs_Sorting" RowStyle-Height="22px">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Edit
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgEdit" runat="server" AlternateText="Edit" CommandName="Edit"
                                                                ImageUrl="~/App_Themes/Theme1/Images/edit3.png" Width="20px" Height="20px" ToolTip="Edit"
                                                                Style="padding: 4px" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:ImageButton ID="imgCancel" runat="server" AlternateText="Cancel" CommandName="CANCEL"
                                                                ImageUrl="~/App_Themes/Theme1/Images/cancel.png" ToolTip="Cancel" CausesValidation="false"
                                                                Style="" />
                                                            <asp:ImageButton ID="imgUpdate" runat="server" AlternateText="Save" CommandName="Update"
                                                                ImageUrl="~/App_Themes/Theme1/Images/save1.jpg" ValidationGroup="edit" ToolTip="Save"
                                                                Style="" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Surname" ReadOnly="true" HeaderText="Surname" SortExpression="Surname">
                                                        <ItemStyle CssClass="gridCoumnName" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Name" ReadOnly="true" HeaderText="Name" HeaderStyle-CssClass=""
                                                        SortExpression="Name">
                                                        <ItemStyle CssClass="gridCoumnName" />

                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="AL-33" SortExpression="AL33">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ch33" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("AL33").ToString())%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="ch33" runat="server" Checked='<%#bool.Parse(Eval("AL33").ToString())%>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AL-15" SortExpression="AL15">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ch15" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("AL15").ToString())%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="ch15" runat="server" Checked='<%#bool.Parse(Eval("AL15").ToString())%>' />
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
                                                    <asp:TemplateField HeaderText="Passport Copy" SortExpression="CopyofPassport">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chCopy" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("CopyofPassport").ToString())%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chCopy" runat="server" Checked='<%#bool.Parse(Eval("CopyofPassport").ToString())%>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="I.D. Copy" ItemStyle-Width="80px" SortExpression="CopyID">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chId" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("CopyID").ToString())%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chId" runat="server" Checked='<%#bool.Parse(Eval("CopyID").ToString())%>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CV" SortExpression="Cv" ItemStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <%# Eval("CvT")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlCv" runat="server" AppendDataBoundItems="true" Style="width: 100%">
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2">TBT</asp:ListItem>
                                                                <asp:ListItem Value="3">T Done</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Diploma" SortExpression="Diploma" ItemStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <%# Eval("DiplomaT")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlDiploma" runat="server" AppendDataBoundItems="true" Style="width: 100%">
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2">TBT</asp:ListItem>
                                                                <asp:ListItem Value="3">T Done</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Obligo 6" SortExpression="Obligo6">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chObligo6" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("Obligo6").ToString())%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chObligo6" runat="server" Checked='<%#bool.Parse(Eval("Obligo6").ToString())%>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Obligo 2" SortExpression="Obligo2">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chObligo2" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("Obligo2").ToString())%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chObligo2" runat="server" Checked='<%#bool.Parse(Eval("Obligo2").ToString())%>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Medical Obligo" SortExpression="MedicalObligo">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chMedicalObligo" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("MedicalObligo").ToString())%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chMedicalObligo" runat="server" Checked='<%#bool.Parse(Eval("MedicalObligo").ToString())%>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--            <asp:TemplateField HeaderText="Obligo" SortExpression="EmployerObligo">
	                                                <ItemTemplate>
	                                                    <asp:CheckBox ID="chEmployerObligo" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("EmployerObligo").ToString())%>' />
	                                                </ItemTemplate>
	                                                <EditItemTemplate>
	                                                    <asp:CheckBox ID="chEmployerObligo" runat="server" Checked='<%#bool.Parse(Eval("EmployerObligo").ToString())%>' />
	                                                </EditItemTemplate>
	                                            </asp:TemplateField>

                                                    --%>
                                                    <asp:TemplateField HeaderText="Affidavit" SortExpression="Affidavit">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chAffidavit" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("Affidavit").ToString())%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chAffidavit" runat="server" Checked='<%#bool.Parse(Eval("Affidavit").ToString())%>' />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="M.Certi" SortExpression="MarriageCertificate" ItemStyle-Width="80px">
                                                        <ItemTemplate>
                                                            <%# Eval("MarriageCertificateT")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlMarriageCertificate" runat="server" AppendDataBoundItems="true"
                                                                Style="width: 100%">
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                <asp:ListItem Value="2">TBT</asp:ListItem>
                                                                <asp:ListItem Value="3">T Done</asp:ListItem>
                                                                <asp:ListItem Value="4">N/A</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="POA" SortExpression="PowerofAttorney">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chPowerofAttorney" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("PowerofAttorney").ToString())%>' />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chPowerofAttorney" runat="server" Checked='<%#bool.Parse(Eval("PowerofAttorney").ToString())%>' />
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
                            <div style="height: 30px;">
                                <table cellpadding="0" style="width: 100%; height: 30px;">
                                    <tr>
                                        <td align="right" valign="bottom" style="font-size: 10px">
                                            <b>TBT</b> = To Be Translated, <b>T DONE</b> = Translation Done, <b>AL-33 / AL-15</b>
                                            = Application Form, <b>Doc in Orange</b> = To be submitted by the company, <b>M. Certi</b>
                                            = Marriage Certificate, <b>POA</b> = Power of Attorney
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="dvDocsProgress" style="position: absolute; background: white; left: 0px; top: 0px; right: 0px; bottom: 0px">
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
