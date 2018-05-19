using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Core.Infrastructure.Database.Repository
{
    public class BaseDB
    {
        string connectionString => @"Server=localhost\SQLEXPRESS;Initial Catalog=PesquisaSatisfacao;Integrated Security=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            using (var con = GetConnection())
            {
                return  await con.QueryAsync<T>(sql, param);
            }
        }

        public async Task ExecuteAsync(string sql, object param)
        {
            using (var con = GetConnection())
            {
                await con.ExecuteAsync(sql, param);
            }
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null)
        {
            using (var con = GetConnection())
            {
                return await con.QueryAsync<dynamic>(sql, param);
            }
        }
    }
}
