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

public partial class Models : PolyPage
{
    #region Variables

    //For Items GridView controls reference
    PolytexControls.TextBox TextBoxName;

    int systemUserId;

    #endregion

    #region Methods GridView Models

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
                    systemUserId = Manage_Models.Insert(TextBoxName.Text.Trim());
                    PolyGridViewUtils.ClearSetRows(GridView1, true, true);
                    PolyGridViewUtils.SetSelectedRow(GridView1, systemUserId);
                    SetActionMessage(ActionMessage.InsertedSuccessfuly); 
                }
                else
                {
                    SetActionMessage(ActionMessage.InsertFailure);
                }

                break;
            case "UpdateModels":
                gridViewRowData = GridView1.Rows[GridView1.EditIndex];

                systemUserId = Convert.ToInt32(e.CommandArgument);

                Page.Validate("GridEditItem");

                if (Page.IsValid)
                {
                    SetInputControls(gridViewRowData);

                    if (Manage_Models.Update(systemUserId, TextBoxName.Text.Trim()))
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
                Manage_Models.Enable(Convert.ToInt32(e.CommandArgument), true);
                PolyGridViewUtils.SetSelectedRow(GridView1, e.CommandArgument);
                SetActionMessage(ActionMessage.EnabledSuccessfuly);
                break;
            case "Disable":
                Manage_Models.Disable(Convert.ToInt32(e.CommandArgument), false);
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

    protected void CustomValidatorModelNameUnique(object source, ServerValidateEventArgs args)
    {
        if (systemUserId > 0)
        {
            string modelName = ((PolytexControls.TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("TextBoxName"))).Text;
            args.IsValid = PolyUtils.DbObjectNameIsUnique(@"""NAME""", "MODELS", modelName, systemUserId);
        }
        else
        {
            args.IsValid = false;
        }
    }

    protected void CustomValidatorModelNameExists(object source, ServerValidateEventArgs args)
    {
        string modelName = ((PolytexControls.TextBox)GridView1.FooterRow.Cells[1].FindControl("TextBoxName")).Text;
        args.IsValid = PolyUtils.DbObjectNameIsNew(@"""NAME""", "MODELS", modelName);
    }

    #endregion

}
