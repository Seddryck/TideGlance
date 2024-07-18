using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance.Testing;

public class StructureIdentityTest
{
    [Test]
    public void Ctor_Valid_Success()
        => Assert.DoesNotThrow(() => new StructureIdentity(["foo", "bar"], ["foo"]));

    [Test]
    public void Ctor_DuplicateFields_Throws()
    {
        var ex = Assert.Throws<DuplicateFieldException>(() => new StructureIdentity(["foo", "bar", "foo"], ["foo"]));
        Assert.That(ex.FieldName, Is.EqualTo("foo"));
    }

    [Test]
    public void Ctor_DuplicateIdentityFields_Throws()
    {
        var ex = Assert.Throws<DuplicateFieldException>(() => new StructureIdentity(["foo", "bar"], ["foo", "foo"]));
        Assert.That(ex.FieldName, Is.EqualTo("foo"));
    }

    [Test]
    public void Ctor_IdentityFieldsNotPartOfFields_Throws()
    {
        var ex = Assert.Throws<IdentityException>(() => new StructureIdentity(["foo", "bar"], ["foo", "qux"]));
        Assert.That(ex.FieldName, Is.EqualTo("qux"));
    }

    [Test]
    public void Add_NewField_DoesNotThrow()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        Assert.DoesNotThrow(() => structure.Add("qux"));
        Assert.That(structure.Contains("qux"), Is.True);
    }

    [Test]
    public void Add_ExistingField_Throws()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        var ex = Assert.Throws<DuplicateFieldException>(() => structure.Add("foo"));
        Assert.That(ex.FieldName, Is.EqualTo("foo"));
    }

    [Test]
    public void Remove_ExistingField_DoesNotThrow()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        Assert.DoesNotThrow(() => structure.Remove("bar"));
        Assert.That(structure.Contains("bar"), Is.False);
    }

    [Test]
    public void Remove_NonExistingField_Throws()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        var ex = Assert.Throws<NonExistingFieldException>(() => structure.Remove("qux"));
        Assert.That(ex.FieldName, Is.EqualTo("qux"));
    }

    [Test]
    public void Remove_IdentityField_Throws()
    {
        var structure = new StructureIdentity(["foo", "bar"], ["foo"]);
        var ex = Assert.Throws<IdentityException>(() => structure.Remove("foo"));
        Assert.That(ex.FieldName, Is.EqualTo("foo"));
    }
}
