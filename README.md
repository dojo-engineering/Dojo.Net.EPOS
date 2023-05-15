# Dojo.Net.EPOS

Dojo.Net.EPOS is a NuGet package designed to simplify the integration with the Dojo Tables API for EPOS systems. This package provides a set of interfaces and classes that make it easier to interact with the API, manage WebSocket connections, and handle different types of requests and responses.

You can read more details about [Dojo Tables API](https://docs.dojo.tech/tables/)

## Installation
To install the Dojo.Net.EPOS NuGet package, run the following command in your project directory:

```sh
dotnet add package Dojo.Net.EPOS
```

Alternatively, you can use the NuGet Package Manager in Visual Studio to search for and install the Dojo.Net.EPOS package.

## Usage
To use the Dojo.Net.EPOS package in your project, follow these steps:

### Step 1: Implement the ITablesAPIServer Interface
The first step is to create a class that implements the ITablesAPIServer interface. This interface contains several methods that correspond to different API endpoints. You should provide custom implementations for each method to handle the corresponding requests and return the appropriate responses.

Here's an example implementation of the ITablesAPIServer interface:

```csharp
public class MyTablesAPIServer : ITablesAPIServer
{
    // Implement the methods from the ITablesAPIServer interface here
    // For example:
    public async Task<ListSessionsResponse> AcceptMessageAsync(ListSessionsRequest request)
    {
        // Your custom implementation for handling the ListSessions request
    }

    // ... other methods from the ITablesAPIServer interface
}
```

### Step 2: Instantiate the DojoTablesConnector Class
Create an instance of the DojoTablesConnector class, passing the required parameters:
* accountId
* apiKey
* softwareHouseId
* isSandbox (optional, set to true for the sandbox environment or false for the production environment)

```csharp
var dojoTablesConnector = new DojoTablesConnector(accountId, apiKey, softwareHouseId, isSandbox);
```

### Step 3: Pass the ITablesAPIServer Instance to the DojoTablesConnector
Instantiate your implementation of the ITablesAPIServer interface and pass it to the DojoTablesConnector instance:

```csharp
var myTablesAPIServer = new MyTablesAPIServer();
```
Now, call the StartAsync method on the dojoTablesConnector instance and pass the myTablesAPIServer instance and a CancellationToken:

```csharp
await dojoTablesConnector.StartAsync(myTablesAPIServer, cancellationToken);
```

That's it! The DojoTablesConnector class will now manage the WebSocket connection and handle communication with the Dojo Tables API using your custom implementation of the ITablesAPIServer interface.

# Stop connector
To stop the DojoTablesConnector, you can simply cancel the CancellationToken that you passed to the StartAsync method. This will close the WebSocket connection and stop any ongoing communication with the Dojo Tables API.

Here's an example of how you can stop the DojoTablesConnector using a CancellationTokenSource:

```csharp
// Instantiate a CancellationTokenSource
var cancellationTokenSource = new CancellationTokenSource();

// Pass the CancellationToken from the CancellationTokenSource to the StartAsync method
await dojoTablesConnector.StartAsync(myTablesAPIServer, cancellationTokenSource.Token);

// ... perform other tasks, or wait for a specific event

// When you're ready to stop the DojoTablesConnector, call Cancel on the CancellationTokenSource
cancellationTokenSource.Cancel();
```

By calling Cancel() on the CancellationTokenSource, the CancellationToken that was passed to StartAsync will be marked as canceled, and the DojoTablesConnector will gracefully stop its operation and close the WebSocket connection.

# Error handling
When your EPOS can't process a request while implementing the ITablesAPIServer interface, it's important to throw the correct exception with a specific TablesErrorCode. This will ensure proper error handling and communication with the Dojo server. The TablesException will be serialized and sent back to the Dojo server, allowing it to understand the nature of the error and take appropriate action.

For example, consider a situation where you are implementing the AcceptMessageAsync(GetSessionRequest request) method from the ITablesAPIServer interface. If your EPOS can't find the requested session, you should throw a TablesException with the SessionNoSuchSession error code:

```csharp
public async Task<GetSessionResponse> AcceptMessageAsync(GetSessionRequest request)
{
    // ... Your code to retrieve the session

    if (sessionDoesNotExist)
    {
        throw new TablesException(TablesErrorCode.SessionNoSuchSession, "The requested session does not exist.");
    }

    // ... Your code to return a GetSessionResponse
}
```
By throwing the TablesException with the appropriate error code and message, your EPOS is providing valuable information about the error scenario to the Dojo server. The Dojo server can then use this information to handle the error and communicate it back to the client or take other actions as necessary.

# Logging
