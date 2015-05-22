using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Linq;

namespace NTNL.Models.DB.DAO
{
    class ColumnDAO : SuperDAO
    {
        public ColumnDAO(SQLiteConnection dbConnectionString, String tableName)
            : base(dbConnectionString, tableName)
        {

        }
    }
}
