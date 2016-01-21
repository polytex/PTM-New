<%@ Page Language="C#" MasterPageFile="~/Popups/Popups.master" AutoEventWireup="true" CodeFile="ReportActivityGroup.aspx.cs" Inherits="Popups_ReportActivityGroup" Title="Untitled Page" %>
<%@ Register Src="~/Controls/QueryFromToDate.ascx" TagName="QueryFromToDate" TagPrefix="Polytex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ObjectDataSource ID="ObjectDataSourceTerritory" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Territories">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="false" Name="includeWorldwide" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>


<div class="DivReportPopup" style="<%=PolyPage.IsMobile ? "height:206px;width:278px" : "height:305px;" %>">
    <table cellpadding="0" cellspacing="0" border="0" class="TableFieldsStretched">
    <tr id="trQueryFromToDate" runat="server">
        <td>
            <Polytex:QueryFromToDate ID="QueryFromToDate1" runat="server" StartTabIndex="2" />            
        </td>
    </tr>
        <tr style="height:28px;">                                
        <td>
            <table cellpadding="0" cellspacing="0" border="0" >
            <tr>        
                <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelTerritory" runat="server" Trans="Territory"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                <td>
                    <Polytex:DropDownList ID="DropDownListTerritoryId" runat="server" DataSourceID="ObjectDataSourceTerritory" DropDownListWidth="85px" DataTextField="NAME" DataValueField="ID" InsertEmptyItem="true" IsRequired="false" ValidationGroup="ReportQuery" ValidatorsPosition="Vertical"></Polytex:DropDownList>
                </td>
            </tr>
            <tr>        
                <td colspan="2" style="padding-<%=CultureAlignReverse %>:12px;"><Polytex:CheckBox ID="CheckBoxIncludeDisabled" runat="server" Trans="IncludeDisabled" Checked="false" /></td>              
            </tr>             
            </table>
        </td>                                               
    </tr>                       
    <tr>
        <td class="CultureReverseAlign" style="padding:0px 20px 0px 20px;height:26px;"><Polytex:Button ID="Button1" runat="server" Trans="ShowReport" ValidationGroup="ReportQuery" TabIndex="5" /></td>        
    </tr>

    </table>
</div>    
</asp:Content>
