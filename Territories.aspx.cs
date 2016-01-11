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

public partial class Territories : PolyPage
{
    #region Variables

    //For Items GridView controls reference
    PolytexControls.TextBox TextBoxName;

    int systemUserId;

    #endregion

    #region Methods GridView Territories

    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
    }

    protected void GridViewOnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;

        //Response.Write(commandName);

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
                    systemUserId = Manage_Territories.Insert(TextBoxName.Text.Trim());
                    PolyGridViewUtils.ClearSetRows(GridView1, true, true);
                    PolyGridViewUtils.SetSelectedRow(GridView1, systemUserId);
                    SetActionMessage(ActionMessage.InsertedSuccessfuly); 
                }
                else
                {
                    SetActionMessage(ActionMessage.InsertFailure);
                }

                break;
            case "UpdateTerritories":
                gridViewRowData = GridView1.Rows[GridView1.EditIndex];

                systemUserId = Convert.ToInt32(e.CommandArgument);

                Page.Validate("GridEditItem");

                if (Page.IsValid)
                {
                    SetInputControls(gridViewRowData);

                    if (Manage_Territories.Update(systemUserId, TextBoxName.Text.Trim()))
                    {
                        ////int systemUserRoleId = Convert.ToInt32(DropDownListRoleId.SelectedValue);
                        ////DbManage_System_User_Permissions.EndAll(systemUserId);
                        ////DbManage_System_User_Permissions.Insert(systemUserId, systemUserRoleId, DateTime.Today.Ticks, 0L);

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
                Manage_Territories.Enable(Convert.ToInt32(e.CommandArgument), true);
                PolyGridViewUtils.SetSelectedRow(GridView1, e.CommandArgument);
                SetActionMessage(ActionMessage.EnabledSuccessfuly);
                break;
            case "Disable":
                Manage_Territories.Disable(Convert.ToInt32(e.CommandArgument), false);
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
        }
    }

    //Assigns the right controls to global controls variables
    public void SetInputControls(GridViewRow gridViewRowData)
    {
        TextBoxName = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxName"));
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

    protected void CustomValidatorTerritoryNameUnique(object source, ServerValidateEventArgs args)
    {
        if (systemUserId > 0)
        {
            string territoryName = ((PolytexControls.TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("TextBoxName"))).Text;
            args.IsValid = PolyUtils.DbObjectNameIsUnique(@"""NAME""", "TERRITORIES", territoryName, systemUserId);
        }
        else
        {
            args.IsValid = false;
        }
    }

    protected void CustomValidatorTerritoryNameExists(object source, ServerValidateEventArgs args)
    {
        string territoryName = ((PolytexControls.TextBox)GridView1.FooterRow.Cells[1].FindControl("TextBoxName")).Text;
        args.IsValid = PolyUtils.DbObjectNameIsNew(@"""NAME""", "TERRITORIES", territoryName);
    }

    #endregion

}
