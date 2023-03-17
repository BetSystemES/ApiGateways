using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace WebApiGateway.UnitTests.Infrastructure;

public class XunitLoggerProvider : ILoggerProvider
{
    private readonly ITestOutputHelper _testOutputHelper;

    public XunitLoggerProvider(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new XUnitTestConsoleLogger(_testOutputHelper, categoryName);
    }

    public void Dispose()
    {
    }
}