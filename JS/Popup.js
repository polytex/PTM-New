function GetIframePopupObject()
{
    return document.getElementById("PopupIframe");
}

function LoadPopup(popupLocation, refreshPopup)
{
    if (GetIframePopupObject().src.toString().indexOf(popupLocation) == -1 || refreshPopup)
    {
        GetIframePopupObject().src = top.SiteURL + "Popups/" + popupLocation + "&" + TrickRefresh();
    }
}


function DisplayPopup(popupWidth, popupHeight)
{
    var divPopup = document.getElementById("DivPopup");
                    
    var selectBoxes = document.getElementsByTagName("SELECT");
    
    if (IsClientBrowserIE() && top.BrowserVersion == "6")
    {                
        
        for (i=0; i<selectBoxes.length; i++)
        {
            selectBoxes[i].style.visibility = "hidden";
        }
    }
    
    var imgBlocker = document.getElementById("ImgBlocker"); 
    
    imgBlocker.style.width = "100%";
    
    imgBlocker.style.height = document.body.clientHeight + "px";    
    imgBlocker.style.display = "block";
    
//    if (popupHeight > f_clientHeight())
//    {//Popup height more than client height.
//        popupHeight = f_clientHeight() - 30;               
//    }    
               
    var horizontalPosition = parseInt(((f_clientWidth() - parseInt(popupWidth)) / 2) + document.documentElement.scrollLeft);
    var verticalPosition = parseInt(((f_clientHeight()- parseInt(popupHeight)) / 2)  + document.documentElement.scrollTop);        
    
    if (IsMobile) 
    {
        divPopup.style.top = "110px";
    }
    else 
    {
        divPopup.style.top = verticalPosition + "px";
    }
    
    divPopup.style.left = horizontalPosition + "px";    
    divPopup.style.zIndex = zIndex + 1;
    divPopup.style.width = popupWidth + "px";

    divPopup.style.height = popupHeight + "px";
    GetIframePopupObject().style.height = "100%";
    
    divPopup.style.display = "block";   //If changed function EscPopupClicked should be changed                                          
}

function OpenPopup(popupLocation, popupWidth, popupHeight, refreshPopup)
{                               
    popupLocation = popupLocation + "&width=" + popupWidth + "&height=" + (popupHeight - 23);
    LoadPopup(popupLocation, refreshPopup);
    DisplayPopup(popupWidth, popupHeight);               
}

function HidePopup()
{

}

function UnloadPopup()
{

}

function ClosePopup()
{
    var divPopup = document.getElementById("DivPopup");
    var iframePopup = GetIframePopupObject();    
    var selectBoxes = document.getElementsByTagName("SELECT");
    var imgBlocker = document.getElementById("ImgBlocker"); 
    
                       
    if (IsClientBrowserIE())
    {                        
        for (i=0; i<selectBoxes.length; i++)
        {
            selectBoxes[i].style.visibility = "visible";
        }
    }
                   
    imgBlocker.style.display = "none";
    imgBlocker.style.width = "0px";
    imgBlocker.style.height = "0px";            
    divPopup.style.display = "none";     
    divPopup.style.width = "0px";
    divPopup.style.height = "0px";
    divPopup.style.zIndex = -1;
    divPopup.style.top = "0px";
    divPopup.style.left = "0px";
    
    iframePopup.style.height = "100%";
    
    
    
    
}

//FOR MULTI TAB POPUPS
function HighlightTab(tabLinkId)
{
    document.getElementById(tabLinkId).className = "TextLightOrange";
}

function UnhighlightTab(tabLinkId)
{
    document.getElementById(tabLinkId).className = "";
}

function ClickTab(urlGoTo)
{
    window.location = urlGoTo;
}

/*For confirm popup*/
var confirmReturnValue;
var objConfirmButton;
function Confirm(objButton, confirmType)
{                        
    var divPopup = document.getElementById("DivPopup");
    if (divPopup.style.display == "none")
    {        
        objConfirmButton = objButton;
        OpenPopup("Confirm.aspx?confirmType=" + confirmType, 220, 100, false);        
        return false;
    }
    else
    {                
        ClosePopup();
        return confirmReturnValue;
    }    
}

/*For confirm popup with mandatory text and logging*/
function ConfirmLog(objButton, confirmType)
{                        
    var divPopup = document.getElementById("DivPopup");
    if (divPopup.style.display == "none")
    {        
        var strConfirmUrl = "ConfirmLog.aspx?confirmType=" + confirmType;
        
        if (confirmType == "disableUsers" || confirmType == "enableUsers")
        {
            if (!isNaN(usersCount));
            {
                strConfirmUrl += "&usersCount=" + usersCount;            
            }
        }
        objConfirmButton = objButton;
        OpenPopup(strConfirmUrl, 340, 180, false);        
        return false;
    }
    else
    {                
        ClosePopup();        
        return confirmReturnValue;
    }    
}

/*For calendar popup*/
function Calendar(calendarButton)
{
    var calendarTextBox;
    if (IsClientBrowserIE())
    {
        calendarTextBox = calendarButton.parentNode.parentNode.firstChild.firstChild;
    }
    else
    {
        calendarTextBox = calendarButton.parentNode.parentNode.childNodes[1].firstChild;        
    }
    
    var divPopup = document.getElementById("DivPopup");
    
    if (divPopup.style.display == "none")
    {                
        var calendarWidth = 280;
        var calendarHeight = 187;
        
        OpenPopup("Calendar.aspx?selectedDate=" + calendarTextBox.value + "&textBoxId=" + calendarTextBox.id, calendarWidth, calendarHeight, false);                
    }
    else
    {                
        ClosePopup();        
    }                
}

function OpenItemsRfidLookup()
{
    OpenPopup("ItemsRfidLookup.aspx?rfid=" + document.getElementById("TextBoxRfid").value, 580, 200, false);
}


function BalancesAdmin(objButton, confirmType)
{                        
    var divPopup = document.getElementById("DivPopup");
    if (divPopup.style.display == "none")
    {        
        objConfirmButton = objButton;        
        OpenPopup("BalancesAdmin.aspx?", 380, 210, true);  
        return false;
    }
    else
    {                
        ClosePopup();
        return confirmReturnValue;
    }    
}

function SwitchTarget()
{
    document.forms[0].target = "";
    document.forms[0].action = "";
    document.forms[0].submit();
}




