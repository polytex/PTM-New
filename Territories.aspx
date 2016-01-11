<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Territories.aspx.cs" Inherits="Territories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<Polytex:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Territories">
     <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="false" Name="includeWorldwide" Type="Boolean" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<span id="spanHiddenFieldsForExport">
<input id="ReportName" name="ReportName" type="hidden" value="<%=CurrentPage %>" />

<input id="TextBoxTerritoryId" name="TextBoxTerritoryId" type="hidden" value="<%=CurrentUser.TerritoryId.ToString() %>" />

</span>


<asp:GridView ID="GridView1" Width="100%" runat="server" PageSize="1000000"
    DataSourceID="ObjectDataSource1" OnInit="GridViewOnInit" OnRowCommand="GridViewOnRowCommand"> 
    <Columns>
        <asp:BoundField DataField="ID" Visible="False" />
                <asp:TemplateField>
            <HeaderTemplate>
                <Polytex:Label ID="LabelNameHeader" runat="server" Trans="Territory"></Polytex:Label>
            </HeaderTemplate>            
            <ItemTemplate>
                <asp:Label ID="LabelName" runat="server" Text='<%# Bind("NAME") %>' ></asp:Label>
            </ItemTemplate>            
            <EditItemTemplate>
                <Polytex:TextBox ID="TextBoxName" runat="server" CssClass="GridEditItemContent" InputType="String" IsRequired="True" Text='<%# Bind("NAME") %>' ValidationGroup="GridEditItem" TextBoxMaxLength="25" OnPreRender="SetOnLoadFocus" Argument="2" />
                <div align="<%#CultureAlign %>"><asp:CustomValidator ID="CustomValidatorEditTerritoryName" runat="server" ErrorMessage='<%#_T["TerritoryNameExists"] %>' ValidationGroup="GridEditItem" Display="Dynamic" OnServerValidate="CustomValidatorTerritoryNameUnique" SetFocusOnError="True" CssClass="Validator"></asp:CustomValidator></div>
            </EditItemTemplate>
            <FooterTemplate>
                <Polytex:TextBox ID="TextBoxName" runat="server" Text='<%# Bind("NAME") %>' InputType="String" ValidationGroup="GridAddItem" CssClass="GridFooterContent" IsRequired="True" TextBoxMaxLength="25" OnPreRender="SetOnLoadFocus" Argument="1" />
                <div align="<%#CultureAlign %>"><asp:CustomValidator ID="CustomValidatorAddTerritoryName" runat="server" ErrorMessage='<%#_T["TerritoryNameExists"] %>' ValidationGroup="GridAddItem" Display="Dynamic" OnServerValidate="CustomValidatorTerritoryNameExists" SetFocusOnError="True" CssClass="Validator"></asp:CustomValidator></div>
            </FooterTemplate>                       
        </asp:TemplateField>
       
        
        
        
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <Polytex:CustomImageButton ID="CustomImageButtonEdit" runat="server" ImageAlign="Top" CustomButtonType="Edit" MouseEffect="true" />               
                <Polytex:CustomImageButton ID="CustomImageButtonDisable" runat="server" ImageAlign="Middle" CustomButtonType="Disable" CommandName="Disable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() == "" %>' />                
                <Polytex:CustomImageButton ID="CustomImageButtonEnable" runat="server" ImageAlign="Middle" CustomButtonType="Enable" CommandName="Enable" CommandArgument='<%# Bind("ID") %>' MouseEffect="True" Visible='<%#Eval("DISABLE_DATE").ToString().Trim() != "" %>' />               
            </ItemTemplate>            
            <EditItemTemplate>                                
                <Polytex:CustomImageButton ID="CustomImageButtonUpdate" runat="server" IsGridRowSubmitButton="true" CustomButtonType="Update" ValidationGroup="GridEditItem" MouseEffect="true" CommandName="UpdateTerritories" CommandArgument='<%# Bind("ID") %>' />
                <Polytex:CustomImageButton ID="CustomImageButtonCancel" runat="server" CustomButtonType="Cancel" MouseEffect="true" />                                               
            </EditItemTemplate>                  
            <FooterTemplate>
               <Polytex:CustomImageButton ID="CustomImageButtonAdd" runat="server" IsGridRowSubmitButton="true" CustomButtonType="Add" ValidationGroup="GridAddItem" CommandName="Insert" MouseEffect="true" />
            </FooterTemplate>
            <HeaderStyle Width="56px" />
            <ItemStyle CssClass="DivButtons" VerticalAlign="Top" />                             
        </asp:TemplateField>        
    </Columns>     
</asp:GridView>
<Polytex:CheckBox ID="CheckBoxIncludeDisabled" runat="server" Checked="false" Trans="IncludeDisabled" AutoPostBack="True" OnCheckedChanged="CheckBoxIncludeDisabled_CheckedChanged"  />
<div class="ActionMessage"><Polytex:Label ID="PolytexLabelActionMessage" runat="server" EnableViewState="false" Visible="false" OnLoad="ActionMessage_Load"></Polytex:Label></div>
</asp:Content>

