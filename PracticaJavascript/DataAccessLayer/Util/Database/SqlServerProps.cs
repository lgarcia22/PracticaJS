using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer.Util.Database
{
    class SqlServerProps
    {
        public static Decimal? GetDecimalReader(SqlDataReader reader, int position)
        {
            if (reader.IsDBNull(position))
            {
                return null;
            }
            else
            {
                return reader.GetDecimal(position);
            }

        }

        public static String GetStringReader(SqlDataReader reader, int position)
        {
            if (reader.IsDBNull(position))
            {
                return null;
            }
            else
            {
                return reader.GetString(position);
            }

        }

        public static DateTime? GetDateTimeReader(SqlDataReader reader, int position)
        {
            if (reader.IsDBNull(position))
            {
                return null;
            }
            else
            {
                return reader.GetDateTime(position);
            }

        }

        public static int? GetIntReader(SqlDataReader reader, int position)
        {
            if (reader.IsDBNull(position))
            {
                return null;
            }
            else
            {
                return reader.GetInt32(position);
            }

        }

        public static String GetStringSqlCommand(SqlCommand cmd, String param)
        {
            if (cmd.Parameters[param].Value == null)
            {
                return null;
            }
            else
            {
                return String.IsNullOrEmpty(cmd.Parameters[param].Value.ToString()) ? null : cmd.Parameters[param].Value.ToString();
            }

        }

        public static int? GetIntSqlCommand(SqlCommand cmd, String param)
        {
            if (cmd.Parameters[param].Value == null ||
                cmd.Parameters[param].Value.GetType().GetProperties().Count() == 0)
            {
                return null;
            }
            else
            {
                return int.Parse(cmd.Parameters[param].Value.ToString());
            }

        }
    }
}
