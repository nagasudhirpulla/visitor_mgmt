using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Visitors.Commands.CreateVisitor
{
    public class CreateVisitorCommandHandler : IRequestHandler<CreateVisitorCommand, List<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<CreateVisitorCommandHandler> _logger;

        public CreateVisitorCommandHandler(UserManager<ApplicationUser> userManager, IEmailSender emailSender, ILogger<CreateVisitorCommandHandler> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task<List<string>> Handle(CreateVisitorCommand request, CancellationToken cancellationToken)
        {
            // TODO implement this
            return await Task.FromResult(new List<string>());
        }
    }
}
