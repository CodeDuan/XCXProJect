using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Model.DB;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using MySql.Data.MySqlClient;
namespace Dal
{
    public class DbBase
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static readonly string connstr=AppConfigurtaionServices.Configuration["ConnectionStrings:DefaultConnection"]; 
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetList<T>()
        {
            return new List<T>();
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">sql命令</param>
        /// <param name="Parameters">参数</param>
        /// <returns></returns>
        public static T GetDetail<T>(string command, DynamicParameters Parameters)
        {
            using (IDbConnection conn = new MySqlConnection(connstr))
            {
                T result = conn.QuerySingleOrDefault<T>(command, Parameters);
                return result;
            }
        }
    }
    public class AppConfigurtaionServices
    {
        public static IConfiguration Configuration { get; set; }
        static AppConfigurtaionServices()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
        }
    }
}
