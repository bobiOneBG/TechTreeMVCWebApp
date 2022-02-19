namespace TechTreeMVCWebApplication.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Content
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "HTML Content")]
        public string HTMLContent { get; set; }

        [Display(Name = "Video Link")]
        public string VideoLink { get; set; }

        public CategoryItem CategoryItem { get; set; }

        [NotMapped]
        //Note: This property cannot be named CategoryItemId because this would interfere with future migrations
        //   It has been named like this so as not to conflict with EF Core naming conventions
        public int CatItemId { get; set; }

        [NotMapped]
        public int CategoryId { get; set; }
    }
}
