using Fieldr.Application.Common.Mappings;
using Fieldr.Application.FieldLists.Queries.GetFields;
using Fieldr.Domain.Entities;
using System.Collections.Generic;

namespace Fieldr.Application.FieldLists.Queries.GetFields
{
    public class FieldListDto : IMapFrom<Field>
    {
        public FieldListDto()
        {
            FieldRecords = new List<FieldRecordDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<FieldRecordDto> FieldRecords { get; set; }
    }
}
