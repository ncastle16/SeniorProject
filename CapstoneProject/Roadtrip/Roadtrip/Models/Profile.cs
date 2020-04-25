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
        [StringLength(501)]
        public string AboutMe { get; set; }

        [Required]
        [StringLength(20)]
        public string PrivacyFlag { get; set; }

        [Required]
        public string Follower { get; set; }

        [Required]
        public string Following { get; set; }

        [Required]
        public string PendingRequests { get; set; }

        [Required]
        public string RequestsPending { get; set; }

        public List<string> FollowerList { get; set; }
        public List<string> FollowingList { get; set; }
        public List<string> PendingRequestsList { get; set; }
        public List<string> RequestsPendingList { get; set; }
    }
}
