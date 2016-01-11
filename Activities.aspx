<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Activities.aspx.cs" Inherits="Activities" %>
<%@ Register Src="~/Controls/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="Polytex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
function AddActivity()
{
    document.location = "ActivityDetails.aspx?activityId=0";
}
</script>
<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="PolytexData.Manage_Activities">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="clientId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="systemUserId" Type="Int32" />
        <asp:Parameter DefaultValue="" Name="unit" Type="String" />
        <asp:Parameter DefaultValue="" Name="dateStart" Type="String" />
        <asp:Parameter DefaultValue="0" Name="activityGroupId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="activitytypeId" Type="Int32" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<asp:ObjectDataSource ID="ObjectDataSourceClient" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Clients">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

<Polytex:ObjectDataSource ID="ObjectDataSourceSystemUser" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_System_Users" IsEmpty="False" Skip="0">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="systemUserId" Type="Int32" />
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
        </SelectParameters>
</Polytex:ObjectDataSource>

<input id="HiddenFieldClientId" name="HiddenFieldClientId" runat="server" type="hidden" />
<input id="HiddenFieldSystemUserId" name="HiddenFieldSystemUserId" runat="server" type="hidden" />
<input id="HiddenFieldDateStart" name="HiddenFieldDateStart" runat="server" type="hidden" />
<input id="HiddenFieldUnit" name="HiddenFieldUnit" runat="server" type="hidden" />
<input id="HiddenFieldActivityGroup" name="HiddenFieldActivityGroup" runat="server" type="hidden" />
<input id="HiddenFieldActivityType" name="HiddenFieldActivityType" runat="server" type="hidden" />
        
    <asp:GridView ID="GridView1" Width="100%" runat="server" PageSize="15" DataSourceID="ObjectDataSource1" OnInit="GridViewOnInit" OnRowCommand="GridViewOnRowCommand" OnPreRender="GridViewOnPreRender"> 
    <Columns>
        <asp:BoundField DataField="ID" Visible="False" />
        
        <asp:TemplateField>
            <HeaderTemplate>
                 <Polytex:LinkButton ID="LinkButtonDateStartNameHeader" runat="server" Category="ColumnHeader" Trans="Date_Start" CommandName="DATE_START" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingDATE_START" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingDATE_START" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>               
            <ItemTemplate>
                <span dir="ltr" style="direction:ltr"><asp:Label ID="LabelDateStart" runat="server" Text='<%# Bind("DATE_START") %>'></asp:Label></span>
            </ItemTemplate> 
            <FooterTemplate>                          
                <Polytex:TextBox ID="TextBoxDateStart" runat="server" CssClass="GridFooterContent" InputType="Date" TextBoxMaxLength="50" IsRequired="false" TextBoxColumns="12" OnPreRender="SetOnLoadFocus" Argument="3"/>                
           </FooterTemplate>                   
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtonSystemUserNameHeader" runat="server" Category="ColumnHeader" Trans="User_Name_Open_Activity" CommandName="SYSTEM_USER_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingSYSTEM_USER_NAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingSYSTEM_USER_NAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelSystemUser" runat="server" Text='<%# ApplicationData.getUserNameIncludeAdmin(Eval("SYSTEM_USER_ID"),Eval("SYSTEM_USER_NAME")) %>'></asp:Label>
            </ItemTemplate> 
            <FooterTemplate>
                <Polytex:DropDownList ID="DropDownListSystemUser" runat="server" DataSourceID="ObjectDataSourceSystemUser" DataTextField="NAME" DataValueField="ID" InsertEmptyItem="true" IsRequired="false" CssClass="GridFooterContent"></Polytex:DropDownList>
            </FooterTemplate>                             
        </asp:TemplateField>

        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LabelClientHeader" runat="server" Category="ColumnHeader" Trans="Client" CommandName="CLIENT_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingNAME_ID" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingWORKER_ID" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelClient" runat="server" Text='<%# Bind("CLIENT_NAME") %>'></asp:Label>
            </ItemTemplate> 
            <FooterTemplate>
                <Polytex:DropDownList ID="DropDownListClient" runat="server" DataSourceID="ObjectDataSourceClient" DataTextField="NAME" DataValueField="ID" InsertEmptyItem="true" IsRequired="false" CssClass="GridFooterContent"></Polytex:DropDownList>
            </FooterTemplate>                       
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtonUnitNameHeader" runat="server" Category="ColumnHeader" Trans="Station" CommandName="UNIT_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingUNIT_NAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingUNIT_NAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelUnits" runat="server" Text=' <%# Bind("UNIT_NAME") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>                          
                <Polytex:TextBox ID="TextBoxUnit" runat="server" CssClass="GridFooterContent" InputType="String" TextBoxMaxLength="50" TextBoxColumns="12" IsRequired="false" OnPreRender="SetOnLoadFocus" Argument="3"/>                
           </FooterTemplate>                              
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtonActivityGroupNameHeader" runat="server" Category="ColumnHeader" Trans="Activity_Group" CommandName="ACTIVITY_GROUP_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingACTIVITY_GROUP_NAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingACTIVITY_GROUP_NAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelActivityGroup" runat="server" Text='<%# Bind("ACTIVITY_GROUP_NAME") %>'></asp:Label>
            </ItemTemplate> 
            <FooterTemplate>
                <Polytex:DropDownList ID="DropDownListGroupName" runat="server" DataSourceID="ObjectDataSourceActivityGroups" DataTextField="NAME" DataValueField="ID" InsertEmptyItem="true" IsRequired="false" CssClass="GridFooterContent"></Polytex:DropDownList>
            </FooterTemplate>                           
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkButtonActivityTypeNameHeader" runat="server" Category="ColumnHeader" Trans="Activity_Type" CommandName="ACTIVITY_TYPE_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingACTIVITY_TYPE_NAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingACTIVITY_TYPE_NAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelActivityType" runat="server" Text='<%# Bind("ACTIVITY_TYPE_NAME") %>'></asp:Label>
            </ItemTemplate> 
            <FooterTemplate>
                <Polytex:DropDownList ID="DropDownListActivityTypeName" runat="server" DataSourceID="ObjectDataSourceActivityTypes" DataTextField="NAME" DataValueField="ID" InsertEmptyItem="true" IsRequired="false" CssClass="GridFooterContent"></Polytex:DropDownList>
            </FooterTemplate>                         
        </asp:TemplateField>
        
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <Polytex:CustomImageButton ID="CustomImageButtonEdit" runat="server" ImageAlign="Top" CustomButtonType="Edit" MouseEffect="true" CommandName="Edit" CommandArgument='<%# Bind("ID") %>' />               
                <Polytex:CustomImageButton ID="CustomImageButtonDisable" runat="server" ImageAlign="Middle" CustomButtonType="Disable" CommandName="Disable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() == "" %>' />                
                <Polytex:CustomImageButton ID="CustomImageButtonEnable" runat="server" ImageAlign="Middle" CustomButtonType="Enable" CommandName="Enable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() != "" %>' />               
            </ItemTemplate> 
            <FooterTemplate>
               <Polytex:CustomImageButton ID="CustomImageButtonClear" runat="server" CustomButtonType="Clear" CommandName="Clear" MouseEffect="true" CommandArgument="0" />
               <Polytex:CustomImageButton ID="CustomImageButtonFilter" runat="server" IsGridRowSubmitButton="true" CustomButtonType="Filter" CommandName="Filter" MouseEffect="true" CommandArgument="0" style="margin-top:1px;margin-bottom:1px;" />
            </FooterTemplate>                            
            <HeaderStyle Width="56px" />
            <ItemStyle CssClass="DivButtons" VerticalAlign="Top" />                             
        </asp:TemplateField> 
               
    </Columns>
    <PagerTemplate>
      <Polytex:GridViewPager ID="GridViewPager" runat="server"  />
    </PagerTemplate>     
</asp:GridView>
<Polytex:CheckBox ID="CheckBoxIncludeDisabled" runat="server" Checked="false" Trans="IncludeDisabled" AutoPostBack="True" OnCheckedChanged="CheckBoxIncludeDisabled_CheckedChanged"  />
<div class="ActionMessage"><Polytex:Label ID="PolytexLabelActionMessage" runat="server" EnableViewState="false" Visible="false" OnLoad="ActionMessage_Load"></Polytex:Label></div>
</asp:Content>

