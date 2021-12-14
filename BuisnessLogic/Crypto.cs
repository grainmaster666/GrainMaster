using System;
using System.Collections.Generic;
using System.Data;
using GrainMaster.Models;

namespace GrainMaster.BuisnessLogic
{
    public class Crypto
    {
        public static List<CryptoModel> Get()
        {
            try
            {
                DBHelper db = new DBHelper();

                List<CryptoModel> cryptos = new List<CryptoModel>();

                DataSet ds = db.ExecuteDataSet("sp_GetCryptosceener", null, CommandType.StoredProcedure);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        cryptos.Add(new CryptoModel()
                        {
                            Name = Convert.ToString(row["Name"]),
                            Symbol = Convert.ToString(row["Symbol"]),
                            Image = Convert.ToString(row["Image"]),
                            CurrentPrice = Convert.ToDecimal(row["Current_price"]),
                            Volume_Change_15M_percent = Convert.ToDecimal(row["Volume_Change_15M_percent"]),
                            Volume_Change_30M_percent = Convert.ToDecimal(row["Volume_Change_30M_percent"]),
                            Volume_Change_1h_percent = Convert.ToDecimal(row["Volume_Change_1h_percent"]),
                        });
                    }

                }
                return cryptos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}