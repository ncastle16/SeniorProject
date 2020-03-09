namespace ClassProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ClassContext : DbContext
    {
        public ClassContext()
            : base("name=ClassContext_Azure")
        {
        }

        public virtual DbSet<Athlete> Athletes { get; set; }
        public virtual DbSet<AthleteTeam> AthleteTeams { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventTeam> EventTeams { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Time> Times { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>()
                .HasMany(e => e.AthleteTeams)
                .WithRequired(e => e.Athlete)
                .HasForeignKey(e => e.AthleteID);

            modelBuilder.Entity<Athlete>()
                .HasMany(e => e.Times)
                .WithRequired(e => e.Athlete)
                .HasForeignKey(e => e.AthleteID);

            modelBuilder.Entity<Coach>()
                .HasMany(e => e.Teams)
                .WithRequired(e => e.Coach)
                .HasForeignKey(e => e.CoachID);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.EventTeams)
                .WithRequired(e => e.Event)
                .HasForeignKey(e => e.EventID);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Times)
                .WithRequired(e => e.Event)
                .HasForeignKey(e => e.EventID);

            modelBuilder.Entity<EventType>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.EventType)
                .HasForeignKey(e => e.EventTypeID);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Location)
                .HasForeignKey(e => e.LocationID);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.AthleteTeams)
                .WithRequired(e => e.Team)
                .HasForeignKey(e => e.TeamID);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.EventTeams)
                .WithRequired(e => e.Team)
                .HasForeignKey(e => e.TeamID);
        }
    }
}
