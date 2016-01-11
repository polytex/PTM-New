<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Models.ascx.cs" Inherits="ExportExcel_Models" %>

<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="PolytexData.Manage_Models">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<Table ss:ExpandedColumnCount="100" ss:ExpandedRowCount="<%=(RepeaterExportReport.Items.Count + 1).ToString() %>" x:FullColumns="1" x:FullRows="1">
<asp:Repeater ID="RepeaterExportReport" runat="server" DataSourceID="ObjectDataSource1">
    <HeaderTemplate>
       <Row ss:StyleID="s20">
              <Cell><Data ss:Type="String">#</Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["Model"]%></Data></Cell>            
       </Row>
    </HeaderTemplate>  
        <ItemTemplate>
        <Row>
            <Cell><Data ss:Type="Number"><%# RepeaterRowCounter%></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("NAME").ToString() %></Data></Cell>                          
        </Row>
    </ItemTemplate>    
</asp:Repeater>  
</Table>