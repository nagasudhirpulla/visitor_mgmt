using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Users;
using Application.Visitors.Commands.EditVisitor;
using AutoMapper;
using Core.Entities;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Extensions;

namespace WebApp.Pages.Visitors
{
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAppDbContext _context;

        public EditModel(ILogger<EditModel> logger, IMediator mediator, IMapper mapper, IAppDbContext context)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
        }

        [BindProperty]
        public EditVisitorCommand VstrCmd { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VisitorEntry vstr = await _context.VisitorEntries.Where(v => v.Id == id).FirstOrDefaultAsync();
            if (vstr == null)
            {
                return NotFound();
            }

            VstrCmd = _mapper.Map<EditVisitorCommand>(vstr);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ValidationResult validationCheck = new EditVisitorCommandValidator().Validate(VstrCmd);
            validationCheck.AddToModelState(ModelState, nameof(VstrCmd));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            List<string> errors = await _mediator.Send(VstrCmd);

            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            // check if we have any errors and redirect if successful
            if (errors.Count == 0)
            {
                _logger.LogInformation("User edit operation successful");
                return RedirectToPage($"./{nameof(Index)}").WithSuccess("Visitor Entry Editing done");
            }

            return Page();
        }
    }
}
