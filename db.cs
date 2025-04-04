
using Microsoft.Data.SqlClient;

public class Database
{

    public string ConnectionString;

    public Database(string connectionString)
    {
        ConnectionString = connectionString;
    }


    public void connect()
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection Succesful!");

                string query = "SELECT * FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Replace 'ColumnName' with the actual column name from your 'Products' table
                        Console.WriteLine(reader["name"].ToString());
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
        }
    }    

    public void insert_user(int id,string username, byte[] password)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection Succesful!");

                string query = "INSERT INTO Users (user_id, username, password) VALUES (@user_id, @username, @password)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", id);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
        }
    }

    public bool check_pass(string username, byte[] password)
    {
        Console.WriteLine(username, password);

        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            try
            {
                con.Open();
                Console.WriteLine("Connection Succesfull");

                string query = "SELECT password FROM users WHERE username = @username";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@username", username);
                    SqlDataReader r = com.ExecuteReader();
                    while (r.Read())
                    {
                        byte[] storedpassword = (byte[])r["password"];
                        if (storedpassword.SequenceEqual(password))
                        {
                            Console.WriteLine("Password is correct!");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Password is invalid!");
                            return false;
                            
                        }
                    }
                    r.Close();
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                return false;
            }
            
        }
    }

    public bool check_user(string username)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            try
            {
                con.Open();
                Console.WriteLine("Connection successfull!");

                string query = "SELECT username FROM users WHERE username = @username";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@username", username);
                    SqlDataReader r = com.ExecuteReader();
                    while (r.Read())
                    {
                        if (username == r["username"].ToString())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                }
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
                return false;
            }
        }
    }

    public int? max_id(string table)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            try
            {
                con.Open();
                Console.WriteLine("Connection successfull!");

                string query = $"SELECT MAX(user_id) AS MAXId FROM {table}";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    SqlDataReader r = com.ExecuteReader();
                    while (r.Read())
                    {
                        return Convert.ToInt32(r["MAXId"]);
                    } 

                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
                return null;
            }
        }
    }

    public int? max_id_pass(string table)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            try
            {
                con.Open();
                Console.WriteLine("Connection successfull!");

                string query = $"SELECT MAX(password_id) AS MAXId FROM {table}";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    SqlDataReader r = com.ExecuteReader();
                    while (r.Read())
                    {
                        return Convert.ToInt32(r["MAXId"]);
                    } 

                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
                return null;
            }
        }
    }

    public void insert_pass(int password_id, string page, string password, string user)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection Succesful!");

                string query = "INSERT INTO Passwords (password_id, page, password, [user]) VALUES (@password_id, @page, @password, @user)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@password_id", password_id);
                    command.Parameters.AddWithValue("@page", page);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@user", user);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
        }
    
    }

    public void get_passwords(string username, string secret)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection successful!");

                string query = "SELECT page, password FROM passwords WHERE [user] = @user";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string page = reader["page"].ToString();
                            string password = reader["password"].ToString();
                            password = EncryptionHelper.DecryptString(password, secret);

                            Console.WriteLine($"Page: {page} | Password: {password}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
    

    



