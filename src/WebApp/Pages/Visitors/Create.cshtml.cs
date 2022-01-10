using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Visitors.Commands.CreateVisitor;
using Application.Visitors.Queries.GetVisitorById;
using AutoMapper;
using Core.Entities;
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
        private readonly IMapper _mapper;

        public CreateModel(ILogger<IndexModel> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }


        [BindProperty]
        public CreateVisitorCommand NewVisitor { get; set; }

        public async Task<IActionResult> OnGet(int? reuseId)
        {
            if (!reuseId.HasValue)
            {
                NewVisitor = new();
            }
            else
            {
                // reuse existing visitor details to create a new visitor form
                VisitorEntry existingVisitor = await _mediator.Send(new GetVisitorByIdQuery() { Id = reuseId.Value });
                NewVisitor = _mapper.Map<CreateVisitorCommand>(existingVisitor);
            }
            // change In Time and Out Time
            NewVisitor.InTime = DateTime.Now;
            NewVisitor.OutTime = null;
            return Page();
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