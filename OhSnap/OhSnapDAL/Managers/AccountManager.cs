namespace OhSnapDAL.Managers
{
    using System;
    using System.Data;
    using System.Text;

    public static class AccountManager
    {
        public static bool IsUserValid(string userLogin, string userPassword)
        {
            DBConnectionSettings.GetFactorySettingsFromConfig();

            var result = false;

            using (var connection = DBConnectionSettings.factory.CreateConnection())
            {
                connection.ConnectionString = DBConnectionSettings.connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select * from Users where UserLogin = @userLogin and UserPassword = @userPassword;";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@userLogin";
                parameter.Value = userLogin;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@userPassword";
                parameter.Value = GetHash(userPassword);
                command.Parameters.Add(parameter);

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = true;
                    }
                }

                connection.Close();
            }

            return result;
        }

        public static string GetRole(int roleID)
        {
            var roleName = default(string);

            DBConnectionSettings.GetFactorySettingsFromConfig();

            using (var connection = DBConnectionSettings.factory.CreateConnection())
            {
                connection.ConnectionString = DBConnectionSettings.connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select RoleName from Roles where RoleID = @roleID;";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@roleID";
                parameter.Value = roleID;
                command.Parameters.Add(parameter);

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roleName = reader["RoleName"].ToString();
                    }
                }

                connection.Close();
            }

            return roleName;
        }

        public static byte[] GetHash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new System.Security.Cryptography.SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return hashBytes;
        }
    }
}
