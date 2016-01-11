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
/// Specific handling for UserControl of export report
/// </summary>
public class PolyExportReport : PolyUserControl
{
    private int repeaterRowCount = 1;

    public PolyExportReport()
	{

	}


    public void FreezeRightPane(int sideColumnsFreeze)
    {
        PlaceHolder worksheetOptions = (PlaceHolder)(this.Page.FindControl("PlaceHolderWorksheetOptions"));

        string panesXml = @"
            <Selected/>
            <DisplayRightToLeft/>
            <DoNotDisplayZeros/>
            <FreezePanes/>
            <FrozenNoSplit/>
            <SplitHorizontal>1</SplitHorizontal>
            <TopRowBottomPane>1</TopRowBottomPane>
            <SplitVertical>" + sideColumnsFreeze.ToString() + @"</SplitVertical>
            <LeftColumnRightPane>" + sideColumnsFreeze.ToString() + @"</LeftColumnRightPane>
            <ActivePane>0</ActivePane>
            <Panes>
                <Pane>
                    <Number>3</Number>
                </Pane>
                <Pane>
                    <Number>1</Number>
                </Pane>
                <Pane>
                    <Number>2</Number>
                </Pane>
                <Pane>
                    <Number>0</Number>
                </Pane>
            </Panes>
            <ProtectObjects>False</ProtectObjects>
            <ProtectScenarios>False</ProtectScenarios>
            <x:AutoFitWidth>True</x:AutoFitWidth>";
        worksheetOptions.Controls.Clear();
        worksheetOptions.Controls.Add(new LiteralControl(panesXml));
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        try
        {
            PlaceHolder placeholderDescription = (PlaceHolder)(this.Page.FindControl("PlaceholderDescription"));

            ObjectDataSource objectDataSource = (ObjectDataSource)(this.FindControl("ObjectDataSource1"));

            string strQuery = "";

            foreach (Parameter parameter in objectDataSource.SelectParameters)
            {
                strQuery += parameter.Name + ": " + Server.HtmlEncode(parameter.DefaultValue) + "&#13;&#10;";
            }

            placeholderDescription.Controls.Add(new LiteralControl(strQuery));

        }
        catch
        { }

    }
  

    public static string ExcelEvalDate(string fieldValue)
    {
        string strExcelDate = "<Cell ss:StyleID=\"s21\"/>";
        if (fieldValue != "")
        {//yyyy-MM-ddTHH:mm:sss
            try
            {
                DateTime tempDateTime = Convert.ToDateTime(fieldValue);

                strExcelDate = "<Cell ss:StyleID=\"s23\"><Data ss:Type=\"DateTime\">";
                strExcelDate += FormatToExcelDate(tempDateTime);
                strExcelDate += "</Data></Cell>";
            }
            catch (Exception)
            {
                
            }
        }
        //return "";
        return strExcelDate;
    }


    public static string ExcelEvalDateTime(string fieldValue)
    {
        string strExcelDate = "<Cell ss:StyleID=\"s21\"/>";
        if (fieldValue != "")
        {//yyyy-MM-ddTHH:mm:sss
            try
            {
                DateTime tempDateTime = Convert.ToDateTime(fieldValue);

                strExcelDate = "<Cell ss:StyleID=\"s24\"><Data ss:Type=\"DateTime\">";
                strExcelDate += FormatToExcelDate(tempDateTime);
                strExcelDate += "</Data></Cell>";
            }
            catch (Exception)
            {
            }
        }

        return strExcelDate;
    }


    public static string FormatToExcelDate(DateTime theDate)
    {
        string exYear = theDate.Year.ToString();
        string exMonth = theDate.Month.ToString();
        string exDay = theDate.Day.ToString();
        string exHour = theDate.Hour.ToString();
        string exMinute = theDate.Minute.ToString();
        string exSecond = theDate.Second.ToString();

        try
        {
            if (exYear.Substring(0, 2) != "19" && exYear.Substring(0, 2) != "20" && exYear.Substring(0, 2) != "21")
            {//Invalid year
                int shortYear;
                if (Int32.TryParse(exYear.Substring(2, 2), out shortYear))
                {
                    if (shortYear > 20)
                    {
                        exYear = "19" + shortYear.ToString();
                    }
                    else
                    {
                        if (shortYear < 10)
                        {
                            exYear = "200" + shortYear.ToString();
                        }
                        else
                        {
                            exYear = "20" + shortYear.ToString();
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            exYear = "2000";
        }
        exMonth = (exMonth.Length == 1 ? "0" + exMonth : exMonth);
        exDay = (exDay.Length == 1 ? "0" + exDay : exDay);
        exHour = (exHour.Length == 1 ? "0" + exHour : exHour);
        exMinute = (exMinute.Length == 1 ? "0" + exMinute : exMinute);
        exSecond = (exSecond.Length == 1 ? "0" + exSecond : exSecond);

        return exYear + "-" + exMonth + "-" + exDay + "T" + exHour + ":" + exMinute + ":" + exSecond;
    }


    public string RepeaterRowCounter
    {
        get
        {
            return (repeaterRowCount++).ToString();
        }
    }


    public static bool IsSendReport
    {
        get
        {
            if (PolyUtils.RequestFormByContainedKey("FromDateTime") == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
