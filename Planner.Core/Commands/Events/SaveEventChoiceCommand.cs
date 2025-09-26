using System;
using Planner.Core.Entities;
using Planner.Core.Models;
using Sorenlorentzen.Invokable;

namespace Planner.Core.Commands.Events;

public class SaveEventChoiceCommand : BaseCommand<Guid>
{
    private readonly EventChoiceModel _model;

    public SaveEventChoiceCommand(EventChoiceModel model)
    {
        this._model = model;
    }

    public override async Task<Guid> ExecuteAsync()
    {
        EventChoice choice;
        if (_model.Id != default)
        {
            choice = await GetSingleEntity<EventChoice>(_model.Id);
        }
        else
        {
            choice = new EventChoice
            {
                Id = Guid.CreateVersion7(),
                EventId = _model.EventId,
                Voters = [],
            };
            AddEntity(choice);
        }

        choice.ChoiceText = _model.ChoiceText;

        return choice.Id;
    }
}
