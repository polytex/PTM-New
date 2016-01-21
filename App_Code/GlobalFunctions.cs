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
using PolytexObjects;
using System.Net.Sockets;
using System.Net;
using System.Text;

/// <summary>
/// Misc utils for external Polytex solution projects
/// </summary>
public class GlobalFunctions
{
    public GlobalFunctions()
    {
    }


    #region Users Functions

    public static string GetUserDetailsXml(
        string userId,
        string workerId,
        string fName,
        string lName,
        string cardNum,
        string departmentName,
        string itemsAmount,
        string amountLimit,
        string ordersInPeriod,
        string transactionsLimit,
        string limitGroupName,
        string limitGroupId)
    {
        string strUserXml = "";
        strUserXml = "<US>";
        strUserXml += "<ID>" + userId + "</ID>";

        strUserXml += "<WI>" + HttpContext.Current.Server.HtmlEncode(workerId) + "</WI>";
        strUserXml += "<FN>" + HttpContext.Current.Server.HtmlEncode(fName) + "</FN>";
        strUserXml += "<LN>" + HttpContext.Current.Server.HtmlEncode(lName) + "</LN>";
        strUserXml += "<CN>" + HttpContext.Current.Server.HtmlEncode(cardNum) + "</CN>";
        strUserXml += "<DP>" + HttpContext.Current.Server.HtmlEncode(departmentName) + "</DP>";
        strUserXml += "<IA>" + itemsAmount + "</IA>";
        strUserXml += "<AL>" + amountLimit + "</AL>";
        strUserXml += "<OP>" + ordersInPeriod + "</OP>";
        strUserXml += "<TL>" + transactionsLimit + "</TL>";
        strUserXml += "<LGN>" + HttpContext.Current.Server.HtmlEncode(limitGroupName) + "</LGN>";
        strUserXml += "<LGI>" + limitGroupId + "</LGI>";
        strUserXml += "</US>";
        return strUserXml;
    }

    #endregion



    #region Reports Functions

    public static string CalculateTotalHours(string totalMinutes)
    {
        string totalHours = "";

        decimal minutes;
        if (Decimal.TryParse(totalMinutes, out minutes))
        {
            totalHours = decimal.Round((minutes / 60), 1).ToString("F1");// (minutes / 60).ToString();
        }

        return totalHours;
    }

}

    /// <summary>
    /// Report Orders
    /// </summary>
    /// <param name="statusId"></param>
    /// <returns></returns>

    #endregion



