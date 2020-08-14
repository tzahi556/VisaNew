<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginMain.aspx.cs" Inherits="LoginMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
	<script type="text/javascript" src="App_Themes/jquery-ui-1.10.3.custom/js/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="App_Themes/jquery-ui-1.10.3.custom/js/jquery-ui-1.10.3.custom.min.js"></script>
    <script type="text/javascript" src="App_Themes/jquery-ui-1.10.3.custom/js/jquery.inputhints.min.js"></script>
    <link href="App_Themes/jquery-ui-1.10.3.custom/css/custom-theme/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />
    <script language="javascript">

        $(document).keypress(function (event) {

            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (keycode == '13') {
                $('#btnLogin').trigger('click');
            }

        });
    </script>
</head>
<body>
    <form id="Form1" runat="server">

        <div id="dvLogin" class="dvRoundAndBg">
         

        	<div class="abs-left">
        		<div class="abs-top">
        			<h2 style="color: #025482; font-size: 21px;">Welcome To DG Law</h2>
        			<div style="text-align: right; color: #555555; font-size: 12px;"> Documents Tracking</div>
        		</div>
        		<div class="abs-middle">
        			<div style="position: absolute; top: 50%; margin-top: -25px; height: 50px;">
	                  	<div>
							<asp:TextBox ID="txtUserName" ToolTip="Username" runat="server" MaxLength="20" Width="180"/>
						</div>
						<div style="margin-top:5px">
							<asp:TextBox ID="txtPassword" ToolTip="Password" TextMode="SingleLine" MaxLength="20" runat="server" Width="180"/>
						</div>
						<div style="margin-top:20px; text-align: left;">
			              	<input type="button" id="btnLogin" runat="server" onserverclick="btnLogin_Click" value="Sign In"/>
						</div>
					</div>
        		</div>
        	</div>
        	<div class="abs-right">
        		<img src="App_Themes/Theme1/Images/login.png" alt="" style="position: absolute; left: 50%; top: 50%; margin-left:-82px; margin-top:-90px;" width="165px" height="180px"/>
        	</div>
        	<div class="abs-bottom">
				<asp:Label ID="lblMsg" runat="server" Text="" CssClass="ui-state-error">&nbsp;</asp:Label>
        	</div>
	    </div>
    </form>
    <script type="text/javascript">
        $("#<%= btnLogin.ClientID %>").button();

        var prevUserName = $("input[id=<%= txtUserName.ClientID %>]").val();
        var prevPassword = $("input[id=<%= txtPassword.ClientID %>]").val();

        $("input[title]").inputHints();

        $("input[id=<%= txtPassword.ClientID %>]")
		      .focus(function () { $(this).attr("type", "password"); })
		      .blur(function () {

		          if ($(this).val() && $(this).val().length > 0 &&
		          		$(this).val() != $(this).attr("title")) {
		              $(this).attr("type", "password");
		          } else {
		              $(this).attr("type", "text");
		          }
		      });

        if (prevUserName) {
            $("input[id=<%= txtUserName.ClientID %>]").val(prevUserName);
        }

        if (prevPassword && prevPassword.length > 0 && prevPassword !=
			$("input[id=<%= txtPassword.ClientID %>]").attr("title")) {
            $("input[id=<%= txtPassword.ClientID %>]").val(prevPassword);
            $("input[id=<%= txtPassword.ClientID %>]").attr("type", "password");
        }
		       
    </script>
</body>
</html>
