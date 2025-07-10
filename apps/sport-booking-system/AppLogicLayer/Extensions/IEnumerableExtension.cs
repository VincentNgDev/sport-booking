using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
        {
            while (source.Any())
            {
                yield return source.Take(chunkSize);
                source = source.Skip(chunkSize);
            }
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> source)
        {
            DataTable dt = new DataTable();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Add Columns to data table
            foreach (var prop in properties)
                dt.Columns.Add(prop.Name, prop.PropertyType);

            // Add rows to data table
            foreach (var data in source)
            {
                var dr = dt.NewRow();
                foreach (var prop in properties)
                    dr[prop.Name] = prop.GetValue(data, null);

                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
