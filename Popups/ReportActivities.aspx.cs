using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PolytexData;

using UniStr;
using PolytexControls;
using PolytexObjects;

public partial class Popups_ReportActivities : PolyPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (PolyPage.IsMobile)
        {
            trQueryFromToDate.Style.Add(HtmlTextWriterStyle.Height, "130px");
        }

        DropDownListTerritoryId.AspDropDownList.DataSource = GetTerritoryIncludeEmpty();
        DropDownListTerritoryId.AspDropDownList.DataBind();
        DropDownListClientId.AspDropDownList.DataSource = GetClientsIncludeEmpty(CurrentUser.TerritoryId);
        DropDownListClientId.AspDropDownList.DataBind();
        DropDownListActivityGroupId.AspDropDownList.DataSource = GetActivityGroupsIncludeEmpty();
        DropDownListActivityGroupId.AspDropDownList.DataBind();

        ObjectDataSourceUnit.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
        ObjectDataSourceSystemUsers.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
        
        DropDownListTerritoryId.AspDropDownList.Attributes.Add("onchange", "SwitchTarget()");
        DropDownListClientId.AspDropDownList.Attributes.Add("onchange", "SwitchTarget()");
        DropDownListActivityGroupId.AspDropDownList.Attributes.Add("onchange", "SwitchTarget()");

        if (CurrentUser.TerritoryId > 0)
        {
            DropDownListTerritoryId.AspDropDownList.SelectedValue = CurrentUser.TerritoryId.ToString();
        }

        if (PolyUtils.RequestQuerystring("ShowReport") == "True")
        {//Set values if in querystring                         
            if (PolyUtils.RequestQuerystring("CheckBoxIncludeDisabled") == "on")
            {
                CheckBoxIncludeDisabled.Checked = true;
            }
            else
            {
                CheckBoxIncludeDisabled.Checked = false;

            }
        }
        
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        

        int territoryId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("TerritoryId"), 0);
        int clientId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("ClientId"), 0);
        int activityGroupId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("ActivityGroupId"), 0);
        int modelId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("ModelId"), 0);
        int unitId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("UnitId"), 0);
        int activityTypeId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("ActivityTypeId"), 0);
        int systemUserId = Util.ValidateInt(PolyUtils.RequestFormOrQuerystringByContainedKey("SystemUserId"), 0);
        string includeImage = PolyUtils.RequestFormOrQuerystringByContainedKey("IncludeImage");

        if (includeImage == "")
        {
            CheckBoxIncludeImage.Checked = false;
        }
        else
        {
            CheckBoxIncludeImage.Checked = true;
        }



        
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
            DropDownListClientId.AspDropDownList.DataSource = GetClientsIncludeEmpty(territoryId);
            ObjectDataSourceUnit.SelectParameters["territoryId"].DefaultValue = territoryId.ToString();            
        }

        DropDownListClientId.AspDropDownList.DataBind();

        if (clientId > 0)
        {
            ObjectDataSourceUnit.SelectParameters["clientId"].DefaultValue = clientId.ToString();
            DropDownListUnitId.AspDropDownList.DataBind();
            if (DropDownListClientId.AspDropDownList.Items.FindByValue(clientId.ToString()) != null)
            {
                DropDownListClientId.AspDropDownList.SelectedValue = clientId.ToString();
            }
        }
        else
        {
            ObjectDataSourceUnit.SelectParameters["clientId"].DefaultValue = "0";
            DropDownListUnitId.AspDropDownList.DataBind();
        }

        if (activityGroupId > 0)
        {
            ObjectDataSourceActivityTypes.SelectParameters["ActivityGroupId"].DefaultValue = activityGroupId.ToString();
            DropDownListActivityGroupId.AspDropDownList.SelectedValue = activityGroupId.ToString();
            DropDownListActivityTypeId.AspDropDownList.DataBind();

        }
        else
        {
            ObjectDataSourceActivityTypes.SelectParameters["ActivityGroupId"].DefaultValue = "0";
            DropDownListActivityTypeId.AspDropDownList.DataBind();
        }

        if (modelId > 0)
        {
            DropDownListModelId.AspDropDownList.SelectedValue = modelId.ToString();
        }

        if (unitId > 0)
        {
            if (DropDownListUnitId.AspDropDownList.Items.FindByValue(unitId.ToString()) != null)
            {
                DropDownListUnitId.AspDropDownList.SelectedValue = unitId.ToString();
            }            
        }

        if (activityTypeId > 0)
        {
            if (DropDownListActivityTypeId.AspDropDownList.Items.FindByValue(activityTypeId.ToString()) != null)
            {
                DropDownListActivityTypeId.AspDropDownList.SelectedValue = activityTypeId.ToString();
            }
        }

        if (systemUserId > 0)
        {
            DropDownListSystemUserId.AspDropDownList.SelectedValue = systemUserId.ToString();
        }
    }

    public DataTable GetTerritoryIncludeEmpty()
    {
        DataTable dt = Manage_Territories.Select(false, CurrentUser.TerritoryId,false);

        DataRow dr = dt.NewRow();
        dr["NAME"] = "";
        dr["ID"] = "0";
        dt.Rows.InsertAt(dr, 0);

        return dt;

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

    public DataTable GetActivityGroupsIncludeEmpty()
    {
        DataTable dt = Manage_Activity_Groups.Select(false);

        DataRow dr = dt.NewRow();
        dr["NAME"] = "";
        dr["ID"] = "0";
        dt.Rows.InsertAt(dr, 0);

        return dt;

    }
    

}
