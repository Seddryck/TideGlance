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
    ICollection<IProjection> Projections { get; set; }
    IRepository Repository { get; set; }
    Structure Structure { get; set; }


    public Glance(ICollection<IProjection> projections, IRepository repository, string identity)
        => (Projections,  Repository,  Structure) = (projections, repository
                , new Structure(projections.Select(x => x.Name).ToArray(), new[] { identity })
            );

    public void Refresh(object obj)
    {
        var projections = Projections.Select(x => new { Key = x.Name , Value = x.Execute(obj)}).ToDictionary(y => y.Key, y => y.Value);
        var record = Structure.Builder.WithDictionary(projections).Build();
        Repository.Merge(record);
    }
}
