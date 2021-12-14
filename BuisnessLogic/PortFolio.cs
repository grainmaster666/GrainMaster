using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GrainMaster.Models;
using GrainMaster.BuisnessLogic;

public class PortFolio
{
    public static string CreatePortFolio(PortFolioModel portFolioModel)
    {
        try
        {
            DBHelper db = new DBHelper();
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@stockName",Convert.ToString(portFolioModel.StockName)),
                new SqlParameter("@Quantity", Convert.ToString(portFolioModel.Quantity)),
                new SqlParameter("@price", Convert.ToString(portFolioModel.Price)),
                new SqlParameter("@purchasedate",Convert.ToDateTime(portFolioModel.Date)),
                new SqlParameter("@purchaseType",Convert.ToInt32(portFolioModel.PurchaseType)),
                new SqlParameter("@UserID",Convert.ToString(UserLogic.LoggedUser.ID)),
                new SqlParameter("@tpsid",Convert.ToString(portFolioModel.TPSID))

            };
            db.ExecuteNonQuery("sp_customerposition", parameters, CommandType.StoredProcedure);
            return "1";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }

    public static string Delete(int id)
    {
        try
        {
            DBHelper db = new DBHelper();
            List<SqlParameter> parameters = new List<SqlParameter> { new SqlParameter("@id", Convert.ToInt32(id)) };
            db.ExecuteNonQuery("sp_deletePosition", parameters, CommandType.StoredProcedure);
            return "1";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }

    public static bool Edit(PortFolioModel portFolioModel)
    {
        try
        {
            DBHelper db = new DBHelper();
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id",Convert.ToInt32(portFolioModel.ID)),
                new SqlParameter("@stockName",Convert.ToString(portFolioModel.StockName)),
                new SqlParameter("@Quantity", Convert.ToString(portFolioModel.Quantity)),
                new SqlParameter("@price", Convert.ToString(portFolioModel.Price)),
                new SqlParameter("@purchasedate",Convert.ToDateTime(portFolioModel.Date)),
                new SqlParameter("@purchaseType",Convert.ToInt32(portFolioModel.PurchaseType)),
                new SqlParameter("@UserID",Convert.ToString(UserLogic.LoggedUser.ID)),
                //new SqlParameter("@tpsid",Convert.ToString(portFolioModel.TPSID))

            };
            db.ExecuteNonQuery("sp_editPosition", parameters, CommandType.StoredProcedure);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static CombinedModel GetByID()
    {
        CombinedModel portFolioRecords = new CombinedModel();
        List<VolDeliveryModel> lst = new List<VolDeliveryModel>();
       
        DBHelper db = new DBHelper();

        List<SqlParameter> parameters = new List<SqlParameter> {new SqlParameter("@UserID",Convert.ToString(UserLogic.LoggedUser.ID)) };

        DataSet ds =  db.ExecuteDataSet("SELECT t1.ID,t2.Name AS stock_name,CompanyId ,Quantity,PurchasePrice,PurchaseDate,PurchaseType,S1,S2,S3,SMA5,SMA10,SMA20,SMA50,SMA100,SMA200,R1,R2,R3,DeliveryAverageMonth,DeliveryAverageWeek,DeliveryYesterday FROM CustomerPositions t1 INNER JOIN Company t2 ON t1.CompanyId = t2.Id WHERE UserID = @UserID", parameters, CommandType.Text);
        if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                lst.Add(FromRow(row));
            }
            portFolioRecords.PortFolioList = lst;
        }
            
        portFolioRecords.NewsRepositoriesModel = News.GetTopFiveNews();
        return portFolioRecords;
    }

