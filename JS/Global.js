function $(id){return document.getElementById(id);}
function $$(tag){return document.createElement(tag);}

function Exists(obj)  {return (obj != null && typeof(obj) != 'undefined');}

var _attachedEvents = new Array();
function AttachEvent(obj, eventName, handler){
	//store attached handlers to detach on page unload
	_attachedEvents.push([obj, eventName, handler]);
	
	try
	{
	    if (obj.attachEvent){
		    //IE
		    obj.attachEvent(eventName, handler);
	    } else {
		    //FF		    
		    obj.addEventListener(eventName.substr(2), handler, false);
	    }
	}
	catch (e)
	{	        	    
	}		
}

function IsClientBrowserIE()
{
    return (top.ClientBrowser == "ie");
}


function IsClientBrowserChrome()
{
    return (is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1);
}

function IsClientBrowserMobile()
{
    return (navigator.userAgent.toLowerCase().indexOf('android') > -1 || navigator.userAgent.toLowerCase().indexOf('iphone') > -1);
}

function SetInnerText(obj, value){
	if (obj.all){(is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1);
		//IE
		obj.innerText = value;
	} else {
		//FF
		if (Exists(value)){
			if (value == " ")
				obj.innerHTML = "&nbsp;";
			else
				obj.innerHTML = value.toString().replace("<", "&lt;").replace(">", "&gt;").replace('"', "&qout;").replace("  ", " &nbsp;");
		} else
			obj.innerHTML = "";
	}
}


/*Belongs to Master but for js file load synchronization placed here*/
var timeoutId = null;

function StartTimer(startTime)
{	    
	var delta = 0;
	var startTicks = startTime - new Date(0).valueOf();
	
	function updateTime()
	{
	    delta += 1000;
	    var o = $("divTimer");
	    if (o)
	    {
		    var d = new Date(startTicks + delta);
		    SetInnerText(o, d.toLocaleString());
	    }
	    timeoutId = setTimeout(updateTime, 1000);
	}
	updateTime();
}

AttachEvent(window, "onunload", function()
{
  if (timeoutId != null) clearTimeout(timeoutId);
});

function Trim(str)
{
    if (str=="")
	    {return str;}

    while (str.indexOf(" ")==0 && str!="")
	    {str = str.slice(1,str.length);}
    while (str.lastIndexOf(" ")==(str.length-1) && str!="")
	    {str = str.slice(0,str.length-1);}	
    return str;		
}

//For easy handling of JS parameter sending. 
//Depends on PolyUtils.FormatParameterForJS()
function ParameterToString(str)
{
    if (str=="")
	    {return str;}

    while (str.indexOf("~~~~")>=0)
	    {str = str.replace("~~~~","\"");}

    while (str.indexOf("####")>=0)
	    {str = str.replace("####","\'");}
	    	    
	return str;	    
}

function TrickRefresh()
{
    var strDate = Date().toString();
    strDate = Replace(strDate, " ", "");
    strDate = Replace(strDate, ":", "");
    strDate = Replace(strDate, "/", "");
    
    return "Trick=" + strDate + "&";
}

function Replace(strFormat, strFind, strReplace)
{
    while (strFormat.indexOf(strFind) >= 0 && strFormat != "")
	    {strFormat = strFormat.replace(strFind, strReplace);}
	return strFormat;
}

function ImageButtonMouseEffect(action, objButton)
{
    if (action == "over")
    {
        objButton.style.margin = "0px";
        objButton.style.borderWidth = "1px";
        objButton.style.borderTopColor = "#D7D6D6";
        objButton.style.borderLeftColor = "#D7D6D6";
        objButton.style.borderBottomColor = "#B5AA73";
        objButton.style.borderRightColor = "#B5AA73";       
    }
    else
    {
        objButton.style.margin = "1px";
        objButton.style.borderWidth = "0px";
    }
}

function f_clientWidth() {
	return f_filterResults (
		window.innerWidth ? window.innerWidth : 0,
		document.documentElement ? document.documentElement.clientWidth : 0,
		document.body ? document.body.clientWidth : 0
	);
}

function f_clientHeight() {
	return f_filterResults (
		window.innerHeight ? window.innerHeight : 0,
		document.documentElement ? document.documentElement.clientHeight : 0,
		document.body ? document.body.clientHeight : 0
	);
}

