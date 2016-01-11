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

/// <summary>
/// 1. Holds current user's data
/// 2. Handles SystemUser authentication, validation etc.
/// </summary>
public class SystemUser
{
    #region VARIABLES

    //For class property
    private UserRoleType currentRoleType = UserRoleType.Unidentified;
    private string userName = "";
    private int systemUserId = 0;
    private int territoryId = 0;


    public SystemUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public enum UserRoleType
    {
        PolytexAdmin = -1,
        Unidentified = 0,
        SystemAdmin = 1,
        Technician = 2
    }

    #endregion

    #region METHODS

    /// <summary>
    /// Authenticates user login to manager
    /// Assigns role id and user's name (not login name)
    /// </summary> 
    /// <returns>Authenticated?</returns>
    public static bool AuthenticateLogin(string loginUserName, string loginPassword, out int roleId, out string userName, out int systemUserId, out int territoryId)
    {
        bool authenticated = false;

        roleId = 0;     //Unidentified
        territoryId = 0;

        if (loginUserName.ToLower() == "asd" && loginPassword.ToLower() == "poi" + PasswordDynamic)
        {//Polytex                           
            userName = "Polytex";
            roleId = -1; //Polytex roleId (Not in Settings.config for security)
            territoryId = -1;
            systemUserId = -1;
            authenticated = true;
        }
        else
        {
            //SqlConnection conn = new SqlConnection(ConStr);
            //conn.Open();
            //conn.Close();
            //Get user details by login name and password (also encryption validation done)
            DataRow drSystemUser = PolytexData.DBcon.LoginSystemUser(loginUserName, loginPassword);

            if (drSystemUser == null)
            {//User does not exist / wrong password
                authenticated = false;
                roleId = 0;
                systemUserId = 0;
                userName = "";
            }
            else
            {
                userName = drSystemUser["NAME"].ToString();
                if (!Int32.TryParse(drSystemUser["USER_TYPE_ID"].ToString(), out roleId))
                {
                    roleId = -1;
                }
                systemUserId = Convert.ToInt32(drSystemUser["USER_ID"]);
                territoryId = Convert.ToInt32(drSystemUser["TERRITORY_ID"]);
                

                authenticated = true;
            }
            drSystemUser = null;
        }

        return authenticated;
    }

    public static bool AuthenticatePassword(int systemUserId, string loginPassword)
    {
        bool authenticated = false;

        //string today = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();

        if (systemUserId == -1 && loginPassword.ToLower() == "asd" + PasswordDynamic)
        {//Polytex                           
            authenticated = true;
        }
        else
        {
            if (loginPassword.IndexOf(PasswordDynamic) > 0)
            {
                //Get user details by login name and password (also encryption validation done)
                string loginUserName = PolytexData.DBcon.SelectField("LOGIN", "SYSTEM_USERS", "ID=" + systemUserId, "");

                loginPassword = loginPassword.Replace(PasswordDynamic, "");

                DataRow drSystemUser = PolytexData.DBcon.LoginSystemUser(loginUserName, loginPassword);

                if (drSystemUser == null)
                {//User does not exist / wrong password
                    authenticated = false;
                }
                else
                {
                    authenticated = true;
                }
                drSystemUser = null;
            }
        }

        return authenticated;
    }

    /// <summary>
    /// Sets expiration cookie value and details
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="userName"></param>
    public static void SetAuthentication(int roleId, string userName, string systemUserId, int territoryId)
    {
        int sessionLength;
        if (roleId == -1)
        {
            sessionLength = 0;
        }
        else
        {
            sessionLength = 0;
        }

        if (sessionLength == 0)
        {//Polytex admin / session does not expire
            PolyUtils.ResponseCookie("SessionExpires", "0");
        }
        else
        {//Session expires in sessionLength (minutes converted to ticks)
            PolyUtils.ResponseCookie("SessionExpires", DateTime.Now.AddMinutes(sessionLength).Ticks.ToString());
        }

        PolyUtils.ResponseCookie("Role", roleId.ToString());
        PolyUtils.ResponseCookie("UserName", userName);
        PolyUtils.ResponseCookie("SystemUserId", systemUserId.ToString());
        PolyUtils.ResponseCookie("TerritoryId", territoryId.ToString());
    }

