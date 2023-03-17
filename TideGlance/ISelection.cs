using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal interface ISelection
{
    string Name { get; set; }
    object? Execute(object obj);
}
