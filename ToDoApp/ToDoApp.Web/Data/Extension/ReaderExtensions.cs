using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ToDoApp.Web.Data.Extension
{
    //Extension Methods for DataReader class to allow safe type returns from MSSM
    public static class ReaderExtensions
    {

        //Get a string from SQL and trim it
        public static string GetSQLString(this IDataReader reader, Int32 index, bool trim = true)
        {
            if (reader[index] != null && reader[index] != DBNull.Value)
            {
                if (trim)
                    return reader.GetString(index).Trim();
                else
                    return reader.GetString(index);
            }
            else
            {
                return null;
            }
        }

        //Get int from SQL and set equal to 0 if value is null
        public static int GetSQLInt(this IDataReader reader, Int32 index)
        {
            if (reader[index] != null && reader[index] != DBNull.Value)
            {
                return reader.GetInt32(index);
            }
            else
            {
                return 0;
            }
        }

        //Get int from SQL and set equal to null if SQL column IS NULL
        public static int? GetSQLInt32Nullable(this IDataReader reader, Int32 index)
        {
            if (reader[index] != null && reader[index] != DBNull.Value)
            {
                return reader.GetInt32(index);
            }
            else
            {
                return null;
            }
        }

        //Get DateTime from SQL 
        public static DateTime GetSQLDateTime(this IDataReader reader, Int32 index)
        {
            if (reader[index] != null && reader[index] != DBNull.Value)
            {
                return reader.GetDateTime(index);
            }
            else
            {
                return default(DateTime);
            }
        }

        //Get Nullable DateTime from SQL
        public static DateTime? GetSQLDateTimeNullable(this IDataReader reader, Int32 index)
        {
            if (reader[index] != null && reader[index] != DBNull.Value)
            {
                return reader.GetDateTime(index);
            }
            else
            {
                return null;
            }
        }
    }
}