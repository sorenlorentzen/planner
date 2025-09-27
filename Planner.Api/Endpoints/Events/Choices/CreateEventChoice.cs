using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Planner.Core.Commands.EventChoices;
using Planner.Core.Models;
using Planner.Core.Queries.Events;
using Sorenlorentzen.Invokable;

namespace Planner.Api.Endpoints.Events.Choices;

public class CreateEventChoiceRequest
{
    [FromRoute(Name = "id")]
    public Guid EventId { get; set; }
    
    [FromBody]
    public InnerRequest EventChoice { get; set; }

    public class InnerRequest
    {
        public string ChoiceText { get; set; }
        
    }
}

public class CreateEventChoice : EndpointBaseAsync.WithRequest<CreateEventChoiceRequest>.WithResult<Guid>
{
    private readonly IDatabaseInvoker _invoker;

    public CreateEventChoice(IDatabaseInvoker invoker)
    {
        _invoker = invoker;
    }

    [HttpPost("/api/events/{id}/choices")]
    [Tags("Events")]
    public override async Task<Guid> HandleAsync(CreateEventChoiceRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var @event = await _invoker.ExecuteQueryAsync(new GetSingleEventQuery(request.EventId));

        var model = new EventChoiceModel
        {
            EventId = request.EventId,
            ChoiceText = request.EventChoice.ChoiceText,
        };
        var newId = await _invoker.ExecuteCommandAsync(new SaveEventChoiceCommand(model));
        await _invoker.SaveChangesAsync();

        return newId;
    }
}