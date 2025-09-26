using System;
using System.Runtime.CompilerServices;
using Planner.Core.Entities;
using Planner.Core.Models;
using Sorenlorentzen.Invokable;

namespace Planner.Core.Commands.Events;

public class SaveEventCommand : BaseCommand<Guid>
{
    private readonly EventModel _eventModel;

    public SaveEventCommand(EventModel eventModel)
    {
        _eventModel = eventModel;
    }
    public override async Task<Guid> ExecuteAsync()
    {
        Event e;
        if (_eventModel.Id != default)
        {
            e = await GetSingleEntity<Event>(_eventModel.Id);
        }
        else
        {
            e = new Event
                {
                    Id = Guid.CreateVersion7(),
                };
            AddEntity(e);
        }

        e.Title = _eventModel.Title;
        e.Description = _eventModel.Description;
        e.IsMultipleChoice = _eventModel.IsMultipleChoice;

        return e.Id;
    }
}
