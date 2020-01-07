using Fieldr.Application.Common.Interfaces;
using Fieldr.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fieldr.Application.FieldRecords.Commands.CreateFieldRecord
{
    public class CreateFieldRecordCommand : IRequest<long>
    {
        public int ListId { get; set; }
        public string Note { get; set; }

        public class CreateFieldRecordCommandHandler : IRequestHandler<CreateFieldRecordCommand, long> 
        {
            private readonly IApplicationDbContext _context;

            public CreateFieldRecordCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateFieldRecordCommand request, CancellationToken cancellationToken)
            {
                var entity = new FieldRecord()
                {
                    FieldId = request.ListId,
                    Note = request.Note
                };

                _context.FieldRecords.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
