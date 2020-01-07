using Fieldr.Application.Common.Exceptions;
using Fieldr.Application.Common.Interfaces;
using Fieldr.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fieldr.Application.FieldLists.Commands.DeleteFieldList
{
    public class DeleteFieldListCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteFieldListCommandHandler : IRequestHandler<DeleteFieldListCommand>
        {
            private IApplicationDbContext _context;

            public DeleteFieldListCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteFieldListCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Fields
                    .Where(l => l.Id == request.Id)
                    .SingleOrDefaultAsync(cancellationToken);

                if(entity == null)
                {
                    throw new NotFoundException(nameof(Field), request.Id);
                }

                _context.Fields.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
