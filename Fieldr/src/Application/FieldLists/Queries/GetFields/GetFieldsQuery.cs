using AutoMapper;
using AutoMapper.QueryableExtensions;
using Fieldr.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fieldr.Application.FieldLists.Queries.GetFields
{
    public class GetFieldsQuery : IRequest<FieldVm>
    {
        public class GetFieldsQueryHandler : IRequestHandler<GetFieldsQuery, FieldVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetFieldsQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FieldVm> Handle(GetFieldsQuery request, CancellationToken cancellationToken)
            {
                var vm = new FieldVm();

                vm.Lists = await _context.Fields
                    .ProjectTo<FieldListDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Name)
                    .ToListAsync(cancellationToken);

                return vm;
            }
        }
    }
}
