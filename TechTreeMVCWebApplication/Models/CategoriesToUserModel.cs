namespace TechTreeMVCWebApplication.Models
{
    using TechTreeMVCWebApplication.Entities;

    public class CategoriesToUserModel
    {
        public string UserId { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Category>CategoriesSelected{ get; set; }
    }
}
