using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GrainMaster.Models;

namespace GrainMaster.BuisnessLogic
{
    public class Campaigen
    {
        public static bool SaveCampaigen(CampaigenModel crypto)
        {
            bool isSave;
            try
            {
                DBHelper db = new DBHelper();

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Name",Convert.ToString(crypto.Name)),
                    new SqlParameter("@TotalBuyPrice",Convert.ToDecimal(crypto.TotalBuyPrice)),
                    new SqlParameter("@TotalSellPrice",Convert.ToDecimal(crypto.TotalSellPrice)),
                    new SqlParameter("@Date",Convert.ToDateTime(crypto.Date)),
                    new SqlParameter("@CurrentPrice",Convert.ToDecimal(crypto.CurrentPrice)),
                    new SqlParameter("@status",1),
                    new SqlParameter("@UserID",UserLogic.LoggedUser.ID)
                };
                db.ExecuteNonQuery("sp_tblCampaigen_AddNDDel", parameters, CommandType.StoredProcedure);
                isSave = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return isSave;
        }

        public static List<CampaigenModel> Get()
        {
            try
            {
                DBHelper db = new DBHelper();

                List<CampaigenModel> campaigens = new List<CampaigenModel>();
                List<SqlParameter> parameters = new List<SqlParameter>() { new SqlParameter("@UserID", UserLogic.LoggedUser.ID) };
               
                DataSet ds= db.ExecuteDataSet("sp_GetCampaigen", parameters, CommandType.StoredProcedure);
                if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        campaigens.Add(new CampaigenModel()
                        {
                            Id = Convert.ToInt32(row["ID"]),
                            Name = Convert.ToString(row["Name"]),
                            TotalBuyPrice = Convert.ToDecimal(row["TotalBuyPrice"]),
                            TotalSellPrice = Convert.ToDecimal(row["TotalSellPrice"]),
                            CurrentPrice = Convert.ToDecimal(row["CurrentPrice"]),
                            Date = Convert.ToDateTime(row["Date"]),
                        });
                    }
                    
                }
                return campaigens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool DeleteCamp(int Id)
        {
            try
            {
                DBHelper db = new DBHelper();
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@UserID",UserLogic.LoggedUser.ID),
                    new SqlParameter("@ID", Convert.ToInt32(Id)),
                };
                db.ExecuteNonQuery("sp_delCampaigen", parameters, CommandType.StoredProcedure);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}