using System;
using Microsoft.EntityFrameworkCore;

namespace Planner.Core.Entities;

public class DatabaseContext : DbContext
{
    public DbSet<Event> Events { get; set; }
}
