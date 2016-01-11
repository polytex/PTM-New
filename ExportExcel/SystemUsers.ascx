<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SystemUsers.ascx.cs" Inherits="ExportExcel_SystemUsers" %>

<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_System_Users">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="systemUserId" Type="Int32" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<Table ss:ExpandedColumnCount="100" ss:ExpandedRowCount="<%=(RepeaterExportReport.Items.Count + 1).ToString() %>" x:FullColumns="1" x:FullRows="1">
<asp:Repeater ID="RepeaterExportReport" runat="server" DataSourceID="ObjectDataSource1">
    <HeaderTemplate>
       <Row ss:StyleID="s20">
              <Cell><Data ss:Type="String">#</Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["Name"]%></Data></Cell>     
              <Cell><Data ss:Type="String"><%=_T["Territory"]%></Data></Cell>   
              <Cell><Data ss:Type="String"><%=_T["UserName"]%></Data></Cell>   
              <Cell><Data ss:Type="String"><%=_T["Permission"]%></Data></Cell>          
       </Row>
    </HeaderTemplate>  
        <ItemTemplate>
        <Row>
            <Cell><Data ss:Type="Number"><%# RepeaterRowCounter%></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("NAME").ToString() %></Data></Cell>   
            <Cell><Data ss:Type="String"><%# Eval("TERRITORY_ID").ToString()=="-1" ? "Worldwide" : Eval("TERRITORY_NAME").ToString() %></Data></Cell>      
            <Cell><Data ss:Type="String"><%# Eval("LOGIN").ToString()%></Data></Cell>      
            <Cell><Data ss:Type="String"><%# GetRoleName(Eval("USER_TYPE_ID")) %></Data></Cell>                             
        </Row>
    </ItemTemplate>    
</asp:Repeater>  
</Table>
