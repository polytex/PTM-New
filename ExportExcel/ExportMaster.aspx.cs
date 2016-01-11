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

using UniStr;

public partial class ExportExcel_ExportMaster : PolyReport
{
    public string CultureDateFormat = "";
    public string CultureTimeFormat = "";

    private string reportName;

    protected void Page_Init(object sender, EventArgs e)
    {        
        CurrentPageType = PageType.ExportExcel;
        AuthenticationRequired = false;
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {        
        Response.BufferOutput = true;
        Response.AddHeader("Content-Disposition", "attachment;filename=" + GetFileName());
        Response.ContentType = "application/vnd.ms-excel";

        CultureDateFormat = UniLang.getCultureInfo(UniLang.DefaultUILanguage).DateTimeFormat.ShortDatePattern;
        CultureTimeFormat = UniLang.getCultureInfo(UniLang.DefaultUILanguage).DateTimeFormat.LongTimePattern.Replace(" tt", " AM/PM");      
        Control userControlExportReport = LoadControl(ReportName + ".ascx");
        
        PlaceHolderTable.Controls.Add(userControlExportReport);                
    }

    public string ReportName
    {
        get
        {
            if (String.IsNullOrEmpty(reportName))
            {
                reportName = PolyUtils.RequestFormOrQuerystring("ReportName");
            }
            return reportName;
        }
    }

    public string GetFileName()
    {
        string fileName = "";
        switch (ReportName)    
        {
            case "ActivityGroups":
                fileName = "Activity Groups";
                break;
            case "Clients":
                fileName = "Sites";
                break;
            case "Models":
                fileName = "Models";
                break;
            case "ReportActivities":
                fileName = "Activities";
                break;
            case "ReportActivityGroup":
                fileName = "Activity Groups";
                break;
            case "ReportActivityType":
                fileName = "Activity Type";
                break;
            case "ReportClients":
                fileName = "Sites";
                break;
            case "ReportTechnicians.ascx":
                fileName = "Technicians";
                break;
            case "ReportUnits":
                fileName = "Stations";
                break;
            case "SystemUsers":
                fileName = "Users";
                break;
            case "Territories":
                fileName = "Territories";
                break;
            case "Units":
                fileName = "Stations";
                break;
        }
        DateTime now = DateTime.Now;
        fileName += " " + now.Year + "-" + now.Month + "-" + now.Day;
        fileName += ".xls";

        return fileName;
    }
}
