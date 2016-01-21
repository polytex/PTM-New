using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using UniStr;
using PolytexData;
//using PolytexTrace;

/// <summary>
/// Misc utils for Polytex WebManager project
/// </summary>
public class PolyUtils
{
    protected static UniStringContainer uniStringContainer;
    private static string currentPage = "";

    public PolyUtils()
	{		
	}

    public static HttpRequest CurrentRequest
    {
        get
        {
            return HttpContext.Current.Request;
        }
    }

    /// <summary>
    /// Currently requested page
    /// </summary>
    public static string CurrentPage
    {
        get
        {                           
            currentPage = System.IO.Path.GetFileNameWithoutExtension(HttpContext.Current.Request.FilePath);
            currentPage = (currentPage == "default" ? "Default" : currentPage);                    
            return currentPage;
        }
    }

    /// <summary>
    /// Multilanguage translation
    /// </summary>
    public static UniStringContainer _T
    {
        get
        {
            return UniStr.UniLang._T;
        }
    }

    /// <summary>
    /// Returns a request form value even if key doesnt exist in Request.Form collection 
    /// </summary>
    /// <param name="RequestKey">Request.Form[RequestKey]</param>    
    public static string RequestForm(string requestKey)
    {
        if (HttpContext.Current.Request.Form[requestKey] == null)
        { return ""; }
        else
        { return HttpContext.Current.Request.Form[requestKey].ToString().Trim(); }
    }

    /// <summary>
    /// Returns a request form value by searching RequestKey name if contained in one of Request.Form collection 
    /// Used when recieving form is not on same .aspx file
    /// Note: Make sure that RequestKey is unique in form submitted
    /// Returns empty string if not found
    /// </summary>
    /// <param name="RequestKey">Request.Form["??????" + RequestKey + "????"]</param>    
    public static string RequestFormByContainedKey(string requestKey)
    {
        string strValue = "";
        for (int i = 0; i < HttpContext.Current.Request.Form.Keys.Count; i++)
        {
            if (HttpContext.Current.Request.Form.Keys[i].IndexOf(requestKey) > -1)
            {
                strValue = HttpContext.Current.Request.Form[i].ToString().Trim();
                break;
            }            
        }

        return strValue; 
    }

    public static string RequestQuerystringByContainedKey(string requestKey)
    {
        string strValue = "";
        for (int i = 0; i < HttpContext.Current.Request.QueryString.Keys.Count; i++)
        {
            try
            {
                if (HttpContext.Current.Request.QueryString.Keys[i] != null)
                {
                    if (HttpContext.Current.Request.QueryString.Keys[i].IndexOf(requestKey) > -1)
                    {
                        strValue = HttpContext.Current.Request.QueryString[i].ToString().Trim();
                        break;
                    }
                }
            }
            catch
            {
                throw new Exception(requestKey);
            }
        }

        return strValue;
    }

    public static string RequestFormOrQuerystring(string requestKey)
    {
        string strValue = "";

        strValue = RequestForm(requestKey);
        if (strValue == "")
        {
            strValue = RequestQuerystring(requestKey);
        } 
        return strValue;
    }

    public static string RequestFormOrQuerystringByContainedKey(string requestKey)
    {
        string strValue = "";

        strValue = RequestFormByContainedKey(requestKey);
        if (strValue == "")
        {
            strValue = RequestQuerystringByContainedKey(requestKey);
        }
        return strValue;
    }

    /// <summary>
    /// Returns a request querystring value. if key doesnt exist in Request.Querystring collection returns ""
    /// </summary>
    /// <param name="RequestKey">Request.Querystring[RequestKey]</param>    
    public static string RequestQuerystring(string requestKey)
    {
        if (HttpContext.Current.Request.QueryString[requestKey] == null)
        { return ""; }
        else
        { return HttpContext.Current.Request.QueryString[requestKey].ToString().Trim(); }
    }

    /// <summary>
    /// Returns a cookie value. if key doesnt exist in Request.Cookies collection returns ""
    /// </summary>
    /// <param name="RequestKey">Request.Querystring[RequestKey]</param>    
    public static string RequestCookie(string CookieName)
    {
        CookieName = CookieName;
        if (HttpContext.Current.Request.Cookies[CookieName] == null)
        { return ""; }
        else
        { return HttpContext.Current.Request.Cookies[CookieName].Value.ToString().Trim(); }
    }

