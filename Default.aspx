<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Visa And Document Tracking</title>
  <meta name="google" value="notranslate" />
    <script src="App_Themes/jquery-ui-1.8.23.custom/js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="App_Themes/jquery-ui-1.8.23.custom/js/jquery-ui-1.8.23.custom.min.js"
        type="text/javascript"></script>
    <link href="App_Themes/jquery-ui-1.8.23.custom/css/smoothness/jquery-ui-1.8.23.custom.css"
        rel="stylesheet" type="text/css" />
    <script src="akquinet-jquery-toastmessage-plugin-6f5d7bf/src/main/javascript/jquery.toastmessage.js"
        type="text/javascript"></script>
    <link href="akquinet-jquery-toastmessage-plugin-6f5d7bf/src/main/resources/css/jquery.toastmessage.css"
        rel="stylesheet" type="text/css" />
    <link href="App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
   
        var dialog;
        var retVal = false;
      


        function Confirm() {
            dialog = $("<div id='dialog' title='Confirmation Required'>Are you sure about this?</div>");
            dialog.dialog({

                modal: true,
                buttons: {
                    "Confirm": function () {
                        $(this).dialog("close");
                        return true;
                       
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                        return false;
                       
                    }
                }
            });

            $(".ui-dialog").css("padding", "4px").css("font-size", "small");

        }


        function winPopup(Url, Width, Height, Title) {
            dialog =  $("<iframe class='ifss' src='" + Url + "' frameborder='0' ></iframe>");
            dialog.dialog({
                modal: true,
                draggable: true,
                resizable: true,
                title: Title,
                width: Width,
                height: Height,
                position: 'top',
                close: function (ev, ui) { $('#ifMain').get(0).contentWindow.ReturnValueFromModal(retVal) }

            });

            $(".ui-dialog-titlebar").css("padding", "4px").css("font-size", "small");
             $(".ifss").css("width", Width).css("height", Height).css("padding", "0px").css("padding-top", "5px");
            $(".ui-dialog").css("margin-top", "30px");
        }






        var MainHeight;


        function ShowMessage(text,type) {
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


        $(function () {
         
            //

            var screenHeight = $(window).height();

            $("#accordion").accordion({
                event: "mouseover",
                autoHeight: false,
                navigation: true

            });

            MainHeight = eval(screenHeight) - $("#dvMainContainer").height();

            // $("#dvMainContainer").css("height", MainHeight * 0.10);

            // $("#dvTitle").css("height", eval(screenHeight) * 0.10);

            setIframeContent("Pages/Expert.aspx");
            //setIframeContent("Pages/ReportGenrator.aspx");
        
			$('#dvBlocker').click(function(e){
			
				evt = e || window.event;
				
				$('#dvBlocker').hide();
				
				// get element at point of click
				starter = document.elementFromPoint(evt.clientX, evt.clientY);
				
				$(starter).click();
				
                $("#dvAccordion").slideUp(300);
                $("#btnMenu").html("Open Menu");
                $('#dvBlocker').hide();
			});


        });


        function ShowHide() {

            var isClose = $("#dvAccordion").css("display");
            if (isClose == "none") {
                $("#dvAccordion").slideDown(300);
                $("#btnMenu").html("Close Menu");
                $('#dvBlocker').show();
                $('#dvBlocker').fadeTo(0, 0);
            } else {

                $("#dvAccordion").slideUp(300);
                $("#btnMenu").html("Open Menu");
                $('#dvBlocker').hide();
            }
        }

        function setIframeContent(Url) {

            //            var objFrame = document.getElementById("ifMain");

            //             objFrame.src = Url;



            $("#ifMain").remove();

            $("<iframe id='ifMain' height='100%' src=" + Url + " width='100%' frameborder='0' allowtransparency='true'></iframe>").appendTo('#dvIframeContainer');

            //location.replace = Url;

            // $("#dvAccordion").css("display", "none");
            if ($("#dvAccordion").css("display")!="none") {
                ShowHide();
            }

        }

        function ToolBarClick(type) {
            if (type == "1") {
                window.location.href = "loginmain.aspx";
            }
            if (type == "2") {

                var w = "1024";
                var h = "700";
                var left = (screen.width / 2) - (w / 2);
                var top = (screen.height / 2) - (h / 2);
                var targetWin = window.open('http://www.dglaw.co.il/?page_id=19', "", 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);



            }
            if (type == "3") {
                var w = "1024";
                var h = "700";
                var left = (screen.width / 2) - (w / 2);
                var top = (screen.height / 2) - (h / 2);
                var targetWin = window.open('http://www.dglaw.co.il', "", 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);






            }
        }
		
	</script>
</head>
<body style="background: #F2F2FC;">
    <form id="form1" runat="server">


    <div class="dvMain">
        <%--
     <div style="background:red">
           
            <img src="App_Themes/Theme1/Images/search.png"  style="background-image:url(../App_Themes/Theme1/Images/transparent.png)"/>
        </div>--%>
        <div id="dvMainContainer">
            <div id="dvTitle" class="dvTopBar">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                    	<td id="btnMenuContainer" style="width: 120px" runat="server">
                            <button id="btnMenu" type="button" runat="server" onclick="ShowHide();" class="menuButton">
                                Open Menu
                            </button>
                            <div id="dvAccordion" class="dvRoundAndBgMenu" style="height:320px;font-size: 12px; position: absolute;display:none;
                                width: 200px; z-index:10">
                                <div id="accordion">
                                    <h3>
                                        <a href="#">Navigator</a></h3>
                                    <div>
                                        <ul>
                                            <li onclick="setIframeContent('Pages/Expert.aspx');">
                                                <img src="App_Themes/Theme1/Images/mans.png" height="16" width="16" />
                                                Experts</li>

                                              <li onclick="setIframeContent('Pages/ExpertIncoming.aspx');">
                                                <img src="App_Themes/Theme1/Images/mans.png" height="16" width="16" />
                                                Experts Incoming</li>
                                            <li onclick="setIframeContent('Pages/Company.aspx');">
                                                <img src="App_Themes/Theme1/Images/company.png" height="16" width="16" />
                                                Companies</li>
                                            <li onclick="setIframeContent('Pages/Users.aspx');">
                                                <img src="App_Themes/Theme1/Images/users.png" height="16" width="16" />
                                                Users</li>

                                            <li onclick="setIframeContent('Pages/Upload.aspx');">
                                                <img src="App_Themes/Theme1/Images/up.png" height="16" width="16" />
                                                Others</li>
                                              <li onclick="setIframeContent('Pages/SendEmailWeekly.aspx');">
                                                <img src="App_Themes/Theme1/Images/cal.jpg" height="16" width="16" />
                                                Emails Schedular</li>

<%--                                             <li onclick="setIframeContent('Calendar/Calendar.aspx');">
                                                <img src="App_Themes/Theme1/Images/cal.jpg" height="16" width="16" />
                                                Calendar</li>--%>

                                                    <li onclick="setIframeContent('Pages/Report.aspx');">
                                                <img src="App_Themes/Theme1/Images/excel.png" height="16" width="16" />
                                                Report</li>
                                         
                                        </ul>
                                    </div>
                                </div>
                            </div>
                    	</td>
                        <td align="center" style="width: 300px">
                            <div style="font-size: 0.8em; font-weight: bold">
                                DG Law - Visas and Documents Tracking</div>
                        </td>
                        <td style="width:120px; visibility:hidden;" class="screenTitle">
                    		<div id="dvScreenTitle">Experts</div>
                        </td>
                        <td>&nbsp;</td>
                        <td align="right" style="width: 500px; font-size: 14px; color:#ff8877">
                         	:: <span id="spName" runat="server"></span> ::
                        </td>
                        <td align="right" style="width: 200px; font-size: 14px;">
                            <span style="border-right: dotted 1px gray">&nbsp;<span class="spToolBar" onclick="ToolBarClick(1)">Exit</span>
                                &nbsp;</span><span style="border-right: dotted 1px gray">&nbsp;<span class="spToolBar"
                                    onclick="ToolBarClick(2)">&nbsp;Contact&nbsp;</span>&nbsp;</span><span>&nbsp;<span class="spToolBar"
                                        onclick="ToolBarClick(3)">&nbsp;About Us</span>&nbsp;&nbsp;</span>
                        </td>
                    </tr>
                </table>
            </div>
            <%--   <div id="dvMenu" runat="server" style="margin-left: 15px; width: 17%;">
                <div onclick="ShowHide()" class="dvMenuTitle">
                    Menu
                </div>
             
            </div>--%>
        </div>
        <div id="dvIframeContainer" class="dvIframeBg">
        </div>
       <%-- <iframe ID="KeepAliveFrame" src="KeepSessionAlive.aspx" frameBorder="0" width="0" height="0" runat="server"></iframe>--%>
        <div id="dvBlocker" style="position:absolute; width: 100%; height: 100%; background: red; z-index:9; display:none">
        	&nbsp;
        </div>
    </div>
    </form>
</body>
</html>
