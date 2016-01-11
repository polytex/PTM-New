<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Clients.aspx.cs" Inherits="Clients" %>
<%@ Register Src="~/Controls/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="Polytex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
function EditSiteMenu(siteId)
{
    OpenPopup("Clients.aspx?clientId=" + siteId, 440, 350, false);
    return false;
}

</script>
<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="PolytexData.Manage_Clients">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="" Name="clientName" Type="String" />
        <asp:Parameter DefaultValue="" Name="version" Type="String" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<asp:ObjectDataSource ID="ObjectDataSourceTerritory" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Territories">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="false" Name="includeWorldwide" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>

<span id="spanHiddenFieldsForExport">
<input id="ReportName" name="ReportName" type="hidden" value="<%=CurrentPage %>" />
<input id="HiddenFieldTerritoryId" name="HiddenFieldTerritoryId" runat="server" type="hidden"/>
<input id="HiddenFieldClient" name="HiddenFieldClientId" runat="server" type="hidden" />
<input id="HiddenFieldVersion" name="HiddenFieldSerial" runat="server" type="hidden" />
</span>
    
<asp:GridView ID="GridView1" Width="100%" runat="server" PageSize="15" DataSourceID="ObjectDataSource1" OnInit="GridViewOnInit"  OnPreRender="GridViewOnPreRender"  OnRowCommand="GridViewOnRowCommand"> 
    <Columns>
        <asp:BoundField DataField="ID" Visible="False" />
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkNameHeader" runat="server" Category="ColumnHeader" Trans="Name" CommandName="NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingNAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingNAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                 <asp:Label ID="LabelName" runat="server" Text='<%# Bind("NAME") %>' ></asp:Label>
            </ItemTemplate>            
            <EditItemTemplate>
                <Polytex:TextBox ID="TextBoxName" runat="server" CssClass="GridEditItemContent" InputType="String" IsRequired="True" Text='<%# Bind("NAME") %>' ValidationGroup="GridEditItem" TextBoxMaxLength="50" OnPreRender="SetOnLoadFocus" Argument="2" />
                <div align="<%#CultureAlign %>"><asp:CustomValidator ID="CustomValidatorEditClientName" runat="server" ErrorMessage='<%#_T["ClientNameExists"] %>' ValidationGroup="GridEditItem" Display="Dynamic" OnServerValidate="CustomValidatorClientNameUnique" SetFocusOnError="True" CssClass="Validator"></asp:CustomValidator></div>
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:TextBox ID="TextBoxName" runat="server" Text='<%# Bind("NAME") %>' InputType="String" ValidationGroup="GridAddItem" CssClass="GridFooterContent" IsRequired="True" TextBoxMaxLength="50" OnPreRender="SetOnLoadFocus" Argument="1" />
                <div align="<%#CultureAlign %>"><asp:CustomValidator ID="CustomValidatorAddClientName" runat="server" ErrorMessage='<%#_T["ClientNameExists"] %>' ValidationGroup="GridAddItem" Display="Dynamic" OnServerValidate="CustomValidatorClientNameExists" SetFocusOnError="True" CssClass="Validator"></asp:CustomValidator></div>
            </FooterTemplate>                       
        </asp:TemplateField>

        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkTerritoryHeader" runat="server" Category="ColumnHeader" Trans="Territory" CommandName="TERRITORY_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingTERRITORY_NAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingTERRITORY_NAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />           
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelTerritory" runat="server" Text='<%# Bind("TERRITORY_NAME") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <Polytex:DropDownList ID="DropDownListTerritory" runat="server" DataSourceID="ObjectDataSourceTerritory" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridEditItemContent" ValidationGroup="GridEditItem" SelectedValue='<%# Bind("TERRITORY_ID") %>'></Polytex:DropDownList>                
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:DropDownList ID="DropDownListTerritory" runat="server" DataSourceID="ObjectDataSourceTerritory" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridFooterContent" ValidationGroup="GridAddItem"></Polytex:DropDownList>
            </FooterTemplate>                            
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LinkManagerVersionNameHeader" runat="server" Category="ColumnHeader" Trans="Version" CommandName="VERSION" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingVERSION" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingVERSION" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />           
            </HeaderTemplate>               
            <ItemTemplate>
                <asp:Label ID="LabelManagerVersion" runat="server" Text='<%# Bind("VERSION") %>'></asp:Label>
            </ItemTemplate>            
            <EditItemTemplate>
                <Polytex:TextBox ID="TextBoxManagerVersion" runat="server" Text='<%# Bind("VERSION") %>' InputType="String" ValidationGroup="GridEditItem" IsRequired="false" CssClass="GridEditItemContent" TextBoxMaxLength="50" />
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:TextBox ID="TextBoxManagerVersion" runat="server" InputType="String" ValidationGroup="GridAddItem" CssClass="GridFooterContent" IsRequired="false" TextBoxMaxLength="50" />
            </FooterTemplate>            
        </asp:TemplateField>
       
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <Polytex:CustomImageButton ID="CustomImageButtonEdit" runat="server" ImageAlign="Top" CustomButtonType="Edit" MouseEffect="true" />    
                <Polytex:CustomImageButton ID="CustomImageButtonProperties" runat="server" ImageAlign="Top" CustomButtonType="Properties" MouseEffect="true" CommandName="Properties" CommandArgument='<%# Bind("ID") %>' OnPreRender="CustomImageButtonProperties_OnPreRender" />                       
                <Polytex:CustomImageButton ID="CustomImageButtonDisable" runat="server" ImageAlign="Middle" CustomButtonType="Disable" CommandName="Disable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() == "" %>' />                
                <Polytex:CustomImageButton ID="CustomImageButtonEnable" runat="server" ImageAlign="Middle" CustomButtonType="Enable" CommandName="Enable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() != "" %>' />               
            </ItemTemplate>            
            <EditItemTemplate>                                
                <Polytex:CustomImageButton ID="CustomImageButtonUpdate" runat="server" IsGridRowSubmitButton="true" CustomButtonType="Update" ValidationGroup="GridEditItem" MouseEffect="true" CommandName="UpdateClient" CommandArgument='<%# Bind("ID") %>' />
                <Polytex:CustomImageButton ID="CustomImageButtonCancel" runat="server" CustomButtonType="Cancel" MouseEffect="true" />                                               
            </EditItemTemplate>                  
            <FooterTemplate>
               <Polytex:CustomImageButton ID="CustomImageButtonAdd" runat="server" IsGridRowSubmitButton="true" CustomButtonType="Add" ValidationGroup="GridAddItem" CommandName="Insert" MouseEffect="true" />
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

