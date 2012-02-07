using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Kevin.Infrastructure.Querying.SQL
{
    internal static class FilterDescriptionSqlDBSQLBuilder
    {
        public static void Write(this FilterDescriptor filter, SqlDBSQLBuildContext sqlDBSQLBuildContext)
        {
            switch (filter.Operator)
            {
                case FilterOperator.IsLessThan:
                    {
                        WriteIsLessThan(filter, sqlDBSQLBuildContext);
                        break;
                    }
                case FilterOperator.IsLessThanOrEqualTo:
                    {
                        WriteIsLessThanOrEqualTo(filter, sqlDBSQLBuildContext);
                        break;
                    }
                case FilterOperator.IsEqualTo:
                    {
                        WriteIsEqualTo(filter, sqlDBSQLBuildContext);
                        break;
                    }
                case FilterOperator.IsNotEqualTo:
                    {
                        WriteIsNotEqualTo(filter, sqlDBSQLBuildContext);
                        break;
                    }
                case FilterOperator.IsGreaterThanOrEqualTo:
                    {
                        WriteIsGreaterThanOrEqualTo(filter, sqlDBSQLBuildContext);
                        break;
                    }
                case FilterOperator.IsGreaterThan:
                    {
                        WriteIsGreaterThan(filter, sqlDBSQLBuildContext);
                        break;
                    }
                case FilterOperator.StartsWith:
                    {
                        WriteStartsWith(filter, sqlDBSQLBuildContext);
                        break;
                    }
                case FilterOperator.EndsWith:
                    {
                        WriteEndsWith(filter, sqlDBSQLBuildContext);
                        break;
                    }
                case FilterOperator.Contains:
                    {
                        WriteContains(filter, sqlDBSQLBuildContext);
                        break;
                    }
                case FilterOperator.IsContainedIn:
                    {
                        WriteIsContainedIn(filter, sqlDBSQLBuildContext);
                        break;
                    }
                default:
                    {
                        throw new InvalidOperationException();
                    }
            }
        }

        private static void WriteIsLessThan(FilterDescriptor filter, SqlDBSQLBuildContext sqlDBSQLBuildContext)
        {
            NullValid(filter.Value);
            Write(sqlDBSQLBuildContext, filter.Member, "<", filter.Value);
        }

        private static void WriteIsLessThanOrEqualTo(FilterDescriptor filter, SqlDBSQLBuildContext sqlDBSQLBuildContext)
        {
            NullValid(filter.Value);
            Write(sqlDBSQLBuildContext, filter.Member, "<=", filter.Value);
        }

        private static void WriteIsEqualTo(FilterDescriptor filter, SqlDBSQLBuildContext sqlDBSQLBuildContext)
        {
            if (filter.Value == null)
            {
                Write(sqlDBSQLBuildContext, filter.Member, "Is", "Null");
            }
            else
            {
                Write(sqlDBSQLBuildContext, filter.Member, "=", filter.Value);
            }
        }

        private static void WriteIsNotEqualTo(FilterDescriptor filter, SqlDBSQLBuildContext sqlDBSQLBuildContext)
        {
            if (filter.Value == null)
            {
                Write(sqlDBSQLBuildContext, filter.Member, "Is Not", "Null");
            }
            else
            {
                Write(sqlDBSQLBuildContext, filter.Member, "!=", filter.Value);
            }
        }

        private static void WriteIsGreaterThanOrEqualTo(FilterDescriptor filter, SqlDBSQLBuildContext sqlDBSQLBuildContext)
        {
            NullValid(filter.Value);
            Write(sqlDBSQLBuildContext, filter.Member, ">=", filter.Value);
        }

        private static void WriteIsGreaterThan(FilterDescriptor filter, SqlDBSQLBuildContext sqlDBSQLBuildContext)
        {
            NullValid(filter.Value);
            Write(sqlDBSQLBuildContext, filter.Member, ">", filter.Value);
        }

        private static void WriteStartsWith(FilterDescriptor filter, SqlDBSQLBuildContext sqlDBSQLBuildContext)
        {
            WirteLikeSQL(filter, sqlDBSQLBuildContext, true, false);
        }

        private static void WriteEndsWith(FilterDescriptor filter, SqlDBSQLBuildContext sqlDBSQLBuildContext)
        {
            WirteLikeSQL(filter, sqlDBSQLBuildContext, false, true);
        }

        private static void WriteContains(FilterDescriptor filter, SqlDBSQLBuildContext sqlDBSQLBuildContext)
        {
            WirteLikeSQL(filter, sqlDBSQLBuildContext, true, true);
        }

        private static void WirteLikeSQL(FilterDescriptor filter, SqlDBSQLBuildContext sqlDBSQLBuildContext, bool appendParameterPrefix, bool appendParamterSuffix)
        {
            NullValid(filter.Value);
            if (!(filter.Value is string))
            {
                throw new ArgumentException("操作的参数必须为字符串");
            }
            object value = filter.Value;
            if (appendParameterPrefix)
            {
                value = "%" + value;
            }
            if (appendParamterSuffix)
            {
                value = value + "%";
            }
            Write(sqlDBSQLBuildContext, filter.Member, "Like", value);
        }

        private static void WriteIsContainedIn(FilterDescriptor filter, SqlDBSQLBuildContext sqlDBSQLBuildContext)
        {
        }

        private static void Write(SqlDBSQLBuildContext sqlDBSQLBuildContext, string member, string dbOperator, object value)
        {
            string parameterName = sqlDBSQLBuildContext.AddDBParameter(value);
            Write(sqlDBSQLBuildContext.Writer, sqlDBSQLBuildContext.TableName, member, dbOperator, parameterName);
        }

        private static void Write(StringWriter writer, string tableName, string columnName, string dbOperator, string parameterName)
        {
            Write(writer, tableName, columnName);
            WriteSpace(writer);
            writer.Write(dbOperator);
            WriteSpace(writer);
            writer.Write(parameterName);
            WriteSpace(writer);
        }

        private static void Write(StringWriter writer, string tableName, string columnName)
        {
            if (!string.IsNullOrEmpty(tableName))
            {
                writer.Write(tableName);
                writer.Write(".");
            }
            writer.Write(columnName);
        }

        private static void WriteSpace(StringWriter writer)
        {
            writer.Write(" ");
        }

        private static void NullValid(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
        }
    }
}
