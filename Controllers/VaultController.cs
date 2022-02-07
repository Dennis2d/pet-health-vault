using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SemesterHealtVaultProject.Data;
using SemesterHealtVaultProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SemesterHealtVaultProject.Controllers
{
    public class VaultController : Controller
    {
        public ApplicationDbContext _db;

        public VaultController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _db.Users.FirstOrDefault(x => x.Id == userId);
            ViewBag.Documents = _db.healthDocuments.Where(x => x.User == user);
            return View();
        }

        public IActionResult Document(int id)
        {
            // Find the document with id = id, out it in the ViewBag, and display all details in the view
            // If id not fond add en error in ViewBag and display the error in View instead
            return View();
        }

        [Authorize]
        public IActionResult Upload()
        {
            return View();

        }

        public IActionResult SaveData(String title, String description, Microsoft.AspNetCore.Http.IFormFile attachment)
        {
            ViewBag.Title = title;
            ViewBag.Description = description;
            ViewBag.Filename = System.IO.Path.GetFileName(attachment.FileName);

            var extension = System.IO.Path.GetExtension(ViewBag.Filename);
            var randomName = Convert.ToString(Guid.NewGuid());

            ViewBag.SavedName = randomName + extension;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool exists = Directory.Exists(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", userId)); 
            if(!exists)
            {
                Directory.CreateDirectory(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", userId));
            }

            var path = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", userId)).Root + ViewBag.SavedName;
            FileStream fs = System.IO.File.Create(path);
            attachment.CopyTo(fs);
            fs.Flush();

            HealthDocument doc = new HealthDocument();
            doc.Title = title;
            doc.Description = description;
            doc.FileName = randomName + extension;
            doc.User = _db.Users.FirstOrDefault(x => x.Id == userId);
            doc.DateCreated = DateTime.Now;

            _db.healthDocuments.Add(doc);
            _db.SaveChanges();


            return View();
        }

    }
}
