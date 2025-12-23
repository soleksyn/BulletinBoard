namespace BulletinBoard.Core
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SubCategoryModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
