<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUp_editSendEmailWeekly.aspx.cs"
    Inherits="Pages_PopUp_editSendEmailWeekly" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" class="popupHtml">
<head id="Head1" runat="server">
    <title>Customer</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-ui-1.8.23.custom.min.js"
        type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dropkick-1.0.0.js" type="text/javascript"></script>
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/dropkick.css" rel="stylesheet"
        type="text/css" />
    <link href="../App_Themes/jquery-ui-1.8.23.custom/css/smoothness/jquery-ui-1.8.23.custom.css"
        rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .dk_fouc select {
            position: relative;
            top: 0em;
            visibility: visible;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {


            $(".txtCalander").datepicker({
                showOn: "button",
                buttonImage: "../App_Themes/Theme1/Images/calendar.gif",
                buttonImageOnly: true,
                dateFormat: "d MM yy"
            });

            $(".ui-datepicker").css("font-size", "11px");

            $(".ui-datepicker-trigger").css("position", "relative").css("left", "3px").css("top", "2px");

            $("#btnSave,#btnSendNow").button();
            $("#btnCancel").button();
            $("#btnGenerate").button();

            $("select").dropkick();
           // $('#lblCompanyIds').val("0");

            $('body').click(function (evt) {
                var elementId = evt.target.id;



                if (elementId == "imgCompany" || elementId.substring(0, 13) == "chCompanyList")
                    return;

                $("#dvChlist").css("display", "none");
              
                //Do processing of click event here for every element except with id menu_content

            });


            $('#chCompanyList').find('input[type="checkbox"]').click(function (e) {
               
                var Company = $(this).next().html();

                var str = $('#dvComp').html();
                var n = str.includes(Company);

                if (!n)
                {
                    $('#dvComp').append(Company + "<br/>");
                    $('#lblCompanyIds').val($('#lblCompanyIds').val() + "," + this.value);
                }
                else
                {
                    $('#dvComp').html($('#dvComp').html().replace(Company + "<br>", ""));
                    $('#lblCompanyIds').val($('#lblCompanyIds').val().replace("," + this.value, ""));

                }
                   
                
              //  var txt = this.value;
               // alert(txt);
            });


        });


        function closeDialog() {

            parent.dialog.dialog('close');


        }


        function ClientScriptOnSave() {
           
            parent.retVal = true;
        }

        function OpenLink() {



        }

        function ShowHide() {
            $("#dvChlist").css("display", "");
        }



   

    </script>
