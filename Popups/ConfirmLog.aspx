<%@ Page Language="C#" MasterPageFile="~/Popups/Popups.master" AutoEventWireup="true" CodeFile="ConfirmLog.aspx.cs" Inherits="Popups_ConfirmLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
function ReturnConfirm(valReturn)
{
    var strComment = Trim(document.getElementById("<%=TextBoxComment.ClientID %>").value);
    
    if (valReturn && strComment == "")
    {
        document.getElementById("SpanCommentRequired").style.visibility = "visible";
    }
    else
    {
        document.getElementById("SpanCommentRequired").style.visibility = "hidden";
        parent.document.getElementById("ConfirmLogComment").value = strComment;
        parent.confirmReturnValue = valReturn;
        parent.objConfirmButton.click();
    }    
}
</script>
<table cellpadding="3" cellspacing="0" border="0" style="width:100%;" align="center">
<tr>
    <td colspan="2"><%=PolyUtils._T["Comment"]%>:&nbsp;</td>
</tr>
<tr>
    <td><asp:TextBox ID="TextBoxComment" runat="server" TextMode="MultiLine" Rows="3" Columns="42"></asp:TextBox></td>
    <td style="width:70px;vertical-align:top;"><img src="../Images/Exclamation.gif" alt="" width="50" height="50"  /></td>
</tr>
<tr>
    <td colspan="2" style="padding:0px 3px 3px 3px;"><span id="SpanCommentRequired" class="Validator" style="visibility:hidden;"><%=PolyUtils._T["ItemRequired"]%></span></td>
</tr>
<tr>
    <td colspan="2"><%=PolyUtils._T["AreYouSure"] %>&nbsp;<%=userCountMessage %></td>
</tr>
<tr>
    <td colspan="2" style="text-align:center;height:26px;">
        <input type="button" title="<%=PolyUtils._T["Cancel"] %>" value="<%=PolyUtils._T["Cancel"] %>" onclick="ReturnConfirm(false)" style="padding-top:2px;padding-bottom:2px;text-align:<%=UniStr.UniLang.UniAlign %>;padding-<%=UniStr.UniLang.UniAlign %>:20px;padding-<%=UniStr.UniLang.UniAlignReverse %>:0px;background-image:url('../Images/Buttons/cancel.gif');background-repeat:no-repeat;background-position:<%=UniStr.UniLang.UniAlign %>" />
        &nbsp;
        <input type="button" title="<%=PolyUtils._T["OK"] %>" value="<%=PolyUtils._T["OK"] %>" onclick="ReturnConfirm(true)" style="padding-top:2px;padding-bottom:2px;text-align:<%=UniStr.UniLang.UniAlign %>;padding-<%=UniStr.UniLang.UniAlign %>:20px;padding-<%=UniStr.UniLang.UniAlignReverse %>:0px;background-image:url('../Images/Buttons/confirm.gif');background-repeat:no-repeat;background-position:<%=UniStr.UniLang.UniAlign %>" />
    </td>
</tr>
</table>
</asp:Content>

