using System;
using Microsoft.EntityFrameworkCore;
using Planner.Core.Entities;
using Planner.Core.Models;
using Sorenlorentzen.Invokable;

namespace Planner.Core.Queries.Events;

public class GetAllEventsQuery : BaseQuery<EventModel[]>
{

    public override async Task<EventModel[]> ExecuteAsync()
    {
        var query = GetQuery<Event>();

        var models = await query.Select(EventModel.FromEntity).ToArrayAsync();

        return models;
    }
}
