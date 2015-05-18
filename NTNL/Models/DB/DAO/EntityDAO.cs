using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTNL.Models.DB.DAO
{
    class EntityDAO : SuperDAO
    {
        public EntityDAO(String dbConnectionString, String tableName)
            : base(dbConnectionString, tableName)
        {

        }
    }
}
