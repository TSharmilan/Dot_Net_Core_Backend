using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace MCCS.Interfaces
{
    public interface IDataService : IDisposable
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void CloseConnection();
        DbDataReader ExecuteReader(string spName, DbParameter[] dbParameters = null);
        int ExecuteNonQuery(string spName, DbParameter[] sqlParameters = null);
    }
}
