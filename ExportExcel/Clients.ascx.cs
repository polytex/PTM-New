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

public partial class ExportExcel_Clients : PolyExportReport
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = Util.ValidateInt(PolyUtils.RequestFormByContainedKey("HiddenFieldTerritoryId"), 0).ToString();
        ObjectDataSource1.SelectParameters["clientName"].DefaultValue = PolyUtils.RequestFormByContainedKey("HiddenFieldClient");
        ObjectDataSource1.SelectParameters["version"].DefaultValue = PolyUtils.RequestFormByContainedKey("HiddenFieldVersion");

        RepeaterExportReport.DataBind();
    }
}
