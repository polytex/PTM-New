<%@ Page Language="C#" MasterPageFile="~/Popups/Popups.master" AutoEventWireup="true" CodeFile="ReportActivities.aspx.cs" Inherits="Popups_ReportActivities "  %>
<%@ Register Src="~/Controls/QueryFromToDate.ascx" TagName="QueryFromToDate" TagPrefix="Polytex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ObjectDataSource ID="ObjectDataSourceModel" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Models">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>

<Polytex:ObjectDataSource ID="ObjectDataSourceUnit" runat="server" SelectMethod="SelectByClient" TypeName="PolytexData.Manage_Units">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="clientId" Type="Int32" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<Polytex:ObjectDataSource ID="ObjectDataSourceActivityGroups" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Activity_Groups">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<Polytex:ObjectDataSource ID="ObjectDataSourceActivityTypes" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Activity_Types">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
            <asp:Parameter DefaultValue="0" Name="ActivityGroupId" Type="Int32" />
        </SelectParameters>
</Polytex:ObjectDataSource>

<Polytex:ObjectDataSource ID="ObjectDataSourceSystemUsers" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_System_Users" IsEmpty="False" Skip="0">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<div class="DivReportPopup" style="<%=PolyPage.IsMobile ? "height:406px;width:278px" : "height:406px;" %>">
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
                    <Polytex:DropDownList ID="DropDownListTerritoryId" runat="server" DropDownListWidth="150px" DataTextField="NAME" DataValueField="ID"   IsRequired="false" ValidationGroup="ReportQuery" ValidatorsPosition="Vertical"></Polytex:DropDownList>
                </td>
            </tr>
            <tr>        
                <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelClient" runat="server" Trans="Client"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                <td>
                    <Polytex:DropDownList ID="DropDownListClientId" runat="server" DropDownListWidth="150px" DataTextField="NAME" DataValueField="ID" IsRequired="false" ValidationGroup="ReportQuery" ValidatorsPosition="Vertical"></Polytex:DropDownList>
                </td>
            </tr> 
            <tr>        
                <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="Label1" runat="server" Trans="Unit"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                <td>
                    <Polytex:DropDownList ID="DropDownListUnitId" runat="server" DropDownListWidth="150px" DataSourceID="ObjectDataSourceUnit" DataTextField="UNIT_NAME" DataValueField="ID" InsertEmptyItem="true" IsRequired="false" ValidationGroup="ReportQuery" ValidatorsPosition="Vertical"></Polytex:DropDownList>
                </td>
            </tr>  
            <tr>        
                <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelModel" runat="server" Trans="Model"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                <td>
                    <Polytex:DropDownList ID="DropDownListModelId" runat="server" DropDownListWidth="150px" DataSourceID="ObjectDataSourceModel" DataTextField="NAME" DataValueField="ID" InsertEmptyItem="true" IsRequired="false" ValidationGroup="ReportQuery" ValidatorsPosition="Vertical"></Polytex:DropDownList>
                </td>
            </tr>
            <tr>        
                <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelActivityGroup" runat="server" Trans="ActivityGroupName"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                <td>
                    <Polytex:DropDownList ID="DropDownListActivityGroupId" runat="server" DropDownListWidth="150px" DataTextField="NAME" DataValueField="ID" IsRequired="false" ValidationGroup="ReportQuery" ValidatorsPosition="Vertical"></Polytex:DropDownList>
                </td>
            </tr>
            <tr>        
                <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelActivityType" runat="server" Trans="ActivityType"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                <td>
                    <Polytex:DropDownList ID="DropDownListActivityTypeId" runat="server" DropDownListWidth="150px" DataSourceID="ObjectDataSourceActivityTypes" DataTextField="NAME" DataValueField="ID" InsertEmptyItem="true" IsRequired="false" ValidationGroup="ReportQuery" ValidatorsPosition="Vertical"></Polytex:DropDownList>
                </td>
            </tr> 
            <tr>        
                <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelSystemUser" runat="server" Trans="Technician"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                <td>
                    <Polytex:DropDownList ID="DropDownListSystemUserId" runat="server" DropDownListWidth="150px" DataSourceID="ObjectDataSourceSystemUsers" DataTextField="NAME" DataValueField="ID" InsertEmptyItem="true" IsRequired="false" ValidationGroup="ReportQuery" ValidatorsPosition="Vertical"></Polytex:DropDownList>
                </td>
            </tr>              
            <tr>        
                <td style="vertical-align:top;"><div style="padding-top:6px"><Polytex:Label ID="LabelIncludeImage" runat="server" Trans="IncludeImage"></Polytex:Label>:&nbsp;&nbsp;</div></td>
                <td>
                    <asp:Checkbox ID="CheckBoxIncludeImage" name="Image" value="true" runat="server"></asp:Checkbox>
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


