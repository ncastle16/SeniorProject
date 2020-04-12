namespace Roadtrip.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SavedRoutesModel : DbContext
    {
        public SavedRoutesModel()
            : base("name=SavedContext")
        {
        }

        public virtual DbSet<SavedRoute> SavedRoutes { get; set; }
      public virtual DbSet<LikedRoute> LikedRoute { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
