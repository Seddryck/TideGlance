using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal class StructureTimestamp : Structure
{
    public string Timestamp { get => TimestampField; }
    private string TimestampField { get; init; }

    public StructureTimestamp(string[] fields, string timestamp)
        : base(fields)
    {
        if (!fields.Contains(timestamp))
            throw new TimestampException(timestamp);
        TimestampField = timestamp;
    }

    protected override void CheckBeforeRemove(string fieldName)
    {
        base.CheckBeforeRemove(fieldName);
        if (Timestamp == fieldName)
            throw new TimestampException(fieldName);
    }
}
