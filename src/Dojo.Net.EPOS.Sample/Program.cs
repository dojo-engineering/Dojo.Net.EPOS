using Dojo.Net.EPOS.Server;

const string AccountId = "alex0000";
const string APIKey = "57c2800b-069a-4232-a70a-5cea81102140";//"Payment1"; //

DojoTablesConnector connector = new DojoTablesConnector(AccountId, APIKey, "asdf", isSandbox: true)
{
    ResellerId = "alex0000",    
};

DemoTablesAPIServer server = new DemoTablesAPIServer();
await connector.StartAsync(server, CancellationToken.None);
