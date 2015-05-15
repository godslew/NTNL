using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTNL.NTNL_Config
{
    class DBConstants
    {
      public  const String PIECE_TABLE_NAME = "%TABLE_NAME%";
	  public  const String PIECE_WHERE = "%WHERE%";
	  public  const String PIECE_COLUMNS = "%COLUMNS%";
	  public  const String PIECE_VALUES = "%VALUES%";
      public  const String PIECE_SET = "%SET%";

	  public  const String SQLBASE_INSERT = "INSERT INTO " + DBConstants.PIECE_TABLE_NAME + " (" + DBConstants.PIECE_COLUMNS + ") VALUES (" + DBConstants.PIECE_VALUES + ")";
	  public  const String SQLBASE_SELECT = "SELECT " + DBConstants.PIECE_COLUMNS + " FROM " + DBConstants.PIECE_TABLE_NAME + " WHERE " + DBConstants.PIECE_WHERE + " ";
	  public  const String SQLBASE_UPDATE = "UPDATE " + DBConstants.PIECE_TABLE_NAME + " SET " + DBConstants.PIECE_SET + " WHERE " + DBConstants.PIECE_WHERE + " ";
	  public  const String SQLBASE_DELETE = "DELETE FROM " + DBConstants.PIECE_TABLE_NAME + " WHERE " + DBConstants.PIECE_WHERE + " ";
    }
}
