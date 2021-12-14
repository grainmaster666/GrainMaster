using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GrainMaster.Models;

public class Register
{
    public bool UserRegister(RegisterModel param)
    {
        try
        {
            DBHelper db = new DBHelper();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@Name",param.Name.ToString()),
                new SqlParameter("@email",param.Email.ToString()),
                new SqlParameter("@Password",PasswordHashing.Hash(param.Password.ToString()))
            };
            db.ExecuteNonQuery("ProcRegister", parameters, CommandType.StoredProcedure);
            return true;
        }
        catch(Exception ex)
        {
            Console.WriteLine("Error : " + ex.Message);
            return false;
        }        
    }

    public static bool ChangePassword(ChangePasswordModel param)
    {
        try
        {
            DBHelper db = new DBHelper();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@email",UserLogic.LoggedUser.Email.ToString()),
                new SqlParameter("@password",PasswordHashing.Hash(param.NewPassword.ToString()))
            };
            db.ExecuteNonQuery("sp_changePassword", parameters, CommandType.StoredProcedure);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}