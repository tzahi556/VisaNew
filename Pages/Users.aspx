<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Users.aspx.cs"   Inherits="Pages_Users" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-ui-1.8.23.custom.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dataTables.min.js" type="text/javascript" ></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dropkick-1.0.0.js" type="text/javascript" ></script>
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/dropkick.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/datatable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/jquery-ui-1.8.23.custom/css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />

	<style type="text/css">
		input[type=text] {
			margin: 5px;
			width: 80px;
		}
		
		th div {
			font-size: 12px;
		}
		
		.dk_options_inner, .dk_touch .dk_options {
		    max-height: 130px;
		}
		
	</style>

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

		$.fn.dataTableExt.afnSortData['include-input'] = function  ( oSettings, iColumn )
		{
			var aData = [];
			
			$( 'td:eq('+iColumn+')', oSettings.oApi._fnGetTrNodes(oSettings) ).each( function () {
			
				if($(this).children().length > 0) {
				
					aData.push($(this).children("input:first").val().trim());
					
				} else {

					aData.push($(this).html().trim());
				}
			});
			
			return aData;
		}

		$.fn.dataTableExt.afnSortData['include-select'] = function  ( oSettings, iColumn )
		{
			var aData = [];
			
			$( 'td:eq('+iColumn+')', oSettings.oApi._fnGetTrNodes(oSettings) ).each( function () {
			
				if($(this).children().length > 0) {
				
					var t = $(this).children("select:first").children("option:selected").text();
					aData.push(t);
					
				} else {

					aData.push($(this).html().trim());
				}
			});
			
			return aData;
		}

        function EnableTheme() {
        
        	//$("select").dropkick();
			$("select").css("margin-top", 5);
			$("select").css("margin-bottom", 5);
        	
			var listHeight = getListHeight();
        	
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

		            setTimeout(function() {
		            
			            $("#<%=GridView1.ClientID%>").dataTable({
			            	"bJQueryUI": true,
			            	"bPaginate": false,
			            	"bFilter": false,
			            	"bInfo": false,
			            	"aaSorting": sorting,
							"aoColumns": [
								null,
								{ "sSortDataType": "include-input" },
								{ "sSortDataType": "include-input" },
								{ "sSortDataType": "include-input" },
								{ "sSortDataType": "include-input" },
								{ "sSortDataType": "include-input" },
								{ "sSortDataType": "include-select" },
								{ "sSortDataType": "include-input" },
                                { "sSortDataType": "dom-checkbox" },
								{ "sSortDataType": "dom-checkbox" },
								null
							],
			            	"sScrollY": "" + (listHeight - 50) + "px",
			            });
			            
						$("#GridView1_wrapper .dataTables_scrollBody").on("scroll", function() { 
							$("#<%=hdnScrollPos.ClientID%>").val(
								$("#GridView1_wrapper .dataTables_scrollBody").scrollTop());
	
						});
						
						$("#GridView1_wrapper thead .DataTables_sort_wrapper").click(function() {
							
							setTimeout(function() {
								
								var sortInfo = $("#<%=GridView1.ClientID%>").dataTable().fnSettings().aaSorting;
								$("#<%=hdnSortInfo.ClientID%>").val(sortInfo);
								
							}, 100);
						});
			        
						var headers = $(".DataTables_sort_wrapper");
						for(var i=0; i<headers.length; i++) {
						
							if($(headers[i]).html().indexOf("Obligo") >= 0) {
								$(headers[i]).append("<div style='background: orange; height: 3px; font-size: 1px;'></div>");
							}
						}
						
						var st = parseInt($("#<%=hdnScrollPos.ClientID%>").val());
						$("#GridView1_wrapper .dataTables_scrollBody").scrollTop(st);

			            
		            }, 100);
		            
				}
		        catch(err) {}
	        }, 100);
	    }

        $(document).ready(function () {

        
			setTimeout(function() {
			
				var listHeight = getListHeight();			

				$("#dvGridContainer").css("height", listHeight + "px");
				
			}, 50);

        });

        function pageLoad() {
        
        	EnableTheme();
        }
        
        function getListHeight() {
	        
			var listHeight = (eval(parent.MainHeight) - 80);
			
			return listHeight;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:HiddenField ID="hdnScrollPos" runat="server" Value="0" />
        <asp:HiddenField ID="hdnSortInfo" runat="server" Value="" />

    <div>
        <div class="dvNoRoundAndBg" style="margin: 10px;">

            <div align="left" Style="text-align:left;padding-bottom:3px;font-size:large;font-weight:bold">Users - Table</div>
            <div id="dvGridContainer" style="overflow: auto; overflow-x: hidden; margin-top: 10px;">
                <div style="padding-right: 10px">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" GridLines="Both" CellPadding="3" AutoGenerateColumns="false"
                                BackColor="WhiteSmoke" AllowPaging="false" AllowSorting="false" OnSorting="GridView1_Sorting"
                                 DataKeyNames="UserId,CompanyId,RoleId"   Width="100%" CssClass="myGrid myGridInTabs" 
                                EmptyDataText="No Users!!" OnRowCommand="GridView1_RowCommand" OnRowEditing="GridView1_RowEditing"
                                OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating"
                                OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound"
                                ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEdit" runat="server" AlternateText="Edit" CommandName="Edit"
                                                ImageUrl="~/App_Themes/Theme1/Images/edit3.png" ToolTip="Edit" Style="padding: 4px" width="20px" height="20px" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgCancel" runat="server" AlternateText="Cancel" CommandName="CANCEL"
                                                ImageUrl="~/App_Themes/Theme1/Images/cancel.png" ToolTip="Cancel" CausesValidation="false"
                                                Style="" />
                                            <asp:ImageButton ID="imgUpdate" runat="server" AlternateText="Save" CommandName="Update"
                                                ImageUrl="~/App_Themes/Theme1/Images/save1.jpg" ValidationGroup="edit" ToolTip="Save"
                                                Style="" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="btnInsert" runat="Server" CommandName="Insert" Text="Insert"
                                               Style="padding: 4px" ImageUrl="~/App_Themes/Theme1/Images/insert.jpg" ValidationGroup="insert"/>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                  


                                    <asp:TemplateField HeaderText="Name" SortExpression="FirstName">
                                        <ItemTemplate>
                                            <%#Eval("FirstName").ToString()%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFirstName" runat="server"  Text=' <%#Eval("FirstName").ToString()%>'></asp:TextBox>
                                             <asp:RequiredFieldValidator runat="server" id="reqName" 
                                              controltovalidate="txtFirstName" ForeColor="red" errormessage="*"  ValidationGroup="edit"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFirstName" runat="server" ></asp:TextBox>
                                             <asp:RequiredFieldValidator runat="server" id="reqName"
                                              controltovalidate="txtFirstName" ForeColor="red" errormessage="*" ValidationGroup="insert"/>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Surname" SortExpression="LastName">
                                        <ItemTemplate>
                                            <%#Eval("LastName").ToString()%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtLastName" runat="server"  Text=' <%#Eval("LastName").ToString()%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtLastName" runat="server" width="70"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                                              controltovalidate="txtLastName" ForeColor="red" errormessage="*" ValidationGroup="insert"/>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Username" SortExpression="UserName">
                                        <ItemTemplate>
                                            <%#Eval("UserName").ToString()%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUserName" runat="server"  Text=' <%#Eval("UserName").ToString()%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtUserName" runat="server" width="70"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                              controltovalidate="txtUserName" ForeColor="red" errormessage="*" ValidationGroup="insert"/>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Password" SortExpression="Password">
                                        <ItemTemplate>
                                            <%#Eval("Password").ToString()%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPassword" runat="server"  Text=' <%#Eval("Password").ToString()%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtPassword" runat="server" width="70"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  runat="server" 
                                              controltovalidate="txtPassword" ForeColor="red" errormessage="*" ValidationGroup="insert"/>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Email" SortExpression="Email">
                                        <ItemTemplate>
                                            <%#Eval("Email").ToString()%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEmail" runat="server"  Text=' <%#Eval("Email").ToString()%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtEmail" runat="server" ></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Company" SortExpression="Company" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <%#Eval("Company").ToString()%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                             <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true" style="width:200px">
                                                 <asp:ListItem Value="0">--- Company ---</asp:ListItem>
                                             </asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                             <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true" style="width:200px">
                                                 <asp:ListItem Value="0">--- Company ---</asp:ListItem>
                                             </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                
                                
                                   <asp:TemplateField HeaderText="Role" SortExpression="Role">
                                        <ItemTemplate>
                                            <%#Eval("Role").ToString()%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                               <asp:DropDownList ID="ddlRoles" runat="server" AppendDataBoundItems="true">
                                                 <asp:ListItem Value="0">---Role--- </asp:ListItem>
                                             </asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                              <asp:DropDownList ID="ddlRoles" runat="server" AppendDataBoundItems="true">
                                                 <asp:ListItem Value="0">---Role--- </asp:ListItem>
                                             </asp:DropDownList>

                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  runat="server" InitialValue="0" 
                                              controltovalidate="ddlRoles" ForeColor="red" errormessage="*" ValidationGroup="insert"/>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                


                                  <asp:TemplateField HeaderText="IsEmail" SortExpression="IsEmail">
                                        <ItemTemplate>
                                             <asp:CheckBox ID="chIsEmail" runat="server" Enabled="false" Checked='<%#bool.Parse(Eval("IsEmail").ToString())%>'/> 
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="chIsEmail" runat="server" Checked='<%#bool.Parse(Eval("IsEmail").ToString())%>'/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                           <asp:CheckBox ID="chIsEmail" runat="server" Checked="false"/>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                  

                                
                                    <asp:TemplateField HeaderText="Active" SortExpression="Active">
                                        <ItemTemplate>
                                             <asp:CheckBox ID="chActive" runat="server" Enabled="false" Checked='<%#bool.Parse(Eval("Active").ToString())%>'/> 
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="chActive" runat="server" Checked='<%#bool.Parse(Eval("Active").ToString())%>'/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                           <asp:CheckBox ID="chActive" runat="server" Checked="true"/>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Delete" CommandArgument='<%# Eval("UserId") %>'
                                                CommandName="Delete" ImageUrl="~/App_Themes/Theme1/Images/delete.png" Style="padding: 4px"
                                                ToolTip="Delete" CausesValidation="false" />
                                            <%--  <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("UserId") %>' CommandName="Delete"
                                                runat="server">
                                                           Delete</asp:LinkButton>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
