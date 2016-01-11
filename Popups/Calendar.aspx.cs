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

public partial class Popups_Calendar : PolyPage
{
    private const int minYear = 1980;
    private const int maxYear = 2100;

    public string textBoxId = "";

    DateTime selectedDate = new DateTime();
    private PolytexControls.ImageButton buttonNextYear;
    private PolytexControls.ImageButton buttonNextMonth;
    private PolytexControls.ImageButton buttonPrevYear;
    private PolytexControls.ImageButton buttonPrevMonth;
    private string prevYearImageUrl;
    private string prevMonthImageUrl;
    private string nextYearImageUrl;
    private string nextMonthImageUrl;
    private string prevYearImageUrlDisabled;
    private string prevMonthImageUrlDisabled;
    private string nextYearImageUrlDisabled;
    private string nextMonthImageUrlDisabled;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = UniStr.UniLang.DefaultCultureInfo;
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        textBoxId = PolyUtils.RequestQuerystring("textBoxId");

        if (PolyPage.IsMobile)
        {
            PolytexImageButtonRight.Width = 50;
            PolytexImageButtonRight.Height = 50;
            PolytexImageButtonDoubleRight.Width = 50;
            PolytexImageButtonDoubleRight.Height = 50;
            PolytexImageButtonLeft.Width = 50;
            PolytexImageButtonLeft.Height = 50;
            PolytexImageButtonDoubleLeft.Width = 50;
            PolytexImageButtonDoubleLeft.Height = 50;
        }
        
        if (PolyPage.CultureDirection == "ltr")
        {
            buttonPrevYear = PolytexImageButtonDoubleLeft;
            buttonPrevMonth = PolytexImageButtonLeft;
            buttonNextYear = PolytexImageButtonDoubleRight;
            buttonNextMonth = PolytexImageButtonRight;

            prevYearImageUrl = "~/Images/Buttons/LeftDoubleArrow.gif";
            prevMonthImageUrl = "~/Images/Buttons/LeftArrow.gif";
            nextYearImageUrl = "~/Images/Buttons/RightDoubleArrow.gif";
            nextMonthImageUrl = "~/Images/Buttons/RightArrow.gif";

            prevYearImageUrlDisabled = "~/Images/Buttons/LeftEndArrowDisabled.gif";
            prevMonthImageUrlDisabled = "~/Images/Buttons/LeftArrowDisabled.gif";
            nextYearImageUrlDisabled = "~/Images/Buttons/RightEndArrowDisabled.gif";
            nextMonthImageUrlDisabled = "~/Images/Buttons/RightArrowDisabled.gif";
        }
        else
        {
            buttonPrevYear = PolytexImageButtonDoubleRight;
            buttonPrevMonth = PolytexImageButtonRight;
            buttonNextYear = PolytexImageButtonDoubleLeft;
            buttonNextMonth = PolytexImageButtonLeft;

            prevYearImageUrl = "~/Images/Buttons/RightDoubleArrow.gif";
            prevMonthImageUrl = "~/Images/Buttons/RightArrow.gif";
            nextYearImageUrl = "~/Images/Buttons/LeftDoubleArrow.gif";
            nextMonthImageUrl = "~/Images/Buttons/LeftArrow.gif";

            prevYearImageUrlDisabled = "~/Images/Buttons/RightEndArrowDisabled.gif";
            prevMonthImageUrlDisabled = "~/Images/Buttons/RightArrowDisabled.gif";
            nextYearImageUrlDisabled = "~/Images/Buttons/LeftEndArrowDisabled.gif";
            nextMonthImageUrlDisabled = "~/Images/Buttons/LeftArrowDisabled.gif";
        }

        buttonPrevYear.CommandName = buttonPrevYear.Trans = "PrevYear";
        buttonPrevMonth.CommandName = buttonPrevMonth.Trans = "PrevMonth";
        buttonNextYear.CommandName = buttonNextYear.Trans = "NextYear";
        buttonNextMonth.CommandName = buttonNextMonth.Trans = "NextMonth";

