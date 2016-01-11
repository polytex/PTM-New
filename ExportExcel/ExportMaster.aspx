<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportMaster.aspx.cs" Inherits="ExportExcel_ExportMaster" ValidateRequest="false" %><?xml version="1.0"?>
<?mso-application progid="Excel.Sheet"?>
<Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"
 xmlns:o="urn:schemas-microsoft-com:office:office"
 xmlns:x="urn:schemas-microsoft-com:office:excel"
 xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
 xmlns:html="http://www.w3.org/TR/REC-html40">
 <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
		<Author><%= Server.HtmlEncode(CurrentUser.UserName)%></Author>		
		<Created><%= DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") %></Created>
		<Version><%=PolyPage.WebManagerVersion %></Version>		
		<Company>Polytex Technologies Ltd.</Company>
		<Manager></Manager>
		<Subject><%=_T["HeaderReports"]%></Subject>
		<Title><%=_T[ReportName] %></Title>
		<Description><asp:PlaceHolder ID="PlaceholderDescription" runat="server"></asp:PlaceHolder></Description>		
	</DocumentProperties>
	<ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel"></ExcelWorkbook>	
    <Styles>
        <Style ss:ID="Default" ss:Name="Normal">
            <Alignment ss:Vertical="Bottom"/>
            <Borders/>
            <Font />
            <Interior/>
            <NumberFormat/>
            <Protection/>
        </Style>
        <Style ss:ID="s20">
             <Font ss:Bold="1"/>                              
        </Style>                
        <Style ss:ID="s21">
            <NumberFormat ss:Format="Short Date"/>
        </Style>
        <Style ss:ID="s22">
            <NumberFormat ss:Format="General Date"/>
        </Style>
        <Style ss:ID="s23">
            <NumberFormat ss:Format="<%=CultureDateFormat %>"/>
        </Style>
        <Style ss:ID="s24">
            <NumberFormat ss:Format="<%=CultureDateFormat + " " + CultureTimeFormat %>"/>
        </Style>
        <Style ss:ID="s25">
            <Interior ss:Color="#CCCCCC" ss:Pattern="Solid"/>
        </Style>            
    </Styles>
	<Worksheet ss:Name="<%=Server.HtmlEncode((_T[ReportName] == "ReportName" ? "" : _T[ReportName]) ) %>" ss:RightToLeft="<%=ExcelWorksheetDirection %>">        
        <asp:PlaceHolder ID="PlaceHolderTable" runat="server"></asp:PlaceHolder>
        <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
            <asp:PlaceHolder ID="PlaceHolderWorksheetOptions" runat="server">
                <Selected/>
                <DisplayRightToLeft/>
                <DoNotDisplayZeros/>
                <FreezePanes/>
                <FrozenNoSplit/>
                <SplitHorizontal>1</SplitHorizontal>
                <TopRowBottomPane>1</TopRowBottomPane>
                <ActivePane>2</ActivePane>
                <Panes>
                    <Pane>
                        <Number>3</Number>
                    </Pane>
                    <Pane>
                        <Number>2</Number>
                    </Pane>
                </Panes>
                <ProtectObjects>False</ProtectObjects>
                <ProtectScenarios>False</ProtectScenarios>
                <x:AutoFitWidth>True</x:AutoFitWidth>                                   
            </asp:PlaceHolder>                          
            <Selected/>        
        </WorksheetOptions>        
	</Worksheet>
    <x:ExcelWorkbook></x:ExcelWorkbook>
</Workbook>