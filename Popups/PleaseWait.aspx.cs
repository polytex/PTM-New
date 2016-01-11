using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using UniStr;
using PolytexData;
public partial class Popups_PleaseWait : PolyPage
{
    public string jsFunctionToCall = "";
    public int stationId = 0;
    public string stationIp = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
