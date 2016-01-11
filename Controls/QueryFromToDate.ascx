<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QueryFromToDate.ascx.cs" Inherits="Controls_QueryFromToDate" %>
<table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
<tr>
    <td style="width:48%;vertical-align:top;">
        <table class="TableQueryFromToDate" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="vertical-align:top;">
                <div class="DivQueryFieldTitle"><Polytex:Label ID="LabelFromDate" runat="server" Trans="Report_txt_FromDate"></Polytex:Label>&nbsp;&nbsp;</div>
            </td>
            <td><Polytex:TextBox ID="TextBoxFromDate" runat="server" ValidatorsPosition="Horizontal" ValidatorDisplay="Dynamic" InputType="Date" IsRequired="true" OnPreRender="TextBoxReportDates_PreRender" ValidationGroup="ReportQuery" /></td>
        </tr>
         <tr>
            <td style="vertical-align:top;">
                <div class="DivQueryFieldTitle"><Polytex:Label ID="LabelToDate" runat="server" Trans="Report_txt_ToDate"></Polytex:Label>&nbsp;&nbsp;</div>
            </td>
            <td><Polytex:TextBox ID="TextBoxToDate" runat="server" ValidatorsPosition="Horizontal" ValidatorDisplay="Dynamic" InputType="Date" IsRequired="true" OnPreRender="TextBoxReportDates_PreRender" ValidationGroup="ReportQuery" /></td>
        </tr>
        </table>
    </td>
    <td style="width:4%;">&nbsp;</td>
    <td style="width:48%;vertical-align:top;">
        <table class="TableFieldsNotStretched" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="vertical-align:top;">
                <div class="DivQueryFieldTitle"><Polytex:Label ID="LabelFromTime" runat="server" Trans="Report_txt_FromTime"></Polytex:Label>&nbsp;&nbsp;</div>
            </td>
            <td><Polytex:TextBox ID="TextBoxFromTime" runat="server" ValidatorsPosition="Horizontal" ValidatorDisplay="Dynamic" InputType="Time" IsRequired="true" Text="00:00" ValidationGroup="ReportQuery" /></td>
        </tr>
        <tr>
            <td style="vertical-align:top;">
                <div class="DivQueryFieldTitle"><Polytex:Label ID="LabelToTime" runat="server" Trans="Report_txt_ToTime"></Polytex:Label>&nbsp;&nbsp;</div>
            </td>
            <td><Polytex:TextBox ID="TextBoxToTime" runat="server" ValidatorsPosition="Horizontal" ValidatorDisplay="Dynamic" InputType="Time" IsRequired="true" Text="23:59:59" ValidationGroup="ReportQuery" /></td>
        </tr>            
        </table>
    </td>                
</tr>
</table>