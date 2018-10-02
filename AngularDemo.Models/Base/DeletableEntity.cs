using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularDemo.Models.Base
{
    public abstract class DeletableEntity : BaseEntity, IDeletableEntity
    {
        public bool Deleted { get; set; } = false;
    }
}
