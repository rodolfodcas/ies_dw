using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess
{
    public class UserDA
    {
        public int Create(UserDTO user)
        {
            SqlConnection sqlConn = null;
            SqlDataReader sqlDr = null;
            int id = 0;
            try
            {
                sqlConn = new SqlConnection("Server=(local); DataBase=NCapas;Integrated Security=SSPI");
                sqlConn.Open();

                SqlCommand sqlcmd = new SqlCommand("CreateUser", sqlConn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //Variables de entrada
                sqlcmd.Parameters.AddWithValue("@NickName_param",SqlDbType.VarChar).Value = user.NickName;
                sqlcmd.Parameters.AddWithValue("@Email_param", SqlDbType.VarChar).Value = user.Email;
                sqlcmd.Parameters.AddWithValue("@Password_param", SqlDbType.VarChar).Value = user.Password;
                sqlcmd.Parameters.AddWithValue("@IsEnabled_param", SqlDbType.VarChar).Value = user.IsEnabled;
                sqlcmd.Parameters.AddWithValue("@CreatedDate_param", SqlDbType.VarChar).Value = user.CreatedDate;

                //Variable de salida
                SqlParameter id_out = new SqlParameter("@Id_param", SqlDbType.Int);
                id_out.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(id_out);

                sqlcmd.ExecuteNonQuery();

                if (id_out.Value != DBNull.Value)
                    id = Convert.ToInt32(id_out.Value);

            }
            finally
            {
                if (sqlConn != null)
                    sqlConn.Close();
                if (sqlDr != null)
                    sqlDr.Close();
            }
            return id;
        }

        public bool Update(UserDTO user)
        {
            SqlConnection sqlConn = null;
            SqlDataReader sqlDr = null;
            bool isEnabled = false;
            try
            {
                sqlConn = new SqlConnection("Server=(local); DataBase=NCapas;Integrated Security=SSPI");
                sqlConn.Open();

                SqlCommand sqlcmd = new SqlCommand("UpdateUser", sqlConn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //Variables de entrada
                sqlcmd.Parameters.AddWithValue("@Id_param", SqlDbType.VarChar).Value = user.Id;
                sqlcmd.Parameters.AddWithValue("@NickName_param", SqlDbType.VarChar).Value = user.NickName;
                sqlcmd.Parameters.AddWithValue("@Email_param", SqlDbType.VarChar).Value = user.Email;
                sqlcmd.Parameters.AddWithValue("@Password_param", SqlDbType.VarChar).Value = user.Password;
                sqlcmd.Parameters.AddWithValue("@UpdatedDate_param", SqlDbType.VarChar).Value = user.UpdatedDate;

                sqlcmd.ExecuteNonQuery();
                isEnabled = true;
            }
            finally
            {
                if (sqlConn != null)
                    sqlConn.Close();
                if (sqlDr != null)
                    sqlDr.Close();
            }
            return isEnabled;
        }

        public List<UserDTO> List()
        {
            SqlConnection sqlConn = null;
            SqlDataReader sqlDr = null;

            List<UserDTO> listUsers = new List<UserDTO>();
            try
            {
                sqlConn = new SqlConnection("Server=(local); DataBase=NCapas;Integrated Security=SSPI");

                using (SqlConnection conn = sqlConn)
                {
                    string spName = @"[dbo].[ListUsers]";

                    SqlCommand cmd = new SqlCommand(spName, conn);
                    conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();

                  
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            UserDTO user = new UserDTO();
                            user.Id = dr.GetInt32(0);
                            user.NickName = dr.GetString(1);
                            user.Email = dr.GetString(2);
                            user.Password = dr.GetString(3);
                            user.IsEnabled = dr.GetBoolean(4);
                            user.CreatedDate = dr.GetDateTime(5);
                            user.UpdatedDate = dr.IsDBNull(6) ? (DateTime?)null :  dr.GetDateTime(6);

                            listUsers.Add(user);
                        }
                    }
                    
                    dr.Close();
                    conn.Close();
                }
            }
            finally
            {
                if (sqlConn != null)
                    sqlConn.Close();
                if (sqlDr != null)
                    sqlDr.Close();
            }

            return listUsers;
        }

        public UserDTO Get(int id)
        {
            SqlConnection sqlConn = null;
            SqlDataReader sqlDr = null;

            UserDTO user = new UserDTO();
            try
            {
                sqlConn = new SqlConnection("Server=(local); DataBase=NCapas;Integrated Security=SSPI");

                using (SqlConnection conn = sqlConn)
                {
                    string spName = @"[dbo].[GetUserById]";

                    SqlCommand cmd = new SqlCommand(spName, conn);

                    //Agregar parametros
                    SqlParameter id_param = new SqlParameter();
                    id_param.ParameterName = "@Id_param";
                    id_param.SqlDbType = SqlDbType.Int;
                    id_param.Value = id;
                    cmd.Parameters.Add(id_param);

                    conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            user.Id = dr.GetInt32(0);
                            user.NickName = dr.GetString(1);
                            user.Email = dr.GetString(2);
                            user.Password = dr.GetString(3);
                            user.IsEnabled = dr.GetBoolean(4);
                            user.CreatedDate = dr.GetDateTime(5);
                            user.UpdatedDate = dr.IsDBNull(6) ? (DateTime?)null : dr.GetDateTime(6);
                        }
                    }

                    dr.Close();
                    conn.Close();
                }
            }
            finally
            {
                if (sqlConn != null)
                    sqlConn.Close();
                if (sqlDr != null)
                    sqlDr.Close();
            }

            return user;
        }

        public bool Delete(int id)
        {
            SqlConnection sqlConn = null;
            SqlDataReader sqlDr = null;
            bool isDeleted = false;
            try
            {
                sqlConn = new SqlConnection("Server=(local); DataBase=NCapas;Integrated Security=SSPI");
                sqlConn.Open();

                SqlCommand sqlcmd = new SqlCommand("DeleteUser", sqlConn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("@Id_param", SqlDbType.VarChar).Value = id;
                sqlcmd.Parameters.AddWithValue("@UpdatedDate_param", SqlDbType.VarChar).Value = DateTime.Now;

                sqlcmd.ExecuteNonQuery();
                isDeleted = true;
            }
            finally
            {
                if (sqlConn != null)
                    sqlConn.Close();
                if (sqlDr != null)
                    sqlDr.Close();
            }
            return isDeleted;
        }
    }
}
