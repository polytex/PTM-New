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
using System.IO;
  
/// <summary>
/// Holds Application scope data
/// </summary>
public class ApplicationData
{
    private static DataTable systemUsersPermissions; 
    private static DataTable systemUsersTerritory;
    private static DataTable alertsTransportMethods;    
    private static DataTable limitPeriodTypes;
     
    public ApplicationData()
	{
         
    }

    #region Methods

    public static string GetAlertIdTranslation(object alertId)
    {
        string lang = UniLang.UniLanguage.ToUpper();
        string alertIdHex = GetHexString(alertId);
        string trans = _A["A_" + alertIdHex, lang];

        if (String.IsNullOrEmpty(trans.Trim()))
        {
            trans = _A["A_" + alertIdHex, "ENG"];
            if (String.IsNullOrEmpty(trans.Trim()))
            {
                trans = alertIdHex;
            }
            else
            {
                trans = alertIdHex + " " + trans;
            }
        }
        return trans;
    }

    public static DataTable GetSchedulePatternTypes()
    {
        return SchedulePatternTypes;
    }

    public static string GetPatternTypeTranslation(int patternTypeId)
    {
        string trans = "";
        DataTable dt = ApplicationData.SchedulePatternTypes;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToInt32(dt.Rows[i]["ID"]) == patternTypeId)
            {
                trans = dt.Rows[i]["PATTERN_TYPE_NAME"].ToString();
                break;
            }
        }
        return trans;
    }

    public static DataTable GetCultureDays()
    {
        return CultureDays;
    }
    
    public static string GetCultureDayName(int dayId)
    {
        string dayName = "";

        DataTable dtDays = CultureDays;

        for (int i = 0; i < dtDays.Rows.Count; i++)
        {
            if (Convert.ToInt32(dtDays.Rows[i]["ID"]) == dayId)
            {
                dayName = dtDays.Rows[i]["DAY_NAME"].ToString();
                break;
            }
        }

        dtDays.Dispose();
        dtDays = null;
        return dayName;
    }

    public static DataTable GetMonthDays()
    {
        DataTable dtDays = new DataTable();

        dtDays.Columns.Add("ID");
        dtDays.Columns.Add("DAY_NAME");
        DataRow dr;
        for (int i = 1; i < 32; i++)
        {
            if (i < 31)
            {
                dr = dtDays.NewRow();
                dr["ID"] = dr["DAY_NAME"] = i;
            }
            else
            {
                dr = dtDays.NewRow();
                dr["ID"] = i;
                dr["DAY_NAME"] = PolyUtils._T["LastDayOfMonth"];
            }
            dtDays.Rows.Add(dr);
        }

        return dtDays;
    }

    public static string GetMonthDayName(int dayId)
    {
        if (dayId < 31)
        {
            return dayId.ToString();
        }
        else
        {
            return PolyUtils._T["LastDayOfMonth"];
        }


    }

    public static string GetHexString(object o)
    {
        if ((o == null) || (o == DBNull.Value))
        {
            return "";
        }
        int num = (int)o;
        return string.Format("0x{0:X4}", (ushort)num);
    }

    public static string GetHexString(int i)
    {
        return GetHexString((object)i);                
    }



    public static DataTable GetTimeOptions()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("NAME");
        int num = 0;
        for (int i = 0; i < 48; i++)
        {
            DataRow dr = dt.NewRow();
            if (num < 10)
            {
                if (i % 2 == 0)
                {
                    dr["ID"] = "0" + num + ":" + "00";
                    dr["Name"] = "0" + num + ":" + "00";
                }
                else
                {
                    dr["ID"] = "0" + num + ":" + "30";
                    dr["Name"] = "0" + num + ":" + "30";
                }

            }
            else
            {
                if (i % 2 == 0)
                {
                    dr["ID"] = num + ":" + "00";
                    dr["Name"] = num + ":" + "00";
                }
                else
                {
                    dr["ID"] = num + ":" + "30";
                    dr["Name"] = num + ":" + "30";
                }
            }
            dt.Rows.Add(dr);
            if (i % 2 == 1)
                num++;
        }
        return dt;

    }

    public static DataTable GetDriveTimeOptions()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("NAME");
        int num = 0;
        for (int i = 0; i < 17; i++)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = (i * 30).ToString();
            if (num < 10)
            {
                if (i % 2 == 0)
                {
                    dr["Name"] = "0" + num + ":" + "00";
                }
                else
                {
                    dr["Name"] = "0" + num + ":" + "30";
                }

            }
            dt.Rows.Add(dr);
            if (i % 2 == 1)
                num++;
        }
        return dt;

    }

    public static string GetClientNameById(int id)
    {
        return Manage_Clients.GetNameById(id);
    }

    public static string[] UpdateClientList(int territory)
    {
        string[] suggestionList = new string[2];
        suggestionList[0] = "";
        suggestionList[1] = "";
        DataTable dt = Manage_Clients.Select(false, territory);

        // Sending Client List in javascript string array
        foreach (DataRow dr in dt.Rows)
        {

            if (string.IsNullOrEmpty(suggestionList[0]))
            {
                suggestionList[0] += "\"" + dr["NAME"].ToString().Replace("\"", "\\\"") + "\"";
                suggestionList[1] += "\"" + dr["ID"].ToString() + "\"";
            }
            else
            {
                suggestionList[0] += ", \"" + dr["NAME"].ToString().Replace("\"", "\\\"") + "\"";
                suggestionList[1] += ", \"" + dr["ID"].ToString() + "\"";
            }
        }
        return suggestionList;

    }

    public static int GetClientIdByName(string name)
    {
        return Manage_Clients.GetIdByClientName(name);
    }

    public static string getUserNameIncludeAdmin(object UserId, object UserName)
    {
        string Id = UserId.ToString();
        string Name = UserName.ToString();
        if (Util.ValidateInt(Id, 0) == -1)
        {
            return "Polytex Admin";
        }
        else
        {
            return Name;
        }
    }

    public static string UpdateLableTerritoryName(int territoryId, int systemUserId, int chooseTerritory)
    {
        string territory = "";
        DataTable dt = Manage_Territories.Select(false, territoryId, false);

        if (systemUserId < 0)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = "-1";
            dr["Name"] = "All";
            dt.Rows.Add(dr);
        }

        foreach (DataRow row in dt.Rows)
        {
            if (row["ID"].ToString() == chooseTerritory.ToString())
            {
                territory = row["Name"].ToString();
            }

        }
        return territory;
    }

    public static string GetImageSource(object Id)
    {
        string source = "ActivityImages/NoImage.png";
        int activityId = Util.ValidateInt(Id.ToString(), 0);
        string rootPath = HttpContext.Current.Request.PhysicalApplicationPath;
        string[] files = Directory.GetFiles(rootPath+"ActivityImages\\", activityId + "_1.*");

        if (files.Length > 0)
        {
            // Getting the extension of the file
            string[] extension = files[files.Length-1].Split('.');
            source = "ActivityImages/" + activityId + "_1." + extension[extension.Length - 1];
        }

        return source;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Holds WebManager system permissions in data table
    /// </summary>
    public static DataTable SystemUsersRoles
    {
        get
        {
            if (systemUsersPermissions == null)
            {
                systemUsersPermissions = SystemUser.GetSystemUsersRoles(true);                
            }
            return systemUsersPermissions;
        }
    }

    public static DataTable SchedulePatternTypes
    {
        get
        {
            DataTable dtPatternTypes = new DataTable();

            dtPatternTypes.Columns.Add("ID");
            dtPatternTypes.Columns.Add("PATTERN_TYPE_NAME");

            DataRow dr = dtPatternTypes.NewRow();
            dr["ID"] = 1;
            dr["PATTERN_TYPE_NAME"] = PolyUtils._T["Once"];
            dtPatternTypes.Rows.Add(dr);

            dr = dtPatternTypes.NewRow();
            dr["ID"] = 2;
            dr["PATTERN_TYPE_NAME"] = PolyUtils._T["Daily"];
            dtPatternTypes.Rows.Add(dr);

            dr = dtPatternTypes.NewRow();
            dr["ID"] = 3;
            dr["PATTERN_TYPE_NAME"] = PolyUtils._T["Weekly"];
            dtPatternTypes.Rows.Add(dr);

            dr = dtPatternTypes.NewRow();
            dr["ID"] = 4;
            dr["PATTERN_TYPE_NAME"] = PolyUtils._T["Monthly"];
            dtPatternTypes.Rows.Add(dr);

            return dtPatternTypes;
        }
    }

    public static DataTable CultureDays
    {
        get
        {
            DataTable dtDays = new DataTable();

            dtDays.Columns.Add("ID");
            dtDays.Columns.Add("DAY_NAME");

            string[] dayNames = UniLang.getCultureInfo(UniLang.DefaultUILanguage).DateTimeFormat.DayNames;
            DataRow dr;
            for (int i = 0; i < dayNames.Length; i++)
            {
                dr = dtDays.NewRow();
                dr["ID"] = i;
                dr["DAY_NAME"] = dayNames[i];
                dtDays.Rows.Add(dr);
            }

            return dtDays;
        }
    }

    public static UniStringContainer _A
    {
        get
        {            
            if (__A == null)
            {
                __A = new UniStringContainer(HttpContext.Current.Server.MapPath("TransAlerts.xml"));
            }
            return __A;
        }
        set
        {
            __A = value;
        }
    }

    protected static UniStringContainer __A
    {
        get
        {
            return (UniStringContainer)HttpRuntime.Cache["TransAlerts.xml"];
        }
        set
        {
            HttpRuntime.Cache["TransAlerts.xml"] = value;
        }
    }

    #endregion
}


