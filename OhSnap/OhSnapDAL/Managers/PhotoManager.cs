namespace OhSnapDAL.Managers
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class PhotoManager
    {
        public static void SavePhotoToDB(string photoName, byte[] byteArray, string fileType, int userID)
        {
            DBConnectionSettings.GetFactorySettingsFromConfig();

            using (var connection = DBConnectionSettings.factory.CreateConnection())
            {
                connection.ConnectionString = DBConnectionSettings.connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "insert into Photos (PhotoName, ByteArray, FileType, UserID, UploadDate) values (@photoName, @byteArray, @fileType ,@userID, getdate());";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@photoName";
                parameter.Value = photoName;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@byteArray";
                parameter.Value = byteArray;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@fileType";
                parameter.Value = fileType;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@userID";
                parameter.Value = userID;
                command.Parameters.Add(parameter);

                command.ExecuteScalar();
                connection.Close();
            }
        }

        public static Photo GetPhotoFromDB(int photoID)
        {
            DBConnectionSettings.GetFactorySettingsFromConfig();

            string photoName = default(string);
            byte[] byteArray = default(byte[]);
            string fileType = default(string);
            int userID = default(int);
            DateTime uploadDate = default(DateTime);
            string likes = default(string);
            int likesCount = default(int);

            using (var connection = DBConnectionSettings.factory.CreateConnection())
            {
                connection.ConnectionString = DBConnectionSettings.connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select * from Photos where PhotoID = @photoID;";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@photoID";
                parameter.Value = photoID;
                command.Parameters.Add(parameter);

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        photoName = reader["PhotoName"].ToString();
                        byteArray = (byte[])reader["ByteArray"];
                        fileType = reader["FileType"].ToString();
                        userID = (int)reader["UserID"];
                        uploadDate = (DateTime)reader["UploadDate"];
                        likes = (reader["Likes"] ?? string.Empty).ToString();
                        likesCount = reader["LikesCount"].ToString() == string.Empty ? default(int) : (int)reader["LikesCount"];
                    }
                }

                connection.Close();
            }

            var photo = new Photo(photoID, photoName, byteArray, fileType, userID, uploadDate, likes, likesCount);

            return photo;
        }

        public static List<Photo> GetFullPhotoListFromDB()
        {
            DBConnectionSettings.GetFactorySettingsFromConfig();

            var photoList = new List<Photo>();

            using (var connection = DBConnectionSettings.factory.CreateConnection())
            {
                connection.ConnectionString = DBConnectionSettings.connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select * from Photos";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var photoID = (int)reader["PhotoID"];
                        var photoName = reader["PhotoName"].ToString();
                        var byteArray = (byte[])reader["ByteArray"];
                        var fileType = reader["FileType"].ToString();
                        var userID = (int)reader["UserID"];
                        var uploadDate = (DateTime)reader["UploadDate"];
                        var likes = (reader["Likes"] ?? string.Empty).ToString();
                        var likesCount = reader["LikesCount"].ToString() == string.Empty ? default(int) : (int)reader["LikesCount"];

                        var photo = new Photo(photoID, photoName, byteArray, fileType, userID, uploadDate, likes, likesCount);

                        photoList.Add(photo);
                    }
                }

                connection.Close();
            }

            return photoList;
        }

        public static void DeletePhotoFromDB(int photoID)
        {
            DBConnectionSettings.GetFactorySettingsFromConfig();

            using (var connection = DBConnectionSettings.factory.CreateConnection())
            {
                connection.ConnectionString = DBConnectionSettings.connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = string.Format("delete from Photos where PhotoID = @photoID;");

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@photoID";
                parameter.Value = photoID;
                command.Parameters.Add(parameter);

                command.ExecuteScalar();
                connection.Close();
            }
        }

        public static void LikePhoto(int photoID, int userID)
        {
            DBConnectionSettings.GetFactorySettingsFromConfig();

            var likes = default(string);
            var likesCount = default(int);

            using (var connection = DBConnectionSettings.factory.CreateConnection())
            {
                connection.ConnectionString = DBConnectionSettings.connectionString;
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select Likes, LikesCount from Photos where PhotoID = photoID";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@photoID";
                parameter.Value = photoID;
                command.Parameters.Add(parameter);

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        likes = (reader["Likes"] ?? string.Empty).ToString();
                        likesCount = reader["LikesCount"].ToString() == string.Empty ? default(int) : (int)reader["LikesCount"];
                    }
                }
                
                connection.Close();
            }

            if (!likes.Contains(string.Format(";{0};", userID)))
            {
                likes += string.Format(";{0};", userID);
                likesCount++;

                using (var connection = DBConnectionSettings.factory.CreateConnection())
                {
                    connection.ConnectionString = DBConnectionSettings.connectionString;
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = "update Photos set Likes = @likes, LikesCount = @likesCount where PhotoID = @photoID;";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@photoID";
                    parameter.Value = photoID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@likes";
                    parameter.Value = likes;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@likesCount";
                    parameter.Value = likesCount;
                    command.Parameters.Add(parameter);

                    command.ExecuteScalar();
                    connection.Close();
                }
            }
            else
            {
                likes = likes.Replace(string.Format(";{0};", userID), string.Empty);
                likesCount--;

                using (var connection = DBConnectionSettings.factory.CreateConnection())
                {
                    connection.ConnectionString = DBConnectionSettings.connectionString;
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = "update Photos set Likes = @likes, LikesCount = @likesCount where PhotoID = @photoID;";

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@photoID";
                    parameter.Value = photoID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@likes";
                    parameter.Value = likes;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@likesCount";
                    parameter.Value = likesCount;
                    command.Parameters.Add(parameter);

                    command.ExecuteScalar();
                    connection.Close();
                }
            }
        }
    }
}
