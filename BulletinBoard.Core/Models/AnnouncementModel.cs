using System;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Core.Models
{
    public class AnnouncementModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Subcategory is required")]
        public int? SubCategoryId { get; set; }

        public decimal? Price { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
    }
}
