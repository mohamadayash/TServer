using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingServer.DB
{
    public static class DatabaseManager
    {
        static string GetConnectionString()
        {
            string returnValue = null;

            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings["default"];

            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
        public static string Login(string userid,string password)
        {
            string connectionString = GetConnectionString();

            string queryString ="SELECT userid from dbo.tblUsers where userid=@userid and password=@password";

            SqlDataReader reader = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@password", password);
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();
                    
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    reader?.Close();
                    connection.Close();
                }

            }
        }

        public static bool LogTrade(string userid,long login,long deal,int action,string symbol,double price,double profit,long volume, DateTime datetime)
        {
            string connectionString = GetConnectionString();

            string queryString = "insert into dbo.tblTrades (userid,login,deal,action,symbol,price,profit,volume,time)" +
                " values (@userid,@login,@deal,@action,@symbol,@price,@profit,@volume,@time)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@deal", deal);
                command.Parameters.AddWithValue("@action", action);
                command.Parameters.AddWithValue("@symbol", symbol);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@profit", profit);
                command.Parameters.AddWithValue("@volume", volume);
                command.Parameters.AddWithValue("@time", datetime);


                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }

            }
        }
    }
}
