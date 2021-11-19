using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Utils.Commands.ImgToDataUri;
using Application.Visitors.Queries.GetVisitorsByDates;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.Pages.Visitors
{
    public class IndexModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [BindProperty]
        public GetVisitorsByDatesQuery VQuery { get; set; }
        public List<VisitorEntry> VisitorsList { get; set; }

        public async Task OnGetAsync()
        {
            DateTime defDate = DateTime.Now.Date;
            VQuery = new() { StartDate = defDate, EndDate = defDate };
            VisitorsList = await _mediator.Send(VQuery);
            await PopulateImageDataUris();
        }

        public async Task OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            VisitorsList = await _mediator.Send(VQuery);
            await PopulateImageDataUris();
        }

        public async Task PopulateImageDataUris()
        {
            // convert image path to image uri for the sake of display in img element
            for (int i = 0; i < VisitorsList.Count; i++)
            {
                string imgFname = VisitorsList[i].ImageFilename;
                var imgDataUri = await _mediator.Send(new ImgToDataUriCommand() { ImageFilename = imgFname });
                VisitorsList[i].ImageFilename = imgDataUri;
            }
        }
    }
}
