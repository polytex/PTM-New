<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="ReportActivities.aspx.cs" Inherits="ReportActivities"  EnableViewStateMac="false" EnableEventValidation="false" %>
<%@ Register Src="~/Controls/ReportHeader.ascx" TagName="ReportHeader" TagPrefix="Polytex" %>
<%@ Register Src="~/Controls/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="Polytex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%=GetReportsGlobalInit()%>
<span id="spanHiddenFieldsForExport">
<input id="ReportName" name="ReportName" type="hidden" value="<%=CurrentPage %>" />
<input id="ShowReport" name="ShowReport" type="hidden" value="<%=ShowReport %>" />

<input id="TextBoxFromDateTime" name="TextBoxFromDateTime" type="hidden" value="<%=FromDateTime.ToString() %>" />
<input id="TextBoxToDateTime" name="TextBoxToDateTime" type="hidden" value="<%=ToDateTime.ToString() %>" />
<input id="TextBoxTerritoryId" name="TextBoxTerritoryId" type="hidden" value="<%=TerritoryId %>" />
<input id="TextBoxModelId" name="TextBoxModelId" type="hidden" value="<%=ModelId %>" />
<input id="TextBoxClientId" name="TextBoxClientId" type="hidden" value="<%=clientId %>" />
<input id="TextBoxUnitId" name="TextBoxUnitId" type="hidden" value="<%=unitId %>" />
<input id="TextBoxActivityGroupId" name="TextBoxActivityGroupId" type="hidden" value="<%=activityGroupId %>" />
<input id="TextBoxActivityTypeId" name="TextBoxActivityTypeId" type="hidden" value="<%=activityTypeId %>" />
<input id="TextBoxSystemUserId" name="TextBoxSystemUserId" type="hidden" value="<%= systemUserId %>" />
<input id="CheckBoxIncludeImage" name="CheckBoxIncludeImage" type="hidden" value="<%=IncludeImage %>" />
<input id="CheckBoxIncludeDisabled" name="CheckBoxIncludeDisabled" type="hidden" value="<%=IncludeDisabled %>" />

</span>

<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" IsEmpty="False" SelectMethod="Activities" TypeName="PolytexData.ReportsTasks" OnInit="ObjectDataSource1_Init">
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
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<table id="TableReport" runat="server" cellpadding="0" cellspacing="0" border="0" class="TableReport" oninit="TableReport_Init">
<tr>
    <td><!-- Report Header table -->
        <table cellpadding="0" cellspacing="0" border="0" style="width:100%;line-height:0.5cm;">
        <tr>
            <td>                
                <Polytex:ReportHeader ID="ReportHeader" runat="server" />                            
                <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td id="TdReportHeaderPages" runat="server" colspan="5"><Polytex:Label ID="LabelPage" runat="server" Trans="Page" Font-Bold="true"></Polytex:Label>&nbsp;<asp:Label ID="LabelCurrentPage" runat="server" OnInit="LabelCurrentPage_Init"></asp:Label>&nbsp;<Polytex:Label ID="LabelOutOf" runat="server" Trans="PageOutOf" Font-Bold="true"></Polytex:Label>&nbsp;<asp:Label ID="LabelPageCount" runat="server" OnInit="LabelPageCount_Init"></asp:Label>&nbsp;;&nbsp;<asp:Label ID="LabelRecords" runat="server" OnInit="LabelRecords_Init"></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight:bold;"><Polytex:Label ID="LabelFromDate" runat="server" Trans="Report_txt_FromDate"></Polytex:Label>:&nbsp;</td>
                    <td style="direction:ltr;"><%=FromDateTime %></td>                
                    <td class="ReportHeaderSpacerTd">&nbsp;</td>                  
                </tr>
                <tr>
                    <td style="font-weight:bold;"><Polytex:Label ID="LabelToDate" runat="server" Trans="Report_txt_ToDate" Category="Undefined"></Polytex:Label>:&nbsp;</td>
                    <td style="direction:ltr;"><%=ToDateTime %></td>                
                    <td class="ReportHeaderSpacerTd">&nbsp;</td>

                </tr>
                <tr>
                    <td style="font-weight:bold;"><Polytex:Label ID="LabelTerritoryId" runat="server" Trans="Territory"></Polytex:Label>:&nbsp;</td>
                    <td style=""><asp:Label ID="LabelTerritoryName" runat="server" style="cursor:default;"></asp:Label></td>
                    <td colspan="3">&nbsp;</td>
                </tr>                                                            
                </table>
            </td>                   
        </tr>
        </table>
    </td>
