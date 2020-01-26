namespace ClassProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EventTeam
    {
        [Key]
        public int ETeamsID { get; set; }

        public int TeamID { get; set; }

        public int EventID { get; set; }

        public virtual Event Event { get; set; }

        public virtual Team Team { get; set; }
    }
}
