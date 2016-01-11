<%@ Page Language="C#" MasterPageFile="~/Popups/Popups.master" AutoEventWireup="true" CodeFile="PleaseWait.aspx.cs" Inherits="Popups_PleaseWait" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table id="TablePleaseWait" cellpadding="0" cellspacing="0" border="0" style="height:<%=RequestQuerystring("height") %>px;" align="center">
<tr>
    <td><img src="../Images/Loading.gif" width="20" height="20" alt="" /></td>
    <td>&nbsp;<%=_T["PleaseWait"] %></td>
</tr>
</table>
<table id="TableResultMessage" cellpadding="0" cellspacing="0" border="0" style="display:none;height:<%=RequestQuerystring("height") %>px;" align="center">
<tr>    
    <td id="TdResultMessage"></td>
</tr>
</table>

<script type="text/javascript">
var xmlHttp;

function UpdateStationsWithPrepaid()
{            
    var xmlHttpUpdateStations = ajaxObject();
   
    xmlHttpUpdateStations.onreadystatechange = function()
        { 
            if(xmlHttpUpdateStations.readyState==4)
            {                                                                      		        		                                             				                
                var strResults = xmlHttpUpdateStations.responseText;
                //alert(strResults);
                var TdResultMessage = document.getElementById("TdResultMessage");
                var TableResultMessage = document.getElementById("TableResultMessage");                
                var TablePleaseMessage = document.getElementById("TablePleaseMessage");     
                
                if (strResults == "-1")
                {                        
                    TdResultMessage.innerHTML = "<%=_T["ErrorOccured"] %>";
                    TableResultMessage.style.display = "block";
                    TablePleaseWait.style.display = "none";
                }
                else
                {
                    var successCount = strResults.split(",")[0];
                    var failedCount = strResults.split(",")[1];
                    
                    var strHtml = "<span class=ActionSuccess><%=_T["StationsUpdated"] %>".toString().replace("%stationsCount%", successCount) + ".</span><br>";
                    strHtml += "<span class=ActionFailure><%=_T["StationsUpdateFailed"] %>".toString().replace("%stationsCount%", failedCount) + ".</span>";
                    
                    TdResultMessage.innerHTML = strHtml;
                    TableResultMessage.style.display = "block";
                    TablePleaseWait.style.display = "none";                    
                }
                                                                       		        				                               
            }
        }
        
        xmlHttpUpdateStations.open("GET","../Ajax/GetValue.aspx?action=UpdateStationsWithPrepaid&" + TrickRefresh(), true);                                              
        xmlHttpUpdateStations.send(null);     
}


function SendSupport()
{                    
    var xmlHttpUpdateStations = ajaxObject();
    
    xmlHttpUpdateStations.onreadystatechange = function()
        { 
            if(xmlHttpUpdateStations.readyState==4)
            {                                                                      		        		                                             				                
                var strResults = xmlHttpUpdateStations.responseText;
                //alert(strResults);

                if (strResults == "-1")
                {                        
                    TdResultMessage.innerHTML = "<%=_T["ErrorOccured"] %>";
                    TableResultMessage.style.display = "block";
                    TablePleaseWait.style.display = "none";
                }
                else
                {                        
                    var strHtml = "<span class=ActionSuccess>" + strResults + " <%=_T["FilesSent"] %>.</span>";                                                
                    TdResultMessage.innerHTML = strHtml;
                    TableResultMessage.style.display = "block";
                    TablePleaseWait.style.display = "none";                    
                }
                                                                       		        				                               
            }
        }
        
        var filesToSend = Trim("<%=RequestQuerystring("filesToSend")%>");
        
        if (filesToSend == "")
        {
            TdResultMessage.innerHTML = "<span class=ActionFailure><%=_T["FileRequired"] %>".toString() + ".</span>";            
            TableResultMessage.style.display = "block";
            TablePleaseWait.style.display = "none";            
        }
        else
        {            
            xmlHttpUpdateStations.open("GET","../Ajax/SendSupport.aspx?filesToSend=" + filesToSend + "&" + TrickRefresh(), true);
            xmlHttpUpdateStations.send(null);          
        }                     
}



