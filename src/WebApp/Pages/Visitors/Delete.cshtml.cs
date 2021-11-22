using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Users;
using Application.Visitors.Commands.DeleteVisitor;
using Core.Entities;
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
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> _logger;
        private readonly IMediator _mediator;
        private readonly IAppDbContext _context;

        public DeleteModel(ILogger<DeleteModel> logger, IMediator mediator, IAppDbContext context)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
        }

        [BindProperty]
        public VisitorEntry Vstr { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Vstr = await _context.VisitorEntries.Where(v => v.Id == id).FirstOrDefaultAsync();

            if (Vstr == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            List<string> errs = await _mediator.Send(new DeleteVisitorCommand() { Id = Vstr.Id });

            if (errs.Count == 0)
            {
                _logger.LogInformation("Visitor entry deleted successfully");
                return RedirectToPage($"./{nameof(Index)}").WithSuccess("Visitor entry deletion done");
            }

            foreach (var error in errs)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return Page();
        }
    }
}
