using MCCS.Interfaces;
using MCCS.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace MCCS.DataServices
{
    public class SqlDataService : IDataService
    {
        private readonly string _connectionString;
        private bool _isTransactionBegin = false;
        private bool _isTransactionCommitted = false;
        private readonly SqlConnection _sqlConnection;
        private SqlTransaction _sqlTransaction;
        private SqlDataReader _sqlDataReader;

        public SqlDataService(string connectionString)
        {
            _connectionString = connectionString;
            _sqlConnection = new SqlConnection
            {
                ConnectionString = _connectionString
            };
            _sqlConnection.Open();
        }

        public ConnectionInfo GetConnectionInfo()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_connectionString);
            ConnectionInfo connectionInfo = new ConnectionInfo
            {
                DatabaseName = builder.InitialCatalog,
                ServerName = builder.DataSource,
                UserID = builder.UserID,
                Password = builder.Password
            };
            return connectionInfo;
        }

        public void BeginTransaction()
        {
            if (_sqlConnection != null)
            {
                if (_sqlConnection.State == ConnectionState.Closed)
                    _sqlConnection.Open();
                _sqlTransaction = _sqlConnection.BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            if (_sqlConnection != null)
            {
                if (_sqlTransaction != null)
                {
                    _sqlTransaction.Commit();
                    _sqlTransaction = null;
                }
                if (_sqlConnection.State == ConnectionState.Open)
                {
                    _sqlConnection.Close();
                    _sqlConnection.Dispose();
                }
            }
        }

        public void RollbackTransaction()
        {
            if (_sqlConnection != null)
            {
                if (_sqlTransaction != null)
                    _sqlTransaction.Rollback();
                if (_sqlConnection.State == ConnectionState.Open)
                {
                    _sqlConnection.Close();
                    _sqlConnection.Dispose();
                }
            }
        }

        public void Dispose()
        {
            if (_sqlDataReader != null && !_sqlDataReader.IsClosed)
            {
                _sqlDataReader.Dispose();
            }
            if (_sqlConnection != null && _sqlConnection.State == ConnectionState.Open)
            {
                _sqlConnection.Close();
            }
            if (_sqlConnection != null)
                _sqlConnection.Dispose();
        }

        public DbDataReader ExecuteReader(string spName, DbParameter[] dbParameters = null)
        {
            using (var sqlCommand = new SqlCommand
            {
                Connection = _sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = spName
            })
            {
                sqlCommand.Parameters.Clear();
                AddParameters(dbParameters, sqlCommand);
                if (_sqlTransaction != null)
                    sqlCommand.Transaction = _sqlTransaction;
                _sqlDataReader = sqlCommand.ExecuteReader();
                return _sqlDataReader;
            }
        }

        private void AddParameters(DbParameter[] dbParameters, SqlCommand command)
        {
            if (dbParameters != null)
                foreach (DbParameter param in dbParameters)
                    command.Parameters.Add(param);
        }

        public int ExecuteNonQuery(string spName, DbParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = spName;
            command.Parameters.Clear();
            AddParameters(sqlParameters, command);
            if (_sqlTransaction != null)
                command.Transaction = _sqlTransaction;
            int result = command.ExecuteNonQuery();
            return result;
        }

        public void CloseConnection()
        {
            if (_sqlConnection != null)
            {
                if (_sqlTransaction != null)
                    _sqlTransaction.Rollback();
                if (_sqlConnection.State == ConnectionState.Open)
                {
                    _sqlConnection.Close();
                    _sqlConnection.Dispose();
                }
            }
        }
    }
}
