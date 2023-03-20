using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TideGlance.Repositories;

namespace TideGlance.Testing.Repositories
{
    public class InMemoryRepositoryTest
    {
        [Test]
        public void Merge_AnyObjOnEmptyDico_IsAdded()
        {
            var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
            var record = structure.Builder.WithField("foo", "fooValue").WithField("bar", "barValue").Build();
            var repository = new InMemoryRepository();
            repository.Merge(record);

            Assert.That(repository.Count(), Is.EqualTo(1));
            Assert.That(repository.ContainsIdentity(record.GetIdentity()), Is.True);
            Assert.That(repository.GetRecord(record.GetIdentity()).GetValue("bar"), Is.EqualTo("barValue"));
        }

        [Test]
        public void Merge_AnyObjOnEmptyDico_BothAdded()
        {
            var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
            var record1 = structure.Builder.WithField("foo", "fooValue").WithField("bar", "barValue").Build();
            var record2 = structure.Builder.WithField("foo", "quarkValue").WithField("bar", "protonValue").Build();
            
            var repository = new InMemoryRepository();
            repository.Merge(record1);
            repository.Merge(record2);

            Assert.That(repository.Count(), Is.EqualTo(2));
            Assert.That(repository.ContainsIdentity(record1.GetIdentity()), Is.True);
            Assert.That(repository.ContainsIdentity(record2.GetIdentity()), Is.True);
        }

        [Test]
        public void Merge_AnyObjAlreadyAdded_Replaced()
        {
            var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
            var record1 = structure.Builder.WithField("foo", "fooValue").WithField("bar", "barValue").Build();
            var record2 = structure.Builder.WithField("foo", "fooValue").WithField("bar", "protonValue").Build();

            var repository = new InMemoryRepository();
            repository.Merge(record1);
            repository.Merge(record2);

            Assert.That(repository.Count(), Is.EqualTo(1));
            Assert.That(repository.ContainsIdentity(record2.GetIdentity()), Is.True);
            Assert.That(repository.GetRecord(record2.GetIdentity()).GetValue("bar"), Is.EqualTo("protonValue"));
        }
    }
}
