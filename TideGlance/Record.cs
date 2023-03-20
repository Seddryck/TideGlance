using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal class Record
{
    private Structure Structure { get; set; }
    private Dictionary<string, object> Values { get; } = new Dictionary<string, object>();

    internal Record(Structure structure)
        => (Structure) = (structure);

    internal void SetValue(string fieldName, object fieldValue)
    {
        if (!Structure.Contains(fieldName))
            throw new NonExistingFieldException(fieldName);
        if (Values.ContainsKey(fieldName))
            Values[fieldName] = fieldValue;
        else
            Values.Add(fieldName, fieldValue);
    }

    internal void SetNull(string fieldName)
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

    public RecordIdentity GetIdentity()
        => new RecordIdentity(Structure.Identity.ToDictionary(x => x, x => GetValue(x)));

    #region Equality
    public static bool operator ==(Record record1, Record record2)
    {
        if (ReferenceEquals(record1, record2))
            return true;
        if (record1 is null || record2 is null)
            return false;
        if (record1.Structure.Identity.Count != record2.Structure.Identity.Count)
            return false;
        if (record1.Structure.Identity.Except(record2.Structure.Identity).Count() != 0)
            return false;

        foreach (string fieldName in record1.Structure.Identity)
        {
            if (!record2.Values.ContainsKey(fieldName) || !record1.Values[fieldName].Equals(record2.Values[fieldName]))
                return false;
        }
        return true;
    }

    public static bool operator !=(Record record1, Record record2)
        => !(record1 == record2);

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not Record)
            return false;

        return this == (Record)obj;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            foreach (string fieldName in Structure.Identity)
            {
                hash = hash * 31 + fieldName.GetHashCode();
                hash = hash * 43 + (!Values.ContainsKey(fieldName) ? 0 : Values[fieldName].GetHashCode());
            }
            return hash;
        }
    }

    #endregion
}
