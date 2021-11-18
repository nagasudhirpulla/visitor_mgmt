using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApp.Pages.Visitors
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;
        public readonly string _imageFolderPath;
        private readonly IAppDbContext _context;

        public VisitorEntry VisitorEntry { get; set; }
        public string ImgDataUri { get; set; }
        public DetailsModel(ILogger<DetailsModel> logger, IConfiguration configuration, IAppDbContext context)
        {
            _logger = logger;
            _imageFolderPath = configuration["ImagesFolder"];
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            VisitorEntry = await _context.VisitorEntries.Where(m => m.Id == id)
                                        .FirstOrDefaultAsync();
            ImgDataUri = "data:image/"
                        + Path.GetExtension(VisitorEntry.ImageFilename).Replace(".", "")
                        + ";base64,"
                        + Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(_imageFolderPath, VisitorEntry.ImageFilename)));
            return Page();
        }
    }
}