function f_filterResults(n_win, n_docel, n_body) {
	var n_result = n_win ? n_win : 0;
	if (n_docel && (!n_result || (n_result > n_docel)))
		n_result = n_docel;
	return n_body && (!n_result || (n_result > n_body)) ? n_body : n_result;
}

//Custom validator - Validates input is numeric
function IsNumericValidation(oSrc, args)
{     
    var textboxBigInt = document.getElementById(oSrc.controltovalidate);
    
    if (isNaN(textboxBigInt.value))
    {        
        args.IsValid = false;
    }
    else
    {
        args.IsValid = true;        
    } 
}

function TextareaRequired(oSrc, args)
{
    if (document.getElementById(oSrc.controltovalidate).value == "")
    {
        args.IsValid = false;
    }
    else
    {
        args.IsValid = true;
    }
}

//Note:
//Validator id starts with CustomValidator
//CheckBoxList id starts with CheckBoxList
function CheckBoxListRequired(oSrc, args)
{     
    var validatorId = oSrc.id;
    var checkBoxListId = "CheckBoxList" + validatorId.substring(validatorId.indexOf("CustomValidator")).replace("CustomValidator", "");

    
    for (i=0; i<document.forms[0].length; i++)
    {
        var hasChecked = false;
        if (document.forms[0][i].id.indexOf(checkBoxListId) > -1)
        {
            if (document.forms[0][i].checked)
            {
                hasChecked = true;
                break;
            }
        }
    }
    
    args.IsValid = hasChecked;  
}


