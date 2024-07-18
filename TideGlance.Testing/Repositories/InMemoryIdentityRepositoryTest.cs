using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TideGlance.Repositories;

namespace TideGlance.Testing.Repositories
{
    public class InMemoryIdentityRepositoryTest
    {
        [Test]
        public void Merge_AnyObjOnEmptyRepo_IsAdded()
        {
            var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
            var record = (RecordIdentity)new RecordIdentityBuilder(structure).WithField("foo", "fooValue").WithField("bar", "barValue").Build();
            var repository = new InMemoryIdentityRepository();
            repository.Merge(record);

            Assert.That(repository.Count(), Is.EqualTo(1));
            Assert.That(repository.ContainsIdentity(record.Identity), Is.True);
            Assert.That(repository.Get(record.Identity).GetValue("bar"), Is.EqualTo("barValue"));
        }

        [Test]
        public void Merge_AnyObjOnEmptyRepo_BothAdded()
        {
            var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
            var record1 = (RecordIdentity)new RecordIdentityBuilder(structure).WithField("foo", "fooValue").WithField("bar", "barValue").Build();
            var record2 = (RecordIdentity)new RecordIdentityBuilder(structure).WithField("foo", "quarkValue").WithField("bar", "protonValue").Build();
            
            var repository = new InMemoryIdentityRepository();
            repository.Merge(record1);
            repository.Merge(record2);

            Assert.That(repository.Count(), Is.EqualTo(2));
            Assert.That(repository.ContainsIdentity(record1.Identity), Is.True);
            Assert.That(repository.ContainsIdentity(record2.Identity), Is.True);
        }

        [Test]
        public void Merge_AnyObjAlreadyAdded_Replaced()
        {
            var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
            var record1 = (RecordIdentity)new RecordIdentityBuilder(structure).WithField("foo", "fooValue").WithField("bar", "barValue").Build();
            var record2 = (RecordIdentity)new RecordIdentityBuilder(structure).WithField("foo", "fooValue").WithField("bar", "protonValue").Build();

            var repository = new InMemoryIdentityRepository();
            repository.Merge(record1);
            repository.Merge(record2);

            Assert.That(repository.Count(), Is.EqualTo(1));
            Assert.That(repository.ContainsIdentity(record2.Identity), Is.True);
            Assert.That(repository.Get(record2.Identity).GetValue("bar"), Is.EqualTo("protonValue"));
        }
    }
}
