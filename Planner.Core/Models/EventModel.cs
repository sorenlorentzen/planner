using System;
using System.Linq.Expressions;
using Planner.Core.Entities;

namespace Planner.Core.Models;

public class EventModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsMultipleChoice { get; set; }

    public static Expression<Func<Event, EventModel>> FromEntity => e => new EventModel
    {
        Id = e.Id,
        Title = e.Title,
        Description = e.Description,
        IsMultipleChoice = e.IsMultipleChoice
    };
}
