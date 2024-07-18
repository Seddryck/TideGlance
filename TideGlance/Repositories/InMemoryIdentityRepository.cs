using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance.Repositories;

internal class InMemoryIdentityRepository : IIdentityRepository
{
    private Dictionary<Identity, RecordIdentity> Repository { get; } = [];

    public InMemoryIdentityRepository()
    { }

    public void Merge(RecordIdentity record)
    {
        if (Repository.ContainsKey(record.Identity))
            Repository[record.Identity] = record;
        else
            Repository.Add(record.Identity, record);
    }

    internal int Count() => Repository.Count;
    internal bool ContainsIdentity(Identity identity) => Repository.ContainsKey(identity);
    public RecordIdentity Get(Identity identity) => Repository[identity];

    public bool TryGet(Identity identity, [NotNullWhen(true)] out RecordIdentity? record)
        => Repository.TryGetValue(identity, out record);
}
