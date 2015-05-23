using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTNL.NTNL_Config
{
    class DBConstants
    {
      public  const String DB_NAME = "NTNL_DB";
      public  const String DB_CONNECTION = "Data Source = sqlite3"+ DBConstants.DB_NAME + ".sqlite3";
      public  const String CREATE_TABLE_ACCOUNT = "CREATE TABLE ACCOUNT(TwitterID TEXT NOT NULL, CK TEXT NOT NULL , CS TEXT NOT NULL , AT TEXT NOT NULL , ATS TEXT NOT NULL ,  PRIMARY KEY(TwitterID ))";
      public  const String CREATE_TABLE_MUTE = "CREATE TABLE MUTE(ID INTEGER NOT NULL , TwitterID TEXT NOT NULL ,userID TEXT NOT NULL , Media NUMERIC NOT NULL , Tweet NUMERIC NOT NULL , RT NUMERIC NOT NULL ,Favorite NUMERIC NOT NULL ,  PRIMARY KEY (ID) , FOREIGN KEY (TwitterID) REFERENCES ACCOUNT(TwitterID))";
      public  const String CREATE_TABLE_TAG = "CREATE TABLE TAG (ID INTEGER NOT NULL , TwitterID TEXT NOT NULL , TagName TEXT NOT NULL ,PRIMARY KEY(ID) , FOREIGN KEY(TwitterID) REFERENCES ACCOUNT(TwitterID))";

      public  const String PIECE_TABLE_NAME = "%TABLE_NAME%";
	  public  const String PIECE_WHERE = "%WHERE%";
	  public  const String PIECE_COLUMNS = "%COLUMNS%";
	  public  const String PIECE_VALUES = "%VALUES%";
      public  const String PIECE_SET = "%SET%";

	  public  const String SQLBASE_INSERT = "INSERT INTO " + DBConstants.PIECE_TABLE_NAME + " (" + DBConstants.PIECE_COLUMNS + ") VALUES (" + DBConstants.PIECE_VALUES + ")";
	  public  const String SQLBASE_SELECT = "SELECT " + DBConstants.PIECE_COLUMNS + " FROM " + DBConstants.PIECE_TABLE_NAME + " WHERE " + DBConstants.PIECE_WHERE + " ";
	  public  const String SQLBASE_UPDATE = "UPDATE " + DBConstants.PIECE_TABLE_NAME + " SET " + DBConstants.PIECE_SET + " WHERE " + DBConstants.PIECE_WHERE + " ";
	  public  const String SQLBASE_DELETE = "DELETE FROM " + DBConstants.PIECE_TABLE_NAME + " WHERE " + DBConstants.PIECE_WHERE + " ";
    
      //ACCOUNT
      public const String ACCOUNT_ID = "ID";
      public const String ACCOUNT_TwitterID = "TwitterID";
      public const String ACCOUNT_CK = "CK";
      public const String ACCOUNT_CS = "CS";
      public const String ACCOUNT_AT = "AT";
      public const String ACCOUNT_ATS = "ATS";
 
      //COLUMN

      //MUTE
      public const String MUTE_ID = "ID";
      public const String MUTE_TwitterID = "TwitterID";
      public const String MUTE_UserID = "UserID";
      public const String MUTE_Media = "Media";
      public const String MUTE_Tweet = "Tweet";
      public const String MUTE_RT = "RT";
      public const String MUTE_Favorite = "Favorite";

      //PRIVATE

      //TAG
      public const String TAG_ID = "ID";
      public const String TAG_TwitterID = "TwitterID";
      public const String TAG_TagName = "TagName";
    }
}
