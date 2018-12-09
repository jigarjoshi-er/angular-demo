using AngularDemo.Models.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularDemo.Models
{
    public class LookupType : DeletableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        #region Static Members (For querying data using Lookup Type)

        public const string Country = "Country";
        public const string State = "State";
        public const string City = "City";
        public const string Source = "Source";
        public const string EnquiryStatus = "Enquiry Status";
        public const string ExpenseType = "Expense Type";

        #endregion
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