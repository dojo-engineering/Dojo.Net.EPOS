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

# Error handling

# Logging
