﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KeepSessionAlive : System.Web.UI.Page
{
    protected string WindowStatusText = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //string RefreshValue = Convert.ToString((Session.Timeout * 60) - 60);

        string RefreshValue = Convert.ToString((Session.Timeout * 60) - 90);

        // Refresh this page 60 seconds before session timeout, effectively resetting the session timeout counter.
        MetaRefresh.Attributes["content"] = RefreshValue + ";url=KeepSessionAlive.aspx?q=" + DateTime.Now.Ticks;

        WindowStatusText = " Last refresh " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
    }
}