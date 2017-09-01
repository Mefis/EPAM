namespace HMT_13.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EditOrCreateOrderItemModel
    {
        [Required]
        public string OrderID { get; set; }

        [Required]
        public string ProductID { get; set; }

        [Required]
        public string UnitPrice { get; set; }

        [Required]
        public string Quantity { get; set; }

        [Required]
        public string Discount { get; set; }
    }
}