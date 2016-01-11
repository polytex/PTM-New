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

using PolytexObjects;
using PolytexData;
using UniStr;
using PolytexControls;
using PolytexObjects;

public partial class Popups_Popups : System.Web.UI.MasterPage
{
    public string PopupOverFlowStyle = "";
    public string strJsFormAction = "";
    public bool OnCloseClearPopup = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (PolyPage.IsMobile)
        {
          
        }      
        string popupTitle = "";
        //Title & Icon for popup
        if (CurrentPopup.IndexOf("Report") == 0)
        {
            popupTitle = PolyUtils._T[CurrentPopup];
            PopupIcon.Src = "../Images/Popups/ReportQuery.gif";

            if (PolyUtils.RequestQuerystring("mailingListId") == "")    //On Mailing lists page query popups submit to themselves
            {
                form1.Target = "_parent";
                //Form's action is set this way because of .Net Framework backward compatibility
                strJsFormAction = @"document.forms[0].action = """ + PolyPage.SiteURL + CurrentPopup + ".aspx?ShowReport=1" + @""";";
                //form1.Action = PolyPage.SiteURL + CurrentPopup + ".aspx?ShowReport=1";
            }       
        }
        else
        {
            int userId;
            string fullName;

            switch (CurrentPopup)
            {
                case "Confirm":
                    switch (PolyUtils.RequestQuerystring("confirmType"))
                    {
                        case "disable":
                            popupTitle = PolyUtils._T["Disable"];
                            break;
                        case "enable":
                            popupTitle = PolyUtils._T["Enable"];
                            break;
                        case "resetItemsAmount":
                            popupTitle = PolyUtils._T["ResetItemsBalance"];
                            break;
                        case "autoFavorites":
                            popupTitle = PolyUtils._T["AutoSetFavoriteItems"];
                            break;
                        case "resetStation":
                            popupTitle = PolyUtils._T["ResetStation"];
                            break;
                        case "restartAllStations":
                            popupTitle = PolyUtils._T["RestartAllStations"];
                            break;
                        case "StationSync":
                            popupTitle = PolyUtils._T["SyncUsers"];
                            break;

                    default:
                        popupTitle = PolyUtils._T[PolyUtils.RequestQuerystring("confirmType")];
                        if (popupTitle == "")
                        {
                            popupTitle = PolyUtils._T["Polytex"];
                        }
                        break;

                    }
                    break;
                case "ConfirmLog":
                    switch (PolyUtils.RequestQuerystring("confirmType"))
                    {
                        case "disableUsers":
                            popupTitle = PolyUtils._T["DisableUsersOnList"];
                            break;
                        case "enableUsers":
                            popupTitle = PolyUtils._T["EnableUsersOnList"];
                            break;
                        default:
                            popupTitle = PolyUtils._T["Polytex"];
                            break;

                    }
                    break;
                case "Calendar":
                    {
                        popupTitle = PolyUtils._T["Calendar"];
                        PopupIcon.Src = "../Images/Popups/Calendar.gif";
                        PopupOverFlowStyle = "overflow:hidden;";
                    }
                    break;
                case "PleaseWait":
                    string actionId = PolyUtils.RequestQuerystring("actionId");

                    if (actionId != "")
                    {
                        popupTitle = PolyUtils._T[actionId];
                    }
                    else
                    {
                        popupTitle = PolyUtils._T["Message"];
                    }
                        
                    
                    break;
                case "ArchiveDb":
                    popupTitle = PolyUtils._T["ArchiveDb"];
                    PopupIcon.Src = "../Images/Toolbar/ArchiveDb.gif";
                    break;
                case "BackupRestoreDb":
                    popupTitle = PolyUtils._T["BackupRestoreDb"];
                    PopupIcon.Src = "../Images/Toolbar/BackupRestoreDb.gif";
                    break;
                case "SWUpdateFiles":
                    popupTitle = PolyUtils._T["SWUpdate"];
                    PopupIcon.Src = "../Images/Toolbar/SWUpdate.gif";
                    break;
                case "BalancesAdmin":
                    popupTitle = PolyUtils._T["BalancesAdmin"];
                    PopupIcon.Src = "../Images/Toolbar/Balances.gif";
                    break;
                case "Clients":
                    string clientId = PolyUtils.RequestQuerystring("clientId");
                    popupTitle = PolytexData.Manage_Clients.GetNameById(Util.ValidateInt(clientId, 0));
                    break;
                default:
                    popupTitle = PolyUtils._T[CurrentPopup];
                    if (popupTitle == "")
                    {
                        popupTitle = PolyUtils._T["Polytex"];
                    }
                    break;                   
            }
        }
        popupTitle = (popupTitle == "" ? "Polytex" : popupTitle);
        LabelTitle.Text = popupTitle;
    }


    /// <summary>
    /// Gets current executed file name 
    /// </summary>
    public string CurrentPopup
    {
        get
        {
            return PolyUtils.CurrentPage;
        }
    }

    public string ClientBrowser
    {
        get
        {
            return PolyPage.ClientBrowser;
        }
    }


    public string PopupContentHeight
    {
        get
        {
            string strStyleHeight = PolyUtils.RequestQuerystring("height");
            if (strStyleHeight != "")
            {
                int intHeight = UniStr.Util.ValidateInt(strStyleHeight, 0);

                if (intHeight > 0)
                {                    
                    strStyleHeight = "height:" + intHeight.ToString() + "px;";
                }
            }
            else
            {
                strStyleHeight = "height:100%;";
            }
            
            return strStyleHeight;
        }
    }
}
