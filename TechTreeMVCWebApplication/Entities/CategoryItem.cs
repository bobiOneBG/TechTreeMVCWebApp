namespace TechTreeMVCWebApplication.Entities
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CategoryItem
    {
        private DateTime _releaseDate = DateTime.MinValue;

        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        
        public string Description { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Media type")]
        public int MediaTypeId { get; set; }

        [NotMapped]
        public ICollection<SelectListItem> MediaTypes { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}"/*, ApplyFormatInEditMode =true)*/)]
        [Display(Name = "Release Date")]
        public  DateTime DateTimeItemReleased
        {
            get
            {
                return (_releaseDate == DateTime.MinValue) ? DateTime.Now : _releaseDate;
            }
            set
            {
                _releaseDate = value;
            }

        }

        [NotMapped]
        public int ContentId { get; set; }
    }
}
