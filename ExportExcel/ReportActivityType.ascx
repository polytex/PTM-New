<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportActivityType.ascx.cs" Inherits="ExportExcel_ReportActivityType" %>

<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" IsEmpty="False" SelectMethod="ActivityType" TypeName="PolytexData.ReportsTasks">
    <SelectParameters>        
        <asp:Parameter DefaultValue="true" Name="hasMaxResults" Type="Boolean" />
        <asp:Parameter Name="skip" Type="Int32" DefaultValue="0" />
        <asp:Parameter Name="fromDate" Type="DateTime" />
        <asp:Parameter Name="toDate" Type="DateTime" />
        <asp:Parameter DefaultValue="0" Name="activityGroupId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<Table ss:ExpandedColumnCount="100" ss:ExpandedRowCount="<%=(RepeaterExportReport.Items.Count + 1).ToString() %>" x:FullColumns="1" x:FullRows="1">
<asp:Repeater ID="RepeaterExportReport" runat="server" DataSourceID="ObjectDataSource1">
    <HeaderTemplate>
       <Row ss:StyleID="s20">
              <Cell><Data ss:Type="String">#</Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["ActivityGroupName"]%></Data></Cell>   
              <Cell><Data ss:Type="String"><%=_T["Activity_Type"]%></Data></Cell>         
              <Cell><Data ss:Type="String"><%=_T["Total_Hours"]%></Data></Cell>            
              <Cell><Data ss:Type="String"><%=_T["TOTAL_ACTIVITIES"]%></Data></Cell>
       </Row>
    </HeaderTemplate>  
        <ItemTemplate>
        <Row>
            <Cell><Data ss:Type="Number"><%# RepeaterRowCounter%></Data></Cell> 
            <Cell><Data ss:Type="String"><%# Eval("ACTIVITY_GROUP_NAME").ToString()%></Data></Cell>  
            <Cell><Data ss:Type="String"><%# Eval("ACTIVITY_TYPE_NAME").ToString()%></Data></Cell>                         
            <Cell><Data ss:Type="String"><%# Eval("TOTAL_HOURS").ToString()%></Data></Cell>                       
            <Cell><Data ss:Type="String"><%# Eval("COUNT_ACTIVITIES").ToString()%></Data></Cell>
        </Row>
    </ItemTemplate>    
</asp:Repeater>  
</Table>