</tr>
<tr>
    <td>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%" OnInit="GridViewOnInit" AutoGenerateColumns="False" OnRowCreated="GridView1_RowCreated" OnDataBinding="GridView1_DataBinding">
    <Columns>
        <asp:TemplateField>
             <HeaderTemplate>                    
             </HeaderTemplate>                    
             <ItemTemplate>
                 <asp:Label ID="LabelSn" runat="server"></asp:Label>                   
              </ItemTemplate>
              <ItemStyle Width="20px" />                               
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate><Polytex:LinkButton ID="LinkButtonDateStartNameHeader" runat="server" Category="ColumnHeader" Trans="Date_Start" CommandName="DATE_START" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingDATE_START" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingDATE_START" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" /></HeaderTemplate>               
            <ItemTemplate>
                <span dir="ltr" style="direction:ltr"><asp:Label ID="LabelDateStart" runat="server" Text='<%# UniStr.Util.MakeShortDateByUIDateTimeFormat(Eval("DATE_START")) %>'></asp:Label></span>
            </ItemTemplate>                    
        </asp:TemplateField>
        
        
        <asp:TemplateField>
            <HeaderTemplate><Polytex:LinkButton ID="LinkButtonTimeStartNameHeader" runat="server" Category="ColumnHeader" Trans="ActivityDetail_FromTime" CommandName="DATE_START" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingFromTimeDATE_START" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingFromTimeDATE_START" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" /></HeaderTemplate>               
            <ItemTemplate>
                <span dir="ltr" style="direction:ltr"><asp:Label ID="LabelDateEnd" runat="server" Text='<%# UniStr.Util.MakeShortTimeByUIDateTimeFormat(Eval("DATE_START")) %>'></asp:Label></span>
            </ItemTemplate>                   
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate><Polytex:LinkButton ID="LinkButtonTimeEndNameHeader" runat="server" Category="ColumnHeader" Trans="ActivityDetail_EndTime" CommandName="DATE_END" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingDATE_END" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingDATE_END" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" /></HeaderTemplate>               
            <ItemTemplate>
                <span dir="ltr" style="direction:ltr"><asp:Label ID="LabelDateEnd" runat="server" Text='<%# UniStr.Util.MakeShortTimeByUIDateTimeFormat(Eval("DATE_END")) %>'></asp:Label></span>
            </ItemTemplate>                   
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtoמDriveTimeHeader" runat="server" Category="ColumnHeader" Trans="Drive_Time" CommandName="DRIVE_TIME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingDRIVE_TIME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingDRIVE_TIME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>               
            <ItemTemplate>
                <asp:Label ID="LabelDriveTime" runat="server" Text='<%# Bind("DRIVE_TIME") %>' ></asp:Label>
            </ItemTemplate>               
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtoTotalNameHeader" runat="server" Category="ColumnHeader" Trans="Total_Hours" CommandName="TOTAL_HOURS" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingTOTAL_HOURS" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingTOTAL_HOURS" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>               
            <ItemTemplate>
                <asp:Label ID="LabelTotal" runat="server" Text='<%# GlobalFunctions.CalculateTotalHours(Eval("TOTAL_HOURS").ToString()) %>' ></asp:Label>
            </ItemTemplate>               
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtonTerritoryNameHeader" runat="server" Category="ColumnHeader" Trans="Territory" CommandName="TERRITORY_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingTERRITORY_NAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingTERRITORY_NAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>               
            <ItemTemplate>
                <asp:Label ID="LabelTerritory" runat="server" Text='<%# Bind("TERRITORY_NAME") %>'></asp:Label>
            </ItemTemplate>                   
        </asp:TemplateField>
        
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtonSystemUserNameHeader" runat="server" Category="ColumnHeader" Trans="User_Name_Open_Activity" CommandName="SYSTEM_USER_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingSYSTEM_USER_NAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingSYSTEM_USER_NAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelSystemUser" runat="server" Text='<%# ApplicationData.getUserNameIncludeAdmin(Eval("SYSTEM_USER_ID"),Eval("SYSTEM_USER_NAME")) %>'></asp:Label>
            </ItemTemplate>                            
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtonSystemUserUpdateNameHeader" runat="server" Category="ColumnHeader" Trans="User_Name_Update_Activity" CommandName="SYSTEM_USER_UPDATE_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingSYSTEM_USER_UPDATE_NAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingSYSTEM_USER_UPDATE_NAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelSystemUserUpdate" runat="server" Text='<%# ApplicationData.getUserNameIncludeAdmin(Eval("SYSTEM_USER_UPDATE_ID"),Eval("SYSTEM_USER_UPDATE_NAME")) %>'></asp:Label>
            </ItemTemplate>                            
        </asp:TemplateField>
        
        
         <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LabelClientHeader" runat="server" Category="ColumnHeader" Trans="Client" CommandName="CLIENT_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingNAME_ID" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingWORKER_ID" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelClient" runat="server" Text='<%# Bind("CLIENT_NAME") %>'></asp:Label>
            </ItemTemplate>                        
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtonUnitNameHeader" runat="server" Category="ColumnHeader" Trans="Station" CommandName="UNIT_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingUNIT_NAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingUNIT_NAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelModel" runat="server" Text='<%# Bind("UNIT_NAME") %>'></asp:Label>
            </ItemTemplate>                        
        </asp:TemplateField>
        
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtonActivityGroupNameHeader" runat="server" Category="ColumnHeader" Trans="Activity_Group" CommandName="ACTIVITY_GROUP_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingACTIVITY_GROUP_NAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingACTIVITY_GROUP_NAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelActivityGroup" runat="server" Text='<%# Bind("ACTIVITY_GROUP_NAME") %>'></asp:Label>
            </ItemTemplate>                           
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtonActivityTypeNameHeader" runat="server" Category="ColumnHeader" Trans="Activity_Type" CommandName="ACTIVITY_TYPE_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingACTIVITY_TYPE_NAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingACTIVITY_TYPE_NAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelActivityType" runat="server" Text='<%# Bind("ACTIVITY_TYPE_NAME") %>'></asp:Label>
            </ItemTemplate>                          
        </asp:TemplateField>     
        
         <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:Label ID="LabelDescriptionHeader" runat="server" Trans="Description"></Polytex:Label>
            </HeaderTemplate>            
            <ItemTemplate>
                <Polytex:GridDataLabel ID="GridDataLabelDescription" runat="server" DataText='<%# Bind("DESCRIPTION") %>' MaxLength="30" ToolTip='<%# Bind("DESCRIPTION") %>'></Polytex:GridDataLabel>
            </ItemTemplate>                          
        </asp:TemplateField>     
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:Label ID="LabelPartsHeader" runat="server" Trans="Parts"></Polytex:Label>
            </HeaderTemplate>            
            <ItemTemplate>
                <Polytex:GridDataLabel ID="GridDataLabelParts" runat="server" DataText='<%# Bind("PARTS") %>' MaxLength="30" ToolTip='<%# Bind("PARTS") %>'></Polytex:GridDataLabel>
            </ItemTemplate>                          
        </asp:TemplateField>     
        
         <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:Label ID="LabelSolutionHeader" runat="server" Trans="Solution"></Polytex:Label>
            </HeaderTemplate>            
            <ItemTemplate>                
                <Polytex:GridDataLabel ID="GridDataLabelSolution" runat="server" DataText='<%# Bind("SOLUTION") %>' MaxLength="20" ToolTip='<%# Bind("SOLUTION") %>'></Polytex:GridDataLabel>
            </ItemTemplate>                          
        </asp:TemplateField>     
        
       <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:Label ID="LabelRecommendationsHeader" runat="server" Trans="Recommendations"></Polytex:Label>
            </HeaderTemplate>            
            <ItemTemplate>
                <Polytex:GridDataLabel ID="GridDataLabelRecommendations" runat="server" DataText='<%# Bind("RECOMMENDATIONS") %>' MaxLength="30" ToolTip='<%# Bind("RECOMMENDATIONS") %>'></Polytex:GridDataLabel>
            </ItemTemplate>                          
        </asp:TemplateField>  
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:Label ID="LabelImageHeader" runat="server" Trans="Image"></Polytex:Label>
            </HeaderTemplate>            
            <ItemTemplate>
                <a href='<%#  ApplicationData.GetImageSource(Eval("ID"))%>' target="_blank" >
                     <asp:Image ID="ImgPreview"   ImageUrl='<%# ApplicationData.GetImageSource(Eval("ID")) %>' runat="server" Width="50px" Height="50px" />
                </a>
            </ItemTemplate>                          
        </asp:TemplateField> 
             
    </Columns>
            <PagerTemplate>
                <Polytex:GridViewPager ID="GridViewPager" runat="server"  />
            </PagerTemplate>      
       </asp:GridView>
       </td>
</tr>
</table>

</asp:Content>

