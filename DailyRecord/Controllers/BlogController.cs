using DailyRecord.Data.Models;
using DailyRecord.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DailyRecord.Controllers
{
    [Authorize(Policy = "MustBelongToAdminDepartment")]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepo;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepo = blogRepository;
        }
        public IActionResult Index()
        {
            Blog model = new()
            {
                Entry = "test",
                DatePublish = DateTime.Now
            };

            _blogRepo.AddEntry(model);

            return View();
        }
    }
}
