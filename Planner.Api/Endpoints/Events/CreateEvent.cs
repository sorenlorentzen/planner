using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Planner.Core.Commands.Events;
using Planner.Core.Models;
using Sorenlorentzen.Invokable;

namespace Planner.Api.Endpoints.Events;

public class CreateEventModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsMultipleChoice { get; set; }

    public EventModel ToEventModel()
    {
        return new EventModel
        {
            Title = Title,
            Description = Description,
            IsMultipleChoice = IsMultipleChoice,
        };
    }
}

public class CreateEvent : EndpointBaseAsync.WithRequest<CreateEventModel>.WithResult<Guid>
{
    private readonly IDatabaseInvoker _invoker;

    public CreateEvent(IDatabaseInvoker invoker)
    {
        _invoker = invoker;
    }

    [HttpPost("/api/events")]
    [Tags("Events")]
    public override async Task<Guid> HandleAsync(CreateEventModel request, CancellationToken cancellationToken = new CancellationToken())
    {
        var model = request.ToEventModel();
        var id = await _invoker.ExecuteCommandAsync(new SaveEventCommand(model));

        await _invoker.SaveChangesAsync();

        return id;
    }
}