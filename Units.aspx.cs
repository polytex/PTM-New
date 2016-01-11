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

public partial class Units : PolyPage
{
    #region Variables

    PolytexControls.DropDownList DropDownListClient;
    PolytexControls.DropDownList DropDownListModel;
    PolytexControls.DropDownList DropDownListTerritory;
    PolytexControls.TextBox TextBoxSerial;
    PolytexControls.TextBox TextBoxDescription;
    PolytexControls.TextBox TextBoxUnitIp;
    PolytexControls.TextBox TextBoxCameraIp;
    PolytexControls.TextBox TextBoxSWVersion;

    public string serial = "";
    public int clientId = 0;

    int territoryId;
    int systemUserId;



    #endregion

    #region Methods GridView Units

    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectDataSource1.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
        ObjectDataSourceClient.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
    }

    protected void GridViewOnPreRender(object sender, EventArgs e)
    {
        int cliendId = Util.ValidateInt(HiddenFieldClientId.Value, 0);
        int modelId = Util.ValidateInt(HiddenFieldModelId.Value, 0);
        GridViewRow gridViewRowData = GridView1.FooterRow;

        HiddenFieldTerritoryId.Value = CurrentUser.TerritoryId.ToString();

        DropDownListModel = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListModel"));
        DropDownListClient = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListClient"));
        TextBoxSerial = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxSerial"));
        TextBoxDescription = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxDescription"));
        TextBoxUnitIp = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxUnitIp"));
        TextBoxCameraIp = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxCameraIp"));
        TextBoxSWVersion = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxSWVersion"));

        TextBoxSerial.Text = HiddenFieldSerial.Value;
        TextBoxDescription.Text = HiddenFieldDescription.Value;
        TextBoxUnitIp.Text = HiddenFieldUnitIP.Value;
        TextBoxCameraIp.Text = HiddenFieldCameraIP.Value;
        TextBoxSWVersion.Text = HiddenFieldSWVersion.Value;

        if (cliendId > 0)
        {
            DropDownListClient.AspDropDownList.SelectedValue = cliendId.ToString();
        }
        if (modelId > 0)
        {
            DropDownListModel.AspDropDownList.SelectedValue = modelId.ToString();
        }
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

                Page.Validate("GridAddItem");
                if (Page.IsValid)
                {
                    SetInputControls(gridViewRowData);
                    systemUserId = Manage_Units.Insert(DropDownListClient.AspDropDownList.SelectedValue, DropDownListModel.AspDropDownList.SelectedValue, TextBoxSerial.Text.Trim(), TextBoxDescription.Text.Trim(), TextBoxUnitIp.Text.Trim(), TextBoxCameraIp.Text.Trim(), TextBoxSWVersion.Text.Trim());
                    PolyGridViewUtils.ClearSetRows(GridView1, true, true);
                    PolyGridViewUtils.SetSelectedRow(GridView1, systemUserId);
                    SetActionMessage(ActionMessage.InsertedSuccessfuly); 
                }
                else
                {
                    SetActionMessage(ActionMessage.InsertFailure);
                }

                break;
            case "UpdateUnits":
                gridViewRowData = GridView1.Rows[GridView1.EditIndex];

                systemUserId = Convert.ToInt32(e.CommandArgument);

                Page.Validate("GridEditItem");

                if (Page.IsValid)
                {
                    SetInputControls(gridViewRowData);

                    if (Manage_Units.Update(systemUserId, DropDownListClient.AspDropDownList.SelectedValue, DropDownListModel.AspDropDownList.SelectedValue, TextBoxSerial.Text.Trim(), TextBoxDescription.Text.Trim(), TextBoxUnitIp.Text.Trim(), TextBoxCameraIp.Text.Trim(), TextBoxSWVersion.Text.Trim()))
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
                Manage_Units.Enable(Convert.ToInt32(e.CommandArgument), true);
                PolyGridViewUtils.SetSelectedRow(GridView1, e.CommandArgument);
                SetActionMessage(ActionMessage.EnabledSuccessfuly);
                break;
            case "Disable":
                Manage_Units.Disable(Convert.ToInt32(e.CommandArgument), false);
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
                DropDownListClient = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListClient"));
                DropDownListModel = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListModel"));
                TextBoxSerial = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxSerial"));
                TextBoxDescription = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxDescription"));
                TextBoxUnitIp = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxUnitIp"));
                TextBoxCameraIp = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxCameraIp"));
                TextBoxSWVersion = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxSWVersion"));
                ObjectDataSource1.SelectParameters["serial"].DefaultValue = TextBoxSerial.Text;
                ObjectDataSource1.SelectParameters["clientId"].DefaultValue = Util.ValidateInt(DropDownListClient.AspDropDownList.SelectedValue, 0).ToString();
                ObjectDataSource1.SelectParameters["modelId"].DefaultValue =  Util.ValidateInt(DropDownListModel.AspDropDownList.SelectedValue, 0).ToString();
                ObjectDataSource1.SelectParameters["description"].DefaultValue = TextBoxDescription.Text;
                ObjectDataSource1.SelectParameters["unitIp"].DefaultValue = TextBoxUnitIp.Text;
                ObjectDataSource1.SelectParameters["cameraIp"].DefaultValue = TextBoxCameraIp.Text;
                ObjectDataSource1.SelectParameters["swVersion"].DefaultValue = TextBoxSWVersion.Text;
                ObjectDataSource1.DataBind();
                HiddenFieldClientId.Value = Util.ValidateInt(DropDownListClient.AspDropDownList.SelectedValue, 0).ToString();
                HiddenFieldSerial.Value = TextBoxSerial.Text;
                HiddenFieldModelId.Value = Util.ValidateInt(DropDownListModel.AspDropDownList.SelectedValue, 0).ToString();
                HiddenFieldDescription.Value = TextBoxDescription.Text;
                HiddenFieldCameraIP.Value = TextBoxCameraIp.Text;
                HiddenFieldSWVersion.Value = TextBoxSWVersion.Text;
                HiddenFieldUnitIP.Value = TextBoxUnitIp.Text;
                break;
            case "Clear":
                HiddenFieldClientId.Value = "0";
                HiddenFieldSerial.Value = "";
                HiddenFieldModelId.Value = "0";
                HiddenFieldDescription.Value = "";
                ObjectDataSource1.SelectParameters["description"].DefaultValue = "";
                ObjectDataSource1.SelectParameters["clientId"].DefaultValue = "0";
                ObjectDataSource1.SelectParameters["serial"].DefaultValue = "";
                ObjectDataSource1.SelectParameters["clientId"].DefaultValue = "0";
                ObjectDataSource1.SelectParameters["modelId"].DefaultValue = "0";
                ObjectDataSource1.SelectParameters["unitIp"].DefaultValue = "";
                ObjectDataSource1.SelectParameters["cameraIp"].DefaultValue = "";
                ObjectDataSource1.SelectParameters["swVersion"].DefaultValue = "";
                break;
        }
    }

    //Assigns the right controls to global controls variables
    public void SetInputControls(GridViewRow gridViewRowData)
    {
        DropDownListClient = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListClient"));
        DropDownListModel = (PolytexControls.DropDownList)(gridViewRowData.FindControl("DropDownListModel"));
        TextBoxSerial = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxSerial"));
        TextBoxDescription = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxDescription"));
        territoryId = getTerritoryIdByClient(Util.ValidateInt(DropDownListClient.AspDropDownList.SelectedValue.ToString(), 0));
        TextBoxUnitIp = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxUnitIP"));
        TextBoxCameraIp = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxCameraIP"));
        TextBoxSWVersion = (PolytexControls.TextBox)(gridViewRowData.FindControl("TextBoxSWVersion"));
    }

    protected string GetTerritoryName(object id)
    {
        if ((id != null) && (id != DBNull.Value))
        {
            DataRow row = UniStr.Util.FindRow(ApplicationData.SystemUsersRoles, "ID", id);
            if (row != null)
            {
                return _T[(string)row["UniTag"]];
            }
        }
        return "";
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

    protected void CustomValidatorLoginSerialExists(object source, ServerValidateEventArgs args)
    {
        string serial = ((PolytexControls.TextBox)GridView1.FooterRow.Cells[1].FindControl("TextBoxSerial")).Text;
        if (serial == "ADMIN")
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = PolyUtils.DbObjectNameIsNew(@"""SERIAL""", "UNITS", serial);
        }
    }

    protected void CustomValidatorLoginSerialUnique(object source, ServerValidateEventArgs args)
    {
        if (systemUserId > 0)
        {
            string serial = ((PolytexControls.TextBox)(GridView1.Rows[GridView1.EditIndex].FindControl("TextBoxSerial"))).Text;
            if (serial == "ADMIN")
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = PolyUtils.DbObjectNameIsUnique(@"""SERIAL""", "UNITS", serial, systemUserId);
            }
            
        }
        else
        {
            args.IsValid = false;
        }
    }

    public static int getTerritoryIdByClient(int client)
    {
        DataTable dt = PolytexData.Manage_Clients.SelectTerritoryByClient(client);
        return Util.ValidateInt(dt.Rows[0][0].ToString(), 0);
    }

    #endregion

}
