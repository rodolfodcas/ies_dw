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
    public class BODA
    {
        public int Create(BODTO user)
        {
            SqlConnection sqlConn = null;
            SqlDataReader sqlDr = null;
            int id = 0;
            try
            {
                sqlConn = new SqlConnection("Server=(local); DataBase=NCapas;Integrated Security=SSPI");
                sqlConn.Open();

                SqlCommand sqlcmd = new SqlCommand("CreateSucursal", sqlConn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //Variables de entrada
                sqlcmd.Parameters.AddWithValue("@Name_param", SqlDbType.VarChar).Value = user.Name;
                sqlcmd.Parameters.AddWithValue("@Address_param", SqlDbType.VarChar).Value = user.Address;
                sqlcmd.Parameters.AddWithValue("@City_param", SqlDbType.VarChar).Value = user.City;
                sqlcmd.Parameters.AddWithValue("@State_param", SqlDbType.VarChar).Value = user.State;
                sqlcmd.Parameters.AddWithValue("@Country_param", SqlDbType.VarChar).Value = user.Country;
                sqlcmd.Parameters.AddWithValue("@ZIP_param", SqlDbType.VarChar).Value = user.ZIP;
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

        public bool Update(BODTO user)
        {
            SqlConnection sqlConn = null;
            SqlDataReader sqlDr = null;
            bool isEnabled = false;
            try
            {
                sqlConn = new SqlConnection("Server=(local); DataBase=NCapas;Integrated Security=SSPI");
                sqlConn.Open();

                SqlCommand sqlcmd = new SqlCommand("UpdateBO", sqlConn);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //Variables de entrada
                sqlcmd.Parameters.AddWithValue("@Id_param", SqlDbType.VarChar).Value = user.Id;
                sqlcmd.Parameters.AddWithValue("@Name_param", SqlDbType.VarChar).Value = user.Name;
                sqlcmd.Parameters.AddWithValue("@Address_param", SqlDbType.VarChar).Value = user.Address;
                sqlcmd.Parameters.AddWithValue("@City_param", SqlDbType.VarChar).Value = user.City;
                sqlcmd.Parameters.AddWithValue("@State_param", SqlDbType.VarChar).Value = user.State;
                sqlcmd.Parameters.AddWithValue("@Country_param", SqlDbType.VarChar).Value = user.Country;
                sqlcmd.Parameters.AddWithValue("@ZIP_param", SqlDbType.VarChar).Value = user.ZIP;
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

        public List<BODTO> List()
        {
            SqlConnection sqlConn = null;
            SqlDataReader sqlDr = null;

            List<BODTO> listUsers = new List<BODTO>();
            try
            {
                sqlConn = new SqlConnection("Server=(local); DataBase=NCapas;Integrated Security=SSPI");

                using (SqlConnection conn = sqlConn)
                {
                    string spName = @"[dbo].[ListUsersBO]";

                    SqlCommand cmd = new SqlCommand(spName, conn);
                    conn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            BODTO user = new BODTO();
                            user.Id = dr.GetInt32(0);
                            user.Name = dr.GetString(1);
                            user.Address = dr.GetString(2);
                            user.City = dr.GetString(3);
                            user.State = dr.GetString(4);
                            user.Country = dr.GetString(5);
                            user.ZIP = dr.GetString(6);
                            user.IsEnabled = dr.GetBoolean(7);
                            user.CreatedDate = dr.GetDateTime(8);
                            user.UpdatedDate = dr.IsDBNull(9) ? (DateTime?)null : dr.GetDateTime(9);

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

        public BODTO Get(int id)
        {
            SqlConnection sqlConn = null;
            SqlDataReader sqlDr = null;

            BODTO user = new BODTO();
            try
            {
                sqlConn = new SqlConnection("Server=(local); DataBase=NCapas;Integrated Security=SSPI");

                using (SqlConnection conn = sqlConn)
                {
                    string spName = @"[dbo].[GetUserByIdBO]";

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
                            user.Name = dr.GetString(1);
                            user.Address = dr.GetString(2);
                            user.City = dr.GetString(3);
                            user.State = dr.GetString(4);
                            user.Country = dr.GetString(5);
                            user.ZIP = dr.GetString(6);
                            user.IsEnabled = dr.GetBoolean(7);
                            user.CreatedDate = dr.GetDateTime(8);
                            user.UpdatedDate = dr.IsDBNull(9) ? (DateTime?)null : dr.GetDateTime(9);

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

                SqlCommand sqlcmd = new SqlCommand("DeleteBO", sqlConn);
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
