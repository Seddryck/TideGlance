using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance.Testing;

public class RecordIdentityTest
{
    [Test]
    public void Equals_TwoRecordIdentities_True()
    {
        var identity1 = new RecordIdentity(new Dictionary<string, object?> { { "foo", 123 } });
        var identity2 = new RecordIdentity(new Dictionary<string, object?> { { "foo", 123 } });

        Assert.That(identity1 == identity2, Is.True);
        Assert.That(identity1.GetHashCode(), Is.EqualTo(identity2.GetHashCode()));
        Assert.That(identity1.Equals(identity2), Is.True);
        Assert.That(identity1 != identity2, Is.False);
        Assert.That(identity1, Is.EqualTo(identity2));
    }

    [Test]
    public void Equals_TwoRecordIdentitiesWithDifferentValues_False()
    {
        var identity1 = new RecordIdentity(new Dictionary<string, object?> { { "foo", 123 } });
        var identity2 = new RecordIdentity(new Dictionary<string, object?> { { "foo", 456 } });

        Assert.That(identity1 == identity2, Is.False);
        Assert.That(identity1 != identity2, Is.True);
        Assert.That(identity1.Equals(identity2), Is.False);
        Assert.That(identity1, Is.Not.EqualTo(identity2));
    }

    [Test]
    public void Equals_TwoDistinctRecordIdentities_False()
    {
        var identity1 = new RecordIdentity(new Dictionary<string, object?> { { "foo", 123 } });
        var identity2 = new RecordIdentity(new Dictionary<string, object?> { { "bar", 123 } });

        Assert.That(identity1 == identity2, Is.False);
        Assert.That(identity1 != identity2, Is.True);
        Assert.That(identity1.Equals(identity2), Is.False);
        Assert.That(identity1, Is.Not.EqualTo(identity2));
    }


    [Test]
    public void Equals_TwoRecordIdentitiesWithDifferentKeys_False()
    {
        var identity1 = new RecordIdentity(new Dictionary<string, object?> { { "foo", 123 }, {"bar", 456 } });
        var identity2 = new RecordIdentity(new Dictionary<string, object?> { { "foo", 456 } });

        Assert.That(identity1 == identity2, Is.False);
        Assert.That(identity1 != identity2, Is.True);
        Assert.That(identity1.Equals(identity2), Is.False);
        Assert.That(identity1, Is.Not.EqualTo(identity2));
    }

    [Test]
    public void Equals_RecordIdentityAndNull_False()
    {
        var identity = new RecordIdentity(new Dictionary<string, object?> { { "foo", 123 } });

        Assert.That(identity.Equals(null), Is.False);
        Assert.That(identity, Is.Not.EqualTo(null));
    }

    [Test]
    public void Equals_RecordIdentityAndItself_True()
    {
        var identity1 = new RecordIdentity(new Dictionary<string, object?> { { "foo", 123 } });
        var identity2 = identity1;

        Assert.That(identity1 == identity2, Is.True);
    }
}
