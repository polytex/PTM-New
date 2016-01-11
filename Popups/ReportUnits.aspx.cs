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

public partial class Popups_ReportUnits : PolyPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (PolyPage.IsMobile)
        {
            trQueryFromToDate.Style.Add(HtmlTextWriterStyle.Height, "130px");
        }

        DropDownListTerritoryId.AspDropDownList.DataSource = GetTerritoryIncludeEmpty();
        DropDownListTerritoryId.AspDropDownList.DataBind();

        ObjectDataSourceClient.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();

        DropDownListTerritoryId.AspDropDownList.Attributes.Add("onchange", "SwitchTarget()");

        if (CurrentUser.TerritoryId > 0)
        {
            DropDownListTerritoryId.AspDropDownList.SelectedValue = CurrentUser.TerritoryId.ToString();
        }
       
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        int territoryId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("TerritoryId"), 0);
        int clientId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("ClientId"), 0);

        if (!IsPostBack)
        {
            PolytexControls.TextBox TextBoxFromDate = (PolytexControls.TextBox)(QueryFromToDate1.FindControl("TextBoxFromDate"));
            PolytexControls.TextBox TextBoxToDate = (PolytexControls.TextBox)(QueryFromToDate1.FindControl("TextBoxToDate"));
            TextBoxToDate.Text = DateTime.Now.ToShortDateString();

            DateTime startDate;
            if (DateTime.TryParse(PolyUtils.RequestQuerystring("TextBoxFromDateTime"), out startDate))
            {
                if (startDate != new DateTime())
                {
                    TextBoxFromDate.Text = startDate.ToShortDateString();
                }
                else
                {
                    TextBoxFromDate.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                }
            }
        }

        if (territoryId > 0)
        {
            DropDownListTerritoryId.AspDropDownList.SelectedValue = territoryId.ToString();
            ObjectDataSourceClient.SelectParameters["territoryId"].DefaultValue = territoryId.ToString();
            DropDownListClientId.AspDropDownList.DataBind();
        }
        else
        {
            DropDownListClientId.AspDropDownList.DataBind();
        }

        if (clientId > 0)
        {
            if (DropDownListClientId.AspDropDownList.Items.FindByValue(clientId.ToString()) != null)
            {
                DropDownListClientId.AspDropDownList.SelectedValue = clientId.ToString();
            }
        }
    }

    public DataTable GetTerritoryIncludeEmpty()
    {
        DataTable dt = Manage_Territories.Select(false, CurrentUser.TerritoryId, false);

        DataRow dr = dt.NewRow();
        dr["NAME"] = "";
        dr["ID"] = "0";
        dt.Rows.InsertAt(dr, 0);

        return dt;

    }
}
