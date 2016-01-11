<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridViewPager.ascx.cs" Inherits="Controls_GridViewPager" %>
<script type="text/javascript">
function SkipResults(skip)
{
    var hiddenSkip = document.getElementById("<%=TextBoxSkip.ClientID %>");
    hiddenSkip.value = parseInt(hiddenSkip.value) + parseInt(skip);    
}
</script>
<input id="TextBoxSkip" name="TextBoxSkip" runat="server" type="hidden" />
<table cellpadding="0" cellspacing="0" border="0" class="GridPagerInsideTable" style="border-top-width:0px;width:100%;">
<tr>
    <td id="tdLeftSkip" runat="server" style="width:20px;padding:0px; height: 22px;">                
        <Polytex:ImageButton ID="PolytexImageButtonLeftSkip" runat="server" ImageUrl="~/Images/Buttons/LeftDoubleArrow.gif" CommandName="SkipResults" CommandArgument='<%#(CultureDirection == "ltr" ? "SkipPrevious" : "SkipNext") %>' Width="20px" Tooltip='<%#(CultureDirection == "ltr" ? "SkipToPreviousResults" : "SkipToNextResults") %>' MouseEffect="true" OnPreRender="PolytexImageButtonSkip_PreRender" OnClientClick='<%#(CultureDirection == "ltr" ? "SkipResults(-1)" : "SkipResults(1)") %>' OnClick="PolytexImageSkipButtonClick" />
    </td>    
    <td style="width:20px;padding:0px; height: 22px;">                
        <Polytex:ImageButton ID="PolytexImageButtonLeftEnd" runat="server" ImageUrl="~/Images/Buttons/LeftEndArrow.gif" CommandName="Page" CommandArgument='<%#(CultureDirection == "ltr" ? "First" : "Last") %>' Width="20px" Trans='<%#(CultureDirection == "ltr" ? "FirstPage" : "LastPage") %>' MouseEffect="true" OnClick="PolytexImageButtonArrowClick" />
    </td>
    <td style="width:20px;padding:0px; height: 22px;">                
        <Polytex:ImageButton ID="PolytexImageButtonLeft" runat="server" ImageUrl="~/Images/Buttons/LeftArrow.gif" CommandName="Page" CommandArgument='<%#(CultureDirection == "ltr" ? "Prev" : "Next") %>' Width="20px" Trans='<%#(CultureDirection == "ltr" ? "PrevPage" : "NextPage") %>' MouseEffect="true" OnClick="PolytexImageButtonArrowClick" />
    </td>
    <td style="text-align:center; height: 22px;">                                
        <asp:DropDownList id="DropDownListPager" AutoPostBack="true" OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged" runat="server" CssClass="CultureAlign" />
    </td>   
    <td style="width:20px;padding:0px; height: 22px;">
        <Polytex:ImageButton ID="PolytexImageButtonRight" runat="server" ImageUrl="~/Images/Buttons/RightArrow.gif" CommandName="Page" CommandArgument='<%#(CultureDirection == "ltr" ? "Next" : "Prev") %>' Width="20px" Trans='<%#(CultureDirection == "ltr" ? "NextPage" : "PrevPage") %>' MouseEffect="true" OnClick="PolytexImageButtonArrowClick" />
    </td>
    <td style="width:20px;padding:0px; height: 22px;">
        <Polytex:ImageButton ID="PolytexImageButtonRightEnd" runat="server" ImageUrl="~/Images/Buttons/RightEndArrow.gif" CommandName="Page" CommandArgument='<%#(CultureDirection == "ltr" ? "Last" : "First") %>' Width="20px" Trans='<%#(CultureDirection == "ltr" ? "LastPage" : "FirstPage") %>' MouseEffect="true" OnClick="PolytexImageButtonArrowClick"  />
    </td>
    <td id="tdRightSkip" runat="server" style="width:20px;padding:0px; height: 22px;">
        <Polytex:ImageButton ID="PolytexImageButtonRightSkip" runat="server" ImageUrl="~/Images/Buttons/RightDoubleArrow.gif" CommandName="SkipResults" CommandArgument='<%#(CultureDirection == "ltr" ? "SkipNext" : "SkipPrevious") %>' Width="20px" Tooltip='<%#(CultureDirection == "ltr" ? "SkipToNextResults" : "SkipToPreviousResults") %>' MouseEffect="true" OnPreRender="PolytexImageButtonSkip_PreRender" OnClientClick='<%#(CultureDirection == "ltr" ? "SkipResults(1)" : "SkipResults(-1)") %>' OnClick="PolytexImageSkipButtonClick" />
    </td>    
</tr>            
</table>        
        
