﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;

using UniStr;
using PolytexControls;
using PolytexObjects;

public partial class ActivityDetails : PolyPage
{
    #region VARIABLES

    private bool isMobile = false;
    private string Path = "";

    private int activityId;
    private int systemuserId;
    private string[] filePaths;
    public string clientList = "";
    public string clientListId = "";

    #endregion

    #region METHODS

    protected void Page_Load(object sender, EventArgs e)
    {
        string sUA = Request.UserAgent.Trim().ToLower();
        if (sUA.Contains("android"))
            isMobile = true;

        clientList = ApplicationData.UpdateClientList(CurrentUser.TerritoryId)[0];
        clientListId = ApplicationData.UpdateClientList(CurrentUser.TerritoryId)[1];
        string root = HttpContext.Current.Request.PhysicalApplicationPath;
        this.Path = root + "ActivityImages\\"; 
        systemuserId = CurrentUser.SystemUserId;
        ObjectDataSourceClient.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
        ObjectDataSourceUnit.SelectParameters["territoryId"].DefaultValue = CurrentUser.TerritoryId.ToString();
        activityId =  Util.ValidateInt(RequestQuerystring("activityId"), 0);

        CustomValidatorDate.ErrorMessage = _T["Date_End_Greater_Then_Date_Start"];
        CustomValidatorRecommendation.ErrorMessage = _T["ItemRequired"];
        CustomValidatorSolution.ErrorMessage = _T["ItemRequired"];
        CustomValidatorDescription.ErrorMessage = _T["ItemRequired"];
        CustomValidatorImageSize.ErrorMessage = _T["Max_File_Size"];
        CustomValidatorCheckClient.ErrorMessage = "Client not exist";
        RegularExpressionValidatorDescription.ErrorMessage = _T["Max_Length_400"];
        RegularExpressionValidatorParts.ErrorMessage = _T["Max_Length_200"];
        RegularExpressionValidatorSolution.ErrorMessage = _T["Max_Length_200"];
        RegularExpressionValidatorRecommendations.ErrorMessage = _T["Max_Length_200"];

        #region Dynamic DropDownList for Clients and Activity Groups

        TextBoxClient.TextChanged += new EventHandler(TextBoxClient_TextBoxChanged);
        TextBoxClient.AutoPostBack = true;
        DropDownListActivityGroup.AspDropDownList.SelectedIndexChanged += new EventHandler(DropDownListActivityGroup_SelectedIndexChanged);
        DropDownListActivityGroup.AspDropDownList.AutoPostBack = true;

        #endregion

        if (!IsPostBack)
        {
            DropDownListStartTime.AspDropDownList.DataSource = ApplicationData.GetTimeOptions();
            DropDownListStartTime.AspDropDownList.DataBind();
            DropDownListTimeEnd.AspDropDownList.DataSource = ApplicationData.GetTimeOptions();
            DropDownListTimeEnd.AspDropDownList.DataBind();
            DropDownListDriveTime.AspDropDownList.DataSource = ApplicationData.GetDriveTimeOptions();
            DropDownListDriveTime.AspDropDownList.DataBind();

            if (activityId > 0)
            {
                
                //Update fields to existing activity
                PolytexObjects.Activity activity = new PolytexObjects.Activity(activityId);
                ObjectDataSourceActivityTypes.SelectParameters["ActivityGroupId"].DefaultValue = activity.ActivityGroupId.ToString();
                DropDownListActivityGroup.AspDropDownList.SelectedValue = activity.ActivityGroupId.ToString();
                DropDownListActivityType.AspDropDownList.SelectedValue = activity.ActivityTypeId.ToString();
                ObjectDataSourceUnit.SelectParameters["clientId"].DefaultValue = activity.ClientId.ToString();
                DropDownListUnit.AspDropDownList.SelectedValue = activity.UnitId.ToString();
                TextBoxClient.Text = ApplicationData.GetClientNameById(activity.ClientId);
                HiddenFieldClientId.Value = activity.ClientId.ToString();
                TextBoxDescription.Text = activity.Description;
                TextBoxParts.Text = activity.Parts;
                TextBoxSolution.Text = activity.Solution;
                TextBoxRecommendations.Text = activity.Recommendation;
                textboxActivityStart.Text = activity.DateStart;
                DropDownListStartTime.AspDropDownList.SelectedValue = activity.HourStart;
                TextBoxActivityEnd.Text = activity.DateEnd;
                DropDownListTimeEnd.AspDropDownList.SelectedValue = activity.HourEnd;
                DropDownListDriveTime.AspDropDownList.SelectedValue = activity.DriveTime.ToString();

                string rootPath = HttpContext.Current.Request.PhysicalApplicationPath;
                string[] files = Directory.GetFiles(rootPath+"ActivityImages\\", activityId + "_1.*");

                if (files.Length > 0)
                {
                    // Getting the extension of the file
                    string[] extension = files[0].Split('.');
                    ImgPreview.ImageUrl = "ActivityImages/" + activityId + "_1." + extension[extension.Length - 1];
                }
                else
                {
                    ImgPreview.Visible = false;
                }
            }
            else
            {
               textboxActivityStart.Text = DateTime.Now.ToShortDateString();
               TextBoxActivityEnd.Text = DateTime.Now.ToShortDateString();
               ImgPreview.Visible = false;
            }
        }

        
    }

