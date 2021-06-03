using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace OEC.Fusion.GlobalImageRepository.Helpers
{
    public class DBHelper
    {
        private readonly DAL dal;
        private readonly string dsn = ConfigHelper.DBConnectionString;
        public DBHelper()
        {
            dal = new DAL(dsn);
        }
     
    }
}
