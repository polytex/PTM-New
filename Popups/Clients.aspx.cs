using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using PolytexData;

using UniStr;
using PolytexControls;
using PolytexObjects;

public partial class Popups_Clients : PolyPage
{
    int clientID = 0;
    


    protected void Page_Load(object sender, EventArgs e)
    {
        clientID = Util.ValidateInt(PolyUtils.RequestFormOrQuerystring("clientID"), 0);
        if (!IsPostBack)
        {
            if (clientID > 0)
            {
                DataTable dt = PolytexData.Manage_RemoteConnection.Select(clientID);
                if (dt.Rows.Count > 0)
                {
                    SetInputControls(dt);
                }
                else
                {
                    Manage_RemoteConnection.Insert(clientID, TextBoxDetails.Text.Trim(), TextBoxLink1.Text.Trim(), TextBoxLink2.Text.Trim());
                }
            }
        }
    
    }

    public void SetInputControls(DataTable dataTableRemoteConnection)
    {
        TextBoxDetails.Text = dataTableRemoteConnection.Rows[0]["DETAILS"].ToString();
        TextBoxLink1.Text = dataTableRemoteConnection.Rows[0]["LINK_1"].ToString();
        TextBoxLink2.Text = dataTableRemoteConnection.Rows[0]["LINK_2"].ToString();
    }

    protected void ButtonUpdateClientDetails(object sender, EventArgs e)
    {
        Manage_RemoteConnection.Update(clientID, TextBoxDetails.Text.Trim(), TextBoxLink1.Text.Trim(), TextBoxLink2.Text.Trim());
    }

}