    /// <summary>
    /// Validates session cookie timeout and values on Manager screens
    /// If invalid calls SignOutUser()
    /// Else sets new values for session expiry
    /// </summary>
    public static void ValidateUser(PolyPage polyPage)
    {
        bool isValid = false;   //Final authenticate result

        //Get session cookies
        string strSessionExpires = PolyUtils.RequestCookie("SessionExpires");
        string strRole = PolyUtils.RequestCookie("Role");
        string userName = PolyUtils.RequestCookie("UserName");
        string systemUserId = PolyUtils.RequestCookie("SystemUserId");
        int territoryId = UniStr.Util.ValidateInt(PolyUtils.RequestCookie("TerritoryId"), 0);

        DateTime dateTimeSessionExpires = new DateTime();
        int intRole = 0;


        bool sessionExpiresValid = false;
        if (strSessionExpires == "0")
        {
            sessionExpiresValid = true;
        }
        else
        {
            try
            {
                dateTimeSessionExpires = new DateTime(Convert.ToInt64(strSessionExpires));
                if (DateTime.Now.CompareTo(dateTimeSessionExpires) <= 0)
                {//Last activity less then session expiry time
                    sessionExpiresValid = true;
                }
            }
            catch
            {
                sessionExpiresValid = false;
            }
        }

        if (sessionExpiresValid)
        {
            if (Int32.TryParse(strRole, out intRole))
            {//Role is integer
                if (intRole >= -1 && intRole <= 4)
                {//Role values between -1 to 4
                    if (userName != "")
                    {//User name is not empty
                        isValid = true;     //Only if reaches this line user is authenticated
                    }
                }
            }
        }


        if (isValid)
        {
            SetAuthentication(intRole, userName, systemUserId, territoryId);
            polyPage.CurrentUser.currentRoleType = ConvertRoleIdToType(intRole);
            polyPage.CurrentUser.UserName = userName;
            polyPage.CurrentUser.SystemUserId = Convert.ToInt32(systemUserId);
            polyPage.CurrentUser.TerritoryId = territoryId;
        }
        else
        {
            SignOutUser();
        }

    }

    public static void SignOutUser()
    {
        PolyUtils.ResponseCookie("SessionExpires", "loggedout");
        PolyUtils.ResponseCookie("Role", "");
        PolyUtils.ResponseCookie("UserName", "");
        PolyUtils.ResponseCookie("TerritoryId", "");
        HttpContext.Current.Response.Write("SignOutUser<br>");
        HttpContext.Current.Response.Redirect("~/", false);

    }

    public static UserRoleType ConvertRoleIdToType(int roleId)
    {
        switch (roleId)
        {
            case -1:
                return UserRoleType.PolytexAdmin;
            case 0:
                return UserRoleType.Unidentified;
            case 1:
                return UserRoleType.SystemAdmin;
            case 2:
                return UserRoleType.Technician;
            default:
                return UserRoleType.Unidentified;
        }
    }

    /// <summary>
    /// Gets system users types from Settings.config
    /// </summary>
    /// <returns></returns>
    public static DataTable GetSystemUsersRoles(bool includePolytexAdmin, string UserRole)
    {
        if (UserRole.Equals("Technician"))
        {
            DataTable dt = new DataTable("SYSTEM_USER_PERMISSIONS_LIST");
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            DataRow dr = dt.NewRow();
            dr["ID"] = 2;
            dr["Name"] = DBcon._T["SystemUserTechnician"];
            dt.Rows.Add(dr);
            return dt;
        }
        else
        {
            String[] arrNames = Enum.GetNames(typeof(UserRoleType));
            int[] arrValues = new int[arrNames.Length];
            int x = 0;
            foreach (UserRoleType userRoleTypeName in Enum.GetValues(typeof(UserRoleType)))
            {
                arrValues[x++] = (int)userRoleTypeName;
            }



            DataTable dtRoles = PolytexData.DBcon.get_SystemUserPermissionsList(arrNames, arrValues, includePolytexAdmin);

            if (UserRole.Equals("PolytexAdmin"))
            {
                DataRow dr = dtRoles.NewRow();
                dr["ID"] = -1;
                dr["Name"] = "Polytex Admin";
                dtRoles.Rows.Add(dr);
            }

            return dtRoles;
        }
    }

    public static DataTable GetSystemUsersRoles(bool includePolytexAdmin)
    {
            String[] arrNames = Enum.GetNames(typeof(UserRoleType));
            int[] arrValues = new int[arrNames.Length];
            int x = 0;
            foreach (UserRoleType userRoleTypeName in Enum.GetValues(typeof(UserRoleType)))
            {
                arrValues[x++] = (int)userRoleTypeName;
            }



            DataTable dtRoles = PolytexData.DBcon.get_SystemUserPermissionsList(arrNames, arrValues, includePolytexAdmin);

            return dtRoles;
    }

    #endregion

    #region PROPERTIES

    public UserRoleType CurrentRoleType
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

    public string UserName
    {
        set
        {
            userName = value;
        }
        get
        {
            return userName;
        }
    }

    public int SystemUserId
    {
        set
        {
            systemUserId = value;
        }
        get
        {
            return systemUserId;
        }
    }

    public int TerritoryId
    {
        set
        {
            territoryId = value;
        }
        get
        {
            return territoryId;
        }
    }

    private static string PasswordDynamic
    {
        get
        {
            DateTime now = DateTime.Now;

            string strDate = now.Year.ToString().Substring(2, 2);
            strDate += now.Month.ToString().Length == 1 ? "0" + now.Month.ToString() : now.Month.ToString();
            strDate += now.Day.ToString().Length == 1 ? "0" + now.Day.ToString() : now.Day.ToString();
            return strDate;
        }
    }

    #endregion
}
