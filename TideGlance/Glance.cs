using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance;

internal class Glance
{
    ICollection<ISelection> Selections { get; set; }
    IRepository Repository { get; set; }


    public Glance(ICollection<ISelection> selections, IRepository repository)
        => (Selections, Repository) = (selections, repository);

    public void Refresh(object obj)
    {
        var projections = Selections.Select(x => new { Key = x.Name , Value = x.Execute(obj)}).ToDictionary(y => y.Key, y => y.Value);
        Repository.Merge(projections);
    }
}
