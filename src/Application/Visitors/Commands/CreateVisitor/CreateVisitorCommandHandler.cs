using Application.Common.Interfaces;
using AutoMapper;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Visitors.Commands.CreateVisitor
{
    public class CreateVisitorCommandHandler : IRequestHandler<CreateVisitorCommand, int>
    {
        private readonly ILogger<CreateVisitorCommandHandler> _logger;
        private readonly IMapper _mapper;
        public readonly string _imageFolderPath;
        private readonly IAppDbContext _context;

        public CreateVisitorCommandHandler(ILogger<CreateVisitorCommandHandler> logger, IMapper mapper, IConfiguration configuration, IAppDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _imageFolderPath = configuration["ImagesFolder"];
            _context = context;
        }

        public async Task<int> Handle(CreateVisitorCommand request, CancellationToken cancellationToken)
        {
            VisitorEntry visitor = _mapper.Map<VisitorEntry>(request);

            // save image uri as a file
            var base64Data = Regex.Match(request.ImageUri, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            string fileName = "Capture_" + milliseconds.ToString() + ".jpeg";
            string filePath = Path.Combine(_imageFolderPath, fileName);
            if (!File.Exists(filePath))
            {
                File.WriteAllBytes(filePath, binData);
                _logger.LogInformation($"Created image file {filePath}");
            }
            visitor.ImageFilename = fileName;

            // save the visitor object
            _context.VisitorEntries.Add(visitor);
            _ = await _context.SaveChangesAsync(cancellationToken);

            return visitor.Id;
        }
    }
}
