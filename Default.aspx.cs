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

public partial class _Default : PolyPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.AuthenticationRequired = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!Page.IsPostBack)
        {//Not PostBack
            PolyUtils.ResponseCookie("AllowsCookies", "1", DateTime.Now.AddYears(5));
        }
        else
        {
            //Check if browser allows cookies
            if (PolyUtils.RequestCookie("AllowsCookies") != "1")
            {
                LabelAllowCookies.Visible = true;
            }
            else
            {
                LabelAllowCookies.Visible = false;
            }
        }
    }

    protected void ButtonLogin_Click(object sender, EventArgs e)
    {
        Page.Validate("Login");

        if (Page.IsValid)
        {
            //Input data
            string loginUserName = TextBoxUserName.Text;
            string loginPassword = TextBoxPassword.Text;


            int roleId;             //Unidentified role
            int territoryId;
            string userName = "";   //Private name
            int systemUserId;

            if (SystemUser.AuthenticateLogin(loginUserName, loginPassword, out roleId, out userName, out systemUserId, out territoryId))
            {
                SystemUser.SetAuthentication(roleId, userName, systemUserId.ToString(), territoryId);
                if (roleId == 2)
                {
                    Response.Redirect("~/ActivityDetails.aspx?activityId=0", false);
                }
                else
                {
                    Response.Redirect("~/Welcome.aspx", false);
                }
                
            }
            else
            {
                LabelLoginFail.Visible = true;                
            }
        }
    }

}
