using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

public class StructureException(string message) : TideGlanceException(message)
{
}

public class IdentityException(string fieldName) 
    : StructureException($"The identity must be defined based on fields which are part of the structure. The field named '{fieldName}' is not part of the structure.")
{
    public string FieldName { get; } = fieldName;
}
public class TimestampException(string fieldName) 
    : StructureException($"The timestamp must be defined based on fields which are part of the structure. The field named '{fieldName}' is not part of the structure.")
{
    public string FieldName { get; } = fieldName;
}

public class NonExistingFieldException(string fieldName) 
    : StructureException($"The current structure doesn't contain a field named '{fieldName}'")
{
    public string FieldName { get; } = fieldName;
}

public class DuplicateFieldException(string fieldName) 
    : StructureException($"The current structure already contains a field named '{fieldName}'. You can define two fields with the same name.")
{
    public string FieldName { get; } = fieldName;
}