<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Units.ascx.cs" Inherits="ExportExcel_Units" %>

<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Units">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="clientId" Type="Int32" />
        <asp:Parameter DefaultValue="" Name="serial" Type="String" />
        <asp:Parameter DefaultValue="0" Name="modelId" Type="Int32" />
        <asp:Parameter DefaultValue="" Name="description" Type="String" />
        <asp:Parameter DefaultValue="" Name="unitIp" Type="String" />
        <asp:Parameter DefaultValue="" Name="cameraIp" Type="String" />
        <asp:Parameter DefaultValue="" Name="swVersion" Type="String" />   
    </SelectParameters>
</Polytex:ObjectDataSource>

<Table ss:ExpandedColumnCount="100" ss:ExpandedRowCount="<%=(RepeaterExportReport.Items.Count + 1).ToString() %>" x:FullColumns="1" x:FullRows="1">
<asp:Repeater ID="RepeaterExportReport" runat="server" DataSourceID="ObjectDataSource1">
    <HeaderTemplate>
       <Row ss:StyleID="s20">
              <Cell><Data ss:Type="String">#</Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["Client"]%></Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["Model"]%></Data></Cell>            
              <Cell><Data ss:Type="String"><%=_T["Serial"]%></Data></Cell>            
              <Cell><Data ss:Type="String"><%=_T["Description"]%></Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["UnitIP"]%></Data></Cell>            
              <Cell><Data ss:Type="String"><%=_T["CameraIP"]%></Data></Cell>
              <Cell><Data ss:Type="String"><%=_T["SWVersion"]%></Data></Cell>
       </Row>
    </HeaderTemplate>  
        <ItemTemplate>
        <Row>
            <Cell><Data ss:Type="Number"><%# RepeaterRowCounter%></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("CLIENT_NAME").ToString()%></Data></Cell>    
            <Cell><Data ss:Type="String"><%# Eval("MODEL_NAME").ToString()%></Data></Cell>                          
            <Cell><Data ss:Type="String"><%# Eval("SERIAL").ToString()%></Data></Cell>                       
            <Cell><Data ss:Type="String"><%# Eval("DESCRIPTION").ToString()%></Data></Cell>
            <Cell><Data ss:Type="String"><%# Eval("IP_UNIT").ToString()%></Data></Cell>                          
            <Cell><Data ss:Type="String"><%# Eval("IP_CAMERA").ToString()%></Data></Cell>                       
            <Cell><Data ss:Type="String"><%# Eval("SW_VERSION").ToString()%></Data></Cell>
        </Row>
    </ItemTemplate>    
</asp:Repeater>  
</Table>
