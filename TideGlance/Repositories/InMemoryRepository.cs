using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance.Repositories;

internal class InMemoryRepository : IRepository
{
    private IDictionary<RecordIdentity, Record> Repository { get; } = new Dictionary<RecordIdentity, Record>();

    public InMemoryRepository()
    { }

    public void Merge(Record record)
    {
        var identity = record.GetIdentity();
        if (Repository.ContainsKey(identity))
            Repository[identity] = record;
        else
            Repository.Add(identity, record);
    }

    internal int Count() => Repository.Count;
    internal bool ContainsIdentity(RecordIdentity identity) => Repository.ContainsKey(identity);
    internal Record GetRecord(RecordIdentity identity) => Repository[identity];

}
