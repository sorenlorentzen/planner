using System;
using System.ComponentModel.DataAnnotations;
using Sorenlorentzen.Invokable;

namespace Planner.Core.Entities;

public class EventChoice : BaseEntity
{
    public string ChoiceText { get; set; }
    
    public string[] Voters { get; set; }

    [Timestamp]
    public uint Version { get; set; }

    public Event Event { get; set; }
    public Guid EventId { get; set; }
}
