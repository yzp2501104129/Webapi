using WebApi.Entity.Models;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Repository.Base.SqlSugar
{
    /// <summary>
    /// SqlSugar 默认上下文
    /// </summary>
    public class DefaultContext<T> where T : class, new()
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static readonly string _DbString = "Data Source=.;Initial Catalog=WebApiSql;Persist Security Info=True;User ID=sa;Password=svse1234.; min pool size=1; max pool size=50;";// ConfigurationManager.ConnectionStrings["Access"].ToString();

        public DefaultContext()
        {
            List<ConnectionConfig> ConfigList = new List<ConnectionConfig>()
            {
                new ConnectionConfig()
                {
                    ConfigId= "0",   
                    DbType = DbType.SqlServer,
                    InitKeyType = InitKeyType.Attribute,
                    ConnectionString = _DbString,
                    IsAutoCloseConnection = true,
                }
            };
            Db = new SqlSugarClient(ConfigList);
            //异常Sql拦截
            Db.Aop.OnError = ((exp) =>
            {
                string Sql = exp.Sql;
                string Parametres = JsonConvert.SerializeObject(exp.Parametres);
                string ExpMessage = exp.Message;
                //Log4写入错误sql日志
                


            });
        }
        /// <summary>
        /// 用来处理事务多表查询和复杂的操作【不能写成静态】
        /// </summary>
        public SqlSugarClient Db;
        /// <summary>
        /// 处理常规的业务操作
        /// </summary>
        public SimpleClient<T> DbContext { get { return new SimpleClient<T>(Db); } }
    }
}
