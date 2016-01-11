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

public partial class Popups_Confirm : PolyPage
{
    public string confirmMessage = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string action = RequestQuerystring("actionId");

        confirmMessage = _T["AreYouSure"];
        if (action != "")
        {            
            confirmMessage = _T[action].Replace("%br%", "<br>");            
        }        
    }
}
