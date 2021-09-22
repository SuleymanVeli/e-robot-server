using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SqlConnection = System.Data.SqlClient.SqlConnection;

namespace e_robot.Infrastructure.Repositories.Dapper
{
    public class DapperClass : IDapper
    {
        private readonly IConfiguration _config;
        private readonly string Connectionstring = "Configurations:ConnectionString";

        public DapperClass(IConfiguration config)
        {
            _config = config;
        }
        public void Dispose()
        {

        }

        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            SqlMapper.SetTypeMap(
                typeof(T),
                new ColumnAttributeTypeMapper<T>());
            using IDbConnection db = new System.Data.SqlClient.SqlConnection(_config[Connectionstring]);
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            SqlMapper.SetTypeMap(
                 typeof(T),
                 new ColumnAttributeTypeMapper<T>());

            using IDbConnection db = new SqlConnection(_config[Connectionstring]);
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        public async Task<List<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            SqlMapper.SetTypeMap(
                 typeof(T),
                 new ColumnAttributeTypeMapper<T>());

            using IDbConnection db = new SqlConnection(_config[Connectionstring]);
            return (await db.QueryAsync<T>(sp, parms, commandType: commandType)).ToList();
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_config[Connectionstring]);
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            T result;

            using IDbConnection db = new SqlConnection(_config[Connectionstring]);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            T result;
            using IDbConnection db = new SqlConnection(_config[Connectionstring]);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }
    }
}
