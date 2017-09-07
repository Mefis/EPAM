namespace OhSnapDAL.Managers
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class UserManager
    {
        public static void CreateUser(User user)
        {
            DBConnectionSettings.GetFactorySettingsFromConfig();

            using (var connection = DBConnectionSettings.factory.CreateConnection())
            {
                connection.ConnectionString = DBConnectionSettings.connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "insert into Users (UserLogin, UserPassword, Email, FirstName, LastName, RoleID, Country, City, CreationDate) " 
                    + "values (@userLogin, @userPassword, @email, @firstName, @lastName, @roleID, @country, @city, getdate());";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@userLogin";
                parameter.Value = user.UserLogin;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@userPassword";
                parameter.Value = OhSnapDAL.Managers.AccountManager.GetHash(user.UserPassword);
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@email";
                parameter.Value = user.Email;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@firstName";
                parameter.Value = user.FirstName;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@lastName";
                parameter.Value = user.LastName;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@roleID";
                parameter.Value = user.RoleID == default(int) ? 2 : user.RoleID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@country";
                parameter.Value = user.Country ?? (object)DBNull.Value;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@city";
                parameter.Value = user.City ?? (object)DBNull.Value;
                command.Parameters.Add(parameter);

                command.ExecuteScalar();
                connection.Close();
            }
        }

        public static List<User> GetFullUserListFromDB()
        {
            DBConnectionSettings.GetFactorySettingsFromConfig();

            var userList = new List<User>();

            using (var connection = DBConnectionSettings.factory.CreateConnection())
            {
                connection.ConnectionString = DBConnectionSettings.connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select * from Users";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User();

                        user.UserID = (int)reader["UserID"];
                        user.UserLogin = reader["UserLogin"].ToString();
                        user.UserPasswordHash = (byte[])reader["UserPassword"];
                        user.Email = reader["Email"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.RoleID = (int)reader["RoleID"];
                        user.Country = (reader["Country"] ?? string.Empty).ToString();
                        user.City = (reader["City"] ?? string.Empty).ToString();
                        user.CreationDate = (DateTime)reader["CreationDate"];

                        userList.Add(user);
                    }
                }

                connection.Close();
            }

            return userList;
        }

        public static User GetUserFromDB(int userID)
        {
            DBConnectionSettings.GetFactorySettingsFromConfig();

            var user = new User();

            using (var connection = DBConnectionSettings.factory.CreateConnection())
            {
                connection.ConnectionString = DBConnectionSettings.connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select * from Users where UserID = @userID;";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@userID";
                parameter.Value = userID;
                command.Parameters.Add(parameter);

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.UserID = userID;
                        user.UserLogin = reader["UserLogin"].ToString();
                        user.UserPasswordHash = (byte[])reader["UserPassword"];
                        user.Email = reader["Email"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.RoleID = (int)reader["RoleID"];
                        user.Country = (reader["Country"] ?? string.Empty).ToString();
                        user.City = (reader["City"] ?? string.Empty).ToString();
                        user.CreationDate = (DateTime)reader["CreationDate"];
                    }
                }

                connection.Close();
            }

            return user;
        }

        public static User GetUserFromDB(string userLogin)
        {
            DBConnectionSettings.GetFactorySettingsFromConfig();

            var user = new User();

            using (var connection = DBConnectionSettings.factory.CreateConnection())
            {
                connection.ConnectionString = DBConnectionSettings.connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select * from Users where UserLogin = @userLogin;";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@userLogin";
                parameter.Value = userLogin;
                command.Parameters.Add(parameter);

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.UserID = (int)reader["UserID"];
                        user.UserLogin = userLogin;
                        user.UserPasswordHash = (byte[])reader["UserPassword"];
                        user.Email = reader["Email"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.RoleID = (int)reader["RoleID"];
                        user.Country = (reader["Country"] ?? string.Empty).ToString();
                        user.City = (reader["City"] ?? string.Empty).ToString();
                        user.CreationDate = (DateTime)reader["CreationDate"];
                    }
                }

                connection.Close();
            }

            return user;
        }

        public static void EditUser(User user)
        {
            DBConnectionSettings.GetFactorySettingsFromConfig();

            using (var connection = DBConnectionSettings.factory.CreateConnection())
            {
                connection.ConnectionString = DBConnectionSettings.connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "update Users set UserLogin = @userLogin, UserPassword = @userPassword, Email = @email, "
                    + "FirstName = @firstName, LastName = @lastName, RoleID = @roleID, Country = @country, City = @city "
                    + "where UserID = @userID;";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@userID";
                parameter.Value = user.UserID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@userLogin";
                parameter.Value = user.UserLogin;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@userPassword";
                parameter.Value = OhSnapDAL.Managers.AccountManager.GetHash(user.UserPassword);
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@email";
                parameter.Value = user.Email;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@firstName";
                parameter.Value = user.FirstName;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@lastName";
                parameter.Value = user.LastName;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@roleID";
                parameter.Value = user.RoleID;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@country";
                parameter.Value = user.Country ?? (object)DBNull.Value;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@city";
                parameter.Value = user.City ?? (object)DBNull.Value;
                command.Parameters.Add(parameter);

                command.ExecuteScalar();
                connection.Close();
            }
        }

        public static void DeleteUserFromDB(int userID)
        {
            DBConnectionSettings.GetFactorySettingsFromConfig();

            using (var connection = DBConnectionSettings.factory.CreateConnection())
            {
                connection.ConnectionString = DBConnectionSettings.connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = string.Format("delete from Users where UserID = @userID;");

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@userID";
                parameter.Value = userID;
                command.Parameters.Add(parameter);

                command.ExecuteScalar();
                connection.Close();
            }
        }
    }
}
