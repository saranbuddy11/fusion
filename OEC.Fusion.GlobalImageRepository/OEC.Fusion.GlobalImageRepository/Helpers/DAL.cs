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

        public DataSet ExecuteSQLSelect(string sql)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(_DSN))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            con.Open();
                            ds = new DataSet();
                            da.Fill(ds);
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
            return ds;
        }

        public DataSet ExecuteSQLSelect(SqlCommand cmd)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(_DSN))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cmd.Connection = con;
                    con.Open();
                    ds = new DataSet();
                    da.Fill(ds);
                    con.Close();
                }
            }

            return ds;
        }

        public object ExecuteStoredProcedureScalar(string sprocName, List<System.Data.SqlClient.SqlParameter> @params)
        {
            Object returnValue = null;
            using (SqlConnection con = new SqlConnection(_DSN))
            {
                using (SqlCommand cmd = new SqlCommand(sprocName, con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(@params.ToArray());
                        con.Open();
                        returnValue = cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }

                }
            }
            return returnValue;
        }



    }
}
