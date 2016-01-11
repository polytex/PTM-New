<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportActivities.ascx.cs" Inherits="ExportExcel_ReportActivities" %>

<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" IsEmpty="False" SelectMethod="Activities" TypeName="PolytexData.ReportsTasks">
    <SelectParameters>        
        <asp:Parameter DefaultValue="true" Name="hasMaxResults" Type="Boolean" />
        <asp:Parameter Name="skip" Type="Int32" DefaultValue="0" />
        <asp:Parameter Name="fromDate" Type="DateTime" />
        <asp:Parameter Name="toDate" Type="DateTime" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="clientId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="modelId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="unitId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="activityGroupId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="activityTypeId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="systemUserId" Type="Int32" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<Table ss:ExpandedColumnCount="100" ss:ExpandedRowCount="<%=(RepeaterExportReport.Items.Count + 1).ToString() %>" x:FullColumns="1" x:FullRows="1">
<asp:Repeater ID="RepeaterExportReport" runat="server" DataSourceID="ObjectDataSource1">
    <HeaderTemplate>
       <Row ss:StyleID="s20">
              <Cell><Data ss:Type="String">#</Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["Date_Start"]%></Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["Date_End"]%></Data></Cell>  
              <Cell><Data ss:Type="String"><%=_T["Total_Hours"]%></Data></Cell>              
              <Cell><Data ss:Type="String"><%=_T["Territory"]%></Data></Cell>            
              <Cell><Data ss:Type="String"><%=_T["User_Name_Open_Activity"]%></Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["User_Name_Update_Activity"]%></Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["Client"]%></Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["Station_Name"]%></Data></Cell>            
              <Cell><Data ss:Type="String"><%=_T["Activity_Group"]%></Data></Cell>                                                   
              <Cell><Data ss:Type="String"><%=_T["Activity_Type"]%></Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["Description"]%></Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["Parts"]%></Data></Cell>            
              <Cell><Data ss:Type="String"><%=_T["Solution"]%></Data></Cell>                                                   
              <Cell><Data ss:Type="String"><%=_T["Recommendations"]%></Data></Cell>
       </Row>
    </HeaderTemplate>  
        <ItemTemplate>
        <Row>
            <Cell><Data ss:Type="Number"><%# RepeaterRowCounter%></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("DATE_START").ToString()%></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("DATE_END").ToString()%></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("TOTAL_HOURS").ToString()%></Data></Cell>    
            <Cell><Data ss:Type="String"><%# Eval("TERRITORY_NAME").ToString() %></Data></Cell>
            <Cell><Data ss:Type="String"><%# ApplicationData.getUserNameIncludeAdmin(Eval("SYSTEM_USER_ID"),Eval("SYSTEM_USER_NAME")) %></Data></Cell>
            <Cell><Data ss:Type="String"><%# ApplicationData.getUserNameIncludeAdmin(Eval("SYSTEM_USER_UPDATE_ID"),Eval("SYSTEM_USER_UPDATE_NAME")) %></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("CLIENT_NAME")%></Data></Cell>                                
            <Cell><Data ss:Type="String"><%# Eval("UNIT_NAME").ToString() %></Data></Cell>                       
            <Cell><Data ss:Type="String"><%# Eval("ACTIVITY_GROUP_NAME").ToString() %></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("ACTIVITY_TYPE_NAME").ToString() %></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("DESCRIPTION").ToString() %></Data></Cell>                                   
            <Cell><Data ss:Type="String"><%# Eval("PARTS").ToString()  %></Data></Cell>                       
            <Cell><Data ss:Type="String"><%# Eval("SOLUTION").ToString() %></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("RECOMMENDATIONS").ToString() %></Data></Cell>
        </Row>
    </ItemTemplate>    
</asp:Repeater>  
</Table>
