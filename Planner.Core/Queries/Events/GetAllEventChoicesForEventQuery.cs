using Microsoft.EntityFrameworkCore;
using Planner.Core.Entities;
using Planner.Core.Models;
using Sorenlorentzen.Invokable;

namespace Planner.Core.Queries.Events;

public class GetAllEventChoicesForEventQuery : BaseQuery<EventChoiceModel[]>
{
    private readonly Guid _eventId;

    public GetAllEventChoicesForEventQuery(Guid eventId)
    {
        _eventId = eventId;
    }

    public override async Task<EventChoiceModel[]> ExecuteAsync()
    {
        var query = GetQuery<EventChoice>();

        var models = await query.Select(EventChoiceModel.FromEntity).ToArrayAsync();
        return models;
    }
}