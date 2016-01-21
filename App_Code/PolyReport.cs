using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for PolyReport
/// </summary>
public class PolyReport : PolyPage
{
    #region VARIABLES

    public static string orderImageWidth = "150";
    public static string orderImageHeight = "139";
    public static int imagesOnThumbviewRow = 4;

    private int skip = 0;
    private bool hasNextSkip = false;

    private static bool displayTwinOrders = false;
    private DateTime fromDateTime = new DateTime();
    private DateTime toDateTime = new DateTime();

    private PolytexControls.ObjectDataSource reportObjectDataSource;
    private GridView reportGridView;
    private Label reportLabelRecords;
    private Label reportCurrentPage;
    private Label reportPageCount;
    private HtmlTable htmlTableReport;

    private bool printSmallFonts = false;

    #endregion

    #region CONSTRUCTORS

    public PolyReport()
	{

    }

    #endregion

    #region METHODS

    public static DateTime GetFromDateTime()
    {
        return Convert.ToDateTime(PolyUtils.RequestFormOrQuerystringByContainedKey("FromDateTime"));        
    }


    public static DateTime GetToDateTime()
    {
        return Convert.ToDateTime(PolyUtils.RequestFormOrQuerystringByContainedKey("ToDateTime"));    
    }


    public string GetReportsGlobalInit()
    {
        int popupWidth, popupHeight;
        SetReportQuerySize(CurrentPage, out popupWidth, out popupHeight);

        string str = "";

        str = @"<script type=""text/javascript"">" + Environment.NewLine;
        str += @"var ReportPageSize = " + PolyConfig.ReportPageSize + ";" + Environment.NewLine;
        try
        {
            str += @"var objTableGridView;" + Environment.NewLine;
        }
        catch
        { }

        try
        {
            str += @"var objReportHeader;" + Environment.NewLine;
        }
        catch
        { }

        str += @"function " + CurrentPage + "OnLoad(){" + Environment.NewLine;
        if (!ShowReport)
        {
            str += "OpenReportQuery();" + Environment.NewLine;
        }
        else if (IsPrintingLayout && !IsForMailingList)
        {//DHTML fnc for printing page            
            str += @"objTableGridView = document.getElementById(""" + ReportGridView.ClientID + @""");" + Environment.NewLine;
            str += @"objReportHeader = document.getElementById(""" + HtmlTableReport.Rows[0].Cells[0].ClientID + @""");" + Environment.NewLine;
            str += "PreparePrintingLayout();" + Environment.NewLine;
        }
        str += "}" + Environment.NewLine + Environment.NewLine;

        str += "function OpenReportQuery()" + Environment.NewLine;
        str += @"{" + Environment.NewLine;

        str += @"var objSpanHiddens = document.getElementById(""spanHiddenFieldsForExport"");" + Environment.NewLine;
        str += @"var strQuerystring = """"" + Environment.NewLine;
        str += @"for (i = 0; i < objSpanHiddens.childNodes.length; i++)" + Environment.NewLine;
        str += @"{" + Environment.NewLine;
        str += @"if (objSpanHiddens.childNodes[i].tagName == ""INPUT"")" + Environment.NewLine;
        str += @"{" + Environment.NewLine;
        str += @"strQuerystring += objSpanHiddens.childNodes[i].id + ""="" + objSpanHiddens.childNodes[i].value + ""&"";" + Environment.NewLine;
        str += @"}" + Environment.NewLine;
        str += @"} " + Environment.NewLine;
        str += @"OpenPopup(""" + CurrentPage + @".aspx?"" + strQuerystring, " + popupWidth.ToString() + ", " + popupHeight.ToString() + ", false)};" + Environment.NewLine;
        str += @"</script>" + Environment.NewLine;

        if (!IsPrintingLayout)
        {//Added to interface report, not report to be printed
            str += @"<iframe id=""PrintFrame"" name=""PrintFrame"" width=""0"" height=""0"" style=""visibility:hidden;width:0px;height:0px;margin:0px;padding:0px;position:absolute;top:0px;left:0px;""></iframe>";
        }
        else
        {
            str += @"<div id=""PagesToPrintContainer""></div>";
        }

        return str;
    }


    public static void SetReportQuerySize(string reportName, out int popupWidth, out int popupHeight)
    {
        popupWidth = 780;
        popupHeight = 430;

        if (PolyPage.IsMobile)
        {
            popupWidth = 280;

            switch (reportName)
            {
                case "ReportActivities":
                    popupHeight = 430;
                    break;
                case "ReportClients":
                    popupHeight = 240;
                    break;
                case "ReportActivityGroup":
                    popupHeight = 230;
                    break;
                case "ReportActivityType":
                    popupHeight = 250;
                    break;
                case "ReportUnits":
                    popupHeight = 260;
                    break;
                case "ReportTechnicians":
                    popupHeight = 230;
                    break;
            }
        }
        else
        {
            switch (reportName)
            {
                case "ReportActivities":
                    popupWidth = 550;
                    popupHeight = 420;
                    break;
                case "ReportClients":
                    popupWidth = 400;
                    popupHeight = 300;
                    break;
                case "ReportActivityGroup":
                    popupWidth = 350;
                    popupHeight = 300;
                    break;
                case "ReportActivityType":
                    popupWidth = 400;
                    popupHeight = 300;
                    break;
                case "ReportUnits":
                    popupWidth = 400;
                    popupHeight = 300;
                    break;
                case "ReportTechnicians":
                    popupWidth = 400;
                    popupHeight = 300;
                    break;
            }
        }


    }


    protected void ObjectDataSource1_Init(object sender, EventArgs e)
    {
        ReportObjectDataSource = (PolytexControls.ObjectDataSource)(sender);
    }


    protected void LabelRecords_Init(object sender, EventArgs e)
    {
        ReportLabelRecords = (Label)(sender);
    }


    protected void TableReport_Init(object sender, EventArgs e)
    {
        HtmlTableReport = (HtmlTable)(sender);
    }


    protected void LabelCurrentPage_Init(object sender, EventArgs e)
    {
        ReportCurrentPage = (Label)(sender);
    }


    protected void LabelPageCount_Init(object sender, EventArgs e)
    {
        ReportPageCount = (Label)(sender);

        ReportPageCount.PreRender += new EventHandler(LabelPageCount_PreRender);
    }


    protected void LabelPageCount_PreRender(object sender, EventArgs e)
    {
        //ReportPageCount.Text = ReportGridView.PageCount.ToString();   
    }


    /// <summary>
    /// Handle data cropping on report printing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PolytexGridDataLabel_PreRender(object sender, EventArgs e)
    {
        if (IsPrintingLayout)
        {
            PolytexControls.GridDataLabel label = ((PolytexControls.GridDataLabel)sender);

            if (label.ToolTip.Length > label.MaxLength)
            {
                label.Font.Size = FontUnit.Parse("8");  //Smaller font
                label.Text = label.ToolTip;
                label.MaxLength = 0;
            }

            if (PrintSmallFonts)
            {
                label.Font.Size = FontUnit.Parse("8");  //Smaller font                
            }
        }
    }


    /// <summary>
    /// Initiates GridView global Polytex Manager properties
    /// </summary>
    protected new void GridViewOnInit(object sender, EventArgs e)
    {
        ReportGridView = (GridView)sender;
        ReportGridView.PageSize = PolyConfig.ReportPageSize;

        PolyGridViewUtils.GridViewOnInit((GridView)sender);

        ReportGridView.PreRender += new EventHandler(ReportGridView_PreRender);
    }


    protected void ReportGridView_PreRender(object sender, EventArgs e)
    {
        if (IsForMailingList)
        {
            ReportPageCount.Text = "1";
        }
        else if (IsPrintingLayout)
        {
            ReportPageCount.Text = (Math.Ceiling((decimal)((GridView)sender).Rows.Count / (decimal)(PolyConfig.ReportPageSize))).ToString();

            if (ReportPageCount.Text == "0")
            {
                ReportPageCount.Text = "1";
            }
        }
        else
        {
            ReportPageCount.Text = ((GridView)sender).PageCount.ToString();
        }
    }


    /// <summary>
    /// Sets row serial number for label in first column 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((Label)e.Row.Cells[0].Controls[1]).Text = (e.Row.DataItemIndex + 1).ToString();
            
            if (PrintSmallFonts)
            {
                ((Label)e.Row.Cells[0].Controls[1]).Font.Size = FontUnit.Parse("8");
            }

            //if (DisplayTwinOrders)
            //{                                
            //    try
            //    {
            //        if (Convert.ToInt64(ReportObjectDataSource.ObjectDataSourceDataTable.Rows[e.Row.DataItemIndex]["TWIN_ID"]) > 0)
            //        {
            //            e.Row.CssClass = "TwinRow";
            //        }
            //    }
            //    catch
            //    {
            //    }                
            //}
        }
    }


    /// <summary>
    /// Limit ObjectDataSource to (MaxReportResults + 1) and format ReportLabelRecords
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_DataBinding(object sender, EventArgs e)
    {
        if (ReportObjectDataSource.ObjectDataSourceDataTable.Columns["ID"] == null)
        {
            ReportObjectDataSource.ObjectDataSourceDataTable.Columns.Add(new DataColumn("ID"));
            ReportGridView.AllowPaging = false;
        }

        if (ReportObjectDataSource.ObjectDataSourceDataTable.Rows.Count > PolytexData.ReportsTasks.MaxReportResults && !IsForMailingList)
        {//ObjectDataSource may have MaxReportResults + 1 (so to know that results were limited)
            HasNextSkip = true;
            ReportObjectDataSource.ObjectDataSourceDataTable.Rows.RemoveAt(ReportObjectDataSource.ObjectDataSourceDataTable.Rows.Count - 1);
            ReportLabelRecords.Text = _T["General_Report_Results_Limited"].Replace("%max%", PolytexData.ReportsTasks.MaxReportResults.ToString());
            ReportLabelRecords.CssClass = "LabelRecordsLimited";
        }
        else
        {
            if (ReportObjectDataSource.IsEmpty)
            {
                ReportLabelRecords.Text = "0 " + _T["Results"];
            }
            else
            {
                ReportLabelRecords.Text = ReportObjectDataSource.ObjectDataSourceDataTable.Rows.Count.ToString() + " " + _T["Results"];
            }
            
            ReportLabelRecords.CssClass = "LabelRecords";
        }

        if (IsForMailingList)
        {//One page
            ReportCurrentPage.Text = "1";
            ReportPageCount.Text = "1";

            if (ReportObjectDataSource.SelectParameters["hasMaxResults"] != null)
            {
                ReportObjectDataSource.SelectParameters["hasMaxResults"].DefaultValue = "false";
            }
        }
        else if (IsPrintingLayout)
        {
            ReportCurrentPage.Text = "ReportCurrentPage.Text";     //To be replaced on print rendering
            ReportPageCount.Text = "ReportPageCount.Text";
        }
        else
        {
            ReportCurrentPage.Text = (ReportGridView.PageIndex + 1).ToString();
        }
    }

