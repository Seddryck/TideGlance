using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal interface IRepository
{
    void Merge(Record record);
}
