<%@ Page Language="C#" MasterPageFile="~/Popups/Popups.master" AutoEventWireup="true" CodeFile="ReportUnits.aspx.cs" Inherits="Popups_ReportUnits"%>
<%@ Register Src="~/Controls/QueryFromToDate.ascx" TagName="QueryFromToDate" TagPrefix="Polytex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<Polytex:ObjectDataSource ID="ObjectDataSourceClient" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Clients">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<div class="DivReportPopup" style="<%=PolyPage.IsMobile ? "height:236px;width:278px" : "height:305px;" %>">
    <table cellpadding="0" cellspacing="0" border="0" class="TableFieldsStretched">
    <tr id="trQueryFromToDate" runat="server">
        <td>
            <Polytex:QueryFromToDate ID="QueryFromToDate1" runat="server" StartTabIndex="2" />            
        </td>
    </tr> 
    <tr style="height:28px;">                                
        <td>
            <table cellpadding="0" cellspacing="0" border="0" style="vertical-align:top;">
            <tr>        
                <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelTerritory" runat="server" Trans="Territory"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                <td>
                    <Polytex:DropDownList ID="DropDownListTerritoryId" runat="server" DropDownListWidth="140px" DataTextField="NAME" DataValueField="ID" IsRequired="false" ValidationGroup="ReportQuery" ValidatorsPosition="Vertical"></Polytex:DropDownList>
                </td>
            </tr> 
            <tr>        
                <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelClient" runat="server" Trans="Client"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                <td>
                    <Polytex:DropDownList ID="DropDownListClientId" runat="server" DropDownListWidth="140px" DataSourceID="ObjectDataSourceClient" DataTextField="NAME" DataValueField="ID" InsertEmptyItem="true" IsRequired="false" ValidationGroup="ReportQuery" ValidatorsPosition="Vertical"></Polytex:DropDownList>
                </td>
            </tr>           
            </table>
        </td>                                               
    </tr>                     
    <tr>
        <td class="CultureReverseAlign" style="padding:0px 60px 0px 60px;height:26px;"><Polytex:Button ID="Button1" runat="server" Trans="ShowReport" ValidationGroup="ReportQuery" TabIndex="5" /></td>        
    </tr>

    </table>
</div>    
</asp:Content>