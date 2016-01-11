<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="true" CodeFile="Menu.ascx.cs" Inherits="Controls_Menu" %>
<%@ Register Assembly="PolytexControls" Namespace="PolytexControls" TagPrefix="Polytex" %>

<script type="text/javascript">
function Logout()
{
    window.location = "Default.aspx?Action=1";
}

function OverMenuItem(menuItem, menuItemTrans)
{
    menuItem.style.borderColor = "#CCCCCC";
    menuItem.style.backgroundColor = "#F1F1F1";
    window.status = "  " + menuItemTrans + "  ";
}

function OutMenuItem(menuItem)
{
    menuItem.style.borderColor = "#F5F4ED";
    menuItem.style.backgroundColor = "#F5F4ED";
    window.status = "";
}

function ClickMenuItem(fileName)
{
    window.location = fileName;
}

function MenuOnClick(button) 
{
    var menu = document.getElementById("<%=PanelMenuHeader.ClientID%>");
    var tdMenu = document.getElementById("DivMenu").parentNode;
    
    if (menu.style.visibility.toLowerCase() == "hidden")
    {
        menu.style.visibility = "visible";
        tdMenu.style.visibility = "visible";
        tdMenu.style.position = "absolute";
        tdMenu.style.top = "89.5px";
        tdMenu.style.zIndex = "1";
        
        
    }
    else
    {
        menu.style.visibility = "hidden";
        tdMenu.style.visibility = "hidden";
        tdMenu.style.position = "absolute";
        tdMenu.style.top = "89.5px";
        tdMenu.style.zIndex = "1";
    }
    
}

</script>
<div id="DivMenu" class="CultureAlign PanelBorder PanelBG" style="color:Blue;">
<asp:Panel ID="PanelMenuHeader" CssClass="CultureReverseAlign" runat="server" style="background-image:url('Images/PanelHeaderBG.gif');">
    <input type="button" value="<%=PolyUtils._T["Menu_btnLogout"]%>" style="cursor:pointer;width:88px;border:outset 2px white;margin:1px;text-align:center;padding:2px 2px 2px 2px;margin:3px 4px 4px 4px;color:Blue;" onclick="Logout();" />    
</asp:Panel>
<asp:PlaceHolder ID="PlaceHolderDisplayedMenuItems" runat="server" EnableViewState="true"></asp:PlaceHolder>
</div>



