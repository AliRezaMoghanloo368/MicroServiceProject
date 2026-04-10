using System.Data;
using System.Reflection;

namespace SharedLibrary.Utilities
{
    public static class ListExtensions
    {
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            // گرفتن تمام property های عمومی کلاس T
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                // اضافه کردن ستون‌ها بر اساس نوع property
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            if (items?.Count > 0)
            {
                foreach (var item in items)
                {
                    var values = new object[props.Length];
                    for (int i = 0; i < props.Length; i++)
                    {
                        values[i] = props[i].GetValue(item, null) ?? DBNull.Value;
                    }
                    dataTable.Rows.Add(values);
                }
            }

            return dataTable;
        }

        public static DataTable ToDataTableDictionary(this List<Dictionary<string, object>> items)
        {
            DataTable table = new DataTable();

            if (items == null || items.Count == 0)
                return table;

            // همه‌ی کلیدهای یکتا از همه دیکشنری‌ها (تا اگر یکی نداشت هم ستون ساخته بشه)
            var allKeys = items.SelectMany(d => d.Keys).Distinct().ToList();

            foreach (var key in allKeys)
            {
                table.Columns.Add(key);
            }

            // اضافه کردن ردیف‌ها
            foreach (var dict in items)
            {
                var row = table.NewRow();

                foreach (var key in allKeys)
                {
                    // اگر دیکشنری این کلید رو نداره، DBNull بذار
                    if (dict.ContainsKey(key))
                        row[key] = dict[key] ?? DBNull.Value;
                    else
                        row[key] = DBNull.Value;

                    if (row[key].ToString() == "{}")
                        row[key] = null;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable ToDataTableViewModel<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            // گرفتن تمام property های عمومی کلاس T
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                // اضافه کردن ستون‌ها بر اساس نوع property
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            if (items?.Count > 0)
            {
                foreach (var item in items)
                {
                    var values = new object[props.Length];
                    for (int i = 0; i < props.Length; i++)
                    {
                        values[i] = props[i].GetValue(item, null) ?? DBNull.Value;
                    }
                    dataTable.Rows.Add(values);
                }
            }

            return dataTable;
        }
    }
}
