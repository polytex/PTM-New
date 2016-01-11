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


public partial class ActivityGroups : PolyPage
{
    #region Variables

    //For Items GridView controls reference
    
    PolytexControls.TextBox TextBoxGroupName;

    PolytexControls.TextBox TextBoxActivityTypeName;

    int systemUserId;

    int activityGroupId;

    #endregion

    #region Methods GridView Activity Groups

    protected void Page_Load(object sender, EventArgs e)
    {

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
                    systemUserId = Manage_Activity_Groups.Insert(TextBoxGroupName.Text.Trim());
                    PolyGridViewUtils.ClearSetRows(GridView1, true, true);
                    PolyGridViewUtils.SetSelectedRow(GridView1, systemUserId);
                    SetActionMessage(ActionMessage.InsertedSuccessfuly);
                }
                else
                {
                    SetActionMessage(ActionMessage.InsertFailure);
                }

                break;
            case "UpdateActivityGroup":
                gridViewRowData = GridView1.Rows[GridView1.EditIndex];

                systemUserId = Convert.ToInt32(e.CommandArgument);

                Page.Validate("GridEditItem");

                if (Page.IsValid)
                {
                    SetInputControls(gridViewRowData);

                    if (Manage_Activity_Groups.Update(systemUserId, TextBoxGroupName.Text.Trim()))
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
                Manage_Activity_Groups.Enable(Convert.ToInt32(e.CommandArgument), true);
                PolyGridViewUtils.SetSelectedRow(GridView1, e.CommandArgument);
                SetActionMessage(ActionMessage.EnabledSuccessfuly);
                break;
            case "Disable":
                Manage_Activity_Groups.Disable(Convert.ToInt32(e.CommandArgument), false);
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
            case "Properties":
                //Current properties clicked
                activityGroupId = Convert.ToInt32(e.CommandArgument);
                DisplayActivityGroupItem();

                break;
        }
    }


    public void DisplayActivityGroupItem()
    {
        //Current items on items gridview
        int objectDataSource2SelectParameter = Convert.ToInt32(ObjectDataSource2.SelectParameters["activityGroupId"].DefaultValue);

        if (activityGroupId == objectDataSource2SelectParameter)
        {//Clicked twice on properties for same items group
            if (DivItems.Visible)
            {
                DivItems.Visible = false;
            }
            else
            {
                DivItems.Visible = true;
               // CheckBoxIncludeDisabledItems.Checked = false;
                PolyGridViewUtils.ClearSetRows(GridView2, true, true);
            }
        }
        else
        {
            DivItems.Visible = true;
            PolyGridViewUtils.ClearSetRows(GridView2, true, true);
           // CheckBoxIncludeDisabledItems.Checked = false;   //Reset include disabled for items grid                    
            PolyGridViewUtils.SetSelectedRow(GridView1, activityGroupId);
            ObjectDataSource2.SelectParameters["activityGroupId"].DefaultValue = activityGroupId.ToString();

        }
    }

    //Assigns the right controls to global controls variables
    public void SetInputControls(GridViewRow gridViewRowData)
    {
        TextBoxGroupName = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxGroupName"));
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

    protected void CustomValidatorActivityGroupsNameUnique(object source, ServerValidateEventArgs args)
    {
        if (systemUserId > 0)
        {
            string activityGroupName = ((PolytexControls.TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("TextBoxGroupName"))).Text;
            args.IsValid = PolyUtils.DbObjectNameIsUnique(@"""NAME""", "ACTIVITY_GROUPS", activityGroupName, systemUserId);
        }
        else
        {
            args.IsValid = false;
        }
    }

    protected void CustomValidatorActivityGroupsNameExists(object source, ServerValidateEventArgs args)
    {
        string activityGroupName = ((PolytexControls.TextBox)GridView1.FooterRow.Cells[1].FindControl("TextBoxGroupName")).Text;
        args.IsValid = PolyUtils.DbObjectNameIsNew(@"""NAME""", "ACTIVITY_GROUPS", activityGroupName);
    }


    #endregion

    #region Methods GridView Activity Types

    protected void GridView2_Load(object sender, EventArgs e)
    {
        //Remove the default handler set on GridViewOnInit
        GridView2.RowCommand -= GridViewOnRowCommand;
    }

    protected void GridViewOnRowTypesCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;

        GridViewRow gridViewRowData;
        string ActivityGroupID = ObjectDataSource2.SelectParameters["activityGroupId"].DefaultValue;
        switch (commandName)
        {
            case "Insert":
                PolyGridViewUtils.ClearSetRows(GridView1, true, true);
                gridViewRowData = GridView2.FooterRow;

                Page.Validate("GridAddActivityTypes");
                if (Page.IsValid)
                {
                    SetInputControlsActivityType(gridViewRowData);
                    systemUserId = Manage_Activity_Types.Insert(TextBoxActivityTypeName.Text.Trim(), ActivityGroupID);
                    PolyGridViewUtils.ClearSetRows(GridView2, true, true);
                    PolyGridViewUtils.SetSelectedRow(GridView2, systemUserId);
                    SetActionMessage(ActionMessage.InsertedSuccessfuly);
                }
                else
                {
                    SetActionMessage(ActionMessage.InsertFailure);
                }

                break;
            case "UpdateActivityTypes":
                gridViewRowData = GridView2.Rows[GridView2.EditIndex];

                systemUserId = Convert.ToInt32(e.CommandArgument);

                Page.Validate("GridEditActivityTypes");

                if (Page.IsValid)
                {
                    SetInputControlsActivityType(gridViewRowData);

                    if (Manage_Activity_Types.Update(systemUserId, TextBoxActivityTypeName.Text.Trim(), ActivityGroupID))
                    {
                        PolyGridViewUtils.ClearSetRows(GridView2, true, true);
                        PolyGridViewUtils.SetSelectedRow(GridView2, systemUserId);
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
                Manage_Activity_Types.Enable(Convert.ToInt32(e.CommandArgument), true);
                PolyGridViewUtils.SetSelectedRow(GridView2, e.CommandArgument);
                SetActionMessage(ActionMessage.EnabledSuccessfuly);
                break;
            case "Disable":
                Manage_Activity_Types.Disable(Convert.ToInt32(e.CommandArgument), false);
                if (CheckBoxIncludeDisabled.Checked)
                {
                    PolyGridViewUtils.SetSelectedRow(GridView2, e.CommandArgument);
                }
                else
                {
                    PolyGridViewUtils.SetSelectedRow(GridView2, 0);
                }
                SetActionMessage(ActionMessage.DisabledSuccessfuly);

                break;
        }
    }

    public void SetInputControlsActivityType(GridViewRow gridViewRowData)
    {
        TextBoxActivityTypeName = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxActivityTypeName"));
    }


    protected void CheckBoxIncludeDisabledActivityType_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBoxActivityType.Checked)
        {
            ObjectDataSource2.SelectParameters["includeDisabled"].DefaultValue = "true";
        }
        else
        {
            ObjectDataSource2.SelectParameters["includeDisabled"].DefaultValue = "false";
        }
        PolyGridViewUtils.ClearSetRows(GridView2, true, true);
        GridView1.PageIndex = 0;

    }


    protected void CustomValidatorActivityTypeNameUnique(object source, ServerValidateEventArgs args)
    {
        string ActivityGroupID = ObjectDataSource2.SelectParameters["activityGroupId"].DefaultValue;
        if (systemUserId > 0)
        {
            string ActivityTypeName = ((PolytexControls.TextBox)(GridView2.Rows[GridView2.EditIndex].FindControl("TextBoxActivityTypeName"))).Text;
            args.IsValid = PolyUtils.DbObjectNameIsUniqueByTable(@"""NAME""", "ACTIVITY_TYPES", ActivityTypeName, "ACTIVITY_GROUP_ID", ActivityGroupID, systemUserId);
        }
        else
        {
            args.IsValid = false;
        }
    }


    protected void CustomValidatorActivityTypeNameExists(object source, ServerValidateEventArgs args)
    {
        string ActivityGroupID = ObjectDataSource2.SelectParameters["activityGroupId"].DefaultValue;
        string ActivityTypeName = ((PolytexControls.TextBox)GridView2.FooterRow.Cells[1].FindControl("TextBoxActivityTypeName")).Text;
        args.IsValid = PolyUtils.DbObjectNameIsNewByTable(@"""NAME""", "ACTIVITY_TYPES", ActivityTypeName, "ACTIVITY_GROUP_ID", ActivityGroupID);
    }

    #endregion
}
