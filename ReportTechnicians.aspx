<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="ReportTechnicians.aspx.cs" Inherits="ReportTechnicians" Title="Untitled Page" %>
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

</span>

<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" IsEmpty="False" SelectMethod="Technicians" TypeName="PolytexData.ReportsTasks" OnInit="ObjectDataSource1_Init">
    <SelectParameters>        
        <asp:Parameter DefaultValue="true" Name="hasMaxResults" Type="Boolean" />
        <asp:Parameter Name="skip" Type="Int32" DefaultValue="0" />
        <asp:Parameter Name="fromDate" Type="DateTime" />
        <asp:Parameter Name="toDate" Type="DateTime" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
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
    <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%" OnInit="GridViewOnInit" AutoGenerateColumns="False" OnRowCreated="GridView1_RowCreated" OnDataBinding="GridView1_DataBinding" OnRowCommand="GridViewOnRowCommand">
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
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtonSystemUserNameHeader" runat="server" Category="ColumnHeader" Trans="User_Name_Open_Activity" CommandName="SYSTEM_USER_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingSYSTEM_USER_NAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingSYSTEM_USER_NAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelSystemUser" runat="server" Text='<%# ApplicationData.getUserNameIncludeAdmin(Eval("SYSTEM_USER_ID"),Eval("SYSTEM_USER_NAME")) %>'></asp:Label>
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
                <Polytex:LinkButton ID="LinkButtoTotalNameHeader" runat="server" Category="ColumnHeader" Trans="Total_Hours" CommandName="TOTAL_HOURS" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingTOTAL_HOURS" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingTOTAL_HOURS" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>               
            <ItemTemplate>
                <asp:Label ID="LabelTotal" runat="server" Text='<%# Bind("TOTAL_HOURS") %>'></asp:Label>
            </ItemTemplate>  
            <FooterTemplate>
                <asp:Label ID="LabelTotalHours" runat="server" CssClass="GridFooterContent" Text='<%# GetTotalHours() %>'></asp:Label>
          </FooterTemplate>                  
        </asp:TemplateField>
        
                
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtoCountActivitiesNameHeader" runat="server" Category="ColumnHeader" Trans="TOTAL_ACTIVITIES" CommandName="COUNT_ACTIVITIES" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingCOUNT_ACTIVITIES" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingCOUNT_ACTIVITIES" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>               
            <ItemTemplate>
                <asp:Label ID="LabelCountActivities" runat="server" Text='<%# Bind("COUNT_ACTIVITIES") %>'></asp:Label>
            </ItemTemplate> 
            <FooterTemplate>
                <asp:Label ID="LabelTotalActivities" runat="server" CssClass="GridFooterContent" Text='<%# GetTotalActivities() %>'></asp:Label>
          </FooterTemplate>                   
        </asp:TemplateField>
        
        <asp:TemplateField ShowHeader="False">               
           <ItemTemplate>
               <Polytex:CustomImageButton ID="CustomImageButtonFind"  CustomButtonType="Search"  runat="server" ImageAlign="Right" CommandName="SystemUserId" CommandArgument='<%# Bind("SYSTEM_USER_ID") %>' MouseEffect="True" />
           </ItemTemplate>                      
           <HeaderStyle Width="56px" />
           <ItemStyle CssClass="DivButtons" VerticalAlign="Top" />                                                    
        </asp:TemplateField>  
             
    </Columns>  
            <PagerTemplate>
                <Polytex:GridViewPager ID="GridViewPager" runat="server"  />
            </PagerTemplate>
             <FooterStyle Font-Bold="True" />    
       </asp:GridView>

       </td>
</tr>
</table>

</asp:Content>

