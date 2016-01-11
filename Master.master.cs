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

public partial class Master : System.Web.UI.MasterPage
{
    public string noScrollHtml = "";     //On login page no <body> scrolling needed

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack || PolyPage.ShowReport)
        {
            if (PolyUtils.RequestQuerystring("Action") == "1")
            {
                //new PolytexObjects.Event(PolytexObjects.Event.Types.ManagerLogout, PolytexObjects.Event.SubTypes.None, 0, true, PolyUtils.RequestCookie("SystemUserId"), true).Insert();
                SystemUser.SignOutUser();
            }
            SpecificPageHandling();

            PolytexMenu.CurrentPage = CurrentPage;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PolytexMenu.CurrentRoleType = ((PolyPage)this.Page).CurrentUser.CurrentRoleType;
    }

    protected override void Render(HtmlTextWriter writer)
    {
        if (PolyReport.IsForMailingList)
        {//Special rendering for mailing lists attachements
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

            ContentPlaceHolder1.RenderControl(hw);


            string htmlHead = @"<html xmlns=""http://www.w3.org/1999/xhtml"" >";
            htmlHead = @"<head><title>" + PageTitle + "</title>";
            htmlHead += @"<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />";
            htmlHead += @"<meta http-equiv=""X-UA-Compatible"" content=""IE=EmulateIE7"" />";

            htmlHead += @"<style type=""text/css"">";

            htmlHead += System.IO.File.ReadAllText(UniStr.Paths.Folder.PolytexWeb + @"CSS\Global.css");
            htmlHead += System.IO.File.ReadAllText(UniStr.Paths.Folder.PolytexWeb + @"CSS\Master.css");
            htmlHead += System.IO.File.ReadAllText(UniStr.Paths.Folder.PolytexWeb + @"CSS\Reports.css");
            htmlHead += System.IO.File.ReadAllText(UniStr.Paths.Folder.PolytexWeb + @"CSS\Gridview.css");
            htmlHead += System.IO.File.ReadAllText(UniStr.Paths.Folder.PolytexWeb + @"CSS\" + PolyConfig.CultureDirection + ".css");

            htmlHead += Environment.NewLine + "img {visibility:hidden;}";
            htmlHead += "</style>";
            htmlHead += "</head>";
            htmlHead += @"<body style="""">";

            string contentPlaceHolder1 = tw.ToString();

            HttpContext.Current.Response.Write(htmlHead + contentPlaceHolder1 + "</body></html>");
            HttpContext.Current.Response.End();
        }
        else
        {
            base.Render(writer);
        }
    }

    /// <summary>
    /// Handles specific page rendering etc.
    /// </summary>
    public void SpecificPageHandling()
    {
        if (CurrentPage == "Default")
        {
            PolytexMenu.Visible = false;
            TdMenu.Visible = false;
            LabelPageTitle.Visible = false;
            noScrollHtml = @"scroll=""no""";
        }
        else
        {
            PolytexMenu.Visible = true;
            TdMenu.Visible = true;

            if (CurrentPage != "Welcome")
            {
                LabelPageTitle.Text = PolyUtils._T[CurrentPage];
            }
        }
            
    }

    /// <summary>
    /// Gets current executed file name 
    /// </summary>
    public static string CurrentPage
    {
        get
        {
            return PolyUtils.CurrentPage;
        }
    }

    public string PageTitle
    {
        get
        {
            string currentPage = CurrentPage;

            if (currentPage == "" || currentPage == "Default")
            {
                return "Polytex Tasks Manager";
            }
            else
            {
                return PolyUtils._T[CurrentPage];
            }
        }
    }

    public string ContentAlign
    {
        get
        {
            if (CurrentPage == "default")
            {
                return "center";
            }
            else
            {
                return PolyPage.CultureAlign;
            }
        }
    }

    public string ClientBrowser
    {
        get
        {
            return PolyPage.ClientBrowser;
        }
    }

    public void AddActivity(object sender, EventArgs e)
    {
        Response.Redirect("ActivityDetails.aspx?activityId=0", true);
    }

    protected void LabelSystemUserName_Load(object sender, EventArgs e)
    {
        try
        {
            LabelSystemUserName.Text = ((PolyPage)Page).CurrentUser.UserName;

            if (LabelSystemUserName.Text != "")
            {
                LabelSystemUserName.Text = PolyUtils._T["User"] + ": " + LabelSystemUserName.Text;
            }
        }
        catch
        {

        }
    }
}
