![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/dotnet-feats/feats.client/2)
[![Build Status](https://dev.azure.com/dotnet-feats/feats.client/_apis/build/status/dotnet-feats.feats.client?branchName=refs%2Fpull%2F2%2Fmerge)](https://dev.azure.com/dotnet-feats/feats.client/_build/latest?definitionId=2&branchName=refs%2Fpull%2F2%2Fmerge)
[![Stage](https://img.shields.io/badge/Stage-alpha-blue)]()

# Feats Evaluation .Net client

Well hello there!

This project is meant to facilitate your life when using the Feats servers (Evaluation & Management), 
providing a simple interface to use in your .Net projects.

## How to use

### Client configuration

In order for the client to phone hoe, it needs to know the host for the evaluation service. 
This host is fed through configuration: most probably your `appsettings.json` file, or your custom configuration provider.

No matter what configuration provider you have, the client requires the following section to be present (json example):
```json
{
    "feats": {
        "host": "your-host-must-be-a-proper-uri",
        "request_timeout_in_seconds" : 300,
        "cache_timeout_in_seconds" : 30
    }
}
```

`request_timeout_in_seconds` is optional and the default value is 300.
`cache_timeout_in_seconds` is optional and the default value is 30.

### Injecting client in DI

In your `Startup.cs` file, in the service collection section, add the following line:

```c#   
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            [...]

            services.AddFeatsEvaluationClient(this._configuration);

            [...]
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            [...]
        }
    }
}

```

### Using in your classes

You simply need to use the interface `IFeatsEvaluationClient` in your class:

```c#

public class IAmSomeRandomThing
{
    private readonly IFeatsEvaluationClient _featsClient;

    public IAmSomeRandomThing(IFeatsEvaluationClient featsClient)
    {
        this._featsClient = featsClient;
    }
}
```

## Evaluating a feature

Each evaluation request should be made with a `FeatureEvaluationRequestBuilder` object.
Each strategy has its own way of building the request, you can also chain strategies.

### Evaluating IsOn Strategy

```c#
var request = new FeatureEvaluationRequestBuilder()
    .WithName("featName")
    .WithPath("featPath")
    .Build();

val answer = await this._featsClient.isOn(request);
```

### Evaluating IsInList Strategy

```c#
var request = new FeatureEvaluationRequestBuilder()
    .WithName("featName")
    .WithPath("featPath")
    .WithIsInList("listName", "listItem")
    .Build();

val answer = await this._featsClient.isOn(request);
```

Multiple lists to check?

```c#
var request = new FeatureEvaluationRequestBuilder()
    .WithName("featName")
    .WithPath("featPath")
    .WithIsInList("listName", "listItem")
    .WithIsInList("listTwoName", "listTwoItem")
    [...]
    .Build();

val answer = await this._featsClient.isOn(request);
```

You get the point :)

### Evaluating IsBefore Strategy

```c#
var request = new FeatureEvaluationRequestBuilder()
    .WithName("featName")
    .WithPath("featPath")
    .WithIsBefore(yourDateTime)
    .Build();

val answer = await this._featsClient.isOn(request);
```

### Evaluating IsAfter Strategy

```c#
var request = new FeatureEvaluationRequestBuilder()
    .WithName("featName")
    .WithPath("featPath")
    .WithIsAfter(yourDateTime)
    .Build();

val answer = await this._featsClient.isOn(request);
```

### Evaluating IsGreaterThan Strategy

```c#
var request = new FeatureEvaluationRequestBuilder()
    .WithName("featName")
    .WithPath("featPath")
    .WithIsGreaterThan(yourNumber)
    .Build();

val answer = await this._featsClient.isOn(request);
```

### Evaluating IsLowerThan Strategy

```c#
var request = new FeatureEvaluationRequestBuilder()
    .WithName("featName")
    .WithPath("featPath")
    .WithIsLowerThan(yourNumber)
    .Build();

val answer = await this._featsClient.isOn(request);
```
