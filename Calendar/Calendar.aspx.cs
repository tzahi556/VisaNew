using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DayPilot.Web.Ui.Conflict;
using DayPilot.Web.Ui.Enums;
using DayPilot.Web.Ui.Events;
using DayPilot.Web.Ui.Events.Bubble;
using DayPilot.Web.Ui.Events.Calendar;
using System.Globalization;
using System.Collections;

public partial class Calendar : System.Web.UI.Page
{
    private DataTable table;

    protected void Page_Load(object sender, EventArgs e)
    {

        FillCalendar();

        if (!IsPostBack)
        {
            DataBind();
            DayPilotCalendar1.UpdateWithMessage("Welcome!");

        }
       
        Hashtable data = new Hashtable();
        data["navigatorRefresh"] = true;
        DayPilotCalendar1.Update(data);

       
    }

    private void FillCalendar()
    {
        table = Dal.ExeSp("GetCalendar"); //(DataTable)Session["AllFeatures"];
        DayPilotCalendar1.DataSource = table;
        DayPilotNavigator1.DataSource = table;
    }

    public static DataTable GetData()
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));
        dt.Columns.Add("column", typeof(string));
        dt.Columns.Add("allday", typeof(bool));
        dt.Columns.Add("color", typeof(string));

        DataRow dr;

        dr = dt.NewRow();
        dr["id"] = 1;
        dr["start"] = Convert.ToDateTime("15:00");
        dr["end"] = Convert.ToDateTime("16:00");
        dr["name"] = "Event 1";
        dr["column"] = "A";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 2;
        dr["start"] = Convert.ToDateTime("11:00").AddDays(1);
        dr["end"] = Convert.ToDateTime("15:00").AddDays(1);
        dr["name"] = "Event 2";
        dr["column"] = "A";
        dr["color"] = "green";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 3;
        dr["start"] = Convert.ToDateTime("14:15").AddDays(1);
        dr["end"] = Convert.ToDateTime("18:45").AddDays(1);
        dr["name"] = "Event 3";
        dr["column"] = "A";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 4;
        dr["start"] = Convert.ToDateTime("12:30").AddDays(2);
        dr["end"] = Convert.ToDateTime("14:30").AddDays(2);
        dr["name"] = "Sales Dept. Meeting";
        dr["column"] = "B";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 5;
        dr["start"] = Convert.ToDateTime("8:00");
        dr["end"] = Convert.ToDateTime("9:00");
        dr["name"] = "Event 4";
        dr["column"] = "B";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 6;
        dr["start"] = Convert.ToDateTime("14:00");
        dr["end"] = Convert.ToDateTime("20:00");
        dr["name"] = "Event 6";
        dr["column"] = "C";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 7;
        dr["start"] = Convert.ToDateTime("11:00");
        dr["end"] = Convert.ToDateTime("13:14");
        dr["name"] = "Unicode test: 公曆 (requires Unicode fonts on the client side)";
        dr["color"] = "red";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 8;
        dr["start"] = Convert.ToDateTime("13:00").AddDays(-1);
        dr["end"] = Convert.ToDateTime("14:15").AddDays(-1);
        dr["name"] = "Event 8";
        dr["column"] = "C";
        dr["color"] = "green";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 9;
        dr["start"] = Convert.ToDateTime("13:00").AddDays(7);
        dr["end"] = Convert.ToDateTime("14:00").AddDays(7);
        dr["name"] = "Event 9";
        dr["column"] = "C";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 10;
        dr["start"] = Convert.ToDateTime("13:00").AddDays(-7);
        dr["end"] = Convert.ToDateTime("14:00").AddDays(-7);
        dr["name"] = "Event 10";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 11;
        dr["start"] = Convert.ToDateTime("00:00").AddDays(-2);
        dr["end"] = Convert.ToDateTime("00:00").AddDays(2);
        dr["name"] = "Event 11";
        dr["column"] = "D";
        dr["allday"] = true;
        // dt.Rows.Add(dr);

        dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };

        return dt;

    }


    // הזזה
    protected void DayPilotCalendar1_EventMove(object sender, EventMoveEventArgs e)
    {


        Dal.ExeSp("SetCalendar", "2", e.NewStart, e.NewEnd, e.Text, e.Value);
        FillCalendar();
        
        DayPilotCalendar1.DataBind();
        DayPilotCalendar1.UpdateWithMessage("Event moved.");

       // DayPilotCalendar1.Update();


    }
    
    // מחיקה
    protected void DayPilotCalendar1_EventMenuClick(object sender, EventMenuClickEventArgs e)
    {
        if (e.Command == "Delete")
        {
            //#region Simulation of database update
            //DataRow dr = table.Rows.Find(e.Value);

            //if (dr != null)
            //{
            //    table.Rows.Remove(dr);
            //    table.AcceptChanges();
            //}
            //#endregion


            Dal.ExeSp("SetCalendar", "3", e.Start, e.End, e.Text, e.Value);
            FillCalendar();


            DayPilotCalendar1.DataBind();
            DayPilotCalendar1.Update();
        }
    }
    
    // הכנסת אירוע חדש
    protected void DayPilotCalendar1_TimeRangeSelected(object sender, TimeRangeSelectedEventArgs e)
    {
        InsertDataCalendar(e.Start,e.End);
    }

    // בעת דאבל קליק
    protected void DayPilotCalendar1_TimeRangeDoubleClick(object sender, TimeRangeDoubleClickEventArgs e)
    {

        InsertDataCalendar(e.Start, e.End);

    }

 

    private void InsertDataCalendar(DateTime start, DateTime end)
    {

        Dal.ExeSp("SetCalendar","1",start,end,"New Event","0");
        FillCalendar();
        DayPilotCalendar1.DataBind();
        DayPilotCalendar1.UpdateWithMessage("New event created.");
    }



    protected void DayPilotCalendar1_EventResize(object sender, EventResizeEventArgs e)
    {
        Dal.ExeSp("SetCalendar", "2", e.NewStart, e.NewEnd, e.Text, e.Value);
        FillCalendar();

        DayPilotCalendar1.DataBind();
        DayPilotCalendar1.UpdateWithMessage("Event resized");
    }

    protected void DayPilotCalendar1_EventClick(object sender, EventClickEventArgs e)
    {
        #region Simulation of database update

        DataRow dr = table.Rows.Find(e.Value);
        if (dr != null)
        {
            dr["name"] = "Event clicked at " + DateTime.Now;
            table.AcceptChanges();
        }

        #endregion

        DayPilotCalendar1.DataBind();
        DayPilotCalendar1.Update();
    }
   
    // עריכה
    protected void DayPilotCalendar1_EventEdit(object sender, EventEditEventArgs e)
    {

        Dal.ExeSp("SetCalendar", "2", e.Start, e.End,e.NewText,e.Value);
        FillCalendar();
        


        DayPilotCalendar1.DataBind();
        DayPilotCalendar1.UpdateWithMessage("Event text changed.");
    }
    protected void DayPilotCalendar1_BeforeEventRender(object sender, BeforeEventRenderEventArgs e)
    {
    }


    protected void DayPilotCalendar1_EventDelete(object sender, EventDeleteEventArgs e)
    {
        #region Simulation of database update

        DataRow dr = table.Rows.Find(e.Value);
        if (dr != null)
        {
            table.Rows.Remove(dr);
            table.AcceptChanges();
        }

        #endregion

        DayPilotCalendar1.DataBind();
        DayPilotCalendar1.UpdateWithMessage("Event deleted.");
    }

    protected void DayPilotCalendar1_EventSelect(object sender, DayPilotEventArgs e)
    {
        DayPilotCalendar1.Update();
    }

    protected void DayPilotCalendar1_BeforeHeaderRender(object sender, BeforeHeaderRenderEventArgs e)
    {
    }

    protected void DayPilotBubble1_RenderContent(object sender, RenderEventArgs e)
    {
        if (e is RenderEventBubbleEventArgs)
        {
            RenderEventBubbleEventArgs re = e as RenderEventBubbleEventArgs;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<b>{0}</b><br />", re.Text);
            sb.AppendFormat("Start: {0}<br />", re.Start);
            sb.AppendFormat("End: {0}<br />", re.End);

            //re.InnerHTML = "<b>Event details</b><br />Here is the right place to show details about the event with ID: " + re.Value + ". This text is loaded dynamically from the server.<br/>";
            re.InnerHTML = sb.ToString();
        }
        else if (e is RenderTimeBubbleEventArgs)
        {
            RenderTimeBubbleEventArgs re = e as RenderTimeBubbleEventArgs;
            e.InnerHTML = "<b>Time header details</b><br />From:" + re.Start + "<br />To: " + re.End;
        }
        else if (e is RenderCellBubbleEventArgs)
        {
            RenderCellBubbleEventArgs re = e as RenderCellBubbleEventArgs;
            e.InnerHTML = "<b>Cell details</b><br />Column:" + re.ResourceId + "<br />From:" + re.Start + "<br />To: " + re.End;
        }
    }


  
  

    protected void DayPilotCalendar1_TimeRangeMenuClick(object sender, TimeRangeMenuClickEventArgs e)
    {
        //if (e.Command == "Insert")
        //{
        //    #region Simulation of database update

        //    DataRow dr = table.NewRow();
        //    dr["start"] = e.Start;
        //    dr["end"] = e.End;
        //    dr["id"] = Guid.NewGuid().ToString();
        //    dr["name"] = "New event";

        //    table.Rows.Add(dr);
        //    table.AcceptChanges();
        //    #endregion

        //    DayPilotCalendar1.DataBind();
        //    DayPilotCalendar1.Update();
        //}

    }
    protected void DayPilotCalendar1_Command(object sender, CommandEventArgs e)
    {
        switch (e.Command)
        {
            case "navigate":
                DateTime start = (DateTime)e.Data["start"];
                DateTime end = (DateTime)e.Data["end"];
                DateTime day = (DateTime)e.Data["day"]; // clicked day

                DayPilotCalendar1.StartDate = start;
                DayPilotCalendar1.DataBind();
                DayPilotCalendar1.UpdateWithMessage("Date changed. You clicked: " + day);
                break;
            case "refresh":
                DayPilotCalendar1.DataBind();
                DayPilotCalendar1.UpdateWithMessage("Refreshed.");
                break;
            case "paste":
                DateTime pasteHere = (DateTime)e.Data["start"];
                string id = (string)e.Data["id"];

                DataRow dr = table.Select("id=" + id)[0];
                if (dr != null)
                {
                    TimeSpan duration = ((DateTime)dr["end"]) - ((DateTime)dr["start"]);

                    Dal.ExeSp("SetCalendar", "1", pasteHere, pasteHere + duration, "Copy of " + dr["name"].ToString(), "0");
                    FillCalendar();


                    //DataRow drNew = table.NewRow();
                    //drNew["start"] = pasteHere;
                    //drNew["end"] = pasteHere + duration;
                    //drNew["id"] = Guid.NewGuid().ToString();
                    //drNew["name"] = "Copy of " + dr["name"];

                    //table.Rows.Add(drNew);
                    //table.AcceptChanges();
                }
                DayPilotCalendar1.DataBind();
                DayPilotCalendar1.UpdateWithMessage("Event copied.");
                break;
            case "test":
                DayPilotCalendar1.CellDuration = 60;
                DayPilotCalendar1.DataBind();
                DayPilotCalendar1.UpdateWithMessage("Updated");
                break;

        }
    }

    protected void DayPilotNavigator1_VisibleRangeChanged(object sender, EventArgs e)
    {
        DayPilotNavigator1.DataBind();
    }

    protected void DayPilotCalendar1_BeforeCellRender(object sender, BeforeCellRenderEventArgs e)
    {
    }

    protected void DayPilotCalendar1_HeaderClick(object sender, HeaderClickEventArgs e)
    {
    }
    protected void DayPilotNavigator1_TimeRangeSelected(object sender, TimeRangeSelectedEventArgs e)
    {
        //throw new Exception(e.Start.ToString());
    }
    protected void DayPilotCalendar1_BeforeTimeHeaderRender(DayPilot.Web.Ui.Events.Calendar.BeforeTimeHeaderRenderEventArgs ea)
    {
        /*
        int cells = 60/DayPilotCalendar1.CellDuration;
        StringBuilder sb = new StringBuilder();

        sb.Append("<table style='width:100%' cellpadding='0' cellspacing='0'>");

        for(int i = 0; i < cells; i++ )
        {
            sb.Append("<tr><td>");
            sb.Append("<div style='height:");
            sb.Append(DayPilotCalendar1.CellHeight - 1);
            sb.Append("px;border-bottom:1px solid ");
            sb.Append(ColorTranslator.ToHtml(DayPilotCalendar1.HourNameBorderColor));
            sb.Append(";'>");
            sb.Append(ea.Hour);
            sb.Append(":");
            sb.Append((i * DayPilotCalendar1.CellDuration).ToString("00"));
            sb.Append("</div>");
            sb.Append("</td></tr>");
        }

        sb.Append("</table>");

        ea.InnerHTML = sb.ToString();
         * */
    }

    protected void DayPilotNavigator1_BeforeCellRender(object sender, DayPilot.Web.Ui.Events.Navigator.BeforeCellRenderEventArgs e)
    {
    }
}
