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


public partial class Popups_ReportActivityType : PolyPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (PolyPage.IsMobile)
        {
            trQueryFromToDate.Style.Add(HtmlTextWriterStyle.Height, "130px");
        }

        ObjectDataSourceTerritory.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        int territoryId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("TerritoryId"), 0);
        int activityGroupId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("ActivityGroupId"), 0);

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

        if (activityGroupId > 0)
        {
            DropDownListActivityGroupId.AspDropDownList.SelectedValue = activityGroupId.ToString();
        }


        if (territoryId > 0)
        {
            DropDownListTerritoryId.AspDropDownList.SelectedValue = territoryId.ToString();
        }

    }
}
