using System;
using System.Reflection;

namespace TideGlance;
public abstract class TideGlanceException : ApplicationException
{
    public TideGlanceException(string message)
         : base(message)
    { }
}
