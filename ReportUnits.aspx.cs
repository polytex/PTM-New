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

public partial class ReportUnits : PolyReport
{
    #region VARIABLES

    public int TerritoryId = 0;
    public int clientId = 0;

    #endregion

    #region METHODS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ShowReport)
        {
            TableReport.Visible = true;

            TerritoryId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("TerritoryId"), CurrentUser.TerritoryId);
            Int32.TryParse(PolyUtils.RequestFormByContainedKey("ClientId"), out clientId);

            ObjectDataSource1.SelectParameters["skip"].DefaultValue = Skip.ToString();
            ObjectDataSource1.SelectParameters["fromDate"].DefaultValue = FromDateTime.ToString();
            ObjectDataSource1.SelectParameters["toDate"].DefaultValue = ToDateTime.ToString();
            ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = TerritoryId.ToString();
            ObjectDataSource1.SelectParameters["clientId"].DefaultValue = clientId.ToString();

            LabelTerritoryName.Text = ApplicationData.UpdateLableTerritoryName(CurrentUser.TerritoryId, CurrentUser.SystemUserId, TerritoryId);
        }
        else
        {
            TableReport.Visible = false;
        }
    }

    public void GridViewOnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string name = e.CommandName;

        if (name == "UnitId")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string fromDate = ObjectDataSource1.SelectParameters["fromDate"].DefaultValue;
            string toDate = ObjectDataSource1.SelectParameters["toDate"].DefaultValue;

            Response.Redirect("ReportActivities.aspx?ShowReport=1&fromPopup=false&FromDateTime=" + fromDate + "&ToDateTime=" + toDate + "&" + name + "=" + id, true);
        }

    }

    public string GetTotalHours()
    {
        string totalHours = Util.ValidateInt(ObjectDataSource1.ObjectDataSourceDataTable.ExtendedProperties["TOTAL"].ToString(), 0).ToString();
        return totalHours;
    }

    public string GetTotalActivities()
    {
        string totalActivities = Util.ValidateInt(ObjectDataSource1.ObjectDataSourceDataTable.ExtendedProperties["TOTAL_ACTIVITIES"].ToString(), 0).ToString();
        return totalActivities;
    }

    #endregion
}
