using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal interface ITimestampRepository
{
    void Append(RecordTimestamp record);
    int CleanBefore(DateTime threshold);
}
