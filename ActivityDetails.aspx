<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="ActivityDetails.aspx.cs" Inherits="ActivityDetails" ValidateRequest="false" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 

  <link rel="stylesheet" href="//code.jquery.com/ui/1.11.3/themes/smoothness/jquery-ui.css">
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.3/jquery-ui.js"></script>
  <script type="text/javascript">

    //autocomplete function - jquery
	$(function() {	
		var availableTags = [ <%= clientList %> ];

		$( "#<%= TextBoxClient.ClientID %>" ).autocomplete({
			source: availableTags,
            change: function(e, ui) { OnClientPress()}  
		});  
	});
	
	function OnClientPress()
	{
	    //Array of Client Names
	    var availableTags = [ <%= clientList %> ];
	    
	    //Array of Clients Id
	    var idTags = [ <%= clientListId %> ];
		
		var TextClient = document.getElementById ("<%=TextBoxClient.ClientID %>").value;
		var ID = availableTags.indexOf(TextClient);
		
		//Update ClientId to HiddenFieldClientId
		document.getElementById("<%=HiddenFieldClientId.ClientID %>").value=idTags[ID];
		
		
		__doPostBack("<%=TextBoxClient.ClientID %>","TextChanged");
	}
        
	</script>
	
<input type="hidden" id="HiddenFieldClientId" name="ClientId" runat="server" EnableViewState="true" />
<input type="hidden" id="HiddenFieldActivityId" name="ActivityId" runat="server" EnableViewState="true" />

	  
<Polytex:ObjectDataSource ID="ObjectDataSourceActivityGroups" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Activity_Groups">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<Polytex:ObjectDataSource ID="ObjectDataSourceActivityTypes" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Activity_Types">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
            <asp:Parameter DefaultValue="0" Name="ActivityGroupId" Type="Int32" />
        </SelectParameters>
</Polytex:ObjectDataSource>

<Polytex:ObjectDataSource ID="ObjectDataSourceClient" runat="server" SelectMethod="Select" TypeName="PolytexData.Manage_Clients">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<Polytex:ObjectDataSource ID="ObjectDataSourceUnit" runat="server" SelectMethod="SelectByClient" TypeName="PolytexData.Manage_Units">
    <SelectParameters>
        <asp:Parameter DefaultValue="false" Name="includeDisabled" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="territoryId" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="clientId" Type="Int32" />
    </SelectParameters>
</Polytex:ObjectDataSource>

<table cellpadding="2" cellspacing="3" border="0" style="margin:0px 12px 0px 12px;">
<tr>
    <td><Polytex:Label ID="LabelActivityStart" runat="server" Trans="ActivityDetail_FromDate"></Polytex:Label>:</td>
    <td>
        <Polytex:TextBox ID="textboxActivityStart" runat="server" InputType="Date" IsRequired="true" ValidationGroup="GridEditItem" ValidatorsPosition="Horizontal" InsertEmptyItem="true"/>
        <div align="<%#CultureAlign %>"><asp:CustomValidator ID="CustomValidatorDate" runat="server" ValidationGroup="GridEditItem" Display="Dynamic" OnServerValidate="CustomValidator_CheckDate" SetFocusOnError="True" CssClass="Validator" ValidatorsPosition="Horizontal"></asp:CustomValidator></div>
    </td>
</tr> 
<tr>         
    <td><Polytex:Label ID="LabelClient" runat="server" Trans="Client"></Polytex:Label>:</td> 
        <td>
            <asp:TextBox ID="TextBoxClient" runat="server" InputType="String" IsRequired="true" CssClass="GridEditItemContent" ValidationGroup="GridEditItem" InsertEmptyItem="true" ValidatorsPosition="Horizontal" onchange="javascript: OnClientPress();"></asp:TextBox>
            <asp:CustomValidator ID="CustomValidatorCheckClient" runat="server" IsRequired="true" ValidateEmptyText="true" ValidationGroup="GridEditItem" ControlToValidate="TextBoxClient" ValidatorsPosition="Horizontal" SetFocusOnError="true" OnServerValidate="CustomValidator_CheckClientExist"></asp:CustomValidator>
        </td>                         
</tr>
<tr>
    <td><Polytex:Label ID="LabelTimeStart" runat="server" Trans="ActivityDetail_FromTime" DropDownListWidth="150">:</Polytex:Label>:&nbsp;</td>
     <td><Polytex:DropDownList ID="DropDownListStartTime" runat="server" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridEditItemContent" ValidationGroup="GridEditItem" ValidatorsPosition="Horizontal" DropDownListWidth="150"/></td>
