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
        var foo = new Mock<ISelection>();
        foo.SetupGet(x => x.Name).Returns("foo");
        foo.Setup(x => x.Execute(It.IsAny<object>())).Returns("fooValue");

        var bar = new Mock<ISelection>();
        bar.SetupGet(x => x.Name).Returns("bar");
        bar.Setup(x => x.Execute(It.IsAny<object>())).Returns("barValue");

        var selections = new List<ISelection>() { foo.Object, bar.Object };

        var repository = new Mock<IRepository>();
        repository.Setup(x => x.Merge(It.IsAny<IDictionary<string, object?>>()));

        var target = new object();  

        var glance = new Glance(selections, repository.Object);
        glance.Refresh(target);

        foo.Verify(x => x.Execute(target), Times.Once);
        bar.Verify(x => x.Execute(target), Times.Once);
    }

    [Test]
    public void Refresh_AnyObject_RepositoryCalled()
    {
        var foo = new Mock<ISelection>();
        foo.SetupGet(x => x.Name).Returns("foo");
        foo.Setup(x => x.Execute(It.IsAny<object>())).Returns("fooValue");

        var bar = new Mock<ISelection>();
        bar.SetupGet(x => x.Name).Returns("bar");
        bar.Setup(x => x.Execute(It.IsAny<object>())).Returns("barValue");

        var selections = new List<ISelection>() { foo.Object, bar.Object };

        var repository = new Mock<IRepository>();
        repository.Setup(x => x.Merge(It.IsAny<IDictionary<string, object?>>()));

        var glance = new Glance(selections, repository.Object);
        glance.Refresh(new object());

        repository.Verify(x => x.Merge(It.Is<IDictionary<string, object?>>(
                x => x.ContainsKey("foo") && (string?)x["foo"] == "fooValue"
                && x.ContainsKey("bar") && (string?)x["bar"] == "barValue"
            )), Times.Once);
    }
}
