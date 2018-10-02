using AngularDemo.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularDemo.Models
{
    public class LeadFollowup : DeletableEntity
    {
        public DateTime Date { get; set; }
        public Guid StatusId { get; set; }
        public DateTime? NextFollowupDate { get; set; }
        public string Remarks { get; set; }
    }
}
