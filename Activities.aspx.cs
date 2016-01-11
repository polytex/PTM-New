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


public partial class Activities : PolyPage
{
    #region VARIABLES

    PolytexControls.DropDownList DropDownListClient;
    PolytexControls.DropDownList DropDownListSystemUser;
    PolytexControls.TextBox TextBoxDateStart;
    PolytexControls.TextBox TextBoxUnit;
    PolytexControls.DropDownList DropDownListActivityGroupName;
    PolytexControls.DropDownList DropDownListActivityTypeName;

    #endregion

    #region METHODS

    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
        ObjectDataSourceClient.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
        ObjectDataSourceSystemUser.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
        ObjectDataSourceSystemUser.SelectParameters["systemUserId"].DefaultValue = CurrentUser.SystemUserId.ToString();

        if (!IsPostBack)
        {
            if (CurrentUser.CurrentRoleType.ToString() == "Technician")
            {
                ObjectDataSource1.SelectParameters["systemUserId"].DefaultValue = CurrentUser.SystemUserId.ToString();
            }
        }
    }

    protected void GridViewOnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        int activityId;
        switch (commandName)
        {
            case "Edit":
                activityId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("ActivityDetails.aspx?activityId=" + activityId.ToString(), true);
                break;
            case "Enable":
                Manage_Activities.Enable(Convert.ToInt32(e.CommandArgument), true);
                PolyGridViewUtils.SetSelectedRow(GridView1, e.CommandArgument);
                SetActionMessage(ActionMessage.EnabledSuccessfuly);
                break;
            case "Disable":
                Manage_Activities.Disable(Convert.ToInt32(e.CommandArgument), false);
                if (CheckBoxIncludeDisabled.Checked)
                {
                    PolyGridViewUtils.SetSelectedRow(GridView1, e.CommandArgument);
                }
                else
                {
                    PolyGridViewUtils.SetSelectedRow(GridView1, 0);
                }
                SetActionMessage(ActionMessage.DisabledSuccessfuly);
                break;
            case "Filter":

                GridViewRow gridViewRowData = GridView1.FooterRow;
                DropDownListSystemUser = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListSystemUser"));
                DropDownListClient = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListClient"));
                TextBoxDateStart = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxDateStart"));
                TextBoxUnit = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxUnit"));
                DropDownListActivityGroupName = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListGroupName"));
                DropDownListActivityTypeName = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListActivityTypeName"));

                ObjectDataSource1.SelectParameters["clientId"].DefaultValue = Util.ValidateInt(DropDownListClient.AspDropDownList.SelectedValue, 0).ToString();
                ObjectDataSource1.SelectParameters["systemUserId"].DefaultValue = Util.ValidateInt(DropDownListSystemUser.AspDropDownList.SelectedValue, 0).ToString();
                ObjectDataSource1.SelectParameters["unit"].DefaultValue = TextBoxUnit.Text;
                ObjectDataSource1.SelectParameters["dateStart"].DefaultValue = (TextBoxDateStart.Text != "" ? DateTime.Parse(TextBoxDateStart.Text).ToString() : "");
                ObjectDataSource1.SelectParameters["activityGroupId"].DefaultValue = Util.ValidateInt(DropDownListActivityGroupName.AspDropDownList.SelectedValue, 0).ToString();
                ObjectDataSource1.SelectParameters["activitytypeId"].DefaultValue = Util.ValidateInt(DropDownListActivityTypeName.AspDropDownList.SelectedValue, 0).ToString();
                ObjectDataSource1.DataBind();

                HiddenFieldClientId.Value = Util.ValidateInt(DropDownListClient.AspDropDownList.SelectedValue, 0).ToString();
                HiddenFieldSystemUserId.Value = Util.ValidateInt(DropDownListSystemUser.AspDropDownList.SelectedValue, 0).ToString();
                HiddenFieldDateStart.Value = TextBoxDateStart.Text;
                HiddenFieldUnit.Value = TextBoxUnit.Text;
                HiddenFieldActivityGroup.Value = Util.ValidateInt(DropDownListActivityGroupName.AspDropDownList.SelectedValue, 0).ToString();
                HiddenFieldActivityType.Value = Util.ValidateInt(DropDownListActivityTypeName.AspDropDownList.SelectedValue, 0).ToString();
                break;
            case "Clear":

                HiddenFieldClientId.Value = "0";
                HiddenFieldSystemUserId.Value = "0";
                HiddenFieldActivityGroup.Value = "0";
                HiddenFieldActivityType.Value = "0";
                HiddenFieldDateStart.Value = "";
                HiddenFieldUnit.Value = "";

                ObjectDataSource1.SelectParameters["clientId"].DefaultValue = "0";
                ObjectDataSource1.SelectParameters["systemUserId"].DefaultValue = "0";
                ObjectDataSource1.SelectParameters["unit"].DefaultValue = "";
                ObjectDataSource1.SelectParameters["dateStart"].DefaultValue = "";
                ObjectDataSource1.SelectParameters["activityGroupId"].DefaultValue = "0";
                ObjectDataSource1.SelectParameters["activitytypeId"].DefaultValue = "0";
                break;
        }
    }

    protected void CheckBoxIncludeDisabled_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBoxIncludeDisabled.Checked)
        {
            ObjectDataSource1.SelectParameters["includeDisabled"].DefaultValue = "true";
        }
        else
        {
            ObjectDataSource1.SelectParameters["includeDisabled"].DefaultValue = "false";
        }
        PolyGridViewUtils.ClearSetRows(GridView1, true, true);
        GridView1.PageIndex = 0;
    }

    protected void GridViewOnPreRender(object sender, EventArgs e)
    {

        int cliendId = Util.ValidateInt(HiddenFieldClientId.Value, 0);
        int systemUserId = Util.ValidateInt(HiddenFieldSystemUserId.Value, 0);
        int activityGroupId = Util.ValidateInt(HiddenFieldActivityGroup.Value, 0);
        int activityTypeId = Util.ValidateInt(HiddenFieldActivityType.Value, 0);

        GridViewRow gridViewRowData = GridView1.FooterRow;

        DropDownListSystemUser = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListSystemUser"));
        DropDownListClient = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListClient"));
        TextBoxDateStart = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxDateStart"));
        TextBoxUnit = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxUnit"));
        DropDownListActivityGroupName = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListGroupName"));
        DropDownListActivityTypeName = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListActivityTypeName"));

        if (!IsPostBack)
        {
            if (CurrentUser.CurrentRoleType.ToString() == "Technician")
            {
                DropDownListSystemUser.AspDropDownList.SelectedValue = CurrentUser.SystemUserId.ToString();
            }
        }

        TextBoxDateStart.Text = HiddenFieldDateStart.Value;
        TextBoxUnit.Text = HiddenFieldUnit.Value;

        if (cliendId > 0)
        {
            DropDownListClient.AspDropDownList.SelectedValue = cliendId.ToString();
        }
        if (systemUserId > 0)
        {
            DropDownListSystemUser.AspDropDownList.SelectedValue = systemUserId.ToString();
        }
        if (activityGroupId > 0)
        {
            DropDownListActivityGroupName.AspDropDownList.SelectedValue = activityGroupId.ToString();
        }
        if (activityTypeId > 0)
        {
            DropDownListActivityTypeName.AspDropDownList.SelectedValue = activityTypeId.ToString();
        }
    }

    #endregion
}
