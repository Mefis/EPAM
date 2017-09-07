namespace OhSnapDAL
{
    using System;
    using System.IO;

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Enter 'load' to load pictures in DataBase.");
            var imputString = Console.ReadLine();
            if (imputString == "load")
            {
                for (int i = 0; i < 5; i++)
                {
                    var folder = "Images\\";
                    var fileName = string.Format("{0}.png", i + 1);
                    var fileType = "image/png";
                    string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, folder, fileName);
                    byte[] byteArray = File.ReadAllBytes(startupPath);
                    OhSnapDAL.Managers.PhotoManager.SavePhotoToDB(fileName, byteArray, fileType, 1);
                }
                for (int i = 5; i < 10; i++)
                {
                    var folder = "Images\\";
                    var fileName = string.Format("{0}.jpg", i + 1);
                    var fileType = "image/jpeg";
                    string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, folder, fileName);
                    byte[] byteArray = File.ReadAllBytes(startupPath);
                    OhSnapDAL.Managers.PhotoManager.SavePhotoToDB(fileName, byteArray, fileType, 2);
                }
                for (int i = 10; i < 12; i++)
                {
                    var folder = "Images\\";
                    var fileName = string.Format("{0}.gif", i + 1);
                    var fileType = "image/gif";
                    string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, folder, fileName);
                    byte[] byteArray = File.ReadAllBytes(startupPath);
                    OhSnapDAL.Managers.PhotoManager.SavePhotoToDB(fileName, byteArray, fileType, 3);
                }
                Console.WriteLine("Load successful.");
            }
            Console.WriteLine("Exiting. Press enter.");
            Console.ReadKey();
        }
    }
}
