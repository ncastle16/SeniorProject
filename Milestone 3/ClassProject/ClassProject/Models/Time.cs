namespace ClassProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Time
    {
        [Key]
        public int TID { get; set; }

        public int AthleteID { get; set; }

        public int EventID { get; set; }

        [Column("Time")]
        public TimeSpan Time1 { get; set; }

        public virtual Athlete Athlete { get; set; }

        public virtual Event Event { get; set; }
    }
}
