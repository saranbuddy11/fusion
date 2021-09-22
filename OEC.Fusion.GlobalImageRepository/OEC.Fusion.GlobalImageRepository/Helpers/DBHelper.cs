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
        private readonly string dsn = ConfigHelper.GetDefaultConnection();
        public string result="";
        public DBHelper()
        {
            dal = new DAL(dsn);
        }

        public string GetPartNumber()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"select top 1 pp.[PartNumber] FROM[GlobalImageRepository].[import].[tblPRTPartCPIPersisted] pp left join[GlobalImageRepository].[import].[tblIMGImageListPersisted] im ON pp.PartId = im.PartId where 1 = 1 and im.PartId is NUll and pp.PartTypeId = 1 and pp.CPIRegionId = 2 order by pp.PartId desc");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            string results = ds.Tables[0].Rows[0][0].ToString().Trim();
            return results;
        }

        public string GetsFTPPath()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"declare @locPath varchar(500) = '' declare @sftpUserName varchar(50) = '' set @locPath = (SELECT TOP(1) LocationPath FROM [GlobalImageRepository].[config].[tblSYSLocation] where 1 = 1 and [LocationName] = 'SFTPPath') set @sftpUserName = (SELECT TOP(1) [ProviderSFTPUserName] FROM [GlobalImageRepository].[config].[tblFORDIllustrationProvider] where 1 = 1 and ProviderId = 2) select @locPath+@sftpUserName");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            string sFTPPath = ds.Tables[0].Rows[0][0].ToString().Trim();
            return sFTPPath;
        }

        public List<String> GetCurDateTime()
        {
            //Pulls the server current date and time in the format payyyy-mm-dd_hhmmss

            var sb = new StringBuilder();
            sb.AppendLine($"declare @curDateTime varchar(19) set @curDateTime = convert(varchar, getdate(), 120) select @curDateTime select 'pa' + SUBSTRING(@curDateTime, 1, 10) + '_' + SUBSTRING(@curDateTime, 12, 2) + SUBSTRING(@curDateTime, 15, 2) + SUBSTRING(@curDateTime, 18, 2)");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            List<String> curDateTime = new List<string>();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                curDateTime.Add(ds.Tables[1].Rows[i][0].ToString().Trim());
            }
            return curDateTime;
        }

        public string VerifyImagesPresentInFolder(string result)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"select Count(1) from [GlobalImageRepository].[import].[tblIMGImageListPersisted]  ilp where 1 = 1 and ilp.ImageView like '360-%' and ilp.PartNumber = '" +result+ "'");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            string verifyImages = ds.Tables[0].Rows[0][0].ToString().Trim();
            return verifyImages;
        }

        public string VerifyProperImagesPresentInFolder(string result)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"select Count(1) from [GlobalImageRepository].[import].[tblIMGImageListPersisted]  ilp where 1 = 1 and ilp.PartNumber = '" + result + "'");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            string verifyImages = ds.Tables[0].Rows[0][0].ToString().Trim();
            return verifyImages;
        }

        public string GetPartNumberAlreadyUsed()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"select top 1 pp.[PartNumber] FROM[GlobalImageRepository].[import].[tblPRTPartCPIPersisted] pp inner join[GlobalImageRepository].[import].[tblIMGImageListPersisted] im ON pp.PartId = im.PartId where 1 = 1 and pp.PartTypeId = 1 and pp.CPIRegionId = 2 and left(im.ImageView, 3) = '360' order by pp.PartId , im.ImageId desc");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            string resultsAU = ds.Tables[0].Rows[0][0].ToString().Trim();
            return resultsAU;
        }

        public List<String> GetDateTimeOfUsedPN(string partNumberAU)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"select MAX(im.UpdateDate) from [GlobalImageRepository].[import].[tblIMGImageListPersisted] im where 1 = 1 and im.PartNumber = '"+ partNumberAU + "'");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            List<String> dateTimeUsedPN = new List<string>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dateTimeUsedPN.Add(ds.Tables[0].Rows[i][0].ToString().Trim());
            }
            return dateTimeUsedPN;
        }

        public string VerifyNonExistingPN()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"SELECT COUNT(1) FROM[GlobalImageRepository].[import].[tblPRTPartCPIPersisted] where 1 = 1 and[PartNumber] = 'a1b2c3d4e5f6g7h8i9j0'");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            string nonExistingPN = ds.Tables[0].Rows[0][0].ToString().Trim();
            return nonExistingPN;
        }

        public string GetDifferentRegionsFTPPath()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"declare @locPath varchar(500) = '' declare @sftpUserName varchar(50) = '' set @locPath = (SELECT TOP(1) LocationPath FROM[GlobalImageRepository].[config].[tblSYSLocation] where 1 = 1 and[LocationName] = 'SFTPPath') set @sftpUserName = (SELECT TOP(1)[ProviderSFTPUserName] FROM[GlobalImageRepository].[config].[tblFORDIllustrationProvider] where 1 = 1 and ProviderId = 1) select @locPath+@sftpUserName");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            string sFTPPath = ds.Tables[0].Rows[0][0].ToString().Trim();
            return sFTPPath;
        }

        public Object SpPRODDailyDownload(string sprocName)
        {
            var ds = dal.ExecuteStoredProcedureScalar(sprocName);
            Object results = new object();
            return ds;
        }
        public string GetProperImageNumber(string CurlImageView)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"SELECT TOP 1 substring(ImageName,1,len(ImageName)-8) FROM [GlobalImageRepository].[frontend].[tblIMGImageDetails] imd WHERE 1=1 and PrimaryView = 1 and ImageView = '"+ CurlImageView +"'");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            string results = ds.Tables[0].Rows[0][0].ToString().Trim();
            return results;
        }

        public List<string> ExpectedImageView(string ExpectedImageView, string result)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"select PrimaryView FROM [GlobalImageRepository].[frontend].[tblIMGImageDetails] imd where 1=1 and substring(ImageName,1,len(ImageName)-8) = '"+result+"' and ImageView = '"+ExpectedImageView+"'");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            List<String> verifyImages = new List<String>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                verifyImages.Add(ds.Tables[0].Rows[i][0].ToString().Trim());
            }
            return verifyImages; 
        }

        public string OtherImageView(string ExpectedImageView, string result)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"select distinct PrimaryView FROM [GlobalImageRepository].[frontend].[tblIMGImageDetails] imd where 1=1 and substring(ImageName,1,len(ImageName)-8) = '"+result+"' and ImageView != '"+ExpectedImageView+"'");
            var sql = sb.ToString();
            System.Data.DataSet ds = dal.ExecuteSQLSelect(sql);
            string results = ds.Tables[0].Rows[0][0].ToString().Trim();
            return results;
        }
    }
}
