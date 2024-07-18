using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;
internal interface IRecordBuilder
{
    Record Build();
}
internal abstract class RecordBuilder : IRecordBuilder
{
    protected Structure Structure { get; init; }
    protected List<KeyValuePair<string, object?>> Actions { get; set; } = [];
    protected RecordBuilder(Structure structure)
        => Structure = structure;

    public RecordBuilder WithField(string fieldName, object? value)
    {
        Actions.Add(new KeyValuePair<string, object?>(fieldName, value));
        return this;
    }

    public RecordBuilder WithDictionary(IDictionary<string, object?> dico)
    {
        dico.ToList().ForEach(Actions.Add);
        return this;
    }

    public abstract Record Build();
    protected Dictionary<string, object> PreBuild()
    {
        var dico = new Dictionary<string, object>();
        foreach (var action in Actions)
            if (action.Value is not null)
                dico.Add(action.Key, action.Value);
            else if (dico.ContainsKey(action.Key))
                dico.Remove(action.Key);
        return dico;
    }
}