</tr>  
<tr>   
    <td><Polytex:Label ID="LabelDriveTime" runat="server" Trans="ActivityDetail_DriveTime"></Polytex:Label>:</td>
    <td><Polytex:DropDownList ID="DropDownListDriveTime" runat="server" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridEditItemContent" ValidationGroup="GridEditItem" ValidatorsPosition="Horizontal" DropDownListWidth="150"/></td>
</tr>          
<tr>         
    <td><Polytex:Label ID="LabelGroupName" runat="server" Trans="Activity_Group" ></Polytex:Label>:</td> 
    <td><Polytex:DropDownList ID="DropDownListActivityGroup" runat="server" DataSourceID="ObjectDataSourceActivityGroups" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridEditItemContent" ValidationGroup="GridEditItem"  ValidatorsPosition="Horizontal" DropDownListWidth="150" ></Polytex:DropDownList></td>               
</tr> 
<tr>         
    <td><Polytex:Label ID="LabelTypeName" runat="server" Trans="Activity_Type" ></Polytex:Label>:</td> 
    <td> <Polytex:DropDownList ID="DropDownListActivityType" runat="server" DataSourceID="ObjectDataSourceActivityTypes" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridEditItemContent" ValidationGroup="GridEditItem"  ValidatorsPosition="Horizontal"  DropDownListWidth="150"></Polytex:DropDownList> </td>               
</tr> 
<tr>         
    <td><Polytex:Label ID="LabelUnits" runat="server" Trans="Units"></Polytex:Label>:</td> 
    <td> <Polytex:DropDownList ID="DropDownListUnit" runat="server" DataSourceID="ObjectDataSourceUnit" DataTextField="UNIT_NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridEditItemContent" ValidationGroup="GridEditItem"  ValidatorsPosition="Horizontal"  DropDownListWidth="150"></Polytex:DropDownList> </td>               
</tr> 
<tr>         
    <td><Polytex:Label ID="LabelDescriptionNameHeader" runat="server" Trans="Description"></Polytex:Label>:</td> 
    <td>
        <asp:TextBox ID="TextBoxDescription" runat="server" TextMode="MultiLine" Rows="3" dir="ltr" InputType="String" ValidationGroup="GridEditItem" CssClass="GridEditItemContent" IsRequired="True" maxlength="400"  ValidatorsPosition="Horizontal" style="text-align:left" Width="150"></asp:TextBox>
        <asp:CustomValidator ID="CustomValidatorDescription" runat="server" ValidationGroup="GridEditItem" CssClass="Validator" ControlToValidate="TextBoxDescription" SetFocusOnError="true" ValidateEmptyText="true" ValidatorsPosition="Horizontal" ClientValidationFunction="TextareaRequired" ></asp:CustomValidator>
        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidatorDescription" ControlToValidate="TextBoxDescription" ValidationExpression="^[\s\S]{0,400}$" ValidationGroup="GridEditItem" Display="Dynamic"></asp:RegularExpressionValidator>
    </td>               
</tr> 
<tr>         
    <td><Polytex:Label ID="LabelPartsNameHeader" runat="server" Trans="Parts"></Polytex:Label>:</td> 
    <td>
        <asp:TextBox ID="TextBoxParts" runat="server" TextMode="MultiLine" Rows="2" dir="ltr" InputType="String" ValidationGroup="GridEditItem" CssClass="GridEditItemContent" IsRequired="True" maxlength="200"  ValidatorsPosition="Horizontal" style="text-align:left" Width="150"></asp:TextBox>
        <asp:CustomValidator ID="CustomValidatorParts" runat="server" ValidationGroup="GridEditItem" CssClass="Validator" ControlToValidate="TextBoxParts" SetFocusOnError="true" ValidateEmptyText="true" ValidatorsPosition="Horizontal" ClientValidationFunction="TextareaRequired" ></asp:CustomValidator>
        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidatorParts" ControlToValidate="TextBoxParts" ValidationExpression="^[\s\S]{0,200}$" ValidationGroup="GridEditItem" Display="Dynamic"></asp:RegularExpressionValidator>
    </td>               
