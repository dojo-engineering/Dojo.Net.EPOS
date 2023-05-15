using Dojo.Net.EPOS.Server;
using Microsoft.Extensions.Logging;

const string AccountId = "alex0000";
const string APIKey = "57c2800b-069a-4232-a70a-5cea81102140";//"Payment1"; //


using ILoggerFactory loggerFactory =
    LoggerFactory.Create(builder =>
        builder.AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.SingleLine = true;
            options.TimestampFormat = "HH:mm:ss ";
        }));

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

DojoTablesConnector connector = new DojoTablesConnector(AccountId, APIKey, "asdf", isSandbox: true)
{
    ResellerId = "alex0000", // optional    
    Logger = logger // optional
};

DemoTablesAPIServer server = new DemoTablesAPIServer();
await connector.StartAsync(server, CancellationToken.None);
