using Newtonsoft.Json;
using WebApiGateway.UnitTests.Infrastructure;
using Xunit.Abstractions;

namespace WebApiGateway.UnitTests.Controllers;

public abstract class BaseTest
{
    protected readonly XUnitTestConsoleLogger Logger;

    protected BaseTest(ITestOutputHelper testOutputHelper, string category = "test")
    {
        Logger = new XUnitTestConsoleLogger(testOutputHelper, category);
    }

    protected string Serialize(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
}