using Fieldr.Application.Common.Exceptions;
using Fieldr.Application.Common.Interfaces;
using Fieldr.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fieldr.Application.FieldLists.Commands.UpdateFieldList
{
    public class UpdateFieldListCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public class UpdateFieldListCommandHandler : IRequestHandler<UpdateFieldListCommand>
        {
            private IApplicationDbContext _context;

            public UpdateFieldListCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateFieldListCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Fields.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Field), request.Id);
                }

                entity.Name = request.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }

        }
    }
}
