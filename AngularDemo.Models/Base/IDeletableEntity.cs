using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularDemo.Models.Base
{
    interface IDeletableEntity : IBaseEntity
    {
        bool Deleted { get; set; }
    }
}