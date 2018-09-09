using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace HoustBuilder.LoadData.DbServices
{
    public interface IDatabase
    {
        void ExecuteScalar(string spCommand, params DbParameter[] parameters);
    }
}
