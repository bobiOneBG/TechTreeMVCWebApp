namespace TechTreeMVCWebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TechTreeMVCWebApplication.Data;
    using TechTreeMVCWebApplication.Entities;

    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index(int categoryItemId)
        {
            var content = await (from item in _context.Content
                                 where item.CategoryItem.Id == categoryItemId
                                 select new Content
                                 {
                                     Title= item.Title,
                                     VideoLink= item.VideoLink,
                                     HTMLContent= item.HTMLContent,
                                 })
                                 .FirstOrDefaultAsync();

            return View(content);
        }
    }
}