function StationSyncUsers(stationId, stataionIp)
{
    //alert(stationId);
    //alert(stataionIp);        
    if (stationId == 1)
    {
        //alert("");
    }
}

function StationSyncUsers(stationId, stationIp)
{
    CheckCommunication(stationId, stationIp);
}



function CheckCommunication(stationId, stationIp)
{        
    if (stationId > 0 && stationIp != "")
    {                   
        xmlHttp = ajaxObject();
        
        xmlHttp.onreadystatechange = function()
        {                    
            if(xmlHttp.readyState==4)
            {                                           
                var commCheckResult = "";
                try
                {                              
                    commCheckResult = xmlHttp.responseText.toString();                                                                                                                                                                                                                                                         
                }                                                           
                catch (e)
                { 
                    //alert(e);                    
                }
                
                if (commCheckResult == "1")               
                {//Communication OK, perform Sync without confirm
                    parent.confirmReturnValue = true;
                    parent.objConfirmButton.click();                    
                }
                else
                {                    
                    window.location = top.SiteURL + "Popups/Confirm.aspx?confirmType=StationSync&actionId=ConfirmStationSync";
                }                
            }                           
        }
                        
        //Start checking            
        xmlHttp.open("POST", "../Ajax/CheckCommunication.aspx?checkType=stations&" + TrickRefresh(), true);
        xmlHttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded;');    
        xmlHttp.send("stationId=" + stationId + "&stationIp=" + stationIp);                
    }            
}


function UpdateInactiveSchedule()
{        
    {        
        var xmlHttpUpdateInactiveSchedule = ajaxObject();
       
        xmlHttpUpdateInactiveSchedule.onreadystatechange = function()
            { 
                if(xmlHttpUpdateInactiveSchedule.readyState==4)
                {                                                                      		        		                                             				                
                    var strResults = xmlHttpUpdateInactiveSchedule.responseText;
                    //alert(strResults);
                    if (strResults == "-1")
                    {                        
                        TdResultMessage.innerHTML = "<span class=ActionFailure><%=_T["ErrorOccured"] %></span>";
                    }
                    else if (strResults == "1")
                    {
                        TdResultMessage.innerHTML = "<span class=ActionSuccess><%=_T["UpdateSuccessful"] %></span>";
                    }
                    else
                    {
                        TdResultMessage.innerHTML = "<span class=ActionFailure><%=_T["UpdateFailure"] %></span>";
                    }
                    
                    TableResultMessage.style.display = "block";
                    TablePleaseWait.style.display = "none";                                                                                                                                           		        				                               
                }
            }
                        
            xmlHttpUpdateInactiveSchedule.open("GET","../Ajax/UpdateInactiveSchedule.aspx?stationId=<%=stationId %>&" + TrickRefresh(), true);
            xmlHttpUpdateInactiveSchedule.send(null); 
    }  
}


function SendReportMailingList()
{
    xmlHttp = ajaxObject();
    
    xmlHttp.onreadystatechange = function()
    {                    
        if(xmlHttp.readyState==4)
        {                                           
            var reportMailingListResult = "";
            try
            {                              
                reportMailingListResult = xmlHttp.responseText.toString();
                
                if (reportMailingListResult == "1")
                {                                            
                    var strHtml = "<span class=ActionSuccess><%=_T["FilesSent"] %>.</span>";                                                
                    TdResultMessage.innerHTML = strHtml;
                    TableResultMessage.style.display = "block";
                    TablePleaseWait.style.display = "none";                     
                }
                else
                {
                    TdResultMessage.innerHTML = "<%=_T["ErrorOccured"] %>";
                    TableResultMessage.style.display = "block";
                    TablePleaseWait.style.display = "none";
                }                                                                                                                                                                                                                                                                                                
            }                                                           
            catch (e)
            { 
                alert(e);                    
            }           
        }                           
    }
    
    //alert("../Ajax/SendReportMailingList.aspx?mailingListId=<%=RequestQuerystring("mailingListId") %>&" + TrickRefresh());
    xmlHttp.open("GET","../Ajax/SendReportMailingList.aspx?mailingListId=<%=RequestQuerystring("mailingListId") %>&" + TrickRefresh(), true);             
    xmlHttp.send(null); 
}


<%=jsFunctionToCall %>

</script>
</asp:Content>

