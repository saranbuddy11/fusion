using OEC.Fusion.GlobalImageRepository.Specflow.Steps;
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
       // GIRSteps gir = new GIRSteps();
        public string result="";
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
            //String results = "MM1PZ16A550BA";
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

        public List<String> getCurDateTime()
        {
            
        var sb = new StringBuilder();
            sb.AppendLine($"declare @curDateTime varchar(19) set @curDateTime = convert(varchar, getdate(), 120) select @curDateTime select 'pa' + SUBSTRING(@curDateTime, 1, 10) + '_' + SUBSTRING(@curDateTime, 12, 2) + SUBSTRING(@curDateTime, 15, 2) + SUBSTRING(@curDateTime, 18, 2)");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            List<String> curDateTime = new List<string>();
           // curDateTime.Add(ds.Tables[1].Rows[1][0].ToString().Trim());
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                curDateTime.Add(ds.Tables[1].Rows[i][0].ToString().Trim());
            }
            return curDateTime;
        }

        public List<string> VerifyImagesPresentInFolder(string result)
        {
            
            //string PN = results[0];
            //string PN = GetPartNumber()[0];
            //String PN = "MM1PZ16A550BA";
            var sb = new StringBuilder();
            sb.AppendLine($"select Count(1) from [GlobalImageRepository].[import].[tblIMGImageListPersisted]  ilp where 1 = 1 and ilp.ImageView like '360-%' and ilp.PartNumber = '" +result+ "'");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            List<String> verifyImages = new List<String>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
            // verifyImages = ds.Tables[0].Rows[0][0].ToString().Trim();
            verifyImages.Add(ds.Tables[0].Rows[i][0].ToString().Trim());
            }
            return verifyImages;
        }

       


        public Object spPRODDailyDownload(string sprocName)
        {
            
            var ds = dal.ExecuteStoredProcedureScalar(sprocName);
            Object results = new object();

            return ds;
        }
    }
}
