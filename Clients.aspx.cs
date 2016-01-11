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

public partial class Clients : PolyPage
{
    #region Variables

    //For Items GridView controls reference
    PolytexControls.TextBox TextBoxName;
    PolytexControls.DropDownList DropDownListTerritoryId;
    PolytexControls.TextBox TextBoxManagerVersion;

    int systemUserId;

    #endregion

    #region Methods

    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSourceTerritory.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
        if (!IsPostBack)
        {
            ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
            HiddenFieldTerritoryId.Value = CurrentUser.TerritoryId.ToString();
        }
    }

    protected void GridViewOnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        GridViewRow gridViewRowData;
        switch (commandName)
        {
            case "Insert":
                PolyGridViewUtils.ClearSetRows(GridView1, true, true);
                gridViewRowData = GridView1.FooterRow;
                Page.Validate("GridEditItem");
                if (Page.IsValid)
                {
                    SetInputControls(gridViewRowData);
                    systemUserId = Manage_Clients.Insert(TextBoxName.Text.Trim(), DropDownListTerritoryId.AspDropDownList.SelectedValue, TextBoxManagerVersion.Text.Trim());
                    PolyGridViewUtils.ClearSetRows(GridView1, true, true);
                    PolyGridViewUtils.SetSelectedRow(GridView1, systemUserId);
                    SetActionMessage(ActionMessage.InsertedSuccessfuly); 
                }
                else
                {
                    SetActionMessage(ActionMessage.InsertFailure);
                }
                break;
            case "UpdateClient":
                gridViewRowData = GridView1.Rows[GridView1.EditIndex];
                systemUserId = Convert.ToInt32(e.CommandArgument);
                Page.Validate("GridEditItem");
                if (Page.IsValid)
                {
                    SetInputControls(gridViewRowData);
                    if (Manage_Clients.Update(systemUserId, TextBoxName.Text, DropDownListTerritoryId.AspDropDownList.SelectedValue,TextBoxManagerVersion.Text))
                    {
                        PolyGridViewUtils.SetSelectedRow(GridView1, systemUserId);
                        SetActionMessage(ActionMessage.UpdatedSuccessfuly);
                    }
                    else
                    {
                        SetActionMessage(ActionMessage.UpdateFailure);
                    }
                }
                else
                {
                    SetActionMessage(ActionMessage.UpdateFailure);
                }
                break;
            case "Enable":
                Manage_Clients.Enable(Convert.ToInt32(e.CommandArgument), true);
                PolyGridViewUtils.SetSelectedRow(GridView1, e.CommandArgument);
                SetActionMessage(ActionMessage.EnabledSuccessfuly);
                break;
            case "Disable":
                Manage_Clients.Disable(Convert.ToInt32(e.CommandArgument), false);
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
                gridViewRowData = GridView1.FooterRow;
                TextBoxName = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxName"));
                DropDownListTerritoryId = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListTerritory"));
                TextBoxManagerVersion = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxManagerVersion"));
                ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = Util.ValidateInt(DropDownListTerritoryId.AspDropDownList.SelectedValue, CurrentUser.TerritoryId).ToString();
                ObjectDataSource1.SelectParameters["clientName"].DefaultValue = TextBoxName.Text;
                ObjectDataSource1.SelectParameters["version"].DefaultValue = TextBoxManagerVersion.Text;
                ObjectDataSource1.DataBind();
                HiddenFieldTerritoryId.Value = Util.ValidateInt(DropDownListTerritoryId.AspDropDownList.SelectedValue, CurrentUser.TerritoryId).ToString();
                HiddenFieldClient.Value = TextBoxName.Text;
                HiddenFieldVersion.Value = TextBoxManagerVersion.Text;
                break;
            case "Clear":
                HiddenFieldTerritoryId.Value = CurrentUser.TerritoryId.ToString();
                HiddenFieldClient.Value = "";
                HiddenFieldVersion.Value = "";
                ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
                ObjectDataSource1.SelectParameters["clientName"].DefaultValue = "";
                ObjectDataSource1.SelectParameters["version"].DefaultValue = "";
                break;
        }
    }

    protected void GridViewOnPreRender(object sender, EventArgs e)
    {
        int territoryId = Util.ValidateInt(HiddenFieldTerritoryId.Value, CurrentUser.TerritoryId);
        GridViewRow gridViewRowData = GridView1.FooterRow;

        TextBoxName = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxName"));
        DropDownListTerritoryId = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListTerritory"));
        TextBoxManagerVersion = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxManagerVersion"));

        TextBoxManagerVersion.Text = HiddenFieldVersion.Value;
        TextBoxName.Text = HiddenFieldClient.Value;
        if (territoryId > 0)
        {
            DropDownListTerritoryId.AspDropDownList.SelectedValue = territoryId.ToString();
        }
        else
        {
            DropDownListTerritoryId.AspDropDownList.SelectedValue = null;
        }
        
    }

    //Assigns the right controls to global controls variables
    public void SetInputControls(GridViewRow gridViewRowData)
    {
        TextBoxName = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxName"));
        DropDownListTerritoryId = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListTerritory"));
        TextBoxManagerVersion = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxManagerVersion"));
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

    protected void CustomValidatorClientNameUnique(object source, ServerValidateEventArgs args)
    {
        GridViewRow gridViewRowData = GridView1.Rows[GridView1.EditIndex];
        SetInputControls(gridViewRowData);
        string TerritoryId = DropDownListTerritoryId.AspDropDownList.SelectedValue.ToString();
        if (systemUserId > 0)
        {
            string clientName = ((PolytexControls.TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("TextBoxName"))).Text;
            args.IsValid = PolyUtils.DbObjectNameIsUniqueByTable(@"""NAME""", "CLIENTS", clientName, "TERRITORY_ID", TerritoryId, systemUserId);
        }
        else
        {
            args.IsValid = false;
        }
    }

    protected void CustomValidatorClientNameExists(object source, ServerValidateEventArgs args)
    {
        string clientName = ((PolytexControls.TextBox)GridView1.FooterRow.Cells[1].FindControl("TextBoxName")).Text;
        string TerritoryId = ((PolytexControls.DropDownList)GridView1.FooterRow.Cells[1].FindControl("DropDownListTerritory")).AspDropDownList.SelectedValue;
        
        args.IsValid = PolyUtils.DbObjectNameIsNewByTable(@"""NAME""", "CLIENTS", clientName, "TERRITORY_ID", TerritoryId);
    }

    protected void CustomImageButtonProperties_OnPreRender(object sender, EventArgs e)
    {
        PolytexControls.CustomImageButton ImageButtonProperties = (PolytexControls.CustomImageButton)sender;

        ImageButtonProperties.OnClientClick = "return EditSiteMenu(" + ImageButtonProperties.CommandArgument + ")";
    }

    #endregion

}
