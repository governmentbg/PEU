using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using static Dapper.SqlMapper;

namespace EAU.Utilities
{
    public class DataContextHelper
    {
        public static CustomPropertyTypeMap ColumnMap<T>(Dictionary<string, string> columnMappings = null)
        {
            return new CustomPropertyTypeMap(typeof(T),
                                    (type, columnName) => type.GetProperties().FirstOrDefault(
                                        prop => string.Compare(GetMappedColumnName(prop, columnMappings), columnName, true) == 0));
        }

        public static ICustomQueryParameter CreateTableValuedParameter<T>(IEnumerable<T> data, string dtColName, string dbTypeName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(dtColName, typeof(T));

            if (data != null)
            {
                foreach (T item in data)
                {
                    dt.Rows.Add(item);
                }
            }

            return dt.AsTableValuedParameter(dbTypeName);
        }

        private static string GetMappedColumnName(MemberInfo member, Dictionary<string, string> columnMappings)
        {
            if (member == null)
                return null;

            var attrib = (DapperColumnAttribute)Attribute.GetCustomAttribute(member, typeof(DapperColumnAttribute), false);

            if (attrib != null)
                return attrib.Name;

            string mappedPropName;
            if (columnMappings?.Any() == true
                && columnMappings.TryGetValue(member.Name, out mappedPropName))
                return mappedPropName;

            return null;
        }
    }
}