    protected void DropDownListActivityGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.DropDownList dropDownList = (System.Web.UI.WebControls.DropDownList)(sender);

        int activityGroupId = Util.ValidateInt(dropDownList.SelectedValue, 0);

        if (activityGroupId > 0)
        {
            ObjectDataSourceActivityTypes.SelectParameters["ActivityGroupId"].DefaultValue = activityGroupId.ToString();
        }
    }

    protected void TextBoxClient_TextBoxChanged(object sender, EventArgs e)
    {
        ObjectDataSourceUnit.SelectParameters["clientId"].DefaultValue = Util.ValidateInt(HiddenFieldClientId.Value, 0).ToString();
    }

    public void onButtonClick(object sender, EventArgs e)
    {
        Page.Validate("GridEditItem");
        if (Page.IsValid)
        {
            PolytexObjects.Activity activity = new Activity();
            activity.Id = activityId;
            activity.SystemUserId = CurrentUser.SystemUserId;
            activity.ClientId = Util.ValidateInt(HiddenFieldClientId.Value, 0);
            activity.TerritoryId = Activity.getTerritoryIdByClient(activity.ClientId);
            activity.UnitId = Util.ValidateInt(DropDownListUnit.AspDropDownList.SelectedValue, 0);
            activity.ActivityGroupId = Util.ValidateInt(DropDownListActivityGroup.AspDropDownList.SelectedValue, 0);
            activity.ActivityTypeId = Util.ValidateInt(DropDownListActivityType.AspDropDownList.SelectedValue, 0);
            activity.Description = TextBoxDescription.Text.Trim();
            activity.Parts = TextBoxParts.Text.Trim();
            activity.Solution = TextBoxSolution.Text.Trim();
            activity.Recommendation = TextBoxRecommendations.Text.Trim();
            string dateStart = textboxActivityStart.Text + " " + DropDownListStartTime.AspDropDownList.SelectedValue + ":00";
            activity.DtDateStart = DateTime.Parse(dateStart);
            string dateEnd = TextBoxActivityEnd.Text + " " + DropDownListTimeEnd.AspDropDownList.SelectedValue + ":00";
            activity.DtDateEnd = DateTime.Parse(dateEnd);
            activity.DriveTime = Util.ValidateInt(DropDownListDriveTime.AspDropDownList.SelectedValue, 0);

            PolytexControls.Button btn = (PolytexControls.Button)sender;
            string id = btn.ID;

            if (activityId > 0)
            {
                activity.update();
                UploadFile();
                if (id.Equals("ButtonSaveActivity"))
                {
                    SetActionMessage(ActionMessage.UpdatedSuccessfuly);
                }
                else
                {
                    Response.Redirect("Activities.aspx", true);
                }

            }
            else
            {
                activityId = activity.insert();
                UploadFile();
                if (id.Equals("ButtonSaveActivity"))
                {
                    SetActionMessage(ActionMessage.InsertedSuccessfuly);
                }
                else
                {
                    Response.Redirect("Activities.aspx", true);
                }
            }
        }

    }

    public void ClearFields()
    {
        activityId = 0;
        TextBoxClient.Text = "";
        ObjectDataSourceActivityTypes.SelectParameters["ActivityGroupId"].DefaultValue = "";
        DropDownListActivityGroup.AspDropDownList.SelectedValue = "";
        DropDownListActivityType.AspDropDownList.SelectedValue = "";
        DropDownListUnit.AspDropDownList.SelectedValue = "";
        TextBoxDescription.Text = "";
        TextBoxParts.Text = "";
        TextBoxSolution.Text = "";
        TextBoxRecommendations.Text = "";
        textboxActivityStart.Text = "";
        DropDownListStartTime.AspDropDownList.SelectedValue = "";
        TextBoxActivityEnd.Text = "";
        DropDownListTimeEnd.AspDropDownList.SelectedValue = "";
        DropDownListDriveTime.AspDropDownList.SelectedValue = "";
        ImgPreview.Visible = false;
        FileUploadImage.Dispose();
    }

    protected void CustomValidator_FileSizeValidate(object source, ServerValidateEventArgs args)
    {
        string[] extension = FileUploadImage.PostedFile.ContentType.Split('/');
        if (extension[0].Equals("image"))
        {
            if (FileUploadImage.FileBytes.Length > 1024 * 1024 * 8)
            {
                CustomValidatorImageSize.ErrorMessage = _T["Max_File_Size"];
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }

        }
        else
        {
            CustomValidatorImageSize.ErrorMessage = _T["Only_image_files_are_allowed"];
            args.IsValid = false;
        }

    }

    protected void CustomValidator_CheckDate(object source, ServerValidateEventArgs args)
    {
        string dateStart;
        string dateEnd;
        DateTime dStart;
        DateTime dEnd;

        if (textboxActivityStart.Text != "" && TextBoxActivityEnd.Text != "")
        {
            dateStart = textboxActivityStart.Text + " " + DropDownListStartTime.AspDropDownList.SelectedValue + ":00";
            dStart = DateTime.Parse(dateStart);
            dateEnd = TextBoxActivityEnd.Text + " " + DropDownListTimeEnd.AspDropDownList.SelectedValue + ":00";
            dEnd = DateTime.Parse(dateEnd);
            if (dStart > DateTime.Now.AddDays(1) || dEnd > DateTime.Now.AddDays(1))
            {
                if (dStart > DateTime.Now.AddDays(1))
                {
                    CustomValidatorDate.ErrorMessage = _T["Date_Start_Greater_Then_Today"];
                }
                else
                {
                    CustomValidatorDate.ErrorMessage = _T["Date_End_Greater_Then_Today"];
                }
                
                args.IsValid = false;
            }
            else if (dStart <= dEnd)
            {
                args.IsValid = true;
            }
            else
            {
                CustomValidatorDate.ErrorMessage = _T["Date_End_Greater_Then_Date_Start"];
                args.IsValid = false;
            }
        }

        else 
        {
            CustomValidatorDate.ErrorMessage = _T["Date_End_Greater_Then_Date_Start"];
            args.IsValid = false;
        }
    }

    protected void CustomValidator_CheckClientExist(object source, ServerValidateEventArgs args)
    {
        string name = TextBoxClient.Text;
        if (ApplicationData.GetClientIdByName(name)>0)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }
    
    public void UploadFile()
    {
        string[] extension = FileUploadImage.PostedFile.ContentType.Split('/');
        if (FileUploadImage.HasFile)
        {
            try
            {
                FileUploadImage.SaveAs(Path + activityId + "_1."+extension[1]);
                // Show image in Image Preview
                ImgPreview.ImageUrl = "ActivityImages/" + activityId + "_1."+extension[1];
                ImgPreview.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
    }

    #endregion

}
