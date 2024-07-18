using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal class StructureIdentity : Structure
{
    public IReadOnlyCollection<string> Identity { get => IdentityFields; }
    private List<string> IdentityFields { get; set; } = [];

    public StructureIdentity(string[] fields, string[] identityFields)
        : base(fields)
    {
        if (identityFields.GroupBy(x => x).Where(g => g.Count() > 1).Any())
            throw new DuplicateFieldException(identityFields.GroupBy(x => x).Where(g => g.Count() > 1).First().First());

        if (identityFields.Except(fields).Any())
            throw new IdentityException(identityFields.Except(fields).First());
        IdentityFields = [.. identityFields];
    }

    protected override void CheckBeforeRemove(string fieldName)
    {
        base.CheckBeforeRemove(fieldName);
        if (Identity.Contains(fieldName))
            throw new IdentityException(fieldName);
    }
}
