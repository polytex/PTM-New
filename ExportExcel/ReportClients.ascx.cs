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

public partial class ExportExcel_ReportClients : PolyExportReport
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["fromDate"].DefaultValue = PolyReport.GetFromDateTime().ToString();
        ObjectDataSource1.SelectParameters["toDate"].DefaultValue = PolyReport.GetToDateTime().ToString();

        if (IsSendReport)
        {

            ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = RequestQuerystring("territoryId");
        }
        else
        {
            ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = PolyUtils.RequestFormByContainedKey("TextBoxTerritoryId");
        }



        RepeaterExportReport.DataBind();
    }
}
