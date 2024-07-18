using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal abstract class Structure
{
    private List<string> Fields { get; set; } = [];
    public IReadOnlyCollection<string> FieldNames { get => Fields; }

    public Structure(string[] fields)
    {
        if (fields.GroupBy(x => x).Where(g => g.Count() > 1).Any())
            throw new DuplicateFieldException(fields.GroupBy(x => x).Where(g => g.Count() > 1).First().First());
        Fields = [..fields];
    }

    public void Add(string fieldName)
    {
        if(Fields.Contains(fieldName))
            throw new DuplicateFieldException(fieldName);
        Fields.Add(fieldName);
    }

    protected virtual void CheckBeforeRemove(string fieldName)
    {
        if (!Fields.Contains(fieldName))
            throw new NonExistingFieldException(fieldName);
    }

    public void Remove(string fieldName)
    {
        CheckBeforeRemove(fieldName);
        Fields.Remove(fieldName);
    }

    public bool Contains(string fieldName)
        => Fields.Contains(fieldName);
}