        if (!IsPostBack)
        {
            if (PolyUtils.RequestQuerystring("selectedDate") == "")
            {
                Calendar1.VisibleDate = DateTime.Now.Date;
            }
            else
            {
                if (DateTime.TryParse(PolyUtils.RequestQuerystring("selectedDate"), out selectedDate))
                {
                    if (selectedDate < new DateTime(minYear, 1, 1))
                    {
                        Calendar1.VisibleDate = new DateTime(minYear, 1, 1);
                    }
                    else if (selectedDate > new DateTime(maxYear, 12, 31))
                    {
                        Calendar1.VisibleDate = new DateTime(maxYear, 12, 31);
                    }
                    else
                    {
                        Calendar1.VisibleDate = Calendar1.SelectedDate = selectedDate;
                    }
                }
                else
                {
                    Calendar1.VisibleDate = DateTime.Now.Date;
                }
            }

            

            //Calendar Navigation
            for (int i = 1; i <= 12; i++)
            {
                DropDownListMonth.Items.Add(new ListItem(System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(i), i.ToString()));
            }

            for (int i = minYear; i <= maxYear; i++)
            {
                DropDownListYear.Items.Add(new ListItem(i.ToString()));
            }


        }
    }


    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        if (e.Day.IsOtherMonth)
        {
            e.Day.IsSelectable = false;
            e.Cell.CssClass = "";
        }
        else
        {
            if (e.Day.Date != Calendar1.SelectedDate)
            {
                e.Cell.Attributes.Add("onmouseover", "this.style.backgroundColor='#FFC080';");
                if (e.Day.IsToday)
                {
                    e.Cell.Attributes.Add("onmouseout", "this.style.backgroundColor='DarkGray';");
                }
                else
                {
                    e.Cell.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                }
                e.Cell.Attributes.Add("onclick", "SelectDate(this);");
            }
            else
            {
                e.Cell.Attributes.Add("onclick", "return SelectedDateClicked();");                
            }
            e.Cell.ToolTip = DropDownListMonth.SelectedItem.Text + " " + e.Day.Date.Day;
        }
    }

    protected void PolytexImageButtonNavigation_Command(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "PrevYear":
                Calendar1.VisibleDate = Calendar1.VisibleDate.AddYears(-1);
                break;
            case "PrevMonth":
                Calendar1.VisibleDate = Calendar1.VisibleDate.AddMonths(-1);
                break;
            case "NextYear":
                Calendar1.VisibleDate = Calendar1.VisibleDate.AddYears(1);
                break;
            case "NextMonth":
                Calendar1.VisibleDate = Calendar1.VisibleDate.AddMonths(1);
                break;
        }

    }

    protected void Calendar1_PreRender(object sender, EventArgs e)
    {
        DropDownListMonth.SelectedValue = Calendar1.VisibleDate.Month.ToString();
        DropDownListYear.SelectedValue = Calendar1.VisibleDate.Year.ToString();

        if (Calendar1.VisibleDate.Year == minYear)
        {
            buttonPrevYear.ImageUrl = prevYearImageUrlDisabled;
            buttonPrevYear.OnClientClick = "return false;";
            buttonPrevYear.Style.Add("cursor", "default");
            if (Calendar1.VisibleDate.Month == 1)
            {
                buttonPrevMonth.ImageUrl = prevMonthImageUrlDisabled;
                buttonPrevMonth.OnClientClick = "return false;";
                buttonPrevMonth.Style.Add("cursor", "default");
            }
        }
        else
        {
            buttonPrevYear.ImageUrl = prevYearImageUrl;
            buttonPrevYear.OnClientClick = "";
            buttonPrevYear.Style.Remove("cursor");
            buttonPrevMonth.ImageUrl = prevMonthImageUrl;
            buttonPrevMonth.OnClientClick = "";
            buttonPrevMonth.Style.Remove("cursor");

        }

        if (Calendar1.VisibleDate.Year == maxYear)
        {
            buttonNextYear.ImageUrl = nextYearImageUrlDisabled;
            buttonNextYear.OnClientClick = "return false;";
            buttonNextYear.Style.Add("cursor", "default");
            if (Calendar1.VisibleDate.Month == 12)
            {
                buttonNextMonth.ImageUrl = nextMonthImageUrlDisabled;
                buttonNextMonth.OnClientClick = "return false;";
                buttonNextMonth.Style.Add("cursor", "default");
            }
        }
        else
        {
            buttonNextYear.ImageUrl = nextYearImageUrl;
            buttonNextYear.OnClientClick = "";
            buttonNextYear.Style.Remove("cursor");
            buttonNextMonth.ImageUrl = nextMonthImageUrl;
            buttonNextMonth.OnClientClick = "";
            buttonNextMonth.Style.Remove("cursor");
        }

    }

    protected void DropDownListNavigation_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedMonth = Convert.ToInt32(DropDownListMonth.SelectedValue);
        int selectedYear = Convert.ToInt32(DropDownListYear.SelectedValue);

        Calendar1.VisibleDate = new DateTime(selectedYear, selectedMonth, 1);
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {        
        PlaceHolderDateSelected.Visible = true;
    }
}
