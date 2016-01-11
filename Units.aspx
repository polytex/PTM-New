<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Units.aspx.cs" Inherits="Units" %>
<%@ Register Src="~/Controls/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="Polytex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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

<asp:ObjectDataSource ID="ObjectDataSourceModel" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Models">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:ObjectDataSource ID="ObjectDataSourceClient" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Clients">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

<span id="spanHiddenFieldsForExport">
<input id="ReportName" name="ReportName" type="hidden" value="<%=CurrentPage %>" />
<input id="HiddenFieldTerritoryId" name="HiddenFieldTerritoryId" runat="server" type="hidden"/>
<input id="HiddenFieldClientId" name="HiddenFieldClientId" runat="server" type="hidden" />
<input id="HiddenFieldSerial" name="HiddenFieldSerial" runat="server" type="hidden" />
<input id="HiddenFieldModelId" name="HiddenFieldModel" runat="server" type="hidden" />
<input id="HiddenFieldDescription" name="HiddenFieldDescription" runat="server" type="hidden" />
<input id="HiddenFieldUnitIP" name="HiddenFieldUnitIP" runat="server" type="hidden" />
<input id="HiddenFieldCameraIP" name="HiddenFieldCameraIP" runat="server" type="hidden" />
<input id="HiddenFieldSWVersion" name="HiddenFieldSWVersion" runat="server" type="hidden" />
</span>

