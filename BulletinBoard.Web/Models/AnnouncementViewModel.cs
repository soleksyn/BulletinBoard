using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulletinBoard.Web.Models
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Date Created")]
        public DateTime? CreatedDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Subcategory is required")]
        [Display(Name = "Subcategory")]
        public int? SubCategoryId { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> SubCategories { get; set; }
    }

    public class AnnouncementListViewModel
    {
        public IEnumerable<AnnouncementViewModel> Announcements { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int? SelectedSubCategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> SubCategories { get; set; }
    }
}
