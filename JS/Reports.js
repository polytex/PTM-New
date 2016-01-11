
//Prepares report for printing:
//Creates multiple tables with report's header and limits all tables by var ReportPageSize 
function PreparePrintingLayout()
{
    //Each page printed has the report's header at top    
    objReportHeader.style.display = "none";
    var strReportHeader = objReportHeader.innerHTML;
    
    //The full report as sent by server and loaded on the hidden page (iframe)    
    objTableGridView.style.display = "none";
    
    //Container div for rendered report pages                           
    var PagesToPrintContainer = document.getElementById("PagesToPrintContainer");
    
    var pageCount = 1;
    
    //Always add a report header at top
    AddReportHeader(pageCount, false, PagesToPrintContainer, strReportHeader);
    
    var PageTable;      //a page table object
    var tBodyTable;     //table's body
    
    PageTable = CreatePageTable(objTableGridView.tHead.cloneNode(true));           
    tBodyTable = document.createElement("TBODY");
    PageTable.appendChild(tBodyTable);
    
    try
    {
    //Iterate through full report and add rows to page tables    
    for (i=1; i<=objTableGridView.rows.length-1; i++)
    {
        //Add current iterated row to current page table
        tBodyTable.appendChild(objTableGridView.rows[i].cloneNode(true));
        
        if (i == objTableGridView.rows.length-1)
        {
            try
            {
                tBodyTable.appendChild(objTableGridView.rows[i+1].cloneNode(true));
            }
            catch(e)
            {            
            }
        }
        
        if ((i % ReportPageSize) == 0 || i == (objTableGridView.rows.length-1))
        {//Reached max rows for page table - or last report row
            PagesToPrintContainer.appendChild(PageTable);
            
            if (i < (objTableGridView.rows.length-2))
            {
                pageCount++;
                AddReportHeader(pageCount, true, PagesToPrintContainer, strReportHeader);
                
                //Reset page table
                PageTable = CreatePageTable(objTableGridView.tHead.cloneNode(true));                                                
                tBodyTable = document.createElement("TBODY");
                PageTable.appendChild(tBodyTable);
            }
        }
    }
    }
    catch(e)
    {
        //alert(e);
    }
    window.print();
}

//Adds another report header to container
function AddReportHeader(pageCounter, breakPageBefore, objContainer, strHtml)
{
    var divHeader = document.createElement("DIV");
    
    strHtml = strHtml.replace("ReportCurrentPage.Text", pageCounter);
    
    divHeader.innerHTML = strHtml;
    
    if (breakPageBefore)
    {
        divHeader.style.pageBreakBefore = "always"; 
    }
    
    objContainer.appendChild(divHeader);    
}

//Creates a page's table with data and all its html attributes
function CreatePageTable(tableHead)
{
    var objTable = document.createElement("TABLE");
    objTable.className = "PanelBorder PanelBG GridView";
    objTable.cellPadding = 0;
    objTable.cellSpacing = 0;
    objTable.style.width = "100%";
    
    objTable.appendChild(tableHead);    
            
    return objTable;
}



