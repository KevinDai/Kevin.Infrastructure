using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kevin.Infrastructure.Querying.SQL
{
    internal static class FilterDescriptorCollectionSqlDBSQLBuilder
    {
        public static void Write(this CompositeFilterDescriptor filter, SqlDBSQLBuildContext sqlDBSQLBuildContext)
        {
            if (filter.FilterDescriptors.Any())
            {
                IFilterDescriptor temp = null;
                for (int i = 0; i < filter.FilterDescriptors.Count; i++)
                {
                    temp = filter.FilterDescriptors[i];
                    if (temp is FilterDescriptor)
                    {
                        (temp as FilterDescriptor).Write(sqlDBSQLBuildContext);
                    }
                    else if (temp is CompositeFilterDescriptor)
                    {
                        sqlDBSQLBuildContext.Writer.Write("(");
                        (temp as CompositeFilterDescriptor).Write(sqlDBSQLBuildContext);
                        sqlDBSQLBuildContext.Writer.Write(")");
                    }
                    if (i < filter.FilterDescriptors.Count - 1)
                    {
                        sqlDBSQLBuildContext.Writer.Write(filter.LogicalOperator.ToString());
                        sqlDBSQLBuildContext.Writer.Write(" ");
                    }
                }
            }
        }

    }
}
