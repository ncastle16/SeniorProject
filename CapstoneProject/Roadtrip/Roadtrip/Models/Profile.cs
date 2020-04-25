namespace Roadtrip.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Profile")]
    public partial class Profile
    {
        [Key]
        public int PPID { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [Required]
        public string Friends { get; set; }

        [Required]
        [StringLength(501)]
        public string AboutMe { get; set; }

        [Required]
        [StringLength(20)]
        public string PrivacyFlag { get; set; }
    }
}
