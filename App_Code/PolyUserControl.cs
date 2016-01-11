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
/// Custom UserControl for Polytex
/// </summary>
public class PolyUserControl : UserControl
{
    #region VARIABLES

    public static string inPage = "";

    #endregion

    #region CONSTRUCTORS

    public PolyUserControl()
	{
    }

    #endregion

    #region METHODS

    public static string RequestQuerystring(string RequestKey)
    {
        return PolyUtils.RequestQuerystring(RequestKey);
    }

    public static string RequestForm(string RequestKey)
    {
        return PolyUtils.RequestForm(RequestKey);
    }

    #endregion

    #region PROPERTIES

    public static UniStringContainer _T
    {
        get
        {
            return PolyUtils._T;
        }
    }

    public static string CultureAlign
    {
        get
        {
            return PolyPage.CultureAlign;
        }
    }

    public static string CultureAlignReverse
    {
        get
        {
            return PolyPage.CultureAlignReverse;
        }
    }

    public string InPage
    {
        set
        {
            inPage = value;
        }
        get
        {
            return inPage;
        }
    }

    public bool IsRFID
    {
        get
        {
            return PolyConfig.IsRFID;
        }
    }

    public bool IsExtendedLimits
    {
        get
        {
            return PolyConfig.IsExtendedLimits;
        }
    }

    public bool IsCreditCards
    {
        get
        {
            return PolyConfig.IsCreditCards;
        }
    }

    #endregion
}
