<%@ Page Language="C#" MasterPageFile="~/Popups/Popups.master"  AutoEventWireup="true" CodeFile="Units.aspx.cs" Inherits="Popups_Units" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<input id="ReportName" name="ReportName" type="hidden" value="<%=CurrentPage %>" />

<div class="DivReportPopup" style="<%=PolyPage.IsMobile ? "height:106px;width:228px" : "height:305px;" %>">
    <table cellpadding="0" cellspacing="0" border="0" class="TableFieldsStretched">
        <tr style="height:28px;">                                
          <td>
            <table cellpadding="0" cellspacing="0" border="0" style="vertical-align:top;">
                 <tr>        
                    <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelClient" runat="server" Trans="Client"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                    <td><asp:DropDownList ID="DropDownListClientId" runat="server" Width="150px" DataTextField="NAME" DataValueField="ID" IsRequired="false" ValidationGroup="ReportQuery" ValidatorsPosition="Vertical" ></asp:DropDownList></td>
                 </tr> 
                 <tr>        
                    <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelSerial" runat="server" Trans="Serial"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                    <td><asp:TextBox ID="TextBoxSerial" InputType="String" runat="server" Width="150px" IsRequired="false"></asp:TextBox></td>
                 </tr>            
            </table>
         </td>                                               
       </tr>                     
       <tr>
          <td class="CultureReverseAlign" style="padding:0px 40px 0px 60px;height:15px;"><Polytex:Button ID="Button2" runat="server" Trans="Find" ValidationGroup="ReportQuery" TabIndex="5" OnClientClick="FindUnit()"/></td>        
       </tr>

    </table>
</div>    
</asp:Content>