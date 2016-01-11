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

public partial class ExportExcel_SystemUsers : PolyExportReport
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = PolyUtils.RequestFormByContainedKey("TextBoxTerritoryId");
        ObjectDataSource1.SelectParameters["systemUserId"].DefaultValue = PolyUtils.RequestFormByContainedKey("TextBoxSystemUserId");

        RepeaterExportReport.DataBind();
    }

    protected string GetRoleName(object id)
    {
        string roleName = "";
        DataTable dtRoles = SystemUser.GetSystemUsersRoles(true, roleName);

        foreach (DataRow row in dtRoles.Rows)
        {
            if (row["ID"].ToString() == id.ToString())
            {
                roleName = row["NAME"].ToString();
                break;
            }
        }
        if (id.ToString() == "-1")
        {
            roleName = "Polytex Admin";
        }

        return roleName;
    }
}
