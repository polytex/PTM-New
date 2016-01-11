<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportHeader.ascx.cs" Inherits="Controls_ReportHeader" %>
<table cellpadding="0" cellspacing="0" border="0" style="font-weight:bold;font-size:14px;line-height:0.6cm;">
<tr>
    <td id="tdLogo" runat="server" style="padding-<%=UniStr.UniLang.UniAlignReverse %>:4px;"><img src="Images/Icon.gif" width="16" height="16" alt="Polytex" title="Polytex" /></td>
    <td><Polytex:Label ID="LabelPolytex" runat="server" Trans="Polytex"></Polytex:Label></td>
    <td>&nbsp;-&nbsp;</td>
    <td>Polytex Tasks</td>
</tr>
</table>
