using Fieldr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fieldr.Domain.Entities
{
    public class FieldRecord : AuditableEntity
    {
        public long Id { get; set; }
        public int FieldId { get; set; }
        public byte[] Photo { get; set; }
        public string Note { get; set; }
        public Field Field { get; set; }
    }
}
