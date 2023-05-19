using Dojo.Net.EPOS.Server;
using Microsoft.Extensions.Logging;

const string AccountId = "<account_id>";
const string APIKey = "<api_key>";

using ILoggerFactory loggerFactory =
    LoggerFactory.Create(builder =>
        builder.AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.SingleLine = true;
            options.TimestampFormat = "HH:mm:ss ";
        }));

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

DojoTablesConnector connector = new DojoTablesConnector(AccountId, APIKey, "test-id", isSandbox: true)
{
    Logger = logger // optional
};

DemoTablesAPIServer server = new DemoTablesAPIServer();
await connector.StartAsync(server, CancellationToken.None);
