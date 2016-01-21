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
using System.IO;
using PolytexData;

using UniStr;
using PolytexControls;
using PolytexObjects;

public partial class ReportActivities : PolyReport
{
    #region VARIABLES

    public int TerritoryId = 0;
    public int ModelId = 0;
    public int clientId = 0;
    public int unitId = 0;
    public int activityGroupId = 0;
    public int activityTypeId = 0;
    public int systemUserId = 0;
    public string IncludeImage = "";
    public string IncludeDisabled = "";

    
    #endregion

    #region METHODS

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (ShowReport)
        {
            TableReport.Visible = true;
            
            if (PolyUtils.RequestFormOrQuerystringByContainedKey("FromDateTime") != "")
            {
                FromDateTime = Convert.ToDateTime(PolyUtils.RequestFormOrQuerystringByContainedKey("FromDateTime"));
                ToDateTime = Convert.ToDateTime(PolyUtils.RequestFormOrQuerystringByContainedKey("ToDateTime"));
            }

            TerritoryId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("TerritoryId"), CurrentUser.TerritoryId);
            ModelId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("ModelId"), 0);
            clientId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("ClientId"), 0);
            unitId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("UnitId"), 0);
            activityGroupId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("ActivityGroupId"), 0);
            activityTypeId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("ActivityTypeId"), 0);
            systemUserId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("SystemUserId"), 0);
            IncludeImage = PolyUtils.RequestFormOrQuerystringByContainedKey("IncludeImage");
            IncludeDisabled = PolyUtils.RequestFormOrQuerystringByContainedKey("IncludeDisabled");


            //Checkbox include image not checked
            if (IncludeImage == "")
            {
                GridView1.Columns[16].Visible = false;
            }

            ObjectDataSource1.SelectParameters["skip"].DefaultValue = Skip.ToString();            
            ObjectDataSource1.SelectParameters["fromDate"].DefaultValue = FromDateTime.ToString();
            ObjectDataSource1.SelectParameters["toDate"].DefaultValue = ToDateTime.ToString();
            ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = TerritoryId.ToString();
            ObjectDataSource1.SelectParameters["modelId"].DefaultValue = ModelId.ToString();
            ObjectDataSource1.SelectParameters["clientId"].DefaultValue = clientId.ToString();
            ObjectDataSource1.SelectParameters["unitId"].DefaultValue = unitId.ToString();
            ObjectDataSource1.SelectParameters["activityGroupId"].DefaultValue = activityGroupId.ToString();
            ObjectDataSource1.SelectParameters["activityTypeId"].DefaultValue = activityTypeId.ToString();
            ObjectDataSource1.SelectParameters["systemUserId"].DefaultValue = systemUserId.ToString();
            ObjectDataSource1.SelectParameters["IncludeDisabled"].DefaultValue = (IncludeDisabled == "on" ? "true" : "false");  

            LabelTerritoryName.Text = ApplicationData.UpdateLableTerritoryName(CurrentUser.TerritoryId,CurrentUser.SystemUserId,TerritoryId);
        }
        else
        {
            TableReport.Visible = false;
        }
    }

    protected void OnLoad(object sender, GridViewRowEventArgs e)
    {
    }

    public string GetImageSource(object Id)
    {
        string source = "ActivityImages/NoImage.png";
        int activityId = Util.ValidateInt(Id.ToString(), 0);
        string rootPath = HttpContext.Current.Request.PhysicalApplicationPath;
        string[] files = Directory.GetFiles(rootPath + "ActivityImages\\", activityId + "_1.*");

        if (files.Length > 0)
        {
            // Getting the extension of the file
           string[] extension = files[0].Split('.');
           source = "ActivityImages/" + activityId + "_1." + extension[extension.Length - 1];
        }

        return source;
    }


    

    #endregion
}
