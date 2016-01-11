using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Xml;
using System.IO;
using UniStr;

/// <summary>
/// Handles configuration both for web.config and Settings.config
/// </summary>
public class PolyConfig
{
    public PolyConfig()
    {
    }

    #region Web.Config

    private static string webManagerVersion;
    private static int reportPageSize = 0;

    /// <summary>
    /// Get Polytex WebManager web.config setting
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetWebConfigSetting(string key)
    {
        return ConfigurationManager.AppSettings[key];
    }


    /// <summary>
    /// Mainly used to refresh JS and CSS files for browsers with cache when changed
    /// </summary>
    public static string WebManagerVersion
    {
        get
        {
            if (String.IsNullOrEmpty(webManagerVersion))
            {
                webManagerVersion = PolyConfig.GetWebConfigSetting("version");
            }
            return webManagerVersion;
        }
    }

    /// <summary>
    /// Mainly used to refresh JS and CSS files for browsers with cache when changed
    /// </summary>
    public static int ReportPageSize
    {
        get
        {
            if (reportPageSize == 0)
            {
                try
                {
                    reportPageSize = UniStr.Settings.RecordsOnReport;
                }
                catch
                {
                    reportPageSize = 25;
                }
            }

            return reportPageSize;
        }
    }

    #endregion



    #region Settings.Config

    #region VARIABLES

    private static string currentCulture;
    private static string cultureDirection;
    private static string distributorName;
    private static string distributorPhone;
    private static string distributorEmail;
    private static string distributorSite;

    #endregion

    /// <summary>
    /// Current Polytex system culture 
    /// </summary>
    public static string CurrentCulture
    {
        get
        {
            if (String.IsNullOrEmpty(currentCulture))
            {
                currentCulture = UniStr.UniLang.getCultureInfo(UniStr.UniLang.DefaultUILanguage).ToString();
            }
            return currentCulture;
        }
    }

    /// <summary>
    /// Current Polytex culture direction (ltr/rtl)
    /// </summary>
    public static string CultureDirection
    {
        get
        {
            if (String.IsNullOrEmpty(cultureDirection))
            {
                cultureDirection = UniLang.UniDir.ToLower();
            }
            return cultureDirection;
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

    public static bool IsRFIDHandling
    {
        get
        {
            return Settings.IsRFIDHandling;
        }
    }

    /// <summary>
    /// Maximum change of items limit when editing a user
    /// </summary>
    public static int MaxLimitItemsAmountChange
    {
        get
        {
            return 30;
        }
    }


    public static bool HRManagerChangeBalances
    {
        get
        {
            return UniStr.Config.GetSettingBooleanValue("HRManagerChangeBalances");
        }
    }


    /// <summary>
    /// True: user has one limit group
    /// False: user has multiple limit groups
    /// </summary>
    public static bool UseSimpleLimitsPolicy
    {
        get
        {
            return Boolean.Parse(UniStr.Config.GetSettingValue("UseSimpleLimitsPolicy"));
        }
    }


    /// <summary>
    /// CSV Encoding 
    /// </summary>
    public static System.Text.Encoding DefaultCSVEncoding
    {
        get
        {
            System.Text.Encoding defaultCSVEncoding = System.Text.Encoding.GetEncoding(1255);

            try
            {
                defaultCSVEncoding = System.Text.Encoding.GetEncoding(int.Parse(UniStr.Config.GetSettingValue("DefaultCSVEncoding")));
            }
            catch
            { }

            return defaultCSVEncoding;
        }
    }


    public static string DistributorName
    {
        get
        {
            if (String.IsNullOrEmpty(distributorName))
            {
                distributorName = UniStr.Config.GetSettingValue("DistributorName");
            }
            return distributorName;
        }
    }


    public static string DistributorPhone
    {
        get
        {
            if (String.IsNullOrEmpty(distributorPhone))
            {
                distributorPhone = UniStr.Config.GetSettingValue("DistributorPhone");
            }
            return distributorPhone;
        }
    }


    public static string DistributorEmail
    {
        get
        {
            if (String.IsNullOrEmpty(distributorEmail))
            {
                distributorEmail = UniStr.Config.GetSettingValue("DistributorEmail");
            }
            return distributorEmail;
        }
    }


    public static string DistributorSite
    {
        get
        {
            if (String.IsNullOrEmpty(distributorSite))
            {
                distributorSite = UniStr.Config.GetSettingValue("DistributorSite");
            }
            return distributorSite;
        }
    }


    public static bool DisplayCardsManager
    {
        get
        {
            return UniStr.Config.GetSettingBooleanValue("DisplayCardsManager");
        }
    }


    #endregion


}
