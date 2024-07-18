using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal abstract class Record
{
    public Structure Structure { get; init; }
    private Dictionary<string, object> Values { get; } = [];

    internal Record(Structure structure, IDictionary<string, object> values)
    {
        (Structure) = (structure);
        foreach (var value in values)
            SetValue(value.Key, value.Value);
    }

    protected void SetValue(string fieldName, object fieldValue)
    {
        if (!Structure.Contains(fieldName))
            throw new NonExistingFieldException(fieldName);
        if (Values.ContainsKey(fieldName))
            Values[fieldName] = fieldValue;
        else
            Values.Add(fieldName, fieldValue);
    }

    protected void SetNull(string fieldName)
    {
        if (!Structure.Contains(fieldName))
            throw new NonExistingFieldException(fieldName);
        if (Values.ContainsKey(fieldName))
            Values.Remove(fieldName);
    }

    public object? GetValue(string fieldName)
    {
        if (!Structure.Contains(fieldName))
            throw new NonExistingFieldException(fieldName);
        if (!Values.ContainsKey(fieldName))
            return null;
        return Values[fieldName];
    }

}
