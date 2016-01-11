<%@ Page Language="C#" MasterPageFile="~/Popups/Popups.master" AutoEventWireup="true" CodeFile="Confirm.aspx.cs" Inherits="Popups_Confirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
function ReturnConfirm(valReturn)
{
    parent.confirmReturnValue = valReturn;
    parent.objConfirmButton.click();
}
</script>
<table cellpadding="0" cellspacing="0" border="0" style="width:100%;" align="center">
<tr>
    <td style="text-align:center;line-height:36px;"><%=confirmMessage %></td>
</tr>
<tr>
    <td style="text-align:center;height:26px;">
        <input type="button" title="<%=PolyUtils._T["Cancel"] %>" value="<%=PolyUtils._T["Cancel"] %>" onclick="ReturnConfirm(false)" style="padding-top:2px;padding-bottom:2px;text-align:<%=UniStr.UniLang.UniAlign %>;padding-<%=UniStr.UniLang.UniAlign %>:20px;padding-<%=UniStr.UniLang.UniAlignReverse %>:0px;background-image:url('../Images/Buttons/cancel.gif');background-repeat:no-repeat;background-position:<%=UniStr.UniLang.UniAlign %>" />
        &nbsp;
        <input type="button" title="<%=PolyUtils._T["OK"] %>" value="<%=PolyUtils._T["OK"] %>" onclick="ReturnConfirm(true)" style="padding-top:2px;padding-bottom:2px;text-align:<%=UniStr.UniLang.UniAlign %>;padding-<%=UniStr.UniLang.UniAlign %>:20px;padding-<%=UniStr.UniLang.UniAlignReverse %>:0px;background-image:url('../Images/Buttons/confirm.gif');background-repeat:no-repeat;background-position:<%=UniStr.UniLang.UniAlign %>" />
    </td>
</tr>
</table>
</asp:Content>

