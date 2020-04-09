namespace Roadtrip.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LikedRoute
    {
        [Key]
        public int LRID { get; set; }

       public int RouteID { get; set; }
        public string UserName { get; set; }
    }
}