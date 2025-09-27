using System.Text.Json.Serialization;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Planner.Core.Commands.Events;
using Planner.Core.Models;
using Planner.Core.Queries.Events;
using Sorenlorentzen.Invokable;

namespace Planner.Api.Endpoints.Events;

public class UpdateEventRequest
{
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
    
    [FromBody]
    public CreateEventRequest Event { get; set; }
}
public class UpdateEvent : EndpointBaseAsync.WithRequest<UpdateEventRequest>.WithResult<Guid>
{
    private readonly IDatabaseInvoker _invoker;

    public UpdateEvent(IDatabaseInvoker invoker)
    {
        _invoker = invoker;
    }
    
    [HttpPut("/api/events/{id}")]
    [Tags("Events")]
    public override async Task<Guid> HandleAsync([FromRoute]UpdateEventRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var @event = await _invoker.ExecuteQueryAsync(new GetSingleEventQuery(request.Id));
        request.Event.MergeIntoEvent(@event);

        await _invoker.ExecuteCommandAsync(new SaveEventCommand(@event));
        await _invoker.SaveChangesAsync();
        return @event.Id;
    }
}