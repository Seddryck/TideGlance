using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance.Testing;

public class GlanceTest
{
    [Test]
    public void Refresh_AnyObject_SelectionCalled()
    {
        var foo = new Mock<IProjection>();
        foo.SetupGet(x => x.Name).Returns("foo");
        foo.Setup(x => x.Execute(It.IsAny<object>())).Returns("fooValue");

        var bar = new Mock<IProjection>();
        bar.SetupGet(x => x.Name).Returns("bar");
        bar.Setup(x => x.Execute(It.IsAny<object>())).Returns("barValue");

        var selections = new List<IProjection>() { foo.Object, bar.Object };

        var repository = new Mock<IRepository>();
        repository.Setup(x => x.Merge(It.IsAny<Record>()));

        var target = new object();  

        var glance = new Glance(selections, repository.Object, "foo");
        glance.Refresh(target);

        foo.Verify(x => x.Execute(target), Times.Once);
        bar.Verify(x => x.Execute(target), Times.Once);
    }

    [Test]
    public void Refresh_AnyObject_RepositoryCalled()
    {
        var foo = new Mock<IProjection>();
        foo.SetupGet(x => x.Name).Returns("foo");
        foo.Setup(x => x.Execute(It.IsAny<object>())).Returns("fooValue");

        var bar = new Mock<IProjection>();
        bar.SetupGet(x => x.Name).Returns("bar");
        bar.Setup(x => x.Execute(It.IsAny<object>())).Returns("barValue");

        var selections = new List<IProjection>() { foo.Object, bar.Object };

        var repository = new Mock<IRepository>();
        repository.Setup(x => x.Merge(It.IsAny<Record>()));

        var glance = new Glance(selections, repository.Object, "foo");
        glance.Refresh(new object());

        repository.Verify(x => x.Merge(It.Is<Record>(
                x => (string?)x.GetValue("foo") == "fooValue"
                && (string?)x.GetValue("bar") == "barValue"
            )), Times.Once);
    }
}
