namespace Roadtrip.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Event
    {
        [Key]
        public int EID { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public string Route { get; set; }

        [Required]
        public string Host { get; set; }

        public DateTime Start { get; set; }

        public DateTime Finish { get; set; }

        [Required]
        public string Attending { get; set; }

        [Required]
        [StringLength(10)]
        public string Privacy { get; set; }
    }
}
