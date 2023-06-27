using System;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Toy.EfCore.Learning.Test.Commons.Attributes;

public class ExceptionAutoFailedAttribute : BeforeAfterTestAttribute
{
    public override void Before(MethodInfo methodUnderTest)
    {
        Assert.ThrowsAny<Exception>(() => methodUnderTest.Invoke(null, null));
    }
}