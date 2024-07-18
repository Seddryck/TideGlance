using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;
internal class RecordTimestampBuilder : RecordBuilder
{
    public RecordTimestampBuilder(StructureTimestamp structure)
        : base(structure) { }
    protected new StructureTimestamp Structure { get => (StructureTimestamp)base.Structure; }

    public override Record Build()
        =>  new RecordTimestamp(Structure, PreBuild());
}
