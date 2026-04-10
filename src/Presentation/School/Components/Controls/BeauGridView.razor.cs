using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Data;

namespace School.Components.Controls
{
    public partial class BeauGridView
    {
        [Parameter] public DataTable DataTable { get; set; } = new();
        [Parameter] public Action<DataRowView>? OnClickRow { get; set; }
        [Parameter] public Dictionary<string, Column>? ColumnOptions { get; set; }
        [Parameter] public bool Editable { get; set; } = false;
        [Parameter] public string? Name { get; set; }
        private List<DataRowView>? ChangedRows { get; set; } = new();
        private DataRowView? SelectedRow { get; set; }
        private int ColumnVisibleCount;
        public class Column
        {
            public string Caption { get; set; } = "";
            public bool Visible { get; set; } = true;
            //public bool ReadOnly { get; set; } = true;
            // public Type DataType { get; set; }
        }

        protected override void OnParametersSet()
        {
            for (int i = 0; i < ColumnOptions?.Count; i++)
            {
                string name = DataTable.Columns[i].ColumnName;
                Column clm = ColumnOptions[name];

                if (!string.IsNullOrEmpty(clm.Caption))
                    DataTable.Columns[name].Caption = clm.Caption;

                // if (clm.DataType != null)
                //     DataTable.Columns[name].DataType = clm.DataType;
            }
            DataTable.DefaultView.RowFilter = filterCollection;
            base.OnParametersSet();
        }
        protected override Task OnInitializedAsync()
        {
            // if (Records?.Count > 0)
            // {
            //     foreach (var item in typeof(T).GetProperties())
            //     {
            //         // var propertyInfo = item.GetType().GetProperty(item.Name);
            //         DataTable.Columns.Add(item.Name, typeof(string));
            //     }
            //     for (int i = 0; i < DataTable.DefaultView.Count; i++)
            //     {
            //         for (int r = 0; r < DataTable.Columns.Count; r++)
            //         {
            //             DataTable.Rows.Add()
            //         }
            //     }
            // }

            if (ColumnOptions?.Count > 0)
                ColumnVisibleCount = ColumnOptions.Select(s => s.Value).Where(w => w.Visible == true).Count();
            else
                ColumnVisibleCount = DataTable.Columns.Count;
            return base.OnInitializedAsync();
        }
        private void SelectRow(DataRowView dataRow)
        {
            if (SelectedRow != dataRow)
            {
                SelectedRow = dataRow;
                OnClickRow?.Invoke(dataRow);
            }
        }

        private void OnChangeRow(string value, DataRowView dataRow, int row, int column)
        {
            DataTable.DefaultView[row][column] = value;
            //if (SelectedRow != dataRow)
            //{
            //    SelectedRow = dataRow;
            //    //OnClickRow?.Invoke(dataRow);
            //}
        }

        private string GetRowClass(DataRowView dataRow)
        {
            string? a = SelectedRow?.Row.ItemArray.ToArray().FirstOrDefault()?.ToString();
            string? b = dataRow?.Row.ItemArray.ToArray().FirstOrDefault()?.ToString();
            return a == b ? "table-primary" : string.Empty;
        }

        Dictionary<string, string> filters = new Dictionary<string, string>();
        string filterCollection = "";
        private void Filter(string sender)
        {

            if (DataTable == null || DataTable.Rows.Count == 0)
                return;

            string filterExpress = "";
            filterCollection = "";

            if (filters.Count == 0)
                filters.Add("+DefaultColumn+", "1=1 ");

            string[] strFilter = sender.Split('-');
            string text = strFilter[0].Trim();
            string columnName = strFilter[1];
            if (!string.IsNullOrEmpty(text))
            {
                string[] textStr = text.Split(' ');
                foreach (string txt in textStr)
                {
                    filterExpress += $" AND (CONVERT({columnName}, System.String) LIKE '%{txt}%' OR  CONVERT({columnName}, System.String) LIKE '%{txt.Replace("ی", "ي").Replace("ک", "ك")}%' OR CONVERT({columnName}, System.String) LIKE '%{txt.Replace("ي", "ی").Replace("ك", "ک")}%'" +
                                     $" OR CONVERT({columnName}, System.String) LIKE '%{txt.Replace("ي", "ی").Replace("ک", "ك")}%' OR CONVERT({columnName}, System.String) LIKE '%{txt.Replace("ي", "ی").Replace("ک", "ك")}%')";
                }
                if (!filters.ContainsKey(columnName))
                    filters.Add(columnName, filterExpress);
                else
                    filters[columnName] = filterExpress;
            }
            else
            {
                if (columnName != null)
                    filters.Remove(columnName);
            }
            foreach (string txt in filters.Values)
            {
                filterCollection += txt;
            }
            DataTable.DefaultView.RowFilter = filterCollection;
        }

        private async Task KeyDownHandler(KeyboardEventArgs e)
        {
            if (!e.ShiftKey && (e.Key.ToLowerInvariant() == "enter" || e.Key.ToLowerInvariant() == "arrowleft"))
            {
                await _js.InvokeVoidAsync("invokeTabKey", 1);
            }
            else if (!e.ShiftKey && e.Key.ToLowerInvariant() == "arrowup")
            {
                await _js.InvokeVoidAsync("invokeTabKey", -(ColumnVisibleCount));
            }
            else if (!e.ShiftKey && e.Key.ToLowerInvariant() == "arrowdown")
            {
                await _js.InvokeVoidAsync("invokeTabKey", ColumnVisibleCount);
            }
            else if (!e.ShiftKey && e.Key.ToLowerInvariant() == "arrowright")
            {
                await _js.InvokeVoidAsync("invokeTabKey", -1);
            }
        }

        private void GroupBy(string columnName)
        {
        //    if (dtPersonnel.DefaultView.Count > 0 && this.cmbType.Items.Count == 0)
        //    {
        //        var groupedData = this.dtPersonnel.AsEnumerable()
        //            .GroupBy(row => row[columnName])
        //            .Select(group => group.Key)
        //            .Where(t => t != DBNull.Value)
        //            .Select(t => t.ToString())
        //            .OrderBy(t =>
        //            {
        //                if (decimal.TryParse(t, out decimal numericValue))
        //                    return numericValue;
        //                else
        //                    return decimal.MaxValue;
        //            })
        //            .ToList();
        //        this.cmbType.Items.Clear();
        //        for (int i = 0; i < groupedData.Count; i++)
        //        {
        //            this.cmbType.Items.Add(groupedData[i].ToString());
        //        }
        //        this.cmbType.Items.Add("همه موارد");
        //        this.cmbType.Text = "همه موارد";
        //    }
        }
    }
}
