using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Visitors.Commands.CreateVisitor;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.Pages.Visitors
{
    public class CreateModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public CreateModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [BindProperty]
        public CreateVisitorCommand NewVisitor { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return await Task.FromResult(Page());
        }
    }
}
