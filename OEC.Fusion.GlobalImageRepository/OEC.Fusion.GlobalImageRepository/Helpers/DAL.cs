using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace OEC.Fusion.GlobalImageRepository.Helpers
{
    public class DAL
    {
        protected internal string _DSN = "";
        public DAL(string dsn)
        {
            this._DSN = dsn;
        }

        public void ExecuteSQL(string sql)
        {
            using (SqlConnection con = new SqlConnection(_DSN))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

    }
}
