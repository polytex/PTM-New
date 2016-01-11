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

public partial class SystemUsers : PolyPage
{
    #region Variables

    //For Items GridView controls reference
    PolytexControls.TextBox TextBoxName;
    PolytexControls.TextBox TextBoxLoginName;
    PolytexControls.TextBox TextBoxPassword;
    PolytexControls.DropDownList DropDownListRoleId;
    PolytexControls.DropDownList DropDownListTerritoryId;

    int systemUserId;

    #endregion

    #region Methods GridView SystemUsers

    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
        ObjectDataSource1.SelectParameters["systemUserId"].DefaultValue = CurrentUser.SystemUserId.ToString();
        ObjectDataSourceTerritory.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
        ObjectDataSourceRoles.SelectParameters["UserRole"].DefaultValue = CurrentUser.CurrentRoleType.ToString();

        if (CurrentUser.TerritoryId == -1)
        {
            ObjectDataSourceTerritory.SelectParameters["includeWorldwide"].DefaultValue = "true";
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
                    systemUserId = Manage_System_Users.Insert(TextBoxLoginName.Text.Trim(), TextBoxPassword.Text.Trim(), TextBoxName.Text.Trim(), DropDownListTerritoryId.AspDropDownList.SelectedValue, DropDownListRoleId.SelectedValue);

                    PolyGridViewUtils.ClearSetRows(GridView1, true, true); 
                    PolyGridViewUtils.SetSelectedRow(GridView1, systemUserId);
                    SetActionMessage(ActionMessage.InsertedSuccessfuly); 
                }
                else
                {
                    SetActionMessage(ActionMessage.InsertFailure); 
                }
    
                break;
            case "UpdateSystemUser":
                gridViewRowData = GridView1.Rows[GridView1.EditIndex];
                
                systemUserId = Convert.ToInt32(e.CommandArgument);

                Page.Validate("GridEditItem");

                if (Page.IsValid)
                {
                    SetInputControls(gridViewRowData);
                    
                    string password = TextBoxPassword.Text;
                    if (password == "")
                    {//Insert existing password
                        password = DBcon.SelectField(@"USER_PASSWORD", "SYSTEM_USERS", "ID=" + systemUserId.ToString(), "");
                    }

                    if (Manage_System_Users.Update(systemUserId, TextBoxLoginName.Text.Trim(), password, TextBoxName.Text.Trim(), DropDownListTerritoryId.AspDropDownList.SelectedValue, DropDownListRoleId.AspDropDownList.SelectedValue))
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
                Manage_System_Users.Enable(Convert.ToInt32(e.CommandArgument), true);
                PolyGridViewUtils.SetSelectedRow(GridView1,  e.CommandArgument);
                SetActionMessage(ActionMessage.EnabledSuccessfuly);  
                break;
            case "Disable":
                Manage_System_Users.Disable(Convert.ToInt32(e.CommandArgument), false);  
                if (CheckBoxIncludeDisabled.Checked)
                {
                    PolyGridViewUtils.SetSelectedRow(GridView1,  e.CommandArgument);
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
        TextBoxLoginName = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxLoginName"));
        TextBoxPassword = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxPassword"));
        DropDownListRoleId = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListRoleId"));
        DropDownListTerritoryId = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListTerritory"));    
     }

    protected string GetRoleName(object id)
    {
        string roleName = "";
        DataTable dtRoles = SystemUser.GetSystemUsersRoles(true,CurrentUser.CurrentRoleType.ToString());
        
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

    protected void CustomValidatorLoginNameExists(object source, ServerValidateEventArgs args)
    {
        string loginName = ((PolytexControls.TextBox)GridView1.FooterRow.Cells[1].FindControl("TextBoxLoginName")).Text;
        args.IsValid = PolyUtils.DbObjectNameIsNew(@"""LOGIN""", "SYSTEM_USERS", loginName);        
    }

    protected void CustomValidatorLoginNameUnique(object source, ServerValidateEventArgs args)
    {        
        if (systemUserId > 0)
        {
            string loginName = ((PolytexControls.TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("TextBoxLoginName"))).Text;
            args.IsValid = PolyUtils.DbObjectNameIsUnique(@"""LOGIN""", "SYSTEM_USERS", loginName, systemUserId);
        }
        else
        {
            args.IsValid = false;
        }
    }

    protected bool ChangeVisibilitybyRole()
    {
        if (CurrentUser.CurrentRoleType.ToString().Equals("Technician"))
            return false;
        else
            return true;
    }

    #endregion
}
