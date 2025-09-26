using System;
using System.Linq.Expressions;
using Planner.Core.Entities;

namespace Planner.Core.Models;

public class EventChoiceModel
{
    public Guid Id { get; set; }
    public string ChoiceText { get; set; }
    public string[] Voters { get; set; }
    public Guid EventId { get; set; }

    public static Expression<Func<EventChoice, EventChoiceModel>> FromEntity => e => new EventChoiceModel
    {
        Id = e.Id,
        ChoiceText = e.ChoiceText,
        Voters = e.Voters,
        EventId = e.EventId
    };

}
