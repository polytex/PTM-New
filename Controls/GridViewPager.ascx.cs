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

public partial class Controls_GridViewPager : System.Web.UI.UserControl
{       
    private GridView gridView;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        gridView = (GridView)(this.Parent.Parent.Parent.Parent);

    }

    public static string CultureDirection
    {
        get
        {
            return PolyConfig.CultureDirection;
        }
    }
    

    protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
    {
        // Set the PageIndex property to display that page selected by the user.
        PolyGridViewUtils.ClearSetRows(gridView, true, true);
        gridView.PageIndex = DropDownListPager.SelectedIndex;
    }

    public override void DataBind()
    {
        base.DataBind();

        if (DropDownListPager != null)
        {
            string strPageText = PolyUtils._T["Page"];

            int skip = 0;

            if (this.Page is PolyReport)
            {
                string imageButtonNextSkipId = (CultureDirection == "ltr" ? "PolytexImageButtonRightSkip" : "PolytexImageButtonLeftSkip");
                string imageButtonNextSkipFileName = (CultureDirection == "ltr" ? "RightDoubleArrowDisabled.gif" : "LeftDoubleArrowDisabled.gif");

                string imageButtonPreviousSkipId = (CultureDirection == "ltr" ? "PolytexImageButtonLeftSkip" : "PolytexImageButtonRightSkip");
                string imageButtonPreviousSkipFileName = (CultureDirection == "ltr" ? "LeftDoubleArrowDisabled.gif" : "RightDoubleArrowDisabled.gif");

                PolytexControls.ImageButton imageButtonNextSkip = (PolytexControls.ImageButton)FindControl(imageButtonNextSkipId);
                PolytexControls.ImageButton imageButtonPreviousSkip = (PolytexControls.ImageButton)FindControl(imageButtonPreviousSkipId);

                skip = ((PolyReport)(this.Page)).Skip;
                if (skip == 0)
                {
                    imageButtonPreviousSkip.Enabled = false;
                    imageButtonPreviousSkip.ImageUrl = "~/Images/Buttons/" + imageButtonPreviousSkipFileName;
                    imageButtonPreviousSkip.Style.Add("cursor", "default");
                }

                //Response.Write(((PolyReport)(this.Page)).ReportObjectDataSource.ObjectDataSourceDataTable.Rows.Count);
                if (!((PolyReport)(this.Page)).HasNextSkip)
                {
                    imageButtonNextSkip.Enabled = false;
                    imageButtonNextSkip.ImageUrl = "~/Images/Buttons/" + imageButtonNextSkipFileName;
                    imageButtonNextSkip.Style.Add("cursor", "default");
                }

                TextBoxSkip.Value = skip.ToString();
            }
            else
            {
                tdLeftSkip.Visible = false;
                tdRightSkip.Visible = false;
            }

            for (int i = 0; i < gridView.PageCount; i++)
            {
                int pageNumber = i + 1;     // Create a ListItem object to represent a page.
                ListItem item = new ListItem(strPageText + " " + (pageNumber + (PolytexData.ReportConfig.MaxReportPages * skip)).ToString(), pageNumber.ToString());

                if (i == gridView.PageIndex)
                {
                    item.Selected = true;
                }

                if (gridView.PageIndex == 0)
                {
                    string imageButtonFirstPageId = (CultureDirection == "ltr" ? "PolytexImageButtonLeftEnd" : "PolytexImageButtonRightEnd");
                    string imageButtonFirstPageFileName = (CultureDirection == "ltr" ? "LeftEndArrowDisabled.gif" : "RightEndArrowDisabled.gif");

                    string imageButtonPrevPageId = (CultureDirection == "ltr" ? "PolytexImageButtonLeft" : "PolytexImageButtonRight");
                    string imageButtonPrevPageFileName = (CultureDirection == "ltr" ? "LeftArrowDisabled.gif" : "RightArrowDisabled.gif");

                    PolytexControls.ImageButton imageButtonFirst = (PolytexControls.ImageButton)FindControl(imageButtonFirstPageId);
                    imageButtonFirst.Enabled = false;
                    imageButtonFirst.ImageUrl = "~/Images/Buttons/" + imageButtonFirstPageFileName;
                    imageButtonFirst.Style.Add("cursor", "default");

                    PolytexControls.ImageButton imageButtonPrev = (PolytexControls.ImageButton)FindControl(imageButtonPrevPageId);
                    imageButtonPrev.Enabled = false;
                    imageButtonPrev.ImageUrl = "~/Images/Buttons/" + imageButtonPrevPageFileName;
                    imageButtonPrev.Style.Add("cursor", "default");
                }
                
                if (gridView.PageIndex == (gridView.PageCount - 1))
                {
                    string imageButtonLastPageId = (CultureDirection == "ltr" ? "PolytexImageButtonRightEnd" : "PolytexImageButtonLeftEnd");
                    string imageButtonLastPageFileName = (CultureDirection == "ltr" ? "RightEndArrowDisabled.gif" : "LeftEndArrowDisabled.gif");

                    string imageButtonNextPageId = (CultureDirection == "ltr" ? "PolytexImageButtonRight" : "PolytexImageButtonLeft");
                    string imageButtonNextPageFileName = (CultureDirection == "ltr" ? "RightArrowDisabled.gif" : "LeftArrowDisabled.gif");

                    PolytexControls.ImageButton imageButtonLast = (PolytexControls.ImageButton)FindControl(imageButtonLastPageId);
                    imageButtonLast.Enabled = false;
                    imageButtonLast.ImageUrl = "~/Images/Buttons/" + imageButtonLastPageFileName;
                    imageButtonLast.Style.Add("cursor", "default");

                    PolytexControls.ImageButton imageButtonNext = (PolytexControls.ImageButton)FindControl(imageButtonNextPageId);
                    imageButtonNext.Enabled = false;
                    imageButtonNext.ImageUrl = "~/Images/Buttons/" + imageButtonNextPageFileName;
                    imageButtonNext.Style.Add("cursor", "default");
                }
                
                DropDownListPager.Items.Add(item);
            }
        }
    }

    protected void PolytexImageButtonArrowClick(object sender, ImageClickEventArgs e)
    {
        PolyGridViewUtils.ClearSetRows(gridView, true, true);        
    }


    protected void PolytexImageSkipButtonClick(object sender, ImageClickEventArgs e)
    {
        gridView.PageIndex = 0;        
        gridView.DataBind();
    }

    protected void PolytexImageButtonSkip_PreRender(object sender, EventArgs e)
    {
        ((PolytexControls.ImageButton)(sender)).ToolTip = PolyUtils._T[((PolytexControls.ImageButton)(sender)).ToolTip].Replace("%max%", PolytexData.ReportsTasks.MaxReportResults.ToString());
    }
}
