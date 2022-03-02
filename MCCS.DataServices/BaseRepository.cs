using MCCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace MCCS.DataServices
{
    public abstract class BaseRepository
    {
        protected readonly IDataService _dataService;
        public BaseRepository(IDataService dataService) => _dataService = dataService;
        private static DbParameter CreateParameter(string name, DbType dbType, object value = null, ParameterDirection direction = ParameterDirection.Input) => new SqlParameter { DbType = dbType, ParameterName = name, Value = value ?? DBNull.Value, Direction = direction };

        private DbParameter CreateParameter(string name, DbType dbType, object value = null, ParameterDirection direction = ParameterDirection.Input, int size = 1)
        {
            if (!name.StartsWith("@"))
                name = $"@{name}";
            var param = new SqlParameter { DbType = dbType, ParameterName = name, Value = value ?? DBNull.Value, Direction = direction };
            if (size > 1)
                param.Size = size;
            return param;
        }

        protected DbParameter IntParameter(string name, Int32? value = null, ParameterDirection direction = ParameterDirection.Input) => CreateParameter(name, DbType.Int32, value, direction);
        protected DbParameter DecimalParameter(string name, Decimal? value = null, ParameterDirection direction = ParameterDirection.Input) => CreateParameter(name, DbType.Decimal, value, direction);
        protected DbParameter StringParameter(string name, string value = null, ParameterDirection direction = ParameterDirection.Input, int size = 1) => CreateParameter(name, DbType.String, value, direction, size);

    }
}
