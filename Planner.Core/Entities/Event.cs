using System;
using System.ComponentModel.DataAnnotations;
using Sorenlorentzen.Invokable;

namespace Planner.Core.Entities;

public class Event : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsMultipleChoice { get; set; }

    [Timestamp]
    public uint Version { get; set; }

    public ICollection<EventChoice> Choices { get; set; }
}
