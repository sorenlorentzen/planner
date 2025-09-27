using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Planner.Core.Models;
using Planner.Core.Queries.Events;
using Sorenlorentzen.Invokable;

namespace Planner.Api.Endpoints.Events;

public class GetAllEvents : EndpointBaseAsync.WithoutRequest.WithResult<EventModel[]>
{
    private readonly IDatabaseInvoker  _invoker;

    public GetAllEvents(IDatabaseInvoker invoker)
    {
        _invoker = invoker;
    }
    
    [HttpGet("api/events")]
    [Tags("Events")]
    public override async Task<EventModel[]> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var models = await _invoker.ExecuteQueryAsync(new GetAllEventsQuery());
        return models;
    }
}