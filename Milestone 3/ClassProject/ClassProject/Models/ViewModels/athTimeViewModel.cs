using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassProject.Models.ViewModels
{
    public class athTimeViewModel
    {
       
        public virtual ICollection<Time> Times { get; set; }
    }
}