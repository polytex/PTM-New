using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using PolytexData;
using UniStr;



/// <summary>
/// Custom screen page for Polytex
/// </summary>
public class PolyPage : Page
{	    
    #region VARIABLES

    private bool authenticationRequired = true;
    private static string cultureAlign;
    private static string cultureAlignReverse;    
    private SystemUser currentUser = new SystemUser();
    private PageType pageType = PageType.ManageData;
    private string focusControlClientId = "";
    private int focusControlPriority = 0;

    #endregion

    #region ENUMS

    public enum ActionMessageType { Undefined, Success, Failure }
    public enum ActionMessage { InsertedSuccessfuly, InsertFailure, UpdatedSuccessfuly, UpdateFailure, EnabledSuccessfuly, DisabledSuccessfuly, DeletedSuccessfuly, DeleteFailure }
    public enum PageType { ManageData, Report, Popup, ExportExcel }
    
    #endregion

    #region CONSTRUCTORS

    public PolyPage()
    {

    }

    #endregion

    #region METHODS

    protected override void OnPreInit(EventArgs e)
    {
        if (IsMobile)
        {

            if (!PolyUtils.CurrentRequest.AppRelativeCurrentExecutionFilePath.Contains("Popups"))
            {
                this.MasterPageFile = "~/Smart.master";
            }

        }
        
        base.OnPreInit(e);
        this.Culture = PolyConfig.CurrentCulture; 
        this.Theme = "PolyPage";        //Set culture for page (especialy important for date formats)

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("he-IL");
    }
    

