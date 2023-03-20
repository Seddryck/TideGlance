using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal class RecordIdentity
{
    private IDictionary<string, object?> Keys { get; } = new Dictionary<string, object?>();

    public RecordIdentity(IDictionary<string, object?> keys)
        => (Keys) = (keys);

    #region Equality
    public static bool operator ==(RecordIdentity identity1, RecordIdentity identity2)
    {
        if (ReferenceEquals(identity1, identity2))
            return true;
        if (identity1 is null || identity2 is null)
            return false;
        if (identity1.Keys.Count != identity2.Keys.Count)
            return false;
        if (identity1.Keys.Keys.Except(identity2.Keys.Keys).Any())
            return false;

        foreach (string fieldName in identity1.Keys.Keys)
        {
            if (!identity2.Keys.ContainsKey(fieldName) || !identity1.Keys[fieldName]!.Equals(identity2.Keys[fieldName]))
                return false; 
        }
        return true;
    }

    public static bool operator !=(RecordIdentity identity1, RecordIdentity identity2)
        => !(identity1 == identity2);

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not RecordIdentity)
            return false;

        return this == (RecordIdentity)obj;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            foreach (string fieldName in Keys.Keys)
            {
                hash = hash * 31 + fieldName.GetHashCode();
                hash = hash * 43 + (Keys[fieldName] == null ? 0 : Keys[fieldName]!.GetHashCode());
            }
            return hash;
        }
    }

    #endregion
}
