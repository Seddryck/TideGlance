using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance.Testing;

public class RecordIdentityTest
{
    [Test]
    public void GetValue_UnsetField_Null()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        var record = new RecordIdentityBuilder(structure).WithField("foo", "fooValue").Build();
        Assert.That(record.GetValue("bar"), Is.Null);
    }

    [Test]
    public void GetValue_SetField_Value()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        var record = new RecordIdentityBuilder(structure).WithField("foo", "fooValue").WithField("bar", "barValue").Build();
        Assert.That(record.GetValue("bar"), Is.Not.Null);
        Assert.That(record.GetValue("bar"), Is.EqualTo("barValue"));
    }

    [Test]
    public void GetValue_UnexpectedField_Throws()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        var record = new RecordIdentityBuilder(structure).WithField("foo", "fooValue").WithField("bar", "barValue").Build();
        var ex = Assert.Throws<NonExistingFieldException>(() => record.GetValue("qux"));
        Assert.That(ex.FieldName, Is.EqualTo("qux"));
    }

    [Test]
    public void GetValue_OneFieldIdenty_Identity()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        var record = (RecordIdentity)new RecordIdentityBuilder(structure).WithField("foo", "fooValue").Build();
        Assert.That(record.Identity, Is.Not.Null);
    }

    [Test]
    public void Equals_TwoRecordsWithSameIdentity_True()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        var record1 = (RecordIdentity)new RecordIdentityBuilder(structure).WithField("foo", "fooValue").Build();
        var record2 = (RecordIdentity)new RecordIdentityBuilder(structure).WithField("foo", "fooValue").Build();

        Assert.That(record1 == record2, Is.True);
        Assert.That(record1.GetHashCode(), Is.EqualTo(record2.GetHashCode()));
        Assert.That(record1.Equals(record2), Is.True);
        Assert.That(record1 != record2, Is.False);
        Assert.That(record1, Is.EqualTo(record2));
    }

    [Test]
    public void Equals_TwoRecordsWithSameIdentityButDistinctValues_True()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        var record1 = (RecordIdentity)new RecordIdentityBuilder(structure).WithField("foo", "fooValue").WithField("bar", "barValue").Build();
        var record2 = (RecordIdentity)new RecordIdentityBuilder(structure).WithField("foo", "fooValue").WithField("bar", "quxValue").Build();

        Assert.That(record1 == record2, Is.True);
        Assert.That(record1.GetHashCode(), Is.EqualTo(record2.GetHashCode()));
        Assert.That(record1.Equals(record2), Is.True);
        Assert.That(record1 != record2, Is.False);
        Assert.That(record1, Is.EqualTo(record2));
    }

    [Test]
    public void Equals_TwoRecordsWithDistinctIdentity_False()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        var record1 = new RecordIdentityBuilder(structure).WithField("foo", "fooValue").Build();
        var record2 = new RecordIdentityBuilder(structure).WithField("foo", "barValue").Build();

        Assert.That(record1 == record2, Is.False);
        Assert.That(record1 != record2, Is.True);
        Assert.That(record1.Equals(record2), Is.False);
        Assert.That(record1, Is.Not.EqualTo(record2));
    }

    [Test]
    public void Equals_RecordAndNull_False()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        var record1 = new RecordIdentityBuilder(structure).WithField("foo", "fooValue").Build();

        Assert.That(record1.Equals(null), Is.False);
        Assert.That(record1, Is.Not.EqualTo(null));
    }

    [Test]
    public void Equals_RecordAndItself_True()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        var record1 = new RecordIdentityBuilder(structure).WithField("foo", "fooValue").Build();
        var record2 = record1;

        Assert.That(record1 == record2, Is.True);
    }


    [Test]
    public void Equals_TwoDifferentStructures_False()
    {
        var structure1 = new StructureIdentity(["foo", "bar"], ["foo"]);
        var structure2 = new StructureIdentity(["foo", "bar"], ["foo", "bar"]);
        var record1 = new RecordIdentityBuilder(structure1).WithField("foo", "fooValue").Build();
        var record2 = new RecordIdentityBuilder(structure2).WithField("foo", "fooValue").Build();

        Assert.That(record1 == record2, Is.False);
    }

    [Test]
    public void Equals_TwoDifferentStructuresSameCount_False()
    {
        var structure1 = new StructureIdentity(["foo", "bar"], ["foo"]);
        var structure2 = new StructureIdentity(["foo", "bar"], ["bar"]);
        var record1 = new RecordIdentityBuilder(structure1).WithField("foo", "fooValue").Build();
        var record2 = new RecordIdentityBuilder(structure2).WithField("foo", "fooValue").Build();

        Assert.That(record1 == record2, Is.False);
    }

    [Test]
    public void Equals_TwoSameStructureButOneIsNull_False()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo", "bar"]);
        var record1 = new RecordIdentityBuilder(structure).WithField("foo", "fooValue").WithField("bar", "barValue").Build();
        var record2 = new RecordIdentityBuilder(structure).WithField("foo", null).WithField("bar", "barValue").Build();

        Assert.That(record1 == record2, Is.False);
        Assert.That(record1.GetHashCode(), Is.Not.EqualTo(record2.GetHashCode()));
    }

    [Test]
    public void Equals_TwoSameStructureButBothHaveNull_False()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo", "bar"]);
        var record1 = new RecordIdentityBuilder(structure).WithField("foo", "fooValue").WithField("bar", null).Build();
        var record2 = new RecordIdentityBuilder(structure).WithField("foo", null).WithField("bar", "barValue").Build();

        Assert.That(record1 == record2, Is.False);
        Assert.That(record1.GetHashCode(), Is.Not.EqualTo(record2.GetHashCode()));
    }
    [Test]
    public void Equals_TwoRecordIdentities_True()
    {
        var identity1 = new Identity(new Dictionary<string, object?> { { "foo", 123 } });
        var identity2 = new Identity(new Dictionary<string, object?> { { "foo", 123 } });

        Assert.That(identity1 == identity2, Is.True);
        Assert.That(identity1.GetHashCode(), Is.EqualTo(identity2.GetHashCode()));
        Assert.That(identity1.Equals(identity2), Is.True);
        Assert.That(identity1 != identity2, Is.False);
        Assert.That(identity1, Is.EqualTo(identity2));
    }

    [Test]
    public void Equals_TwoRecordIdentitiesWithDifferentValues_False()
    {
        var identity1 = new Identity(new Dictionary<string, object?> { { "foo", 123 } });
        var identity2 = new Identity(new Dictionary<string, object?> { { "foo", 456 } });

        Assert.That(identity1 == identity2, Is.False);
        Assert.That(identity1 != identity2, Is.True);
        Assert.That(identity1.Equals(identity2), Is.False);
        Assert.That(identity1, Is.Not.EqualTo(identity2));
    }

    [Test]
    public void Equals_TwoDistinctRecordIdentities_False()
    {
        var identity1 = new Identity(new Dictionary<string, object?> { { "foo", 123 } });
        var identity2 = new Identity(new Dictionary<string, object?> { { "bar", 123 } });

        Assert.That(identity1 == identity2, Is.False);
        Assert.That(identity1 != identity2, Is.True);
        Assert.That(identity1.Equals(identity2), Is.False);
        Assert.That(identity1, Is.Not.EqualTo(identity2));
    }


    [Test]
    public void Equals_TwoRecordIdentitiesWithDifferentKeys_False()
    {
        var identity1 = new Identity(new Dictionary<string, object?> { { "foo", 123 }, {"bar", 456 } });
        var identity2 = new Identity(new Dictionary<string, object?> { { "foo", 456 } });

        Assert.That(identity1 == identity2, Is.False);
        Assert.That(identity1 != identity2, Is.True);
        Assert.That(identity1.Equals(identity2), Is.False);
        Assert.That(identity1, Is.Not.EqualTo(identity2));
    }

    [Test]
    public void Equals_RecordIdentityAndNull_False()
    {
        var identity = new Identity(new Dictionary<string, object?> { { "foo", 123 } });

        Assert.That(identity.Equals(null), Is.False);
        Assert.That(identity, Is.Not.EqualTo(null));
    }

    [Test]
    public void Equals_RecordIdentityAndItself_True()
    {
        var identity1 = new Identity(new Dictionary<string, object?> { { "foo", 123 } });
        var identity2 = identity1;

        Assert.That(identity1 == identity2, Is.True);
    }
}
