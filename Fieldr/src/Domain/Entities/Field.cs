using System;
using System.Collections.Generic;
using System.Text;

namespace Fieldr.Domain.Entities
{
    public class Field
    {
        public Field() => FieldRecords = new HashSet<FieldRecord>();
        public int Id { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string wktEPSG4326 { get; set; }

        public ICollection<FieldRecord> FieldRecords { get; set; }
    }
}
