using Fieldr.Application.Common.Interfaces;
using Fieldr.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fieldr.Application.FieldLists.Commands.CreateFieldList
{
    public class CreateFieldListCommand : IRequest<int>
    {
        public string Name { get; set; }
        public class CreateFieldListHandler : IRequestHandler<CreateFieldListCommand, int>
        {
            private IApplicationDbContext _context;

            public CreateFieldListHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateFieldListCommand request, CancellationToken cancellationToken)
            {
                var entity = new Field();

                entity.Name = request.Name;

                _context.Fields.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
