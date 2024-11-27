using FluentValidation;
using MediatR;
using MyChronicle.Application.Relations;
using MyChronicle.Domain;
using MyChronicle.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChronicle.Application.Files
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public required Guid Id { get; set; }
            public required MyChronicle.Domain.File File { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.File).SetValidator(new FileValidator());
            }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var file = await _context.Files.FindAsync(request.File.Id);
                if (file == null) return null;

                if (file.Id != request.Id)
                {
                    //return Result<Unit>.Failure("No file with the given id was found");
                    return Result<Unit>.Failure($"Not matching Id. Request Id was {request.Id}. File id was {file.Id}");
                }

                file.Content = request.File.Content;
                file.FileType = request.File.FileType;
                file.FileExtension = request.File.FileExtension;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update File");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
