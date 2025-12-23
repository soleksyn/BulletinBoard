namespace BulletinBoard.Data.DTOs
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public decimal? Price { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
    }
}
