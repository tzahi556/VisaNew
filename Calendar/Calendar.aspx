<%@ Page Language="C#" Culture="he-IL" AutoEventWireup="true" CodeFile="Calendar.aspx.cs"
    Inherits="Calendar" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Media/calendar_green.css" rel="stylesheet" type="text/css" />
    <link href="Media/calendar_blue.css" rel="stylesheet" type="text/css" />
    <link href="Media/navigator_green.css" rel="stylesheet" type="text/css" />
    <link href="Media/menu_default.css" rel="stylesheet" type="text/css" />
    <link href="Media/layout.css" rel="stylesheet" type="text/css" />
    <link href="Media/bubble_default.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Theme1/Tracking.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/jquery-ui-1.8.23.custom/js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var copied = null;

        var calendarHeight;

        $(function () {

            $("div:contains('DEMO'):last").remove();


            calendarHeight = getListHeight();
            $(".calendarInner").css("height", calendarHeight);
            $(".calendar_green_main > div:eq(1)").css("height", calendarHeight - 50);

        });



        function afterRender(data) {
            if (data && data.navigatorRefresh) {
                dpn.visibleRangeChangedCallBack();
                $(".calendar_green_main > div:eq(1)").css("height", calendarHeight - 50);
                //  $(".calendar_blue_wrap_inner").css("height", getListHeight());
            }
        }




        function getListHeight() {
            var topHeight = $(topBar).height() + 20;
            var listHeight = (eval(parent.MainHeight) - topHeight - 40);
            return listHeight;
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">

       <div id="topBar" class="dvNoRoundAndBg">
     &nbsp;
    </div>



    <div style="padding: 10px;">
        <div class="calendar_blue_wrap" >
            <div class="dvRoundAndBgPopupBlue calendarInner">
                <table style="width: 100%;">
                    <tr>
                        <td valign="top" style="width: 150px;">
                            <DayPilot:DayPilotNavigator ID="DayPilotNavigator1" runat="server" ClientObjectName="dpn"
                                BoundDayPilotClientObjectName="dpc1" SelectMode="Week" ShowMonths="3" SkipMonths="3"
                                CssClassPrefix="navigator_green" CssOnly="true" DataStartField="start" DataEndField="end"
                                VisibleRangeChangedHandling="CallBack" OnVisibleRangeChanged="DayPilotNavigator1_VisibleRangeChanged"
                                ShowWeekNumbers="true" OnTimeRangeSelected="DayPilotNavigator1_TimeRangeSelected"
                                OnBeforeCellRender="DayPilotNavigator1_BeforeCellRender" RowsPerMonth="Auto">
                            </DayPilot:DayPilotNavigator>
                            <br />
                            <br />
                            <div style="height: 20px">
                            </div>
                        </td>
                        <td valign="top" style="width: 14px;">
                        </td>
                        <td valign="top"  style="">
                            <DayPilot:DayPilotCalendar ID="DayPilotCalendar1" runat="server" DataStartField="start"
                                DataEndField="end" DataTextField="name" DataValueField="id" DataServerTagFields="color"
                                DataAllDayField="allday" OnEventMove="DayPilotCalendar1_EventMove" ViewType="Week"
                                OnTimeRangeSelected="DayPilotCalendar1_TimeRangeSelected" TimeRangeSelectedHandling="JavaScript"
                                TimeRangeSelectedJavaScript="" AfterRenderJavaScript="afterRender(data)" EventMoveHandling="CallBack"
                                ContextMenuID="DayPilotMenu1" OnEventMenuClick="DayPilotCalendar1_EventMenuClick"
                                EventResizeHandling="CallBack" OnEventResize="DayPilotCalendar1_EventResize"
                                EventClickHandling="JavaScript" EventClickJavaScript="" EventSelectHandling="JavaScript"
                                xHeightSpec="Parent100Pct" OnEventClick="DayPilotCalendar1_EventClick" ClientObjectName="dpc1"
                                EventEditHandling="CallBack" OnEventEdit="DayPilotCalendar1_EventEdit" OnBeforeEventRender="DayPilotCalendar1_BeforeEventRender"
                                EventDeleteHandling="JavaScript" OnEventDelete="DayPilotCalendar1_EventDelete"
                                EventDeleteJavaScript="if (confirm('Do you really want to delete ' + e.text() + ' ?')) dpc1.eventDeleteCallBack(e);"
                                OnEventSelect="DayPilotCalendar1_EventSelect" EventSelectColor="red" ShowAllDayEvents="True"
                                UseEventBoxes="Always" OnBeforeHeaderRender="DayPilotCalendar1_BeforeHeaderRender"
                                ShowToolTip="false" EventDoubleClickHandling="Edit" EventDoubleClickJavaScript="alert(e.value());"
                                EventHoverHandling="Bubble" TimeRangeDoubleClickHandling="CallBack" TimeFormat="Clock24Hours"
                                OnTimeRangeDoubleClick="DayPilotCalendar1_TimeRangeDoubleClick" ContextMenuSelectionID="DayPilotMenuSelection"
                                OnTimeRangeMenuClick="DayPilotCalendar1_TimeRangeMenuClick" OnCommand="DayPilotCalendar1_Command"
                                OnBeforeCellRender="DayPilotCalendar1_BeforeCellRender" HourNameBackColor=""
                                BorderColor="#A0A0A0" CellBorderColor="#A0A0A0" EventBorderColor="#505050" AllDayEventBorderColor="#a0a0a0"
                                EventArrangement="Full" BubbleID="DayPilotBubble1" ColumnBubbleID="DayPilotBubble1"
                                OnBeforeTimeHeaderRender="DayPilotCalendar1_BeforeTimeHeaderRender" CssOnly="true"
                                CssClassPrefix="calendar_green" AllDayEventHeight="25" CellHeight="25" HourWidth="60" HeightSpec="Fixed"
                                
                                MoveBy="Top" DurationBarVisible="true" Direction="RTL">
                            </DayPilot:DayPilotCalendar>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <DayPilot:DayPilotMenu ID="DayPilotMenu1" runat="server" CssClassPrefix="menu_default"
        ShowMenuTitle="true">
        <%--  <DayPilot:MenuItem Text="Open" Action="JavaScript" JavaScript="edit(e);">
        </DayPilot:MenuItem>--%>
        <DayPilot:MenuItem Text="Send" Action="JavaScript" JavaScript="alert('Sending event (id ' + e.value() +')');">
        </DayPilot:MenuItem>
        <DayPilot:MenuItem Text="-">
        </DayPilot:MenuItem>
        <DayPilot:MenuItem Text="Copy" Action="JavaScript" JavaScript="copied = e.value();">
        </DayPilot:MenuItem>
        <DayPilot:MenuItem Text="-" Action="NavigateUrl">
        </DayPilot:MenuItem>
        <DayPilot:MenuItem Text="Delete" Action="Callback" Command="Delete" Image='Media/menu_delete.png' />
        <%--    <DayPilot:MenuItem Action="NavigateUrl" NavigateUrl="javascript:alert('Going somewhere else (id {0})');"
            Text="NavigateUrl test" />
        <DayPilot:MenuItem Action="NavigateUrl" NavigateUrl="http://www.google.com/" NavigateUrlTarget="_blank"
            Text="Google in New Window" />--%>
    </DayPilot:DayPilotMenu>
    <DayPilot:DayPilotMenu ID="DayPilotMenuSelection" runat="server" CssClassPrefix="menu_default"
        ShowMenuTitle="true">
        <DayPilot:MenuItem Action="JavaScript" JavaScript="dpc1.timeRangeSelectedCallBack(e.start, e.end, e.resource); dpc1.clearSelection();"
            Text="Create New Event" />
        <%-- <DayPilot:MenuItem Action="PostBack" Command="Insert" Text="Create new event (PostBack)" />
        <DayPilot:MenuItem Action="CallBack" Command="Insert" Text="Create new event (CallBack)" />--%>
        <DayPilot:MenuItem Text="-">
        </DayPilot:MenuItem>
        <DayPilot:MenuItem Action="JavaScript" JavaScript="if (!copied) { alert('You need to copy an event first.'); return; } dpc1.commandCallBack('paste', {id:copied, start: e.start});"
            Text="Paste" />
        <DayPilot:MenuItem Text="-">
        </DayPilot:MenuItem>
        <DayPilot:MenuItem Action="JavaScript" JavaScript="alert('Start: ' + e.start.toString() + '\nEnd: ' + e.end.toString() + '\nResource id: ' + e.resource);"
            Text="Show selection details" />
        <DayPilot:MenuItem Action="JavaScript" JavaScript="dpc1.clearSelection();" Text="Clean selection" />
    </DayPilot:DayPilotMenu>
    <DayPilot:DayPilotBubble ID="DayPilotBubble1" runat="server" OnRenderContent="DayPilotBubble1_RenderContent"
        ClientObjectName="bubble" Position="EventTop" CssOnly="true" CssClassPrefix="bubble_default">
    </DayPilot:DayPilotBubble>
    </form>
</body>
</html>
