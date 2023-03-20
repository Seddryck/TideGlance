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
internal class RecordBuilder : IRecordBuilder
{
    private Structure Structure { get; set; }
    private List<KeyValuePair<string, object?>> Actions { get; set; } = new List<KeyValuePair<string, object?>>();
    public RecordBuilder(Structure structure)
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


    public Record Build()
    {
        var record = new Record(Structure);
        foreach (var action in Actions)
            if (action.Value == null)
                record.SetNull(action.Key);
            else
                record.SetValue(action.Key, action.Value);
        return record;
    }
}
