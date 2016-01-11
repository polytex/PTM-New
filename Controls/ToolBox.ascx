<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ToolBox.ascx.cs" Inherits="Controls_ToolBox" EnableViewState="true" %>
<input type="hidden" id="ConfirmLogComment" name="ConfirmLogComment" value="" />
<input type="hidden" id="HiddenToolboxValue" name="HiddenToolboxValue" value="" />
<table id="TableToolbar" cellpadding="0" cellspacing="0" border="0" style="<%=PolyPage.IsMobile ? "height:35px;width:150px;" : "height:25px;" %>" align="<%=PolyPage.CultureAlignReverse %>">
<tr>
    <td style="width:<%=PolyPage.IsMobile ? "4" : "7" %>px;background-image:url('<%=PolyPage.SiteURL %>Images/Toolbar/<%=PolyPage.CultureAlign %>Side<%=PolyPage.IsMobile ? "2" : "" %>.gif');font-size:1px;">&nbsp;</td>  
    <td id="SideMenu" runat="server" class="ToolBoxTdMobile" visible="false">
        <img src="Images/Sidemenu.gif" width="30" height="30" alt="<%=PolyUtils._T["SideMenu"] %>" title="<%=PolyUtils._T["SideMenu"] %>" class="ToolBoxButtonOut" onmouseover="ToolboxMouseAction('over', this);" onmouseout="ToolboxMouseAction('out', this);" onclick="MenuOnClick(this);return false;" />
    </td> 
    <td id="AddActivity" runat="server" class="ToolBoxTd" visible="false">
        <img id="ImageAddActivity" src="Images/Buttons/Add.gif" width="<%=PolyPage.IsMobile ? "30" : "20" %>" height="<%=PolyPage.IsMobile ? "30" : "20" %> alt="<%=PolyUtils._T["AddActivity"] %>" title="<%=PolyUtils._T["AddActivity"] %>" class="ToolBoxButtonOut" onmouseover="ToolboxMouseAction('over', this);" onmouseout="ToolboxMouseAction('out', this);" onclick="AddActivity();" />
    </td>  
    <td id="ReportQuery" runat="server" class="ToolBoxTd" visible="false">
        <img id="ImageReportQuery" src="Images/Toolbar/ReportQuery.gif" width="<%=PolyPage.IsMobile ? "30" : "20" %>" height="<%=PolyPage.IsMobile ? "30" : "20" %>" alt="<%=PolyUtils._T["ReportQuery"] %>" title="<%=PolyUtils._T["ReportQuery"] %>" class="ToolBoxButtonOut" onmouseover="ToolboxMouseAction('over', this);" onmouseout="ToolboxMouseAction('out', this);" onclick="OpenReportQuery();" />
    </td>             
    <td id="ImportExcel" runat="server" class="ToolBoxTd" visible="false">
        <img src="Images/Toolbar/Import.gif" width="20" height="20" alt="<%=PolyUtils._T["ImportExcel"] %>" title="<%=PolyUtils._T["ImportExcel"] %>" class="ToolBoxButtonOut" onmouseover="ToolboxMouseAction('over', this);" onmouseout="ToolboxMouseAction('out', this);" onclick="ImportExcel('<%=PolyUtils.CurrentPage %>');" />
    </td>
    <td id="ExportExcel" runat="server" class="ToolBoxTd" visible="false">        
        <img src="Images/Toolbar/Export.gif" width="20" height="20" alt="<%=PolyUtils._T["ExportExcel"] %>" title="<%=PolyUtils._T["ExportExcel"] %>" class="ToolBoxButtonOut" onmouseover="ToolboxMouseAction('over', this);" onmouseout="ToolboxMouseAction('out', this);" onclick="ExportExcel();" />
    </td>                
    <td id="PrintReport" runat="server" class="ToolBoxTd" visible="false">
        <img src="Images/Toolbar/Print.gif" width="20" height="20" alt="<%=PolyUtils._T["Print"] %>" title="<%=PolyUtils._T["Print"] %>" class="ToolBoxButtonOut" onmouseover="ToolboxMouseAction('over', this);" onmouseout="ToolboxMouseAction('out', this);" onclick="PrintReport();" /><input type="hidden" id="PrintingLayout" name="PrintingLayout" value="0" />
    </td>
    <td id="Save" runat="server" class="ToolBoxTd" visible="false">
        <Polytex:ImageButton ID="ImageButtonSave" runat="server" Trans="Save" Width="20px" Height="20px" CssClass="ToolBoxButtonOut" ImageUrl="~/Images/Buttons/Update.gif" onmouseover="ToolboxMouseAction('over', this);" onmouseout="ToolboxMouseAction('out', this);"  />        
    </td>                                
    <td id="PrintPage" runat="server" class="ToolBoxTd" visible="false">
        <img src="Images/Toolbar/Print.gif" width="20" height="20" alt="<%=PolyUtils._T["Print"] %>" title="<%=PolyUtils._T["Print"] %>" class="ToolBoxButtonOut" onmouseover="ToolboxMouseAction('over', this);" onmouseout="ToolboxMouseAction('out', this);" onclick="PrintPage();" />
    </td>    
    <td id="EmptyControl" runat="server" class="ToolBoxTdMobileSpacer" visible="false">&nbsp;</td>                
    <td style="width:<%=PolyPage.IsMobile ? "4" : "7" %>px;background-image:url('<%=PolyPage.SiteURL %>Images/Toolbar/<%=PolyPage.CultureAlignReverse %>Side<%=PolyPage.IsMobile ? "2" : "" %>.gif');font-size:1px;">&nbsp;</td>
    <script type="text/javascript">
        
        var IsMobile = "<%=PolyPage.IsMobile %>";
        
        if (IsMobile=="True") 
               {
                 var AddActivity = document.getElementById("<%=AddActivity.ClientID%>");
                 var ReportQuery = document.getElementById("<%=ReportQuery.ClientID%>");
            
            
             if (AddActivity!=null) 
            {
                AddActivity.style.backgroundImage="url('Images/Toolbar/BG2.gif')";
            }
       
            if (ReportQuery!=null) 
            {
                ReportQuery.style.backgroundImage="url('Images/Toolbar/BG2.gif')";
            }
        }

    
    </script>
</tr>
</table>

          
