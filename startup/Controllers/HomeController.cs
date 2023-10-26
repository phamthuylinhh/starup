using Microsoft.AspNetCore.Mvc;
using startup.Models;
using System.Diagnostics;

namespace startup.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/post-{slug}-{id:long}.html", Name = "Default")]
        public IActionResult Default(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _context.Posts
                .Where(m => (m.PostID == id) && (m.IsActive == true)).FirstOrDefault();
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        [Route("/list-{slug}-{id:long}.html", Name = "List")]
        public IActionResult List(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var List = _context.Post_Menus
                .Where(m => (m.PostID == id) && (m.IsActive == true)).ToList();
            if (List == null)
            {
                return NotFound();
            }
            return View(List);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}