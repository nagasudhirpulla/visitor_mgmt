using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Visitors.Commands.DeleteVisitor
{
    public class DeleteVisitorCommandHandler : IRequestHandler<DeleteVisitorCommand, List<string>>
    {
        private readonly IAppDbContext _context;
        public readonly string _imageFolderPath;
        private readonly ILogger<DeleteVisitorCommandHandler> _logger;

        public DeleteVisitorCommandHandler(IAppDbContext context, ILogger<DeleteVisitorCommandHandler> logger, IConfiguration configuration)
        {
            _context = context;
            _imageFolderPath = configuration["ImagesFolder"];
            _logger = logger;
        }

        public async Task<List<string>> Handle(DeleteVisitorCommand request, CancellationToken cancellationToken)
        {
            var vstrEntry = await _context.VisitorEntries.Where(v => v.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (vstrEntry == null)
            {
                string errorMsg = $"visitor entry Id {request.Id} not present for deletion";
                return new List<string>() { errorMsg };
            }

            _context.VisitorEntries.Remove(vstrEntry);
            await _context.SaveChangesAsync(cancellationToken);

            // delete the file from file system
            string imgPath = Path.Combine(_imageFolderPath, vstrEntry.ImageFilename);
            if (File.Exists(imgPath))
            {
                File.Delete(imgPath);
                _logger.LogInformation($"Removed image file {imgPath}");
            }

            return new List<string>();
        }
    }
}
