using System;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_UploadFile.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UploadController : ControllerBase
	{
		[HttpGet]
		public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"/Users/liyang/Documents/LearnSomething/LearnSomething-asp.net/WebApi-UploadFile/test file.txt");
            string fileName = "test file.txt";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

		[HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }
    }
}

