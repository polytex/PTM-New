<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="SystemUsers.aspx.cs" Inherits="SystemUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_System_Users" IsEmpty="False" Skip="0">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="systemUserId" Type="Int32" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<asp:ObjectDataSource ID="ObjectDataSourceRoles" runat="server" SelectMethod="GetSystemUsersRoles" TypeName="SystemUser">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includePolytexAdmin" Type="Boolean" />
        <asp:Parameter DefaultValue="Technician" Name="UserRole" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:ObjectDataSource ID="ObjectDataSourceTerritory" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Territories">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="false" Name="includeWorldwide" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>

<span id="spanHiddenFieldsForExport">
<input id="ReportName" name="ReportName" type="hidden" value="<%=CurrentPage %>" />
<input id="TextBoxTerritoryId" name="TextBoxTerritoryId" type="hidden" value="<%=CurrentUser.TerritoryId.ToString() %>" />
<input id="TextBoxSystemUserId" name="TextBoxSystemUserId" type="hidden" value="<%=CurrentUser.SystemUserId.ToString() %>" />
</span>

<asp:GridView ID="GridView1" Width="100%" runat="server" PageSize="1000000" DataSourceID="ObjectDataSource1" OnInit="GridViewOnInit" OnRowCommand="GridViewOnRowCommand"> 
    <Columns>
        <asp:BoundField DataField="ID" Visible="False" />
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:Label ID="LabelNameHeader" runat="server" Trans="Name"></Polytex:Label>
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelName" runat="server" Text='<%# Bind("NAME") %>' ></asp:Label>
            </ItemTemplate>            
            <EditItemTemplate>
                <Polytex:TextBox ID="TextBoxName" runat="server" CssClass="GridEditItemContent" InputType="String" IsRequired="True" Text='<%# Bind("NAME") %>' ValidationGroup="GridEditItem" TextBoxMaxLength="25" Argument="2"  ReadOnly='<%# !ChangeVisibilitybyRole() %>'/>
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:TextBox ID="TextBoxName" runat="server" InputType="String" ValidationGroup="GridAddItem" CssClass="GridFooterContent" IsRequired="True" TextBoxMaxLength="50" Argument="1"  Visible='<%# ChangeVisibilitybyRole() %>'/>
            </FooterTemplate>                       
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:Label ID="LabelTerritoryHeader" runat="server" Trans="Territory"></Polytex:Label>
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelTerritory" runat="server" Text='<%# Eval("TERRITORY_ID").ToString()=="-1" ? "Worldwide" : Eval("TERRITORY_NAME").ToString() %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <Polytex:DropDownList ID="DropDownListTerritory" runat="server" DataSourceID="ObjectDataSourceTerritory" DataTextField="NAME" DataValueField="ID" InsertEmptyItem="true" IsRequired="true" CssClass="GridEditItemContent" ValidationGroup="GridEditItem" SelectedValue='<%# Bind("TERRITORY_ID") %>' ></Polytex:DropDownList>                
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:DropDownList ID="DropDownListTerritory" runat="server" DataSourceID="ObjectDataSourceTerritory" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridFooterContent" ValidationGroup="GridAddItem" Visible='<%# ChangeVisibilitybyRole() %>'></Polytex:DropDownList>
            </FooterTemplate>                            
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:Label ID="LabelLoginNameHeader" runat="server" Trans="Login_UserName"></Polytex:Label>
            </HeaderTemplate>               
            <ItemTemplate>
                <asp:Label ID="LabelLoginName" runat="server" Text='<%# Bind("LOGIN") %>'></asp:Label>
            </ItemTemplate>            
            <EditItemTemplate>
                <Polytex:TextBox ID="TextBoxLoginName" runat="server" Text='<%# Bind("LOGIN") %>' InputType="String" ValidationGroup="GridEditItem" CssClass="GridEditItemContent" IsRequired="True" TextBoxMaxLength="50" ReadOnly='<%# !ChangeVisibilitybyRole() %>'/>
                <div align="<%#CultureAlign %>"><asp:CustomValidator ID="CustomValidatorEditLoginName" runat="server" ErrorMessage='<%#_T["LoginNameExists"] %>' ValidationGroup="GridEditItem" Display="Dynamic" OnServerValidate="CustomValidatorLoginNameUnique" SetFocusOnError="True" CssClass="Validator"></asp:CustomValidator></div>
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:TextBox ID="TextBoxLoginName" runat="server" InputType="String" ValidationGroup="GridAddItem" CssClass="GridFooterContent" IsRequired="True" TextBoxMaxLength="50" Visible='<%# ChangeVisibilitybyRole() %>'/>
                <div align="<%#CultureAlign %>"><asp:CustomValidator ID="CustomValidatorAddLoginName" runat="server" ErrorMessage='<%#_T["LoginNameExists"] %>' ValidationGroup="GridAddItem" Display="Dynamic" OnServerValidate="CustomValidatorLoginNameExists" SetFocusOnError="True" CssClass="Validator" ></asp:CustomValidator></div>
            </FooterTemplate>            
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:Label ID="LabelPasswordHeader" runat="server" Trans="Login_Password"></Polytex:Label>
            </HeaderTemplate>                  
            <ItemTemplate>
                <asp:Label ID="LabelPassword" runat="server" Text="****"></asp:Label>
            </ItemTemplate>            
            <EditItemTemplate>                
                <Polytex:TextBox ID="TextBoxPassword" runat="server" InputType="Password" ValidationGroup="GridEditItem" CssClass="GridEditItemContent" IsRequired="false" TextBoxMaxLength="50"/>
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:TextBox ID="TextBoxPassword" runat="server" InputType="String" ValidationGroup="GridAddItem" CssClass="GridFooterContent" IsRequired="true" TextBoxMaxLength="50" Visible='<%# ChangeVisibilitybyRole() %>'/>
            </FooterTemplate>            
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:Label ID="LabelPermissionHeader" runat="server" Trans="Permission"></Polytex:Label>
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelPermission" runat="server" Text='<%# GetRoleName(Eval("USER_TYPE_ID")) %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <Polytex:DropDownList ID="DropDownListRoleId" runat="server" DataSourceID="ObjectDataSourceRoles" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridEditItemContent" ValidationGroup="GridEditItem" SelectedValue='<%# Bind("USER_TYPE_ID") %>'></Polytex:DropDownList>                
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:DropDownList ID="DropDownListRoleId" runat="server" DataSourceID="ObjectDataSourceRoles" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridFooterContent" ValidationGroup="GridAddItem" Visible='<%# ChangeVisibilitybyRole() %>'></Polytex:DropDownList>
            </FooterTemplate>                            
        </asp:TemplateField>
        
        
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <Polytex:CustomImageButton ID="CustomImageButtonEdit" runat="server" ImageAlign="Top" CustomButtonType="Edit" MouseEffect="true" />               
                <Polytex:CustomImageButton ID="CustomImageButtonDisable" runat="server" ImageAlign="Middle" CustomButtonType="Disable" CommandName="Disable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() == "" && ChangeVisibilitybyRole()%>' />                
                <Polytex:CustomImageButton ID="CustomImageButtonEnable" runat="server" ImageAlign="Middle" CustomButtonType="Enable" CommandName="Enable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() != "" && ChangeVisibilitybyRole()%>' />               
            </ItemTemplate>            
            <EditItemTemplate>                                
                <Polytex:CustomImageButton ID="CustomImageButtonUpdate" runat="server" IsGridRowSubmitButton="true" CustomButtonType="Update" ValidationGroup="GridEditItem" MouseEffect="true" CommandName="UpdateSystemUser" CommandArgument='<%# Bind("ID") %>' />
                <Polytex:CustomImageButton ID="CustomImageButtonCancel" runat="server" CustomButtonType="Cancel" MouseEffect="true" />                                               
            </EditItemTemplate>                  
            <FooterTemplate>
               <Polytex:CustomImageButton ID="CustomImageButtonAdd" runat="server" IsGridRowSubmitButton="true" CustomButtonType="Add" ValidationGroup="GridAddItem" CommandName="Insert" MouseEffect="true" Visible='<%# ChangeVisibilitybyRole() %>'/>
            </FooterTemplate>
            <HeaderStyle Width="56px" />
            <ItemStyle CssClass="DivButtons" VerticalAlign="Top" />                             
        </asp:TemplateField>        
    </Columns>     
</asp:GridView>
<Polytex:CheckBox ID="CheckBoxIncludeDisabled" runat="server" Checked="false" Trans="IncludeDisabled" AutoPostBack="True" OnCheckedChanged="CheckBoxIncludeDisabled_CheckedChanged"/>
<div class="ActionMessage"><Polytex:Label ID="PolytexLabelActionMessage" runat="server" EnableViewState="false" Visible="false" OnLoad="ActionMessage_Load"></Polytex:Label></div>
</asp:Content>

