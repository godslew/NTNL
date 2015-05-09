using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTNL.Models.DB.DAO
{
    class PrivateDAO : SuperDAO
    {
        public PrivateDAO(String dbConnectionString, String tableName)
            : base(dbConnectionString, tableName)
        {

        }
    }
}
