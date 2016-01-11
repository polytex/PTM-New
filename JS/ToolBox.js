
function ToolboxMouseAction(action, toolBoxItem)
{
    if (action == "over")
    {
        toolBoxItem.className = "ToolBoxButtonOver";
        toolBoxItem.style.border = "solid 1px #FF8B00";        
    }
    else
    {
        toolBoxItem.className = "ToolBoxButtonOut";
        toolBoxItem.style.border = "solid 0px #FF8B00"; 
    }
}


function PrintPage()
{
    window.print();
}

function AddActivity()
   {
      document.location = "ActivityDetails.aspx?activityId=0";
   }
  

var displayRenderedToPrint = false;       //For dev - if true allows to view rendered page for printing (does not submit to hidden iFrame)

function PrintReport()
{    
    var objHiddenInput = document.getElementById("PrintingLayout");
    
    objHiddenInput.value = "1";     //Flag to know if printing layout for page
    
    var formPrint = document.createElement("FORM");
    formPrint.action = document.forms[0].action;
    formPrint.method = "POST";
    
    if (IsClientBrowserIE())
    {
        formPrint.target = "PrintFrame";        
    }
    else
    {
        var iframePrint = document.createElement("IFRAME");
        iframePrint.id = "PrintFrameCreated";
        iframePrint.name = "PrintFrameCreated";
        iframePrint.style.visibility = "hidden";
        document.body.appendChild(iframePrint);        
        formPrint.target = "PrintFrameCreated";
    }
    
    if (displayRenderedToPrint)     
    {        
        formPrint.target = "_blank";
    }
    
    formPrint.innerHTML = document.forms[0].innerHTML;
    document.body.appendChild(formPrint);
    formPrint.submit();
    document.body.removeChild(formPrint);
    
    objHiddenInput.value = "0";    
}

function ExportExcel()
{                            
    var formExport = document.createElement("FORM");
    formExport.action = SiteURL + "ExportExcel/ExportMaster.aspx";
    formExport.method = "POST";
    
    var spanHiddens = document.getElementById("spanHiddenFieldsForExport");
    formExport.innerHTML = spanHiddens.innerHTML;
    document.body.appendChild(formExport);
    formExport.submit();
    document.body.removeChild(formExport);     
}

function ExportFavorites()
{
    OpenPopup("FavoritesExportPrompt.aspx?", 450, 250, false);   
}


function ImportExcel(importPage)
{
    OpenPopup(importPage + "Import.aspx?", 740, 480, false);    
}

function ImportFavorites(importPage)
{
    OpenPopup("FavoritesImport.aspx?", 740, 480, false);    
}


