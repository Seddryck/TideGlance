﻿using System;
using System.Reflection;

namespace TideGlance;

public abstract class TideGlanceException : ApplicationException
{
    public TideGlanceException(string message)
         : base(message)
    { }

    public TideGlanceException(string message, Exception innerException)
         : base(message, innerException)
    { }
}