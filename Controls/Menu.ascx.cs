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
using System.Collections.Generic;

public partial class Controls_Menu : System.Web.UI.UserControl
{
    private SystemUser.UserRoleType currentRoleType = SystemUser.UserRoleType.Unidentified;
    private string currentPage = "";
        
    /// <summary>
    /// Holds a list of all menu items (including screens and misc)
    /// </summary>
    private enum MenuItem
    {
        HeaderAdmin,
        Welcome,
        HeaderReports,
        Spacer,
        Users,
        UsersLimitGroups,
        Departments,
        ItemsGroups,
        PrepaidPackages,
        Stations,
        Orders,
        CardsManager,
        CreditCharge,
        SystemUsers,
        Alerts,
        MailingLists,
        SystemSettings,
        Territories,
        ActivityGroups,
        Activities,
        Models,
        Clients,
        Units,
        SystemAdmin,        
        MAC,
        LogFiles,        
        //EncryptPasswordsBatch,        
        ReportOrders,
        ReportActivities,
        ReportClients,
        ReportActivityGroup,
        ReportActivityType,
        ReportUnits,
        ReportTechnicians,
        ReportWorkerOrders,             //Cancelled => Data displayed in ReportOrders
        ReportOrdersSummary,
        ReportUsersConsumption,
        ReportInactiveUsers,
        ReportStationOrders,            //Cancelled => Data displayed in ReportOrders
        ReportDepartmentsItems,
        ReportStationItems,
        ReportInventory,
        ReportUsersBalance,
        ReportItemHistory,              //Cancelled => Data displayed in ReportOrders
        ReportDelayedItems,             //Cancelled => Data displayed in ReportItems
        ReportItems,
        ReportAlerts,
        ReportCreditTransactions,
        ReportEvents,
        Ignore
    }

    //The following arrays hold items to display for each system user role
    private static MenuItem[] PolytexAdminItems = new MenuItem[] 
        {
            MenuItem.HeaderAdmin,

            MenuItem.Territories,
            MenuItem.Activities,
            MenuItem.SystemUsers,
            MenuItem.Clients,
            MenuItem.ActivityGroups,
            MenuItem.Models,
            MenuItem.Units,
            MenuItem.HeaderReports,
            MenuItem.ReportActivities,
            MenuItem.ReportClients,
            MenuItem.ReportActivityGroup,
            MenuItem.ReportActivityType,
            MenuItem.ReportUnits,
            MenuItem.ReportTechnicians,
        };

    private static MenuItem[] AdminItems = new MenuItem[] 
        {
            MenuItem.HeaderAdmin,

            MenuItem.Activities,
            MenuItem.SystemUsers,
            MenuItem.ActivityGroups,
            MenuItem.Models,
            MenuItem.Units,
            MenuItem.Clients,
            MenuItem.HeaderReports,
            MenuItem.ReportActivities,
            MenuItem.ReportClients,
            MenuItem.ReportActivityGroup,
            MenuItem.ReportActivityType,
            MenuItem.ReportUnits,
            MenuItem.ReportTechnicians,
        };


    private static MenuItem[] Technician = new MenuItem[] 
        {
            MenuItem.HeaderAdmin,

            MenuItem.Activities,
            MenuItem.SystemUsers,
            MenuItem.Units,
            MenuItem.Clients,

            MenuItem.HeaderReports,
            MenuItem.ReportActivities,    
            MenuItem.ReportClients,  
            MenuItem.ReportActivityGroup,
            MenuItem.ReportActivityType,
            MenuItem.ReportUnits,   
            MenuItem.ReportTechnicians,
        };


