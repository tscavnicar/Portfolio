using Fieldr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fieldr.Application.FieldLists.Queries.GetFields
{
    public class FieldVm
    {
        public IList<FieldListDto> Lists { get; set; }
    }
}
