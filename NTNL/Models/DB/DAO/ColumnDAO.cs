using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using NTNL.Models.DB.Entity;
using NTNL.NTNL_Config;

namespace NTNL.Models.DB.DAO
{
    class ColumnDAO : EntityDAO
    {
        public ColumnDAO(SQLiteConnection dbConnectionString, String tableName)
            : base(dbConnectionString, DBConstants.COLUMN_NUM, DBConstants.COLUMN_NAME)
        {

        }
    }
}
