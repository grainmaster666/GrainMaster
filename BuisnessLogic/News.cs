using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using GrainMaster.Models;

namespace GrainMaster.BuisnessLogic
{
    public class News
    {
        private static readonly int _days ;

       static News()
        {
            _days = Convert.ToInt32(WebConfigurationManager.AppSettings["days"].ToString());
        }
        public static NewsModel GetNews(string CName)
        {
            NewsModel newsModel = new NewsModel();

            
            List<SqlParameter> sqlparameters = new List<SqlParameter>()
            {
                new SqlParameter("@days",_days),
                new SqlParameter("@CompanyName", CName)
            };

            DBHelper db = new DBHelper();
            DataSet ds = db.ExecuteDataSet("sp_getnews", sqlparameters, CommandType.StoredProcedure);
            if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                newsModel.Count = ds.Tables[0].Rows.Count;
                newsModel.link = "../News/Get?CName=" + CName;
                
            }
            return newsModel;
        }

        public static List<NewsRepository> GetNewsDetail(string CName)
        {
            List<NewsRepository> lstRepo = new List<NewsRepository>();


            List<SqlParameter> sqlparameters = new List<SqlParameter>()
            {
                new SqlParameter("@days",_days),
                new SqlParameter("@CompanyName", CName)
            };

            DBHelper db = new DBHelper();
            DataSet ds = db.ExecuteDataSet("sp_getnews", sqlparameters, CommandType.StoredProcedure);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                foreach (DataRow row in ds.Tables[0].Rows)
                    lstRepo.Add(FromDataRow(row));
            return lstRepo;
        }

        public static List<NewsRepository> GetTopFiveNews()
        {
            List<NewsRepository> lstRepo = new List<NewsRepository>();
            DBHelper db = new DBHelper();
            string query = "select top 5 Title,Description,FeedType,Link,Image,Newsdate from tblNews order by Newsdate desc";
            DataSet ds = db.ExecuteDataSet(query, null, CommandType.Text);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                foreach (DataRow row in ds.Tables[0].Rows)
                    lstRepo.Add(FromDataRow(row));
            return lstRepo;
        }

        public static List<NewsRepository> GetAllNews()
        {
            List<NewsRepository> lstRepo = new List<NewsRepository>();
            DBHelper db = new DBHelper();
            string query = "select Title,Description,FeedType,Link,Image,Newsdate from tblNews order by Newsdate desc";
            DataSet ds = db.ExecuteDataSet(query, null, CommandType.Text);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                foreach (DataRow row in ds.Tables[0].Rows)
                    lstRepo.Add(FromDataRow(row));
            return lstRepo;
        }

        public static NewsRepository FromDataRow(DataRow row)
        {
            NewsRepository newsRepository = new NewsRepository()
            {
                Title = Convert.ToString(row["Title"]),
                Image = Convert.ToString(row["Image"]),
                Description = Convert.ToString(row["Description"]),
                Link = Convert.ToString(row["Link"]),
                Date = Convert.ToDateTime(row["Newsdate"]).ToString("dd MMMM yyy"),
                FeedType = Convert.ToString(row["FeedType"]),
            };
            return newsRepository;
        }
    }
}