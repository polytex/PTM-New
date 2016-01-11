<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Clients.ascx.cs" Inherits="ExportExcel_Clients" %>

<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Clients">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="" Name="clientName" Type="String" />
        <asp:Parameter DefaultValue="" Name="version" Type="String" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<Table ss:ExpandedColumnCount="100" ss:ExpandedRowCount="<%=(RepeaterExportReport.Items.Count + 1).ToString() %>" x:FullColumns="1" x:FullRows="1">
<asp:Repeater ID="RepeaterExportReport" runat="server" DataSourceID="ObjectDataSource1">
    <HeaderTemplate>
       <Row ss:StyleID="s20">
              <Cell><Data ss:Type="String">#</Data></Cell>        
              <Cell><Data ss:Type="String"><%=_T["Territory"]%></Data></Cell>            
              <Cell><Data ss:Type="String"><%=_T["Client"]%></Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["Version"]%></Data></Cell>            
       </Row>
    </HeaderTemplate>  
        <ItemTemplate>
        <Row>
            <Cell><Data ss:Type="Number"><%# RepeaterRowCounter%></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("TERRITORY_NAME").ToString() %></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("NAME")%></Data></Cell>                                
            <Cell><Data ss:Type="String"><%# Eval("VERSION").ToString() %></Data></Cell>                                        
        </Row>
    </ItemTemplate>    
</asp:Repeater>  
</Table>
