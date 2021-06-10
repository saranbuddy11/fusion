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
        private readonly string dsn = "Data Source=UQWDB023.qa.oec.local;Initial Catalog=GlobalImageRepository;Integrated Security=True";//ConfigHelper.DBConnectionString;
        public DBHelper()
        {
            dal = new DAL(dsn);
        }

        public List<String> GetPartNumber()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"select top 1 pp.[PartNumber] FROM[GlobalImageRepository].[import].[tblPRTPartCPIPersisted] pp left join[GlobalImageRepository].[import].[tblIMGImageListPersisted] im ON pp.PartId = im.PartId where 1 = 1 and im.PartId is NUll and pp.PartTypeId = 1 and pp.CPIRegionId = 2 order by pp.PartId desc");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            List<String> results = new List<String>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                results.Add(ds.Tables[0].Rows[i][0].ToString().Trim());
            }
            return results;
        }

        public List<String> getsFTPPath()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"declare @locPath varchar(500) = '' declare @sftpUserName varchar(50) = '' set @locPath = (SELECT TOP(1) LocationPath FROM [GlobalImageRepository].[config].[tblSYSLocation] where 1 = 1 and [LocationName] = 'SFTPPath') set @sftpUserName = (SELECT TOP(1) [ProviderSFTPUserName] FROM [GlobalImageRepository].[config].[tblFORDIllustrationProvider] where 1 = 1 and ProviderId = 2) select @locPath+@sftpUserName");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            List<String> sFTPpath = new List<string>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sFTPpath.Add(ds.Tables[0].Rows[i][0].ToString().Trim());
            }
            return sFTPpath;
        }

    }
}