    /// <summary>
    /// Write session cookie
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public static void ResponseCookie(string CookieName, string CookieValue)
    {
        ResponseCookie(CookieName, CookieValue, new DateTime());
    }

    /// <summary>
    /// Write expirable cookie
    /// </summary>    
    public static void ResponseCookie(string CookieName, string CookieValue, DateTime CookieExpiration)
    {
        HttpResponse ResponseCookie = HttpContext.Current.Response;
        ResponseCookie.Cookies[CookieName].Name = CookieName;
        ResponseCookie.Cookies[CookieName].HttpOnly = true;
        ResponseCookie.Cookies[CookieName].Value = CookieValue;
        if (CookieExpiration != new DateTime())
        {
            ResponseCookie.Cookies[CookieName].Expires = CookieExpiration;
        }
    }

    /// <summary>
    /// For easy handling of JS parameter sending.
    /// Applied with JS/Global.js function "ParameterToString(str)" 
    /// </summary>
    /// <param name="parameterValue"></param>
    /// <returns></returns>
    public static string FormatParameterForJS(string parameterValue)
    {
        return parameterValue.Replace(@"""", @"~~~~").Replace("'", "####");
    }

    public static string StringToSQL(string str)
    {
        return str.Replace(@"'", "''");
    }
     
    /// <summary>
    /// Checks if new object's name is unique
    /// </summary>
    /// <param name="fieldName">DB field representing the object's name</param>
    /// <param name="tableName">DB table name</param>
    /// <param name="newName">Inserted new name</param>
    /// <returns>True if unique</returns>
    public static bool DbObjectNameIsNew(string fieldName, string tableName, string newName)
    {
        return (!DBcon.RecordExists(fieldName, tableName, fieldName + "='" + PolyUtils.StringToSQL(newName) + "'"));
    }

    /// <summary>
    /// Checks if edited object's name is unique for its id
    /// </summary>
    /// <param name="fieldName">DB field representing the object's name</param>
    /// <param name="tableName">DB table name</param>
    /// <param name="newName">Edited new name</param>
    /// <returns>True if unique</returns>
    public static bool DbObjectNameIsUnique(string fieldName, string tableName, string newName, int objectId)
    {
        return (!DBcon.RecordExists(fieldName, tableName, "ID<>" + objectId.ToString() + " AND " + fieldName + "='" + PolyUtils.StringToSQL(newName) + "'"));
    }

    public static bool DbObjectNameIsUniqueByTable(string fieldName, string tableName, string newName, string fieldName2, string CheckID, int objectId)
    {
        return (!DBcon.RecordExists(fieldName, tableName, "ID<>" + objectId.ToString() + 
            " AND " + fieldName + "='" + PolyUtils.StringToSQL(newName) + "'"+
            " AND " + fieldName2 + "='" + CheckID + "'"));
    }

    public static bool DbObjectNameIsNewByTable(string fieldName, string tableName, string newName, string fieldName2, string CheckID)
    {
        return (!DBcon.RecordExists(fieldName, tableName, fieldName + "='" + PolyUtils.StringToSQL(newName) + "'"+
           " AND " + fieldName2 + "='" + CheckID + "'"));
    }

    public static void ValidateCsvFile(FileUpload fileUpload, CustomValidator customValidator)
    {
        ValidateFileType(fileUpload, customValidator, ".csv");
    }

    public static void ValidateFileType(FileUpload fileUpload, CustomValidator customValidator, string fileType)
    {
        customValidator.IsValid = false;

        if (fileUpload.HasFile)
        {
            customValidator.Text = PolyUtils._T["ErrorInFile"];

            if (System.IO.Path.GetExtension(fileUpload.FileName).ToLower() == fileType)
            {
                if (fileUpload.PostedFile.ContentLength > 0)
                {
                    customValidator.IsValid = true;
                }
            }
        }
        else
        {
            customValidator.Text = PolyUtils._T["FileRequired"];
        }
    }

    public static bool IsNewDateTime(DateTime dateTime)
    {
        if (dateTime == new DateTime())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string HtmlAttributeHtmlEncode(string str)
    {
        return (str.Replace(@"""", "''"));
    }

    /// <summary>
    /// Crops string to max chars and adds '...'
    /// </summary>
    /// <param name="stringToCrop"></param>
    /// <param name="maxChars"></param>
    /// <returns></returns>
    public static string CropString(string stringToCrop, int maxChars)
    {
        string croppedString = "";

        if (!String.IsNullOrEmpty(stringToCrop))
        {
            if (stringToCrop.Length <= maxChars)
            {
                croppedString = stringToCrop;
            }
            else
            {
                croppedString = stringToCrop.Substring(0, (maxChars - 3)) + "...";
            }
        }

        return croppedString;
    }

    public static void CropLabelText(Label label, int maxChars)
    {                
        label.ToolTip = label.Text;
        label.Text = PolyUtils.CropString(label.Text, maxChars);
        label.Style.Add(HtmlTextWriterStyle.Cursor, "default");        
    }

    /// <summary>
    /// Specific handling of system user's name label
    /// </summary>
    /// <param name="label"></param>
    public static void CropSystemUserNameLabel(object label)
    {
        Label labelSystemUserName = (Label)label;

        if (labelSystemUserName.Text.Trim() == "")
        {
            labelSystemUserName.Visible = false;
        }
        else
        {            
            CropLabelText(labelSystemUserName, 10);
            labelSystemUserName.Text = " [" + labelSystemUserName.Text + "]";
        }
    }

    //public static void WriteTrace(LogEntry.EntryTypes entryType, string title, string message)
    //{
    //    new LogEntry(entryType, LogEntry.ProjectName.WebProject, title).Insert(message);
    //}

    //public static void WriteTrace(string title, string station, Exception ex)
    //{
    //    new LogEntry(LogEntry.EntryTypes.Exception, LogEntry.ProjectName.WebProject, station, title).Insert(ex);
    //}

    public static void RemoveLastComma(ref string str)
    {
        if (!String.IsNullOrEmpty(str))
        {
            str = str.TrimEnd();

            if (str.LastIndexOf(",") == (str.Length - 1))
            {
                str = str.Substring(0, str.Length - 1);
            }
        }
        else
        {
            str = str;
        }
    }

    public static string GetCheckBoxListValues(CheckBoxList checkBoxList)
    {
        string checkBoxListValues = "";
        foreach (ListItem li in checkBoxList.Items)
        {
            if (li.Selected)
            {
                checkBoxListValues += li.Value + "|";
            }
        }
        checkBoxListValues = checkBoxListValues.Substring(0, checkBoxListValues.Length - 1);
        return checkBoxListValues;
    }

    public static string TrickRefresh()
    {
        string trick = DateTime.Now.ToString();
        trick = trick.Replace(" ", "");
        trick = trick.Replace(":", "");
        trick = trick.Replace("/", "");

        return "Trick=" + trick + "&";
    }

    public static string GetEnumNameById<T>(int id)
    {
        Type enumType = typeof(T);
        return Enum.GetName(enumType, (object)(Convert.ToInt32(id)));
    }

    public static string GetEnumTransNameById<T>(int id)
    {
        return _T[GetEnumNameById<T>(id)];
    }

    /// <summary>
    /// Transforms an Enum list to a datatable for control binding
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ignoreFirst">For unidentified ignoring</param>
    /// <returns></returns>
    public static DataTable EnumToDataTable<T>(bool ignoreFirst)
    {
        Type enumType = typeof(T);

        // Can't use type constraints on value types, so have to do check like this
        if (enumType.BaseType != typeof(Enum))
            throw new ArgumentException("T must be of type System.Enum");

        Array enumValArray = Enum.GetValues(enumType);

        DataTable dtEnum = new DataTable("ENUM");
        dtEnum.Columns.Add("ID");
        dtEnum.Columns.Add("NAME");
        

        DataRow dr;
        for (int i = (ignoreFirst ? 1 : 0); i < enumValArray.Length; i++)
        {
            dr = dtEnum.NewRow();
            dr["NAME"] = _T[enumValArray.GetValue(i).ToString()];
            dr["ID"] = Enum.Parse(typeof(T), enumValArray.GetValue(i).ToString()).GetHashCode().ToString();// enumValArray.GetValue(i).ToString();

            dtEnum.Rows.Add(dr);
        }
 
        return dtEnum;
    }
}
