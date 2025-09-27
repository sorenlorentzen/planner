using Microsoft.EntityFrameworkCore;
using Planner.Core.Entities;
using Sorenlorentzen.Invokable;

namespace Planner.Core.Commands.Events;

public class DeleteEventCommand : BaseCommand<Guid>
{
    private readonly Guid _id;

    public DeleteEventCommand(Guid id)
    {
        _id = id;
    }

    public override async Task<Guid> ExecuteAsync()
    {
        var entity = await GetSingleEntity<Event>(_id);
        DeleteEntity(entity);
        
        //Delete related event choices
        var query = GetQuery<EventChoice>();
        query = query.Where(x => x.EventId == entity.Id);
        var choicesToDelete = await query.ToArrayAsync();
        foreach (var eventChoice in choicesToDelete)
        {
            DeleteEntity(eventChoice, entity.Deleted);
        }

        return entity.Id;
    }
}