//Returns an ajax object
function ajaxObject()
{  
    var xmlHttp;
    try {xmlHttp=new XMLHttpRequest();}    // Firefox, Opera 8.0+, Safari
    catch (e)
        {try  {xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");}  // Internet Explorer
        catch (e)
        {try {xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");}
        catch (e)
        {alert("Your browser does not allow AJAX");return false;}}
    }
    return xmlHttp; 
}

function LoadXMLDocument(fname)
{
    var xmlDoc;
    // code for IE
    if (window.ActiveXObject)
    {
        xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
    }
    // code for Mozilla, Firefox, Opera, etc.
    else if (document.implementation && document.implementation.createDocument)
    {
        xmlDoc = document.implementation.createDocument("","",null);
    }
    else
    {
        return null;
    }
    
    xmlDoc.async=false;
    xmlDoc.load(fname);
    
    return(xmlDoc);
}


//Converts a string to a xml document
function StringToXmlDoc(strXml)
{        
    try //Internet Explorer
    {                
        if (IsClientBrowserIE())
        {
            
            if (parseInt(top.BrowserVersion) < 7)
            {
                xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
                
                xmlDoc.async = "false";
                xmlDoc.loadXML(strXml);                 
            }
            else
            {
                xmlDoc = new ActiveXObject("msxml2.DOMDocument") ;
                xmlDoc.async = "false";
                xmlDoc.loadXML(strXml);                                                                 
            }        
        }
        else    //Not ie
        {
            parser = new DOMParser();
            
            
            xmlDoc = parser.parseFromString(strXml, "text/xml");                   
        }       
    }
    catch(e)
    {
        alert("StringToXmlDoc exception: " + e.message);
        return;        
    }
    
    return xmlDoc;
}


///
//action: Which value to return
//strQuerystring: additional querystring data. No & necessary before or after
///
function AjaxGetValue(action, strQuerystring)
{
    var strReturnValue = "";
    var xmlHttpGetValue = ajaxObject();   
    xmlHttpGetValue.onreadystatechange = function()
        { 
            if(xmlHttpGetValue.readyState == 4)
            {                                                                      		        		        		                                              				                
                strReturnValue = Trim(xmlHttpGetValue.responseText);
                //alert(strReturnValue);
                //document.write(strReturnValue);
                var funcToEval = action + "(strReturnValue)"
                try
                {
                    eval(funcToEval);
                }
                catch (e)
                {
                    //alert(e);
                    alert("Could not eval " + funcToEval);
                }
            }                                                                                                                         		        				                               
        }
    
    xmlHttpGetValue.open("GET","Ajax/GetValue.aspx?action=" + action + "&" + strQuerystring + "&" + TrickRefresh(), true);
    xmlHttpGetValue.send(null);  
}


function DisplayThumb(thumbImg, thumbId, toolTipDirection)
{    
    //var thumbHTML = "<img src='" + thumbImg.src + "' style='width:100%;height:100%;' alt='" + thumbImg.alt + "' title='" + thumbImg.title + "'>";
    var thumbWindow = window.open("", thumbId, "scrollbars=1, location=0, resizable=1, status=0, width=640, height=660", "false");
    thumbWindow.moveTo(0,0);
    
    if (thumbWindow.document.getElementById(thumbId) == null)
    {        
        var newImage = thumbWindow.document.createElement("IMG");
        newImage.id = thumbId;
        newImage.src = thumbImg.src;
        newImage.style.width = "100%";
        newImage.style.height = "480px";
        //newImage.alt = thumbImg.alt;
        //newImage.title = thumbImg.title;
        thumbWindow.document.body.style.direction = toolTipDirection;        
        thumbWindow.document.body.appendChild(newImage);
        
        thumbWindow.document.title = thumbId;
        thumbWindow.title = thumbId;
        
        var newDiv = thumbWindow.document.createElement("DIV");
        newDiv.id = "Div" + thumbId;
        var strThumbData = "<br>" + Replace(thumbImg.title, "\n", "<br>");
        strThumbData += "<br>";
        newDiv.innerHTML = strThumbData;
        newDiv.style.fontFamily = "Arial";
        newDiv.style.fontSize = "12px";
        newDiv.style.fontWeight = "bold";
        newDiv.style.paddingRight = "2px";
        newDiv.style.paddingLeft = "2px";       
        thumbWindow.document.body.appendChild(newDiv);                        
    }
    
    thumbWindow.focus();
}

function GetLastKeyPressedCode(e)
{
    var keyCode = -1;
    
    if (IsClientBrowserIE())
    {
        keyCode = window.event.keyCode;
    }
    else
    {
        keyCode = e.keyCode;
    }
    
    return keyCode;
}

//Aborts submit-form when user clicks "enter" in a textbox
function AbortSubmit(e)
{                   
    var kC = (window.event) ? event.keyCode : e.keyCode;
          
    if (kC == 13)
    {        
        return false;
    }
    else
    {
        return true;
    }
}	

//If users clicks enter button, clicks the object with id buttonIdToClick
function RedirectSubmit(e, buttonIdToClick)
{        
    if (GetLastKeyPressedCode(e) == 13)
    {
        //try
        {                                          
            document.getElementById(buttonIdToClick).click();          
        }
        //catch (e)
        {
        //    alert("RedirectSubmit() exception");
        }
    }
}	

function KeyPressHandler(e) 
{         
    var kC  = (window.event) ?    // MSIE or Firefox?
             event.keyCode : e.keyCode;
    
    var Esc = (window.event) ?   
            27 : e.DOM_VK_ESCAPE // MSIE : Firefox
    
    //alert(kC)
    if (kC == Esc)
    {                                
        top.EscPopupClicked();  
    }
}

//Fiond most top popup and close it
function EscPopupClicked()
{
    //Find popup iframe on top
    var currentWindowPopupOwner = top;
    currentWindowPopupOwner.focus();
    var divPopup = currentWindowPopupOwner.document.getElementById("DivPopup");
    
    
    
    if (divPopup.style.display == "block")
    {//top poup is displayed, start iterating and find if containd more open popups
        var currentWindowHasDisplayedPopup = true;
        
        while (currentWindowHasDisplayedPopup)
        {
            //currentWindowPopupOwner.document.getElementById("DivPopup").style.display == "block")        
            try
            {
                if (currentWindowPopupOwner.frames["PopupIframe"].document.getElementById("DivPopup").style.display == "block")
                {
                    currentWindowPopupOwner = currentWindowPopupOwner.frames["PopupIframe"];                    
                }
                else
                {
                    currentWindowHasDisplayedPopup = false;
                }
            }
            catch(e)
            {
                currentWindowHasDisplayedPopup = false;
            }
        }
        currentWindowPopupOwner.ClosePopup();        
    } 
}

//Actions taken when form is submmited
function OnFormSubmit()
{
    try
    {
        document.getElementById(spanPolytexLabelActionMessageId).style.visibility = "hidden";
    }
    catch(e)
    {}
    
    try
    {
        document.getElementById(spanPolytexLabelActionMessage2Id).style.visibility = "hidden";
    }
    catch(e)
    {}
}




