using Fieldr.Application.Common.Exceptions;
using Fieldr.Application.Common.Interfaces;
using Fieldr.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fieldr.Application.FieldRecords.Commands.DeleteFieldRecord
{
    public class DeleteFieldRecordCommand : IRequest
    {
        public long Id { get; set; }

        public class DeleteFieldRecordCommandHanlder : IRequestHandler<DeleteFieldRecordCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteFieldRecordCommandHanlder(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteFieldRecordCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.FieldRecords.FindAsync(request.Id);

                if(entity == null)
                {
                    throw new NotFoundException(nameof(FieldRecord), request.Id);
                }

                _context.FieldRecords.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
