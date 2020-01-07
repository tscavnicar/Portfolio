using Fieldr.Application.Common.Exceptions;
using Fieldr.Application.Common.Interfaces;
using Fieldr.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fieldr.Application.FieldRecords.Commands.UpdateFieldRecord
{
    public class UpdateFieldRecordCommand : IRequest
    {
        public long Id { get; set; }
        public string Note { get; set; }

        public class UpdateFieldRecordCommandHandler : IRequestHandler<UpdateFieldRecordCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateFieldRecordCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateFieldRecordCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.FieldRecords.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(FieldRecord), request.Id);
                }

                entity.Note = request.Note;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }

        }
    }
}
