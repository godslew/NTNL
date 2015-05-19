using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Linq;


namespace NTNL.Models.DB.DAO
{
    class AccountDAO : SuperDAO
    {
        public AccountDAO(String dbConnectionString, String tableName)
            : base(dbConnectionString, tableName)
        {
        }

    }
}
