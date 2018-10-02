using AngularDemo.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularDemo.Models
{
    public class LookupType : DeletableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Lookup : DeletableEntity
    {
        public string Name { get; set; }
        public Guid TypeId { get; set; }
        public int? Order { get; set; }
        public Guid? ParentTypeId { get; set; }
        public Guid? ParentId { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(TypeId))]
        public virtual LookupType LookupType { get; set; }

        [ForeignKey(nameof(ParentTypeId))]
        public virtual LookupType ParentLookupType { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Lookup ParentLookup { get; set; }
    }
}