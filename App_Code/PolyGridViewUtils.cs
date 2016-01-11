using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for PolyGridViewUtils
/// </summary>
public class PolyGridViewUtils
{
	public PolyGridViewUtils()
	{

	}

    public static void GridViewOnInit(GridView gridView)
    {
        gridView.RowEditing += new GridViewEditEventHandler(GridViewOnRowEditing);
        gridView.RowCancelingEdit += new GridViewCancelEditEventHandler(GridViewOnRowCancelingEdit);
        gridView.PreRender += new EventHandler(GridViewOnPreRender);

        gridView.ShowFooter = true;
        gridView.ShowHeader = true;
        gridView.AutoGenerateColumns = false;

        if (gridView.PageSize == 10)
        {
            gridView.PageSize = UniStr.Settings.RecordsOnPage;
        }

        if (PolyReport.IsPrintingLayout || PolyReport.IsForMailingList)
        {
            gridView.EnableViewState = false;    
            gridView.AllowPaging = false;
            gridView.DataBind();            
        }
        //gridView.RowHeaderColumn = "ID"; 
    }

    /// <summary>
    /// Removes selected row from grid view
    /// </summary>
    public static void GridViewOnRowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridView = (GridView)sender;

        ClearSetRows(gridView, true, false);
    }

    /// <summary>
    /// Sets selected row for the row whose editing has been cancelled
    /// </summary>
    public static void GridViewOnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridView = (GridView)sender;
        gridView.SelectedIndex = e.RowIndex;
    }


    /// <summary>
    /// Removes empty row if necessary
    /// </summary>
    /// <param name="e"></param>
    public static void GridViewOnPreRender(object sender, EventArgs e)
    {
        GridView gridView = (GridView)sender;

        gridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        //foreach (GridViewRow row in gridView.Rows) row.TableSection = TableRowSection.TableBody;
        //gridView.FooterRow.TableSection = TableRowSection.TableFooter;

        try
        {
            if (((PolytexControls.ObjectDataSource)(gridView.Parent.FindControl(gridView.DataSourceID))).IsEmpty)
            {
                gridView.Rows[0].Visible = false;
            }
        }
        catch
        {
            
            if (((DataTable)(gridView.DataSource)).Rows.Count == 0)
            {
                gridView.Rows[0].Visible = false;
            }
        }

        if (((PolyPage)gridView.Page).CurrentPageType == PolyPage.PageType.Report)
        {
            gridView.FooterStyle.CssClass = "ReportGridFooter PanelHighlightedBG";
        }     
    }


    /// <summary>
    /// Displays all rows with item template (not selected or edited)
    /// </summary>
    /// <param name="selectedRow"></param>
    /// <param name="editedRow"></param>
    public static void ClearSetRows(GridView gridView, bool selectedRow, bool editedRow)
    {
        if (selectedRow)
        {
            gridView.SelectedIndex = -1;
        }
        if (editedRow)
        {
            gridView.EditIndex = -1;
        }
    }

    /// <summary>
    /// Reloads the data source and sets selected the row with dataKeyValue (such as table's "ID")
    /// </summary>
    /// <param name="dataKeyValue"></param>
    public static void SetSelectedRow(GridView gridView, object dataKeyValue)
    {
        DataTable dataTableView = ((DataView)(((PolytexControls.ObjectDataSource)(gridView.Parent.FindControl(gridView.DataSourceID))).Select())).Table;

        string strSort = "";
        if (!String.IsNullOrEmpty(gridView.SortExpression))
        {
            strSort = gridView.SortExpression;
            switch (gridView.SortDirection)
            {
                case SortDirection.Ascending:
                    strSort += " ASC";
                    break;
                case SortDirection.Descending:
                    strSort += " DESC";
                    break;
            }
        }

        if (strSort != "")
        {
            dataTableView.DefaultView.Sort = strSort;
            dataTableView = dataTableView.DefaultView.ToTable();
        }

        int dataRowIndex = -1;
        for (int i = 0; i < dataTableView.Rows.Count; i++)
        {
            if (dataTableView.Rows[i][gridView.DataKeyNames[0]].ToString() == dataKeyValue.ToString())
            {
                dataRowIndex = i;
                break;
            }
        }

        if (dataRowIndex > -1)
        {
            gridView.EditIndex = -1;
            gridView.PageIndex = (dataRowIndex / gridView.PageSize);
            gridView.SelectedIndex = (dataRowIndex % gridView.PageSize);
        }

        gridView.DataBind();
    }

    public static string EmptyDataTemplate()
    {
        return @"<div align=""center"">" + PolyUtils._T["NoRecordsFound"] + "</div>";
    }    
}
