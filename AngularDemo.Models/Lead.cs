using AngularDemo.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularDemo.Models
{
    public class Lead : DeletableEntity
    {
        [Column(TypeName = "DATE")]
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public string BusinessName { get; set; }
        public string ContactPerson { get; set; }
        public string PrimaryContactNumber { get; set; }
        public string OptionalContactNumber { get; set; }
        public string Email { get; set; }
        public string BusinessAddress { get; set; }
        public Guid CityId { get; set; }
        public Guid StateId { get; set; }
        public Guid SourceId { get; set; }
        public Guid? ReferenceId { get; set; }
        public Guid? OfferedMembershipId { get; set; }
        public decimal? OfferedAmount { get; set; }
        public Guid StatusId { get; set; }
        public string Remarks { get; set; }
        
        public bool IsClient { get; set; }

        [ForeignKey(nameof(CityId))]
        public virtual Lookup City { get; set; }

        [ForeignKey(nameof(StateId))]
        public virtual Lookup State { get; set; }

        [ForeignKey(nameof(SourceId))]
        public virtual Lookup Source { get; set; }

        [ForeignKey(nameof(ReferenceId))]
        public virtual Lead ReferenceLead { get; set; }

        [ForeignKey(nameof(OfferedMembershipId))]
        public virtual Membership OfferedMembership { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual Lookup Status { get; set; }
    }
}
