using Fieldr.Application.Common.Interfaces;
using Fieldr.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public string PhotoBase64 { get; set; }
        public string PhotoName { get; set; }

        public class CreateFieldRecordCommandHandler : IRequestHandler<CreateFieldRecordCommand, long> 
        {
            private readonly IApplicationDbContext _context;
            private readonly IAzureStorageService _azureStorageService;

            public CreateFieldRecordCommandHandler(IApplicationDbContext context, IAzureStorageService azureStorageService)
            {
                _context = context;
                _azureStorageService = azureStorageService;
            }

            public async Task<long> Handle(CreateFieldRecordCommand request, CancellationToken cancellationToken)
            {
                var imageUrl = await _azureStorageService.UploadFileToStorage(request.PhotoBase64, request.PhotoName);

                var entity = new FieldRecord()
                {
                    FieldId = request.ListId,
                    Note = request.Note,
                    PhotoUrl = imageUrl
                };

                _context.FieldRecords.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
