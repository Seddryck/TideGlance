using System;
using System.Reflection;

namespace TideGlance
{
    abstract class TideGlanceException : Exception
    {
        public TideGlanceException(string message)
             : base(message)
        { }
    }
}
