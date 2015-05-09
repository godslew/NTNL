using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTNL.Models.DB.DAO
{
    class ColumnDAO : SuperDAO
    {
        public ColumnDAO(String dbConnectionString, String tableName)
            : base(dbConnectionString, tableName)
        {

        }
    }
}
