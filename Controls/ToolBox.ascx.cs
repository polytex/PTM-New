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

public partial class Controls_ToolBox : System.Web.UI.UserControl
{        
    protected void Page_Load(object sender, EventArgs e)
    {                        
        if (((PolyPage)Page).CurrentPageType == PolyPage.PageType.Report)
        {
            PrintReport.Visible = true;
            ReportQuery.Visible = true;

            ExportExcel.Visible = (PolyReport.DisplayThumbview ? false : true);
        }
        else if (((PolyPage)Page).CurrentPageType == PolyPage.PageType.Popup)
        {
            PrintPage.Visible = false;
        }
        else
        {
            PrintPage.Visible = true;

        }

        switch (PolyUtils.CurrentPage)
        {
            case "Default":     //Login page
                this.Visible = false;
                break;
            case "Activities":
                AddActivity.Visible = true;
                break;
            case "Territories":
                ExportExcel.Visible = true;
                break;
            case "SystemUsers":
                ExportExcel.Visible = true;
                break;
            case "Models":
                ExportExcel.Visible = true;
                break;
            case "ActivityGroups":
                ExportExcel.Visible = true;
                break;
            case "Units":
                ExportExcel.Visible = true;
                break;
            case "Clients":
                ExportExcel.Visible = true;
                break;
            default:
                //this.Visible = false;
                break;
        }

        if (PolyPage.IsMobile)
        {

            ExportExcel.Visible = false;
            PrintReport.Visible = false;
            PrintPage.Visible = false;
            SideMenu.Visible = true;
            AddActivity.Visible = true;

            if (!ReportQuery.Visible)
            {
                EmptyControl.Visible = true;
            }
        }
        
        
    }
}
