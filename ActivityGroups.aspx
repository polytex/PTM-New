<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="ActivityGroups.aspx.cs" Inherits="ActivityGroups" %>
<%@ Register Src="~/Controls/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="Polytex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
    TypeName="PolytexData.Manage_Activity_Groups">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
    </SelectParameters>
</Polytex:ObjectDataSource>

    <Polytex:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Activity_Types">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
            <asp:Parameter DefaultValue="0" Name="ActivityGroupId" Type="Int32" />
        </SelectParameters>
    </Polytex:ObjectDataSource>

<span id="spanHiddenFieldsForExport">
<input id="ReportName" name="ReportName" type="hidden" value="<%=CurrentPage %>" />
</span>

<table cellpadding="0" cellspacing="0" border="0" style="width:100%">
<tr>
    <td style="vertical-align:top;width:50%;">
        <asp:GridView ID="GridView1" runat="server" Width="100%" DataSourceID="ObjectDataSource1" DataKeyNames="ID" PageSize="1000000" OnInit="GridViewOnInit" OnRowCommand="GridViewOnRowCommand">
            <Columns>
                <asp:BoundField DataField="ID" Visible="False" />
                <asp:TemplateField>
                    <HeaderTemplate>
                        <Polytex:Label ID="LabelHeaderGroupName" runat="server" Trans='<%#_T["ActivityGroupName"] %>'></Polytex:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelGroupName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                    </ItemTemplate>                        
                    <EditItemTemplate>
                        <Polytex:TextBox ID="TextBoxGroupName" runat="server" CssClass="GridEditItemContent" InputType="String" IsRequired="True" Text='<%# Bind("NAME") %>' ValidationGroup="GridEditItem" TextBoxMaxLength="50" TextBoxColumns="12" OnPreRender="SetOnLoadFocus" Argument="2"  />
                        <div align="<%#CultureAlign %>"><asp:CustomValidator ID="CustomValidatorEditItemsGroupName" runat="server" ErrorMessage='<%#_T["ItemsGroupNameExists"] %>' ValidationGroup="GridEditItem" Display="Dynamic" OnServerValidate="CustomValidatorActivityGroupsNameUnique" SetFocusOnError="True" CssClass="Validator"></asp:CustomValidator></div>
                    </EditItemTemplate>
                    <FooterTemplate>                          
                        <Polytex:TextBox ID="TextBoxGroupName" runat="server" CssClass="GridFooterContent" InputType="String" ValidationGroup="GridAddItem" TextBoxMaxLength="50" TextBoxColumns="12" OnPreRender="SetOnLoadFocus" Argument="1" />                
                        <div align="<%#CultureAlign %>"><asp:CustomValidator ID="CustomValidatorAddItemsGroupName" runat="server" ErrorMessage='<%#_T["ItemsGroupNameExists"] %>' ValidationGroup="GridAddItem" Display="Dynamic" OnServerValidate="CustomValidatorActivityGroupsNameExists" SetFocusOnError="True" CssClass="Validator"></asp:CustomValidator></div>
                    </FooterTemplate>                       
                </asp:TemplateField>  
                                                              
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <Polytex:CustomImageButton ID="CustomImageButtonEdit" runat="server" ImageAlign="Top" CustomButtonType="Edit" MouseEffect="true" /> 
                <Polytex:CustomImageButton ID="CustomImageButtonProperties" runat="server" ImageAlign="Top" CustomButtonType="Properties" MouseEffect="true" CommandName="Properties" CommandArgument='<%# Bind("ID") %>' />              
                <Polytex:CustomImageButton ID="CustomImageButtonDisable" runat="server" ImageAlign="Middle" CustomButtonType="Disable" CommandName="Disable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() == "" %>' />
                <Polytex:CustomImageButton ID="CustomImageButtonEnable" runat="server" ImageAlign="Middle" CustomButtonType="Enable" CommandName="Enable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() != "" %>' />               
            </ItemTemplate>            
            <EditItemTemplate>                                
                <Polytex:CustomImageButton ID="CustomImageButtonUpdate" runat="server" IsGridRowSubmitButton="true" CustomButtonType="Update" ValidationGroup="GridEditItem" MouseEffect="true" CommandName="UpdateActivityGroup" CommandArgument='<%# Bind("ID") %>' />
                <Polytex:CustomImageButton ID="CustomImageButtonCancel" runat="server" CustomButtonType="Cancel" MouseEffect="true" />                                               
            </EditItemTemplate>                  
            <FooterTemplate>
               <Polytex:CustomImageButton ID="CustomImageButtonAdd" runat="server" IsGridRowSubmitButton="true" CustomButtonType="Add" ValidationGroup="GridAddItem" CommandName="Insert" MouseEffect="true" />
            </FooterTemplate>
            <HeaderStyle Width="80px" />
            <ItemStyle CssClass="DivButtons" VerticalAlign="Top" />                             
        </asp:TemplateField> 
                     
            </Columns>         
        </asp:GridView>
        <Polytex:CheckBox ID="CheckBoxIncludeDisabled" runat="server" Checked="false" Trans="IncludeDisabled" AutoPostBack="True" OnCheckedChanged="CheckBoxIncludeDisabled_CheckedChanged"  />
        <div class="ActionMessage"><Polytex:Label ID="PolytexLabelActionMessage" runat="server" EnableViewState="false" Visible="false" OnLoad="ActionMessage_Load"></Polytex:Label></div>
    </td>
    <td style="vertical-align:top;width:50%;">
        <div id="DivItems" runat="server" visible="false">            
            <asp:GridView ID="GridView2" runat="server" Width="98%" HorizontalAlign="Center" DataSourceID="ObjectDataSource2" OnLoad="GridView2_Load" OnInit="GridViewOnInit" OnRowCommand="GridViewOnRowTypesCommand" PageSize="1000000">
                <Columns>
                    <asp:BoundField DataField="ID" Visible="False" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <Polytex:Label ID="LabelHeaderItemName" runat="server" Trans='<%#_T["ActivityType"] %>'></Polytex:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelItemName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                        </ItemTemplate>                        
                        <EditItemTemplate>
                            <Polytex:TextBox ID="TextBoxActivityTypeName" runat="server" CssClass="GridEditItemContent" InputType="String" Text='<%# Bind("NAME") %>' ValidationGroup="GridEditActivityTypes" TextBoxMaxLength="50" TextBoxColumns="12" OnPreRender="SetOnLoadFocus" Argument="4" IsRequired="true"  />
                            <div align="<%#CultureAlign %>"><asp:CustomValidator ID="CustomValidatorEditItemName" runat="server" ErrorMessage='<%#_T["ItemNameExists"] %>' ValidationGroup="GridEditActivityTypes" Display="Dynamic" OnServerValidate="CustomValidatorActivityTypeNameUnique" SetFocusOnError="True" CssClass="Validator"></asp:CustomValidator></div>
                        </EditItemTemplate>
                        <FooterTemplate>                          
                            <Polytex:TextBox ID="TextBoxActivityTypeName" runat="server" CssClass="GridFooterContent" InputType="String" ValidationGroup="GridAddActivityTypes" TextBoxMaxLength="50" TextBoxColumns="12" OnPreRender="SetOnLoadFocus" Argument="3" IsRequired="true" />                
                            <div align="<%#CultureAlign %>"><asp:CustomValidator ID="CustomValidatorAddItemName" runat="server" ErrorMessage='<%#_T["ItemNameExists"] %>' ValidationGroup="GridAddActivityTypes" Display="Dynamic" OnServerValidate="CustomValidatorActivityTypeNameExists" SetFocusOnError="True" CssClass="Validator"></asp:CustomValidator></div>
                        </FooterTemplate>                                                 
                    </asp:TemplateField>
                                                                               
                    <asp:TemplateField ShowHeader="False">               
                        <ItemTemplate>
                            <Polytex:CustomImageButton ID="CustomImageButtonEdit" runat="server" ImageAlign="Top" CustomButtonType="Edit" MouseEffect="true" />
                            <Polytex:CustomImageButton ID="CustomImageButtonDisable" runat="server" ImageAlign="Middle" CustomButtonType="Disable" CommandName="Disable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() == "" %>' ValidationGroup="DisableItem" />
                            <Polytex:CustomImageButton ID="CustomImageButtonEnable" runat="server" ImageAlign="Middle" CustomButtonType="Enable" CommandName="Enable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() != "" %>' />
                        </ItemTemplate>
                        <EditItemTemplate>                
                            <Polytex:CustomImageButton ID="CustomImageButtonUpdate" runat="server" CustomButtonType="Update" IsGridRowSubmitButton="true" CommandName="UpdateActivityTypes" ValidationGroup="GridEditActivityTypes" MouseEffect="true" CommandArgument='<%# Bind("ID") %>' />
                            <Polytex:CustomImageButton ID="CustomImageButtonCancel" runat="server" CustomButtonType="Cancel" MouseEffect="true" /></div>
                        </EditItemTemplate>                                           
                        <FooterTemplate>
                           <Polytex:CustomImageButton ID="CustomImageButtonAdd" runat="server" IsGridRowSubmitButton="true" CustomButtonType="Add" ValidationGroup="GridAddActivityTypes" CommandName="Insert" MouseEffect="true" CommandArgument="0" />
                        </FooterTemplate>                        
                        <HeaderStyle Width="56px" />
                        <ItemStyle CssClass="DivButtons" VerticalAlign="Top" />                                                    
                    </asp:TemplateField>                              
                </Columns>            
            </asp:GridView>
            <Polytex:CheckBox ID="CheckBoxActivityType" runat="server" Checked="false" Trans="IncludeDisabled" AutoPostBack="True" OnCheckedChanged="CheckBoxIncludeDisabledActivityType_CheckedChanged"  />
            <div class="ActionMessage"><Polytex:Label ID="PolytexLabelActionMessage2" runat="server" EnableViewState="false" Visible="false" OnLoad="ActionMessage2_Load"></Polytex:Label></div>
        </div>
    </td>
</tr>
</table>
</asp:Content>

