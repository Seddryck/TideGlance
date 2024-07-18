using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal interface IIdentityRepository
{
    void Merge(RecordIdentity record);
    //RecordIdentity Get(Identity identity);
    //bool TryGet(Identity identity, [NotNullWhen(true)] out RecordIdentity? record);
}
