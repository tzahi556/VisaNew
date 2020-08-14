<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Company.aspx.cs" Inherits="Pages_Company" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../ScrollableGridPlugin.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-ui-1.8.23.custom.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dropkick-1.0.0.js" type="text/javascript" ></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dataTables.min.js" type="text/javascript" ></script>
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/dropkick.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/datatable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/jquery-ui-1.8.23.custom/css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet"  type="text/css" />
    <link href="../App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
      
		/* Create an array with the values of all the checkboxes in a column */
		$.fn.dataTableExt.afnSortData['dom-checkbox'] = function  ( oSettings, iColumn )
		{
			var aData = [];
			$( 'td:eq('+iColumn+') input', oSettings.oApi._fnGetTrNodes(oSettings) ).each( function () {
				aData.push( this.checked==true ? "1" : "0" );
			} );
			return aData;
		}

        function OpenCompanyDetails(CompanyId, CompanyName) {

			if(!CompanyName)
				CompanyName = "New Company";

            var x = parent.winPopup("pages/PopUp_editCompany.aspx?CompanyId=" + CompanyId, '1000px', '550px', CompanyName);
            //alert(x);

        }

        function ReturnValueFromModal(val) {
            if (val) {
                $("#btnSearch").click();
                //parent.setIframeContent('Pages/Expert.aspx');
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
        
                setTimeout(function() {
            
	            try
	            {
	            	var listHeight = getListHeight();
		            $("#<%=gvTheGrid.ClientID%>").dataTable({
		            	"bJQueryUI": true,
		            	"bPaginate": false,
		            	"bFilter": false,
		            	"bInfo": false,
		            	"aaSorting":[],
						"aoColumns": [
							null,
							null,
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
		        catch(err) {}
	            
	            $("#dvTheGridProgress").hide();
	            
			}, 100);

            $("#dvDocsProgress").show();
            
            setTimeout(function() {

	            try
	            {
	            	var sortingString = $("#<%=hdnSortInfo.ClientID%>").val();
	            	var sorting = [];
	            	if(sortingString != "") {
	            	
	            		var ss = sortingString.split(",");
	            		for(var i=0; i<ss.length; i++) {
	            			if(i == 0 || i == 2) {
		            			sorting[i] = parseInt(ss[i]);
		            		} else {
			            		sorting[i] = ss[i];
		            		}
	            		}
		            	
		            	sorting = [ sorting ];
	            	}

	            	var listHeight = getListHeight();
		            $("#<%=gvDocs.ClientID%>").dataTable({
		            	"bJQueryUI": true,
		            	"bPaginate": false,
		            	"bFilter": false,
		            	"bInfo": false,
		            	"aaSorting":sorting,
						"aoColumns": [
							null,
							null,
							{ "sSortDataType": "dom-checkbox" },
							null,
							null,
							{ "sSortDataType": "dom-checkbox" },
							{ "sSortDataType": "dom-checkbox" },
							{ "sSortDataType": "dom-checkbox" }
						],
		            	"sScrollY": "" + (listHeight - 28) + "px",
		            });
		            
					$("#gvDocs_wrapper .dataTables_scrollBody").on("scroll", function() { 
						$("#<%=hdnScrollPos.ClientID%>").val(
							$("#gvDocs_wrapper .dataTables_scrollBody").scrollTop());

					});
					
					$("#gvDocs_wrapper thead .DataTables_sort_wrapper").click(function() {
						
						setTimeout(function() {
							
							var sortInfo = $("#<%=gvDocs.ClientID%>").dataTable().fnSettings().aaSorting;
							$("#<%=hdnSortInfo.ClientID%>").val(sortInfo);
							
						}, 100);
					});
		        }
		        catch(err) {}
	            
				var headers = $(".DataTables_sort_wrapper");
				for(var i=0; i<headers.length; i++) {
				
					if($(headers[i]).html().indexOf("Obligo") >= 0) {
						$(headers[i]).append("<div style='background: orange; height: 3px; font-size: 1px;'></div>");
					}
				}

				var st = parseInt($("#<%=hdnScrollPos.ClientID%>").val());
				$("#gvDocs_wrapper .dataTables_scrollBody").scrollTop(st);

	            $("#dvDocsProgress").hide();
	            
			}, 100);
        }

        $(document).ready(function () {

            $("#tabs").tabs({
				select: function(event, ui) {
					switch (ui.index) {
						case 0:
						
							$("#dvTheGridProgress").show();
							
							setTimeout(function() {
								$("#<%=gvTheGrid.ClientID%>").dataTable().fnAdjustColumnSizing();
								$("#<%=gvTheGrid.ClientID%>").dataTable().fnAdjustColumnSizing();
								
								$("#dvTheGridProgress").hide();
								
							}, 100);
							
							break;
							
						case 1:
						
							$("#dvDocsProgress").show();
							
							setTimeout(function() {
								$("#<%=gvDocs.ClientID%>").dataTable().fnAdjustColumnSizing();
								$("#<%=gvDocs.ClientID%>").dataTable().fnAdjustColumnSizing();
								
								$("#gvDocs_wrapper .dataTables_scrollBody").on("scroll", function() { 
									$("#<%=hdnScrollPos.ClientID%>").val(
										$("#gvDocs_wrapper .dataTables_scrollBody").scrollTop());
			
								});

								$("#dvDocsProgress").hide();

							}, 100);
							
							break;
					}
				}
			});
        
			setTimeout(function() {
			
				var listHeight = getListHeight();			

	            $("#dvGridContainer").css("height", (listHeight + 10) + "px");
	            $("#dvDocs").css("height", (listHeight + 40) + "px");
				
			}, 50);
			
            
            $(".ui-tabs-panel").css("padding", "5px");
            //  alert($(".ui-state-active").attr("id"));

            $("body").css("display", "");

            //$(".ui-tabs-nav A").css("background", "red");

//            $('#<%=gvTheGrid.ClientID %>').Scrollable({
//                ScrollHeight: 400
//              
//            });
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
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <table  border="0" cellpadding="5" cellspacing="5">
                        <tr>
                        	<td>
                               Company Name:
                            
                            </td>
                           
                           
                            <td style="font-size: 14px;">
                               
                                <asp:TextBox ID="txtName" runat="server" AutoPostBack="true" OnTextChanged="txt_TextChanged"></asp:TextBox>
                            </td>
                               <td>
                               Company Number:
                            
                            </td>


                            <td style="font-size: 14px;">
                               
                                <asp:TextBox ID="txtNumber" runat="server" AutoPostBack="true" OnTextChanged="txt_TextChanged"></asp:TextBox>
                            </td>
                            <td style="font-size: 14px; width: 80px" class="blueButton" align="right">
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
	                <li><a href="#tabs-1">Company</a></li>
	                <li><a href="#tabs-2">Company Docs</a></li>
	            </ul>
	            <div id="tabs-1">
	                <div class="dvTabContent">
	                   <div id="dvGridContainer" style="overflow: auto; overflow-x: hidden">
	                    <div>
	                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
	                            <ContentTemplate>
	                                <asp:GridView ID="gvTheGrid" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="false"
	                                    BackColor="WhiteSmoke" AllowPaging="false" AllowSorting="false" OnSorting="gvTheGrid_Sorting"
	                                    Width="100%" CssClass="myGrid myGridInTabs" OnPageIndexChanging="gvTheGrid_PageIndexChanging"
	                                    EmptyDataText="No Company Found!" EmptyDataRowStyle-Font-Bold="true" OnRowDeleting="gvTheGrid_RowDeleting"
	                                    EmptyDataRowStyle-Font-Size="Medium" OnRowDataBound="gvTheGrid_RowDataBound"
	                                    DataKeyNames="CompanyId">
	                                    <Columns>
	
	                                     <asp:TemplateField HeaderText="">
	                                            <ItemTemplate>
	                                                <img id="imgEdit" style="padding: 2px;" title="Edit" onclick="OpenCompanyDetails('<%# Eval("CompanyId") %>', '<%# Eval("Name") %>')"
	                                                    src="../App_Themes/Theme1/images/edit3.png" width="20px" height="20px" />
	                                            </ItemTemplate>
	                                        </asp:TemplateField>
	                                     
	                                        <asp:TemplateField HeaderText="Name"    SortExpression="Name">
	                                            <ItemTemplate>
	                                                <%#Eval("Name").ToString()%>
	                                            </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="Reg.Number" SortExpression="Number">
	                                            <ItemTemplate>
	                                                <%#Eval("Number").ToString()%>
	                                            </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="Interior Reg" SortExpression="Interior Reg" >
	                                            <ItemTemplate>
	                                                <%#Eval("Interior Reg").ToString()%>
	                                            </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="Country" SortExpression="Country">
	                                            <ItemTemplate>
	                                                <%#Eval("Country").ToString()%>
	                                            </ItemTemplate>
	                                        </asp:TemplateField>
	                                      <%--  <asp:TemplateField HeaderText="Email" SortExpression="Email">
	                                            <ItemTemplate>
	                                                <%#Eval("Email").ToString()%>
	                                            </ItemTemplate>
	                                        </asp:TemplateField>--%>
	                                        <asp:TemplateField HeaderText="Contact Person" SortExpression="ContactMan">
	                                            <ItemTemplate>
	                                                <%#Eval("ContactMan").ToString()%>
	                                            </ItemTemplate>
	                                        </asp:TemplateField>
	                                      <%--  <asp:TemplateField HeaderText="Contact Person Email" SortExpression="ContactManEmail">
	                                            <ItemTemplate>
	                                                <%#Eval("ContactManEmail").ToString()%>
	                                            </ItemTemplate>
	                                        </asp:TemplateField>--%>


	                                      <%--  <asp:TemplateField HeaderText="Contact Person Phone" SortExpression="ContactManPhone">
	                                            <ItemTemplate>
	                                                <%#Eval("ContactManPhone").ToString()%>
	                                            </ItemTemplate>
	                                        </asp:TemplateField>
--%>


	                                      <%--  <asp:TemplateField HeaderText="Is Expert Allowed" SortExpression="IsExpAllow">
	                                            <ItemTemplate>
	                                                <asp:CheckBox ID="chExpAllow" runat="server" Enabled="false" Checked='<%#bool.Parse(Eval("IsExpAllow").ToString())%>' />
	                                            </ItemTemplate>
	                                        </asp:TemplateField>--%>

                                              <asp:TemplateField HeaderText="Parent Company" SortExpression="ParentCompany" ItemStyle-Width="210">
	                                            <ItemTemplate>
	                                                <%#Eval("ParentCompany").ToString()%>
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
		                <div id="dvTheGridProgress" style="position: absolute; background: white; left: 0px; top:0px; right: 0px; bottom: 0px">
		                	<div style="position: absolute; top: 50%; left: 50%; margin-top: -16px; margin-left: -32px;">
		                		<img src="../App_Themes/Theme1/Images/ajax-loader.gif" width="32px" height="32px"/>
		                	</div>
		                </div>
                    </div>
				    <div style="margin-top: 4px; text-align: left;" class="blueButton">
				        <button id="btnNew" type="button" runat="server" onclick="OpenCompanyDetails(-1);">
				            New Company
				        </button>
				    </div>
                </div>
            </div>
	        <div id="tabs-2">
	            <div class="dvTabContent">
	                <div id="dvDocs" style="overflow: auto; overflow-x: hidden">
	                    <div>
	                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
	                            <ContentTemplate>
	                                <asp:GridView ID="gvDocs" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="false"
	                                    BackColor="WhiteSmoke" AllowPaging="false" DataKeyNames="CompanyId,PowerofAttorney,certificate"
	                                    OnRowEditing="gvDocs_RowEditing" OnRowCancelingEdit="gvDocs_RowCancelingEdit"
	                                    OnRowUpdating="gvDocs_RowUpdating" Width="100%" CssClass="myGrid myGridInTabs"
	                                    EmptyDataRowStyle-Font-Size="Medium" EmptyDataRowStyle-Font-Bold="true" EmptyDataText="No Company Found!!"
	                                    AllowSorting="false" OnSorting="gvDocs_Sorting">
	                                    <Columns>
	                                        <asp:TemplateField>
	                                            <HeaderTemplate>
	                                                Edit
	                                            </HeaderTemplate>
	                                            <ItemTemplate>
	                                                <asp:ImageButton ID="imgEdit" runat="server" AlternateText="Edit" CommandName="Edit"
	                                                    ImageUrl="~/App_Themes/Theme1/Images/edit3.png" ToolTip="Edit"  width="20px" height="20px" Style="padding: 4px" />
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
	                                        <asp:BoundField DataField="Name" ReadOnly="true" HeaderText="Name" HeaderStyle-CssClass=""
	                                            SortExpression="Name"></asp:BoundField>
	                                        <asp:TemplateField HeaderText="Affidavit" SortExpression="Affidavit">
	                                            <ItemTemplate>
	                                                <asp:CheckBox ID="chAffidavit" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("Affidavit").ToString())%>' />
	                                            </ItemTemplate>
	                                            <EditItemTemplate>
	                                                <asp:CheckBox ID="chAffidavit" runat="server" Checked='<%#bool.Parse(Eval("Affidavit").ToString())%>' />
	                                            </EditItemTemplate>
	                                        </asp:TemplateField>
	                                    <%--    <asp:TemplateField HeaderText="Application Form" SortExpression="ApplicationForm">
	                                            <ItemTemplate>
	                                                <asp:CheckBox ID="chApplicationForm" runat="server" OnClick="return false" Checked='<%#bool.Parse(Eval("ApplicationForm").ToString())%>' />
	                                            </ItemTemplate>
	                                            <EditItemTemplate>
	                                                <asp:CheckBox ID="chApplicationForm" runat="server" Checked='<%#bool.Parse(Eval("ApplicationForm").ToString())%>' />
	                                            </EditItemTemplate>
	                                        </asp:TemplateField>--%>
	                                        <asp:TemplateField HeaderText="POA" SortExpression="PowerofAttorney" ItemStyle-Width="80px">
	                                            <ItemTemplate>
	                                                <%# Eval("PowerofAttorneyT")%>
	                                            </ItemTemplate>
	                                            <EditItemTemplate>
	                                                <asp:DropDownList ID="ddlAttorney" runat="server" AppendDataBoundItems="true">
	                                                    <asp:ListItem Value="0">No</asp:ListItem>
	                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
	                                                    <asp:ListItem Value="2">TBT</asp:ListItem>
	                                                    <asp:ListItem Value="3">T Done</asp:ListItem>
	                                                </asp:DropDownList>
	                                            </EditItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="Inc.Certi" SortExpression="certificate" ItemStyle-Width="80px">
	                                            <ItemTemplate>
	                                                <%# Eval("certificateT")%>
	                                            </ItemTemplate>
	                                            <EditItemTemplate>
	                                                <asp:DropDownList ID="ddlcertificate" runat="server" AppendDataBoundItems="true">
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
	                                    </Columns>
	                                </asp:GridView>
	                            </ContentTemplate>
	                        </asp:UpdatePanel>
	                    </div>
	                    
				        <div style="height: 30px;">
				        	<table cellpadding="0" style="width: 100%; height: 30px; ">
				        		<tr>
				        			<td align="right" valign="bottom" style="font-size: 10px">
				        				<b>TBT</b> = To Be Translated,
				        				<b>T DONE</b> = Translation Done,
				        				<b>Doc in Orange</b> = To be submitted by the company,
				        				<b>POA</b> = Power of Attorney
				        			</td>
				        		</tr>
				        	</table>
				        </div>

		                <div id="dvDocsProgress" style="position: absolute; background: white; left: 0px; top:0px; right: 0px; bottom: 0px">
		                	<div style="position: absolute; top: 50%; left: 50%; margin-top: -16px; margin-left: -32px;">
		                		<img src="../App_Themes/Theme1/Images/ajax-loader.gif" width="32px" height="32px"/>
		                	</div>
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
