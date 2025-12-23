namespace BulletinBoard.Web.Models
{
    public class CategoryApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SubCategoryApiModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