</tr> 
<tr>         
    <td><Polytex:Label ID="LabelSolutionNameHeader" runat="server" Trans="Solution"></Polytex:Label>:</td> 
    <td>
        <asp:TextBox ID="TextBoxSolution" runat="server" TextMode="MultiLine" Rows="3" dir="ltr" InputType="String" ValidationGroup="GridEditItem" CssClass="GridEditItemContent" IsRequired="True" maxlength="200"  ValidatorsPosition="Horizontal" style="text-align:left" Width="150"></asp:TextBox>
        <asp:CustomValidator ID="CustomValidatorSolution" runat="server" ValidationGroup="GridEditItem" CssClass="Validator" ControlToValidate="TextBoxSolution" SetFocusOnError="true" ValidateEmptyText="true" ValidatorsPosition="Horizontal" ClientValidationFunction="TextareaRequired" ></asp:CustomValidator>
        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidatorSolution" ControlToValidate="TextBoxSolution" ValidationExpression="^[\s\S]{0,200}$" ValidationGroup="GridEditItem" Display="Dynamic"></asp:RegularExpressionValidator>
    </td>                
</tr> 
<tr>         
    <td><Polytex:Label ID="LabelRecommendationsNameHeader" runat="server" Trans="Recommendations"></Polytex:Label>:</td> 
    <td>
        <asp:TextBox ID="TextBoxRecommendations" runat="server" TextMode="MultiLine" Rows="3" dir="ltr" InputType="String" ValidationGroup="GridEditItem" CssClass="GridEditItemContent" IsRequired="True" maxlength="200"  ValidatorsPosition="Horizontal" style="text-align:left" Width="150"></asp:TextBox>    
        <asp:CustomValidator ID="CustomValidatorRecommendation" runat="server" ValidationGroup="GridEditItem" CssClass="Validator" ControlToValidate="TextBoxRecommendations" SetFocusOnError="true" ValidateEmptyText="true" ValidatorsPosition="Horizontal" ClientValidationFunction="TextareaRequired" ></asp:CustomValidator>
        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidatorRecommendations" ControlToValidate="TextBoxRecommendations" ValidationGroup="GridEditItem" ValidationExpression="^[\s\S]{0,200}$" Display="Dynamic"></asp:RegularExpressionValidator>
    </td>                           
</tr> 
<tr>
    <td><Polytex:Label ID="LabelTimeEnd" runat="server" Trans="ActivityDetail_EndTime"></Polytex:Label>:</td>
    <td>
        <Polytex:DropDownList ID="DropDownListTimeEnd" runat="server" DataTextField="NAME" DataValueField="ID" IsRequired="true" InsertEmptyItem="true" CssClass="GridEditItemContent" ValidationGroup="GridEditItem" ValidatorsPosition="Horizontal" DropDownListWidth="150"/>
        <asp:CustomValidator ID="CustomValidatorTimeEnd" runat="server" ValidationGroup="GridEditItem" Display="Dynamic" OnServerValidate="CustomValidator_CheckTimeEnd" SetFocusOnError="True" CssClass="Validator" ValidatorsPosition="Horizontal"></asp:CustomValidator>   
    </td>
</tr>
<tr>
    <td><Polytex:Label ID="LabelUploadImage" runat="server" Trans="Image"></Polytex:Label>:</td>
     <td><asp:FileUpload  ID="FileUploadImage" runat="server" ValidationGroup="GridEditItem" accept="image/*" style="width: 12.5em" onchange="setImage(this);" />
         <asp:CustomValidator ID="CustomValidatorImageSize" runat="server" ValidationGroup="GridEditItem" ControlToValidate="FileUploadImage" OnServerValidate="CustomValidator_FileSizeValidate"></asp:CustomValidator>
     </td>
</tr> 
<tr>
<td></td>
<td> <asp:Image ID="ImgPreview" runat="server" Width="100px" Height="100px" /> </td>
</tr>  
</table>

<Polytex:Button ID="ButtonSaveActivity" runat="server" Trans="SaveAndContinueEditing" ValidationGroup="GridEditItem" style="background-position:<%# CultureAlign %> ;right: 138px;" OnClick="onButtonClick" />
<Polytex:Button ID="ButtonSaveActivityAndClose" runat="server" Trans="SaveAndClose" ValidationGroup="GridEditItem" style="background-position:<%# CultureAlign %> ;right: 38px;" OnClick="onButtonClick" />
<br />
<div class="ActionMessage"><Polytex:Label ID="PolytexLabelActionMessage" runat="server" EnableViewState="false" Visible="false" OnLoad="ActionMessage_Load"></Polytex:Label></div>
<br /><br />
     
</asp:Content>

