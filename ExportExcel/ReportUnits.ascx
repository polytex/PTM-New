<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportUnits.ascx.cs" Inherits="ExportExcel_ReportUnits" %>

<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" IsEmpty="False" SelectMethod="Units" TypeName="PolytexData.ReportsTasks">
    <SelectParameters>        
        <asp:Parameter DefaultValue="true" Name="hasMaxResults" Type="Boolean" />
        <asp:Parameter Name="skip" Type="Int32" DefaultValue="0" />
        <asp:Parameter Name="fromDate" Type="DateTime" />
        <asp:Parameter Name="toDate" Type="DateTime" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="clientId" Type="Int32" />
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<Table ss:ExpandedColumnCount="100" ss:ExpandedRowCount="<%=(RepeaterExportReport.Items.Count + 1).ToString() %>" x:FullColumns="1" x:FullRows="1">
<asp:Repeater ID="RepeaterExportReport" runat="server" DataSourceID="ObjectDataSource1">
    <HeaderTemplate>
       <Row ss:StyleID="s20">
              <Cell><Data ss:Type="String">#</Data></Cell>        
              <Cell><Data ss:Type="String"><%=_T["Territory"]%></Data></Cell>            
              <Cell><Data ss:Type="String"><%=_T["Client"]%></Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["Station_Name"]%></Data></Cell>            
              <Cell><Data ss:Type="String"><%=_T["Total_Hours"]%></Data></Cell>                                                   
              <Cell><Data ss:Type="String"><%=_T["TOTAL_ACTIVITIES"]%></Data></Cell>
       </Row>
    </HeaderTemplate>  
        <ItemTemplate>
        <Row>
            <Cell><Data ss:Type="Number"><%# RepeaterRowCounter%></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("TERRITORY_NAME").ToString() %></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("CLIENT_NAME")%></Data></Cell>                                
            <Cell><Data ss:Type="String"><%# Eval("UNIT_NAME").ToString() %></Data></Cell>                                        
            <Cell><Data ss:Type="String"><%# GlobalFunctions.CalculateTotalHours(Eval("TOTAL_HOURS").ToString()) %></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("COUNT_ACTIVITIES").ToString()%></Data></Cell>
        </Row>
    </ItemTemplate>    
</asp:Repeater>  
</Table>
