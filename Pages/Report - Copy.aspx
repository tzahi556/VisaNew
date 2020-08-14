<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="Report - Copy.aspx.cs"
    Inherits="Pages_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title></title>
    <link href="../App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-ui-1.8.23.custom.min.js"
        type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.inputhints.min.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dropkick-1.0.0.js" type="text/javascript"></script>
    <script src="../App_Themes/jquery-ui-1.10.3.custom/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/dropkick.css" rel="stylesheet"
        type="text/css" />
    <link href="../App_Themes/jquery-ui-1.10.3.custom/css/datatable.css" rel="stylesheet"
        type="text/css" />
    <link href="../App_Themes/jquery-ui-1.8.23.custom/css/smoothness/jquery-ui-1.8.23.custom.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            $('body').click(function (evt) {
                var elementId = evt.target.id;



                if (elementId == "imgCompany" || elementId.substring(0, 13) == "chCompanyList")
                    return;

                $("#dvChlist").css("display", "none");
                //Do processing of click event here for every element except with id menu_content

            });



            //$("body").css("display", "");

        });


        function pageLoad() {
            $("#btnDelete").button();
            $("#btnSave").button();

            $("#btnClear").button();

            $("#ddlPrevReport").dropkick({
                change: function (value, label) {

                    // setTimeout(function () {

                    __doPostBack('ddlPrevReport', '');

                    // }, 0);
                }
            });



            $("#dvMain").css("height", getListHeight());
            $("#dvDrop ol").css("height", getListHeight() - 100);
            $("#dvFields").css("height", getListHeight() - 50);

            $(".dvDrag").disableSelection();

            $(".tbRow").droppable({
                accept: ".remove",
                drop: function (event, ui) {
                    $(ui.draggable).remove();
                }
            });


            $(".dvDrag").draggable({
                helper: "clone"
            });


            $("#dvDrop ol").droppable({
                accept: ":not(.ui-sortable-helper)",
                drop: function (event, ui) {
                    $(this).find(".placeholder").remove();
                    var text = ui.draggable.text();
                    $(getNewElement(text)).appendTo(this);
                }
            }).sortable({
                items: "li:not(.placeholder)",
                sort: function () {
                    $(this).removeClass("ui-state-default");

                }
            });




            $("body").css("display", "");



        }




        function getNewElement(text) {


            var name = $.trim(text);  //.replace(/\s+/g, '');
             

            return "<li class='dvDrags ui-corner-all remove'><input onchange='SetHtmlSign(this)' checked name='ch_" + name + "_" + Math.random() + "'  type='checkbox' />&nbsp;&nbsp;" + text
                 + " <div id='dv" + name + "' style='float:right;font-size:small;'>"
                 + "<input type='radio' id='1" + name + "' onchange='SetHtmlSignRadio(this)' name='radio_" + name + "' value='All' checked='checked'>All &nbsp;&nbsp;"
                 + "<input type='radio' id='2" + name + "' onchange='SetHtmlSignRadio(this)' name='radio_" + name + "'  value='Done'>Done &nbsp;&nbsp;"
                 + "<input type='radio' id='3" + name + "' onchange='SetHtmlSignRadio(this)' name='radio_" + name + "' value='Not Done'>Not Done"
                 + "</div>"
                 + "</li>";


           // return "<li class='dvDrags ui-corner-all remove'><input onchange='SetHtmlSign(this)' checked name='ch_" + name + "_" + Math.random() + "'  type='checkbox' />&nbsp;&nbsp;" + text
           //+ " <div id='dv" + name + "' style='float:right;font-size:small;'>"
           //+ "<input type='radio' id='1" + name + "' onchange='SetHtmlSignRadio(this)' name='radio_" + name + "' value='All' checked='checked'>All &nbsp;&nbsp;"
           //+ "<input type='radio' id='2" + name + "' onchange='SetHtmlSignRadio(this)' name='radio_" + name + "'  value='Done'>Done &nbsp;&nbsp;"
           //+ "<input type='radio' id='3" + name + "' onchange='SetHtmlSignRadio(this)' name='radio_" + name + "' value='Not Done'>Not Done"
           //+ "</div>"
           //+ "</li>";






        }

        function SetHtmlSign(Obj) {
            if (Obj.checked)
                $(Obj).attr("checked", "checked");
            else
                $(Obj).removeAttr('checked');

        }


        function SetHtmlSignRadio(Obj) {

            //  alert(Obj.name);
            //$("#Checkbox1").removeAttr('checked');
            $("[name='" + Obj.name + "'][id!='" + Obj.id + "']").removeAttr('checked');

            $(Obj).attr("checked", "checked");

        }



        function getListHeight() {
            var topHeight = $(topBar).height() + 20;
            var listHeight = (eval(parent.MainHeight) - topHeight - 50);
            return listHeight;
        }




        function ClinetGenarteBtnClick() {

            if ($("[id*=chCompanyList] input:checked").length > 0) {
                var dropHTML = $("#dvDrop").html();
                dropHTML = dropHTML.replace(/onchange/g, "");
                $('#<%=hdn.ClientID %>').val(dropHTML);
                return true;
            } else {
                parent.ShowMessage('Must Select Company', '4');
                return false;
            }


        }



        function ClinetBtnClick() {

            var dropHTML = $("#dvDrop").html();
            dropHTML = dropHTML.replace(/onchange/g, "");
           // אם יהיו בעיות נחזיר את זה 
            $('#<%=hdn.ClientID %>').val(dropHTML);
           
            ////replace all single quotes
            //var myStr = myStr.replace(/'/g, '');

            ////replace all double quotes
            //var myStr = myStr.replace(/"/g, '');

            ////or abit of fun, replace single quotes with double quotes
            //var myStr = myStr.replace(/'/g, '"');

            ////or vice versa, replace double quotes with single quotes
            //var myStr = myStr.replace(/"/g, ''');


        }


        function ShowHide() {
            $("#dvChlist").css("display", "");
        }


        function Reset() {

            $(".ch input").removeAttr("checked");
            $("[id*=chCompanyList] input").removeAttr("checked");
            $('#<%=txtNewName.ClientID %>').val("");
            $("#dvDrop ol").html("");
        }

    </script>
    <style>
        .dvDrags
        {
            background: #95B3D7;
            cursor: move;
        }
        
        .dvDrag
        {
            background: #95B3D7;
            cursor: move;
        }
        
        .dvDrop
        {
            margin-top: 5px;
            height: 100%;
            width: 90%;
        }
        
        
        .tbRow
        {
            padding-top: 5px;
        }
        
        .tbRow td
        {
            background: #95B3D7;
            width: 50%;
            height: 50px;
            padding: 5px;
            text-align: center;
        }
        
        .ui-draggable-dragging
        {
            z-index: 9999;
        }
        
        
        ol li
        {
            list-style-position: inside;
            margin-bottom: 6px;
            cursor: move;
            width: 99%;
            padding: 3px;
        }
        
        .dv160
        {
            border: solid 2px #385D8A;
            padding: 5px;
            font-weight: bold;
            background: #5CCD8F;
        }
        
        .dv160:hover
        {
            border: solid 3px #385D8A;
        }
        
        
        .dv75160
        {
            border: solid 2px #385D8A;
            padding: 5px;
            font-weight: bold;
            background: #FAC090;
        }
        .dv75160:hover
        {
            border: solid 3px #385D8A;
        }
        
        .dv74
        {
            border: solid 2px #385D8A;
            padding: 5px;
            font-weight: bold;
            background: #FF4040;
        }
        
        .dv74:hover
        {
            border: solid 3px #385D8A;
        }
        
        
        .dvBelow
        {
            border: solid 2px #385D8A;
            padding: 5px;
            font-weight: bold;
            background: black;
            color: White;
        }
        
        .dvBelow:hover
        {
            border: solid 3px #385D8A;
        }
        
        
        .dvGenerate
        {
            border: solid 2px #385D8A;
            padding: 13px;
            font-weight: bold;
            color: White;
            background: url(../App_Themes/Theme1/Images/bg-main.png);
            width: 100%;
            font-size: large;
        }
        
        .dvGenerate:hover
        {
            border: solid 3px #385D8A;
            cursor: pointer;
        }
        
        
        #dvChlist label
        {
            margin-left: 10px;
        }
        
        .FormatRadioButtonList label
        {
            margin-right: 20px;
        }
    </style>
