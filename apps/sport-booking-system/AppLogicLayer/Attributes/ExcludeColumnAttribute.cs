using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExcludeColumnAttribute : Attribute
    {
        private static readonly string[] ColumnsCannotExclude = 
        { 
            nameof(SchemaBase.ObjectId),
            nameof(SchemaBase.IsDeleted),
            nameof(SchemaBase.Version),
        };
        public List<string> ColumnNames { get; }
        public ExcludeColumnAttribute(params string[] columns)
        {
            if (columns.Any(c => ColumnsCannotExclude.Contains(c)))
                throw new ArgumentException("ObjectId, IsDeleted and Version cannot be excluded.");

            ColumnNames = new List<string>();
            foreach (string c in columns)
            {
                if (c != null && c.Trim().Length > 0)
                    ColumnNames.Add(c);
            }
        }
    }
}
