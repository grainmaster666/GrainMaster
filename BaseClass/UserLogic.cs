using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public static class UserLogic
{
  
    public static UsersDetail AuthentiateUser(string userid, string password)
    {
        UsersDetail user = new UsersDetail();
        DataSet ds = DsGetUser(userid);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            bool IsVerify = PasswordHashing.Verify(password, Convert.ToString(dr["password"]));
            if (IsVerify)
            {
                user.ID = Convert.ToInt32(dr["ID"]);
                user.Name = Convert.ToString(dr["Name"]);
                user.Email = Convert.ToString(dr["email"]);
                user.UserStatus = (UserStatus)Enum.Parse(typeof(UserStatus), Convert.ToString(dr["IsActive"]));

                SetLoggedUser = user;
            }
        }

        return user;
    }

    public static UsersDetail SetLoggedUser
    {
        set
        {
            HttpContext.Current.Session["LoggedUser"] = value;
        }
    }

    public static UsersDetail LoggedUser
    {
        get
        {
            if (HttpContext.Current.Session["LoggedUser"] != null)
                return (UsersDetail)HttpContext.Current.Session["LoggedUser"];
            else
                return new UsersDetail();
        }
    }

    private static DataSet DsGetUser(string userid, string password =null)
    {
        DBHelper db = new DBHelper();
        List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@username", userid),
                new SqlParameter("@password", password)
        };
        return db.ExecuteDataSet("proc_Login", parameters);
    }
}

[Serializable]
public class UsersDetail
{
    public int ID;
    public string Name;
    public string Email;
    public UserStatus UserStatus = UserStatus.NotFound;
}

public enum UserStatus { NotFound, Active, Inactive}