    public static VolDeliveryModel FromRow(DataRow row)
    {
        VolDeliveryModel volDeliveryModel = new VolDeliveryModel
        {
            ID = Convert.ToInt32(row["ID"]),
            StockName = Convert.ToString(row["stock_name"]),
            Quantity = Convert.ToString(row["Quantity"]),
            Price = Convert.ToString(row["PurchasePrice"]),
            PurchaseType = Convert.ToInt32(row["PurchaseType"]),
            Date = Convert.ToDateTime(row["PurchaseDate"]),
            R1 = Convert.ToDecimal(row["R1"] == DBNull.Value ? 0 : row["R1"]),
            R2 = Convert.ToDecimal(row["R2"] == DBNull.Value ? 0 : row["R2"]),
            R3 = Convert.ToDecimal(row["R3"] == DBNull.Value ? 0 : row["R3"]),
            S1 = Convert.ToDecimal(row["S1"] == DBNull.Value ? 0 : row["S1"]),
            S2 = Convert.ToDecimal(row["S2"] == DBNull.Value ? 0 : row["S2"]),
            S3 = Convert.ToDecimal(row["S3"] == DBNull.Value ? 0 : row["S3"]),
            SMA5 = Convert.ToDecimal(row["SMA5"] == DBNull.Value ? 0 : row["SMA5"]),
            SMA10 = Convert.ToDecimal(row["SMA10"] == DBNull.Value ? 0 : row["SMA10"]),
            SMA20 = Convert.ToDecimal(row["SMA20"] == DBNull.Value ? 0 : row["SMA20"]),
            SMA50 = Convert.ToDecimal(row["SMA50"] == DBNull.Value ? 0 : row["SMA50"]),
            SMA100 = Convert.ToDecimal(row["SMA100"] == DBNull.Value ? 0 : row["SMA100"]),
            SMA200 = Convert.ToDecimal(row["SMA200"] == DBNull.Value ? 0 : row["SMA200"]),
            DeliveryAverageMonth = row["DeliveryAverageMonth"] == DBNull.Value ? "0%" : Convert.ToString(row["DeliveryAverageMonth"]),
            DeliveryAverageWeek = row["DeliveryAverageWeek"] == DBNull.Value ? "0%" : Convert.ToString(row["DeliveryAverageWeek"]),
            DeliveryYesterday = row["DeliveryYesterday"] == DBNull.Value ? "0%" : Convert.ToString(row["DeliveryYesterday"]),
            ISDealType = GetDealType(Convert.ToString(row["stock_name"]))
        };

        NewsModel newsModel = News.GetNews(Convert.ToString(row["stock_name"]));
        volDeliveryModel.Count = newsModel.Count;
        volDeliveryModel.link = newsModel.link;
        return volDeliveryModel;
    }
    public static CombinedModel GetAll()
    {
        CombinedModel combinedModel = new CombinedModel();
        List<VolDeliveryModel> lst = new List<VolDeliveryModel>();

        DBHelper db = new DBHelper();
        DataSet ds = db.ExecuteDataSet("SELECT t1.ID,t2.Name AS stock_name,CompanyId ,Quantity,PurchasePrice,PurchaseDate,S1,S2,S3,SMA5,SMA10,SMA20,SMA50,SMA100,SMA200,R1,R2,R3,DeliveryAverageMonth,DeliveryAverageWeek,DeliveryYesterday FROM CustomerPositions  t1 INNER JOIN Company t2 ON t1.CompanyId = t2.Id", null, CommandType.Text);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                lst.Add(FromRow(row));               
            }
            combinedModel.PortFolioList = lst;
        }
        combinedModel.NewsRepositoriesModel = News.GetTopFiveNews();
        return combinedModel;
    }

    public static List<StockDetail> GetSector()
    {
        List<StockDetail> stockDetails = new List<StockDetail>();

        try
        {
            DBHelper db = new DBHelper();

            DataSet ds = db.ExecuteDataSet("select ID, Name from Company", null, CommandType.Text);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    StockDetail stockDetail = new StockDetail
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Name = Convert.ToString(row["Name"]),
                    };
                    stockDetails.Add(stockDetail);
                }
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
        
        return stockDetails;
    }
    public static TPDetail GetTP(string Name)
    {
        TPDetail tPDetails = new TPDetail();

        try
        {
            DBHelper db = new DBHelper();

            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@name", Name.ToString()) };
            DataSet ds = db.ExecuteDataSet("SELECT TP_SID,TP_URL FROM Company where Name=@name", sqlParameters, CommandType.Text);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];
                tPDetails.TPSID = Convert.ToString(row["TP_SID"]);
                tPDetails.URL = Convert.ToString(row["TP_URL"]);
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
        
        return tPDetails;
    }
    public static DealType GetDealType(string cname)
    {
        DealType dealType = new DealType();
        try
        {
            DBHelper db = new DBHelper();

            List<SqlParameter> sqlParameters = new List<SqlParameter>() { new SqlParameter("@company", cname.ToString()) };
            DataSet ds = db.ExecuteDataSet("sp_postivedeal", sqlParameters, CommandType.StoredProcedure);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dealType.IsPositiveDeal = true;
            }
            ds.Clear();
            ds = db.ExecuteDataSet("sp_negativedeal", sqlParameters, CommandType.StoredProcedure);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dealType.IsNegativeDeal = true;
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
        
        return dealType;
    }
}
