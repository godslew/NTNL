using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Linq;

namespace NTNL.Models.DB.DAO
{
    class EntityDAO : SuperDAO
    {
        protected String primaryColumnName;

        public EntityDAO(String dbConnectionString, String tableName, String primaryColumnName)
            : base(dbConnectionString, tableName)
        {
            this.primaryColumnName = primaryColumnName;
        }
        /*       
                protected SQLiteDataReader find(int id)
                {
                    Dictionary<String,Object> where = new Dictionary<String, Object>();
                    where[this.primaryColumnName] = id;
                    return this.select(where);
                }

                protected SQLiteDataReader findIdentity(String columnName, Object value)
                {
                    Dictionary<String, Object> where = new Dictionary<string, object>();
                    where[columnName] = value;
                    return this.select(where);
                }
            }
        */
    }
}
