using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.OrmFramework.Entities
{
    public interface IHasId<TId>
    {
        TId Id { get; set; }
    }
}
