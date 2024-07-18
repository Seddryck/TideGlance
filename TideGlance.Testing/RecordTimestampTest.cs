using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance.Testing;

public class RecordTimestampTest
{
    [Test]
    public void GetValue_UnsetField_Null()
    {
        var structure = new StructureTimestamp(["foo", "bar"], "foo");
        var record = new RecordTimestampBuilder(structure).WithField("foo", new DateTime(2024, 7, 18, 13, 45, 0)).Build();
        Assert.That(record.GetValue("bar"), Is.Null);
    }

    [Test]
    public void GetValue_SetField_Value()
    {
        var structure = new StructureTimestamp(["foo", "bar"], "foo");
        var record = new RecordTimestampBuilder(structure)
                        .WithField("foo", new DateTime(2024, 7, 18, 13, 45, 0))
                        .WithField("bar", "barValue")
                        .Build();
        Assert.That(record.GetValue("bar"), Is.Not.Null);
        Assert.That(record.GetValue("bar"), Is.EqualTo("barValue"));
    }

    [Test]
    public void GetValue_UnexpectedField_Throws()
    {
        var structure = new StructureTimestamp(["foo", "bar"], "foo");
        var record = new RecordTimestampBuilder(structure)
                        .WithField("foo", new DateTime(2024, 7, 18, 13, 45, 0))
                        .WithField("bar", "barValue")
                        .Build();
        var ex = Assert.Throws<NonExistingFieldException>(() => record.GetValue("qux"));
        Assert.That(ex.FieldName, Is.EqualTo("qux"));
    }

    [Test]
    public void GetValue_TimestampField_Value()
    {
        var structure = new StructureTimestamp(["foo", "bar"], "foo");
        var record = (RecordTimestamp)new RecordTimestampBuilder(structure)
                        .WithField("foo", new DateTime(2024, 7, 18, 13, 45, 0))
                        .WithField("bar", "barValue")
                        .Build();
        Assert.That(record.Timestamp, Is.Not.Null);
        Assert.That(record.Timestamp, Is.EqualTo(new DateTime(2024, 7, 18, 13, 45, 0)));
    }
}