    protected override void OnInit(EventArgs e)
    {
        //Before Page_Init()
        

        base.OnInit(e);
        
        //Set CurrentPageType as Report
        //IMPORTANT: All reports' files name should start with 'Report'
        if (CurrentPage.IndexOf("Report") == 0)
        {
            CurrentPageType = PageType.Report;
        }

        //Internal Server Request for mailing list
        if (this.AuthenticationRequired && !PolyReport.IsForMailingList)
        {
            SystemUser.ValidateUser(this);            
        }        
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!PolyReport.IsForMailingList)
        {
            //base.VerifyRenderingInServerForm(control);
        }
    } 


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        
        //Register client scripts        
        ClientScriptManager clientScriptManager = Page.ClientScript;

        //Set ActionMessageLabel's id as javascript variable for later reference
        if (this.ActionMessageLabel != null)
        {
            string clientScriptName = "PolytexLabelActionMessageVariable";
            if (!clientScriptManager.IsClientScriptBlockRegistered(clientScriptName))
            {
                String clientScriptText = @"var spanPolytexLabelActionMessageId = """ + this.ActionMessageLabel.ClientID + @""";";
                clientScriptManager.RegisterStartupScript(this.GetType(), clientScriptName, clientScriptText, true);
            }
        }

        if (this.ActionMessageLabel2 != null)
        {
            string clientScriptName = "PolytexLabelActionMessage2Variable";
            if (!clientScriptManager.IsClientScriptBlockRegistered(clientScriptName))
            {
                String clientScriptText = @"var spanPolytexLabelActionMessage2Id = """ + this.ActionMessageLabel2.ClientID + @""";";
                clientScriptManager.RegisterStartupScript(this.GetType(), clientScriptName, clientScriptText, true);
            }
        }
        
    }
    
    /// <summary>
    /// Initiates GridView global Polytex Manager properties
    /// </summary>
    protected void GridViewOnInit(object sender, EventArgs e)
    {
        PolyGridViewUtils.GridViewOnInit((GridView)sender);
    }


    public static string GetAppSetting(string key)
    {
        return PolyConfig.GetWebConfigSetting(key);
    }

  
    public static string RequestForm(string RequestKey)
    {
        return PolyUtils.RequestForm(RequestKey);
    }


    public static string RequestQuerystring(string RequestKey)
    {
        return PolyUtils.RequestQuerystring(RequestKey);
    }

    protected void ActionMessage_Load(object sender, EventArgs e)
    {
        this.ActionMessageLabel = (PolytexControls.Label)(sender);
    }

    protected void ActionMessage2_Load(object sender, EventArgs e)
    {
        this.ActionMessageLabel2 = (PolytexControls.Label)(sender);
    }


    public void SetActionMessage(PolytexControls.Label label, string message, ActionMessageType messageType)
    {
        try
        {
            label.Visible = true;
            label.Trans = message;
            label.CssClass = "Action" + messageType.ToString();
        }
        catch
        {
            throw new Exception("Add ActionMessageLabel to your aspx file!");
        }

    }


    public void SetActionMessage(PolytexControls.Label label, ActionMessage actionMessage)
    {
        switch (actionMessage)
        {
            case ActionMessage.InsertedSuccessfuly:
                SetActionMessage(label, actionMessage.ToString(), ActionMessageType.Success);
                break;
            case ActionMessage.InsertFailure:
                SetActionMessage(label, actionMessage.ToString(), ActionMessageType.Failure);
                break;
            case ActionMessage.UpdatedSuccessfuly:
                SetActionMessage(label, actionMessage.ToString(), ActionMessageType.Success);
                break;
            case ActionMessage.UpdateFailure:
                SetActionMessage(label, actionMessage.ToString(), ActionMessageType.Failure);
                break;
            case ActionMessage.EnabledSuccessfuly:
                SetActionMessage(label, actionMessage.ToString(), ActionMessageType.Success);
                break;
            case ActionMessage.DisabledSuccessfuly:
                SetActionMessage(label, actionMessage.ToString(), ActionMessageType.Success);
                break;
            case ActionMessage.DeletedSuccessfuly:
                SetActionMessage(label, actionMessage.ToString(), ActionMessageType.Success);
                break;
            case ActionMessage.DeleteFailure:
                SetActionMessage(label, actionMessage.ToString(), ActionMessageType.Success);
                break;
        }
    }

    public void SetActionMessage(string message, ActionMessageType messageType)
    {
        SetActionMessage(ActionMessageLabel, message, messageType);
    }


    public void SetActionMessage(ActionMessage actionMessage)
    {
        SetActionMessage(ActionMessageLabel, actionMessage);
    }


    protected void SetOnLoadFocus(object sender, EventArgs e)
    {
        PolytexControls.TextBox textBox = (PolytexControls.TextBox)(sender);

        int focusPriority = UniStr.Util.ValidateInt(textBox.Argument, 0);

        if (focusPriority >= this.FocusControlPriority)
        {
            this.FocusControlClientId = textBox.AspTextBoxControl.ClientID;
            this.FocusControlPriority = focusPriority;
        }

    }

 
    /// <summary>
    /// Sorting Gridview util
    /// </summary>
    public void SortGridView(object sender, CommandEventArgs e)
    {
        PolytexControls.LinkButton linkButtonHeader = (PolytexControls.LinkButton)(sender);

        GridView gridView = (GridView)(linkButtonHeader.Parent.Parent.Parent.Parent);

        string strSelectedDataKey = "";
        if (gridView.SelectedDataKey != null)
        {
            strSelectedDataKey = gridView.SelectedDataKey.Value.ToString();
        }

        if (gridView.SortExpression == linkButtonHeader.CommandName)
        {
            if (gridView.SortDirection == SortDirection.Ascending)
            {
                gridView.Sort(linkButtonHeader.CommandName, SortDirection.Descending);
            }
            else
            {
                gridView.Sort("", SortDirection.Ascending);
            }

        }
        else
        {
            gridView.Sort(linkButtonHeader.CommandName, SortDirection.Ascending);
        }

        if (strSelectedDataKey != "")
        {
            PolyGridViewUtils.SetSelectedRow(gridView, strSelectedDataKey);
        }
    }    


    /// <summary>
    /// Sorting Gridview util
    /// </summary>
    public void ImageSortAscending_PreRender(object sender, EventArgs e)
    {
        Image imageSort = (Image)sender;
        GridView gridView = (GridView)(imageSort.Parent.Parent.Parent.Parent);
        
        if (imageSort.ID.IndexOf(gridView.SortExpression) > 0 && gridView.SortDirection == SortDirection.Ascending)
        {
            imageSort.Visible = true;
        }
        else
        {
            imageSort.Visible = false;
        }
    }


    /// <summary>
    /// Sorting Gridview util
    /// </summary>
    public void ImageSortDescending_PreRender(object sender, EventArgs e)
    {
        Image imageSort = (Image)sender;
        GridView gridView = (GridView)(imageSort.Parent.Parent.Parent.Parent);

        if (imageSort.ID.IndexOf(gridView.SortExpression) > -1 && gridView.SortDirection == SortDirection.Descending)
        {
            imageSort.Visible = true;
        }
        else
        {
            imageSort.Visible = false;
        }
    }


    protected void ButtonImageText_PreRender(object sender, EventArgs e)
    {
        PolytexControls.Button button = (PolytexControls.Button)sender;
        button.Text = "     " + button.Text.Replace(" ", "");
        button.Style.Add("background-position", CultureAlign + " center");
    }

    #endregion

    #region PROPERTIES

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


    /// <summary>
    /// Polytex screen type
    /// </summary>
    public PageType CurrentPageType
    {
        get
        {
            return pageType;
        }
        set
        {
            pageType = value;
        }
    }


    /// <summary>
    /// Which html object to focus on when page loads
    /// </summary>
    public string FocusControlClientId
    {
        get
        {
            return focusControlClientId;
        }
        set
        {
            focusControlClientId = value;
        }
    }


    public int FocusControlPriority
    {
        get
        {
            return focusControlPriority;
        }
        set
        {
            focusControlPriority = value;
        }
    }


    /// <summary>
    /// Mainly used to refresh JS and CSS files for browsers with cache when changed
    /// </summary>
    public static string WebManagerVersion
    {
        get
        {            
            return PolyConfig.WebManagerVersion;
        }
    }

    
    /// <summary>
    /// Authenticate Polytex user?
    /// </summary>
    public bool AuthenticationRequired
    {
        get
        {
            return authenticationRequired;
        }
        set
        {
            authenticationRequired = value;
        }
    }


    public SystemUser CurrentUser
    {
        set
        {
            currentUser = value;
        }
        get
        {
            return currentUser;
        }
    }


    /// <summary>
    /// Multilanguage translation
    /// </summary>
    public static UniStringContainer _T
    {
        get
        {
            return PolyUtils._T;
        }
    }


    /// <summary>
    /// Returns site's URL
    /// </summary>
    public static string SiteURL
    {
        get
        {                        
            //string absoluteUri = HttpContext.Current.Request.Url.AbsoluteUri;            
            //siteUrl = absoluteUri.Replace(System.IO.Path.GetFileName(absoluteUri), "");
            //siteUrl = siteUrl.Replace("Popups/", "").Replace("Controls/", "");            
            //return siteUrl;            
            string siteUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + System.Web.VirtualPathUtility.ToAbsolute("~");
            
            if (siteUrl.LastIndexOf("/") != (siteUrl.Length - 1))
            {//Add "/" only if already exists
                siteUrl += "/";
            }

            return siteUrl;                        
        }
    }


    /// <summary>
    /// Current Polytex culture alignment (left/right)
    /// </summary>
    public static string CultureAlign
    {
        get
        {
            if (String.IsNullOrEmpty(cultureAlign))
            {
                cultureAlign = UniLang.UniAlign.ToLower();                
            }
            return cultureAlign;
        }
    }


    /// <summary>
    /// Current Polytex culture reverse alignment (right/left)
    /// </summary>
    public static string CultureAlignReverse
    {
        get
        {
            if (String.IsNullOrEmpty(cultureAlignReverse))
            {
                cultureAlignReverse = UniLang.UniAlignReverse.ToLower();
            }
            return cultureAlignReverse;
        }
    }


    public static string CultureDirection
    {
        get
        {            
            return PolyConfig.CultureDirection;
        }
    }


    private PolytexControls.Label actionMessageLabel;
    public PolytexControls.Label ActionMessageLabel
    {
        get
        {            
            return actionMessageLabel;            
        }
        set
        {
            actionMessageLabel = value;
        }
    }


    private PolytexControls.Label actionMessageLabel2;
    public PolytexControls.Label ActionMessageLabel2
    {
        get
        {
            return actionMessageLabel2;            
        }
        set
        {
            actionMessageLabel2 = value;
        }
    }


    public static string ClientBrowser
    {
        get
        {
            string browser = HttpContext.Current.Request.Browser.Browser.ToLower();
            if (browser == "internetexplorer")
            {
                browser = "ie";
            }
            return browser;
        }
    }


    public static bool IsChrome
    {
        get
        {
            return (ClientBrowser.ToLower() == "applemac-safari");
        }
    }

    public static bool IsMobile
    {
        get
        {
            string sourceUserAgent = HttpContext.Current.Request.UserAgent.Trim().ToLower();
            return sourceUserAgent.Contains("android") || sourceUserAgent.Contains("iphone");
        }
    }


    public static bool IsExtendedLimits
    {
        get
        {
            return Settings.IsExtendedLimits;
        }
    }


    public static bool IsRFID
    {
        get
        {
            return Settings.IsRFID;
        }
    }


    public static bool IsCreditCards
    {
        get
        {
            return Settings.IsCreditCards;
        }
    }


    public static bool IsLimitByFavoriteItems
    {
        get
        {
            return Settings.IsLimitByFavoriteItems;
        }
    }

    /// <summary>
    /// For reports display mechanism. Report query pop up submits form to main
    /// screen with ShowReport=1 in querystring
    /// </summary>
    public static bool ShowReport
    {
        get
        {
            if (PolyUtils.RequestQuerystring("ShowReport") == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    public static bool IsNewDateTime(DateTime dateTime)
    {
        return PolyUtils.IsNewDateTime(dateTime);
    }

    public static HttpRequest CurrentRequest
    {
        get
        {
            return PolyUtils.CurrentRequest;
        }
    }

    #endregion
}
