using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Visitors.Commands.CreateVisitor;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApp.Extensions;

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
            NewVisitor = new();
            NewVisitor.InTime = DateTime.Now;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ValidationResult validationCheck = new CreateVisitorCommandValidator().Validate(NewVisitor);
            validationCheck.AddToModelState(ModelState, nameof(NewVisitor));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            int newVisitorId = await _mediator.Send(NewVisitor);

            return RedirectToPage($"./Details", new { Id = newVisitorId });
        }
    }
}