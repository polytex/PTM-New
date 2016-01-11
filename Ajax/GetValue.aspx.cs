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

using PolytexObjects;
using PolytexData;
//using FirebirdSql.Data.FirebirdClient;


public partial class Ajax_GetValue : System.Web.UI.Page
{

    public string returnValue = "";

    
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = PolyUtils.RequestQuerystring("action");

        switch (action)
        {
            case "GetClients":
                GetClients();
                break;
        }
    }


    private void GetClients()
    {
        returnValue = "Teva,Tnuva...";
    }
}
