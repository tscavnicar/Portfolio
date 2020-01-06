using Fieldr.Application.Common.Mappings;
using Fieldr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fieldr.Application.FieldLists.Queries.GetFields
{
    public class FieldRecordDto : IMapFrom<FieldRecord>
    {
        public long Id { get; set; }
        public int FieldId { get; set; }
        public string Note { get; set; }
    }
}
