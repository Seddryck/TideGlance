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
        public void Append_AnyObjOnEmptyRepo_IsAdded()
        {
            var structure = new StructureTimestamp(["foo", "bar"], "foo");
            var record = (RecordTimestamp)new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2024,07,18)).WithField("bar", "barValue").Build();
            var repository = new InMemoryTimestampRepository();
            repository.Append(record);

            Assert.That(repository.CountRecords(), Is.EqualTo(1));
            Assert.That(repository.CountTimestamps(), Is.EqualTo(1));
        }

        [Test]
        public void Append_AnyObjWithSameTimestampOnEmptyRepo_BothAdded()
        {
            var structure = new StructureTimestamp(["foo", "bar"], "foo");
            var record1 = (RecordTimestamp)new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2024, 07, 18)).WithField("bar", 1).Build();
            var record2 = (RecordTimestamp)new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2024, 07, 18)).WithField("bar", 2).Build();

            var repository = new InMemoryTimestampRepository();
            repository.Append(record1);
            repository.Append(record2);

            Assert.That(repository.CountRecords(), Is.EqualTo(2));
            Assert.That(repository.CountTimestamps(), Is.EqualTo(1));
        }

        [Test]
        public void Append_AnyObjWithDistinctTimestampOnEmptyRepo_BothAdded()
        {
            var structure = new StructureTimestamp(["foo", "bar"], "foo");
            var record1 = (RecordTimestamp)new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2024, 07, 18)).WithField("bar", 1).Build();
            var record2 = (RecordTimestamp)new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2024, 07, 19)).WithField("bar", 2).Build();

            var repository = new InMemoryTimestampRepository();
            repository.Append(record1);
            repository.Append(record2);

            Assert.That(repository.CountRecords(), Is.EqualTo(2));
            Assert.That(repository.CountTimestamps(), Is.EqualTo(2));
        }

        [Test]
        public void Clean_AnyObjWithSameTimestampOnEmptyRepo_BothRemoved()
        {
            var structure = new StructureTimestamp(["foo", "bar"], "foo");
            var record1 = (RecordTimestamp)new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2024, 07, 18)).WithField("bar", 1).Build();
            var record2 = (RecordTimestamp)new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2024, 07, 18)).WithField("bar", 2).Build();

            var repository = new InMemoryTimestampRepository();
            repository.Append(record1);
            repository.Append(record2);
            repository.CleanBefore(new DateTime(2025, 1, 1));

            Assert.That(repository.CountRecords(), Is.EqualTo(0));
            Assert.That(repository.CountTimestamps(), Is.EqualTo(0));
        }

        [Test]
        public void Clean_AnyObjWithDistinctTimestamp_BothRemoved()
        {
            var structure = new StructureTimestamp(["foo", "bar"], "foo");
            var record1 = (RecordTimestamp)new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2024, 07, 18)).WithField("bar", 1).Build();
            var record2 = (RecordTimestamp)new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2024, 07, 19)).WithField("bar", 2).Build();

            var repository = new InMemoryTimestampRepository();
            repository.Append(record1);
            repository.Append(record2);
            repository.CleanBefore(new DateTime(2025, 1, 1));

            Assert.That(repository.CountRecords(), Is.EqualTo(0));
            Assert.That(repository.CountTimestamps(), Is.EqualTo(0));
        }

        [Test]
        public void Clean_AnyObjWithDistinctTimestampOnEmptyRepo_OneRemoved()
        {
            var structure = new StructureTimestamp(["foo", "bar"], "foo");
            var record1 = (RecordTimestamp)new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2024, 07, 18)).WithField("bar", 1).Build();
            var record2 = (RecordTimestamp)new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2026, 07, 18)).WithField("bar", 2).Build();

            var repository = new InMemoryTimestampRepository();
            repository.Append(record1);
            repository.Append(record2);
            repository.CleanBefore(new DateTime(2025, 1, 1));

            Assert.That(repository.CountRecords(), Is.EqualTo(1));
            Assert.That(repository.CountTimestamps(), Is.EqualTo(1));
        }

        [Test]
        public void Clean_AnyObjWithDistinctTimestampOnEmptyRepo_NoneRemoved()
        {
            var structure = new StructureTimestamp(["foo", "bar"], "foo");
            var record1 = (RecordTimestamp)new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2024, 07, 18)).WithField("bar", 1).Build();
            var record2 = (RecordTimestamp)new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2026, 07, 18)).WithField("bar", 2).Build();

            var repository = new InMemoryTimestampRepository();
            repository.Append(record1);
            repository.Append(record2);
            repository.CleanBefore(new DateTime(2023, 1, 1));

            Assert.That(repository.CountRecords(), Is.EqualTo(2));
            Assert.That(repository.CountTimestamps(), Is.EqualTo(2));
        }
    }
}
