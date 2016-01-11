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


public partial class Smart : System.Web.UI.MasterPage
{
    public string noScrollHtml = "";     //On login page no <body> scrolling needed

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack || PolyPage.ShowReport)
        {
            if (PolyUtils.RequestQuerystring("Action") == "1")
            {
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
