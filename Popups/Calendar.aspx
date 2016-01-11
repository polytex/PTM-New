<%@ Page Language="C#" MasterPageFile="~/Popups/Popups.master" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="Popups_Calendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
<asp:PlaceHolder ID="PlaceHolderDateSelected" runat="server" Visible="false">
<script type="text/javascript">
parent.document.getElementById("<%=textBoxId %>").value = "<%=Calendar1.SelectedDate.ToShortDateString() %>";
parent.ClosePopup();
</script>
</asp:PlaceHolder>

<script type="text/javascript">
function SelectDate(objCell)
{            
    if (IsClientBrowserIE() == "ie")
    {        
        objCell.firstChild.click();
    }
    else
    {
       //objCell.firstChild.fireEvent("click");
    }
}

function SelectedDateClicked()
{
    parent.document.getElementById("<%=textBoxId %>").value = "<%=Calendar1.SelectedDate.ToShortDateString() %>";
    parent.ClosePopup();    
    return false;
}
</script>
<table cellpadding="0" cellspacing="0" border="0" style="width:280px;direction:ltr;height:24px;">
<tr style="background-color:#DBDBFF;">
    <td style="width:22px;text-align:center;"><Polytex:ImageButton ID="PolytexImageButtonDoubleLeft" runat="server" ImageAlign="Middle" ImageUrl="../Images/Buttons/LeftDoubleArrow.gif" OnCommand="PolytexImageButtonNavigation_Command" CssClass="NavigationButton" /></td>
    <td style="width:22px;text-align:center;"><Polytex:ImageButton ID="PolytexImageButtonLeft" runat="server" ImageAlign="Middle" ImageUrl="../Images/Buttons/LeftArrow.gif" OnCommand="PolytexImageButtonNavigation_Command" CssClass="NavigationButton" /></td>
    <td class="CultureDirection" style="text-align:center;">
        <asp:DropDownList ID="DropDownListMonth" ForeColor="#404040" Font-Bold="true" runat="server" EnableViewState="true" AutoPostBack="True" OnSelectedIndexChanged="DropDownListNavigation_SelectedIndexChanged"></asp:DropDownList>&nbsp;
        <asp:DropDownList ID="DropDownListYear" ForeColor="#404040" Font-Bold="true" runat="server" EnableViewState="true" AutoPostBack="True" OnSelectedIndexChanged="DropDownListNavigation_SelectedIndexChanged"></asp:DropDownList>
    </td>        
    <td style="width:22px;text-align:center;"><Polytex:ImageButton ID="PolytexImageButtonRight" runat="server" ImageAlign="Middle" ImageUrl="../Images/Buttons/RightArrow.gif" OnCommand="PolytexImageButtonNavigation_Command" CssClass="NavigationButton" /></td>    
    <td style="width:22px;text-align:center;"><Polytex:ImageButton ID="PolytexImageButtonDoubleRight" runat="server" ImageAlign="Middle" ImageUrl="../Images/Buttons/RightDoubleArrow.gif" OnCommand="PolytexImageButtonNavigation_Command" CssClass="NavigationButton" /></td>
</tr>
</table>
<asp:Calendar ID="Calendar1" runat="server" Height="141px" ShowTitle="False" Width="279px" CssClass="Calendar PanelBG" OnDayRender="Calendar1_DayRender" ShowGridLines="True" OnPreRender="Calendar1_PreRender" BorderStyle="Solid" BorderWidth="1px" OnSelectionChanged="Calendar1_SelectionChanged">
    <TodayDayStyle BackColor="DarkGray" />
    <OtherMonthDayStyle ForeColor="DarkGray" />
    <DayStyle CssClass="CalendarDayStyle" ForeColor="Navy" />
    <DayHeaderStyle ForeColor="#404040" />
    <SelectedDayStyle ForeColor="Navy" BackColor="#FFC080" />
</asp:Calendar>
</asp:Content>

