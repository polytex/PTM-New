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

/// <summary>
/// From/To Date & Time control for reports query popups
/// </summary>
public partial class Controls_QueryFromToDate : System.Web.UI.UserControl
{
    private short startTabIndex = 1;
    private bool writeTable = true;

    protected void Page_Init(object sender, EventArgs e)
    {
        TextBoxFromDate.TextBoxTabIndex = StartTabIndex;
        TextBoxFromTime.TextBoxTabIndex = StartTabIndex;
        TextBoxToDate.TextBoxTabIndex = StartTabIndex;
        TextBoxToTime.TextBoxTabIndex = StartTabIndex;

        if (PolyPage.IsMobile)
        {
            
            LabelFromDate.Style.Add(HtmlTextWriterStyle.Position, "absolute");
            LabelFromDate.Style.Add(HtmlTextWriterStyle.Top, "30px");
            LabelFromDate.Style.Add(HtmlTextWriterStyle.Left, "10px");
            TextBoxFromDate.Style.Add(HtmlTextWriterStyle.Position, "absolute");
            TextBoxFromDate.Style.Add(HtmlTextWriterStyle.Top, "30px");
            TextBoxFromDate.Style.Add(HtmlTextWriterStyle.Left, "100px");
            LabelToDate.Style.Add(HtmlTextWriterStyle.Position, "absolute");
            LabelToDate.Style.Add(HtmlTextWriterStyle.Left, "10px");
            LabelToDate.Style.Add(HtmlTextWriterStyle.Top, "60px");
            TextBoxToDate.Style.Add(HtmlTextWriterStyle.Position, "absolute");
            TextBoxToDate.Style.Add(HtmlTextWriterStyle.Top, "60px");
            TextBoxToDate.Style.Add(HtmlTextWriterStyle.Left, "100px");
            LabelFromTime.Style.Add(HtmlTextWriterStyle.Position, "absolute");
            LabelFromTime.Style.Add(HtmlTextWriterStyle.Top, "90px");
            LabelFromTime.Style.Add(HtmlTextWriterStyle.Left, "10px");
            TextBoxFromTime.Style.Add(HtmlTextWriterStyle.Position, "absolute");
            TextBoxFromTime.Style.Add(HtmlTextWriterStyle.Left, "100px");
            TextBoxFromTime.Style.Add(HtmlTextWriterStyle.Top, "90px");
            LabelToTime.Style.Add(HtmlTextWriterStyle.Position, "absolute");
            LabelToTime.Style.Add(HtmlTextWriterStyle.Top, "120px");
            LabelToTime.Style.Add(HtmlTextWriterStyle.Left, "10px");
            TextBoxToTime.Style.Add(HtmlTextWriterStyle.Position, "absolute");
            TextBoxToTime.Style.Add(HtmlTextWriterStyle.Top, "120px");
            TextBoxToTime.Style.Add(HtmlTextWriterStyle.Left, "100px");
            


        }

        
    }    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //Set values if in querystring        
        DateTime startDate;
        if (DateTime.TryParse(PolyUtils.RequestQuerystring("TextBoxFromDateTime"), out startDate))
        {
            if (startDate != new DateTime())
            {
                TextBoxFromDate.Text = startDate.ToShortDateString();
                if (startDate.Second > 0)
                {
                    TextBoxFromTime.Text = startDate.ToLongTimeString();
                }
                else
                {
                    TextBoxFromTime.Text = startDate.ToShortTimeString();
                }
            }
        }

        DateTime endDate;
        if (DateTime.TryParse(PolyUtils.RequestQuerystring("TextBoxToDateTime"), out endDate))
        {
            if (endDate != new DateTime())
            {                
                TextBoxToDate.Text = endDate.ToShortDateString();
                if (endDate.Second > 0)
                {
                    TextBoxToTime.Text = endDate.ToLongTimeString();
                }
                else
                {
                    TextBoxToTime.Text = endDate.ToShortTimeString();
                }
            }
        }
        
        
        TextBoxFromDate.Focus();
    }

    protected void TextBoxReportDates_PreRender(object sender, EventArgs e)
    {
        PolytexControls.TextBox textBoxDate = (PolytexControls.TextBox)sender;

        if (String.IsNullOrEmpty(textBoxDate.Text))
        {
            textBoxDate.Text = UniStr.Util.MakeShortDate(DateTime.Now);
        }
    }

    public short StartTabIndex
    {
        get
        {
            startTabIndex++;
            return (short)(startTabIndex - 1);
        }
        set
        {
            startTabIndex = value;
        }
    }

    public bool WriteTable
    {
        get
        {
            return writeTable;
        }
        set
        {
            writeTable = value;
        }
    }
}
