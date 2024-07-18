using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal class RecordTimestamp : Record
{
    public DateTime Timestamp { get; init; }

    internal RecordTimestamp(StructureTimestamp structure, IDictionary<string, object> values)
        : base(structure, values)
        => (Timestamp) = ((DateTime)values[structure.Timestamp]);
}