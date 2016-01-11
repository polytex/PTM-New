<%@ Page Language="C#" MasterPageFile="~/Popups/Popups.master" AutoEventWireup="true" CodeFile="Clients.aspx.cs" Inherits="Popups_Clients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
function OpenLink(linkNum)
{
    if (linkNum=="1")
    {
        var link = document.getElementById("<%=TextBoxLink1.ClientID %>").value;
    }
    else
    {
        var link = document.getElementById("<%=TextBoxLink2.ClientID %>").value;
    }
    if (link!="")
    {
        window.open(link, '_blank', 'toolbar=0,location=0,menubar=0');
    }
    
    
}
</script>
<div class="DivReportPopup" style="height:326px;width:438px">
    <table cellpadding="0" cellspacing="0" border="0" class="TableFieldsStretched">
    <tr style="height:28px;">                                
        <td>
            <table cellpadding="0" cellspacing="0" border="0" style="vertical-align:top;">
            <tr>             
                <td>
                    <Polytex:CustomImageButton ID="CustomImageButtonUpdate" runat="server" ImageAlign="Left" CustomButtonType="Update" CommandName="Update" ValidationGroup="AddItem" MouseEffect="True" OnClick="ButtonUpdateClientDetails" />
                </td>

            </tr>
            <tr>        
                <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelDetails" runat="server" Trans="Details"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                <td>
                    <asp:TextBox ID="TextBoxDetails" runat="server" TextMode="MultiLine" Rows="14" dir="ltr" InputType="String" CssClass="GridEditItemContent" IsRequired="false" maxlength="500" ValidationGroup="AddItem"  style="text-align:left" Width="350"></asp:TextBox>
                </td>
            </tr> 
            <tr>        
                <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelLink1" runat="server" Trans="Link"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                <td>
                    <asp:TextBox ID="TextBoxLink1" runat="server" IsRequired="false" ValidationGroup="AddItem" Width="350" ></asp:TextBox>
                </td>
                <td><Polytex:CustomImageButton ID="CustomImageButtonLink1" runat="server" ImageAlign="Left" CustomButtonType="ShowToolButtons" CommandName="ShowToolButtons" MouseEffect="True" OnClientClick="OpenLink('1');return false;" /></td>
            </tr>  
            <tr>        
                <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelLink2" runat="server" Trans="Link"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                <td>
                    <asp:TextBox ID="TextBoxLink2" runat="server" IsRequired="false" ValidationGroup="AddItem" Width="350"></asp:TextBox>
                </td>
                    <td><Polytex:CustomImageButton ID="CustomImageButtonLink2" runat="server" ImageAlign="Left" CustomButtonType="ShowToolButtons" CommandName="ShowToolButtons" MouseEffect="True" OnClientClick="OpenLink('2');return false;" /></td>
            </tr>           
            </table>
        </td>                                               
    </tr>                     
    </table>
</div>   

</asp:Content>

