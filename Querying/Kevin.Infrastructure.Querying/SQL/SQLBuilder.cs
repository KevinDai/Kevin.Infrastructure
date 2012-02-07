using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Kevin.Infrastructure.Querying.SQL
{
    public static class SQLBuilder
    {
        public static string TSQLBuild(string tableName, IEnumerable<IFilterDescriptor> filters, ref IDataParameterCollection parameters)
        {
            if (filters.Any())
            {
                StringWriter writer = new StringWriter();
                SqlDBSQLBuildContext context = new SqlDBSQLBuildContext(tableName, writer, parameters);
                CompositeFilterDescriptor cfd = new CompositeFilterDescriptor();
                foreach (var item in filters)
                {
                    cfd.FilterDescriptors.Add(item);
                }
                cfd.Write(context);
                return writer.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
