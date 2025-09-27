using System.Runtime.CompilerServices;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Planner.Core.Models;
using Planner.Core.Queries.Events;
using Sorenlorentzen.Invokable;

namespace Planner.Api.Endpoints.Events;

public class GetEventRequest
{
    [FromRoute(Name = "id")] 
    public Guid Id { get; set; }
}

public class GetEventResponse : EventModel
{
    public EventChoiceModel[] Choices { get; set; }

    public static GetEventResponse FromModel(EventModel m)
    {
        return new GetEventResponse
        {
            Id = m.Id,
            Title = m.Title,
            Description = m.Description,
            IsMultipleChoice = m.IsMultipleChoice,
        };
    }
}

public class GetEvent : EndpointBaseAsync.WithRequest<GetEventRequest>.WithResult<GetEventResponse>
{
    private readonly IDatabaseInvoker _invoker;

    public GetEvent(IDatabaseInvoker invoker)
    {
        _invoker = invoker;
    }

    [HttpGet("/api/events/{id}")]
    [Tags("Events")]
    public override async Task<GetEventResponse> HandleAsync(GetEventRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var @event = await _invoker.ExecuteQueryAsync(new GetSingleEventQuery(request.Id));
        var choices = await _invoker.ExecuteQueryAsync(new GetAllEventChoicesForEventQuery(@event.Id));

        var response = GetEventResponse.FromModel(@event);
        response.Choices = choices;

        return response;
    }
}