</head>
<body class="popupBody">
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdn" runat="server" Value="0" />
        <div align="center">
            <asp:Label ID="lblTitle" Text="New Email Task" Font-Bold="true" Font-Size="Large" runat="server"
                Style="display: none"></asp:Label>

            <div class="dvRoundAndBgPopup" style="font-size: medium; margin-top: 8px; padding-top: 20px;">
                <div class="section-head">
                    Companies
                </div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">

                    <tr>

                        <td id="tdCompany" align="left" valign="top">
                            <img id="imgCompany" src="../App_Themes/Theme1/Images/Selectcompany.png" onclick="ShowHide();" /><br />
                            <div align="left" id="dvChlist" style="display: none; position: absolute; background: white; border: solid 2px #385D8A; left: 10px; text-align: left; height: 300px; overflow: auto; z-index: 9999">
                           
                                  <asp:CheckBoxList ID="chCompanyList" CellPadding="8" CellSpacing="8" AppendDataBoundItems="true"
                                    
                                    runat="server" >
                                   
                                </asp:CheckBoxList>
                              
                            </div>



                        </td>
                        <td>

                            <div id="dvComp" runat="server" style="text-align:left;font-weight:bold;height:90px;overflow:auto;background:white;border:solid 1px lightGray;border-radius:10px;padding:5px;width:300px">

                              
                            </div>
                         <%--   <div id="dvCompanyIds" runat="server">


                            </div>--%>
                             <asp:HiddenField runat="server" ID="lblCompanyIds" ClientIDMode="Static"></asp:HiddenField>
                            <asp:Label runat="server" ID="Label1" ></asp:Label>
                        </td>

                        <td valign="top" style="font-weight:bold">
                            Title:

                        </td>
                        <td>
                             <div id="Div1" runat="server" style="text-align:left;font-weight:bold;height:90px;overflow:auto;background:white;border:solid 1px lightGray;border-radius:10px;padding:5px;width:300px">
                                   <asp:TextBox ID="txtTitle" TextMode="MultiLine" runat="server" Width="290" BorderStyle="None" Rows="5"></asp:TextBox>
                              
                            </div>

                        </td>



                    </tr>
                </table>
            </div>

            <br />
          
            <div class="dvRoundAndBgPopup" style="font-size: medium;">
                 <div class="section-head">
                    Email
                </div>
                <table width="100%" border="0" cellpadding="5" cellspacing="3">
                    <tr>
                        <td align="left">Email:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtEmail" runat="server" Width="450"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                ForeColor="red" ErrorMessage="*" ValidationGroup="up" />
                        </td>

                        <td align="left">Choose Day:
                        </td>
                        <td align="left">

                            <table>
                                <tr>
                                    <td>
                                         <asp:DropDownList ID="ddlDays" runat="server" AppendDataBoundItems="true" Width="150px">
                              
                                          </asp:DropDownList>

                                    </td>
                                    <td>
                                          <asp:DropDownList ID="ddlHour" runat="server" AppendDataBoundItems="true" Width="30">
                                              <asp:ListItem Value="0">0</asp:ListItem>
                                               <asp:ListItem Value="1">1</asp:ListItem>
                                               <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                               <asp:ListItem Value="4">4</asp:ListItem>
                                               <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                               <asp:ListItem Value="7">7</asp:ListItem>
                                               <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="9">9</asp:ListItem>
                                               <asp:ListItem Value="10">10</asp:ListItem>
                                               <asp:ListItem Value="11">11</asp:ListItem>
                                                <asp:ListItem Value="12">12</asp:ListItem>
                                               <asp:ListItem Value="13">13</asp:ListItem>
                                               <asp:ListItem Value="14">14</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                               <asp:ListItem Value="16">16</asp:ListItem>
                                               <asp:ListItem Value="17">17</asp:ListItem>
                                                <asp:ListItem Value="18">18</asp:ListItem>
                                               <asp:ListItem Value="19">19</asp:ListItem>
                                               <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="21">21</asp:ListItem>
                                               <asp:ListItem Value="22">22</asp:ListItem>
                                               <asp:ListItem Value="23">23</asp:ListItem>
                                               


                                        </asp:DropDownList>

                                    </td>

                                </tr>

                            </table>

                           

                           
                        </td>

                    </tr>
                    <tr>
                         <td align="left">Email CC:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtEmailCopy" runat="server" Width="450"></asp:TextBox>
                        </td>
                      <td align="left">Subject:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSubject" runat="server" Width="250"></asp:TextBox>
                        </td>
                      
                    </tr>

                    <tr>
                         <td align="left">Email BCC:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtEmailHide" runat="server" Width="450"></asp:TextBox>
                        </td>
                       <td >
                            Active: <asp:CheckBox ID="chActive" runat="server" />
                        </td>
                        <td  style="text-align:left">
                           
                         <span class="blueButton">
                    <button id="btnSendNow" type="button" runat="server" onserverclick="btnSend_Click" 
                        validationgroup="up" style="width: 200px">
                        <img src="../App_Themes/Theme1/Images/save1.jpg" width="16" height="16" style="display: none" />
                        Send Email Now!
                    </button>
                </span>

                             
                  </td>
                         

                    </tr>

                </table>
            </div>

            <div class="dvRoundAndBgPopup" style="font-size: medium; margin-top: 8px; padding-top: 20px;">
                <div class="section-head">
                    Email Body
                </div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left">
                         <%--   <asp:TextBox ID="txtBody" TextMode="MultiLine" runat="server" Rows="10" Columns="79"
                                Style="height: 150px; width: 100%;"></asp:TextBox>--%>
                     
                           <FCKeditorV2:FCKeditor ID="txtBody" runat="server"  BasePath="../fckeditor/" Height="150"></FCKeditorV2:FCKeditor>
                             </td>


                      

                    </tr>
                </table>
            </div>
       
             <div style="padding-top: 5px; margin-right: 5px">
                <span class="blueButton">
                    <button id="btnSave" type="button" runat="server" onserverclick="btnSave_Click" onclick="ClientScriptOnSave();"
                     style="width: 100px">
                        <img src="../App_Themes/Theme1/Images/save1.jpg" width="16" height="16" style="display: none" />
                        Save
                    </button>
                </span>
                <button id="btnCancel" type="button" onclick="closeDialog();" style="width: 100px">
                    <img src="../App_Themes/Theme1/Images/close.png" width="16" height="16" style="display: none" />
                    Close
                </button>

            </div>



        </div>
    </form>
</body>
</html>