</head>
<body style="display: none">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="topBar" class="dvNoRoundAndBg">
        <%--  <div style="float: left">
            Company:
        </div>
        <div style="float: left">
            <img id="imgCompany" src="../App_Themes/Theme1/Images/Selectcompany.png" onclick="ShowHide();" />
        </div>
        <div style="float: left">
            <div align="left" id="dvChlist" style="display: none; position: absolute; background: white;
                border: solid 2px #385D8A; left: 110px; text-align: left; height: 300px; overflow: auto">
                <asp:CheckBoxList ID="chCompanyList" CellPadding="8" CellSpacing="8" AppendDataBoundItems="true"
                    runat="server">
                    <asp:ListItem Text="-- All Companies --" Value="0"></asp:ListItem>
                </asp:CheckBoxList>
            </div>
        </div>
        <div style="float: left">
            Save Current Report:
        </div>
        <div style="float: left">
            <asp:TextBox ID="txtNewName" runat="server" Style="font-size: 14px; width: 200px;
                padding: 5px;"></asp:TextBox>
        </div>
        <div style="float: left" class="blueButton">
            <button id="btnSave" type="button" runat="server" onclick="ClinetBtnClick();" onserverclick="btnSave_Click"
                style="font-size: 14px; height: 30px; width: 100%">
                Save
            </button>
        </div>
        <div style="float: left">
            Select Report:
        </div>
        <div style="float: left">
            <div style="text-align: left">
                <asp:DropDownList ID="ddlPrevReport" runat="server" Width="170px" OnSelectedIndexChanged="ddlPrevReport_SelectedChange"
                    AppendDataBoundItems="true" AutoPostBack="true">
                    <asp:ListItem Value="0">-- Choose Saved Report --</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div style="float: left" class="redButton">
            <button id="btnDelete" type="button" runat="server" onserverclick="btnDelete_Click"
                style="font-size: 14px; height: 30px; width: 100%">
                Delete
            </button>
        </div>--%>
        <table width="100%" border="0">
            <tr>
                <td>
                    Company:
                </td>
                <td id="tdCompany" align="left">
                    <img id="imgCompany" src="../App_Themes/Theme1/Images/Selectcompany.png" onclick="ShowHide();" /><br />
                    <div align="left" id="dvChlist" style="display: none; position: absolute; background: white;
                        border: solid 2px #385D8A; left: 95px; text-align: left; height: 300px; overflow: auto;
                        z-index: 9999">
                        <asp:CheckBoxList ID="chCompanyList" CellPadding="8" CellSpacing="8" AppendDataBoundItems="true"
                            runat="server">
                            <asp:ListItem Text="-- All Companies --" Value="0"></asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </td>
                <td align="right" style="padding-right: 0px">
                    Save Current Report:
                    <asp:TextBox ID="txtNewName" runat="server" Style="font-size: 14px; width: 200px;
                        padding: 5px;"></asp:TextBox>
                </td>
                <td class="blueButton" align="right">
                    <button id="btnSave" type="button" runat="server" onclick="ClinetBtnClick();" onserverclick="btnSave_Click"
                        style="font-size: 14px; height: 30px; width: 100%">
                        Save
                    </button>
                </td>
                <td align="right" width="30%">
                    <div style="float: left; margin-top: 5px">
                        &nbsp; &nbsp; &nbsp;&nbsp; Select Report:&nbsp;
                    </div>
                    <div style="text-align: left">
                        <asp:DropDownList ID="ddlPrevReport" runat="server" Width="170px" OnSelectedIndexChanged="ddlPrevReport_SelectedChange"
                            AppendDataBoundItems="true" AutoPostBack="true">
                            <asp:ListItem Value="0">-- Choose Saved Report --</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td class="redButton" align="right">
                    <button id="btnDelete" type="button" runat="server" onserverclick="btnDelete_Click"
                        style="font-size: 14px; height: 30px; width: 100%">
                        Delete
                    </button>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding: 5px;">
        <div id="dvMain" style="background-color: White; border: solid 3px #385D8A;" class="">
            <table width="100%" border="0" style="padding-top: 15px">
                <tr>
                    <td valign="top" align="center" style="width: 22%">
                        <div class="dvReportTitle">
                            Expert Fields
                        </div>
                        <div id="dvFields" style="overflow:auto">
                        <table border="0" width="100%" cellspacing="5" class="tbRow">
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Expert Name</div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        Expert Surname
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Expert Passport</div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        Approval Submission</div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Approval From</div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        Approval Expired
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Invitation Date
                                    </div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        Visa Expierd</div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Multiple Visa Issue</div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        Multiple Visa Exp
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Comments
                                    </div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        Active
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Days To Expire</div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        Title</div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Address</div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                       Nationality</div>
                                </td>
                            </tr>

                              <tr>
                                <td>
                                    <div class="dvDrag">
                                        Monthly</div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                       Family</div>
                                </td>
                            </tr>



                        </table>
                        </div>


                    </td>
                    <td valign="top" align="center" style="width: 22%">
                        <div class="dvReportTitle">
                            Expert Doc
                        </div>
                        <table border="0" width="100%" cellspacing="5" class="tbRow">
                            <tr>
                                <td>
                                    <div class="dvDrag ">
                                        AL 33</div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        AL 15
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        ID</div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        Passport Copy</div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        CV
                                    </div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        Diploma
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Obligo 6
                                    </div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        Obligo 2</div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Medical
                                    </div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        Affivavit</div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Marriage Certificate</div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        POA</div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Photo</div>
                                </td>
                                <td>
                                    <div class="">
                                        &nbsp;</div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" align="center" style="width: 22%">
                        <div class="dvReportTitle">
                            Company Fields
                        </div>
                        <table border="0" width="100%" cellspacing="5" class="tbRow">
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Company Name</div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        Company Phone
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="dvDrag">
                                        Company Reg</div>
                                </td>
                                <td>
                                    <div class="dvDrag">
                                        Interior Reg</div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="bottom" colspan="2" style="background: transparent">
                                    <div class="dv160 ui-corner-all ch">
                                        120 +
                                        <div style="float: right">
                                            <asp:CheckBox ID="ch160" runat="server" /></div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="bottom" colspan="2" style="background: transparent">
                                    <div class="dv75160 ui-corner-all ch">
                                       90 - 120
                                        <div style="float: right">
                                            <asp:CheckBox ID="ch75160" runat="server" /></div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="bottom" colspan="2" style="background: transparent">
                                    <div class="dv74 ui-corner-all ch">
                                        0 - 90
                                        <div style="float: right">
                                            <asp:CheckBox ID="ch74" runat="server" /></div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="bottom" colspan="2" style="background: transparent;">
                                    <div class="dvBelow ui-corner-all ch">
                                        0 - Below
                                        <div style="float: right">
                                            <asp:CheckBox ID="ch0" runat="server" /></div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="bottom" colspan="2" style="background: transparent">
                                    <asp:Button OnClientClick="return ClinetGenarteBtnClick();" CssClass="dvGenerate ui-corner-all"
                                        ID="btnSubmit" runat="server" OnClick="btnExcel_Click" Text="Generate Report">
                                    </asp:Button>
                                    <%-- <button id="btnGenerate" type="button" runat="server" onclick="ClinetGenarteBtnClick();"
                                        onserverclick="btnExcel_Click" class="dvGenerate ui-corner-all">
                                        Generate Report
                                    </button>
                                    <div class="dvGenerate ui-corner-all"  onclick="ClinetBtnClick();" onserverclick="btnExcel_Click" runat="server">
                                        Generate Report
                                    </div>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" align="center">
                        <div class="dvReportTitle">
                            <div style="float: right" class="blueButton">
                                <button id="btnClear" type="button" runat="server" onclick="Reset();" style="font-size: 11px;
                                    height: 20px;">
                                    Reset
                                </button>
                            </div>
                            <div id='dvActive'>
                                <asp:RadioButtonList ID="rdActive" CellPadding="0" CellSpacing="0" CssClass="FormatRadioButtonList"
                                    RepeatDirection="Horizontal" runat="server">
                                    <asp:ListItem Text="All" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="2" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Not Active" Value="3"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div id="dvDrop" runat="server" class="dvRoundAndBgPopup dvDrop">
                            <ol>
                                <li class="ui-corner-all placeholder">Add your items here</li>
                            </ol>
                        </div>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdn" runat="server" Value="0" />
        </div>
    </div>
    </form>
</body>
</html>
