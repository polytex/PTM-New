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

public partial class Popups_Units : PolyPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DropDownListClientId.DataSource = GetClientsIncludeEmpty(CurrentUser.TerritoryId);
            DropDownListClientId.DataBind();

            TextBoxSerial.Text = PolyUtils.RequestFormOrQuerystringByContainedKey("Serial");
            DropDownListClientId.SelectedValue = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("ClientId"), 0).ToString();
        }
    }

    public DataTable GetClientsIncludeEmpty(int territoryId)
    {
        DataTable dt = Manage_Clients.Select(false, territoryId);

        DataRow dr = dt.NewRow();
        dr["NAME"] = "";
        dr["ID"] = "0";
        dt.Rows.InsertAt(dr, 0);

        return dt;

    }
}
