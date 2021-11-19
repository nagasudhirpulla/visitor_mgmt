using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Utils.Commands.ImgToDataUri;
using Core.Entities;
using MediatR;
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
        private readonly IMediator _mediator;
        private readonly IAppDbContext _context;

        public VisitorEntry VisitorEntry { get; set; }
        public string ImgDataUri { get; set; }
        public DetailsModel(ILogger<DetailsModel> logger, IConfiguration configuration, IAppDbContext context, IMediator mediator)
        {
            _logger = logger;
            _imageFolderPath = configuration["ImagesFolder"];
            _mediator = mediator;
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
            if (VisitorEntry == null)
            {
                return NotFound();
            }

            ImgDataUri = await _mediator.Send(new ImgToDataUriCommand() { ImageFilename = VisitorEntry.ImageFilename });
            return Page();
        }
    }
}