#endregion

    #region PROPERTIES

    /// <summary>
    /// Reference to report's ObjectDataSource
    /// </summary>
    public PolytexControls.ObjectDataSource ReportObjectDataSource
    {
        get
        {
            return reportObjectDataSource;
        }
        set
        {
            reportObjectDataSource = value;
        }
    }


    /// <summary>
    /// Reference to report GridView
    /// </summary>
    public GridView ReportGridView
    {
        get
        {
            return reportGridView;
        }
        set
        {
            reportGridView = value;
        }
    }


    /// <summary>
    /// Reference to report records count Label in report header
    /// </summary>
    public Label ReportLabelRecords
    {
        get
        {
            return reportLabelRecords;
        }
        set
        {
            reportLabelRecords = value;
        }
    }


    public HtmlTable HtmlTableReport
    {
        get
        {
            return htmlTableReport;
        }
        set
        {
            htmlTableReport = value;
        }
    }


    /// <summary>
    /// Reference to current report displayed page label 
    /// </summary>
    public Label ReportCurrentPage
    {
        get
        {
            return reportCurrentPage;
        }
        set
        {
            reportCurrentPage = value;
        }
    }
    

        /// <summary>
    /// Reference to current report displayed page label 
    /// </summary>
    public Label ReportPageCount
    {
        get
        {
            return reportPageCount;
        }
        set
        {
            reportPageCount = value;
        }
    }

    
    /// <summary>
    /// Report's query from date time value
    /// </summary>
    public DateTime FromDateTime
    {
        get
        {
            try
            {
                if (IsNewDateTime(fromDateTime))
                {
                    if (IsForMailingList)
                    {
                        fromDateTime = Convert.ToDateTime(PolyUtils.RequestQuerystring("FromDateTime"));
                    }
                    else
                    {
                        string strFromDate = PolyUtils.RequestFormByContainedKey("TextBoxFromDate");
                        string strFromTime = PolyUtils.RequestFormByContainedKey("TextBoxFromTime");
                        fromDateTime = Convert.ToDateTime(strFromDate + " " + strFromTime);
                    }
                }

                return fromDateTime;
            }
            catch
            {
                return new DateTime();
            }
        }
        set
        {
            fromDateTime = value;
        }
    }


    /// <summary>
    /// Report's query to date time value
    /// </summary>
    public DateTime ToDateTime
    {
        get
        {
            try
            {
                if (IsNewDateTime(toDateTime))
                {
                    if (IsForMailingList)
                    {
                        toDateTime = Convert.ToDateTime(PolyUtils.RequestQuerystring("ToDateTime"));
                    }
                    else
                    {
                        string strFromDate = PolyUtils.RequestFormByContainedKey("TextBoxToDate");
                        string strFromTime = PolyUtils.RequestFormByContainedKey("TextBoxToTime");
                        strFromTime = (strFromTime == "23:59" ? "23:59:59" : strFromTime);
                        toDateTime = Convert.ToDateTime(strFromDate + " " + strFromTime);
                    }
                }

                return toDateTime;
            }
            catch
            {
                return new DateTime();
            }
        }
        set
        {
            toDateTime = value;
        }
    }


    /// <summary>
    /// Results skip
    /// </summary>
    public int Skip
    {
        get
        {
            return UniStr.Util.ValidateInt(PolyUtils.RequestFormByContainedKey("Skip"), 0); ;
        }
    }


    public bool HasNextSkip
    {
        get
        {
            return hasNextSkip;
        }
        set
        {
            hasNextSkip = value;
        }
    }


    public static string ExcelWorksheetDirection
    {
        get
        {
            if (PolyConfig.CultureDirection == "rtl")
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
    } 


    public static string OrderImageWidth
    {
        get
        {
            return orderImageWidth;
        }
    }


    public static string OrderImageHeight
    {
        get
        {
            return orderImageHeight;
        }
    }


    public static bool DisplayThumbview
    {
        get
        {
            return (PolyUtils.RequestFormByContainedKey("CheckBoxThumbview") == "on" ? true : false);
        }
    }


    /// <summary>
    /// Layout Polytex report for printing
    /// </summary>
    public static bool IsPrintingLayout
    {
        get
        {
            try
            {
                return (PolyUtils.RequestFormOrQuerystringByContainedKey("PrintingLayout") == "1" ? true : false);
            }
            catch
            {
                return false;
            }
        }
    }


    /// <summary>
    /// Send HTML as attachement by email
    /// </summary>
    public static bool IsForMailingList
    {
        get
        { 
            return (PolyUtils.RequestQuerystring("ForMailingList") == "1");            
        }
    }
    

    /// <summary>
    /// For wide reports
    /// Need to set OnPreRender="PolytexGridDataLabel_PreRender" for all labels on report!
    /// </summary>
    public bool PrintSmallFonts
    {
        get
        {
            if (IsPrintingLayout)
            {
                return printSmallFonts;
            }
            else
            {
                return false;
            }
        }
        set
        {
            printSmallFonts = value;
        }
    }
    

    public static int ImagesOnThumbviewRow
    {
        get
        {
            return imagesOnThumbviewRow;
        }
    }


    /// <summary>
    /// Display twin return orders with a dotted line between them 
    /// </summary>
    public static bool DisplayTwinOrders
    {
        get
        {
            return displayTwinOrders;
        }
        set
        {
            displayTwinOrders = value;
        }
    }

    #endregion
}
