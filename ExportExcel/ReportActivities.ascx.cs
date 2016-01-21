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
using System.Web.UI.HtmlControls;

public partial class ExportExcel_ReportActivities : PolyExportReport
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["fromDate"].DefaultValue = PolyReport.GetFromDateTime().ToString();
        ObjectDataSource1.SelectParameters["toDate"].DefaultValue = PolyReport.GetToDateTime().ToString();

        if (IsSendReport)
        {

            ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = RequestQuerystring("territoryId");
            ObjectDataSource1.SelectParameters["clientId"].DefaultValue = RequestQuerystring("clientId");
            ObjectDataSource1.SelectParameters["modelId"].DefaultValue = RequestQuerystring("modelId");
            ObjectDataSource1.SelectParameters["unitId"].DefaultValue = RequestQuerystring("unitId");
            ObjectDataSource1.SelectParameters["activityGroupId"].DefaultValue = RequestQuerystring("activityGroupId");
            ObjectDataSource1.SelectParameters["activityTypeId"].DefaultValue = RequestQuerystring("activityTypeId");
            ObjectDataSource1.SelectParameters["systemUserId"].DefaultValue = RequestQuerystring("systemUserId");
            ObjectDataSource1.SelectParameters["IncludeDisabled"].DefaultValue = (RequestQuerystring("IncludeDisabled") == "on" ? "true" : "false");  
        }
        else
        {
            ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = PolyUtils.RequestFormByContainedKey("TextBoxTerritoryId");
            ObjectDataSource1.SelectParameters["clientId"].DefaultValue = PolyUtils.RequestFormByContainedKey("TextBoxClientId");
            ObjectDataSource1.SelectParameters["modelId"].DefaultValue = PolyUtils.RequestFormByContainedKey("TextBoxModelId");
            ObjectDataSource1.SelectParameters["unitId"].DefaultValue = PolyUtils.RequestFormByContainedKey("TextBoxUnitId");
            ObjectDataSource1.SelectParameters["activityGroupId"].DefaultValue = PolyUtils.RequestFormByContainedKey("TextBoxActivityGroupId");
            ObjectDataSource1.SelectParameters["activityTypeId"].DefaultValue = PolyUtils.RequestFormByContainedKey("TextBoxActivityTypeId");
            ObjectDataSource1.SelectParameters["systemUserId"].DefaultValue = PolyUtils.RequestFormByContainedKey("TextBoxSystemUserId");
            ObjectDataSource1.SelectParameters["IncludeDisabled"].DefaultValue = (PolyUtils.RequestFormByContainedKey("IncludeDisabled") == "on" ? "true" : "false");  
        }



        RepeaterExportReport.DataBind();
    }
}
