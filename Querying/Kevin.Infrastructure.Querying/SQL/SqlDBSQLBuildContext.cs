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
    internal sealed class SqlDBSQLBuildContext
    {

        /// <summary>
        /// 数据库参数名称前缀
        /// </summary>
        private const string ParameterNamePrefix = "@p";

        /// <summary>
        /// .NET类型到SQL数据库映射的
        /// </summary>
        private static readonly Dictionary<Type, SqlDbType> TypeMapper = new Dictionary<Type, SqlDbType>();

        /// <summary>
        /// 数据库参数列表
        /// </summary>
        protected IDataParameterCollection Parameters
        {
            get;
            private set;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get;
            private set;
        }

        public StringWriter Writer
        {
            get;
            private set;
        }

        public SqlDBSQLBuildContext(string tableName, StringWriter writer, IDataParameterCollection parameters)
        {
            this.TableName = tableName;
            this.Parameters = parameters;
            this.Writer = writer;
        }

        /// <summary>
        /// 初始化.NET类型到数据库类型的映射
        /// </summary>
        static SqlDBSQLBuildContext()
        {
            TypeMapper.Add(typeof(bool), SqlDbType.Bit);
            TypeMapper.Add(typeof(byte), SqlDbType.TinyInt);
            TypeMapper.Add(typeof(DateTime), SqlDbType.DateTime);
            TypeMapper.Add(typeof(DateTimeOffset), SqlDbType.DateTimeOffset);
            TypeMapper.Add(typeof(Decimal), SqlDbType.Decimal);
            TypeMapper.Add(typeof(double), SqlDbType.Float);
            TypeMapper.Add(typeof(Single), SqlDbType.Real);
            TypeMapper.Add(typeof(Guid), SqlDbType.UniqueIdentifier);
            TypeMapper.Add(typeof(Int16), SqlDbType.SmallInt);
            TypeMapper.Add(typeof(Int32), SqlDbType.Int);
            TypeMapper.Add(typeof(Int64), SqlDbType.BigInt);
            TypeMapper.Add(typeof(string), SqlDbType.NVarChar);
            TypeMapper.Add(typeof(TimeSpan), SqlDbType.Time);
        }

        /// <summary>
        /// 根据过滤参数添加数据库DBParameter对象并返回生成的数据库参数名称
        /// </summary>
        /// <param name="value">参数值</param>
        /// <returns>生成的数据库参数名称</returns>
        public string AddDBParameter(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            SqlParameter param = new SqlParameter();
            param.SqlDbType = SQLDBTypeGet(value.GetType());
            param.Value = value;
            param.ParameterName = GenerateDBParameterName();
            Parameters.Add(param);

            return param.ParameterName;
        }

        /// <summary>
        /// 根据参数类型获取相应的数据库参数类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private SqlDbType SQLDBTypeGet(Type type)
        {
            if (TypeMapper.ContainsKey(type))
            {
                return TypeMapper[type];
            }
            else
            {
                throw new Exception("SQL不支持参数类型" + type.FullName);
            }
        }

        /// <summary>
        /// 生成数据库参数名称
        /// </summary>
        /// <returns></returns>
        private string GenerateDBParameterName()
        {
            if (Parameters == null)
            {
                throw new Exception("Parameters未初始化");
            }
            return ParameterNamePrefix + Parameters.Count.ToString();
        }
    }
}
