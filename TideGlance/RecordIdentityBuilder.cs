using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;
internal class RecordIdentityBuilder : RecordBuilder
{
    public RecordIdentityBuilder(StructureIdentity structure)
        : base(structure) { }

    protected new StructureIdentity Structure { get => (StructureIdentity)base.Structure; }

    public override Record Build()
        => new RecordIdentity(Structure, PreBuild());
}
