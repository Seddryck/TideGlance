using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance.Testing;

public class RecordTest
{
    [Test]
    public void GetValue_UnsetField_Null()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record = structure.Builder.WithField("foo", "fooValue").Build();
        Assert.That(record.GetValue("bar"), Is.Null);
    }

    [Test]
    public void GetValue_SetField_Value()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record = structure.Builder.WithField("foo", "fooValue").WithField("bar", "barValue").Build();
        Assert.That(record.GetValue("bar"), Is.Not.Null);
        Assert.That(record.GetValue("bar"), Is.EqualTo("barValue"));
    }

    [Test]
    public void GetValue_UnexpectedField_Throws()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record = structure.Builder.WithField("foo", "fooValue").WithField("bar", "barValue").Build();
        var ex = Assert.Throws<NonExistingFieldException>(() => record.GetValue("qux"));
        Assert.That(ex.FieldName, Is.EqualTo("qux"));
    }

    [Test]
    public void SetValue_UnsetField_DoesNotThrow()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record = structure.Builder.WithField("foo", "fooValue").Build();
        Assert.DoesNotThrow(() => record.SetValue("bar", "barValue"));
        Assert.That(record.GetValue("bar"), Is.EqualTo("barValue"));
    }

    [Test]
    public void SetValue_SetField_DoesNotThrow()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record = structure.Builder.WithField("foo", "fooValue").WithField("bar", "barValue").Build();
        Assert.DoesNotThrow(() => record.SetValue("bar", "newBarValue"));
        Assert.That(record.GetValue("bar"), Is.EqualTo("newBarValue"));
    }

    [Test]
    public void SetValue_UnexpectedField_Throw()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record = structure.Builder.WithField("foo", "fooValue").WithField("bar", "barValue").Build();
        var ex = Assert.Throws<NonExistingFieldException>(() => record.SetValue("qux", "quxValue"));
        Assert.That(ex.FieldName, Is.EqualTo("qux"));
    }

    [Test]
    public void SetNull_UnsetField_DoesNotThrow()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record = structure.Builder.WithField("foo", "fooValue").Build();
        Assert.DoesNotThrow(() => record.SetNull("bar"));
        Assert.That(record.GetValue("bar"), Is.Null);
    }

    [Test]
    public void SetNull_SetField_DoesNotThrow()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record = structure.Builder.WithField("foo", "fooValue").WithField("bar", "barValue").Build();
        Assert.DoesNotThrow(() => record.SetNull("bar"));
        Assert.That(record.GetValue("bar"), Is.Null);
    }

    [Test]
    public void SetNull_UnexpectedField_Throw()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record = structure.Builder.WithField("foo", "fooValue").WithField("bar", "barValue").Build();
        var ex = Assert.Throws<NonExistingFieldException>(() => record.SetNull("qux"));
        Assert.That(ex.FieldName, Is.EqualTo("qux"));
    }

    [Test]
    public void GetValue_OneFieldIdenty_Identity()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record = structure.Builder.WithField("foo", "fooValue").Build();
        var identity = record.GetIdentity();
        Assert.That(identity, Is.Not.Null);
    }

    [Test]
    public void Equals_TwoRecordsWithSameIdentity_True()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record1 = structure.Builder.WithField("foo", "fooValue").Build();
        var record2 = structure.Builder.WithField("foo", "fooValue").Build();

        Assert.That(record1 == record2, Is.True);
        Assert.That(record1.GetHashCode(), Is.EqualTo(record2.GetHashCode()));
        Assert.That(record1.Equals(record2), Is.True);
        Assert.That(record1 != record2, Is.False);
        Assert.That(record1, Is.EqualTo(record2));
    }

    [Test]
    public void Equals_TwoRecordsWithSameIdentityButDistinctValues_True()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record1 = structure.Builder.WithField("foo", "fooValue").WithField("bar", "barValue").Build();
        var record2 = structure.Builder.WithField("foo", "fooValue").WithField("bar", "quxValue").Build();

        Assert.That(record1 == record2, Is.True);
        Assert.That(record1.GetHashCode(), Is.EqualTo(record2.GetHashCode()));
        Assert.That(record1.Equals(record2), Is.True);
        Assert.That(record1 != record2, Is.False);
        Assert.That(record1, Is.EqualTo(record2));
    }

    [Test]
    public void Equals_TwoRecordsWithDistinctIdentity_False()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record1 = structure.Builder.WithField("foo", "fooValue").Build();
        var record2 = structure.Builder.WithField("foo", "barValue").Build();

        Assert.That(record1 == record2, Is.False);
        Assert.That(record1 != record2, Is.True);
        Assert.That(record1.Equals(record2), Is.False);
        Assert.That(record1, Is.Not.EqualTo(record2));
    }

    [Test]
    public void Equals_RecordAndNull_False()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record1 = structure.Builder.WithField("foo", "fooValue").Build();

        Assert.That(record1.Equals(null), Is.False);
        Assert.That(record1, Is.Not.EqualTo(null));
    }

    [Test]
    public void Equals_RecordAndItself_True()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var record1 = structure.Builder.WithField("foo", "fooValue").Build();
        var record2 = record1;

        Assert.That(record1 == record2, Is.True);
    }


    [Test]
    public void Equals_TwoDifferentStructures_False()
    {
        var structure1 = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var structure2 = new Structure(new[] { "foo", "bar" }, new[] { "foo", "bar" });
        var record1 = structure1.Builder.WithField("foo", "fooValue").Build();
        var record2 = structure2.Builder.WithField("foo", "fooValue").Build();

        Assert.That(record1 == record2, Is.False);
    }

    [Test]
    public void Equals_TwoDifferentStructuresSameCount_False()
    {
        var structure1 = new Structure(new[] { "foo", "bar" }, new[] { "foo" });
        var structure2 = new Structure(new[] { "foo", "bar" }, new[] { "bar" });
        var record1 = structure1.Builder.WithField("foo", "fooValue").Build();
        var record2 = structure2.Builder.WithField("foo", "fooValue").Build();

        Assert.That(record1 == record2, Is.False);
    }

    [Test]
    public void Equals_TwoSameStructureButOneIsNull_False()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo", "bar" });
        var record1 = structure.Builder.WithField("foo", "fooValue").WithField("bar", "barValue").Build();
        var record2 = structure.Builder.WithField("foo", null).WithField("bar", "barValue").Build();

        Assert.That(record1 == record2, Is.False);
        Assert.That(record1.GetHashCode(), Is.Not.EqualTo(record2.GetHashCode()));
    }

    [Test]
    public void Equals_TwoSameStructureButBothHaveNull_False()
    {
        var structure = new Structure(new[] { "foo", "bar" }, new[] { "foo", "bar" });
        var record1 = structure.Builder.WithField("foo", "fooValue").WithField("bar", null).Build();
        var record2 = structure.Builder.WithField("foo", null).WithField("bar", "barValue").Build();

        Assert.That(record1 == record2, Is.False);
        Assert.That(record1.GetHashCode(), Is.Not.EqualTo(record2.GetHashCode()));
    }
}
