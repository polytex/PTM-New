<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="PolytexControls" Namespace="PolytexControls" TagPrefix="Polytex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
//window.location = SiteURL; 
function DefaultOnLoad()
{
    //Network address ok but does not match SiteURL
    if(this.document.location.toString().indexOf(SiteURL) == -1)
    {
        document.location = SiteURL;
    }
        
    //Align footer     
     document.getElementById("<%=TextBoxUserName.AspTextBoxControl.ClientID %>").focus();           
}
</script>

<table cellpadding="0" cellspacing="0" class="CultureAlign PanelBorder" style="<%=PolyPage.IsMobile ? "margin:-50px 50px 50px 0px;border-collapse:collapse;" : "margin:50px 50px 50px 500px;border-collapse:collapse;" %>" align="center">
	<tr>
		<td colspan="2" align="center" style="background-image: url(Images/PanelHeaderBG.gif);color:white;font-weight:bold;white-space:nowrap;padding:5px"><Polytex:Label ID="LabelLoginHeader" runat="server" Trans="Login_SystemLogin" Category="ColumnHeader" /></td>
	</tr>
	<tr>
		<td colspan="2" style="height:20px;line-height:1px;font-size:1px;background-color: #f5f4ed;">&nbsp;</td>
	</tr>
	<tr style="background-color:#f5f4ed;">
		<td style="<%=PolyPage.IsMobile ? "padding:4px 0px 4px 4px;" : "padding:4px 20px 4px 20px;" %>"><Polytex:Label ID="LabelUserName" runat="server" Trans="Login_UserName" Category="FormInput" AssociatedControlID="TextBoxUserName" />: </td>
		<td style="padding:4px 20px 4px 20px;"><Polytex:TextBox ID="TextBoxUserName" ValidatorDisplay="Static" InputType="String" runat="server" IsRequired="true" DisplayRequiredAsAsterix="true" TextBoxCssClass="AlignCenter" ValidationGroup="Login" ValidatorsPosition="Horizontal" AutoCompleteType="DisplayName" TextBoxMaxLength="20" /></td>
	</tr>
	<tr style="background-color:#f5f4ed;">
		<td style="<%=PolyPage.IsMobile ? "padding:4px 0px 4px 4px;" : "padding:4px 20px 4px 20px;" %>"><Polytex:Label ID="LabelPassword" runat="server" Trans="Login_Password" Category="FormInput" AssociatedControlID="TextBoxPassword" />:</td>
		<td style="padding:4px 20px 4px 20px;"><Polytex:TextBox ID="TextBoxPassword" ValidatorDisplay="Static" InputType="Password" runat="server" IsRequired="true" DisplayRequiredAsAsterix="true" TextBoxCssClass="AlignCenter" ValidationGroup="Login" ValidatorsPosition="Horizontal" TextBoxMaxLength="13" /></td>
	</tr>
	<tr style="background-color:#f5f4ed;text-align:center;">
		<td style="<%=PolyPage.IsMobile ? "padding:4px 0px 4px 4px;" : "padding:4px 20px 4px 20px;" %>">
		    <input type="reset" value="<%=_T["Login_btnReset"]%>" />
		</td>		
		<td style="<%=PolyPage.IsMobile ? "padding:4px 0px 4px 4px;" : "padding:4px 20px 4px 20px;" %>">	            
            <Polytex:Button ID="ButtonLogin" runat="server" Trans="Login_btnLogin" UseSubmitBehavior="true" ValidationGroup="Login" OnClick="ButtonLogin_Click" />            
         </td>
	</tr>
	<tr style="background-color:#f5f4ed;text-align:center;">
		<td class="CultureAlign" colspan="2" style="padding:4px 20px 4px 20px;">
            <Polytex:Label ID="LabelAllowCookies" runat="server" Visible="false" Trans="AllowCookies" Category="Validator"></Polytex:Label>
            <Polytex:Label ID="LabelLoginFail" runat="server" Visible="false" Trans="LoginFailed" Category="Validator"></Polytex:Label>
            <Polytex:Label ID="LabelLoginFailMAC" runat="server" Visible="false" Trans="LoginFailedMAC" Category="Validator"></Polytex:Label>
		</td>				
	</tr>	
</table>
</asp:Content>

