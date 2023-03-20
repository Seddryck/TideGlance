using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal class Structure
{
    public IReadOnlyCollection<string> Identity { get => IdentityFields; }
    private List<string> IdentityFields { get; set; } = new List<string>();
    private Dictionary<string, object> Fields { get; set; } = new Dictionary<string, object>();

    public Structure(string[] fields, string[] identityFields)
    {
        if (fields.GroupBy(x => x).Where(g => g.Count() > 1).Any())
            throw new DuplicateFieldException(fields.GroupBy(x => x).Where(g => g.Count() > 1).First().First());

        if (identityFields.GroupBy(x => x).Where(g => g.Count() > 1).Any())
            throw new DuplicateFieldException(identityFields.GroupBy(x => x).Where(g => g.Count() > 1).First().First());

        if (identityFields.Except(fields).Any())
            throw new IdentityException(identityFields.Except(fields).First());
        IdentityFields = identityFields.ToList();
        Fields = fields.ToDictionary(x => x, y => new object());
    }

    public void Add(string fieldName)
    {
        if(Fields.ContainsKey(fieldName))
            throw new DuplicateFieldException(fieldName);
        Fields.Add(fieldName, new object());
    }

    public void Remove(string fieldName)
    {
        if (!Fields.ContainsKey(fieldName))
            throw new NonExistingFieldException(fieldName);
        if (Identity.Contains(fieldName))
            throw new IdentityException(fieldName);

        Fields.Remove(fieldName);
    }

    public bool Contains(string fieldName)
        => Fields.ContainsKey(fieldName);

    public RecordBuilder Builder { get => new(this); }
}
