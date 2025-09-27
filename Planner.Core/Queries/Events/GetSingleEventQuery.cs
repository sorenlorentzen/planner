using Microsoft.EntityFrameworkCore;
using Planner.Core.Entities;
using Planner.Core.Models;
using Sorenlorentzen.Invokable;

namespace Planner.Core.Queries.Events;

public class GetSingleEventQuery : BaseQuery<EventModel?>
{
    private readonly Guid _id;

    public GetSingleEventQuery(Guid id)
    {
        _id = id;
    }

    public override async Task<EventModel?> ExecuteAsync()
    {
        var query = GetQuery<Event>();
        query = query.Where(x => x.Id == _id);

        var model = await query.Select(EventModel.FromEntity).SingleOrDefaultAsync();
        return model;
    }
}