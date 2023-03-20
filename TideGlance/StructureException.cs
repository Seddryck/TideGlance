using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

public class StructureException : TideGlanceException
{
    public StructureException(string message)
        : base(message) { }
}

public class IdentityException : StructureException
{
    public string FieldName { get; }

    public IdentityException(string fieldName)
        : base($"The identity must be defined based on fields which are part of the structure. The field named '{fieldName}' is not part of the structure")
    { FieldName = fieldName; }
}

public class NonExistingFieldException : StructureException
{
    public string FieldName { get; } 
    
    public NonExistingFieldException(string fieldName)
        : base($"The current structure doesn't contain a field named '{fieldName}'")
    { FieldName = fieldName; }
}

public class DuplicateFieldException : StructureException
{
    public string FieldName { get; }

    public DuplicateFieldException(string fieldName)
        : base($"The current structure already contains a field named '{fieldName}'. You can define two fields with the same name.")
    { FieldName = fieldName; }
}