    protected void Page_Load(object sender, EventArgs e)
    {
        if (PolyPage.IsMobile)
        {
            PanelMenuHeader.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
        }
        
        //if (!IsPostBack)
        {
            //List<MenuItem> DisplayedItems = new List<MenuItem>();
            MenuItem[] DisplayedItems;      //Array to hold menu items to display
            switch (CurrentRoleType)
            {
                case SystemUser.UserRoleType.PolytexAdmin:      //Polytex admin items                                         
                    DisplayedItems = PolytexAdminItems;
                    break;
                case SystemUser.UserRoleType.Unidentified:      //Unidentified role, empty menu
                    DisplayedItems = new MenuItem[] { };                    
                    break;
                case SystemUser.UserRoleType.SystemAdmin:       //Admin user items  
                    DisplayedItems = AdminItems;                                       
                    break;
                case SystemUser.UserRoleType.Technician:       //Technician items
                    DisplayedItems = Technician;
                    break;
                default:    //Unidentified role/new role not added to code, empty menu
                    DisplayedItems = new MenuItem[] { };
                    break;
            }

            //Iterate through menu items to display
            string strMenu = "";
            for (int i = 0; i < DisplayedItems.GetLength(0); i++)
            {
                if (DisplayedItems[i] == MenuItem.Spacer)
                {//Spacer menu item
                    strMenu += SpacerHtml();
                }
                else if (DisplayedItems[i] == MenuItem.HeaderAdmin || DisplayedItems[i] == MenuItem.HeaderReports)
                {//Header menu item
                    strMenu += HeaderHtml(DisplayedItems[i]);
                }
                else
                {//Screen menu item
                    if (DisplayedItems[i] != MenuItem.Ignore)
                    {
                        strMenu += ItemHtml(DisplayedItems[i], CurrentPage);
                    }
                }


            }
            
            MenuHTML = strMenu;
            PlaceHolderDisplayedMenuItems.Controls.Add(new LiteralControl(MenuHTML));
        }
        //else
        //{
        //    PlaceHolderDisplayedMenuItems.Controls.Add(new LiteralControl(MenuHTML));
        //}
    }

    /// <summary>
    /// Returns html string for menu section header
    /// </summary>    
    private static string HeaderHtml(MenuItem itemToDisplay)
    {
        string headerHtml = "";
        headerHtml += @"<div class=""MenuWidth HeaderWrapperDiv"">";  //Wrapper div
        headerHtml += @"<div class=""HeaderInnerDiv"">";              //Inner div
        headerHtml += PolyUtils._T[itemToDisplay.ToString()]; //Header text
        headerHtml += "</div>";
        headerHtml += "</div>";
        return headerHtml;
    }

    /// <summary>
    /// Returns html string for menu goto screen item
    /// </summary>
    /// <param name="itemToDisplay"></param>
    /// <returns></returns>
    private static string ItemHtml(MenuItem itemToDisplay, string currentPage)
    {
        string itemName = itemToDisplay.ToString();
        string itemTrans = PolyUtils._T[itemName];        
        string itemJS = PolyUtils.FormatParameterForJS(itemTrans);

        bool isOnItemScreen = (currentPage == itemName.ToLower());

        string itemHtml = "";

        itemHtml += @"<div class=""MenuItemWidth ";
        if (itemToDisplay.ToString() == currentPage)
        {//On current page, display highlighted
            itemHtml += @"ItemWrapperDivOnPage"" ";
        }
        else
        {
            itemHtml += @"ItemWrapperDiv"" ";
            itemHtml += @"onmouseover=""OverMenuItem(this, ParameterToString('" + itemJS + @"'))"" ";
            itemHtml += @"onmouseout=""OutMenuItem(this)"" ";
        }
        
        
        itemHtml += @"onclick=""ClickMenuItem('" + itemName + @".aspx')"" ";
        itemHtml += @"title=""" + HttpUtility.HtmlEncode(itemTrans) + @""" ";
        itemHtml += @">";

        itemHtml += @"<div class=""MenuItemWidth ItemInnerDiv"">&nbsp;";
        itemHtml += PolyUtils.CropString(itemTrans, 20); //Menu item text
        itemHtml += "</div>";
        itemHtml += "</div>";
      
        return itemHtml;
    }

    private static string SpacerHtml()
    {
        return @"<div class=""MenuWidth MenuSpacer"">&nbsp;</div>";
    }
  //  

    /// <summary>
    /// For which menu items to display
    /// </summary>
    public SystemUser.UserRoleType CurrentRoleType
    {
        set
        {
            currentRoleType = value;
        }
        get
        {
            return currentRoleType;
        }
    }

    /// <summary>
    /// Gets current executed file name 
    /// </summary>
    public string CurrentPage
    {
        get
        {
            return currentPage;
        }
        set
        {
            currentPage = value;
        }
    }

    /// <summary>
    /// Saves the menu's html for postbacks
    /// </summary>
    public String MenuHTML
    {
        get
        {
            return (String)ViewState["MenuHTML"];
        }
        set
        {
            ViewState["MenuHTML"] = value;
        }
    }

}
