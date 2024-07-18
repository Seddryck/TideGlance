using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal class RecordIdentity : Record
{
    public Identity Identity { get; init; }
    public new StructureIdentity Structure { get => (StructureIdentity)base.Structure; }

    internal RecordIdentity(StructureIdentity structure, IDictionary<string, object> values)
        : base(structure, values)
        => (Identity) = (new Identity(structure.Identity.ToDictionary(x => x, GetValue)));

    #region Equality
    public static bool operator ==(RecordIdentity record1, RecordIdentity record2)
    {
        if (ReferenceEquals(record1, record2))
            return true;
        if (record1 is null || record2 is null)
            return false;
        if (record1.Structure.Identity.Count != record2.Structure.Identity.Count)
            return false;
        if (record1.Structure.Identity.Except(record2.Structure.Identity).Any())
            return false;

        foreach (string fieldName in record1.Structure.Identity)
        {
            if (!record1.GetValue(fieldName)?.Equals(record2.GetValue(fieldName)) ?? false)
                return false;
        }
        return true;
    }

    public static bool operator !=(RecordIdentity record1, RecordIdentity record2)
        => !(record1 == record2);

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
            foreach (string fieldName in Structure.Identity)
            {
                hash = hash * 31 + fieldName.GetHashCode();
                hash = hash * 43 + (GetValue(fieldName)?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }

    #endregion
}
