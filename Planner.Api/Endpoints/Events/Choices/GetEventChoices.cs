using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Planner.Core.Models;
using Planner.Core.Queries.Events;
using Sorenlorentzen.Invokable;

namespace Planner.Api.Endpoints.Events.Choices;

public class GetEventChoicesRequest
{
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
}

public class GetEventChoices : EndpointBaseAsync.WithRequest<GetEventChoicesRequest>.WithResult<EventChoiceModel[]>
{
    private readonly IDatabaseInvoker _invoker;

    public GetEventChoices(IDatabaseInvoker invoker)
    {
        _invoker = invoker;
    }

    [HttpGet("/events/{id}/choices")]
    [Tags("Events")]
    public override async Task<EventChoiceModel[]> HandleAsync(GetEventChoicesRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var choices = await _invoker.ExecuteQueryAsync(new GetAllEventChoicesQuery(request.Id));

        return choices;
    }
}