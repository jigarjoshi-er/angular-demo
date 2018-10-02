using AngularDemo.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularDemo.Models
{
    public class Membership : DeletableEntity
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
