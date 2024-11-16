using Microsoft.AspNetCore.Mvc;
using PROG6212.PART2.Models;
using System.IO;
using System.Threading.Tasks;

namespace PROG6212.PART2.Controllers
{
    public class UploadDocumentsController : Controller
    {
        [HttpGet]
        public IActionResult Index1()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index1(UploadDocuments model)
        {
            if (ModelState.IsValid)
            {
                
                if (model.Receipt != null)
                    await SaveFile(model.Receipt);
                if (model.Invoice != null)
                    await SaveFile(model.Invoice);
                if (model.Timesheet != null)
                    await SaveFile(model.Timesheet);
                if (model.ApprovalLetter != null)
                    await SaveFile(model.ApprovalLetter);
                if (model.Other != null)
                    await SaveFile(model.Other);

                // Redirect or display a success message
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private async Task SaveFile(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var filePath = Path.Combine(uploadsFolder, file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
