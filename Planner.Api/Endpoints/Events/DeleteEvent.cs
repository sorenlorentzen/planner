using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Planner.Core.Commands.Events;
using Planner.Core.Queries.Events;
using Sorenlorentzen.Invokable;

namespace Planner.Api.Endpoints.Events;

public class DeleteEventRequest
{
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
}
public class DeleteEvent : EndpointBaseAsync.WithRequest<DeleteEventRequest>.WithoutResult
{
    private readonly IDatabaseInvoker _invoker;

    public DeleteEvent(IDatabaseInvoker invoker)
    {
        _invoker = invoker;
    }

    [HttpDelete("/api/events/{id}")]
    [Tags("Events")]
    public override async Task HandleAsync(DeleteEventRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var @event = await _invoker.ExecuteQueryAsync(new GetSingleEventQuery(request.Id));
        await _invoker.ExecuteCommandAsync(new DeleteEventCommand(@event.Id));

        await _invoker.SaveChangesAsync();
    }
}