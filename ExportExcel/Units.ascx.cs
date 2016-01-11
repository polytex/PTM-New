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

public partial class ExportExcel_Units : PolyExportReport
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = PolyUtils.RequestFormByContainedKey("HiddenFieldTerritoryId");
        ObjectDataSource1.SelectParameters["serial"].DefaultValue = PolyUtils.RequestFormByContainedKey("HiddenFieldSerial");
        ObjectDataSource1.SelectParameters["clientId"].DefaultValue = Util.ValidateInt(PolyUtils.RequestFormByContainedKey("HiddenFieldClientId"), 0).ToString();
        ObjectDataSource1.SelectParameters["modelId"].DefaultValue = Util.ValidateInt(PolyUtils.RequestFormByContainedKey("HiddenFieldModelId"), 0).ToString();
        ObjectDataSource1.SelectParameters["description"].DefaultValue = PolyUtils.RequestFormByContainedKey("HiddenFieldDescription");
        ObjectDataSource1.SelectParameters["unitIp"].DefaultValue = PolyUtils.RequestFormByContainedKey("HiddenFieldUnitIP");
        ObjectDataSource1.SelectParameters["cameraIp"].DefaultValue = PolyUtils.RequestFormByContainedKey("HiddenFieldCameraIP");
        ObjectDataSource1.SelectParameters["swVersion"].DefaultValue = PolyUtils.RequestFormByContainedKey("HiddenFieldSWVersion");

        RepeaterExportReport.DataBind();
    }
}
