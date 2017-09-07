namespace OhSnapDAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Photo
    {
        public Photo(int photoID, string photoName, byte[] byteArray, string fileType, int userID, DateTime uploadDate, string likes, int likesCount)
        {
            this.PhotoID = photoID;
            this.PhotoName = photoName;
            this.ByteArray = byteArray;
            this.FileType = fileType;
            this.UserID = userID;
            this.UploadDate = uploadDate;
            this.Likes = likes;
            this.LikesCount = likesCount;
        }

        [Required]
        public int PhotoID { get; }

        [Required]
        public string PhotoName { get; }

        [Required]
        public byte[] ByteArray { get; }

        [Required]
        public string FileType { get; }

        [Required]
        public int UserID { get; }

        [Required]
        public DateTime UploadDate { get; }

        public string Likes { get; }

        public int LikesCount { get; }
    }
}
