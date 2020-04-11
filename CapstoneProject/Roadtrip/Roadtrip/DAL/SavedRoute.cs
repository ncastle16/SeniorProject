namespace Roadtrip.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SavedRoute
    {
        [Key]
        public int SRID { get; set; }

        [Required]
        public string Route { get; set; }

        public string RouteName { get; set; }

        public DateTime Timestamp { get; set; }

        [Required]
        [StringLength(256)]
        public string Username { get; set; }
    }
}