<asp:GridView ID="GridView1" Width="100%" runat="server" PageSize="30" DataSourceID="ObjectDataSource1" OnInit="GridViewOnInit" OnPreRender="GridViewOnPreRender" OnRowCommand="GridViewOnRowCommand"> 
    <Columns>
        <asp:BoundField DataField="ID" Visible="False" />
        <asp:TemplateField>
            <HeaderTemplate>
                 <Polytex:LinkButton ID="LabelClientHeader" runat="server" Category="ColumnHeader" Trans="Client" CommandName="CLIENT_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingNAME_ID" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingWORKER_ID" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelClient" runat="server" Text='<%# Bind("CLIENT_NAME") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <Polytex:DropDownList ID="DropDownListClient" runat="server" DataSourceID="ObjectDataSourceClient" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridEditItemContent" ValidationGroup="GridEditItem" SelectedValue='<%# Bind("CLIENT_ID") %>'></Polytex:DropDownList>                
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:DropDownList ID="DropDownListClient" runat="server" DataSourceID="ObjectDataSourceClient" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridFooterContent" ValidationGroup="GridAddItem"></Polytex:DropDownList>
            </FooterTemplate>                            
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LabelModelsHeader" runat="server" Category="ColumnHeader" Trans="Model" CommandName="MODEL_NAME" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingMODEL_NAME" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingMODEL_NAME" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelModels" runat="server" Text='<%# Bind("MODEL_NAME") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <Polytex:DropDownList ID="DropDownListModel" runat="server" DataSourceID="ObjectDataSourceModel" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridEditItemContent" ValidationGroup="GridEditItem" SelectedValue='<%# Bind("MODEL_ID") %>'></Polytex:DropDownList>                
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:DropDownList ID="DropDownListModel" runat="server" DataSourceID="ObjectDataSourceModel" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridFooterContent" ValidationGroup="GridAddItem"></Polytex:DropDownList>
            </FooterTemplate>                            
        </asp:TemplateField>
            
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LabelSerialHeader" runat="server" Category="ColumnHeader" Trans="Serial" CommandName="SERIAL" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingSERIAL" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingSERIAL" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelSerial" runat="server" Text='<%# Bind("SERIAL") %>' ></asp:Label>
            </ItemTemplate>            
            <EditItemTemplate>
                <Polytex:TextBox ID="TextBoxSerial" runat="server" CssClass="GridEditItemContent" InputType="String" IsRequired="True" Text='<%# Bind("SERIAL") %>' ValidationGroup="GridEditItem" TextBoxMaxLength="25" OnPreRender="SetOnLoadFocus" Argument="2" />
                <div align="<%#CultureAlign %>"><asp:CustomValidator ID="CustomValidatorEditLoginName" runat="server" ErrorMessage='<%#_T["SerialNameExists"] %>' ValidationGroup="GridEditItem" Display="Dynamic" OnServerValidate="CustomValidatorLoginSerialUnique" SetFocusOnError="True" CssClass="Validator"></asp:CustomValidator></div>
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:TextBox ID="TextBoxSerial" runat="server" Text='<%# Bind("SERIAL") %>' InputType="String" ValidationGroup="GridAddItem" CssClass="GridFooterContent" IsRequired="True" TextBoxMaxLength="25" OnPreRender="SetOnLoadFocus" Argument="1" />
                <div align="<%#CultureAlign %>"><asp:CustomValidator ID="CustomValidatorAddLoginName" runat="server" ErrorMessage='<%#_T["SerialNameExists"] %>' ValidationGroup="GridAddItem" Display="Dynamic" OnServerValidate="CustomValidatorLoginSerialExists" SetFocusOnError="True" CssClass="Validator" ></asp:CustomValidator></div>
            </FooterTemplate>                       
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LabelDescriptionNameHeader" runat="server" Category="ColumnHeader" Trans="Description" CommandName="DESCRIPTION" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingDESCRIPTION" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingDESCRIPTION" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>               
            <ItemTemplate>
                <asp:Label ID="LabelDescription" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
            </ItemTemplate>            
            <EditItemTemplate>
                <Polytex:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("DESCRIPTION") %>' InputType="String" ValidationGroup="GridEditItem" CssClass="GridEditItemContent" IsRequired="True" TextBoxMaxLength="50" />
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:TextBox ID="TextBoxDescription" runat="server" InputType="String" ValidationGroup="GridAddItem" CssClass="GridFooterContent" IsRequired="True" TextBoxMaxLength="50" />
            </FooterTemplate>            
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LabelUnitIPNameHeader" runat="server" Category="ColumnHeader" Trans="UnitIP" CommandName="IP_UNIT" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingIP_UNIT" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingIP_UNIT" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>               
            <ItemTemplate>
                <asp:Label ID="LabelUnitIP" runat="server" Text='<%# Bind("IP_UNIT") %>'></asp:Label>
            </ItemTemplate>            
            <EditItemTemplate>
                <Polytex:TextBox ID="TextBoxUnitIP" runat="server" Text='<%# Bind("IP_UNIT") %>' InputType="String" ValidationGroup="GridEditItem" IsRequired="false" CssClass="GridEditItemContent" TextBoxMaxLength="50" />
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:TextBox ID="TextBoxUnitIP" runat="server" InputType="String" ValidationGroup="GridAddItem" CssClass="GridFooterContent" IsRequired="false" TextBoxMaxLength="50" />
            </FooterTemplate>            
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LabelCameraIPNameHeader" runat="server" Category="ColumnHeader" Trans="CameraIP" CommandName="IP_CAMERA" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingIP_CAMERA" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingIP_CAMERA" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>               
            <ItemTemplate>
                <asp:Label ID="LabelCameraIP" runat="server" Text='<%# Bind("IP_CAMERA") %>'></asp:Label>
            </ItemTemplate>            
            <EditItemTemplate>
                <Polytex:TextBox ID="TextBoxCameraIP" runat="server" Text='<%# Bind("IP_CAMERA") %>' InputType="String" ValidationGroup="GridEditItem" IsRequired="false" CssClass="GridEditItemContent" TextBoxMaxLength="50" />
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:TextBox ID="TextBoxCameraIP" runat="server" InputType="String" ValidationGroup="GridAddItem" CssClass="GridFooterContent" IsRequired="false" TextBoxMaxLength="50" />
            </FooterTemplate>            
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:LinkButton ID="LabelSwVersionNameHeader" runat="server" Category="ColumnHeader" Trans="SWVersion" CommandName="SW_VERSION" OnCommand="SortGridView"></Polytex:LinkButton><asp:Image ID="ImageAscendingSW_VERSION" runat="server" ImageUrl="~/Images/SortAscending.gif" OnPreRender="ImageSortAscending_PreRender" /><asp:Image ID="ImageDescendingSW_VERSION" runat="server" ImageUrl="~/Images/SortDescending.gif" OnPreRender="ImageSortDescending_PreRender" />
            </HeaderTemplate>               
            <ItemTemplate>
                <asp:Label ID="LabelSWVersion" runat="server" Text='<%# Bind("SW_VERSION") %>'></asp:Label>
            </ItemTemplate>            
            <EditItemTemplate>
                <Polytex:TextBox ID="TextBoxSWVersion" runat="server" Text='<%# Bind("SW_VERSION") %>' InputType="String" ValidationGroup="GridEditItem" IsRequired="false" CssClass="GridEditItemContent" TextBoxMaxLength="50" />
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:TextBox ID="TextBoxSWVersion" runat="server" InputType="String" ValidationGroup="GridAddItem" CssClass="GridFooterContent" IsRequired="false" TextBoxMaxLength="50" />
            </FooterTemplate>            
        </asp:TemplateField>
        

        
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <Polytex:CustomImageButton ID="CustomImageButtonEdit" runat="server" ImageAlign="Top" CustomButtonType="Edit" MouseEffect="true" />               
                <Polytex:CustomImageButton ID="CustomImageButtonDisable" runat="server" ImageAlign="Middle" CustomButtonType="Disable" CommandName="Disable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() == "" %>' />                
                <Polytex:CustomImageButton ID="CustomImageButtonEnable" runat="server" ImageAlign="Middle" CustomButtonType="Enable" CommandName="Enable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() != "" %>' />               
            </ItemTemplate>            
            <EditItemTemplate>                                
                <Polytex:CustomImageButton ID="CustomImageButtonUpdate" runat="server" IsGridRowSubmitButton="true" CustomButtonType="Update" ValidationGroup="GridEditItem" MouseEffect="true" CommandName="UpdateUnits" CommandArgument='<%# Bind("ID") %>' />
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

