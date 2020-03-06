namespace Roadtrip.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SavedRoutesModel : DbContext
    {
        public SavedRoutesModel()
            : base("name=SavedRoutesModel")
        {
        }

        public virtual DbSet<SavedRoute> SavedRoutes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
